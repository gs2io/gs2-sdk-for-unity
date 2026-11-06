#if UNITY_2021_3_OR_NEWER
using System.Collections.Generic;
namespace Gs2.Realtime.Sync
{
    // Fixed wire contract, shared with authoring RoomSync/RoomFields. No per-object asset needed.
    public static class NetworkStandardFields
    {
        public const uint PositionMask = 7, RotationMask = 56, TransformMask = 63;
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
