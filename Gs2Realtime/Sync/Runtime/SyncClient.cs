using System;
using System.Collections.Generic;

namespace Gs2.Realtime.Sync
{
    // Invoke Receive/Set/Flush on one thread (Unity's main thread). The transport
    // must copy or consume the provided buffer before TrySend returns true.
    public delegate bool TrySend(byte[] data, int count);
    public enum ObjectMode : byte { Player, Lease, Server }
    public enum LeaseAction : byte { Acquire = AuthorityKind.Acquire, Renew = AuthorityKind.Renew, Release = AuthorityKind.Release }
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
        // Owner of a player object (changes with ownership transfers); 0 for shared objects.
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
        // Interest group (0 reaches everyone); updates and calls go to its subscribers.
        public uint Group { get; internal set; }
        public uint Controller { get; internal set; }
        public uint Epoch { get; internal set; }
        // Remaining server lease time at the last notification; not a client clock.
        public uint LeaseMilliseconds { get; internal set; }
        // Raised when the owner (player object) or controller/epoch (leased object) changes.
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
        // Fields sent over the unreliable channel since the last Refresh.
        internal uint Recent;
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
        // Optional unreliable sender (e.g. a UDP side channel). When set, the periodic
        // Flush sends updates through it; flushes before commands stay on the reliable
        // sender so updates still precede the command. Call Refresh about once per
        // second so values lost on the way are sent again.
        public TrySend Unreliable { get; set; }
        readonly Record[] refresh = new Record[SyncCodec.MaxRecords];
        // The room's master client (0 until the snapshot names it).
        public uint MasterId
        {
            get; private set;
        }
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
        // Arguments must be consumed in these callbacks; they borrow the input packet.
        // (object, method, sender, args, offset, count)
        public event Action<NetworkObject, uint, uint, byte[], int, int> RpcReceived;
        // Room events: (code, sender, args, offset, count).
        public event Action<uint, uint, byte[], int, int> EventReceived;
        // Properties: (scope 0 = room or a player's connection, writer, args, offset, count).
        public event Action<uint, uint, byte[], int, int> PropertyChanged;
        // A refused command (result code).
        public event Action<CommandResult> Rejected;
        public event Action<NetworkObject> AuthorityChanged;
        public event Action<uint, uint, CommandResult> CommandCompleted;
        // A spawn request (the ID Spawn returned) that the room refused.
        public event Action<uint, CommandResult> SpawnRejected;
        public event Action<uint> MasterChanged;
        public event Action<NetworkObject> GroupChanged;
        // A snapshot starts: objects, master and properties are replaced.
        public event Action SnapshotStarted;
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
            command.Mode = (byte)ObjectMode.Player;
            command.Mask = mask;
            for (int f = 0; f < 32; f++)
                if ((mask & (1u << f)) != 0)
                {
                    if (values == null || f >= values.Length)
                        throw new ArgumentException("Missing field");
                    command.Values[f] = values[f];
                }
            if (!FlushReliable() || !SendCommand(Op.Spawn))
                return 0;
            return nextRequest++;
        }
        NetworkObject Owned(uint id)
        {
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Player || o.Owner != ConnectionId)
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
        // Sends pending owner and lease-holder writes, batched per packet, through the
        // unreliable sender when one is set.
        public bool Flush()
        {
            var unreliable = Unreliable;
            return FlushVia(unreliable ?? send, unreliable != null);
        }
        bool FlushReliable()
        {
            return FlushVia(send, false);
        }
        bool FlushVia(TrySend via, bool unreliable)
        {
            CheckReady();
            int count = 0;
            foreach (var o in objects.Values)
            {
                if (o.Pending.Mask == 0)
                    continue;
                output[count++] = o.Pending;
                if (count == output.Length)
                {
                    if (!FlushBatch(count, via, unreliable))
                        return false;
                    count = 0;
                }
            }
            return count == 0 || FlushBatch(count, via, unreliable);
        }
        bool FlushBatch(int count, TrySend via, bool unreliable)
        {
            int n = SyncCodec.Encode(buffer, Op.Update, output, count);
            if (!via(buffer, n))
                return false;
            for (int i = 0; i < count; i++)
            {
                var p = output[i];
                var o = objects[p.Id];
                for (int f = 0; f < 32; f++)
                    if ((p.Mask & (1u << f)) != 0)
                        o.Sent[f] = p.Values[f];
                if (unreliable)
                    o.Recent |= p.Mask;
                p.Mask = 0;
            }
            return true;
        }
        // Resends the current values of fields sent unreliably since the last call.
        public bool Refresh()
        {
            CheckReady();
            var unreliable = Unreliable;
            int count = 0;
            foreach (var o in objects.Values)
            {
                if (o.Recent == 0)
                    continue;
                uint mask = o.Recent;
                o.Recent = 0;
                if (unreliable == null || !CanWrite(o.Id))
                    continue;
                var r = refresh[count] ?? (refresh[count] = new Record());
                r.Clear();
                r.Id = o.Id;
                r.Epoch = o.Mode == ObjectMode.Lease ? o.Epoch : 0;
                r.Mask = mask;
                Array.Copy(o.Sent, r.Values, 32);
                if (++count == refresh.Length)
                {
                    if (!unreliable(buffer, SyncCodec.Encode(buffer, Op.Update, refresh, count)))
                        return false;
                    count = 0;
                }
            }
            return count == 0 || unreliable(buffer, SyncCodec.Encode(buffer, Op.Update, refresh, count));
        }
        public bool CanWrite(uint id) {
            var o = Find(id);
            return Ready && o != null && (o.Mode == ObjectMode.Player ? o.Owner == ConnectionId : o.Mode == ObjectMode.Lease && o.Controller == ConnectionId);
        }
        public uint Lease(uint id, LeaseAction action) {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Lease) throw new InvalidOperationException("Not a leased object");
            if (!FlushReliable()) return 0;
            command.Clear(); command.Id = id; command.Epoch = o.Epoch; command.Kind = (byte)action;
            return SendRequest(Op.Authority);
        }
        public uint Interact(uint id, uint method, byte[] args = null, int count = 0) {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Server) throw new InvalidOperationException("Not a server object");
            if (!FlushReliable()) return 0;
            command.Clear(); command.Id = id; command.Method = method; command.Args = args; command.ArgsCount = count;
            return SendRequest(Op.Call);
        }
        uint SendRequest(Op op) {
            if (nextCommand == 0) throw new InvalidOperationException("Command IDs exhausted");
            command.Request = nextCommand;
            return SendCommand(op) ? nextCommand++ : 0;
        }
        public bool Destroy(uint id)
        {
            CheckReady();
            Owned(id);
            if (!FlushReliable())
                return false;
            command.Clear();
            command.Id = id;
            return SendCommand(Op.Despawn);
        }
        // Calls on another player's object need the method in the prefab's publicMethods;
        // the server rejects other non-owner calls. recipient is used with RpcTargetByte.Player.
        public bool Rpc(uint id, uint method, byte target, byte[] args, int count, uint recipient = 0)
        {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Player)
                throw new InvalidOperationException("RPCs target player objects");
            return Call(id, method, target, args, count, recipient);
        }
        // A room event (not tied to an object), like PhotonNetwork.RaiseEvent. Any member
        // may raise any code; target is All/Others (optionally Buffered) or Player.
        public bool RaiseEvent(uint code, byte target, byte[] args, int count, uint recipient = 0)
        {
            CheckReady();
            if ((target & ~RpcTargetByte.Buffered) == RpcTargetByte.Owner)
                throw new ArgumentException("Room events have no owner");
            return Call(0, code, target, args, count, recipient);
        }
        bool Call(uint id, uint method, byte target, byte[] args, int count, uint recipient)
        {
            if (RpcTargetByte.HasRecipient(target) && recipient == 0)
                throw new ArgumentException("Player target requires a recipient");
            if (!FlushReliable())
                return false;
            command.Clear();
            command.Id = id;
            command.Method = method;
            command.Target = target;
            command.Recipient = recipient;
            command.Args = args;
            command.ArgsCount = count;
            return SendCommand(Op.Call);
        }
        // Hands a player object to another member (from its owner), or takes it over
        // (newOwner = ConnectionId) when the prefab allows takeover.
        public bool Transfer(uint id, uint newOwner)
        {
            CheckReady();
            var o = Find(id);
            if (o == null || o.Mode != ObjectMode.Player || newOwner == 0)
                throw new InvalidOperationException("Ownership transfers player objects");
            if (!FlushReliable())
                return false;
            command.Clear();
            command.Id = id;
            command.Kind = AuthorityKind.Transfer;
            command.Owner = newOwner;
            return SendCommand(Op.Authority);
        }
        // Moves an owned player object to an interest group (0 = everyone).
        public bool SetGroup(uint id, uint group)
        {
            CheckReady();
            Owned(id);
            if (!FlushReliable())
                return false;
            command.Clear();
            command.Id = id;
            command.Kind = AuthorityKind.Group;
            command.Owner = group;
            return SendCommand(Op.Authority);
        }
        // Starts or stops receiving an interest group's updates and calls.
        public bool Subscribe(uint group, bool subscribe)
        {
            CheckReady();
            if (group == 0)
                throw new ArgumentOutOfRangeException("group", "Group 0 is always received");
            command.Clear();
            command.Kind = RoomKind.Interest;
            command.Id = group;
            command.Owner = subscribe ? 1u : 0u;
            return SendCommand(Op.Room);
        }
        // Only the current master may name another master.
        public bool SetMaster(uint connectionId)
        {
            CheckReady();
            command.Clear();
            command.Kind = RoomKind.Master;
            command.Id = connectionId;
            return SendCommand(Op.Room);
        }
        // scope 0 writes a room property, ConnectionId this player's; args hold the key
        // length, key and value (an empty value deletes the key).
        public bool SetProperty(uint scope, byte[] args, int count)
        {
            CheckReady();
            command.Clear();
            command.Kind = RoomKind.Property;
            command.Id = scope;
            command.Args = args;
            command.ArgsCount = count;
            return SendCommand(Op.Room);
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
        static void ResetPending(NetworkObject o)
        {
            o.Pending.Mask = 0;
            o.Pending.Epoch = o.Epoch;
            Array.Copy(o.Values, o.Sent, 32);
            Array.Copy(o.Values, o.Pending.Values, 32);
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
                        case Op.Snapshot:
                            if (r.Id != 1)
                                throw new FormatException("Unsupported schema");
                            if (r.Kind == SnapshotKind.End)
                            {
                                Ready = true;
                                break;
                            }
                            Ready = false;
                            ClearObjects();
                            MasterId = r.Owner;
                            SnapshotStarted?.Invoke();
                            MasterChanged?.Invoke(MasterId);
                            break;
                        case Op.Spawn:
                            if (objects.ContainsKey(r.Id) || objects.Count >= 4096)
                                throw new FormatException("Duplicate object or capacity exceeded");
                            o = new NetworkObject { Id = r.Id, Prefab = r.Prefab, Mode = (ObjectMode)r.Mode, Request = r.Request, Fields = r.Mask };
                            if (o.Mode == ObjectMode.Player)
                                o.Owner = r.Owner;
                            else
                            {
                                o.Controller = r.Owner;
                                o.Epoch = r.Epoch;
                                o.LeaseMilliseconds = r.TTL;
                            }
                            o.Pending.Id = r.Id;
                            Array.Copy(r.Values, o.Values, 32);
                            ResetPending(o);
                            objects.Add(o.Id, o);
                            Spawned?.Invoke(o);
                            break;
                        case Op.Update:
                            o = Find(r.Id);
                            // Unreliable updates may overtake a spawn or trail a despawn.
                            if (o == null)
                                break;
                            if ((r.Mask & ~o.Fields) != 0)
                                throw new FormatException("Unknown field");
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
                        case Op.Call:
                            if (r.Id == 0)
                            {
                                EventReceived?.Invoke(r.Method, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                                break;
                            }
                            o = Find(r.Id);
                            if (o == null)
                                throw new FormatException("Unknown RPC object");
                            RpcReceived?.Invoke(o, r.Method, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                            break;
                        case Op.Authority:
                            o = Find(r.Id);
                            if (o != null && r.Kind == AuthorityKind.Group)
                            {
                                o.Group = r.Owner;
                                GroupChanged?.Invoke(o);
                                break;
                            }
                            if (o == null || r.Kind != AuthorityKind.State || o.Mode == ObjectMode.Server)
                                throw new FormatException("Unknown authority object");
                            if (o.Mode == ObjectMode.Player)
                            {
                                if (o.Owner == r.Owner)
                                    break;
                                // The new owner continues from the replicated state.
                                o.Owner = r.Owner;
                                ResetPending(o);
                                o.NotifyAuthority();
                                AuthorityChanged?.Invoke(o);
                                break;
                            }
                            // Renewal does not discard pending movement. Generation/holder changes do.
                            bool changed = o.Epoch != r.Epoch || o.Controller != r.Owner;
                            o.Controller = r.Owner; o.Epoch = r.Epoch; o.LeaseMilliseconds = r.TTL;
                            if (changed)
                            {
                                ResetPending(o);
                                o.NotifyAuthority();
                            }
                            AuthorityChanged?.Invoke(o);
                            break;
                        case Op.Result:
                            if (r.Id != 0)
                                CommandCompleted?.Invoke(r.Id, r.Request, (CommandResult)r.Method);
                            else if (r.Request != 0)
                                SpawnRejected?.Invoke(r.Request, (CommandResult)r.Method);
                            else
                                Rejected?.Invoke((CommandResult)r.Method);
                            break;
                        case Op.Room:
                            if (r.Kind == RoomKind.Master)
                            {
                                MasterId = r.Id;
                                MasterChanged?.Invoke(MasterId);
                            }
                            else
                                PropertyChanged?.Invoke(r.Id, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                            break;
                        default:
                            throw new FormatException("Unknown sync operation");
                    }
                }
            }
            finally { receiving = false; }
        }
    }
}
