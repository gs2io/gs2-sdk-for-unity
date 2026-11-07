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
        [SerializeField, Min(0), Tooltip("Remote objects farther than this from the received position jump instead of interpolating (0 = never).")] float teleportDistance;
        [SerializeField, Min(0), Tooltip("With server timestamps, remote objects show the pose from this many seconds ago, interpolated between received updates.")] float interpolationDelay = .1f;
        [SerializeField, Min(0), Tooltip("Predicted objects fade out a correction by the room over about this many seconds (0 = jump).")] float correctionTime = .1f;
        int[] positionSlots = new int[3], rotationSlots = new int[3];
        float[] positionUnits = new float[3], rotationUnits = new float[3];
        SyncClient client;
        NetworkObject state;
        Vector3 targetPosition, targetEuler;
        // Predicted objects: the remaining display offset of corrections by the room.
        Vector3 positionError, eulerError;
        // Timestamped poses (server time) when the world receives with timestamps.
        struct Sample
        {
            public double Time;
            public Vector3 Position, Euler;
            public bool Cut; // reached by a teleport: do not blend into it
        }
        const int MaxSamples = 32;
        readonly Sample[] samples = new Sample[MaxSamples];
        int sampleCount;
        NetworkWorld world;
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
        public float Interpolation
        {
            get => interpolation; set => interpolation = Mathf.Max(0, value);
        }
        public float TeleportDistance
        {
            get => teleportDistance; set => teleportDistance = Mathf.Max(0, value);
        }
        public float InterpolationDelay
        {
            get => interpolationDelay; set => interpolationDelay = Mathf.Max(0, value);
        }
        // Time constant (seconds) over which a predicted object's correction fades.
        public float CorrectionTime
        {
            get => correctionTime; set => correctionTime = Mathf.Max(0, value);
        }
        // Remote objects jump to the latest received pose (e.g. after a respawn).
        public void SnapToNetwork()
        {
            if (state != null && !IsOwner)
                Snap();
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
            state.Predicted += OnPredicted;
            state.PredictionCorrected += OnCorrected;
            positionError = eulerError = Vector3.zero;
            world = GetComponentInParent<NetworkWorld>(true);
            sampleCount = 0;
            targetPosition = Target.position;
            targetEuler = Target.eulerAngles;
            // A confirmed Instantiate keeps the pose the owner already moved it to.
            if (GetComponentInParent<NetworkEntity>(true)?.BindingPlaceholder == true && value.Owner == session.ConnectionId)
                return;
            OnChanged(uint.MaxValue);
            Snap();
        }
        void Snap()
        {
            sampleCount = 0;
            Target.position = Merge(Target.position, targetPosition, positionAxes);
            Target.eulerAngles = Merge(Target.eulerAngles, targetEuler, rotationAxes);
        }
        void OnAuthority()
        {
            OnChanged(uint.MaxValue);
            Snap();
        }
        void Record(double time)
        {
            if (sampleCount > 0 && time < samples[sampleCount - 1].Time)
                return; // out of order: keep the newer pose
            if (sampleCount == 0 && world.HasServerTime)
            {
                // Start from the pose on screen so the first update blends in instead of jumping.
                double now = world.ServerTime - interpolationDelay;
                if (now < time)
                    samples[sampleCount++] = new Sample { Time = now, Position = Target.position, Euler = Target.eulerAngles };
            }
            var previous = sampleCount > 0 ? samples[sampleCount - 1].Position : Target.position;
            var sample = new Sample
            {
                Time = time,
                Position = targetPosition,
                Euler = targetEuler,
                Cut = teleportDistance > 0 && (Merge(previous, targetPosition, positionAxes) - previous).sqrMagnitude > teleportDistance * teleportDistance
            };
            if (sampleCount > 0 && samples[sampleCount - 1].Time == time)
            {
                sample.Cut |= samples[sampleCount - 1].Cut;
                samples[sampleCount - 1] = sample;
                return;
            }
            if (sampleCount == MaxSamples)
            {
                Array.Copy(samples, 1, samples, 0, MaxSamples - 1);
                sampleCount--;
            }
            samples[sampleCount++] = sample;
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
            if (mask != uint.MaxValue && world != null && !double.IsNaN(world.ReceivedServerTime))
                Record(world.ReceivedServerTime);
        }
        // Shows the pose at ServerTime - interpolationDelay between the two received
        // updates around it; holds the newest pose when updates stop (no extrapolation).
        bool Interpolate()
        {
            if (sampleCount == 0 || world == null || !world.HasServerTime)
                return false;
            double time = world.ServerTime - interpolationDelay;
            int next = 0;
            while (next < sampleCount && samples[next].Time <= time)
                next++;
            Vector3 position, euler;
            Quaternion rotation;
            if (next == 0)
            {
                position = samples[0].Position;
                euler = samples[0].Euler;
                rotation = Quaternion.Euler(euler);
            }
            else if (next == sampleCount)
            {
                position = samples[next - 1].Position;
                euler = samples[next - 1].Euler;
                rotation = Quaternion.Euler(euler);
                // Keep the newest sample as the base for later updates.
                if (next > 1)
                {
                    samples[0] = samples[next - 1];
                    sampleCount = 1;
                }
            }
            else
            {
                var a = samples[next - 1];
                var b = samples[next];
                float u = b.Cut ? 0 : (float)((time - a.Time) / (b.Time - a.Time));
                position = Vector3.Lerp(a.Position, b.Position, u);
                rotation = Quaternion.Slerp(Quaternion.Euler(a.Euler), Quaternion.Euler(b.Euler), u);
                euler = a.Euler;
                for (int axis = 0; axis < 3; axis++)
                    euler[axis] = Mathf.LerpAngle(a.Euler[axis], b.Euler[axis], u);
                // Drop samples that are entirely in the past.
                if (next > 1)
                {
                    Array.Copy(samples, next - 1, samples, 0, sampleCount - (next - 1));
                    sampleCount -= next - 1;
                }
            }
            Target.position = Merge(Target.position, position, positionAxes);
            if (rotationAxes == SyncAxes.All)
                Target.rotation = rotation;
            else if (rotationAxes != SyncAxes.None)
                Target.eulerAngles = Merge(Target.eulerAngles, euler, rotationAxes);
            return true;
        }
        // Predicted objects (NetworkWorld.RegisterPrediction) show the prediction between
        // its last two ticks, plus the fading offset of the latest corrections.
        void OnPredicted(float alpha)
        {
            float fade = correctionTime <= 0 ? 0 : Mathf.Exp(-Time.unscaledDeltaTime / correctionTime);
            positionError *= fade;
            eulerError *= fade;
            var position = Target.position;
            var euler = Target.eulerAngles;
            for (int axis = 0; axis < 3; axis++)
            {
                if (((int)positionAxes & (1 << axis)) != 0)
                {
                    int slot = positionSlots[axis];
                    position[axis] = Mathf.Lerp(state.PreviousPredictedValues[slot], state.PredictedValues[slot], alpha) * positionUnits[axis] + positionError[axis];
                }
                if (((int)rotationAxes & (1 << axis)) != 0)
                {
                    int slot = rotationSlots[axis];
                    euler[axis] = Mathf.LerpAngle(state.PreviousPredictedValues[slot] * rotationUnits[axis], state.PredictedValues[slot] * rotationUnits[axis], alpha) + eulerError[axis];
                }
            }
            Target.position = position;
            if (rotationAxes != SyncAxes.None)
                Target.eulerAngles = euler;
        }
        // Places an instance that is not bound to an object yet (a predicted spawn).
        // Slots and precisions come from the entity's schema, resolved once.
        NetworkSchema.Field[] unbound;
        internal void ShowUnbound(int[] previous, int[] current, float alpha)
        {
            if (unbound == null)
            {
                var schema = GetComponentInParent<NetworkEntity>(true)?.Schema;
                unbound = new NetworkSchema.Field[6];
                for (int axis = 0; axis < 3; axis++)
                {
                    if (((int)positionAxes & (1 << axis)) != 0)
                        unbound[axis] = NetworkSchema.Resolve(schema, "Position" + "XYZ"[axis]);
                    if (((int)rotationAxes & (1 << axis)) != 0)
                        unbound[3 + axis] = NetworkSchema.Resolve(schema, "Rotation" + "XYZ"[axis]);
                }
            }
            var position = Target.position;
            var euler = Target.eulerAngles;
            for (int axis = 0; axis < 3; axis++)
            {
                var field = unbound[axis];
                if (field != null)
                    position[axis] = Mathf.Lerp(previous[field.Slot], current[field.Slot], alpha) * field.Precision;
                field = unbound[3 + axis];
                if (field != null)
                    euler[axis] = Mathf.LerpAngle(previous[field.Slot] * field.Precision, current[field.Slot] * field.Precision, alpha);
            }
            Target.position = position;
            if (rotationAxes != SyncAxes.None)
                Target.eulerAngles = euler;
        }
        void OnCorrected(uint mask)
        {
            for (int axis = 0; axis < 3; axis++)
            {
                if (((int)positionAxes & (1 << axis)) != 0 && (mask & (1u << positionSlots[axis])) != 0)
                    positionError[axis] += state.PredictionError[positionSlots[axis]] * positionUnits[axis];
                if (((int)rotationAxes & (1 << axis)) != 0 && (mask & (1u << rotationSlots[axis])) != 0)
                    eulerError[axis] = Mathf.DeltaAngle(0, eulerError[axis] + state.PredictionError[rotationSlots[axis]] * rotationUnits[axis]);
            }
            // A large correction (e.g. a respawn) is a teleport, not an error to fade.
            if (teleportDistance > 0 && positionError.sqrMagnitude > teleportDistance * teleportDistance)
                positionError = eulerError = Vector3.zero;
        }
        void Update()
        {
            if (client == null || !client.CanSend || state.IsPredicted)
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
            else if (!Interpolate())
            {
                // Without timestamps: approach the latest pose at a rate per second.
                float t = interpolation <= 0 ? 1 : 1 - Mathf.Exp(-interpolation * Time.unscaledDeltaTime);
                var current = Target.position;
                var goal = Merge(current, targetPosition, positionAxes);
                if (teleportDistance > 0 && (goal - current).sqrMagnitude > teleportDistance * teleportDistance)
                    t = 1;
                Target.position = Vector3.Lerp(current, goal, t);
                if (rotationAxes == SyncAxes.All)
                {
                    // Interpolate the orientation as a whole; per-axis Euler blending takes
                    // detours when several axes change at once.
                    Target.rotation = Quaternion.Slerp(Target.rotation, Quaternion.Euler(targetEuler), t);
                }
                else if (rotationAxes != SyncAxes.None)
                {
                    var euler = Target.eulerAngles;
                    for (int axis = 0; axis < 3; axis++)
                        if (((int)rotationAxes & (1 << axis)) != 0)
                            euler[axis] = Mathf.LerpAngle(euler[axis], targetEuler[axis], t);
                    Target.eulerAngles = euler;
                }
            }
        }
        // Angles just below 360 round up to one full turn; send them as 0 so the
        // same orientation never alternates between two values.
        internal static int QuantizeAngle(float degrees, float unit)
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
                state.Predicted -= OnPredicted;
                state.PredictionCorrected -= OnCorrected;
            }
            state = null;
            client = null;
            world = null;
            sampleCount = 0;
        }
        void OnDestroy()
        {
            Unbind();
        }
    }
}
#endif
