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
    // Like OnPhotonSerializeView. The owner (or lease holder) writes synchronized fields with
    // entity.SetInt/SetFloat/SetBool at the world's send rate; other clients read them with
    // GetInt/GetFloat/GetBool after updates arrive. Unchanged values are not sent.
    public interface INetworkObservable
    {
        void OnNetworkWrite(NetworkEntity entity);
        void OnNetworkRead(NetworkEntity entity, uint changedFields);
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
        // True when the named field is among changedFields (from OnNetworkRead).
        public bool HasChanged(string name, uint changedFields)
        {
            var field = NetworkSchema.Resolve(schema, name);
            return field != null && (changedFields & (1u << field.Slot)) != 0;
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
        INetworkObservable[] observables = Array.Empty<INetworkObservable>();
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

        // True for the owner of a player or simulated object, and for the current controller
        // of a leased object. Server objects are never mine. A simulated object's owner sends
        // inputs (NetworkWorld.SetInput); the room writes its fields, so CanWrite stays false.
        public bool IsMine => placeholder || Client != null && Object != null && (Owned ? Object.Owner == Client.ConnectionId : Object.Mode == ObjectMode.Lease && Object.Controller == Client.ConnectionId);
        // Owner of a player or simulated object, controller of a leased object (0 when free), 0 for server objects.
        public uint OwnerId => Object == null ? 0 : Owned ? Object.Owner : Object.Mode == ObjectMode.Lease ? Object.Controller : 0;
        bool Owned => Object.Mode == ObjectMode.Player || Object.Mode == ObjectMode.Simulated;
        public ObjectMode Mode => Object?.Mode ?? ObjectMode.Player;

        // ---- Remote procedure calls ----
        sealed class RpcHandler
        {
            public readonly Component Target;
            public readonly NetworkRpcMethod Method;
            public RpcHandler(Component target, NetworkRpcMethod method)
            {
                Target = target;
                Method = method;
            }
        }
        readonly struct PendingRpc
        {
            public readonly uint Method, Recipient;
            public readonly byte Target;
            public readonly byte[] Args;
            public PendingRpc(uint method, byte target, uint recipient, byte[] args)
            {
                Method = method;
                Target = target;
                Recipient = recipient;
                Args = args;
            }
        }
        const int MaxPendingRpcs = 256;
        static readonly byte[] rpcScratch = new byte[SyncCodec.MaxArgs];
        System.Collections.Generic.Dictionary<uint, RpcHandler> rpcById;
        System.Collections.Generic.Dictionary<string, RpcHandler> rpcByName;
        readonly System.Collections.Generic.List<PendingRpc> pendingRpcs = new System.Collections.Generic.List<PendingRpc>();
        // destroying: Destroy was called (no more RPCs); destroyPending: not yet sent.
        bool destroying, destroyPending;

        // Collects [NetworkRpc] methods of components that belong to this entity (not nested entities).
        void BuildRpcTable()
        {
            var byId = new System.Collections.Generic.Dictionary<uint, RpcHandler>();
            var byName = new System.Collections.Generic.Dictionary<string, RpcHandler>(StringComparer.Ordinal);
            foreach (var component in GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (component == null || component.GetComponentInParent<NetworkEntity>(true) != this)
                    continue;
                foreach (var method in NetworkRpcMethod.Of(component.GetType()))
                {
                    if (byId.ContainsKey(method.Id))
                        throw new InvalidOperationException("RPC method ID " + method.Id + " is declared by more than one component of " + name);
                    var handler = new RpcHandler(component, method);
                    byId.Add(method.Id, handler);
                    // A name shared by several components can only be called by ID.
                    byName[method.Name] = byName.ContainsKey(method.Name) ? null : handler;
                }
            }
            rpcById = byId;
            rpcByName = byName;
        }
        // [NetworkRpc] methods of this entity's components (works on prefab assets too).
        public NetworkRpcDescription[] DescribeRpcs()
        {
            var result = new System.Collections.Generic.List<NetworkRpcDescription>();
            foreach (var component in GetComponentsInChildren<MonoBehaviour>(true))
            {
                if (component == null || component.GetComponentInParent<NetworkEntity>(true) != this)
                    continue;
                foreach (var method in NetworkRpcMethod.Of(component.GetType()))
                    result.Add(new NetworkRpcDescription(method.Id, method.Name, component.GetType(), method.AnyCaller, (Type[])method.Parameters.Clone()));
            }
            result.Sort((a, b) => a.Id.CompareTo(b.Id));
            return result.ToArray();
        }
        RpcHandler FindRpc(uint method)
        {
            if (rpcById == null)
                BuildRpcTable();
            if (rpcById.TryGetValue(method, out var handler) && handler.Target != null)
                return handler;
            // Components may be added after spawning; look again once before failing.
            BuildRpcTable();
            return rpcById.TryGetValue(method, out handler) ? handler : null;
        }
        public uint RpcId(string method)
        {
            if (rpcByName == null || !rpcByName.ContainsKey(method))
                BuildRpcTable();
            if (!rpcByName.TryGetValue(method, out var handler))
                throw new ArgumentException("No [NetworkRpc] method named " + method + " on " + name);
            if (handler == null)
                throw new ArgumentException("Several components of " + name + " declare RPC " + method + "; call it by ID");
            return handler.Method.Id;
        }
        void RequireOwnedPlayerObject(string action)
        {
            if (Client == null || Object == null)
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (!Owned || Object.Owner != Client.ConnectionId)
                throw new InvalidOperationException("Only the owner of a player or simulated object can " + action + "; shared objects change through SharedObject.Interact or a lease");
        }
        // Calls a [NetworkRpc] method by name on the receivers selected by target.
        public bool Rpc(string method, RpcTarget target, params object[] args)
        {
            return Rpc(RpcId(method), target, args);
        }
        // Sends now when possible. Inside network callbacks (or before the snapshot completes)
        // the call is queued and sent on the next world Update, preserving call order.
        // Returns false when the transport refuses the packet or the queue is full.
        public bool Rpc(uint method, RpcTarget target, params object[] args)
        {
            byte value = (byte)target;
            if (!RpcTargetByte.IsValid(value) || RpcTargetByte.HasRecipient(value))
                throw new ArgumentOutOfRangeException(nameof(target));
            return Send(method, value, 0, args);
        }
        // Calls the method on one room member only (by connection ID), like Photon's
        // RPC(name, Player, ...). A member that already left receives nothing.
        public bool RpcTo(string method, uint connectionId, params object[] args)
        {
            return RpcTo(RpcId(method), connectionId, args);
        }
        public bool RpcTo(uint method, uint connectionId, params object[] args)
        {
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            return Send(method, RpcTargetByte.Player, connectionId, args);
        }
        bool Send(uint method, byte target, uint recipient, object[] args)
        {
            if (!placeholder && (Client == null || Object == null))
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (!placeholder && !Owned)
                throw new InvalidOperationException("RPCs target player and simulated objects; shared objects change through SharedObject.Interact or a lease");
            var handler = FindRpc(method) ?? throw new ArgumentException("No [NetworkRpc(" + method + ")] method on " + name);
            // Who may call is decided by the room: with an object catalog, only the owner
            // unless the method is in publicMethods; without one, anyone.
            int count = NetworkRpcCodec.Encode(rpcScratch, handler.Method.Parameters, args);
            if (destroying)
                return false;
            // Until the room confirms an Instantiate, calls wait in the queue.
            if (!placeholder && Client.CanSend && pendingRpcs.Count == 0)
                return Client.Rpc(Object.Id, method, target, rpcScratch, count, recipient);
            if (pendingRpcs.Count >= MaxPendingRpcs)
                return false;
            var copy = new byte[count];
            Buffer.BlockCopy(rpcScratch, 0, copy, 0, count);
            pendingRpcs.Add(new PendingRpc(method, target, recipient, copy));
            return true;
        }
        // Ownership of player objects, like PUN's TransferOwnership / RequestOwnership.
        // The owner may hand the object to any member; a prefab with "ownership":"takeover"
        // in the object catalog can be taken by anyone. IsMine and the controlled
        // behaviours follow when the room confirms (OwnershipChanged).
        public event Action<NetworkEntity> OwnershipChanged;
        uint pendingTransfer;
        public bool TransferOwnership(uint connectionId)
        {
            if (Client == null || Object == null)
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (!Owned)
                throw new InvalidOperationException("Ownership transfers player and simulated objects; shared objects use leases");
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            if (Client.CanSend && pendingRpcs.Count == 0)
                return Client.Transfer(Object.Id, connectionId);
            pendingTransfer = connectionId;
            return true;
        }
        public bool RequestOwnership()
        {
            return TransferOwnership(Client?.ConnectionId ?? 0);
        }
        void OnOwnerChanged()
        {
            RefreshControl();
            OwnershipChanged?.Invoke(this);
        }

        // Interest group (like PhotonView.Group): updates and calls of this object reach
        // members subscribed to the group (NetworkWorld.SubscribeGroup); 0 reaches everyone.
        public uint Group => Object?.Group ?? 0;
        uint? pendingGroup;
        public bool SetGroup(uint group)
        {
            RequireOwnedPlayerObject("change its group");
            if (Client.CanSend && pendingRpcs.Count == 0)
                return Client.SetGroup(Object.Id, group);
            pendingGroup = group;
            return true;
        }

        // Despawns a player object owned by this connection (queued like Rpc inside callbacks).
        // Simulated objects belong to the room rules: they are removed by Room.Despawn or
        // when the owner leaves, never by the owner.
        public bool Destroy()
        {
            if (!placeholder)
                RequireOwnedPlayerObject("destroy it");
            if (!placeholder && Object.Mode == ObjectMode.Simulated)
                throw new InvalidOperationException("Simulated objects are removed by the room rules (Room.Despawn) or when their owner leaves");
            if (destroying)
                return true;
            destroying = true;
            if (!placeholder && Client.CanSend && pendingRpcs.Count == 0)
            {
                if (Client.Destroy(Object.Id))
                    return true;
            }
            destroyPending = true;
            return true;
        }
        void ServiceOutgoing()
        {
            if (Client == null || Object == null || !Client.CanSend)
                return;
            while (pendingRpcs.Count > 0)
            {
                var call = pendingRpcs[0];
                if (!Client.Rpc(Object.Id, call.Method, call.Target, call.Args, call.Args.Length, call.Recipient))
                    return;
                pendingRpcs.RemoveAt(0);
            }
            if (pendingGroup.HasValue && Client.SetGroup(Object.Id, pendingGroup.Value))
                pendingGroup = null;
            if (pendingTransfer != 0 && Client.Transfer(Object.Id, pendingTransfer))
                pendingTransfer = 0;
            if (destroyPending && Client.Destroy(Object.Id))
                destroyPending = false;
        }
        // Decodes now (the arguments borrow the received packet) and returns the call to run
        // after the packet is applied. Malformed or unknown calls are reported and dropped.
        internal Action PrepareRpc(uint method, uint sender, byte[] args, int offset, int count)
        {
            RpcHandler handler;
            try
            {
                handler = FindRpc(method);
            }
            catch (InvalidOperationException e)
            {
                Debug.LogException(e, this);
                return null;
            }
            if (handler == null)
            {
                Debug.LogWarning("Dropped RPC " + method + ": no [NetworkRpc(" + method + ")] method on " + name, this);
                return null;
            }
            object[] values;
            try
            {
                var info = new NetworkRpcInfo(this, method, sender, Client != null && sender == Client.ConnectionId);
                values = NetworkRpcCodec.Decode(args, offset, count, handler.Method, info);
            }
            catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                Debug.LogWarning("Dropped RPC " + method + " on " + name + ": " + e.Message, this);
                return null;
            }
            return () => InvokeRpc(handler, values);
        }
        void InvokeRpc(RpcHandler handler, object[] values)
        {
            // The entity may have been despawned or detached by an earlier call in the same packet.
            if (Object == null || handler.Target == null)
                return;
            object result;
            try
            {
                result = handler.Method.Method.Invoke(handler.Target, values);
            }
            catch (System.Reflection.TargetInvocationException e)
            {
                Debug.LogException(e.InnerException ?? e, handler.Target);
                return;
            }
            if (handler.Method.IsCoroutine && result is System.Collections.IEnumerator routine)
            {
                if (handler.Target is MonoBehaviour behaviour && behaviour.isActiveAndEnabled)
                    behaviour.StartCoroutine(routine);
                else
                    Debug.LogWarning("Coroutine RPC " + handler.Method.Name + " skipped on an inactive component", handler.Target);
            }
        }

        // Instantiate: owned locally before the room confirms the object.
        bool placeholder;
        // True while binding a confirmed placeholder: components keep the local pose.
        internal bool BindingPlaceholder
        {
            get; private set;
        }
        public bool IsSpawned => Object != null;
        internal void BeginPlaceholder()
        {
            placeholder = true;
            RefreshControl();
        }

        public void Bind(SyncClient client, NetworkObject state)
        {
            // A confirmed placeholder keeps the calls queued before confirmation.
            bool confirming = placeholder;
            var queued = confirming ? pendingRpcs.ToArray() : null;
            bool destroyQueued = confirming && destroying;
            // Unbind while still a placeholder so control does not flicker off and on.
            Unbind();
            placeholder = false;
            if (confirming)
            {
                pendingRpcs.AddRange(queued);
                destroying = destroyPending = destroyQueued;
            }
            BindingPlaceholder = confirming;
            Client = client;
            Object = state;
            desired = acquireOnEnable && isActiveAndEnabled;
            var behaviours = GetComponentsInChildren<MonoBehaviour>(true);
            var b = new System.Collections.Generic.List<INetworkBinding>();
            var t = new System.Collections.Generic.List<INetworkTick>();
            var o = new System.Collections.Generic.List<INetworkObservable>();
            foreach (var component in behaviours)
            {
                // Child NetworkEntities have their own replicated identity.
                if (component.GetComponentInParent<NetworkEntity>(true) != this)
                    continue;
                if (component is INetworkBinding binding)
                    b.Add(binding);
                if (component is INetworkTick tick)
                    t.Add(tick);
                if (component is INetworkObservable observable)
                    o.Add(observable);
            }
            bindings = b.ToArray();
            ticks = t.ToArray();
            observables = o.ToArray();
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
                if (observables.Length != 0)
                {
                    Object.Changed += ReadObservables;
                    ReadObservables(Object.Fields);
                }
                if (state.Mode == ObjectMode.Player || state.Mode == ObjectMode.Simulated)
                    state.AuthorityChanged += OnOwnerChanged;
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
            finally
            {
                BindingPlaceholder = false;
            }
        }

        void RefreshControl()
        {
            bool next = isActiveAndEnabled && (placeholder || Client != null && Object != null && Client.CanWrite(Object.Id) && (Object.Mode != ObjectMode.Lease || desired));
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
        void ReadObservables(uint changed)
        {
            if (IsMine)
                return;
            foreach (var observable in observables)
            {
                try
                {
                    observable.OnNetworkRead(this, changed);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }
        // Called by the world right before each batched send.
        internal void WriteObservables()
        {
            if (observables.Length == 0 || Client == null || Object == null || !Client.CanWrite(Object.Id))
                return;
            foreach (var observable in observables)
            {
                try
                {
                    observable.OnNetworkWrite(this);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
        }
        // The world services even inactive entities so a delayed grant can be released.
        internal void Service()
        {
            ServiceOutgoing();
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
            {
                Object.Changed -= PublishProperties;
                Object.Changed -= ReadObservables;
                Object.AuthorityChanged -= OnOwnerChanged;
            }
            pendingTransfer = 0;
            pendingGroup = null;
            observables = Array.Empty<INetworkObservable>();
            propertyFields = Array.Empty<NetworkSchema.Field>();
            rpcById = null;
            rpcByName = null;
            pendingRpcs.Clear();
            destroying = destroyPending = false;
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
