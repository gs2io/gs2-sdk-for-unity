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
        // Ticks of inputs kept for re-simulation; a larger gap shows the room's state.
        public const int PredictionWindow = 64;
        sealed class InputFrame
        {
            public uint Tick, Mask;
            public readonly int[] Values = new int[32];
        }
        readonly Dictionary<uint, Action<int, int>> predictions = new Dictionary<uint, Action<int, int>>();
        readonly InputFrame[] inputFrames = new InputFrame[PredictionWindow];
        // Room state of simulated objects at confirmedTick, copied when its clock arrived:
        // later updates belong to later ticks.
        readonly Dictionary<uint, int[]> confirmed = new Dictionary<uint, int[]>();
        readonly Dictionary<uint, int[]> scratch = new Dictionary<uint, int[]>();
        readonly Stack<int[]> valuePool = new Stack<int[]>();
        readonly Dictionary<int, List<uint>> prefabObjects = new Dictionary<int, List<uint>>();
        readonly List<NetworkObject> predictedObjects = new List<NetworkObject>();
        readonly List<uint> idBuffer = new List<uint>();
        uint confirmedTick, predictedTick, predictingTick;
        bool hasConfirmed, predictionDirty, predictionFailed;

        // Registers the prediction of a prefab's simulated objects: step(objectId, tick)
        // as generated from the [Predict] method. Generated Register methods call this.
        public void RegisterPrediction(uint prefab, Action<int, int> step)
        {
            predictions[prefab] = step ?? throw new ArgumentNullException(nameof(step));
            predictionDirty = true;
            predictionFailed = false;
        }
        // Stops predicting a prefab's objects; they are shown from the room's state again.
        public void UnregisterPrediction(uint prefab)
        {
            predictions.Remove(prefab);
            predictionDirty = true;
        }
        // The tick the room state used for prediction belongs to (the latest clock).
        public uint ConfirmedTick => confirmedTick;

        void ResetPrediction()
        {
            foreach (var o in predictedObjects)
                o.IsPredicted = false;
            predictedObjects.Clear();
            foreach (var spawn in localSpawns)
                spawnPool.Push(spawn);
            localSpawns.Clear();
            spawnsByKey.Clear();
            predictedRemovals.Clear();
            corrections.Clear();
            foreach (var values in scratch.Values)
                valuePool.Push(values);
            scratch.Clear();
            foreach (var values in confirmed.Values)
                valuePool.Push(values);
            confirmed.Clear();
            Array.Clear(inputFrames, 0, inputFrames.Length);
            hasConfirmed = predictionFailed = false;
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
            frame.Mask = inputMask;
            Array.Copy(inputValues, frame.Values, 32);
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
        void Confirm(uint tick)
        {
            if (hasConfirmed && unchecked((int)(tick - confirmedTick)) <= 0)
                return; // an older clock overtaken by a newer one
            hasConfirmed = true;
            confirmedTick = tick;
            predictionDirty = true;
            predictionFailed = false; // retry with the room's new state
            idBuffer.Clear();
            foreach (var id in confirmed.Keys)
                if (Client.Find(id) == null)
                    idBuffer.Add(id);
            foreach (var id in idBuffer)
            {
                valuePool.Push(confirmed[id]);
                confirmed.Remove(id);
            }
            foreach (var o in Client.Objects)
            {
                if (o.Mode != ObjectMode.Simulated)
                    continue;
                if (!confirmed.TryGetValue(o.Id, out var values))
                    confirmed.Add(o.Id, values = Rent());
                Array.Copy(o.Values, values, 32);
            }
        }
        int[] Rent()
        {
            return valuePool.Count > 0 ? valuePool.Pop() : new int[32];
        }

        // ---- Predicted spawns ----
        // An object that a prediction spawned with SpawnOwned. Local IDs are negative
        // (-1 for the first spawn of a run); keys match the room's Record.Request.
        sealed class LocalSpawn
        {
            public uint Key, Prefab, SpawnTick;
            public bool Mine, Removed;
            public readonly int[] Values = new int[32], Previous = new int[32];
        }
        // The instance shown for a predicted spawn until the room's object takes it over.
        sealed class SpawnInstance
        {
            public NetworkEntity Entity;
            public NetworkTransform[] Motions;
            public uint SpawnTick, Prefab;
            public bool Seen;
            public readonly int[] Values = new int[32], Previous = new int[32];
        }
        readonly List<LocalSpawn> localSpawns = new List<LocalSpawn>();
        readonly Stack<LocalSpawn> spawnPool = new Stack<LocalSpawn>();
        readonly Dictionary<ulong, uint> spawnCounts = new Dictionary<ulong, uint>();
        readonly HashSet<uint> predictedRemovals = new HashSet<uint>();
        readonly Dictionary<uint, SpawnInstance> spawnInstances = new Dictionary<uint, SpawnInstance>();
        readonly Dictionary<uint, LocalSpawn> spawnsByKey = new Dictionary<uint, LocalSpawn>();
        readonly List<uint> keyBuffer = new List<uint>();
        // Renderers this world disabled on objects the prediction despawned.
        readonly Dictionary<NetworkObject, List<Renderer>> hidden = new Dictionary<NetworkObject, List<Renderer>>();
        readonly List<NetworkObject> objectBuffer = new List<NetworkObject>();
        readonly List<NetworkObject> corrections = new List<NetworkObject>();
        InputFrame currentFrame;
        bool prefabListsDirty;
        // Raised when a prediction shows a spawned object before the room confirms it.
        // The instance is a placeholder (IsMine) that the room's object takes over; it
        // is destroyed if the room does not spawn it.
        public event Action<NetworkEntity> PredictedSpawned;

        static uint SpawnKey(uint tick, uint n)
        {
            return n >= 32 ? 0 : 1u << 31 | (tick & ((1u << 26) - 1)) << 5 | n;
        }
        void ResetSpawns()
        {
            foreach (var pair in spawnInstances)
            {
                placeholders.Remove(pair.Key);
                if (pair.Value.Entity != null && pair.Value.Entity.Object == null)
                    Discard(pair.Value.Entity.gameObject);
            }
            spawnInstances.Clear();
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
            foreach (var instance in spawnInstances.Values)
            {
                if (instance.Entity == null || instance.Entity.Object != null)
                    continue;
                // An instance no longer predicted waits, frozen, for the room's object.
                float shown = instance.Seen ? alpha : 1;
                foreach (var motion in instance.Motions)
                    if (motion != null)
                        motion.ShowUnbound(instance.Previous, instance.Values, shown);
            }
        }
        void Predict()
        {
            // IsPredicted stays set on objects predicted before: Correct compares with them.
            bool active = predictions.Count != 0 && clockStarted && hasConfirmed;
            foreach (var o in predictedObjects)
                if (!active || !Predictable(o) && !Unconfirmed(o))
                    o.IsPredicted = false;
            predictedObjects.Clear();
            spawnsByKey.Clear();
            foreach (var spawn in localSpawns)
                spawnPool.Push(spawn);
            localSpawns.Clear();
            predictedRemovals.Clear();
            if (!active)
            {
                UpdateSpawnInstances();
                UpdateHidden();
                return;
            }
            foreach (var o in Client.Objects)
                if (Predictable(o))
                    predictedObjects.Add(o);
            predictedObjects.Sort((a, b) => a.Id.CompareTo(b.Id));
            int span = unchecked((int)(InputTick - confirmedTick));
            uint oldTick = predictedTick;
            bool compare = predictedTick != 0 && unchecked((int)(oldTick - confirmedTick)) > 0 && unchecked((int)(InputTick - oldTick)) >= 0;
            if (span <= 0 || span > PredictionWindow || predictionFailed)
            {
                // Nothing to predict (or too far ahead): show the room's state.
                foreach (var o in predictedObjects)
                {
                    var values = confirmed.TryGetValue(o.Id, out var v) ? v : o.Values;
                    Array.Copy(values, o.PredictedValues, 32);
                    Array.Copy(values, o.PreviousPredictedValues, 32);
                    o.IsPredicted = true;
                }
                predictedTick = 0;
                UpdateSpawnInstances();
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
                for (uint tick = confirmedTick + 1; unchecked((int)(InputTick - tick)) >= 0; tick++)
                {
                    if (tick == InputTick)
                    {
                        foreach (var o in predictedObjects)
                            if (!predictedRemovals.Contains(o.Id))
                                Array.Copy(Values(o.Id), o.PreviousPredictedValues, 32);
                        foreach (var spawn in localSpawns)
                            Array.Copy(spawn.Values, spawn.Previous, 32);
                    }
                    predictingTick = tick;
                    currentFrame = InputAt(tick);
                    spawnCounts.Clear();
                    // The room's objects first, then predicted spawns (which have higher
                    // IDs in the room), including the ones spawned in this tick.
                    foreach (var o in predictedObjects)
                        if (!predictedRemovals.Contains(o.Id))
                            predictions[o.Prefab](unchecked((int)o.Id), unchecked((int)tick));
                    for (int i = 0; i < localSpawns.Count; i++)
                        if (!localSpawns[i].Removed && predictions.TryGetValue(localSpawns[i].Prefab, out var step))
                            step(-(i + 1), unchecked((int)tick));
                    if (compare && tick == oldTick)
                        foreach (var o in predictedObjects)
                            if (!predictedRemovals.Contains(o.Id))
                                Correct(o, Values(o.Id));
                }
            }
            catch (Exception e)
            {
                // The same method failing in the room would fail the room's Tick; stop
                // predicting rather than show a state the room cannot reach.
                predictionFailed = true;
                Debug.LogException(e, this);
                foreach (var o in predictedObjects)
                    o.IsPredicted = false;
                predictedObjects.Clear();
                foreach (var spawn in localSpawns)
                    spawnPool.Push(spawn);
                localSpawns.Clear();
                corrections.Clear();
                predictedRemovals.Clear();
                UpdateSpawnInstances();
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
                    o.NotifyCorrected(o.PredictionErrorMask);
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
                    Array.Copy(Values(o.Id), o.PredictedValues, 32);
                o.IsPredicted = true;
            }
            predictedTick = InputTick;
            // The room's objects that a prediction spawned, before their spawn tick is
            // confirmed, show that prediction (they took over its instance).
            spawnsByKey.Clear();
            foreach (var spawn in localSpawns)
                if (spawn.Mine && !spawn.Removed && spawn.Key != 0)
                    spawnsByKey[spawn.Key] = spawn;
            foreach (var o in Client.Objects)
            {
                if (!Unconfirmed(o) || !spawnsByKey.TryGetValue(o.Request, out var spawn) || spawn.Prefab != o.Prefab)
                    continue;
                Array.Copy(spawn.Values, o.PredictedValues, 32);
                Array.Copy(spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous, o.PreviousPredictedValues, 32);
                o.IsPredicted = true;
                predictedObjects.Add(o);
                spawnsByKey.Remove(o.Request);
            }
            UpdateSpawnInstances();
            UpdateHidden();
        }
        // Own simulated objects spawned by a prediction whose spawn tick the room has not
        // confirmed yet.
        bool Unconfirmed(NetworkObject o)
        {
            return o.Mode == ObjectMode.Simulated && o.Owner == Client.ConnectionId && (o.Request & (1u << 31)) != 0 &&
                !confirmed.ContainsKey(o.Id) && Client.Find(o.Id) == o;
        }
        // Shows predicted spawns that the room has not delivered yet, and removes those
        // the prediction no longer makes.
        void UpdateSpawnInstances()
        {
            foreach (var instance in spawnInstances.Values)
                instance.Seen = false;
            foreach (var spawn in spawnsByKey.Values)
            {
                if (!spawnInstances.TryGetValue(spawn.Key, out var instance))
                {
                    if (!catalog.TryGetValue(spawn.Prefab, out var prefab))
                        continue;
                    var entity = UnityEngine.Object.Instantiate(prefab, transform).GetComponent<NetworkEntity>();
                    entity.BeginPlaceholder();
                    instance = new SpawnInstance { Entity = entity, SpawnTick = spawn.SpawnTick, Prefab = spawn.Prefab, Motions = entity.GetComponentsInChildren<NetworkTransform>() };
                    spawnInstances.Add(spawn.Key, instance);
                    placeholders[spawn.Key] = entity;
                    Array.Copy(spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous, instance.Previous, 32);
                    Array.Copy(spawn.Values, instance.Values, 32);
                    foreach (var motion in instance.Motions)
                        motion.ShowUnbound(instance.Values, instance.Values, 1);
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
                    Array.Copy(spawn.SpawnTick == InputTick ? spawn.Values : spawn.Previous, instance.Previous, 32);
                    Array.Copy(spawn.Values, instance.Values, 32);
                }
                instance.Seen = true;
            }
            keyBuffer.Clear();
            foreach (var pair in spawnInstances)
            {
                var instance = pair.Value;
                if (instance.Entity == null || instance.Entity.Object != null)
                {
                    keyBuffer.Add(pair.Key); // destroyed, or taken over by the room's object
                    continue;
                }
                if (instance.Seen)
                    continue;
                // Not predicted any more: a misprediction when its tick is still
                // unconfirmed; otherwise wait briefly for the room's spawn message.
                int age = unchecked((int)(confirmedTick - instance.SpawnTick));
                if (age < 0 || age > Math.Max(1, Client.TickRate / 2))
                {
                    keyBuffer.Add(pair.Key);
                    Discard(instance.Entity.gameObject);
                }
            }
            foreach (var key in keyBuffer)
            {
                spawnInstances.Remove(key);
                if (placeholders.TryGetValue(key, out var entity) && (entity == null || entity.Object == null))
                    placeholders.Remove(key);
            }
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
        // The prefab of a predicted spawn's placeholder (0 when there is none).
        uint PlaceholderPrefab(uint key)
        {
            return spawnInstances.TryGetValue(key, out var instance) ? instance.Prefab : 0;
        }
        bool Predictable(NetworkObject o)
        {
            // Only objects in the confirmed state: one spawned after the latest clock has
            // newer values than that state and would be stepped twice.
            return o.Mode == ObjectMode.Simulated && o.Owner == Client.ConnectionId && predictions.ContainsKey(o.Prefab) &&
                confirmed.ContainsKey(o.Id) && Client.Find(o.Id) == o;
        }
        // Compares the earlier prediction for a tick with the new one for the same tick.
        void Correct(NetworkObject o, int[] values)
        {
            if (!o.IsPredicted)
                return; // first prediction of this object: nothing was shown
            uint mask = 0;
            for (int f = 0; f < 32; f++)
            {
                int error = unchecked(o.PredictedValues[f] - values[f]);
                o.PredictionError[f] = error;
                if (error != 0)
                    mask |= 1u << f;
            }
            o.PredictionErrorMask = mask;
            if (mask != 0)
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
            if (scratch.TryGetValue(id, out var values) || confirmed.TryGetValue(id, out values))
                return values;
            var o = Client.Find(id) ?? throw new InvalidOperationException("Unknown object " + id);
            return o.Values;
        }
        static void Field(int field)
        {
            if (field < 0 || field >= 32)
                throw new ArgumentOutOfRangeException(nameof(field));
        }
        static uint Field(NetworkObject o, int field)
        {
            if (field < 0 || field >= 32 || (o.Fields & (1u << field)) == 0)
                throw new ArgumentOutOfRangeException(nameof(field), "Field " + field + " is not synchronized for prefab " + o.Prefab);
            return 1u << field;
        }
        NetworkObject Known(int objectId)
        {
            var o = Client.Find(unchecked((uint)objectId));
            if (o == null || predictedRemovals.Contains(o.Id))
                throw new InvalidOperationException("Unknown object " + objectId);
            return o;
        }
        internal int PredictionGet(int objectId, int field)
        {
            var local = Local(objectId);
            if (local != null)
            {
                Field(field);
                return local.Values[field];
            }
            Field(Known(objectId), field);
            return Values(unchecked((uint)objectId))[field];
        }
        internal void PredictionSet(int objectId, int field, int value)
        {
            var local = Local(objectId);
            if (local != null)
            {
                Field(field);
                local.Values[field] = value;
                return;
            }
            Field(Known(objectId), field);
            uint id = unchecked((uint)objectId);
            if (!scratch.TryGetValue(id, out var values))
            {
                values = Rent();
                Array.Copy(Values(id), values, 32);
                scratch.Add(id, values);
            }
            values[field] = value;
        }
        internal int PredictionInput(int objectId, int field)
        {
            var local = Local(objectId);
            bool mine;
            if (local != null)
                mine = local.Mine;
            else
            {
                var o = Known(objectId);
                if (o.Mode != ObjectMode.Simulated)
                    throw new InvalidOperationException("Input requires a simulated object");
                mine = o.Owner == Client.ConnectionId;
            }
            if (field < 0 || field >= 32 || !mine)
                return 0;
            var frame = currentFrame;
            return frame != null && (frame.Mask & (1u << field)) != 0 ? frame.Values[field] : 0;
        }
        internal int PredictionSpawnOwned(int prefab, int ownerObjectId, int x, int y, int z)
        {
            if (prefab <= 0)
                throw new ArgumentOutOfRangeException(nameof(prefab));
            uint owner;
            var localOwner = Local(ownerObjectId);
            if (localOwner != null)
                owner = localOwner.Mine ? Client.ConnectionId : 0;
            else
            {
                var o = Known(ownerObjectId);
                if (o.Mode != ObjectMode.Player && o.Mode != ObjectMode.Simulated || o.Owner == 0)
                    throw new InvalidOperationException("SpawnOwned requires an object owned by a member");
                owner = o.Owner;
            }
            // Counted per owner and prefab like the room, so the n-th spawn has its key.
            ulong count = (ulong)owner << 32 | (uint)prefab;
            spawnCounts.TryGetValue(count, out var n);
            spawnCounts[count] = n + 1;
            var spawn = spawnPool.Count > 0 ? spawnPool.Pop() : new LocalSpawn();
            spawn.Prefab = (uint)prefab;
            spawn.SpawnTick = predictingTick;
            spawn.Mine = owner == Client.ConnectionId;
            spawn.Key = spawn.Mine ? SpawnKey(predictingTick, n) : 0;
            spawn.Removed = false;
            Array.Clear(spawn.Values, 0, 32);
            spawn.Values[0] = x;
            spawn.Values[1] = y;
            spawn.Values[2] = z;
            Array.Copy(spawn.Values, spawn.Previous, 32);
            localSpawns.Add(spawn);
            prefabListsDirty = true;
            return -localSpawns.Count;
        }
        internal void PredictionDespawn(int objectId)
        {
            var local = Local(objectId);
            if (local != null)
                local.Removed = true;
            else
            {
                var o = Known(objectId);
                if (o.Mode == ObjectMode.Player)
                    throw new InvalidOperationException("Player objects cannot be despawned by the rules");
                predictedRemovals.Add(o.Id);
            }
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
                    if (predictedRemovals.Contains(o.Id) || o.Mode == ObjectMode.Simulated && !confirmed.ContainsKey(o.Id))
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
