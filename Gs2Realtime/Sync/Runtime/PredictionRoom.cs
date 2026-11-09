#if UNITY_2021_3_OR_NEWER
using System;
namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// The room API seen by prediction code generated from [Predict] rule methods
    /// (unity/&lt;RuleClass&gt;Prediction.cs). Valid only while NetworkWorld predicts: reads
    /// see the room's latest confirmed state plus this prediction's own writes, and
    /// writes never leave the client. Calls outside a prediction throw InvalidOperationException.
    /// </summary>
    public static class PredictionRoom
    {
        internal static NetworkWorld Current;
        static NetworkWorld World => Current ?? throw new InvalidOperationException("PredictionRoom is only available while NetworkWorld predicts");

        /// <summary>The object's field value in this prediction.</summary>
        public static int Get(int objectId, int field)
        {
            return World.PredictionGet(objectId, field);
        }
        /// <summary>Sets the object's field for the rest of this prediction (never sent).</summary>
        public static void Set(int objectId, int field, int value)
        {
            World.PredictionSet(objectId, field, value);
        }
        /// <summary>
        /// This client's input for the predicted tick on its own objects; other members'
        /// inputs are unknown to clients and read as 0.
        /// </summary>
        public static int Input(int objectId, int field)
        {
            return World.PredictionInput(objectId, field);
        }
        /// <summary>
        /// The input clamped to min..max (Room.Input with a range, or an [InputRange] slot).
        /// </summary>
        public static int Input(int objectId, int field, int min, int max)
        {
            return RoomMath.Clamp(Input(objectId, field), min, max);
        }
        /// <summary>
        /// A predicted spawn: returns a local object ID (negative), shown at once when this
        /// client owns it; the room's object with the same spawn key takes over its instance.
        /// </summary>
        public static int SpawnOwned(int prefab, int ownerObjectId, int x, int y, int z)
        {
            return World.PredictionSpawnOwned(prefab, ownerObjectId, x, y, z);
        }
        /// <summary>Removes the object for the rest of this prediction.</summary>
        public static void Despawn(int objectId)
        {
            World.PredictionDespawn(objectId);
        }
        /// <summary>
        /// The object's prefab ID (Room.PrefabOf, and Room.Is&lt;T&gt; in predicted code).
        /// </summary>
        public static int PrefabOf(int objectId)
        {
            return World.PredictionPrefabOf(objectId);
        }
        /// <summary>The object's owner or lease holder (Room.OwnerOf; 0: none).</summary>
        public static int OwnerOf(int objectId)
        {
            return World.PredictionOwnerOf(objectId);
        }
        /// <summary>
        /// The prefab's first object owned (or leased) by the connection (Room.FindOwned), or 0.
        /// </summary>
        public static int FindOwned(int prefab, int connectionId)
        {
            return World.PredictionFindOwned(prefab, connectionId);
        }
        /// <summary>
        /// Whether the ID names an object (Room.Exists): false for 0 and removed objects.
        /// </summary>
        public static bool Exists(int objectId)
        {
            return World.PredictionExists(objectId);
        }
        /// <summary>
        /// Number of the prefab's objects: those known to this client in ascending ID order,
        /// followed by the objects spawned by this prediction.
        /// </summary>
        public static int ObjectCount(int prefab)
        {
            return World.PredictionObjects(prefab).Count;
        }
        /// <summary>
        /// The prefab's object at index (0..ObjectCount - 1), in ObjectCount's order.
        /// </summary>
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
