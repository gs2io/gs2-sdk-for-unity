using System;

namespace Gs2.Realtime.Sync
{
    public enum Op : byte
    {
        SpawnRequest = 1, Spawn, Update, Despawn, Rpc, SnapshotBegin, SnapshotEnd, Rejected, SharedSpawn, Lease, Authority, LeasedUpdate, Interact, Result
    }
    // Reusable storage. Rpc arguments borrow the received byte[] until Receive returns.
    public sealed class Record
    {
        public uint Id, Prefab, Owner, Request, Mask, Method, Epoch, TTL;
        public byte Target, Mode;
        public readonly int[] Values = new int[32];
        public byte[] Args;
        public int ArgsOffset, ArgsCount;
        public void Clear()
        {
            Id = Prefab = Owner = Request = Mask = Method = Epoch = TTL = 0;
            Target = Mode = 0;
            Args = null;
            ArgsOffset = ArgsCount = 0;
            Array.Clear(Values, 0, 32);
        }
    }
    public sealed class SyncCodec
    {
        public const int MaxPacket = 16384, MaxRecords = 64, MaxArgs = 512;
        public readonly Record[] Records = new Record[MaxRecords];
        public Op Operation
        {
            get; private set;
        }
        public int Count
        {
            get; private set;
        }
        public SyncCodec()
        {
            for (int i = 0; i < Records.Length; i++)
                Records[i] = new Record();
        }
        static uint Zig(int v)
        {
            return unchecked((uint)(v << 1) ^ (uint)(v >> 31));
        }
        static int Size(uint v)
        {
            int n = 1;
            while (v >= 128)
            {
                n++;
                v >>= 7;
            }
            return n;
        }
        static void Put(byte[] b, ref int at, uint v)
        {
            while (v >= 128)
            {
                b[at++] = (byte)(v | 128);
                v >>= 7;
            }
            b[at++] = (byte)v;
        }
        static uint Get(byte[] b, ref int at, int end)
        {
            uint v = 0;
            for (int i = 0; i < 5; i++)
            {
                if (at >= end)
                    throw new FormatException("Truncated sync packet");
                byte x = b[at++];
                if (i == 4 && x > 15)
                    throw new FormatException("Varint overflow");
                v |= (uint)(x & 127) << (7 * i);
                if (x < 128)
                {
                    if (i > 0 && x == 0)
                        throw new FormatException("Noncanonical varint");
                    return v;
                }
            }
            throw new FormatException("Invalid varint");
        }
        static bool HasFields(Op op)
        {
            return op == Op.SpawnRequest || op == Op.Spawn || op == Op.Update || op == Op.SharedSpawn || op == Op.LeasedUpdate;
        }
        public static int Encode(byte[] dst, Op op, Record[] records, int count)
        {
            if (dst == null || records == null || count < 1 || count > MaxRecords || count > records.Length || op < Op.SpawnRequest || op > Op.Result)
                throw new ArgumentException("Invalid sync packet");
            int length = 3;
            for (int i = 0; i < count; i++)
            {
                var r = records[i];
                length += Size(r.Id);
                if (op == Op.SpawnRequest)
                    length += Size(r.Prefab);
                if (op == Op.Spawn)
                    length += Size(r.Prefab) + Size(r.Owner) + Size(r.Request);
                if (op == Op.SharedSpawn) {
                    if (r.Mode < 1 || r.Mode > 2) throw new ArgumentException("Invalid shared mode");
                    length += Size(r.Prefab) + 1 + Size(r.Owner) + Size(r.Epoch) + Size(r.TTL);
                }
                if (op == Op.Lease) length += Size(r.Epoch) + Size(r.Method) + Size(r.Request);
                if (op == Op.Authority) length += Size(r.Owner) + Size(r.Epoch) + Size(r.TTL);
                if (op == Op.LeasedUpdate) length += Size(r.Epoch);
                if (op == Op.Result) length += Size(r.Request) + Size(r.Method);
                if (op == Op.Interact) {
                    if (r.ArgsCount < 0 || r.ArgsCount > MaxArgs || r.ArgsOffset < 0 || (r.ArgsCount > 0 && (r.Args == null || r.ArgsOffset > r.Args.Length - r.ArgsCount))) throw new ArgumentException("Invalid action args");
                    length += Size(r.Method) + Size(r.Request) + Size((uint)r.ArgsCount) + r.ArgsCount;
                }
                if (op == Op.Rpc)
                {
                    if (r.Target > 2 || r.ArgsCount < 0 || r.ArgsCount > MaxArgs || r.ArgsOffset < 0 || (r.ArgsCount > 0 && (r.Args == null || r.ArgsOffset > r.Args.Length - r.ArgsCount)))
                        throw new ArgumentException("Invalid RPC");
                    length += Size(r.Method) + Size(r.Owner) + 1 + Size((uint)r.ArgsCount) + r.ArgsCount;
                }
                if (HasFields(op))
                {
                    length += Size(r.Mask);
                    for (int f = 0; f < 32; f++)
                        if ((r.Mask & (1u << f)) != 0)
                            length += Size(Zig(r.Values[f]));
                }
            }
            if (length > MaxPacket || dst.Length < length)
                throw new ArgumentException("Sync packet too large");
            int at = 0;
            dst[at++] = 0xd7;
            dst[at++] = (byte)(0x10 | (byte)op);
            dst[at++] = (byte)count;
            for (int i = 0; i < count; i++)
            {
                var r = records[i];
                Put(dst, ref at, r.Id);
                if (op == Op.SpawnRequest)
                    Put(dst, ref at, r.Prefab);
                if (op == Op.Spawn)
                {
                    Put(dst, ref at, r.Prefab);
                    Put(dst, ref at, r.Owner);
                    Put(dst, ref at, r.Request);
                }
                if (op == Op.SharedSpawn) {
                    Put(dst, ref at, r.Prefab); dst[at++] = r.Mode; Put(dst, ref at, r.Owner); Put(dst, ref at, r.Epoch); Put(dst, ref at, r.TTL);
                }
                if (op == Op.Lease) { Put(dst, ref at, r.Epoch); Put(dst, ref at, r.Method); Put(dst, ref at, r.Request); }
                if (op == Op.Authority) { Put(dst, ref at, r.Owner); Put(dst, ref at, r.Epoch); Put(dst, ref at, r.TTL); }
                if (op == Op.LeasedUpdate) Put(dst, ref at, r.Epoch);
                if (op == Op.Result) { Put(dst, ref at, r.Request); Put(dst, ref at, r.Method); }
                if (op == Op.Interact) {
                    Put(dst, ref at, r.Method); Put(dst, ref at, r.Request); Put(dst, ref at, (uint)r.ArgsCount);
                    if (r.ArgsCount > 0) { Buffer.BlockCopy(r.Args, r.ArgsOffset, dst, at, r.ArgsCount); at += r.ArgsCount; }
                }
                if (op == Op.Rpc)
                {
                    Put(dst, ref at, r.Method);
                    Put(dst, ref at, r.Owner);
                    dst[at++] = r.Target;
                    Put(dst, ref at, (uint)r.ArgsCount);
                    if (r.ArgsCount > 0)
                    {
                        Buffer.BlockCopy(r.Args, r.ArgsOffset, dst, at, r.ArgsCount);
                        at += r.ArgsCount;
                    }
                }
                if (HasFields(op))
                {
                    Put(dst, ref at, r.Mask);
                    for (int f = 0; f < 32; f++)
                        if ((r.Mask & (1u << f)) != 0)
                            Put(dst, ref at, Zig(r.Values[f]));
                }
            }
            return at;
        }
        public void Decode(byte[] b, int count)
        {
            if (b == null || count < 3 || count > b.Length || count > MaxPacket || b[0] != 0xd7 || (b[1] >> 4) != 1 || b[2] < 1 || b[2] > MaxRecords)
                throw new FormatException("Invalid sync header");
            var op = (Op)(b[1] & 15);
            if (op < Op.SpawnRequest || op > Op.Result)
                throw new FormatException("Unknown sync operation");
            int at = 3;
            for (int i = 0; i < b[2]; i++)
            {
                var r = Records[i];
                r.Clear();
                r.Id = Get(b, ref at, count);
                if (op == Op.SpawnRequest)
                    r.Prefab = Get(b, ref at, count);
                if (op == Op.Spawn)
                {
                    r.Prefab = Get(b, ref at, count);
                    r.Owner = Get(b, ref at, count);
                    r.Request = Get(b, ref at, count);
                }
                if (op == Op.SharedSpawn) {
                    r.Prefab = Get(b, ref at, count);
                    if (at >= count) throw new FormatException("Missing shared mode");
                    r.Mode = b[at++]; if (r.Mode < 1 || r.Mode > 2) throw new FormatException("Invalid shared mode");
                    r.Owner = Get(b, ref at, count); r.Epoch = Get(b, ref at, count); r.TTL = Get(b, ref at, count);
                }
                if (op == Op.Lease) { r.Epoch = Get(b, ref at, count); r.Method = Get(b, ref at, count); r.Request = Get(b, ref at, count); }
                if (op == Op.Authority) { r.Owner = Get(b, ref at, count); r.Epoch = Get(b, ref at, count); r.TTL = Get(b, ref at, count); }
                if (op == Op.LeasedUpdate) r.Epoch = Get(b, ref at, count);
                if (op == Op.Result) { r.Request = Get(b, ref at, count); r.Method = Get(b, ref at, count); }
                if (op == Op.Interact) {
                    r.Method = Get(b, ref at, count); r.Request = Get(b, ref at, count);
                    uint n = Get(b, ref at, count);
                    if (n > MaxArgs || n > (uint)(count-at)) throw new FormatException("Invalid action args");
                    r.Args = b; r.ArgsOffset = at; r.ArgsCount = (int)n; at += (int)n;
                }
                if (op == Op.Rpc)
                {
                    r.Method = Get(b, ref at, count);
                    r.Owner = Get(b, ref at, count);
                    if (at >= count)
                        throw new FormatException("Missing RPC target");
                    r.Target = b[at++];
                    uint n = Get(b, ref at, count);
                    if (r.Target > 2 || n > MaxArgs || n > (uint)(count - at))
                        throw new FormatException("Invalid RPC args");
                    r.Args = b;
                    r.ArgsOffset = at;
                    r.ArgsCount = (int)n;
                    at += (int)n;
                }
                if (HasFields(op))
                {
                    r.Mask = Get(b, ref at, count);
                    for (int f = 0; f < 32; f++)
                        if ((r.Mask & (1u << f)) != 0)
                        {
                            uint v = Get(b, ref at, count);
                            r.Values[f] = unchecked((int)(v >> 1) ^ -(int)(v & 1));
                        }
                }
            }
            if (at != count)
                throw new FormatException("Trailing sync data");
            Operation = op;
            Count = b[2];
        }
        public static int Quantize(double value, double unitsPerStep)
        {
            if (double.IsNaN(unitsPerStep) || double.IsInfinity(unitsPerStep) || unitsPerStep <= 0)
                throw new ArgumentOutOfRangeException("unitsPerStep");
            double v = Math.Round(value / unitsPerStep, MidpointRounding.AwayFromZero);
            if (double.IsNaN(v) || v < int.MinValue || v > int.MaxValue)
                throw new ArgumentOutOfRangeException("value");
            return (int)v;
        }
    }
}
