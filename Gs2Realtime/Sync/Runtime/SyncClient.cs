using System;
using System.Collections.Generic;

namespace Gs2.Realtime.Sync
{
    // Invoke Receive/Set/Flush on one thread (Unity's main thread). The transport
    // must copy or consume the provided buffer before TrySend returns true.
    public delegate bool TrySend(byte[] data, int count);
    public enum ObjectMode : byte { Player, Lease, Server }
    public enum LeaseAction : uint { Acquire = 1, Renew, Release }
    public enum CommandResult : uint { Success, MissingObject, Forbidden, LeaseBusy, StaleEpoch, InvalidOperation, AlreadyApplied, StaleRequest }
    public sealed class NetworkObject
    {
        public uint Id
        {
            get; internal set;
        }
        public uint Prefab
        {
            get; internal set;
        }
        public uint Owner
        {
            get; internal set;
        }
        public uint Request
        {
            get; internal set;
        }
        public uint Fields
        {
            get; internal set;
        }
        public ObjectMode Mode { get; internal set; }
        public uint Controller { get; internal set; }
        public uint Epoch { get; internal set; }
        // Remaining server lease time at the last notification; not a client clock.
        public uint LeaseMilliseconds { get; internal set; }
        public event Action AuthorityChanged;
        internal void NotifyAuthority() { AuthorityChanged?.Invoke(); }
        public readonly int[] Values = new int[32];
        public event Action<uint> Changed;
        internal void Notify(uint mask)
        {
            Changed?.Invoke(mask);
        }
        internal readonly Record Pending = new Record();
        internal readonly int[] Sent = new int[32];
    }
    public sealed class SyncClient
    {
        readonly TrySend send;
        readonly SyncCodec input = new SyncCodec();
        readonly Record[] output = new Record[SyncCodec.MaxRecords];
        readonly Record command = new Record();
        readonly byte[] buffer = new byte[SyncCodec.MaxPacket];
        readonly Dictionary<uint, NetworkObject> objects = new Dictionary<uint, NetworkObject>();
        uint nextRequest = 1, nextCommand = 1;
        bool receiving;
        public uint ConnectionId
        {
            get; private set;
        }
        public bool Ready
        {
            get; private set;
        }
        public bool CanSend { get { return Ready && !receiving; } }
        public IEnumerable<NetworkObject> Objects
        {
            get
            {
                return objects.Values;
            }
        }
        public event Action<NetworkObject> Spawned;
        public event Action<NetworkObject, uint> Changed;
        public event Action<NetworkObject> Despawned;
        // RPC args must be consumed in this callback; they borrow the input packet.
        public event Action<NetworkObject, uint, uint, byte[], int, int> RpcReceived;
        public event Action<uint> Rejected;
        public event Action<NetworkObject> AuthorityChanged;
        public event Action<uint, uint, CommandResult> CommandCompleted;
        public SyncClient(uint connectionId, TrySend trySend)
        {
            if (connectionId == 0 || trySend == null)
                throw new ArgumentException();
            ConnectionId = connectionId;
            send = trySend;
        }
        public NetworkObject Find(uint id)
        {
            NetworkObject v;
            return objects.TryGetValue(id, out v) ? v : null;
        }
        void CheckReady()
        {
            if (!Ready || receiving)
                throw new InvalidOperationException("Wait for snapshot completion; queue callback commands for the next Update");
        }
        bool SendCommand(Op op)
        {
            output[0] = command;
            int n = SyncCodec.Encode(buffer, op, output, 1);
            return send(buffer, n);
        }
        public uint Spawn(uint prefab, uint mask, int[] values)
        {
            CheckReady();
            if (nextRequest == 0)
                throw new InvalidOperationException("Request IDs exhausted");
            command.Clear();
            command.Id = nextRequest;
            command.Prefab = prefab;
            command.Mask = mask;
            for (int f = 0; f < 32; f++)
                if ((mask & (1u << f)) != 0)
                {
                    if (values == null || f >= values.Length)
                        throw new ArgumentException("Missing field");
                    command.Values[f] = values[f];
                }
            if (!Flush() || !SendCommand(Op.SpawnRequest))
                return 0;
            return nextRequest++;
        }
        NetworkObject Owned(uint id)
        {
            var o = Find(id);
            if (o == null || o.Owner != ConnectionId)
                throw new InvalidOperationException("Object is not owned by this connection");
            return o;
        }
        public void Set(uint id, int field, int value)
        {
            CheckReady();
            var o = Find(id);
            if (!CanWrite(id)) throw new InvalidOperationException("No write authority");
            if (field < 0 || field >= 32 || (o.Fields & (1u << field)) == 0)
                throw new ArgumentOutOfRangeException("field");
            o.Pending.Values[field] = value;
            if (o.Sent[field] == value)
                o.Pending.Mask &= ~(1u << field);
            else
                o.Pending.Mask |= 1u << field;
        }
        public bool Flush()
        {
            CheckReady();
            return FlushMode(ObjectMode.Player, Op.Update) && FlushMode(ObjectMode.Lease, Op.LeasedUpdate);
        }
        bool FlushMode(ObjectMode mode, Op op) {
            int count = 0;
            foreach (var o in objects.Values)
            {
                if (o.Mode != mode || o.Pending.Mask == 0)
                    continue;
                output[count++] = o.Pending;
                if (count == output.Length)
                {
                    if (!FlushBatch(op, count))
                        return false;
                    count = 0;
                }
            }
            return count == 0 || FlushBatch(op, count);
        }
        bool FlushBatch(Op op, int count)
        {
            int n = SyncCodec.Encode(buffer, op, output, count);
            if (!send(buffer, n))
                return false;
            for (int i = 0; i < count; i++)
            {
                var p = output[i];
                var o = objects[p.Id];
                for (int f = 0; f < 32; f++)
                    if ((p.Mask & (1u << f)) != 0)
                        o.Sent[f] = p.Values[f];
                p.Mask = 0;
            }
            return true;
        }
        public bool CanWrite(uint id) {
            var o = Find(id);
            return Ready && o != null && (o.Mode == ObjectMode.Player ? o.Owner == ConnectionId : o.Mode == ObjectMode.Lease && o.Controller == ConnectionId);
        }
        public uint Lease(uint id, LeaseAction action) {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Lease) throw new InvalidOperationException("Not a leased object");
            if (!Flush()) return 0;
            command.Clear(); command.Id = id; command.Epoch = o.Epoch; command.Method = (uint)action;
            return SendShared(Op.Lease);
        }
        public uint Interact(uint id, uint method, byte[] args = null, int count = 0) {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Server) throw new InvalidOperationException("Not a server object");
            if (!Flush()) return 0;
            command.Clear(); command.Id = id; command.Method = method; command.Args = args; command.ArgsCount = count;
            return SendShared(Op.Interact);
        }
        uint SendShared(Op op) {
            if (nextCommand == 0) throw new InvalidOperationException("Command IDs exhausted");
            command.Request = nextCommand;
            return SendCommand(op) ? nextCommand++ : 0;
        }
        public bool Destroy(uint id)
        {
            CheckReady();
            Owned(id);
            if (!Flush())
                return false;
            command.Clear();
            command.Id = id;
            return SendCommand(Op.Despawn);
        }
        public bool Rpc(uint id, uint method, byte target, byte[] args, int count)
        {
            CheckReady();
            Owned(id);
            if (!Flush())
                return false;
            command.Clear();
            command.Id = id;
            command.Method = method;
            command.Target = target;
            command.Args = args;
            command.ArgsCount = count;
            return SendCommand(Op.Rpc);
        }
        // Call Reset when the SDK changes MyConnectionId; then consume a fresh snapshot.
        public void Reset(uint connectionId)
        {
            if (connectionId == 0 || receiving)
                throw new ArgumentException();
            ConnectionId = connectionId;
            Ready = false;
            ClearObjects();
        }
        void ClearObjects()
        {
            foreach (var o in objects.Values)
                Despawned?.Invoke(o);
            objects.Clear();
        }
        public void Receive(byte[] bytes, int count)
        {
            if (receiving)
                throw new InvalidOperationException("Reentrant Receive");
            input.Decode(bytes, count);
            receiving = true;
            try
            {
                for (int i = 0; i < input.Count; i++)
                {
                    var r = input.Records[i];
                    NetworkObject o;
                    switch (input.Operation)
                    {
                        case Op.SnapshotBegin:
                            if (r.Id != 1)
                                throw new FormatException("Unsupported schema");
                            Ready = false;
                            ClearObjects();
                            break;
                        case Op.SnapshotEnd:
                            Ready = true;
                            break;
                        case Op.Spawn:
                        case Op.SharedSpawn:
                            if (objects.ContainsKey(r.Id) || objects.Count >= 4096)
                                throw new FormatException("Duplicate object or capacity exceeded");
                            o = new NetworkObject { Id = r.Id, Prefab = r.Prefab, Owner = r.Owner, Request = r.Request, Fields = r.Mask };
                            if (input.Operation == Op.SharedSpawn) {
                                o.Owner = 0; o.Mode = (ObjectMode)r.Mode; o.Controller = r.Owner;
                                o.Epoch = r.Epoch; o.LeaseMilliseconds = r.TTL;
                            }
                            o.Pending.Id = r.Id; o.Pending.Epoch = o.Epoch;
                            Array.Copy(r.Values, o.Values, 32);
                            Array.Copy(r.Values, o.Sent, 32);
                            Array.Copy(r.Values, o.Pending.Values, 32);
                            objects.Add(o.Id, o);
                            Spawned?.Invoke(o);
                            break;
                        case Op.Update:
                            o = Find(r.Id);
                            if (o == null || (r.Mask & ~o.Fields) != 0)
                                throw new FormatException("Unknown object/field");
                            for (int f = 0; f < 32; f++)
                                if ((r.Mask & (1u << f)) != 0)
                                    o.Values[f] = r.Values[f];
                            o.Notify(r.Mask);
                            Changed?.Invoke(o, r.Mask);
                            break;
                        case Op.Despawn:
                            o = Find(r.Id);
                            if (o == null)
                                throw new FormatException("Unknown object");
                            objects.Remove(r.Id);
                            Despawned?.Invoke(o);
                            break;
                        case Op.Rpc:
                            o = Find(r.Id);
                            if (o == null)
                                throw new FormatException("Unknown RPC object");
                            RpcReceived?.Invoke(o, r.Method, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                            break;
                        case Op.Authority:
                            o = Find(r.Id);
                            if (o == null || o.Mode != ObjectMode.Lease) throw new FormatException("Unknown lease object");
                            // Renewal does not discard pending movement. Generation/holder changes do.
                            bool changed = o.Epoch != r.Epoch || o.Controller != r.Owner;
                            o.Controller = r.Owner; o.Epoch = r.Epoch; o.LeaseMilliseconds = r.TTL;
                            if (changed) {
                                o.Pending.Mask = 0; o.Pending.Epoch = r.Epoch;
                                Array.Copy(o.Values, o.Sent, 32); Array.Copy(o.Values, o.Pending.Values, 32);
                                o.NotifyAuthority();
                            }
                            AuthorityChanged?.Invoke(o);
                            break;
                        case Op.Result:
                            CommandCompleted?.Invoke(r.Id, r.Request, (CommandResult)r.Method);
                            break;
                        case Op.Rejected:
                            Rejected?.Invoke(r.Id);
                            break;
                        default:
                            throw new FormatException("Server sent a client command");
                    }
                }
            }
            finally { receiving = false; }
        }
    }
}
