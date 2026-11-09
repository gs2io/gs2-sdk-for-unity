#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    /// <summary>Which axes of a position or rotation are synchronized.</summary>
    [Flags]
    public enum SyncAxes
    {
        None = 0, X = 1, Y = 2, Z = 4, All = 7
    }
    public static class SyncAxesExtensions
    {
        /// <summary>Whether axes includes axis (0: x, 1: y, 2: z).</summary>
        public static bool Includes(this SyncAxes axes, int axis) => ((int)axes & (1 << axis)) != 0;
    }
    /// <summary>
    /// Synchronizes a Transform's position and rotation through the entity's float fields
    /// PositionX/Y/Z and RotationX/Y/Z (Euler degrees). The owner writes its pose each frame;
    /// other clients interpolate the received poses, and predicted objects show the prediction.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class NetworkTransform : MonoBehaviour, INetworkBinding
    {
        [SerializeField] Transform target;
        [SerializeField] SyncAxes positionAxes = SyncAxes.All, rotationAxes = SyncAxes.All;
        bool PositionChanged(uint mask, int axis) => positionAxes.Includes(axis) && FieldMask.Contains(mask, positionSlots[axis]);
        bool RotationChanged(uint mask, int axis) => rotationAxes.Includes(axis) && FieldMask.Contains(mask, rotationSlots[axis]);
        [SerializeField, Min(0)] float interpolation = 20;
        [SerializeField, Min(0), Tooltip("Remote objects farther than this from the received position jump instead of interpolating (0 = never).")] float teleportDistance;
        [SerializeField, Min(0), Tooltip("With server timestamps, remote objects show the pose from this many seconds ago, interpolated between received updates.")] float interpolationDelay = .1f;
        [SerializeField, Min(0), Tooltip("Predicted objects fade out a correction by the room over about this many seconds (0 = jump).")] float correctionTime = .1f;
        int[] positionSlots = new int[3], rotationSlots = new int[3];
        float[] positionUnits = new float[3], rotationUnits = new float[3];
        SyncClient client;
        NetworkObject state;
        // The room's latest pose of the object.
        Vector3 targetPosition, targetEuler;
        NetworkWorld world;
        // The object's role on this client: it writes the pose (owner), shows another member's
        // or the room's (remote), or shows its own prediction (predicted).
        OwnerMotion owner;
        RemoteMotion remote;
        PredictedMotion predicted;
        Motion Role => state.IsPredicted ? predicted : client.Ready && state.WritableByMe ? owner : (Motion)remote;
        Transform Target => target == null ? transform : target;
        /// <summary>The bound room object, or null while unbound.</summary>
        public NetworkObject Object => state;
        /// <summary>The client this transform is bound through, or null while unbound.</summary>
        public SyncClient Session => client;
        /// <summary>True when this client writes the object's pose.</summary>
        public bool IsOwner => client != null && state != null && client.Ready && state.WritableByMe;
        /// <summary>Synchronized position axes. Set before binding: fields are resolved at Bind.</summary>
        public SyncAxes PositionAxes
        {
            get => positionAxes; set => positionAxes = value;
        }
        /// <summary>Synchronized rotation axes. Set before binding: fields are resolved at Bind.</summary>
        public SyncAxes RotationAxes
        {
            get => rotationAxes; set => rotationAxes = value;
        }
        /// <summary>
        /// Without server timestamps, how fast remote objects approach the received pose, per
        /// second (0 = jump).
        /// </summary>
        public float Interpolation
        {
            get => interpolation; set => interpolation = Mathf.Max(0, value);
        }
        /// <summary>
        /// Remote objects farther than this from the received position jump instead of
        /// interpolating (0 = never).
        /// </summary>
        public float TeleportDistance
        {
            get => teleportDistance; set => teleportDistance = Mathf.Max(0, value);
        }
        /// <summary>
        /// With server timestamps, remote objects show the pose from this many seconds ago,
        /// interpolated between received updates.
        /// </summary>
        public float InterpolationDelay
        {
            get => interpolationDelay; set => interpolationDelay = Mathf.Max(0, value);
        }
        /// <summary>
        /// Time constant (seconds) over which a predicted object's correction by the room fades
        /// out (0 = jump).
        /// </summary>
        public float CorrectionTime
        {
            get => correctionTime; set => correctionTime = Mathf.Max(0, value);
        }
        /// <summary>
        /// Moves a remote object straight to the latest received pose (e.g. after a respawn).
        /// Does nothing for the owner or while unbound.
        /// </summary>
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
                if (!axes.Includes(axis))
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
        /// <summary>Called by the NetworkEntity when it binds to a room object.</summary>
        /// <exception cref="ArgumentException">
        /// The entity's schema does not match the object, or the synchronized axes lack float
        /// fields or overlap.
        /// </exception>
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
            world = GetComponentInParent<NetworkWorld>(true);
            owner ??= new OwnerMotion(this);
            remote ??= new RemoteMotion(this);
            predicted ??= new PredictedMotion(this);
            remote.Reset();
            predicted.Reset();
            targetPosition = Target.position;
            targetEuler = Target.eulerAngles;
            // A confirmed Instantiate keeps the pose the owner already moved it to.
            if (GetComponentInParent<NetworkEntity>(true)?.BindingPlaceholder == true && value.IsMine)
                return;
            OnChanged(uint.MaxValue);
            Snap();
        }
        void Snap()
        {
            remote.Reset();
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
                if (PositionChanged(mask, axis))
                    targetPosition[axis] = state.Values[positionSlots[axis]] * positionUnits[axis];
                if (RotationChanged(mask, axis))
                    targetEuler[axis] = state.Values[rotationSlots[axis]] * rotationUnits[axis];
            }
            if (mask != uint.MaxValue && world != null && !double.IsNaN(world.ReceivedServerTime))
                remote.Record(world.ReceivedServerTime);
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
                    if (positionAxes.Includes(axis))
                        unbound[axis] = NetworkSchema.Resolve(schema, "Position" + "XYZ"[axis]);
                    if (rotationAxes.Includes(axis))
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
        void Update()
        {
            if (client == null || !client.Ready)
                return;
            Role.Update();
        }
        void OnPredicted(float alpha) => predicted.Show(alpha);
        void OnCorrected(uint mask) => predicted.Correct(mask);

        abstract class Motion
        {
            protected readonly NetworkTransform host;
            protected Motion(NetworkTransform transform) => host = transform;
            protected Transform Target => host.Target;
            public abstract void Update();
        }
        // This client writes the object's pose; the world sends it at its send rate.
        sealed class OwnerMotion : Motion
        {
            public OwnerMotion(NetworkTransform transform) : base(transform) { }
            public override void Update()
            {
                var p = Target.position;
                var r = Target.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                {
                    if (host.positionAxes.Includes(axis))
                        host.client.Set(host.state.Id, host.positionSlots[axis], SyncCodec.Quantize(p[axis], host.positionUnits[axis]));
                    if (host.rotationAxes.Includes(axis))
                        host.client.Set(host.state.Id, host.rotationSlots[axis], QuantizeAngle(r[axis], host.rotationUnits[axis]));
                }
            }
        }
        // Another member's or the room's object: shown from the received poses, between
        // timestamped samples when the world has server time, else approached at a rate.
        sealed class RemoteMotion : Motion
        {
            struct Sample
            {
                public double Time;
                public Vector3 Position, Euler;
                public bool Cut; // reached by a teleport: do not blend into it
            }
            const int MaxSamples = 32;
            readonly Sample[] samples = new Sample[MaxSamples];
            int sampleCount;
            public RemoteMotion(NetworkTransform transform) : base(transform) { }
            public void Reset() => sampleCount = 0;
            public override void Update()
            {
                if (!Interpolate())
                    Approach();
            }
            public void Record(double time)
            {
                if (sampleCount > 0 && time < samples[sampleCount - 1].Time)
                    return; // out of order: keep the newer pose
                if (sampleCount == 0 && host.world.HasServerTime)
                {
                    // Start from the pose on screen so the first update blends in instead of jumping.
                    double now = host.world.ServerTime - host.interpolationDelay;
                    if (now < time)
                        samples[sampleCount++] = new Sample { Time = now, Position = Target.position, Euler = Target.eulerAngles };
                }
                var previous = sampleCount > 0 ? samples[sampleCount - 1].Position : Target.position;
                var sample = new Sample
                {
                    Time = time,
                    Position = host.targetPosition,
                    Euler = host.targetEuler,
                    Cut = host.teleportDistance > 0 && (Merge(previous, host.targetPosition, host.positionAxes) - previous).sqrMagnitude > host.teleportDistance * host.teleportDistance
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
            // Shows the pose at ServerTime - host.interpolationDelay between the two received
            // updates around it; holds the newest pose when updates stop (no extrapolation).
            bool Interpolate()
            {
                if (sampleCount == 0 || host.world == null || !host.world.HasServerTime)
                    return false;
                double time = host.world.ServerTime - host.interpolationDelay;
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
                Target.position = Merge(Target.position, position, host.positionAxes);
                if (host.rotationAxes == SyncAxes.All)
                    Target.rotation = rotation;
                else if (host.rotationAxes != SyncAxes.None)
                    Target.eulerAngles = Merge(Target.eulerAngles, euler, host.rotationAxes);
                return true;
            }
            // Without timestamps: approach the latest pose at a rate per second.
            void Approach()
            {
                float rate = host.interpolation <= 0 ? 1 : 1 - Mathf.Exp(-host.interpolation * Time.unscaledDeltaTime);
                var current = Target.position;
                var goal = Merge(current, host.targetPosition, host.positionAxes);
                if (host.teleportDistance > 0 && (goal - current).sqrMagnitude > host.teleportDistance * host.teleportDistance)
                    rate = 1;
                Target.position = Vector3.Lerp(current, goal, rate);
                if (host.rotationAxes == SyncAxes.All)
                {
                    // Interpolate the orientation as a whole; per-axis Euler blending takes
                    // detours when several axes change at once.
                    Target.rotation = Quaternion.Slerp(Target.rotation, Quaternion.Euler(host.targetEuler), rate);
                }
                else if (host.rotationAxes != SyncAxes.None)
                {
                    var euler = Target.eulerAngles;
                    for (int axis = 0; axis < 3; axis++)
                        if (host.rotationAxes.Includes(axis))
                            euler[axis] = Mathf.LerpAngle(euler[axis], host.targetEuler[axis], rate);
                    Target.eulerAngles = euler;
                }
            }
        }
        // This client's prediction of the object: between its last two ticks, plus the
        // fading offset of the latest corrections by the room.
        sealed class PredictedMotion : Motion
        {
            Vector3 positionError, eulerError;
            public PredictedMotion(NetworkTransform transform) : base(transform) { }
            public void Reset() => positionError = eulerError = Vector3.zero;
            // The world calls Show as it predicts; nothing to do per frame.
            public override void Update() { }
            public void Show(float alpha)
            {
                float fade = host.correctionTime <= 0 ? 0 : Mathf.Exp(-Time.unscaledDeltaTime / host.correctionTime);
                positionError *= fade;
                eulerError *= fade;
                var position = Target.position;
                var euler = Target.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                {
                    if (host.positionAxes.Includes(axis))
                    {
                        int slot = host.positionSlots[axis];
                        position[axis] = Mathf.Lerp(host.state.PreviousPredictedValues[slot], host.state.PredictedValues[slot], alpha) * host.positionUnits[axis] + positionError[axis];
                    }
                    if (host.rotationAxes.Includes(axis))
                    {
                        int slot = host.rotationSlots[axis];
                        euler[axis] = Mathf.LerpAngle(host.state.PreviousPredictedValues[slot] * host.rotationUnits[axis], host.state.PredictedValues[slot] * host.rotationUnits[axis], alpha) + eulerError[axis];
                    }
                }
                Target.position = position;
                if (host.rotationAxes != SyncAxes.None)
                    Target.eulerAngles = euler;
            }
            public void Correct(uint mask)
            {
                for (int axis = 0; axis < 3; axis++)
                {
                    if (host.PositionChanged(mask, axis))
                        positionError[axis] += host.state.PredictionError[host.positionSlots[axis]] * host.positionUnits[axis];
                    if (host.RotationChanged(mask, axis))
                        eulerError[axis] = Mathf.DeltaAngle(0, eulerError[axis] + host.state.PredictionError[host.rotationSlots[axis]] * host.rotationUnits[axis]);
                }
                // A large correction (e.g. a respawn) is a teleport, not an error to fade.
                if (host.teleportDistance > 0 && positionError.sqrMagnitude > host.teleportDistance * host.teleportDistance)
                    positionError = eulerError = Vector3.zero;
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
        /// <summary>Called by the NetworkEntity when it unbinds.</summary>
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
            remote?.Reset();
        }
        void OnDestroy()
        {
            Unbind();
        }
    }
}
#endif
