#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    [Flags]
    public enum SyncAxes
    {
        None = 0, X = 1, Y = 2, Z = 4, All = 7
    }
    [DisallowMultipleComponent]
    public sealed class NetworkTransform : MonoBehaviour, INetworkBinding
    {
        [SerializeField] Transform target;
        [SerializeField] SyncAxes positionAxes = SyncAxes.All, rotationAxes = SyncAxes.All;
        [SerializeField, Min(0)] float interpolation = 20;
        int[] positionSlots = new int[3], rotationSlots = new int[3];
        float[] positionUnits = new float[3], rotationUnits = new float[3];
        SyncClient client;
        NetworkObject state;
        Vector3 targetPosition, targetEuler;
        Transform Target => target == null ? transform : target;
        public NetworkObject Object => state;
        public SyncClient Session => client;
        public bool IsOwner => client != null && state != null && client.CanWrite(state.Id);
        public SyncAxes PositionAxes
        {
            get => positionAxes; set => positionAxes = value;
        }
        public SyncAxes RotationAxes
        {
            get => rotationAxes; set => rotationAxes = value;
        }
        uint Resolve(SyncAxes axes, string prefix, int[] slots, float[] units, NetworkSchema schema)
        {
            if (((int)axes & ~7) != 0)
                throw new ArgumentException("Invalid axes");
            uint mask = 0;
            for (int axis = 0; axis < 3; axis++)
            {
                if (((int)axes & (1 << axis)) == 0)
                    continue;
                var name = prefix + "XYZ"[axis];
                var field = NetworkSchema.Resolve(schema, name);
                if (field == null || field.Type != NetworkValueType.Float)
                    throw new ArgumentException("Transform requires a float field enabled in the schema: " + name);
                slots[axis] = field.Slot;
                units[axis] = field.Precision;
                mask |= 1u << slots[axis];
            }
            return mask;
        }
        public void Bind(SyncClient session, NetworkObject value)
        {
            var schema = GetComponentInParent<NetworkEntity>(true)?.Schema;
            if (schema != null && !schema.Describes(value))
                throw new ArgumentException("Transform schema does not match the object");
            uint positionMask = Resolve(positionAxes, "Position", positionSlots, positionUnits, schema);
            uint rotationMask = Resolve(rotationAxes, "Rotation", rotationSlots, rotationUnits, schema);
            if ((positionMask & rotationMask) != 0 || ((positionMask | rotationMask) & ~value.Fields) != 0)
                throw new ArgumentException("Transform fields overlap or are missing from server schema");
            Unbind();
            client = session;
            state = value;
            state.Changed += OnChanged;
            state.AuthorityChanged += OnAuthority;
            targetPosition = Target.position;
            targetEuler = Target.eulerAngles;
            OnChanged(uint.MaxValue);
            Snap();
        }
        void Snap()
        {
            Target.position = Merge(Target.position, targetPosition, positionAxes);
            Target.eulerAngles = Merge(Target.eulerAngles, targetEuler, rotationAxes);
        }
        void OnAuthority()
        {
            OnChanged(uint.MaxValue);
            Snap();
        }
        static Vector3 Merge(Vector3 original, Vector3 update, SyncAxes axes)
        {
            if ((axes & SyncAxes.X) != 0)
                original.x = update.x;
            if ((axes & SyncAxes.Y) != 0)
                original.y = update.y;
            if ((axes & SyncAxes.Z) != 0)
                original.z = update.z;
            return original;
        }
        void OnChanged(uint mask)
        {
            for (int axis = 0; axis < 3; axis++)
            {
                if (((int)positionAxes & (1 << axis)) != 0 && (mask & (1u << positionSlots[axis])) != 0)
                    targetPosition[axis] = state.Values[positionSlots[axis]] * positionUnits[axis];
                if (((int)rotationAxes & (1 << axis)) != 0 && (mask & (1u << rotationSlots[axis])) != 0)
                    targetEuler[axis] = state.Values[rotationSlots[axis]] * rotationUnits[axis];
            }
        }
        void Update()
        {
            if (client == null || !client.CanSend)
                return;
            if (IsOwner)
            {
                var p = Target.position;
                var r = Target.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                {
                    if (((int)positionAxes & (1 << axis)) != 0)
                        client.Set(state.Id, positionSlots[axis], SyncCodec.Quantize(p[axis], positionUnits[axis]));
                    if (((int)rotationAxes & (1 << axis)) != 0)
                        client.Set(state.Id, rotationSlots[axis], QuantizeAngle(r[axis], rotationUnits[axis]));
                }
            }
            else
            {
                float t = interpolation <= 0 ? 1 : 1 - Mathf.Exp(-interpolation * Time.unscaledDeltaTime);
                Target.position = Merge(Target.position, Vector3.Lerp(Target.position, targetPosition, t), positionAxes);
                var euler = Target.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                    if (((int)rotationAxes & (1 << axis)) != 0)
                        euler[axis] = Mathf.LerpAngle(euler[axis], targetEuler[axis], t);
                Target.eulerAngles = euler;
            }
        }
        // Angles just below 360 round up to one full turn; send them as 0 so the
        // same orientation never alternates between two values.
        static int QuantizeAngle(float degrees, float unit)
        {
            int value = SyncCodec.Quantize(Mathf.Repeat(degrees, 360), unit);
            int turn = SyncCodec.Quantize(360, unit);
            return value >= turn ? value - turn : value;
        }
        public void Unbind()
        {
            if (state != null)
            {
                state.Changed -= OnChanged;
                state.AuthorityChanged -= OnAuthority;
            }
            state = null;
            client = null;
        }
        void OnDestroy()
        {
            Unbind();
        }
    }
}
#endif
