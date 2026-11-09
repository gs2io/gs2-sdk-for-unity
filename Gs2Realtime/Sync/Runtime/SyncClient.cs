using System;
using System.Collections.Generic;

namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// Hands count bytes of data to the transport; false means not sent. The transport must
    /// copy or consume the buffer before returning true: the client reuses it.
    /// </summary>
    public delegate bool TrySend(byte[] data, int count);
    /// <summary>
    /// How an object is synchronized (see <see cref="ObjectKind"/>). Player: a member owns it
    /// and writes its fields. Lease: members borrow it. Server: the room's rules own it and
    /// members Interact with it. Simulated: the rules write the fields every tick; the owner
    /// sends inputs.
    /// </summary>
    public enum ObjectMode : byte { Player, Lease, Server, Simulated }
    // Ticks and state versions are serial numbers that may wrap: compare by difference.
    static class Serial
    {
        public static int Diff(uint a, uint b) => unchecked((int)(a - b));
        public static bool Before(uint a, uint b) => Diff(a, b) < 0;
        public static bool After(uint a, uint b) => Diff(a, b) > 0;
    }
    // Mirrors the room's table (kind.go): keep the two in step.
    /// <summary>
    /// What an object mode allows; test these instead of naming modes. An object's holder is
    /// its Owner: the member owning a player or simulated object, or holding a leased one's lease.
    /// </summary>
    public struct ObjectKind
    {
        /// <summary>A member owns it for its life (RPC target, ownership transfer).</summary>
        public readonly bool Owned;
        /// <summary>Its owner can hand it to another member (Transfer, takeover, keepOnLeave).</summary>
        public readonly bool Transferable;
        /// <summary>Members borrow it for a time (Acquire / Renew / Release).</summary>
        public readonly bool Leased;
        /// <summary>The holder sends its field values.</summary>
        public readonly bool HolderWrites;
        /// <summary>The holder may despawn it; the room rules despawn the other kinds.</summary>
        public readonly bool HolderDespawns;
        /// <summary>Members call the room rules' Interact on it.</summary>
        public readonly bool Interactive;
        /// <summary>The room rules move it from its owner's inputs (ticking rooms; predicted).</summary>
        public readonly bool Simulated;
        /// <summary>It has a holder (owned or leased): authority messages describe it.</summary>
        public bool HasHolder => Owned || Leased;
        ObjectKind(bool owned, bool transferable, bool leased, bool holderWrites, bool holderDespawns, bool interactive, bool simulated)
        {
            Owned = owned;
            Transferable = transferable;
            Leased = leased;
            HolderWrites = holderWrites;
            HolderDespawns = holderDespawns;
            Interactive = interactive;
            Simulated = simulated;
        }
        static readonly ObjectKind[] kinds =
        {
            new ObjectKind(owned: true, transferable: true, leased: false, holderWrites: true, holderDespawns: true, interactive: false, simulated: false),  // Player
            new ObjectKind(owned: false, transferable: false, leased: true, holderWrites: true, holderDespawns: false, interactive: false, simulated: false), // Lease
            new ObjectKind(owned: false, transferable: false, leased: false, holderWrites: false, holderDespawns: false, interactive: true, simulated: false), // Server
            new ObjectKind(owned: true, transferable: false, leased: false, holderWrites: false, holderDespawns: false, interactive: false, simulated: true),  // Simulated
        };
        /// <summary>The properties of a mode.</summary>
        public static ObjectKind Of(ObjectMode mode) => kinds[(int)mode];
    }
    /// <summary>What <see cref="SyncClient.Lease(uint, LeaseAction)"/> asks of a leased object.</summary>
    public enum LeaseAction : byte { Acquire = AuthorityKind.Acquire, Renew = AuthorityKind.Renew, Release = AuthorityKind.Release }
    /// <summary>
    /// Receivers of an RPC or room event. Owner is the object's owner (the caller itself for
    /// owner-only methods; room events have none). Buffered targets are also replayed to
    /// players who join later, until the object is removed or the caller leaves. One player is
    /// reached with RpcTo / RaiseEventTo instead.
    /// </summary>
    public enum RpcTarget : byte
    {
        All = RpcTargetByte.All,
        Others = RpcTargetByte.Others,
        Owner = RpcTargetByte.Owner,
        AllBuffered = RpcTargetByte.All | RpcTargetByte.Buffered,
        OthersBuffered = RpcTargetByte.Others | RpcTargetByte.Buffered
    }
    /// <summary>
    /// What became of a request (a spawn, a lease request or an interaction): the room applied
    /// it (Succeeded) or refused it (Refused, with its reason); it never left (NotSent: the
    /// outbox was full); or this client stopped waiting for it (Abandoned: Reset, a new
    /// snapshot after rejoining, or AbandonRequests) and no answer will come.
    /// </summary>
    public enum RequestStatus : byte
    {
        Pending,
        Succeeded,
        Refused,
        NotSent,
        Abandoned,
    }
    /// <summary>
    /// A request's answer. result is the room's (Succeeded: Success or AlreadyApplied;
    /// Refused: the reason, from 100 the rules' own code); default otherwise.
    /// </summary>
    public delegate void CommandCallback(RequestStatus status, CommandResult result);
    /// <summary>A spawn request's answer; spawned is the room's object when it Succeeded.</summary>
    public delegate void SpawnCallback(RequestStatus status, NetworkObject spawned, CommandResult result);
    /// <summary>
    /// The room's results (0..99). A rule-based room also refuses commands and spawns with the
    /// rules' own codes (Step.Ignore), from 100: compare them with the error enum the compiler
    /// generates with the rules (unity/&lt;Error&gt;.cs), e.g. <c>(MyError)result</c>.
    /// </summary>
    public enum CommandResult : uint
    {
        Success, MissingObject, Forbidden, LeaseBusy, StaleEpoch, InvalidOperation, AlreadyApplied, StaleRequest,
        /// <summary>A room limit: objects owned, subscriptions, properties, buffered calls, members.</summary>
        LimitReached,
        /// <summary>An RPC the object catalog does not allow this caller.</summary>
        MethodNotAllowed,
    }
    /// <summary>Helpers for <see cref="CommandResult"/>.</summary>
    public static class CommandResults
    {
        /// <summary>The lowest code of the rules' own results; lower codes are the room's.</summary>
        public const uint FirstRulesCode = 100;
        /// <summary>A code of the room's rules (Step.Ignore), not one of the room's own results.</summary>
        public static bool IsRulesCode(this CommandResult result) => (uint)result >= FirstRulesCode;
    }
    /// <summary>
    /// A command the room refused: which (its op and the ID its record named: an object, a
    /// group, a member or a property scope) and why.
    /// </summary>
    public struct Rejection
    {
        public readonly Op Command;
        public readonly uint Target;
        public readonly CommandResult Reason;
        public Rejection(Op command, uint target, CommandResult reason)
        {
            Command = command;
            Target = target;
            Reason = reason;
        }
        public override string ToString() => Command + " (" + Target + "): " + Reason;
    }
    /// <summary>
    /// A room object as this client sees it. Read-only for the game: the client updates it from
    /// the room's messages.
    /// </summary>
    public sealed class NetworkObject
    {
        // This client's connection ID: the object answers what this client may do with it.
        readonly uint me;
        NetworkObject(uint me)
        {
            this.me = me;
        }
        // The room's spawn of an object, as received by the client me.
        internal static NetworkObject Spawned(Record r, uint me)
        {
            var o = new NetworkObject(me)
            {
                Id = r.Id,
                Prefab = r.Prefab,
                Mode = (ObjectMode)r.Mode,
                Fields = r.Mask,
                Owner = r.Owner,
                Epoch = r.Epoch,
                LeaseMilliseconds = r.TTL,
            };
            // Request IDs are numbered per member: another member's (it also starts at 1)
            // would look like ours, so only our own are kept.
            o.Request = o.IsMine ? r.Request : 0;
            o.Initialize(r);
            return o;
        }
        /// <summary>The room's object ID.</summary>
        public uint Id { get; private set; }
        /// <summary>The prefab the object was spawned from.</summary>
        public uint Prefab { get; private set; }
        /// <summary>
        /// The holder's connection ID: owner of a player or simulated object (changes with
        /// ownership transfers), or the member holding a leased object's lease (0 while free).
        /// </summary>
        public uint Owner { get; private set; }
        /// <summary>This client's spawn request that created it (0 for other objects).</summary>
        public uint Request { get; private set; }
        /// <summary>Bit mask of the fields (0..31) the object's schema has.</summary>
        public uint Fields { get; private set; }
        /// <summary>How the object is synchronized.</summary>
        public ObjectMode Mode { get; private set; }
        /// <summary>What the object's mode allows.</summary>
        public ObjectKind Kind => ObjectKind.Of(Mode);
        /// <summary>Somebody holds it (an owner, or a lease holder).</summary>
        public bool HasHolder => Owner != 0;
        /// <summary>This client holds it: owns a player or simulated object, or holds the lease.</summary>
        public bool IsMine => me != 0 && Owner == me;
        /// <summary>A player or simulated object this client owns.</summary>
        public bool OwnedByMe => Kind.Owned && IsMine;
        /// <summary>A simulated object this client owns (drives with inputs; predicts).</summary>
        public bool SimulatedForMe => Kind.Simulated && IsMine;
        /// <summary>A leased object whose lease this client holds.</summary>
        public bool LeaseHeldByMe => Kind.Leased && IsMine;
        /// <summary>This client sends its field values (it holds a kind the holder writes).</summary>
        public bool WritableByMe => Kind.HolderWrites && IsMine;
        /// <summary>This client may despawn it (it owns a kind the holder despawns).</summary>
        public bool DespawnableByMe => Kind.HolderDespawns && IsMine;
        /// <summary>
        /// The room confirms one of this client's own spawn requests with it (not a predicted
        /// spawn, whose Request is the prediction's key).
        /// </summary>
        public bool ConfirmsMySpawn => Request != 0 && !HasPredictedSpawnKey;
        /// <summary>
        /// The room spawned it in a simulation step, with the key a prediction uses for the same
        /// spawn (Request has the high bit).
        /// </summary>
        public bool HasPredictedSpawnKey => IsPredictedSpawnKey(Request);
        internal static bool IsPredictedSpawnKey(uint request) => FieldMask.Contains(request, 31);
        /// <summary>The schema has field (0..31); false for any other index.</summary>
        public bool HasField(int field) => field >= 0 && field < 32 && FieldMask.Contains(Fields, field);
        internal bool HasUnknownFields(uint mask) => (mask & ~Fields) != 0;
        /// <summary>Interest group (0 reaches everyone); updates and calls go to its subscribers.</summary>
        public uint Group { get; private set; }
        internal void MoveToGroup(uint group) => Group = group;
        /// <summary>The current authority period, as the room numbers it (see AuthorityChanged).</summary>
        public uint Epoch { get; private set; }
        /// <summary>Remaining server lease time at the last notification; not a client clock.</summary>
        public uint LeaseMilliseconds { get; private set; }
        // Applies the room's authority state (holder, period, remaining lease). A new holder
        // or period discards pending writes: the holder continues from the replicated
        // state (a lease renewal keeps them). Returns whether holder or period changed.
        internal bool ApplyAuthority(Record r)
        {
            bool changed = Owner != r.Owner || Epoch != r.Epoch;
            Owner = r.Owner;
            Epoch = r.Epoch;
            LeaseMilliseconds = r.TTL;
            if (changed)
                ResetWrites();
            return changed;
        }
        /// <summary>Raised when the holder or the authority period (Epoch) changes.</summary>
        public event Action AuthorityChanged;
        internal void NotifyAuthority() { AuthorityChanged?.Invoke(); }
        /// <summary>
        /// The result of this client's lease request or interaction on the object
        /// (request ID, result).
        /// </summary>
        public event Action<uint, CommandResult> CommandCompleted;
        internal void NotifyCommand(uint request, CommandResult result) { CommandCompleted?.Invoke(request, result); }

        // ---- Room state ----
        readonly int[] values = new int[32];
        /// <summary>The room's latest state of each field (32 entries); the room's updates write it.</summary>
        public IReadOnlyList<int> Values => values;
        /// <summary>Raised when a room update changes field values; the argument is their bit mask.</summary>
        public event Action<uint> Changed;
        internal void Notify(uint mask)
        {
            Changed?.Invoke(mask);
        }
        internal int Value(int field) => values[field];
        internal void CopyValuesTo(int[] destination) => Array.Copy(values, destination, 32);

        // Updates travel over UDP and WebSocket, so one may arrive after a newer one. Per
        // field: the newest state version received (receivedMask: fields that got one),
        // which rejects older updates; the version from which the current value holds
        // (versionedMask), which places it before or after a clock; and the value it
        // replaced (priorMask), so a clock that a newer update overtook finds its value.
        readonly uint[] received = new uint[32];
        readonly uint[] versions = new uint[32];
        readonly int[] priorValues = new int[32];
        readonly uint[] priorVersions = new uint[32];
        uint receivedMask, versionedMask, priorMask, priorVersionedMask;

        void Initialize(Record spawn)
        {
            Array.Copy(spawn.Values, values, 32);
            pending.Id = spawn.Id;
            ResetWrites();
        }
        // Applies the fields of a room update with state version state that are not older
        // than the newest received; returns the fields whose value changed. An update
        // repeating the value held (a resend, a repeat of the same change) only moves the
        // received version on: the value still holds from its earlier version, it keeps
        // the value it replaced and it is not a change.
        internal uint ApplyUpdate(Record update, uint state)
        {
            uint applied = 0;
            for (int f = 0; f < 32; f++)
            {
                uint bit = 1u << f;
                if ((update.Mask & bit) == 0 || IsOutdated(f, state))
                    continue;
                received[f] = state;
                receivedMask |= bit;
                int value = update.Values[f];
                if (value == values[f])
                    continue;
                // Keep the value this update replaces, with its version when it has one
                // (the spawn's initial values have none).
                priorValues[f] = values[f];
                priorVersions[f] = versions[f];
                priorMask |= bit;
                if ((versionedMask & bit) != 0)
                    priorVersionedMask |= bit;
                else
                    priorVersionedMask &= ~bit;
                values[f] = value;
                versions[f] = state;
                applied |= bit;
            }
            versionedMask |= applied;
            return applied;
        }
        // An update of the field with state version state is older than one received.
        bool IsOutdated(int field, uint state)
        {
            return FieldMask.Contains(receivedMask, field) && Serial.Before(state, received[field]);
        }
        // The field's value comes from a state version newer than state.
        internal bool IsNewer(int field, uint state)
        {
            return FieldMask.Contains(versionedMask, field) && Serial.After(versions[field], state);
        }
        // How far the field's current value is known, as of the clock's state: the newest
        // version received, but not past state (false: nothing received, a spawn value).
        internal bool KnownAsOf(int field, uint state, out uint version)
        {
            version = Serial.After(received[field], state) ? state : received[field];
            return FieldMask.Contains(receivedMask, field);
        }
        // The field's current value and its state version (false: it has none yet).
        internal bool Current(int field, out int value, out uint version)
        {
            value = values[field];
            version = versions[field];
            return FieldMask.Contains(versionedMask, field);
        }
        // The value the field's latest update replaced, if that one was not newer than state.
        // versioned is false for a spawn's initial value, which holds as of any state
        // before the field's first update.
        internal bool TryPriorAsOf(int field, uint state, out int value, out uint version, out bool versioned)
        {
            uint bit = 1u << field;
            value = priorValues[field];
            version = priorVersions[field];
            versioned = (priorVersionedMask & bit) != 0;
            if ((priorMask & bit) == 0)
                return false;
            return !versioned || !Serial.After(version, state);
        }

        // ---- This client's writes (owner or lease holder) ----
        readonly Record pending = new Record(); // staged writes (Mask) over the last sent values
        readonly int[] sent = new int[32];
        uint recent; // fields sent over the unreliable channel since the last refresh
        uint written; // fields this client wrote in the current authority period

        internal bool HasPendingWrites => pending.Mask != 0;
        // The staged writes as an update record (reused; valid until WritesSent).
        internal Record PendingWrites => pending;
        // Stages a write; writing back the last sent value cancels it.
        internal void StageWrite(int field, int value)
        {
            pending.Values[field] = value;
            written |= 1u << field;
            if (sent[field] == value)
                pending.Mask &= ~(1u << field);
            else
                pending.Mask |= 1u << field;
        }
        // The staged writes were sent: they become the last sent values.
        internal void WritesSent(bool unreliable)
        {
            for (int f = 0; f < 32; f++)
                if (pending.Has(f))
                    sent[f] = pending.Values[f];
            if (unreliable)
                recent |= pending.Mask;
            else
                recent &= ~pending.Mask; // delivered reliably: nothing to resend
            pending.Mask = 0;
        }
        // A new holder or authority period: drop staged writes and the resend set (values
        // of an earlier period are not this period's to repeat) and continue from the
        // room's state.
        void ResetWrites()
        {
            pending.Mask = 0;
            recent = 0;
            written = 0;
            pending.Epoch = Epoch;
            Array.Copy(values, sent, 32);
            Array.Copy(values, pending.Values, 32);
        }
        // Fields to resend reliably (Refresh); cleared until they are sent unreliably again.
        internal uint TakeRecent()
        {
            uint mask = recent;
            recent = 0;
            return mask;
        }
        internal void RestoreRecent(uint mask) => recent |= mask;
        // This client's latest write of the field (staged or sent) in the current authority
        // period; false when it has not written the field since the period began.
        internal bool TryWritten(int field, out int value)
        {
            value = pending.Values[field];
            return FieldMask.Contains(written, field);
        }
        internal void CopySentTo(int[] destination) => Array.Copy(sent, destination, 32);

        // ---- What this client shows ----
        /// <summary>
        /// The values to present, per field: the prediction of an object this client predicts
        /// (IsPredicted); for the writer (owner or lease holder), its own latest write of the
        /// field in the current authority period, sent or not; otherwise the room's latest
        /// state. A writer sees its writes at once, and a late echo of an older write never shows.
        /// </summary>
        public IReadOnlyList<int> ShownValues
        {
            get
            {
                if (shown == null)
                    shown = new ShownView(this);
                return shown;
            }
        }
        ShownView shown;
        int Shown(int field)
        {
            if (IsPredicted)
                return predicted[field];
            int written;
            return WritableByMe && TryWritten(field, out written) ? written : values[field];
        }
        sealed class ShownView : IReadOnlyList<int>
        {
            readonly NetworkObject o;
            public ShownView(NetworkObject o)
            {
                this.o = o;
            }
            public int this[int field]
            {
                get
                {
                    if (field < 0 || field >= 32)
                        throw new ArgumentOutOfRangeException(nameof(field));
                    return o.Shown(field);
                }
            }
            public int Count => 32;
            public IEnumerator<int> GetEnumerator()
            {
                for (int f = 0; f < 32; f++)
                    yield return o.Shown(f);
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

        // ---- Prediction (NetworkWorld.RegisterPrediction) ----
        /// <summary>
        /// True while this client predicts the object: a simulated object it owns whose prefab
        /// has a registered prediction. Values stays the room's latest state.
        /// </summary>
        public bool IsPredicted { get; internal set; }
        readonly int[] predicted = new int[32], previousPredicted = new int[32], predictionError = new int[32];
        uint predictionErrorMask;
        /// <summary>
        /// Values after the newest predicted tick (NetworkWorld.InputTick); show them blended
        /// with PreviousPredictedValues by the alpha of Predicted.
        /// </summary>
        public IReadOnlyList<int> PredictedValues => predicted;
        /// <summary>Values after the tick before the newest predicted one.</summary>
        public IReadOnlyList<int> PreviousPredictedValues => previousPredicted;
        /// <summary>
        /// Raised every frame while predicted, after the prediction ran; the argument is the
        /// progress (0..1) from the previous predicted tick to the newest one.
        /// </summary>
        public event Action<float> Predicted;
        internal void NotifyPredicted(float alpha) { Predicted?.Invoke(alpha); }
        internal void SetPrediction(int[] current, int[] previous)
        {
            Array.Copy(current, predicted, 32);
            Array.Copy(previous, previousPredicted, 32);
        }
        internal void SetPredicted(int[] current) => Array.Copy(current, predicted, 32);
        internal void SetPreviousPredicted(int[] previous) => Array.Copy(previous, previousPredicted, 32);
        /// <summary>
        /// After the room disagreed with an earlier prediction: per field, the old predicted
        /// value minus the corrected one at the same tick. Fade this offset out to avoid a jump.
        /// </summary>
        public IReadOnlyList<int> PredictionError => predictionError;
        /// <summary>
        /// Raised after a correction of the prediction; the argument is the bit mask of the
        /// fields whose PredictionError is nonzero.
        /// </summary>
        public event Action<uint> PredictionCorrected;
        // Records the difference between the shown prediction and corrected values for
        // the same tick; returns whether they differ.
        internal bool MeasurePredictionError(int[] corrected)
        {
            uint mask = 0;
            for (int f = 0; f < 32; f++)
            {
                int error = unchecked(predicted[f] - corrected[f]);
                predictionError[f] = error;
                if (error != 0)
                    mask |= 1u << f;
            }
            predictionErrorMask = mask;
            return mask != 0;
        }
        internal void NotifyCorrected() { PredictionCorrected?.Invoke(predictionErrorMask); }
        /// <summary>
        /// The prediction despawned this object (Room.Despawn in a [Predict] method); the world
        /// hides its renderers until the room agrees or the prediction changes.
        /// </summary>
        public bool PredictedRemoved { get; internal set; }
    }
    /// <summary>
    /// Values of up to 32 slots (object fields or input slots, 0..31) and which slots are set,
    /// for Spawn and SendInput. Reuse one instance to avoid allocating per call. Slot indexes
    /// outside 0..31 throw ArgumentOutOfRangeException.
    /// </summary>
    public sealed class SlotValues
    {
        readonly int[] values = new int[32];
        /// <summary>Bit mask of the slots that are set.</summary>
        public uint Mask { get; private set; }
        /// <summary>The slot's value (0 when not set).</summary>
        public int this[int slot] => values[Slot(slot)];
        /// <summary>Whether the slot is set.</summary>
        public bool Has(int slot) => FieldMask.Contains(Mask, Slot(slot));
        /// <summary>Sets a slot; returns this instance for chaining.</summary>
        public SlotValues Set(int slot, int value)
        {
            values[Slot(slot)] = value;
            Mask |= 1u << slot;
            return this;
        }
        /// <summary>Unsets a slot; returns this instance for chaining.</summary>
        public SlotValues Remove(int slot)
        {
            Mask &= ~(1u << Slot(slot));
            values[slot] = 0;
            return this;
        }
        /// <summary>Unsets every slot; returns this instance for chaining.</summary>
        public SlotValues Clear()
        {
            Mask = 0;
            Array.Clear(values, 0, 32);
            return this;
        }
        /// <summary>Makes this instance a copy of other.</summary>
        public void CopyFrom(SlotValues other)
        {
            Mask = other.Mask;
            Array.Copy(other.values, values, 32);
        }
        internal void CopyTo(int[] destination) => Array.Copy(values, destination, 32);
        internal void CopyTo(Record r)
        {
            r.Mask = Mask;
            Array.Copy(values, r.Values, 32);
        }
        static int Slot(int slot) => slot >= 0 && slot < 32 ? slot : throw new ArgumentOutOfRangeException(nameof(slot), "Slots are 0..31");
    }
    /// <summary>
    /// An RPC received on target from sender. args borrow the received packet: consume them in
    /// the callback, or copy them (ToArray) to keep them.
    /// </summary>
    public delegate void RpcHandler(NetworkObject target, uint method, uint sender, ReadOnlySpan<byte> args);
    /// <summary>
    /// A room event received from sender. args borrow the received packet: consume them in the
    /// callback, or copy them (ToArray) to keep them.
    /// </summary>
    public delegate void RoomEventHandler(uint code, uint sender, ReadOnlySpan<byte> args);
    /// <summary>
    /// A property written by writer (value empty: deleted) in scope 0 (the room) or a player's
    /// connection ID. value borrows the received packet; copy it (ToArray) to keep it.
    /// </summary>
    public delegate void PropertyHandler(uint scope, uint writer, string key, ReadOnlySpan<byte> value);
    // How a SyncClient tells the world that presents its objects about the room's messages,
    // before raising its public events.
    internal interface ISyncPresenter
    {
        void Spawned(NetworkObject o);
        void Despawned(NetworkObject o);
        // Every update of a known object with its state version, including values older
        // than the object's (the world completes its confirmed state with them).
        void Updated(NetworkObject o, Record update, uint state);
        void RpcReceived(NetworkObject o, uint method, uint sender, byte[] args, int offset, int count);
        void EventReceived(uint code, uint sender, byte[] args, int offset, int count);
        void PropertyChanged(uint scope, string key, byte[] value, int offset, int count);
        void ClockReceived(uint tick, uint rate, uint latest);
    }
    /// <summary>
    /// The client side of a room's object synchronization: decodes the room's packets
    /// (Receive), keeps the room's objects and properties, and sends this client's writes,
    /// inputs and commands. Call Receive, Set, Flush and the other methods on one thread
    /// (Unity's main thread).
    /// </summary>
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
        /// <summary>This client's connection ID in the room.</summary>
        public uint ConnectionId
        {
            get; private set;
        }
        /// <summary>
        /// True once the room's snapshot is applied (SnapshotCompleted); false after Reset and
        /// while a new snapshot arrives. Set and SendInput throw while it is false.
        /// </summary>
        public bool Ready
        {
            get; private set;
        }
        /// <summary>
        /// A command now would be accepted: sent at once, or kept in the outbox while it has
        /// room (see MaxHeldPackets).
        /// </summary>
        public bool CanSend { get { return held.Count < MaxHeldPackets; } }
        /// <summary>
        /// Optional unreliable sender (e.g. a UDP side channel). When set, Flush and SendInput
        /// send through it; flushes before commands stay on the reliable sender so updates still
        /// precede the command. Call Refresh about once per second so values lost on the way are
        /// sent again (reliably).
        /// </summary>
        public TrySend Unreliable { get; set; }
        readonly Record[] refresh = new Record[SyncCodec.MaxRecords];
        // State version of this client's update packets, numbered in sending order over
        // both senders: the room keeps, per field, the value of the newest one, so a
        // datagram delayed behind a reliable update cannot undo it. Process-wide, so a
        // new SyncClient on the same connection never numbers below an earlier one.
        static int updateState;
        static uint NextUpdateState()
        {
            uint state = unchecked((uint)System.Threading.Interlocked.Increment(ref updateState));
            return state != 0 ? state : unchecked((uint)System.Threading.Interlocked.Increment(ref updateState));
        }
        /// <summary>
        /// The rules the room runs: the first 4 bytes of the SHA-256 of its GSL (0 for a room
        /// without rules), from the latest snapshot.
        /// </summary>
        public uint RulesHash
        {
            get; private set;
        }
        /// <summary>The room's master client (0 until the snapshot names it).</summary>
        public uint MasterId
        {
            get; private set;
        }
        /// <summary>The room objects this client currently knows.</summary>
        public Dictionary<uint, NetworkObject>.ValueCollection Objects
        {
            get
            {
                return objects.Values;
            }
        }
        /// <summary>Raised when an object arrives (in a snapshot or spawned later).</summary>
        public event Action<NetworkObject> Spawned;
        /// <summary>Raised when a room update changes an object's fields (object, changed-field mask).</summary>
        public event Action<NetworkObject, uint> Changed;
        /// <summary>Raised when an object is removed, also for every object a snapshot or Reset clears.</summary>
        public event Action<NetworkObject> Despawned;
        /// <summary>Raised for each RPC received on an object.</summary>
        public event RpcHandler RpcReceived;
        /// <summary>Raised for each room event received (RaiseEvent).</summary>
        public event RoomEventHandler EventReceived;
        /// <summary>Raised when a custom property changes, after TryGetProperty reflects the change.</summary>
        public event PropertyHandler PropertyChanged;
        /// <summary>A refused command, with the reason.</summary>
        public event Action<Rejection> Rejected;
        /// <summary>
        /// Raised for every authority state the room sends about an object, even one that
        /// changes neither holder nor Epoch (NetworkObject.AuthorityChanged is raised only then).
        /// </summary>
        public event Action<NetworkObject> AuthorityChanged;
        /// <summary>The answer to a lease request or interaction: (object ID, request ID, result).</summary>
        public event Action<uint, uint, CommandResult> CommandCompleted;
        /// <summary>
        /// A spawn request of this client (the ID Spawn returned) and the object the room made
        /// for it.
        /// </summary>
        public event Action<uint, NetworkObject> SpawnConfirmed;
        /// <summary>A spawn request (the ID Spawn returned) that the room refused.</summary>
        public event Action<uint, CommandResult> SpawnRejected;
        /// <summary>Raised with the new master's connection ID, and with the snapshot's at its start.</summary>
        public event Action<uint> MasterChanged;
        /// <summary>Raised when an object moves to another interest group (NetworkObject.Group).</summary>
        public event Action<NetworkObject> GroupChanged;
        /// <summary>A snapshot starts: objects, master and properties are replaced.</summary>
        public event Action SnapshotStarted;
        /// <summary>
        /// The snapshot is applied (Ready), raised after the packet, outside Receive. Commands
        /// made before (from Attach on) wait in the outbox and leave now, in call order; those
        /// made from here on leave at once, and those made inside later callbacks right after
        /// their packet.
        /// </summary>
        public event Action SnapshotCompleted;
        /// <summary>
        /// A ticking room's clock: (tick, ticks per second, newest input tick it received from
        /// this client).
        /// </summary>
        public event Action<uint, uint, uint> ClockReceived;
        /// <summary>
        /// State version of the latest clock: the newest update the room sent before it.
        /// Updates up to this version make up the room's state at the clock's tick.
        /// </summary>
        public uint ClockState { get; private set; }
        // The world that presents this client's objects (NetworkWorld): told of every
        // change before the public events, so that their observers find it up to date.
        internal ISyncPresenter Presenter;
        /// <summary>Ticks per second of a ticking room (0 until a clock arrives or when not ticking).</summary>
        public uint TickRate { get; private set; }
        /// <summary>How many recent ticks' inputs the client keeps for repeating in input packets.</summary>
        public const int InputHistory = 8;
        readonly Record[] inputs = new Record[InputHistory];
        int inputCount;
        int inputRedundancy = 3;
        /// <summary>
        /// Ticks per input packet (1..8, default 3): the newest input and the ones before it, so
        /// a lost packet's inputs arrive with a later one. Values outside the range are clamped.
        /// </summary>
        public int InputRedundancy
        {
            get => inputRedundancy;
            set => inputRedundancy = Math.Max(1, Math.Min(InputHistory, value));
        }
        /// <summary>
        /// Creates a client for this connection; trySend is the reliable transport. Throws
        /// ArgumentException when connectionId is 0 or trySend is null.
        /// </summary>
        public SyncClient(uint connectionId, TrySend trySend)
        {
            if (connectionId == 0 || trySend == null)
                throw new ArgumentException();
            ConnectionId = connectionId;
            send = trySend;
            reliable = Transmit;
        }
        // ---- Reliable sending: the outbox ----
        // Every reliable packet leaves through Transmit, in call order. A packet waits in
        // the outbox (held) while it cannot leave yet: before the snapshot completes,
        // inside callbacks (Receive), or when the transport refused it; the outbox drains
        // after each packet received, on the next command and on Flush. Reset and a new
        // snapshot after rejoining drop what is left.
        /// <summary>
        /// Most packets the outbox holds. Commands wait there before the snapshot completes,
        /// inside callbacks, or while the transport refuses them; when it is full, commands are
        /// not sent (they return false or 0).
        /// </summary>
        public const int MaxHeldPackets = 256;
        /// <summary>Objects a room holds at most (the room's MaxObjects).</summary>
        public const int MaxObjects = 4096;
        /// <summary>Ticks per second a ticking room runs at most (the room's MaxTickRate).</summary>
        public const int MaxTickRate = 60;
        readonly TrySend reliable;
        readonly Queue<byte[]> held = new Queue<byte[]>();
        bool Releasing => Ready && !receiving;
        bool Transmit(byte[] data, int count)
        {
            if (ReleaseHeld() && Releasing && send(data, count))
                return true;
            if (held.Count >= MaxHeldPackets)
                return false;
            var copy = new byte[count];
            Buffer.BlockCopy(data, 0, copy, 0, count);
            held.Enqueue(copy);
            return true;
        }
        // Sends what the outbox holds, when it may; true once it is empty.
        bool ReleaseHeld()
        {
            if (!Releasing)
                return held.Count == 0;
            while (held.Count > 0)
            {
                var packet = held.Peek();
                if (!send(packet, packet.Length))
                    return false;
                held.Dequeue();
            }
            return true;
        }
        /// <summary>The object with this ID, or null when this client does not know it.</summary>
        public NetworkObject Find(uint id)
        {
            NetworkObject v;
            return objects.TryGetValue(id, out v) ? v : null;
        }
        // Inputs need the snapshot (commands wait in the outbox instead).
        void CheckReady()
        {
            if (!Ready)
                throw new InvalidOperationException("Wait for snapshot completion (SnapshotCompleted)");
        }
        // The one way commands leave: this client's writes made before a command are sent
        // first, reliably, so the room applies them before it (an RPC sees the values its
        // caller wrote). Writes go out on the unreliable sender only with Flush.
        bool SendCommand(Op op)
        {
            if (!FlushVia(reliable, false))
                return false;
            output[0] = command;
            int n = SyncCodec.Encode(buffer, op, output, 1);
            return reliable(buffer, n);
        }
        // ---- Answers to this client's requests ----
        // The callbacks of requests awaiting the room's answer, by request ID. Each runs
        // exactly once: with the room's answer, NotSent at once, or Abandoned.
        readonly Dictionary<uint, CommandCallback> awaitedCommands = new Dictionary<uint, CommandCallback>();
        readonly Dictionary<uint, SpawnCallback> awaitedSpawns = new Dictionary<uint, SpawnCallback>();
        uint Await(uint request, CommandCallback completed)
        {
            if (request == 0)
                completed(RequestStatus.NotSent, default(CommandResult));
            else
                awaitedCommands[request] = completed;
            return request;
        }
        static RequestStatus Answer(CommandResult result) =>
            result == CommandResult.Success || result == CommandResult.AlreadyApplied ? RequestStatus.Succeeded : RequestStatus.Refused;
        void AnswerCommand(uint request, CommandResult result)
        {
            CommandCallback completed;
            if (!awaitedCommands.TryGetValue(request, out completed))
                return;
            awaitedCommands.Remove(request);
            completed(Answer(result), result);
        }
        void AnswerSpawn(uint request, NetworkObject spawned, CommandResult result)
        {
            SpawnCallback completed;
            if (!awaitedSpawns.TryGetValue(request, out completed))
                return;
            awaitedSpawns.Remove(request);
            completed(spawned != null ? RequestStatus.Succeeded : RequestStatus.Refused, spawned, result);
        }
        /// <summary>
        /// Stops waiting for every request: their callbacks run with Abandoned. Reset and a new
        /// snapshot after rejoining do this (the room answers the requests of a session in that
        /// session only); a world detaching from this client does it too.
        /// </summary>
        public void AbandonRequests()
        {
            if (awaitedCommands.Count == 0 && awaitedSpawns.Count == 0)
                return;
            var commands = new List<CommandCallback>(awaitedCommands.Values);
            var spawns = new List<SpawnCallback>(awaitedSpawns.Values);
            awaitedCommands.Clear();
            awaitedSpawns.Clear();
            foreach (var completed in commands)
                completed(RequestStatus.Abandoned, default(CommandResult));
            foreach (var completed in spawns)
                completed(RequestStatus.Abandoned, null, default(CommandResult));
        }

        /// <summary>
        /// Requests an object whose fields start at the room's values (its defaults, or what the
        /// rules' Spawned sets). Returns the request ID (see SpawnConfirmed / SpawnRejected), or
        /// 0 when it was not sent (the outbox is full).
        /// </summary>
        public uint Spawn(uint prefab)
        {
            return Spawn(prefab, emptySlots);
        }
        /// <summary>
        /// Spawn with its answer: completed runs once, with the room's object (Succeeded), its
        /// refusal (Refused), NotSent (at once) or Abandoned.
        /// </summary>
        public uint Spawn(uint prefab, SpawnCallback completed)
        {
            return Spawn(prefab, emptySlots, completed);
        }
        /// <summary>Spawn with initial field values and its answer (see Spawn(uint, SpawnCallback)).</summary>
        public uint Spawn(uint prefab, SlotValues values, SpawnCallback completed)
        {
            if (completed == null)
                throw new ArgumentNullException(nameof(completed));
            uint request = Spawn(prefab, values);
            if (request == 0)
                completed(RequestStatus.NotSent, null, default(CommandResult));
            else
                awaitedSpawns[request] = completed;
            return request;
        }
        static readonly SlotValues emptySlots = new SlotValues();
        /// <summary>
        /// Requests a player or simulated object with the field values set in values. Returns
        /// the request ID, or 0 when it was not sent (the outbox is full).
        /// </summary>
        public uint Spawn(uint prefab, SlotValues values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            // Bit 31 marks the room's predicted-spawn keys.
            if (nextRequest == 0 || nextRequest >= 1u << 31)
                throw new InvalidOperationException("Request IDs exhausted");
            command.Clear();
            command.Id = nextRequest;
            command.Prefab = prefab;
            command.Mode = (byte)ObjectMode.Player;
            values.CopyTo(command);
            if (!SendCommand(Op.Spawn))
                return 0;
            return nextRequest++;
        }
        // ---- The objects commands address ----
        // Each command names the rule its object must satisfy; another object is the
        // caller's mistake.
        NetworkObject Require(uint id, Func<NetworkObject, bool> rule, string refusal)
        {
            var o = Find(id);
            if (o == null || !rule(o))
                throw new InvalidOperationException(refusal);
            return o;
        }
        NetworkObject MyOwnedObject(uint id) => Require(id, o => o.OwnedByMe, "Object is not owned by this connection");
        NetworkObject LeasedObject(uint id) => Require(id, o => o.Kind.Leased, "Not a leased object");
        NetworkObject ServerObject(uint id) => Require(id, o => o.Kind.Interactive, "Not a server object");
        NetworkObject MyDespawnableObject(uint id) => Require(id, o => o.DespawnableByMe, "Only the owner of a player object despawns it");
        NetworkObject RpcObject(uint id) => Require(id, o => o.Kind.Owned, "RPCs target player and simulated objects");
        NetworkObject TransferableObject(uint id) => Require(id, o => o.Kind.Transferable, "Ownership transfers player objects");
        /// <summary>
        /// Sends this client's input for a tick, together with the inputs of the
        /// InputRedundancy - 1 ticks before it, over the unreliable sender when one is set;
        /// slots not set in input read as 0. Requires Ready and a ticking room (TickRate &gt; 0);
        /// tick must be newer than the previous call's. Returns false when it was not sent.
        /// </summary>
        /// <param name="viewLag">
        /// How far the remote objects on screen trailed tick, in 1/16 ticks; the room rewinds hit
        /// tests made for this client's objects by it (lag compensation).
        /// </param>
        public bool SendInput(uint tick, SlotValues input, uint viewLag = 0)
        {
            CheckReady();
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (TickRate == 0)
                throw new InvalidOperationException("The room is not ticking");
            bool advances = inputCount == 0 || Serial.After(tick, inputs[(inputCount - 1) % InputHistory].Id);
            if (!advances)
                throw new ArgumentOutOfRangeException(nameof(tick), "Inputs must advance");
            var r = inputs[inputCount % InputHistory] ?? (inputs[inputCount % InputHistory] = new Record());
            r.Clear();
            r.Id = tick;
            r.Epoch = viewLag;
            input.CopyTo(r);
            inputCount++;
            int count = Math.Min(inputRedundancy, Math.Min(inputCount, InputHistory));
            for (int i = 0; i < count; i++)
                output[i] = inputs[(inputCount - count + i) % InputHistory];
            int n = SyncCodec.Encode(buffer, Op.Input, output, count);
            return (Unreliable ?? reliable)(buffer, n);
        }
        /// <summary>
        /// Stages a write of an object's field for the next Flush. Allowed inside callbacks
        /// (nothing is sent). Requires Ready and write authority (CanWrite) and a field the
        /// object has; writing back the last sent value cancels the staged write.
        /// </summary>
        public void Set(uint id, int field, int value)
        {
            CheckReady();
            var o = Require(id, x => x.WritableByMe, "No write authority");
            if (!o.HasField(field))
                throw new ArgumentOutOfRangeException("field");
            o.StageWrite(field, value);
        }
        /// <summary>
        /// Sends the outbox and the pending owner and lease-holder writes, batched per packet,
        /// through the unreliable sender when one is set. Returns false when a packet was not
        /// sent; its writes stay pending for the next Flush.
        /// </summary>
        public bool Flush()
        {
            var unreliable = Unreliable;
            ReleaseHeld();
            return FlushVia(unreliable ?? reliable, unreliable != null);
        }
        /// <summary>
        /// Largest unreliable update packet, in bytes: it fits one datagram, as the room's do
        /// (larger ones would be fragmented on the way).
        /// </summary>
        public const int MaxDatagram = 1000;
        const int HeaderSize = 8; // with the largest state version
        bool FlushVia(TrySend via, bool unreliable)
        {
            if (!Ready) return true; // nothing is written before the snapshot
            int count = 0, size = HeaderSize;
            int limit = unreliable ? MaxDatagram : SyncCodec.MaxPacket;
            foreach (var o in objects.Values)
            {
                if (!o.HasPendingWrites)
                    continue;
                var record = o.PendingWrites;
                int k = SyncCodec.UpdateSize(record);
                bool full = count == output.Length || (count > 0 && size + k > limit);
                if (full)
                {
                    if (!FlushBatch(count, via, unreliable))
                        return false;
                    count = 0;
                    size = HeaderSize;
                }
                output[count++] = record;
                size += k;
            }
            return count == 0 || FlushBatch(count, via, unreliable);
        }
        // Before this client's authority over an object ends (a transfer, a lease
        // release), its pending writes go out reliably, then the values it sent over UDP
        // since the last refresh are sent again reliably: a lost or delayed datagram
        // would arrive in the next period and be dropped, so the room would keep an
        // older value. Writes just sent reliably are not resent.
        bool HandOverWrites(NetworkObject o)
        {
            return FlushVia(reliable, false) && ConfirmSent(o);
        }
        bool ConfirmSent(NetworkObject o)
        {
            uint mask = o.TakeRecent();
            if (mask == 0)
                return true;
            var r = refresh[0] ?? (refresh[0] = new Record());
            r.Clear();
            r.Id = o.Id;
            r.Epoch = o.Epoch;
            r.Mask = mask;
            o.CopySentTo(r.Values);
            if (reliable(buffer, SyncCodec.Encode(buffer, Op.Update, NextUpdateState(), refresh, 1)))
                return true;
            o.RestoreRecent(mask);
            return false;
        }
        bool FlushBatch(int count, TrySend via, bool unreliable)
        {
            int n = SyncCodec.Encode(buffer, Op.Update, NextUpdateState(), output, count);
            if (!via(buffer, n))
                return false;
            for (int i = 0; i < count; i++)
                objects[output[i].Id].WritesSent(unreliable);
            return true;
        }
        /// <summary>
        /// Resends the current values of fields sent unreliably since the last call. The resend
        /// is reliable, so the final value of every field reaches the room however many datagrams
        /// were lost; its state version keeps it from undoing newer values. Call it about once
        /// per second while Unreliable is used.
        /// </summary>
        public bool Refresh()
        {
            if (!Ready) return true; // nothing is written before the snapshot
            int count = 0;
            foreach (var o in objects.Values)
            {
                uint mask = o.TakeRecent();
                if (mask == 0)
                    continue;
                // Resent even after the unreliable sender was removed (UDP went inactive):
                // the values it lost reach the room only this way.
                if (!CanWrite(o.Id))
                    continue;
                var r = refresh[count] ?? (refresh[count] = new Record());
                r.Clear();
                r.Id = o.Id;
                r.Epoch = o.Epoch;
                r.Mask = mask;
                o.CopySentTo(r.Values);
                if (++count == refresh.Length)
                {
                    if (!SendRefresh(count))
                        return false;
                    count = 0;
                }
            }
            return count == 0 || SendRefresh(count);
        }
        bool SendRefresh(int count)
        {
            if (reliable(buffer, SyncCodec.Encode(buffer, Op.Update, NextUpdateState(), refresh, count)))
                return true;
            // Not sent: the next Refresh tries again.
            for (int i = 0; i < count; i++)
                if (objects.TryGetValue(refresh[i].Id, out var o))
                    o.RestoreRecent(refresh[i].Mask);
            return false;
        }
        /// <summary>
        /// This client may write the object's fields (Set): it is Ready and holds a kind the
        /// holder writes. False for unknown objects.
        /// </summary>
        public bool CanWrite(uint id) => Ready && (Find(id)?.WritableByMe ?? false);
        /// <summary>
        /// This client drives the object: it holds it (owns a player or simulated object, or
        /// holds a leased object's lease). The holder of a simulated object drives it with inputs
        /// (NetworkWorld.SetInput) while the rules write its fields.
        /// </summary>
        public bool Controls(uint id) => Ready && (Find(id)?.IsMine ?? false);
        /// <summary>Lease with its answer: completed runs once (see RequestStatus).</summary>
        public uint Lease(uint id, LeaseAction action, CommandCallback completed)
        {
            if (completed == null)
                throw new ArgumentNullException(nameof(completed));
            return Await(Lease(id, action), completed);
        }
        /// <summary>Interact with its answer: completed runs once (see RequestStatus).</summary>
        public uint Interact(uint id, uint method, CommandCallback completed)
        {
            if (completed == null)
                throw new ArgumentNullException(nameof(completed));
            return Await(Interact(id, method), completed);
        }
        /// <summary>Interact with its answer: completed runs once (see RequestStatus).</summary>
        public uint Interact(uint id, uint method, ReadOnlySpan<byte> args, CommandCallback completed)
        {
            if (completed == null)
                throw new ArgumentNullException(nameof(completed));
            return Await(Interact(id, method, args), completed);
        }
        /// <summary>
        /// Acquires, renews or releases a leased object's lease. Releasing a held lease first
        /// sends this client's writes reliably. Returns the request ID (answered by
        /// CommandCompleted), or 0 when it was not sent.
        /// </summary>
        public uint Lease(uint id, LeaseAction action) {
            var o = LeasedObject(id);
            // Only the holder hands its writes over; a lease that already expired has none.
            bool handingOver = action == LeaseAction.Release && o.LeaseHeldByMe;
            if (handingOver && !HandOverWrites(o)) return 0;
            command.Clear(); command.Id = id; command.Epoch = o.Epoch; command.Kind = (byte)action;
            return SendRequest(Op.Authority);
        }
        /// <summary>
        /// Runs the rules' Interact(state, objectId, method) on a server object. Rule-based rooms
        /// take no arguments (args are reserved for host-defined actions; rules refuse them).
        /// args are sent whole unless count (0..args.Length) gives how many of their first bytes
        /// to send; -1, the default, means all of them. Returns the request ID (answered by
        /// CommandCompleted), or 0 when it was not sent.
        /// </summary>
        public uint Interact(uint id, uint method, byte[] args = null, int count = -1) {
            ServerObject(id);
            if (count == -1) count = args?.Length ?? 0;
            if (count < 0 || count > (args?.Length ?? 0)) throw new ArgumentOutOfRangeException(nameof(count));
            command.Clear(); command.Id = id; command.Method = method; command.Args = args; command.ArgsCount = count;
            return SendRequest(Op.Call);
        }
        /// <summary>Interact with arguments from a span (copied before return).</summary>
        public uint Interact(uint id, uint method, ReadOnlySpan<byte> args)
        {
            var copy = Args(args);
            return Interact(id, method, copy, args.Length);
        }
        uint SendRequest(Op op) {
            if (nextCommand == 0) throw new InvalidOperationException("Command IDs exhausted");
            command.Request = nextCommand;
            return SendCommand(op) ? nextCommand++ : 0;
        }
        /// <summary>
        /// Despawns an object this client may despawn (DespawnableByMe; throws otherwise).
        /// Returns false when it was not sent.
        /// </summary>
        public bool Destroy(uint id)
        {
            MyDespawnableObject(id);
            command.Clear();
            command.Id = id;
            return SendCommand(Op.Despawn);
        }
        // ---- RPCs and room events ----
        /// <summary>
        /// An RPC on a player or simulated object, to target's receivers. Calls on another
        /// player's object need the method in the prefab's publicMethods; the room refuses other
        /// non-owner calls. Returns false when it was not sent.
        /// </summary>
        public bool Rpc(uint id, uint method, RpcTarget target)
        {
            return Rpc(id, method, target, ReadOnlySpan<byte>.Empty);
        }
        /// <summary>Rpc with arguments (at most SyncCodec.MaxArgs bytes, copied before return).</summary>
        public bool Rpc(uint id, uint method, RpcTarget target, ReadOnlySpan<byte> args)
        {
            var copy = Args(args);
            return Rpc(id, method, target, copy, args.Length);
        }
        /// <summary>
        /// Rpc with arguments: args are sent whole unless count gives how many of their first
        /// bytes to send (-1, the default, sends all of them).
        /// </summary>
        public bool Rpc(uint id, uint method, RpcTarget target, byte[] args, int count = -1)
        {
            return SendRpc(id, method, Receivers(target), 0, args, count);
        }
        /// <summary>An RPC to one player (a connection ID, not 0), not buffered.</summary>
        public bool RpcTo(uint id, uint method, uint recipient)
        {
            return RpcTo(id, method, recipient, ReadOnlySpan<byte>.Empty);
        }
        /// <summary>RpcTo with arguments (copied before return).</summary>
        public bool RpcTo(uint id, uint method, uint recipient, ReadOnlySpan<byte> args)
        {
            var copy = Args(args);
            return RpcTo(id, method, recipient, copy, args.Length);
        }
        /// <summary>RpcTo with arguments; count as in Rpc.</summary>
        public bool RpcTo(uint id, uint method, uint recipient, byte[] args, int count = -1)
        {
            return SendRpc(id, method, RpcTargetByte.Player, Recipient(recipient), args, count);
        }
        /// <summary>
        /// A room event (not tied to an object), like PhotonNetwork.RaiseEvent: any member may
        /// raise any code, to All or Others (optionally Buffered); Owner throws. Returns false
        /// when it was not sent.
        /// </summary>
        public bool RaiseEvent(uint code, RpcTarget target)
        {
            return RaiseEvent(code, target, ReadOnlySpan<byte>.Empty);
        }
        /// <summary>RaiseEvent with arguments (copied before return).</summary>
        public bool RaiseEvent(uint code, RpcTarget target, ReadOnlySpan<byte> args)
        {
            var copy = Args(args);
            return RaiseEvent(code, target, copy, args.Length);
        }
        /// <summary>RaiseEvent with arguments; count as in Rpc.</summary>
        public bool RaiseEvent(uint code, RpcTarget target, byte[] args, int count = -1)
        {
            return SendEvent(code, Receivers(target), 0, args, count);
        }
        /// <summary>A room event to one player (a connection ID, not 0), not buffered.</summary>
        public bool RaiseEventTo(uint code, uint recipient)
        {
            return RaiseEventTo(code, recipient, ReadOnlySpan<byte>.Empty);
        }
        /// <summary>RaiseEventTo with arguments (copied before return).</summary>
        public bool RaiseEventTo(uint code, uint recipient, ReadOnlySpan<byte> args)
        {
            var copy = Args(args);
            return RaiseEventTo(code, recipient, copy, args.Length);
        }
        /// <summary>RaiseEventTo with arguments; count as in Rpc.</summary>
        public bool RaiseEventTo(uint code, uint recipient, byte[] args, int count = -1)
        {
            return SendEvent(code, RpcTargetByte.Player, Recipient(recipient), args, count);
        }
        // The target byte of an RpcTarget (a value outside the enum is refused).
        static byte Receivers(RpcTarget target)
        {
            byte value = (byte)target;
            if (!RpcTargetByte.IsUnaddressed(value))
                throw new ArgumentOutOfRangeException(nameof(target));
            return value;
        }
        static uint Recipient(uint recipient)
        {
            if (recipient == 0)
                throw new ArgumentOutOfRangeException(nameof(recipient), "A player is a connection ID (not 0)");
            return recipient;
        }
        // The forms above in one: target with its recipient (Player) or without (0).
        internal bool SendRpc(uint id, uint method, byte target, uint recipient, byte[] args, int count)
        {
            RpcObject(id);
            return Call(id, method, target, recipient, args, count);
        }
        internal bool SendEvent(uint code, byte target, uint recipient, byte[] args, int count)
        {
            if (RpcTargetByte.Receivers(target) == RpcTargetByte.Owner)
                throw new ArgumentException("Room events have no owner");
            return Call(0, code, target, recipient, args, count);
        }
        // Arguments from a span, in a reused buffer (the packet is encoded before return).
        readonly byte[] spanArgs = new byte[SyncCodec.MaxArgs];
        byte[] Args(ReadOnlySpan<byte> args)
        {
            if (args.Length > spanArgs.Length)
                throw new ArgumentException("Arguments exceed " + SyncCodec.MaxArgs + " bytes");
            args.CopyTo(spanArgs);
            return spanArgs;
        }
        // A recipient goes with the Player target only: one with another target would be
        // ignored and the call reach everyone.
        bool Call(uint id, uint method, byte target, uint recipient, byte[] args, int count)
        {
            if (!RpcTargetByte.IsValid(target) || RpcTargetByte.HasRecipient(target) != (recipient != 0))
                throw new ArgumentException("A recipient goes with the Player target only, and the Player target needs one");
            if (count == -1)
                count = args?.Length ?? 0;
            if (count < 0 || count > (args?.Length ?? 0))
                throw new ArgumentOutOfRangeException(nameof(count));
            command.Clear();
            command.Id = id;
            command.Method = method;
            command.Target = target;
            command.Recipient = recipient;
            command.Args = args;
            command.ArgsCount = count;
            return SendCommand(Op.Call);
        }
        /// <summary>
        /// Hands a player object to another member (from its owner), or takes it over
        /// (newOwner = ConnectionId) when the prefab allows takeover. The owner's pending writes
        /// are sent reliably first. Returns false when it was not sent.
        /// </summary>
        public bool Transfer(uint id, uint newOwner)
        {
            if (newOwner == 0)
                throw new InvalidOperationException("Ownership transfers to a member");
            var o = TransferableObject(id);
            bool handingOver = o.IsMine; // the owner's writes go out before the transfer
            if (handingOver && !HandOverWrites(o))
                return false;
            command.Clear();
            command.Id = id;
            command.Kind = AuthorityKind.Transfer;
            command.Owner = newOwner;
            return SendCommand(Op.Authority);
        }
        /// <summary>Moves an object this client owns to an interest group (0 = everyone).</summary>
        public bool SetGroup(uint id, uint group)
        {
            MyOwnedObject(id);
            command.Clear();
            command.Id = id;
            command.Kind = AuthorityKind.Group;
            command.Owner = group;
            return SendCommand(Op.Authority);
        }
        /// <summary>
        /// Starts or stops receiving an interest group's updates and calls. Group 0 is always
        /// received (passing it throws).
        /// </summary>
        public bool Subscribe(uint group, bool subscribe)
        {
            if (group == 0)
                throw new ArgumentOutOfRangeException("group", "Group 0 is always received");
            command.Clear();
            command.Kind = RoomKind.Interest;
            command.Id = group;
            command.Owner = subscribe ? 1u : 0u;
            return SendCommand(Op.Room);
        }
        /// <summary>Names another master client. Only the current master may.</summary>
        public bool SetMaster(uint connectionId)
        {
            command.Clear();
            command.Kind = RoomKind.Master;
            command.Id = connectionId;
            return SendCommand(Op.Room);
        }
        // ---- Custom properties ----
        // Raw values by scope (0: the room; a connection ID: that player's) and key. The
        // room echoes every write; the stored value changes when the echo arrives.
        /// <summary>Longest property key, in UTF-8 bytes.</summary>
        public const int MaxPropertyKey = 64;
        /// <summary>The scope of the room's own properties (a player's scope is its connection ID).</summary>
        public const uint RoomScope = 0;
        static readonly System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false, true);
        readonly Dictionary<uint, Dictionary<string, byte[]>> properties = new Dictionary<uint, Dictionary<string, byte[]>>();
        readonly byte[] propertyArgs = new byte[SyncCodec.MaxArgs];
        /// <summary>The keys of a scope's properties (read their values with TryGetProperty).</summary>
        public IReadOnlyCollection<string> PropertyKeys(uint scope)
        {
            return properties.TryGetValue(scope, out var values) ? values.Keys : (IReadOnlyCollection<string>)Array.Empty<string>();
        }
        /// <summary>
        /// The stored value, read-only; false when there is none. The client keeps the bytes;
        /// copy them to keep a version.
        /// </summary>
        public bool TryGetProperty(uint scope, string key, out ReadOnlyMemory<byte> value)
        {
            value = default(ReadOnlyMemory<byte>);
            if (!properties.TryGetValue(scope, out var values) || !values.TryGetValue(key, out var stored))
                return false;
            value = stored;
            return true;
        }
        /// <summary>
        /// Writes a property of the room (RoomScope) or this player (ConnectionId); an empty value
        /// deletes it. Keys are 1..64 UTF-8 bytes, and key and value together fit
        /// SyncCodec.MaxArgs bytes (ArgumentException otherwise). The stored value changes when
        /// the room's echo arrives (PropertyChanged).
        /// </summary>
        public bool SetProperty(uint scope, string key, ReadOnlySpan<byte> value)
        {
            int length = string.IsNullOrEmpty(key) ? 0 : utf8.GetByteCount(key);
            if (length == 0 || length > MaxPropertyKey)
                throw new ArgumentException("Property keys are 1.." + MaxPropertyKey + " UTF-8 bytes");
            if (SyncCodec.Size((uint)length) + length + value.Length > propertyArgs.Length)
                throw new ArgumentException("Property exceeds " + SyncCodec.MaxArgs + " bytes");
            int n = 0;
            SyncCodec.Put(propertyArgs, ref n, (uint)length);
            n += utf8.GetBytes(key, 0, key.Length, propertyArgs, n);
            value.CopyTo(new Span<byte>(propertyArgs, n, value.Length));
            command.Clear();
            command.Kind = RoomKind.Property;
            command.Id = scope;
            command.Args = propertyArgs;
            command.ArgsCount = n + value.Length;
            return SendCommand(Op.Room);
        }
        /// <summary>Writes a property as UTF-8 text (an empty text deletes it, like an empty value).</summary>
        public bool SetTextProperty(uint scope, string key, string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            return SetProperty(scope, key, new ReadOnlySpan<byte>(utf8.GetBytes(text)));
        }
        /// <summary>The stored value as UTF-8 text (false when there is none, or it is not UTF-8).</summary>
        public bool TryGetTextProperty(uint scope, string key, out string text)
        {
            text = null;
            ReadOnlyMemory<byte> value;
            if (!TryGetProperty(scope, key, out value))
                return false;
            try
            {
                text = utf8.GetString(value.ToArray());
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        /// <summary>Deletes a property (writes an empty value).</summary>
        public bool DeleteProperty(uint scope, string key)
        {
            return SetProperty(scope, key, ReadOnlySpan<byte>.Empty);
        }
        // Applies a property the room announced; returns the key and the value's place.
        string ApplyProperty(uint scope, byte[] args, int offset, int count, out int valueOffset, out int valueCount)
        {
            int at = offset, end = offset + count;
            uint length;
            try
            {
                length = SyncCodec.Get(args, ref at, end);
            }
            catch (FormatException e)
            {
                throw new FormatException("Invalid property key", e);
            }
            if (length == 0 || length > MaxPropertyKey || length > end - at)
                throw new FormatException("Invalid property key");
            string key;
            try
            {
                key = utf8.GetString(args, at, (int)length);
            }
            catch (ArgumentException e)
            {
                throw new FormatException("Invalid UTF-8 in property key", e);
            }
            valueOffset = at + (int)length;
            valueCount = end - valueOffset;
            if (!properties.TryGetValue(scope, out var values))
            {
                if (valueCount == 0)
                    return key;
                values = properties[scope] = new Dictionary<string, byte[]>();
            }
            if (valueCount == 0)
            {
                values.Remove(key);
                if (values.Count == 0)
                    properties.Remove(scope);
            }
            else
            {
                var copy = new byte[valueCount];
                Buffer.BlockCopy(args, valueOffset, copy, 0, valueCount);
                values[key] = copy;
            }
            return key;
        }
        /// <summary>
        /// Call when the SDK changes MyConnectionId, then consume a fresh snapshot. Clears the
        /// objects (raising Despawned), properties and outbox, and abandons awaited requests.
        /// Throws ArgumentException for connection ID 0 or when called inside Receive.
        /// </summary>
        public void Reset(uint connectionId)
        {
            if (connectionId == 0 || receiving)
                throw new ArgumentException();
            ConnectionId = connectionId;
            Ready = false;
            TickRate = 0;
            ClockState = 0;
            inputCount = 0;
            MasterId = 0;
            RulesHash = 0;
            properties.Clear();
            // Held packets and awaited requests belong to the previous room.
            held.Clear();
            ClearObjects();
            AbandonRequests();
        }
        void ClearObjects()
        {
            foreach (var o in objects.Values)
            {
                Presenter?.Despawned(o);
                Despawned?.Invoke(o);
            }
            objects.Clear();
        }
        /// <summary>
        /// Applies one packet from the room and raises the events it causes. Not reentrant: do
        /// not call it from its callbacks. Throws FormatException for a packet it cannot apply.
        /// </summary>
        public void Receive(byte[] bytes, int count)
        {
            if (receiving)
                throw new InvalidOperationException("Reentrant Receive");
            input.Decode(bytes, count);
            receiving = true;
            bool completed = false;
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
                                completed = true;
                                break;
                            }
                            // A snapshot after a completed one (rejoined) replaces every object
                            // and property: held commands no longer apply and the previous
                            // session's requests get no answer. Before the first snapshot, or
                            // when one restarts before completing (Ready false), the outbox and
                            // its requests are kept: nothing was sent since, so they reach this
                            // session.
                            if (Ready)
                            {
                                held.Clear();
                                AbandonRequests();
                            }
                            Ready = false;
                            ClearObjects();
                            properties.Clear();
                            MasterId = r.Owner;
                            RulesHash = r.Epoch;
                            SnapshotStarted?.Invoke();
                            MasterChanged?.Invoke(MasterId);
                            break;
                        case Op.Spawn:
                            if (objects.ContainsKey(r.Id) || objects.Count >= MaxObjects)
                                throw new FormatException("Duplicate object or capacity exceeded");
                            o = NetworkObject.Spawned(r, ConnectionId);
                            objects.Add(o.Id, o);
                            Presenter?.Spawned(o);
                            Spawned?.Invoke(o);
                            if (o.ConfirmsMySpawn)
                            {
                                SpawnConfirmed?.Invoke(o.Request, o);
                                AnswerSpawn(o.Request, o, CommandResult.Success);
                            }
                            break;
                        case Op.Update:
                            o = Find(r.Id);
                            // Unreliable updates may overtake a spawn or trail a despawn.
                            if (o == null)
                                break;
                            if (o.HasUnknownFields(r.Mask))
                                throw new FormatException("Unknown field");
                            uint state = input.State;
                            uint applied = o.ApplyUpdate(r, state);
                            Presenter?.Updated(o, r, state);
                            if (applied == 0)
                                break;
                            o.Notify(applied);
                            Changed?.Invoke(o, applied);
                            break;
                        case Op.Despawn:
                            o = Find(r.Id);
                            if (o == null)
                                throw new FormatException("Unknown object");
                            objects.Remove(r.Id);
                            Presenter?.Despawned(o);
                            Despawned?.Invoke(o);
                            break;
                        case Op.Call:
                            if (r.Id == 0)
                            {
                                Presenter?.EventReceived(r.Method, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                                EventReceived?.Invoke(r.Method, r.Owner, new ReadOnlySpan<byte>(r.Args, r.ArgsOffset, r.ArgsCount));
                                break;
                            }
                            o = Find(r.Id);
                            if (o == null)
                                throw new FormatException("Unknown RPC object");
                            Presenter?.RpcReceived(o, r.Method, r.Owner, r.Args, r.ArgsOffset, r.ArgsCount);
                            RpcReceived?.Invoke(o, r.Method, r.Owner, new ReadOnlySpan<byte>(r.Args, r.ArgsOffset, r.ArgsCount));
                            break;
                        case Op.Authority:
                            o = Find(r.Id);
                            if (o != null && r.Kind == AuthorityKind.Group)
                            {
                                o.MoveToGroup(r.Owner);
                                GroupChanged?.Invoke(o);
                                break;
                            }
                            bool describesAHolder = o != null && r.Kind == AuthorityKind.State && o.Kind.HasHolder;
                            if (!describesAHolder)
                                throw new FormatException("Unknown authority object");
                            if (o.ApplyAuthority(r))
                                o.NotifyAuthority();
                            AuthorityChanged?.Invoke(o);
                            break;
                        case Op.Result:
                            if (r.Id != 0)
                            {
                                Find(r.Id)?.NotifyCommand(r.Request, (CommandResult)r.Method);
                                CommandCompleted?.Invoke(r.Id, r.Request, (CommandResult)r.Method);
                                AnswerCommand(r.Request, (CommandResult)r.Method);
                            }
                            else if (r.Request != 0)
                            {
                                SpawnRejected?.Invoke(r.Request, (CommandResult)r.Method);
                                AnswerSpawn(r.Request, null, (CommandResult)r.Method);
                            }
                            else
                                Rejected?.Invoke(new Rejection((Op)r.Kind, r.Owner, (CommandResult)r.Method));
                            break;
                        case Op.Clock:
                            if (r.TTL == 0 || r.TTL > MaxTickRate)
                                throw new FormatException("Invalid tick rate");
                            TickRate = r.TTL;
                            ClockState = input.State;
                            Presenter?.ClockReceived(r.Id, r.TTL, r.Request);
                            ClockReceived?.Invoke(r.Id, r.TTL, r.Request);
                            break;
                        case Op.Room:
                            if (r.Kind == RoomKind.Master)
                            {
                                MasterId = r.Id;
                                MasterChanged?.Invoke(MasterId);
                            }
                            else
                            {
                                string key = ApplyProperty(r.Id, r.Args, r.ArgsOffset, r.ArgsCount, out int at, out int length);
                                Presenter?.PropertyChanged(r.Id, key, r.Args, at, length);
                                PropertyChanged?.Invoke(r.Id, r.Owner, key, new ReadOnlySpan<byte>(r.Args, at, length));
                            }
                            break;
                        default:
                            throw new FormatException("Unknown sync operation");
                    }
                }
            }
            finally { receiving = false; }
            ReleaseHeld(); // commands made in the callbacks (or before the snapshot), in order
            if (completed)
                SnapshotCompleted?.Invoke();
        }
    }
}
