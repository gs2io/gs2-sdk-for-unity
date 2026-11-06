using System;

namespace Gs2.Realtime.Sync
{
    // Packet operation (wire version 2). One packet carries 1..64 records of one op.
    // Header: 0xd7, 0x20|op, count; the count's high bit is a per-op variant flag that
    // Encode derives from the records (Update: epoch present; Call: request present;
    // Snapshot: end marker).
    public enum Op : byte
    {
        // id, prefab, mode, owner, request, epoch, ttl, mask, values.
        Spawn = 1,
        // id, [epoch], mask, values.
        Update,
        // id.
        Despawn,
        // id, method, sender, target, [recipient], [request], argLength, args.
        // Player object: RPC. Server object: interaction (with request). ID 0: room event.
        Call,
        // id, kind, owner, epoch, ttl, request.
        Authority,
        // id (schema 1), owner (master client).
        Snapshot,
        // id, request, code. ID 0 with a request: refused spawn; ID 0 without: refused command.
        Result,
        // kind, id, owner, argLength, args.
        Room
    }
    // Call target byte. Low bits select receivers; Buffered (All/Others only) replays the
    // call to later joiners. Player is followed by Record.Recipient. The server delivers
    // calls with 0 (all), 1 (others) or 2 (direct) only.
    public static class RpcTargetByte
    {
        public const byte All = 0, Others = 1, Owner = 2, Player = 3, Buffered = 0x80;
        public static bool IsValid(byte target)
        {
            int baseTarget = target & ~Buffered;
            return baseTarget <= Player && ((target & Buffered) == 0 || baseTarget <= Others);
        }
        public static bool HasRecipient(byte target)
        {
            return (target & ~Buffered) == Player;
        }
    }
    // Record.Kind values.
    public static class AuthorityKind
    {
        // State: sent by the server (owner/controller, epoch, remaining lease).
        // Group: the object's interest group in Owner (0 = everyone), set by its owner.
        public const byte State = 0, Acquire = 1, Renew = 2, Release = 3, Transfer = 4, Group = 5;
    }
    public static class RoomKind
    {
        // Master: ID is the master client. Property: ID is the scope (0 room, otherwise a
        // player), Owner the writer, Args key length, key, value (empty value deletes).
        // Interest: ID is a group, Owner 1 subscribes and 0 unsubscribes.
        public const byte Master = 1, Property = 2, Interest = 3;
    }
    public static class SnapshotKind
    {
        public const byte Begin = 0, End = 1;
    }
    // Reusable storage. Arguments borrow the received byte[] until Receive returns.
    public sealed class Record
    {
        public uint Id, Prefab, Owner, Request, Mask, Method, Epoch, TTL, Recipient;
        public byte Mode, Kind, Target;
        public readonly int[] Values = new int[32];
        public byte[] Args;
        public int ArgsOffset, ArgsCount;
        public void Clear()
        {
            Id = Prefab = Owner = Request = Mask = Method = Epoch = TTL = Recipient = 0;
            Mode = Kind = Target = 0;
            Args = null;
            ArgsOffset = ArgsCount = 0;
            Array.Clear(Values, 0, 32);
        }
    }
    public sealed class SyncCodec
    {
        public const int MaxPacket = 16384, MaxRecords = 64, MaxArgs = 4096;
        const byte Version = 0x20, Variant = 0x80;
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
        static byte GetByte(byte[] b, ref int at, int end)
        {
            if (at >= end)
                throw new FormatException("Truncated sync packet");
            return b[at++];
        }
        static bool ValidArgs(Record r)
        {
            return r.ArgsCount >= 0 && r.ArgsCount <= MaxArgs && r.ArgsOffset >= 0 && (r.ArgsCount == 0 || (r.Args != null && r.ArgsOffset <= r.Args.Length - r.ArgsCount));
        }
        static int FieldsSize(Record r)
        {
            int n = Size(r.Mask);
            for (int f = 0; f < 32; f++)
                if ((r.Mask & (1u << f)) != 0)
                    n += Size(Zig(r.Values[f]));
            return n;
        }
        static void PutFields(byte[] dst, ref int at, Record r)
        {
            Put(dst, ref at, r.Mask);
            for (int f = 0; f < 32; f++)
                if ((r.Mask & (1u << f)) != 0)
                    Put(dst, ref at, Zig(r.Values[f]));
        }
        static void GetFields(byte[] b, ref int at, int end, Record r)
        {
            r.Mask = Get(b, ref at, end);
            for (int f = 0; f < 32; f++)
                if ((r.Mask & (1u << f)) != 0)
                {
                    uint v = Get(b, ref at, end);
                    r.Values[f] = unchecked((int)(v >> 1) ^ -(int)(v & 1));
                }
        }
        static void PutArgs(byte[] dst, ref int at, Record r)
        {
            Put(dst, ref at, (uint)r.ArgsCount);
            if (r.ArgsCount > 0)
            {
                Buffer.BlockCopy(r.Args, r.ArgsOffset, dst, at, r.ArgsCount);
                at += r.ArgsCount;
            }
        }
        static void GetArgs(byte[] b, ref int at, int end, Record r)
        {
            uint n = Get(b, ref at, end);
            if (n > MaxArgs || n > (uint)(end - at))
                throw new FormatException("Invalid arguments");
            r.Args = b;
            r.ArgsOffset = at;
            r.ArgsCount = (int)n;
            at += (int)n;
        }
        static bool Flagged(Op op, Record[] records, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var r = records[i];
                if ((op == Op.Update && r.Epoch != 0) || (op == Op.Call && r.Request != 0) || (op == Op.Snapshot && r.Kind == SnapshotKind.End))
                    return true;
            }
            return false;
        }
        static int RecordSize(Op op, Record r, bool flagged)
        {
            int n = Size(r.Id);
            switch (op)
            {
                case Op.Spawn:
                    if (r.Mode > (byte)ObjectMode.Server)
                        throw new ArgumentException("Invalid object mode");
                    return n + Size(r.Prefab) + 1 + Size(r.Owner) + Size(r.Request) + Size(r.Epoch) + Size(r.TTL) + FieldsSize(r);
                case Op.Update:
                    return n + (flagged ? Size(r.Epoch) : 0) + FieldsSize(r);
                case Op.Despawn:
                    return n;
                case Op.Call:
                    if (!RpcTargetByte.IsValid(r.Target) || !ValidArgs(r))
                        throw new ArgumentException("Invalid call");
                    n += Size(r.Method) + Size(r.Owner) + 1 + Size((uint)r.ArgsCount) + r.ArgsCount;
                    if (RpcTargetByte.HasRecipient(r.Target))
                        n += Size(r.Recipient);
                    return flagged ? n + Size(r.Request) : n;
                case Op.Authority:
                    if (r.Kind > AuthorityKind.Group)
                        throw new ArgumentException("Invalid authority kind");
                    return n + 1 + Size(r.Owner) + Size(r.Epoch) + Size(r.TTL) + Size(r.Request);
                case Op.Snapshot:
                    return n + Size(r.Owner);
                case Op.Result:
                    return n + Size(r.Request) + Size(r.Method);
                case Op.Room:
                    if (r.Kind < RoomKind.Master || r.Kind > RoomKind.Interest || !ValidArgs(r))
                        throw new ArgumentException("Invalid room record");
                    return n + 1 + Size(r.Owner) + Size((uint)r.ArgsCount) + r.ArgsCount;
            }
            throw new ArgumentException("Invalid sync operation");
        }
        public static int Encode(byte[] dst, Op op, Record[] records, int count)
        {
            if (dst == null || records == null || count < 1 || count > MaxRecords || count > records.Length || op < Op.Spawn || op > Op.Room)
                throw new ArgumentException("Invalid sync packet");
            bool flagged = Flagged(op, records, count);
            int length = 3;
            for (int i = 0; i < count; i++)
                length += RecordSize(op, records[i], flagged);
            if (length > MaxPacket || dst.Length < length)
                throw new ArgumentException("Sync packet too large");
            int at = 0;
            dst[at++] = 0xd7;
            dst[at++] = (byte)(Version | (byte)op);
            dst[at++] = (byte)(count | (flagged ? Variant : 0));
            for (int i = 0; i < count; i++)
            {
                var r = records[i];
                if (op == Op.Room)
                    dst[at++] = r.Kind;
                Put(dst, ref at, r.Id);
                switch (op)
                {
                    case Op.Spawn:
                        Put(dst, ref at, r.Prefab);
                        dst[at++] = r.Mode;
                        Put(dst, ref at, r.Owner);
                        Put(dst, ref at, r.Request);
                        Put(dst, ref at, r.Epoch);
                        Put(dst, ref at, r.TTL);
                        PutFields(dst, ref at, r);
                        break;
                    case Op.Update:
                        if (flagged)
                            Put(dst, ref at, r.Epoch);
                        PutFields(dst, ref at, r);
                        break;
                    case Op.Call:
                        Put(dst, ref at, r.Method);
                        Put(dst, ref at, r.Owner);
                        dst[at++] = r.Target;
                        if (RpcTargetByte.HasRecipient(r.Target))
                            Put(dst, ref at, r.Recipient);
                        if (flagged)
                            Put(dst, ref at, r.Request);
                        PutArgs(dst, ref at, r);
                        break;
                    case Op.Authority:
                        dst[at++] = r.Kind;
                        Put(dst, ref at, r.Owner);
                        Put(dst, ref at, r.Epoch);
                        Put(dst, ref at, r.TTL);
                        Put(dst, ref at, r.Request);
                        break;
                    case Op.Snapshot:
                        Put(dst, ref at, r.Owner);
                        break;
                    case Op.Result:
                        Put(dst, ref at, r.Request);
                        Put(dst, ref at, r.Method);
                        break;
                    case Op.Room:
                        Put(dst, ref at, r.Owner);
                        PutArgs(dst, ref at, r);
                        break;
                }
            }
            return at;
        }
        public void Decode(byte[] b, int count)
        {
            if (b == null || count < 3 || count > b.Length || count > MaxPacket || b[0] != 0xd7 || (b[1] & 0xf0) != Version)
                throw new FormatException("Invalid sync header");
            var op = (Op)(b[1] & 15);
            int records = b[2] & ~Variant;
            bool flagged = (b[2] & Variant) != 0;
            if (op < Op.Spawn || op > Op.Room || records < 1 || records > MaxRecords || (flagged && op != Op.Update && op != Op.Call && op != Op.Snapshot))
                throw new FormatException("Invalid sync header");
            int at = 3;
            for (int i = 0; i < records; i++)
            {
                var r = Records[i];
                r.Clear();
                if (op == Op.Room)
                {
                    r.Kind = GetByte(b, ref at, count);
                    if (r.Kind < RoomKind.Master || r.Kind > RoomKind.Interest)
                        throw new FormatException("Invalid room record");
                }
                r.Id = Get(b, ref at, count);
                switch (op)
                {
                    case Op.Spawn:
                        r.Prefab = Get(b, ref at, count);
                        r.Mode = GetByte(b, ref at, count);
                        if (r.Mode > (byte)ObjectMode.Server)
                            throw new FormatException("Invalid object mode");
                        r.Owner = Get(b, ref at, count);
                        r.Request = Get(b, ref at, count);
                        r.Epoch = Get(b, ref at, count);
                        r.TTL = Get(b, ref at, count);
                        GetFields(b, ref at, count, r);
                        break;
                    case Op.Update:
                        if (flagged)
                            r.Epoch = Get(b, ref at, count);
                        GetFields(b, ref at, count, r);
                        break;
                    case Op.Call:
                        r.Method = Get(b, ref at, count);
                        r.Owner = Get(b, ref at, count);
                        r.Target = GetByte(b, ref at, count);
                        if (!RpcTargetByte.IsValid(r.Target))
                            throw new FormatException("Invalid call target");
                        if (RpcTargetByte.HasRecipient(r.Target))
                            r.Recipient = Get(b, ref at, count);
                        if (flagged)
                            r.Request = Get(b, ref at, count);
                        GetArgs(b, ref at, count, r);
                        break;
                    case Op.Authority:
                        r.Kind = GetByte(b, ref at, count);
                        if (r.Kind > AuthorityKind.Group)
                            throw new FormatException("Invalid authority kind");
                        r.Owner = Get(b, ref at, count);
                        r.Epoch = Get(b, ref at, count);
                        r.TTL = Get(b, ref at, count);
                        r.Request = Get(b, ref at, count);
                        break;
                    case Op.Snapshot:
                        r.Kind = flagged ? SnapshotKind.End : SnapshotKind.Begin;
                        r.Owner = Get(b, ref at, count);
                        break;
                    case Op.Result:
                        r.Request = Get(b, ref at, count);
                        r.Method = Get(b, ref at, count);
                        break;
                    case Op.Room:
                        r.Owner = Get(b, ref at, count);
                        GetArgs(b, ref at, count, r);
                        break;
                }
            }
            if (at != count)
                throw new FormatException("Trailing sync data");
            Operation = op;
            Count = records;
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
