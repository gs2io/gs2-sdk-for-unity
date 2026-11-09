#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    // Client prediction of simulated objects. The room rules mark the method that moves
    // one object with [Predict]; the compiler emits it as C# (<RuleClass>Prediction.cs)
    // and its Register method calls RegisterPrediction. For each simulated object this
    // client owns, the world then runs that method once per tick with this client's
    // inputs, starting from the room's state at its latest clock (ServerTick) up to
    // InputTick. Every new clock starts again from the room's state, so a prediction
    // the room disagreed with is corrected within one round trip; the difference is
    // reported as PredictionError for presentation to fade out.
    public sealed partial class NetworkWorld
    {
        /// <summary>
        /// Ticks of inputs kept for re-simulation. When InputTick is further than this
        /// ahead of ConfirmedTick, predicted objects show the room's state instead.
        /// </summary>
        public const int PredictionWindow = 64;
        sealed class InputFrame
        {
            public uint Tick, Mask;
            public readonly int[] Values = new int[32];
            public int Value(int slot) => FieldMask.Contains(Mask, slot) ? Values[slot] : 0;
        }
        readonly Dictionary<uint, Action<int, int>> predictions = new Dictionary<uint, Action<int, int>>();
        // Prefabs in registration order: each tick runs one prefab's prediction over all
        // its objects before the next prefab's, as the rules' Tick runs the [Predict]
        // methods (the generated Register lists them in declaration order).
        readonly List<uint> predictionOrder = new List<uint>();
        readonly InputFrame[] inputFrames = new InputFrame[PredictionWindow];
        // Room state of simulated objects at confirmedTick: the values of state versions up
        // to the clock's (confirmedState). Updates and clocks travel over UDP and WebSocket,
        // so a value of that state may arrive after the clock and a later one before it.
        readonly Dictionary<uint, ConfirmedFields> confirmed = new Dictionary<uint, ConfirmedFields>();
        readonly Stack<ConfirmedFields> confirmedPool = new Stack<ConfirmedFields>();
        // One object's fields in the confirmed state: each value, and the state version it
        // is known as of when it has one (a spawn's initial value has none). A late update
        // older than that version changes nothing.
        sealed class ConfirmedFields
        {
            public readonly int[] Values = new int[32];
            readonly uint[] versions = new uint[32];
            uint versionedMask;
            public bool IsVersioned(int field) => FieldMask.Contains(versionedMask, field);
            public uint Version(int field) => versions[field];
            // The value is known as of a version newer than state: an update of state is
            // older than what the confirmed state holds.
            public bool KnowsNewerThan(int field, uint state) => IsVersioned(field) && Serial.Before(state, versions[field]);
            public void Set(int field, int value, uint version, bool versioned)
            {
                Values[field] = value;
                versions[field] = version;
                if (versioned)
                    versionedMask |= 1u << field;
                else
                    versionedMask &= ~(1u << field);
            }
        }
        uint confirmedState;
        readonly Dictionary<uint, int[]> scratch = new Dictionary<uint, int[]>();
        readonly Stack<int[]> valuePool = new Stack<int[]>();
        readonly Dictionary<int, List<uint>> prefabObjects = new Dictionary<int, List<uint>>();
        readonly List<NetworkObject> predictedObjects = new List<NetworkObject>();
        readonly List<uint> idBuffer = new List<uint>();
        uint confirmedTick, predictedTick, predictingTick;
        // Unconfirmed: no clock yet (nothing to predict from). Predicting: from the latest
        // confirmed state. Failed: a [Predict] method threw; the room's state is shown until
        // the next confirmed state or registration retries.
        enum PredictionPhase : byte { Unconfirmed, Predicting, Failed }
        PredictionPhase phase;
        // The prediction is recomputed on the next frame (inputs, state or registrations changed).
        bool predictionDirty;

        /// <summary>
        /// Registers the prediction of a prefab's simulated objects: step(objectId, tick) as
        /// generated from the [Predict] method. Generated Register methods call this; a
        /// prediction that failed is retried after a registration.
        /// </summary>
        public void RegisterPrediction(uint prefab, Action<int, int> step)
        {
            if (step == null)
                throw new ArgumentNullException(nameof(step));
            if (!predictions.ContainsKey(prefab))
                predictionOrder.Add(prefab);
            predictions[prefab] = step;
            predictionDirty = true;
            if (phase == PredictionPhase.Failed)
                phase = PredictionPhase.Predicting;
        }
        /// <summary>
        /// Stops predicting a prefab's objects; they are shown from the room's state again.
        /// </summary>
        public void UnregisterPrediction(uint prefab)
        {
            predictions.Remove(prefab);
            predictionOrder.Remove(prefab);
            predictionDirty = true;
        }
        /// <summary>
        /// The tick of the room state that prediction starts from (the latest clock).
        /// </summary>
        public uint ConfirmedTick => confirmedTick;

        void ResetPrediction()
        {
            foreach (var o in predictedObjects)
                o.IsPredicted = false;
            predictedObjects.Clear();
            foreach (var spawn in localSpawns)
                spawnPool.Push(spawn);
            localSpawns.Clear();
            unmatchedSpawns.Clear();
            predictedRemovals.Clear();
            corrections.Clear();
            foreach (var values in scratch.Values)
                valuePool.Push(values);
            scratch.Clear();
            foreach (var fields in confirmed.Values)
                confirmedPool.Push(fields);
            confirmed.Clear();
            confirmedState = 0;
            Array.Clear(inputFrames, 0, inputFrames.Length);
            phase = PredictionPhase.Unconfirmed;
            ResetSpawns();
            confirmedTick = predictedTick = 0;
            predictionDirty = true;
            if (Client == null)
                return;
            foreach (var o in Client.Objects)
                o.IsPredicted = false;
        }
        void RecordInput(uint tick)
        {
            var frame = inputFrames[tick % PredictionWindow] ??= new InputFrame();
            frame.Tick = tick;
            frame.Mask = input.Mask;
            input.CopyTo(frame.Values);
            predictionDirty = true;
        }
        InputFrame InputAt(uint tick)
        {
            // Like the room, a tick without input repeats the previous one.
            for (uint back = 0; back < PredictionWindow; back++)
            {
                var frame = inputFrames[(tick - back) % PredictionWindow];
                if (frame != null && frame.Tick == tick - back)
                    return frame;
            }
            return null;
        }
        void Confirm(uint tick, uint state)
        {
            if (phase != PredictionPhase.Unconfirmed && !Serial.After(tick, confirmedTick))
                return; // an older clock overtaken by a newer one
            phase = PredictionPhase.Predicting; // also retries a failed prediction
            confirmedTick = tick;
            confirmedState = state;
            predictionDirty = true;
            idBuffer.Clear();
            foreach (var id in confirmed.Keys)
                if (Client.Find(id) == null)
                    idBuffer.Add(id);
            foreach (var id in idBuffer)
            {
                confirmedPool.Push(confirmed[id]);
                confirmed.Remove(id);
            }
            foreach (var o in Client.Objects)
            {
                if (!o.Kind.Simulated)
                    continue;
                bool known = confirmed.TryGetValue(o.Id, out var fields);
                if (!known)
                    confirmed.Add(o.Id, fields = confirmedPool.Count > 0 ? confirmedPool.Pop() : new ConfirmedFields());
                for (int f = 0; f < 32; f++)
                {
                    bool confirmedVersioned = known && fields.IsVersioned(f);
                    if (o.IsNewer(f, state))
                    {
                        // The value overtook the clock: it belongs to a later tick. Use the
                        // value it replaced, unless the earlier confirmed value is newer.
                        // A versioned confirmed value is newer than a spawn's initial one.
                        bool priorUsable = o.TryPriorAsOf(f, state, out int prior, out uint priorVersion, out bool priorVersioned) &&
                            (!confirmedVersioned || priorVersioned && NotOlder(priorVersion, fields.Version(f)));
                        if (priorUsable)
                        {
                            fields.Set(f, prior, priorVersion, priorVersioned);
                            continue;
                        }
                        if (known)
                            continue; // keep the earlier confirmed value
                    }
                    o.Current(f, out int current, out _);
                    bool versioned = o.KnownAsOf(f, state, out uint version);
                    fields.Set(f, current, version, versioned);
                }
            }
        }
        // A value of the confirmed state that arrived after its clock (sent over WebSocket
        // while the clock took UDP, or a reliable resend): complete the confirmed state
        // and predict again from it.
        void OnUpdateReceived(NetworkObject o, Record r, uint state)
        {
            bool partOfConfirmedState = phase != PredictionPhase.Unconfirmed && !Serial.After(state, confirmedState);
            if (!partOfConfirmedState || !confirmed.TryGetValue(o.Id, out var fields))
                return;
            for (int f = 0; f < 32; f++)
            {
                if (!r.Has(f) || fields.KnowsNewerThan(f, state))
                    continue;
                bool changed = fields.Values[f] != r.Values[f];
                fields.Set(f, r.Values[f], state, true); // known as of this update now
                if (changed)
                    predictionDirty = true;
            }
        }
        readonly int[] liveValues = new int[32];
        // The object's confirmed values, or a copy of its latest values outside the
        // confirmed state (valid until the next call).
        int[] ConfirmedValues(NetworkObject o)
        {
            if (confirmed.TryGetValue(o.Id, out var fields))
                return fields.Values;
            o.CopyValuesTo(liveValues);
            return liveValues;
        }
        // State versions are serial numbers: a is not older than b.
        static bool NotOlder(uint a, uint b) => !Serial.Before(a, b);
        int[] Rent()
        {
            return valuePool.Count > 0 ? valuePool.Pop() : new int[32];
        }

        // ---- Predicted spawns ----
        // An object that a prediction spawned with SpawnOwned. Local IDs are negative
        // (-1 for the first spawn of a run); keys match the room's Record.Request.
        sealed class LocalSpawn : PredictedObject
        {
            public uint Key, Prefab, SpawnTick, Owner;
            public bool Mine, Removed;
            public readonly int[] Values = new int[32], Previous = new int[32];
            // This client's spawn, still made by the prediction: the room's object for it
            // (same prefab and key) takes over its instance.
            public bool AwaitsRoom => Mine && !Removed && Key != 0;
            public override uint PrefabId => Prefab;
            public override uint Holder => Owner;
            public override int Get(int field) => Values[CheckField(field)];
            public override void Set(int field, int value) => Values[CheckField(field)] = value;
            public override bool ReceivesMyInput => Mine;
            public override uint SpawnOwner(uint me) => Mine ? me : 0;
            public override void Despawn() => Removed = true;
            static int CheckField(int field)
            {
                if (field < 0 || field >= 32)
                    throw new ArgumentOutOfRangeException(nameof(field));
                return field;
            }
        }
        // What a [Predict] method's Room calls act on: an object of the room, or one the
        // prediction itself spawned (SpawnOwned). Each answers in its own way, so the
        // calls need not tell them apart.
        abstract class PredictedObject
        {
            public abstract uint PrefabId { get; }
            // Its owner or lease holder (0: none), as Room.OwnerOf reads it.
            public abstract uint Holder { get; }
            public abstract int Get(int field);
            public abstract void Set(int field, int value);
            // Its owner's input is this client's (the only input the prediction has).
            public abstract bool ReceivesMyInput { get; }
            // The member a SpawnOwned on its behalf belongs to (me: this client).
            public abstract uint SpawnOwner(uint me);
            public abstract void Despawn();
        }
        // A room object in the prediction: reads the confirmed state (or the room's latest
        // values), writes a working copy. One view is reused: valid until the next Resolve.
        sealed class RoomObjectView : PredictedObject
        {
            readonly NetworkWorld world;
            NetworkObject o;
            public RoomObjectView(NetworkWorld world) => this.world = world;
            public RoomObjectView Of(NetworkObject value)
            {
                o = value;
                return this;
            }
            public override uint PrefabId => o.Prefab;
            public override uint Holder => o.Owner;
            public override int Get(int field) => world.Values(o.Id)[CheckField(field)];
            public override void Set(int field, int value) => world.WorkingValues(o.Id)[CheckField(field)] = value;
            public override bool ReceivesMyInput => o.Kind.Simulated ? o.IsMine : throw new InvalidOperationException("Input requires a simulated object");
            public override uint SpawnOwner(uint me) => o.Kind.Owned && o.HasHolder ? o.Owner : throw new InvalidOperationException("SpawnOwned requires an object owned by a member");
            public override void Despawn()
            {
                if (o.Kind.HolderDespawns)
                    throw new InvalidOperationException("Player objects cannot be despawned by the rules");
                world.predictedRemovals.Add(o.Id);
            }
            int CheckField(int field)
            {
                if (!o.HasField(field))
                    throw new ArgumentOutOfRangeException(nameof(field), "Field " + field + " is not synchronized for prefab " + o.Prefab);
                return field;
            }
        }
        RoomObjectView roomView;
        // The object an ID names in the prediction: negative IDs are its own spawns.
        PredictedObject Resolve(int objectId) =>
            objectId < 0 ? (PredictedObject)Local(objectId) : (roomView ??= new RoomObjectView(this)).Of(Known(objectId));
        // The instance shown for a predicted spawn, from the prediction that makes it until
        // the room's object takes it over (Spawn), the prediction stops making it while
        // its tick is unconfirmed (a misprediction), or the room's spawn does not come in
        // time. Identified by (prefab, key): keys count spawns per owner and prefab, so
        // two prefabs spawned in one tick share a key (SpawnSlot).
        enum ShownState : byte
        {
            Predicted, // made by the latest prediction: moves with it
            Waiting,   // no longer predicted (its tick is confirmed): frozen, waits for the room
        }
        sealed class ShownSpawn
        {
            public NetworkEntity Entity;
            public NetworkTransform[] Motions;
            public uint SpawnTick, Prefab;
            public ShownState State;
            public readonly int[] Values = new int[32], Previous = new int[32];
        }
        readonly List<LocalSpawn> localSpawns = new List<LocalSpawn>();
        readonly Stack<LocalSpawn> spawnPool = new Stack<LocalSpawn>();
        readonly Dictionary<ulong, uint> spawnCounts = new Dictionary<ulong, uint>();
        readonly HashSet<uint> predictedRemovals = new HashSet<uint>();
        readonly Dictionary<ulong, ShownSpawn> shownSpawns = new Dictionary<ulong, ShownSpawn>();
        // The latest prediction's own spawns that no room object has taken over yet.
        readonly Dictionary<ulong, LocalSpawn> unmatchedSpawns = new Dictionary<ulong, LocalSpawn>();
        readonly List<ulong> slotBuffer = new List<ulong>();
        // Renderers this world disabled on objects the prediction despawned.
        readonly Dictionary<NetworkObject, List<Renderer>> hidden = new Dictionary<NetworkObject, List<Renderer>>();
        readonly List<NetworkObject> objectBuffer = new List<NetworkObject>();
        readonly List<NetworkObject> corrections = new List<NetworkObject>();
        InputFrame currentFrame;
        bool prefabListsDirty;
        /// <summary>
        /// Raised when a prediction shows a spawned object before the room confirms it.
        /// The instance is a placeholder (IsMine) that the room's object takes over; it
        /// is destroyed if the room does not spawn it.
        /// </summary>
        public event Action<NetworkEntity> PredictedSpawned;

        static ulong SpawnSlot(uint prefab, uint key)
        {
            return (ulong)prefab << 32 | key;
        }
        // Hands a shown predicted spawn's instance to the room's object (null when none is
        // shown, or the game destroyed it).
        NetworkEntity TakeShownSpawn(uint prefab, uint key)
        {
            ulong slot = SpawnSlot(prefab, key);
            if (!shownSpawns.TryGetValue(slot, out var shown))
                return null;
            shownSpawns.Remove(slot);
            return shown.Entity;
        }
        const uint PredictedSpawnBit = 1u << 31;
        static uint SpawnKey(uint tick, uint n)
        {
            return n >= 32 ? 0 : PredictedSpawnBit | (tick & ((1u << 26) - 1)) << 5 | n;
        }
        void ResetSpawns()
        {
            foreach (var shown in shownSpawns.Values)
                if (shown.Entity != null)
                    Discard(shown.Entity.gameObject);
            shownSpawns.Clear();
            foreach (var pair in hidden)
                foreach (var r in pair.Value)
                    if (r != null)
                        r.enabled = true;
            hidden.Clear();
        }

        void ServicePrediction()
        {
            if (predictionDirty)
            {
                predictionDirty = false;
                Predict();
            }
            // While sampling is paused the predicted tick does not advance: hold the newest.
            float alpha = pausedTicks > 0 ? 1 : Mathf.Clamp01((float)tickPhase);
            foreach (var o in predictedObjects)
            {
                try
                {
                    o.NotifyPredicted(alpha);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            foreach (var shown in shownSpawns.Values)
            {
                if (shown.Entity == null)
                    continue;
                float progress = shown.State == ShownState.Predicted ? alpha : 1;
                foreach (var motion in shown.Motions)
                    if (motion != null)
                        motion.ShowUnbound(shown.Previous, shown.Values, progress);
            }
        }
        void Predict()
        {
            // IsPredicted stays set on objects predicted before: Correct compares with them.
            bool active = predictions.Count != 0 && clockStarted && phase != PredictionPhase.Unconfirmed;
            foreach (var o in predictedObjects)
            {
                bool stillPredicted = active && (Predictable(o) || Unconfirmed(o));
                if (!stillPredicted)
                    o.IsPredicted = false;
            }
            predictedObjects.Clear();
            unmatchedSpawns.Clear();
            foreach (var spawn in localSpawns)
                spawnPool.Push(spawn);
            localSpawns.Clear();
            predictedRemovals.Clear();
            if (!active)
            {
                UpdateShownSpawns();
                UpdateHidden();
                return;
            }
            foreach (var o in Client.Objects)
                if (Predictable(o))
                    predictedObjects.Add(o);
            predictedObjects.Sort((a, b) => a.Id.CompareTo(b.Id));
            int span = Serial.Diff(InputTick, confirmedTick);
            uint oldTick = predictedTick;
            // The earlier prediction's tick is re-predicted in this run (after the confirmed
            // tick, not after InputTick): compare the two for corrections.
            bool repredictsOldTick = predictedTick != 0 && Serial.After(oldTick, confirmedTick) && !Serial.After(oldTick, InputTick);
            bool nothingToPredict = span <= 0 || span > PredictionWindow || phase == PredictionPhase.Failed;
            if (nothingToPredict)
            {
                // Show the room's state as the prediction.
                foreach (var o in predictedObjects)
                {
                    var values = ConfirmedValues(o);
                    o.SetPrediction(values, values);
                    o.IsPredicted = true;
                }
                predictedTick = 0;
                UpdateShownSpawns();
                UpdateHidden();
                return;
            }
            foreach (var values in scratch.Values)
                valuePool.Push(values);
            scratch.Clear();
            prefabListsDirty = true;
            corrections.Clear();
            PredictionRoom.Current = this;
            try
            {
                for (uint tick = confirmedTick + 1; !Serial.After(tick, InputTick); tick++)
                {
                    if (tick == InputTick)
                    {
                        foreach (var o in predictedObjects)
                            if (!predictedRemovals.Contains(o.Id))
                                o.SetPreviousPredicted(Values(o.Id));
                        foreach (var spawn in localSpawns)
                            Array.Copy(spawn.Values, spawn.Previous, 32);
                    }
                    predictingTick = tick;
                    currentFrame = InputAt(tick);
                    spawnCounts.Clear();
                    foreach (var prefab in predictionOrder)
                        PredictPrefab(prefab, tick);
                    if (repredictsOldTick && tick == oldTick)
                        foreach (var o in predictedObjects)
                            if (!predictedRemovals.Contains(o.Id))
                                Correct(o, Values(o.Id));
                }
            }
            catch (Exception e)
            {
                // The same method failing in the room would fail the room's Tick; stop
                // predicting rather than show a state the room cannot reach.
                phase = PredictionPhase.Failed;
                Debug.LogException(e, this);
                foreach (var o in predictedObjects)
                    o.IsPredicted = false;
                predictedObjects.Clear();
                foreach (var spawn in localSpawns)
                    spawnPool.Push(spawn);
                localSpawns.Clear();
                corrections.Clear();
                predictedRemovals.Clear();
                UpdateShownSpawns();
                UpdateHidden();
                predictionDirty = true; // show the room's state from the next frame
                return;
            }
            finally
            {
                PredictionRoom.Current = null;
            }
            // Observers run outside the prediction, so they cannot disturb it.
            foreach (var o in corrections)
            {
                try
                {
                    o.NotifyCorrected();
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            corrections.Clear();
            foreach (var o in predictedObjects)
            {
                if (!predictedRemovals.Contains(o.Id))
                    o.SetPredicted(Values(o.Id));
                o.IsPredicted = true;
            }
            predictedTick = InputTick;
            // The room's objects that a prediction spawned, before their spawn tick is
            // confirmed, show that prediction (they took over its instance).
            unmatchedSpawns.Clear();
            foreach (var spawn in localSpawns)
                if (spawn.AwaitsRoom)
                    unmatchedSpawns[SpawnSlot(spawn.Prefab, spawn.Key)] = spawn;
            foreach (var o in Client.Objects)
            {
                if (!Unconfirmed(o) || !unmatchedSpawns.TryGetValue(SpawnSlot(o.Prefab, o.Request), out var spawn))
                    continue;
                o.SetPrediction(spawn.Values, spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous);
                o.IsPredicted = true;
                predictedObjects.Add(o);
                unmatchedSpawns.Remove(SpawnSlot(o.Prefab, o.Request));
            }
            UpdateShownSpawns();
            UpdateHidden();
        }
        // One prefab's step of a tick: the room's objects in ID order, then the predicted
        // spawns of that prefab made so far (an earlier prefab's step may have spawned
        // them this tick; the room's loop counts them too).
        void PredictPrefab(uint prefab, uint tick)
        {
            var step = predictions[prefab];
            foreach (var o in predictedObjects)
                if (o.Prefab == prefab && !predictedRemovals.Contains(o.Id))
                    step(unchecked((int)o.Id), unchecked((int)tick));
            int spawned = localSpawns.Count;
            for (int i = 0; i < spawned; i++)
                if (localSpawns[i].Prefab == prefab && !localSpawns[i].Removed)
                    step(-(i + 1), unchecked((int)tick));
        }
        // Own simulated objects spawned by a prediction whose spawn tick the room has not
        // confirmed yet.
        bool Unconfirmed(NetworkObject o)
        {
            return IsLiveSimulatedOfMine(o) && o.HasPredictedSpawnKey && !confirmed.ContainsKey(o.Id);
        }
        // Shows the latest prediction's spawns that the room has not delivered yet, and
        // ends the shown spawns it no longer makes.
        void UpdateShownSpawns()
        {
            foreach (var shown in shownSpawns.Values)
                if (shown.State == ShownState.Predicted)
                    shown.State = ShownState.Waiting;
            foreach (var pair in unmatchedSpawns)
            {
                var spawn = pair.Value;
                if (!shownSpawns.TryGetValue(pair.Key, out var shown))
                {
                    if (!catalog.TryGetValue(spawn.Prefab, out var prefab))
                        continue;
                    var entity = UnityEngine.Object.Instantiate(prefab, transform).GetComponent<NetworkEntity>();
                    entity.BeginPlaceholder();
                    shown = new ShownSpawn { Entity = entity, SpawnTick = spawn.SpawnTick, Prefab = spawn.Prefab, Motions = entity.GetComponentsInChildren<NetworkTransform>() };
                    shownSpawns.Add(pair.Key, shown);
                    Array.Copy(spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous, shown.Previous, 32);
                    Array.Copy(spawn.Values, shown.Values, 32);
                    foreach (var motion in shown.Motions)
                        motion.ShowUnbound(shown.Values, shown.Values, 1);
                    try
                    {
                        PredictedSpawned?.Invoke(entity);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e, this);
                    }
                }
                else
                {
                    Array.Copy(spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous, shown.Previous, 32);
                    Array.Copy(spawn.Values, shown.Values, 32);
                }
                shown.State = ShownState.Predicted;
            }
            slotBuffer.Clear();
            foreach (var pair in shownSpawns)
            {
                var shown = pair.Value;
                if (shown.Entity == null)
                {
                    slotBuffer.Add(pair.Key); // destroyed by the game
                    continue;
                }
                if (shown.State == ShownState.Predicted)
                    continue;
                // Not predicted any more: a misprediction while its tick is unconfirmed;
                // otherwise wait briefly for the room's spawn message.
                int age = Serial.Diff(confirmedTick, shown.SpawnTick);
                if (age < 0 || age > Math.Max(1, Client.TickRate / 2))
                {
                    slotBuffer.Add(pair.Key);
                    Discard(shown.Entity.gameObject);
                }
            }
            foreach (var slot in slotBuffer)
                shownSpawns.Remove(slot);
        }
        // Own objects that the prediction despawned are hidden until the room agrees.
        // Only renderers that were enabled are hidden, and only those are shown again.
        void UpdateHidden()
        {
            foreach (var o in Client.Objects)
            {
                bool remove = predictedRemovals.Contains(o.Id);
                o.PredictedRemoved = remove;
                var entity = remove && !hidden.ContainsKey(o) ? Find(o.Id) : null;
                if (entity == null)
                    continue; // not instantiated yet: retried at the next prediction
                var disabled = new List<Renderer>();
                foreach (var r in entity.GetComponentsInChildren<Renderer>(true))
                    if (r.enabled)
                    {
                        r.enabled = false;
                        disabled.Add(r);
                    }
                hidden.Add(o, disabled);
            }
            objectBuffer.Clear();
            foreach (var o in hidden.Keys)
                if (!o.PredictedRemoved || Client.Find(o.Id) != o)
                    objectBuffer.Add(o);
            foreach (var o in objectBuffer)
            {
                foreach (var r in hidden[o])
                    if (r != null)
                        r.enabled = true;
                hidden.Remove(o);
            }
        }
        // A simulated object this client owns, still the client's current copy (not one a
        // new snapshot replaced).
        bool IsLiveSimulatedOfMine(NetworkObject o) => o.SimulatedForMe && Client.Find(o.Id) == o;
        bool Predictable(NetworkObject o)
        {
            // Only objects in the confirmed state: one spawned after the latest clock has
            // newer values than that state and would be stepped twice.
            return IsLiveSimulatedOfMine(o) && predictions.ContainsKey(o.Prefab) && confirmed.ContainsKey(o.Id);
        }
        // Compares the earlier prediction for a tick with the new one for the same tick.
        void Correct(NetworkObject o, int[] values)
        {
            if (!o.IsPredicted)
                return; // first prediction of this object: nothing was shown
            if (o.MeasurePredictionError(values))
                corrections.Add(o);
        }

        // ---- PredictionRoom ----
        LocalSpawn Local(int objectId)
        {
            if (objectId >= 0)
                return null;
            long index = -(long)objectId - 1;
            if (index >= localSpawns.Count || localSpawns[(int)index].Removed)
                throw new InvalidOperationException("Unknown object " + objectId);
            return localSpawns[(int)index];
        }
        int[] Values(uint id)
        {
            if (predictedRemovals.Contains(id))
                throw new InvalidOperationException("Unknown object " + id);
            if (scratch.TryGetValue(id, out var values))
                return values;
            if (confirmed.TryGetValue(id, out var fields))
                return fields.Values;
            // An object outside the confirmed state: this prediction works on a copy of the
            // room's latest values.
            var o = Client.Find(id) ?? throw new InvalidOperationException("Unknown object " + id);
            values = Rent();
            o.CopyValuesTo(values);
            scratch.Add(id, values);
            return values;
        }
        // The prediction's own copy of a room object's values, made on its first write.
        int[] WorkingValues(uint id)
        {
            if (!scratch.TryGetValue(id, out var values))
            {
                values = Rent();
                Array.Copy(Values(id), values, 32);
                scratch.Add(id, values);
            }
            return values;
        }
        NetworkObject Known(int objectId)
        {
            var o = Client.Find(unchecked((uint)objectId));
            if (o == null || predictedRemovals.Contains(o.Id))
                throw new InvalidOperationException("Unknown object " + objectId);
            return o;
        }
        internal int PredictionGet(int objectId, int field) => Resolve(objectId).Get(field);
        internal void PredictionSet(int objectId, int field, int value) => Resolve(objectId).Set(field, value);
        internal int PredictionInput(int objectId, int field)
        {
            bool mine = Resolve(objectId).ReceivesMyInput;
            bool slot = field >= 0 && field < 32;
            return mine && slot ? currentFrame?.Value(field) ?? 0 : 0;
        }
        internal int PredictionSpawnOwned(int prefab, int ownerObjectId, int x, int y, int z)
        {
            if (prefab <= 0)
                throw new ArgumentOutOfRangeException(nameof(prefab));
            uint owner = Resolve(ownerObjectId).SpawnOwner(Client.ConnectionId);
            // Counted per owner and prefab like the room, so the n-th spawn has its key.
            ulong count = (ulong)owner << 32 | (uint)prefab;
            spawnCounts.TryGetValue(count, out var n);
            spawnCounts[count] = n + 1;
            var spawn = spawnPool.Count > 0 ? spawnPool.Pop() : new LocalSpawn();
            spawn.Prefab = (uint)prefab;
            spawn.SpawnTick = predictingTick;
            spawn.Owner = owner;
            spawn.Mine = owner == Client.ConnectionId;
            spawn.Key = spawn.Mine ? SpawnKey(predictingTick, n) : 0;
            spawn.Removed = false;
            Array.Clear(spawn.Values, 0, 32);
            // Fields with a [RoomField(Default)] start with it, as in the room.
            if (catalog.TryGetValue((uint)prefab, out var shown))
                shown.GetComponent<NetworkEntity>().Schema?.ApplyDefaults(spawn.Values);
            spawn.Values[0] = x;
            spawn.Values[1] = y;
            spawn.Values[2] = z;
            Array.Copy(spawn.Values, spawn.Previous, 32);
            localSpawns.Add(spawn);
            prefabListsDirty = true;
            return -localSpawns.Count;
        }
        internal int PredictionPrefabOf(int objectId) => unchecked((int)Resolve(objectId).PrefabId);
        internal int PredictionOwnerOf(int objectId) => unchecked((int)Resolve(objectId).Holder);
        // The first object of the prefab (room objects by ID, then this prediction's spawns)
        // whose holder is the connection, or 0.
        internal int PredictionFindOwned(int prefab, int connectionId)
        {
            if (connectionId <= 0)
                return 0;
            foreach (var id in PredictionObjects(prefab))
                if (Resolve(unchecked((int)id)).Holder == (uint)connectionId)
                    return unchecked((int)id);
            return 0;
        }
        // An object the prediction can address: a room object it has not despawned, or one
        // of its own spawns (negative IDs) it has not despawned. 0 is none.
        internal bool PredictionExists(int objectId)
        {
            if (objectId < 0)
            {
                long index = -(long)objectId - 1;
                return index < localSpawns.Count && !localSpawns[(int)index].Removed;
            }
            var o = objectId == 0 ? null : Client.Find(unchecked((uint)objectId));
            return o != null && !predictedRemovals.Contains(o.Id);
        }
        internal void PredictionDespawn(int objectId)
        {
            Resolve(objectId).Despawn();
            prefabListsDirty = true;
        }
        internal List<uint> PredictionObjects(int prefab)
        {
            if (prefabListsDirty)
            {
                prefabListsDirty = false;
                foreach (var list in prefabObjects.Values)
                    list.Clear();
                foreach (var o in Client.Objects)
                {
                    // Simulated objects belong to the confirmed state; later ones (such as
                    // the room's copy of a predicted spawn) would be counted twice.
                    bool removedByPrediction = predictedRemovals.Contains(o.Id);
                    bool simulatedAfterClock = o.Kind.Simulated && !confirmed.ContainsKey(o.Id);
                    if (removedByPrediction || simulatedAfterClock)
                        continue;
                    if (!prefabObjects.TryGetValue((int)o.Prefab, out var list))
                        prefabObjects.Add((int)o.Prefab, list = new List<uint>());
                    list.Add(o.Id);
                }
                foreach (var list in prefabObjects.Values)
                    list.Sort();
                // Predicted spawns follow, as their room IDs would be higher.
                for (int i = 0; i < localSpawns.Count; i++)
                {
                    if (localSpawns[i].Removed)
                        continue;
                    if (!prefabObjects.TryGetValue((int)localSpawns[i].Prefab, out var list))
                        prefabObjects.Add((int)localSpawns[i].Prefab, list = new List<uint>());
                    list.Add(unchecked((uint)-(i + 1)));
                }
            }
            if (!prefabObjects.TryGetValue(prefab, out var ids))
                prefabObjects.Add(prefab, ids = new List<uint>());
            return ids;
        }
    }
}
#endif
