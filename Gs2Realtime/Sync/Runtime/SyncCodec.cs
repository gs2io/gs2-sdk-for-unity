using System;

namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// Sync packet operation (wire version 3). One packet carries 1..64 records of one op.
    /// Header: 0xd7, 0x30|op, count; the count's high bit is a per-op variant flag that
    /// Encode derives from the records (Update: epoch present; Call: request present;
    /// Snapshot: end marker). Update and Clock headers continue with the state version:
    /// the server numbers its update packets in sending order, and a clock names the
    /// newest update sent before it.
    /// </summary>
    public enum Op : byte
    {
        /// <summary>id, prefab, mode, owner, request, epoch, ttl, mask, values.</summary>
        Spawn = 1,
        /// <summary>id, [epoch], mask, values.</summary>
        Update,
        /// <summary>id.</summary>
        Despawn,
        /// <summary>
        /// id, method, sender, target, [recipient], [request], argLength, args.
        /// Player object: RPC. Server object: interaction (with request). ID 0: room event.
        /// </summary>
        Call,
        /// <summary>id, kind, owner, epoch, ttl, request.</summary>
        Authority,
        /// <summary>id (schema 1), owner (master client).</summary>
        Snapshot,
        /// <summary>
        /// id, request, code. ID 0 with a request: refused spawn; ID 0 without: refused command.
        /// </summary>
        Result,
        /// <summary>kind, id, owner, argLength, args.</summary>
        Room,
        /// <summary>
        /// id (tick), epoch (view lag in 1/16 ticks), mask, values. Client to server:
        /// input for a tick of a ticking room; packets repeat recent ticks so that a lost
        /// datagram costs nothing.
        /// </summary>
        Input,
        /// <summary>
        /// id (tick), ttl (ticks per second), request (newest input tick the room has
        /// received from this client). Server to client after each simulation step.
        /// </summary>
        Clock
    }
    /// <summary>
    /// Call target byte. Low bits select receivers; Buffered (All/Others only) replays the
    /// call to later joiners. Player is followed by Record.Recipient. The server delivers
    /// calls with 0 (all), 1 (others) or 2 (direct) only.
    /// </summary>
    public static class RpcTargetByte
    {
        public const byte All = 0, Others = 1, Owner = 2, Player = 3, Buffered = 0x80;
        /// <summary>Whether the byte is a target a call may carry.</summary>
        public static bool IsValid(byte target)
        {
            int baseTarget = target & ~Buffered;
            return baseTarget <= Player && ((target & Buffered) == 0 || baseTarget <= Others);
        }
        /// <summary>Who receives the call now (the target without the Buffered flag).</summary>
        public static byte Receivers(byte target) => (byte)(target & ~Buffered);
        /// <summary>Whether the target is followed by a recipient (Player).</summary>
        public static bool HasRecipient(byte target) => Receivers(target) == Player;
        /// <summary>
        /// A valid target that selects its receivers without naming one (All, Others, Owner).
        /// </summary>
        public static bool IsUnaddressed(byte target) => IsValid(target) && !HasRecipient(target);
    }
    /// <summary>
    /// Record.Kind values of Authority records. State: sent by the server
    /// (owner/controller, epoch, remaining lease). Group: the object's interest group in
    /// Owner (0 = everyone), set by its owner.
    /// </summary>
    public static class AuthorityKind
    {
        public const byte State = 0, Acquire = 1, Renew = 2, Release = 3, Transfer = 4, Group = 5;
    }
    /// <summary>
    /// Record.Kind values of Room records. Master: ID is the master client. Property: ID is
    /// the scope (0 room, otherwise a player), Owner the writer, Args key length, key, value
    /// (empty value deletes). Interest: ID is a group, Owner 1 subscribes and 0 unsubscribes.
    /// </summary>
    public static class RoomKind
    {
        public const byte Master = 1, Property = 2, Interest = 3;
    }
    /// <summary>
    /// Record.Kind values of Snapshot records (End is the header's variant flag).
    /// </summary>
    public static class SnapshotKind
    {
        public const byte Begin = 0, End = 1;
    }
    /// <summary>A set of fields (or input slots) 0..31 as a bit mask.</summary>
    public static class FieldMask
    {
        public static bool Contains(uint mask, int field) => (mask & (1u << field)) != 0;
    }
    /// <summary>
    /// One record of a sync packet; instances are reused. Args borrows the received
    /// byte[] until Receive returns.
    /// </summary>
    public sealed class Record
    {
        /// <summary>Whether the record carries field (0..31) in Mask.</summary>
        public bool Has(int field) => FieldMask.Contains(Mask, field);
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
    /// <summary>
    /// Encodes and decodes sync packets. Decode fills the reusable Records; a decoded
    /// record is valid until the next Decode.
    /// </summary>
    public sealed class SyncCodec
    {
        public const int MaxPacket = 16384, MaxRecords = 64, MaxArgs = 4096;
        const byte Version = 0x30, VariantBit = 0x80;
        public readonly Record[] Records = new Record[MaxRecords];
        public Op Operation
        {
            get; private set;
        }
        public int Count
        {
            get; private set;
        }
        /// <summary>
        /// State version of the decoded Update or Clock packet (0 for other operations).
        /// </summary>
        public uint State
        {
            get; private set;
        }
        static bool HasState(Op op)
        {
            return op == Op.Update || op == Op.Clock;
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
        // Unsigned varints (7 bits per byte, least significant first), shared with the
        // property encoding in SyncClient. Get accepts only the canonical (shortest) form.
        internal static int Size(uint v)
        {
            int n = 1;
            while (v >= 128)
            {
                n++;
                v >>= 7;
            }
            return n;
        }
        internal static void Put(byte[] b, ref int at, uint v)
        {
            while (v >= 128)
            {
                b[at++] = (byte)(v | 128);
                v >>= 7;
            }
            b[at++] = (byte)v;
        }
        internal static uint Get(byte[] b, ref int at, int end)
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
        // Args holds ArgsCount bytes from ArgsOffset (a count of 0 needs no array).
        static bool ValidArgs(Record r)
        {
            if (r.ArgsCount < 0 || r.ArgsCount > MaxArgs || r.ArgsOffset < 0)
                return false;
            if (r.ArgsCount == 0)
                return true;
            return r.Args != null && r.ArgsOffset <= r.Args.Length - r.ArgsCount;
        }
        static bool HasVariant(Op op)
        {
            return op == Op.Update || op == Op.Call || op == Op.Snapshot;
        }
        // The variant flag: an Update record with an epoch, a Call record with a request
        // ID, or the end of a snapshot.
        static bool Flagged(Op op, Record[] records, int count)
        {
            for (int i = 0; i < count; i++)
                if (Variant(op, records[i]))
                    return true;
            return false;
        }
        static bool Variant(Op op, Record r)
        {
            switch (op)
            {
                case Op.Update:
                    return r.Epoch != 0;
                case Op.Call:
                    return r.Request != 0;
                case Op.Snapshot:
                    return r.Kind == SnapshotKind.End;
                default:
                    return false;
            }
        }
        static bool ValidOp(Op op)
        {
            return op >= Op.Spawn && op <= Op.Clock;
        }

        // A direction a record is walked in: measuring its encoded size (Sizer), writing
        // it (Writer) or reading it (Reader). Layout states each op's field order once for
        // all three, so they cannot disagree. The directions are structs passed as type
        // arguments: each gets its own compiled Layout, without virtual calls.
        interface IWire
        {
            void Uint(ref uint v);
            void Byte(ref byte v);
            void Fields(Record r);
            void Args(Record r);
            // A value no record may carry: an encoder's argument error, a decoder's
            // format error.
            Exception Invalid(string message);
        }
        struct Sizer : IWire
        {
            public int Length;
            public void Uint(ref uint v) { Length += Size(v); }
            public void Byte(ref byte v) { Length++; }
            public void Fields(Record r)
            {
                uint mask = r.Mask;
                int n = Size(mask);
                var values = r.Values;
                for (int f = 0; mask != 0; f++, mask >>= 1)
                    if ((mask & 1) != 0)
                        n += Size(Zig(values[f]));
                Length += n;
            }
            public void Args(Record r)
            {
                if (!ValidArgs(r))
                    throw Invalid("Invalid arguments");
                Length += Size((uint)r.ArgsCount) + r.ArgsCount;
            }
            public Exception Invalid(string message) { return new ArgumentException(message); }
        }
        // Writes a packet whose size was measured.
        struct Writer : IWire
        {
            readonly byte[] dst;
            public int At;
            public Writer(byte[] dst, int at)
            {
                this.dst = dst;
                At = at;
            }
            public void Uint(ref uint v) { Put(dst, ref At, v); }
            public void Byte(ref byte v) { dst[At++] = v; }
            public void Fields(Record r)
            {
                int at = At;
                uint mask = r.Mask;
                Put(dst, ref at, mask);
                var values = r.Values;
                for (int f = 0; mask != 0; f++, mask >>= 1)
                    if ((mask & 1) != 0)
                        Put(dst, ref at, Zig(values[f]));
                At = at;
            }
            public void Args(Record r)
            {
                Put(dst, ref At, (uint)r.ArgsCount);
                if (r.ArgsCount > 0)
                {
                    Buffer.BlockCopy(r.Args, r.ArgsOffset, dst, At, r.ArgsCount);
                    At += r.ArgsCount;
                }
            }
            public Exception Invalid(string message) { return new ArgumentException(message); }
        }
        struct Reader : IWire
        {
            readonly byte[] b;
            readonly int end;
            public int At;
            public Reader(byte[] b, int at, int end)
            {
                this.b = b;
                this.end = end;
                At = at;
            }
            public void Uint(ref uint v) { v = Get(b, ref At, end); }
            public void Byte(ref byte v)
            {
                if (At >= end)
                    throw new FormatException("Truncated sync packet");
                v = b[At++];
            }
            public void Fields(Record r)
            {
                int at = At;
                uint mask = Get(b, ref at, end);
                r.Mask = mask;
                var values = r.Values;
                for (int f = 0; mask != 0; f++, mask >>= 1)
                    if ((mask & 1) != 0)
                    {
                        uint v = Get(b, ref at, end);
                        values[f] = unchecked((int)(v >> 1) ^ -(int)(v & 1));
                    }
                At = at;
            }
            // Arguments borrow the packet buffer (no copy).
            public void Args(Record r)
            {
                uint n = Get(b, ref At, end);
                if (n > MaxArgs || n > (uint)(end - At))
                    throw new FormatException("Invalid arguments");
                r.Args = b;
                r.ArgsOffset = At;
                r.ArgsCount = (int)n;
                At += (int)n;
            }
            public Exception Invalid(string message) { return new FormatException(message); }
        }

        // A record's fields in wire order, walked in direction W.
        static void Layout<W>(ref W w, Op op, Record r, bool flagged) where W : struct, IWire
        {
            if (op == Op.Room)
            {
                w.Byte(ref r.Kind);
                if (r.Kind < RoomKind.Master || r.Kind > RoomKind.Interest)
                    throw w.Invalid("Invalid room record");
            }
            w.Uint(ref r.Id);
            switch (op)
            {
                case Op.Spawn:
                    w.Uint(ref r.Prefab);
                    w.Byte(ref r.Mode);
                    if (r.Mode > (byte)ObjectMode.Simulated)
                        throw w.Invalid("Invalid object mode");
                    w.Uint(ref r.Owner);
                    w.Uint(ref r.Request);
                    w.Uint(ref r.Epoch);
                    w.Uint(ref r.TTL);
                    w.Fields(r);
                    break;
                case Op.Update:
                    if (flagged)
                        w.Uint(ref r.Epoch);
                    w.Fields(r);
                    break;
                case Op.Despawn:
                    break;
                case Op.Call:
                    w.Uint(ref r.Method);
                    w.Uint(ref r.Owner);
                    w.Byte(ref r.Target);
                    if (!RpcTargetByte.IsValid(r.Target))
                        throw w.Invalid("Invalid call target");
                    if (RpcTargetByte.HasRecipient(r.Target))
                        w.Uint(ref r.Recipient);
                    if (flagged)
                        w.Uint(ref r.Request);
                    w.Args(r);
                    break;
                case Op.Authority:
                    w.Byte(ref r.Kind);
                    if (r.Kind > AuthorityKind.Group)
                        throw w.Invalid("Invalid authority kind");
                    w.Uint(ref r.Owner);
                    w.Uint(ref r.Epoch);
                    w.Uint(ref r.TTL);
                    w.Uint(ref r.Request);
                    break;
                case Op.Snapshot:
                    w.Uint(ref r.Owner);
                    w.Uint(ref r.Epoch);
                    break;
                case Op.Result:
                    w.Uint(ref r.Request);
                    w.Uint(ref r.Method);
                    w.Byte(ref r.Kind);
                    w.Uint(ref r.Owner);
                    break;
                case Op.Room:
                    w.Uint(ref r.Owner);
                    w.Args(r);
                    break;
                case Op.Input:
                    w.Uint(ref r.Epoch);
                    w.Fields(r);
                    break;
                case Op.Clock:
                    w.Uint(ref r.TTL);
                    w.Uint(ref r.Request);
                    break;
                default:
                    throw w.Invalid("Invalid sync operation");
            }
        }

        // The encoded size of an update record, counting its epoch as present (the most
        // it takes in a packet).
        internal static int UpdateSize(Record r)
        {
            var size = new Sizer();
            Layout(ref size, Op.Update, r, true);
            return size.Length;
        }
        /// <summary>
        /// Writes count records of op into dst and returns the packet length. Throws
        /// ArgumentException for invalid records or a packet over MaxPacket bytes.
        /// </summary>
        public static int Encode(byte[] dst, Op op, Record[] records, int count)
        {
            return Encode(dst, op, 0, records, count);
        }
        /// <summary>
        /// Encode with the state version of Update and Clock packets (clients send 0).
        /// </summary>
        public static int Encode(byte[] dst, Op op, uint state, Record[] records, int count)
        {
            if (dst == null || records == null || !ValidOp(op))
                throw new ArgumentException("Invalid sync packet");
            if (count < 1 || count > MaxRecords || count > records.Length)
                throw new ArgumentException("Invalid sync packet");
            bool flagged = Flagged(op, records, count);
            var size = new Sizer { Length = 3 };
            if (HasState(op))
                size.Uint(ref state);
            for (int i = 0; i < count; i++)
                Layout(ref size, op, records[i], flagged);
            if (size.Length > MaxPacket || dst.Length < size.Length)
                throw new ArgumentException("Sync packet too large");
            dst[0] = 0xd7;
            dst[1] = (byte)(Version | (byte)op);
            dst[2] = (byte)(count | (flagged ? VariantBit : 0));
            var output = new Writer(dst, 3);
            if (HasState(op))
                output.Uint(ref state);
            for (int i = 0; i < count; i++)
                Layout(ref output, op, records[i], flagged);
            return output.At;
        }
        /// <summary>
        /// Decodes the first count bytes of b into Operation, Count, State and Records.
        /// Throws FormatException for a malformed or non-canonical packet.
        /// </summary>
        public void Decode(byte[] b, int count)
        {
            if (b == null || count < 3 || count > b.Length || count > MaxPacket)
                throw new FormatException("Invalid sync header");
            if (b[0] != 0xd7 || (b[1] & 0xf0) != Version)
                throw new FormatException("Invalid sync header");
            var op = (Op)(b[1] & 15);
            int records = b[2] & ~VariantBit;
            bool flagged = (b[2] & VariantBit) != 0;
            if (!ValidOp(op) || records < 1 || records > MaxRecords)
                throw new FormatException("Invalid sync header");
            if (flagged && !HasVariant(op))
                throw new FormatException("Invalid sync header");
            var input = new Reader(b, 3, count);
            uint state = 0;
            if (HasState(op))
                input.Uint(ref state);
            for (int i = 0; i < records; i++)
            {
                var r = Records[i];
                r.Clear();
                Layout(ref input, op, r, flagged);
                if (op == Op.Snapshot)
                    r.Kind = flagged ? SnapshotKind.End : SnapshotKind.Begin;
            }
            if (input.At != count)
                throw new FormatException("Trailing sync data");
            // The variant flag is derived from the records; one that none needs is not canonical.
            if (flagged != Flagged(op, Records, records))
                throw new FormatException("Invalid sync header");
            Operation = op;
            Count = records;
            State = state;
        }
        /// <summary>
        /// value / unitsPerStep rounded to the nearest int (halves away from zero).
        /// Throws ArgumentOutOfRangeException for a non-positive step or a result outside int.
        /// </summary>
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
