#if UNITY_2021_3_OR_NEWER
using System.Collections.Generic;
namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// The built-in position and rotation fields (slots 0-5) every object has without a
    /// schema. A fixed wire contract shared with the room's RoomSync/RoomFields.
    /// </summary>
    public static class NetworkStandardFields
    {
        /// <summary>Slot bit masks of the position, rotation, and both.</summary>
        public const uint PositionMask = 7, RotationMask = 56, TransformMask = 63;
        /// <summary>Quantization steps: position in units, rotation in degrees.</summary>
        public const float PositionPrecision = .01f, RotationPrecision = 360f / 4096;
        static readonly IReadOnlyList<NetworkSchema.Field> fields = System.Array.AsReadOnly(new[]{
            new NetworkSchema.Field("PositionX",0,NetworkValueType.Float,PositionPrecision),
            new NetworkSchema.Field("PositionY",1,NetworkValueType.Float,PositionPrecision),
            new NetworkSchema.Field("PositionZ",2,NetworkValueType.Float,PositionPrecision),
            new NetworkSchema.Field("RotationX",3,NetworkValueType.Float,RotationPrecision),
            new NetworkSchema.Field("RotationY",4,NetworkValueType.Float,RotationPrecision),
            new NetworkSchema.Field("RotationZ",5,NetworkValueType.Float,RotationPrecision)
        });
        public static IReadOnlyList<NetworkSchema.Field> Fields => fields;
        /// <summary>The standard field with this name; null when none.</summary>
        public static NetworkSchema.Field Find(string name)
        {
            foreach (var field in fields)
                if (field.Name == name)
                    return field;
            return null;
        }
    }
}
#endif
