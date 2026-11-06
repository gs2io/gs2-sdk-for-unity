#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Realtime.Sync
{
    public interface INetworkBinding
    {
        void Bind(SyncClient client, NetworkObject state);
        void Unbind();
    }
    // Called by the world outside Receive, including for disabled components.
    public interface INetworkTick
    {
        void NetworkTick();
    }
    [Serializable]
    public sealed class NetworkBoolEvent : UnityEvent<bool>
    {
    }
    [Serializable]
    public sealed class NetworkIntEvent : UnityEvent<int>
    {
    }
    [Serializable]
    public sealed class NetworkFloatEvent : UnityEvent<float>
    {
    }
    [Serializable]
    public sealed class NetworkStringEvent : UnityEvent<string>
    {
    }

    [DisallowMultipleComponent]
    public sealed class NetworkEntity : MonoBehaviour
    {
        [Header("Synchronized properties")]
        [SerializeField, Tooltip("Optional. Position and rotation have built-in fields; assign a schema for custom fields or precision.")] NetworkSchema schema;
        [SerializeField] NetworkPropertyBinding[] propertyBindings = Array.Empty<NetworkPropertyBinding>();
        NetworkSchema.Field[] propertyFields = Array.Empty<NetworkSchema.Field>();
        public NetworkSchema Schema
        {
            get => schema;
            set
            {
                if (Object != null)
                    throw new InvalidOperationException("Unbind before changing schema");
                schema = value;
            }
        }
        public NetworkPropertyBinding[] PropertyBindings
        {
            get => (NetworkPropertyBinding[])propertyBindings.Clone();
            set
            {
                if (Object != null)
                    throw new InvalidOperationException("Unbind before changing bindings");
                propertyBindings = value == null ? Array.Empty<NetworkPropertyBinding>() : (NetworkPropertyBinding[])value.Clone();
            }
        }
        NetworkSchema.Field RequireField(string name, NetworkValueType type)
        {
            var field = NetworkSchema.Resolve(schema, name);
            if (Object == null || field == null || field.Type != type || (Object.Fields & (1u << field.Slot)) == 0)
                throw new InvalidOperationException("Unavailable or differently typed network field: " + name);
            return field;
        }
        public int GetInt(string name)
        {
            var field = RequireField(name, NetworkValueType.Int);
            return Object.Values[field.Slot];
        }
        public bool GetBool(string name)
        {
            var field = RequireField(name, NetworkValueType.Bool);
            return Object.Values[field.Slot] != 0;
        }
        public float GetFloat(string name)
        {
            var field = RequireField(name, NetworkValueType.Float);
            return Object.Values[field.Slot] * field.Precision;
        }
        public void SetInt(string name, int value)
        {
            var field = RequireField(name, NetworkValueType.Int);
            Client.Set(Object.Id, field.Slot, value);
        }
        public void SetBool(string name, bool value)
        {
            var field = RequireField(name, NetworkValueType.Bool);
            Client.Set(Object.Id, field.Slot, value ? 1 : 0);
        }
        public void SetFloat(string name, float value)
        {
            var field = RequireField(name, NetworkValueType.Float);
            Client.Set(Object.Id, field.Slot, SyncCodec.Quantize(value, field.Precision));
        }
        void BindProperties()
        {
            if (schema != null && !schema.Describes(Object))
                throw new InvalidOperationException("Object does not match its network schema");
            propertyFields = new NetworkSchema.Field[propertyBindings.Length];
            for (int i = 0; i < propertyBindings.Length; i++)
            {
                var name = propertyBindings[i]?.Field;
                propertyFields[i] = NetworkSchema.Resolve(schema, name) ?? throw new InvalidOperationException("Unknown network field: " + name);
                if ((Object.Fields & (1u << propertyFields[i].Slot)) == 0)
                    throw new InvalidOperationException("Field is not enabled by the server: " + name);
            }
        }
        void PublishProperties(uint changed)
        {
            for (int i = 0; i < propertyFields.Length; i++)
                if ((changed & (1u << propertyFields[i].Slot)) != 0)
                    propertyBindings[i].Publish(propertyFields[i], Object.Values[propertyFields[i].Slot]);
        }
        [Header("Control")]
        [SerializeField] bool acquireOnEnable;
        [SerializeField] bool autoRenew = true;
        [SerializeField] Behaviour[] controlledBehaviours = Array.Empty<Behaviour>();
        [SerializeField] NetworkBoolEvent controlChanged = new NetworkBoolEvent();
        [SerializeField] NetworkIntEvent commandCompleted = new NetworkIntEvent();
        INetworkBinding[] bindings = Array.Empty<INetworkBinding>();
        INetworkTick[] ticks = Array.Empty<INetworkTick>();
        bool desired, hasControl, pending, releasePending;
        uint request;
        float renewAt, retryAt;

        public NetworkObject Object
        {
            get; private set;
        }
        public SyncClient Client
        {
            get; private set;
        }
        public bool HasControl => hasControl;
        public bool IsRequesting => pending;
        public CommandResult LastResult
        {
            get; private set;
        }
        public Behaviour[] ControlledBehaviours
        {
            get => (Behaviour[])controlledBehaviours.Clone();
            set => controlledBehaviours = value ?? Array.Empty<Behaviour>();
        }
        public NetworkBoolEvent ControlChanged => controlChanged;
        public NetworkIntEvent CommandCompleted => commandCompleted;

        public void Bind(SyncClient client, NetworkObject state)
        {
            Unbind();
            Client = client;
            Object = state;
            desired = acquireOnEnable && isActiveAndEnabled;
            var behaviours = GetComponentsInChildren<MonoBehaviour>(true);
            var b = new System.Collections.Generic.List<INetworkBinding>();
            var t = new System.Collections.Generic.List<INetworkTick>();
            foreach (var component in behaviours)
            {
                // Child NetworkEntities have their own replicated identity.
                if (component.GetComponentInParent<NetworkEntity>(true) != this)
                    continue;
                if (component is INetworkBinding binding)
                    b.Add(binding);
                if (component is INetworkTick tick)
                    t.Add(tick);
            }
            bindings = b.ToArray();
            ticks = t.ToArray();
            try
            {
                BindProperties();
                foreach (var binding in bindings)
                    binding.Bind(client, state);
                if (propertyFields.Length != 0)
                {
                    Object.Changed += PublishProperties;
                    PublishProperties(Object.Fields);
                }
                // Only leased objects need command results and renewal notices.
                if (state.Mode == ObjectMode.Lease)
                {
                    client.AuthorityChanged += OnAuthority;
                    client.CommandCompleted += OnResult;
                }
                RefreshControl();
                ScheduleRenew();
            }
            catch
            {
                Unbind();
                throw;
            }
        }

        void RefreshControl()
        {
            bool next = isActiveAndEnabled && Client != null && Object != null && Client.CanWrite(Object.Id) && (Object.Mode != ObjectMode.Lease || desired);
            foreach (var behaviour in controlledBehaviours)
            {
                if (behaviour == null)
                    continue;
                if (behaviour is INetworkBinding || behaviour is NetworkEntity || behaviour is NetworkWorld)
                    throw new InvalidOperationException("Control only gameplay behaviours, not network components");
                behaviour.enabled = next;
            }
            if (next != hasControl)
            {
                hasControl = next;
                controlChanged.Invoke(next);
            }
        }
        void ScheduleRenew()
        {
            renewAt = Time.unscaledTime + Mathf.Max(.05f, Mathf.Min(2f, (Object?.LeaseMilliseconds ?? 0) / 3000f));
        }
        void OnAuthority(NetworkObject value)
        {
            if (value != Object)
                return;
            if (hasControl && value.Controller != Client.ConnectionId)
                desired = false;
            RefreshControl();
            ScheduleRenew();
        }
        void OnResult(uint id, uint requestId, CommandResult result)
        {
            if (Object == null || id != Object.Id || requestId != request)
                return;
            pending = false;
            LastResult = result;
            if (result != CommandResult.Success && result != CommandResult.AlreadyApplied)
                desired = false;
            commandCompleted.Invoke((int)result);
        }
        // Safe from UnityEvents/Receive callbacks: send on the next world Update.
        public void RequestControl()
        {
            desired = true;
            releasePending = false;
            retryAt = 0;
            RefreshControl();
        }
        public void ReleaseControl()
        {
            desired = false;
            releasePending = true;
            retryAt = 0;
            RefreshControl();
        }

        void ServiceControl()
        {
            RefreshControl();
            if (Client == null || !Client.CanSend || Object == null || Object.Mode != ObjectMode.Lease)
                return;
            if (!isActiveAndEnabled)
            {
                desired = false;
                releasePending = true;
            }
            bool owned = Client.CanWrite(Object.Id);
            if (pending || Time.unscaledTime < retryAt)
                return;
            LeaseAction action;
            if (releasePending && owned)
                action = LeaseAction.Release;
            else if (desired && !owned)
                action = LeaseAction.Acquire;
            else if (desired && owned && autoRenew && Time.unscaledTime >= renewAt)
                action = LeaseAction.Renew;
            else
                return;
            request = Client.Lease(Object.Id, action);
            retryAt = Time.unscaledTime + .25f;
            if (request != 0)
            {
                pending = true;
                if (action == LeaseAction.Release)
                    releasePending = false;
                ScheduleRenew();
            }
        }
        // The world services even inactive entities so a delayed grant can be released.
        internal void Service()
        {
            ServiceControl();
            foreach (var tick in ticks)
                tick.NetworkTick();
        }
        void OnEnable()
        {
            if (acquireOnEnable)
                RequestControl();
        }
        void OnDisable()
        {
            ReleaseControl();
        }

        public void Unbind()
        {
            if (Client != null)
            {
                if (Object != null && Object.Mode == ObjectMode.Lease && Client.CanSend && Client.CanWrite(Object.Id))
                    Client.Lease(Object.Id, LeaseAction.Release);
                Client.AuthorityChanged -= OnAuthority;
                Client.CommandCompleted -= OnResult;
            }
            if (Object != null)
                Object.Changed -= PublishProperties;
            propertyFields = Array.Empty<NetworkSchema.Field>();
            Client = null;
            Object = null;
            desired = pending = releasePending = false;
            request = 0;
            RefreshControl();
            foreach (var binding in bindings)
                binding.Unbind();
            bindings = Array.Empty<INetworkBinding>();
            ticks = Array.Empty<INetworkTick>();
        }
        void OnDestroy()
        {
            Unbind();
        }
    }
}
#endif
