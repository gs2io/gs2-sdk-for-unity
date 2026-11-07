#if UNITY_2021_3_OR_NEWER
using System;
namespace Gs2.Realtime.Sync
{
    // The room API seen by prediction code generated from [Predict] rule methods
    // (unity/<RuleClass>Prediction.cs). Valid only while NetworkWorld predicts: reads
    // see the room's latest confirmed state plus this prediction's own writes, and
    // writes never leave the client.
    public static class PredictionRoom
    {
        internal static NetworkWorld Current;
        static NetworkWorld World => Current ?? throw new InvalidOperationException("PredictionRoom is only available while NetworkWorld predicts");

        public static int Get(int objectId, int field)
        {
            return World.PredictionGet(objectId, field);
        }
        public static void Set(int objectId, int field, int value)
        {
            World.PredictionSet(objectId, field, value);
        }
        // This client's input for the predicted tick on its own objects; other members'
        // inputs are unknown to clients and read as 0.
        public static int Input(int objectId, int field)
        {
            return World.PredictionInput(objectId, field);
        }
        // A predicted spawn: a local object (negative ID) shown at once when this client
        // owns it; the room's object with the same spawn key takes over its instance.
        public static int SpawnOwned(int prefab, int ownerObjectId, int x, int y, int z)
        {
            return World.PredictionSpawnOwned(prefab, ownerObjectId, x, y, z);
        }
        public static void Despawn(int objectId)
        {
            World.PredictionDespawn(objectId);
        }
        // A prefab's objects in ascending ID order, as known to this client, followed by
        // the objects spawned by this prediction.
        public static int ObjectCount(int prefab)
        {
            return World.PredictionObjects(prefab).Count;
        }
        public static int ObjectAt(int prefab, int index)
        {
            var ids = World.PredictionObjects(prefab);
            if (index < 0 || index >= ids.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return unchecked((int)ids[index]);
        }
    }
}
#endif
