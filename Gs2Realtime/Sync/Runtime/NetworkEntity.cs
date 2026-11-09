#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// Implement on a component of a NetworkEntity to receive the room's object when the
    /// entity binds (Bind) and to let go of it when the entity unbinds (Unbind).
    /// </summary>
    public interface INetworkBinding
    {
        void Bind(SyncClient client, NetworkObject state);
        void Unbind();
    }
    /// <summary>
    /// Implement on a component of a NetworkEntity to run on each world update while the
    /// entity is bound. Called outside Receive, including for disabled components.
    /// </summary>
    public interface INetworkTick
    {
        void NetworkTick();
    }
    /// <summary>
    /// Like OnPhotonSerializeView. The owner (or lease holder) writes synchronized fields with
    /// entity.SetInt/SetFloat/SetBool at the world's send rate; other clients read them with
    /// GetInt/GetFloat/GetBool after updates arrive. Unchanged values are not sent.
    /// </summary>
    public interface INetworkObservable
    {
        /// <summary>Called right before each batched send while this client writes the object.</summary>
        void OnNetworkWrite(NetworkEntity entity);
        /// <summary>
        /// Called when updates arrive on a client that does not hold the object;
        /// changedFields is a slot bit mask (test it with entity.HasChanged).
        /// </summary>
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

    /// <summary>
    /// Ties a GameObject to one object of the room: synchronized fields, RPCs, ownership and
    /// control. Instances are bound and unbound by the NetworkWorld that creates them.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class NetworkEntity : MonoBehaviour
    {
        [Header("Synchronized properties")]
        [SerializeField, Tooltip("Optional. Position and rotation have built-in fields; assign a schema for custom fields or precision.")] NetworkSchema schema;
        // Fields whose changes raise UnityEvents.
        [SerializeField] NetworkPropertyBinding[] propertyBindings = Array.Empty<NetworkPropertyBinding>();
        NetworkSchema.Field[] propertyFields = Array.Empty<NetworkSchema.Field>();
        /// <summary>
        /// The schema of the object's fields; null means only the standard Position/Rotation
        /// fields. Throws when changed while bound.
        /// </summary>
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
        /// <summary>
        /// Field-change bindings (get returns a copy). Throws when changed while bound.
        /// </summary>
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
            // Values exist only once the room confirms the object: an Instantiate'd entity
            // may send RPCs before that, but its fields wait for IsSpawned.
            if (Object == null)
                throw new InvalidOperationException(phase == EntityPhase.Placeholder
                    ? "Field " + name + " is not available yet: the room has not confirmed this Instantiate (IsSpawned is false); set fields once it has, e.g. in NetworkWorld.Spawned"
                    : "Field " + name + " is not available: the entity is not bound to a network object");
            var field = NetworkSchema.Resolve(schema, name);
            if (field == null)
                throw new InvalidOperationException("No network field " + name + (schema != null ? " in schema " + schema.name : " (no schema: only the standard Position/Rotation fields)"));
            if (field.Type != type)
                throw new InvalidOperationException("Network field " + name + " is " + field.Type + ", not " + type);
            if (!Object.HasField(field.Slot))
                throw new InvalidOperationException("Network field " + name + " is not among the fields the room enabled for prefab " + Object.Prefab);
            return field;
        }
        /// <summary>
        /// True when the named field is among changedFields (from OnNetworkRead); false for an
        /// unknown name.
        /// </summary>
        public bool HasChanged(string name, uint changedFields)
        {
            var field = NetworkSchema.Resolve(schema, name);
            return field != null && FieldMask.Contains(changedFields, field.Slot);
        }
        /// <summary>
        /// Reads the value this client sees: the writer (owner or lease holder) reads its own
        /// latest Set* of the field, sent or not, so read-modify-write works between sends;
        /// other clients, and fields the writer has not set, read the room's latest state.
        /// Throws until the room confirms the object (IsSpawned) or for an unknown field.
        /// </summary>
        public int GetInt(string name)
        {
            return Read(RequireField(name, NetworkValueType.Int));
        }
        /// <summary>Reads a Bool field; see GetInt.</summary>
        public bool GetBool(string name)
        {
            return Read(RequireField(name, NetworkValueType.Bool)) != 0;
        }
        /// <summary>Reads a Float field, scaled by its precision; see GetInt.</summary>
        public float GetFloat(string name)
        {
            var field = RequireField(name, NetworkValueType.Float);
            return Read(field) * field.Precision;
        }
        /// <summary>
        /// Reads the room's latest state of the field; for the writer it does not include its
        /// own unconfirmed writes.
        /// </summary>
        public int GetConfirmedInt(string name)
        {
            return Object.Values[RequireField(name, NetworkValueType.Int).Slot];
        }
        /// <summary>Reads the room's latest state of a Bool field; see GetConfirmedInt.</summary>
        public bool GetConfirmedBool(string name)
        {
            return Object.Values[RequireField(name, NetworkValueType.Bool).Slot] != 0;
        }
        /// <summary>Reads the room's latest state of a Float field; see GetConfirmedInt.</summary>
        public float GetConfirmedFloat(string name)
        {
            var field = RequireField(name, NetworkValueType.Float);
            return Object.Values[field.Slot] * field.Precision;
        }
        int Read(NetworkSchema.Field field)
        {
            if (WritableHere && Object.TryWritten(field.Slot, out int written))
                return written;
            return Object.Values[field.Slot];
        }
        /// <summary>
        /// Writes a field for the owner or lease holder to send at the world's send rate.
        /// Throws until the room confirms the object (IsSpawned) or for an unknown field.
        /// </summary>
        public void SetInt(string name, int value)
        {
            var field = RequireField(name, NetworkValueType.Int);
            Client.Set(Object.Id, field.Slot, value);
        }
        /// <summary>Writes a Bool field; see SetInt.</summary>
        public void SetBool(string name, bool value)
        {
            var field = RequireField(name, NetworkValueType.Bool);
            Client.Set(Object.Id, field.Slot, value ? 1 : 0);
        }
        /// <summary>Writes a Float field, quantized to its precision; see SetInt.</summary>
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
                if (!Object.HasField(propertyFields[i].Slot))
                    throw new InvalidOperationException("Field is not enabled by the server: " + name);
            }
        }
        void PublishProperties(uint changed)
        {
            for (int i = 0; i < propertyFields.Length; i++)
                if (FieldMask.Contains(changed, propertyFields[i].Slot))
                    propertyBindings[i].Publish(propertyFields[i], Object.Values[propertyFields[i].Slot]);
        }
        [Header("Control")]
        // Calls RequestControl when enabled (for a leased object: acquire its lease).
        [SerializeField] bool acquireOnEnable;
        // Renews a held lease before it expires while control is requested.
        [SerializeField] bool autoRenew = true;
        // Enabled only while this client has control (HasControl), disabled otherwise.
        [SerializeField] Behaviour[] controlledBehaviours = Array.Empty<Behaviour>();
        // Invoked with the new HasControl when it changes.
        [SerializeField] NetworkBoolEvent controlChanged = new NetworkBoolEvent();
        // Invoked with the room's CommandResult (as int) for each lease request.
        [SerializeField] NetworkIntEvent commandCompleted = new NetworkIntEvent();
        INetworkBinding[] bindings = Array.Empty<INetworkBinding>();
        INetworkTick[] ticks = Array.Empty<INetworkTick>();
        INetworkObservable[] observables = Array.Empty<INetworkObservable>();
        bool hasControl;
        // How this client comes to drive the bound object, by its kind (none while unbound).
        Control control = NoControl.Instance;

        /// <summary>
        /// The room's object this entity is bound to; null while unbound and before the room
        /// confirms an Instantiate.
        /// </summary>
        public NetworkObject Object
        {
            get; private set;
        }
        // Unbound -> Placeholder (an Instantiate the room has not confirmed: owned locally,
        // its calls queued) -> Bound (to the room's object) -> Unbound. Only the world moves
        // an entity forward (BeginPlaceholder, Bind); Unbind ends any phase.
        enum EntityPhase : byte { Unbound, Placeholder, Bound }
        EntityPhase phase;
        bool Bound => phase == EntityPhase.Bound;
        // Bound, and this client holds the object (once the room's state is applied).
        bool HeldHere => Bound && Client.Controls(Object.Id);
        // Bound, and this client sends the object's field values.
        bool WritableHere => Bound && Client.CanWrite(Object.Id);
        // Bound to a leased object, whose lease this client holds.
        bool LeaseHeldHere => Bound && Client.Ready && Object.LeaseHeldByMe;
        // Bound to an object no member owns (leased or server): it takes no RPCs.
        bool BoundToShared => Bound && !Object.Kind.Owned;
        internal SyncClient Client
        {
            get; private set;
        }
        // The world that created this instance (null while unbound).
        internal NetworkWorld World
        {
            get; private set;
        }
        /// <summary>
        /// True when bound to an object of the prefab (false before the room confirms an
        /// Instantiate).
        /// </summary>
        public bool IsPrefab(uint prefab) => Bound && Object.Prefab == prefab;
        /// <summary>
        /// True while this client controls the object and the controlled behaviours are
        /// enabled (see ControlChanged).
        /// </summary>
        public bool HasControl => hasControl;
        /// <summary>True while a lease request waits for the room's answer.</summary>
        public bool IsRequesting => control.IsRequesting;
        /// <summary>The room's result for the last answered lease request.</summary>
        public CommandResult LastResult
        {
            get; private set;
        }
        /// <summary>
        /// Gameplay behaviours enabled only while this client has control (get returns a
        /// copy). Network components are refused.
        /// </summary>
        public Behaviour[] ControlledBehaviours
        {
            get => (Behaviour[])controlledBehaviours.Clone();
            set => controlledBehaviours = value ?? Array.Empty<Behaviour>();
        }
        /// <summary>Invoked with the new HasControl when it changes.</summary>
        public NetworkBoolEvent ControlChanged => controlChanged;
        /// <summary>Invoked with the room's CommandResult (as int) for each lease request.</summary>
        public NetworkIntEvent CommandCompleted => commandCompleted;

        /// <summary>
        /// True while this client holds the object: owns a player or simulated object, holds
        /// a leased object's lease, or made an Instantiate the room has not confirmed yet.
        /// Server objects are never mine. A simulated object's owner sends inputs
        /// (NetworkWorld.SetInput); the room writes its fields. HasControl follows IsMine,
        /// and for a leased object also the game's RequestControl.
        /// </summary>
        public bool IsMine => phase == EntityPhase.Placeholder || Holds();
        bool Holds() => Bound && Object.IsMine;
        /// <summary>
        /// Connection ID of the holder (owner, or the member holding the lease); 0 when free,
        /// for a server object, or while unbound.
        /// </summary>
        public uint OwnerId => Object?.Owner ?? 0;
        /// <summary>The bound object's mode; Player while unbound.</summary>
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
        // Calls made on an Instantiate the room has not confirmed yet (no object ID to
        // address): sent, in order, when it confirms. Everything else goes straight to the
        // client, whose outbox keeps the order of all commands.
        readonly System.Collections.Generic.List<PendingRpc> pendingRpcs = new System.Collections.Generic.List<PendingRpc>();
        // destroying: Destroy was taken, so no more RPCs; the despawn follows any queued
        // calls. despawnSent: the client took the despawn; until then it is retried.
        bool destroying, despawnSent;

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
        /// <summary>
        /// Lists the [NetworkRpc] methods of this entity's own components (not nested
        /// entities), sorted by ID. Works on prefab assets too.
        /// </summary>
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
        /// <summary>
        /// The ID of the [NetworkRpc] method with this name. Throws ArgumentException when no
        /// component declares it, or several do (call it by ID then).
        /// </summary>
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
            if (!Bound)
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (!Object.OwnedByMe)
                throw new InvalidOperationException("Only the owner of a player or simulated object can " + action + "; shared objects change through SharedObject.Interact or a lease");
        }
        /// <summary>Calls a [NetworkRpc] method by name; see Rpc(uint, RpcTarget, object[]).</summary>
        public bool Rpc(string method, RpcTarget target, params object[] args)
        {
            return Rpc(RpcId(method), target, args);
        }
        /// <summary>
        /// Calls a [NetworkRpc] method on the receivers selected by target, in call order with
        /// every other command of this client. On an unconfirmed Instantiate the call waits
        /// for the room's confirmation. Only player and simulated objects take RPCs.
        /// </summary>
        /// <returns>False when the outbox (or the confirmation queue) is full, or after Destroy.</returns>
        public bool Rpc(uint method, RpcTarget target, params object[] args)
        {
            byte value = (byte)target;
            if (!RpcTargetByte.IsUnaddressed(value))
                throw new ArgumentOutOfRangeException(nameof(target));
            return Send(method, value, 0, args);
        }
        /// <summary>
        /// Calls the method on one room member only (by connection ID), like Photon's
        /// RPC(name, Player, ...). A member that already left receives nothing.
        /// </summary>
        public bool RpcTo(string method, uint connectionId, params object[] args)
        {
            return RpcTo(RpcId(method), connectionId, args);
        }
        /// <summary>Calls the method by ID on one room member only; see RpcTo(string, uint, object[]).</summary>
        public bool RpcTo(uint method, uint connectionId, params object[] args)
        {
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            return Send(method, RpcTargetByte.Player, connectionId, args);
        }
        bool Send(uint method, byte target, uint recipient, object[] args)
        {
            if (phase == EntityPhase.Unbound)
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (BoundToShared)
                throw new InvalidOperationException("RPCs target player and simulated objects; shared objects change through SharedObject.Interact or a lease");
            var handler = FindRpc(method) ?? throw new ArgumentException("No [NetworkRpc(" + method + ")] method on " + name);
            // Who may call is decided by the room: with an object catalog, only the owner
            // unless the method is in publicMethods; without one, anyone.
            int count = NetworkRpcCodec.Encode(rpcScratch, handler.Method, args);
            if (destroying)
                return false;
            // Calls queued before the room confirmed this Instantiate go first: a later call
            // waits behind any the outbox has not taken yet.
            if (Bound && pendingRpcs.Count == 0)
                return Client.SendRpc(Object.Id, method, target, recipient, rpcScratch, count);
            if (pendingRpcs.Count >= MaxPendingRpcs)
                return false;
            var copy = new byte[count];
            Buffer.BlockCopy(rpcScratch, 0, copy, 0, count);
            pendingRpcs.Add(new PendingRpc(method, target, recipient, copy));
            if (Bound)
                SendQueued(); // behind the waiting calls, in order
            return true;
        }
        /// <summary>
        /// Raised when the room confirms a new owner of a player or simulated object; IsMine
        /// and the controlled behaviours are already updated.
        /// </summary>
        public event Action<NetworkEntity> OwnershipChanged;
        /// <summary>
        /// Hands a player object to another member, like PUN's TransferOwnership. Only the
        /// owner may, unless the prefab has "ownership":"takeover" in the object catalog.
        /// Takes effect when the room confirms (OwnershipChanged).
        /// </summary>
        public bool TransferOwnership(uint connectionId)
        {
            if (!Bound)
                throw new InvalidOperationException("Entity is not bound to a network object");
            if (!Object.Kind.Transferable)
                throw new InvalidOperationException("Ownership transfers player objects; shared objects use leases, simulated objects stay with their owner");
            if (connectionId == 0)
                throw new ArgumentOutOfRangeException(nameof(connectionId));
            return Client.Transfer(Object.Id, connectionId);
        }
        /// <summary>
        /// Asks for ownership of a player object for this client, like PUN's
        /// RequestOwnership; see TransferOwnership.
        /// </summary>
        public bool RequestOwnership()
        {
            return TransferOwnership(Client?.ConnectionId ?? 0);
        }
        void OnOwnerChanged()
        {
            RefreshControl();
            OwnershipChanged?.Invoke(this);
        }

        /// <summary>
        /// Interest group (like PhotonView.Group): updates and calls of this object reach
        /// members subscribed to the group (NetworkWorld.SubscribeGroup); 0 reaches everyone.
        /// </summary>
        public uint Group => Object?.Group ?? 0;
        /// <summary>
        /// Moves the object to an interest group (see Group). Only the owner of a player or
        /// simulated object may.
        /// </summary>
        public bool SetGroup(uint group)
        {
            RequireOwnedPlayerObject("change its group");
            return Client.SetGroup(Object.Id, group);
        }

        /// <summary>
        /// Despawns a player object owned by this connection, in call order with its RPCs
        /// (on an unconfirmed Instantiate, once the room confirms it). Simulated objects are
        /// removed by the room rules (Room.Despawn) or when the owner leaves, never by the owner.
        /// </summary>
        /// <returns>False when the outbox refused the despawn: call again.</returns>
        public bool Destroy()
        {
            if (phase != EntityPhase.Placeholder)
                RequireOwnedPlayerObject("destroy it");
            bool removedByTheRules = Bound && !Object.Kind.HolderDespawns; // a simulated object
            if (removedByTheRules)
                throw new InvalidOperationException("Simulated objects are removed by the room rules (Room.Despawn) or when their owner leaves");
            if (destroying)
                return !Bound || SendQueued(); // taken: sent, or sent again now
            if (Bound && pendingRpcs.Count == 0)
            {
                if (!SendDespawn())
                    return false; // not taken: call again
                destroying = true;
                return true;
            }
            // Before the confirmation, or behind calls still waiting: taken, and sent after them.
            destroying = true;
            if (Bound)
                SendQueued();
            return true;
        }
        // Sends the despawn once; false while the outbox refuses it.
        bool SendDespawn()
        {
            if (!despawnSent)
                despawnSent = Client.Destroy(Object.Id);
            return despawnSent;
        }
        // Sends what waits since before the room confirmed this Instantiate, in order: the
        // calls, then the despawn. It stops at the first one the outbox refuses, which is
        // tried again (next update, or the next Rpc / Destroy); true once all are sent.
        bool SendQueued()
        {
            int sent = 0;
            while (sent < pendingRpcs.Count)
            {
                var call = pendingRpcs[sent];
                if (!Client.SendRpc(Object.Id, call.Method, call.Target, call.Recipient, call.Args, call.Args.Length))
                    break;
                sent++;
            }
            pendingRpcs.RemoveRange(0, sent);
            return pendingRpcs.Count == 0 && (!destroying || SendDespawn());
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

        // True while a confirmed placeholder binds: components keep the local pose.
        internal bool BindingPlaceholder
        {
            get; private set;
        }
        /// <summary>
        /// True once bound to the room's object; fields can be read and written only then.
        /// </summary>
        public bool IsSpawned => Object != null;
        /// <summary>
        /// The room's answer to the Instantiate that created this instance (null for
        /// instances the world created for the room's objects).
        /// </summary>
        public SpawnRequest SpawnRequest
        {
            get; internal set;
        }
        internal void BeginPlaceholder()
        {
            if (phase != EntityPhase.Unbound)
                throw new InvalidOperationException("Only an unbound entity becomes a placeholder");
            phase = EntityPhase.Placeholder;
            RefreshControl();
        }

        // Called by the NetworkWorld that created this instance.
        internal void Bind(NetworkWorld world, SyncClient client, NetworkObject state)
        {
            if (phase == EntityPhase.Bound)
                throw new InvalidOperationException(name + " is already bound to object " + Object.Id);
            // A confirmed placeholder goes straight to Bound: its queued calls (and a queued
            // Destroy) stay for SendQueued, and its control does not flicker off and on.
            bool confirming = phase == EntityPhase.Placeholder;
            BindingPlaceholder = confirming;
            World = world;
            Client = client;
            Object = state;
            phase = EntityPhase.Bound;
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
                control = Control.For(this, state);
                RefreshControl();
                if (confirming && !SendQueued())
                    Debug.LogWarning("Calls or the despawn of " + name + " wait: the outbox is full; they are sent on the next update", this);
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

        // This client drives the object, and wants to: a leased object only while the
        // game requested control (RequestControl). Driving a simulated object means
        // sending inputs, so its input behaviours are enabled although the rules write
        // its fields (CanWrite is false).
        bool Controlling() => HeldHere && control.Wants;
        void RefreshControl()
        {
            bool next = isActiveAndEnabled && (phase == EntityPhase.Placeholder || Controlling());
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
                if (next)
                    control.Gained();
                controlChanged.Invoke(next);
            }
        }
        /// <summary>
        /// Asks for the object (a leased object's lease, renewed while held with Auto Renew).
        /// Safe from UnityEvents and Receive callbacks: the request goes out on the next world
        /// update. The controlled behaviours are enabled once this client has control
        /// (HasControl, ControlChanged).
        /// </summary>
        public void RequestControl()
        {
            control.Request();
            RefreshControl();
        }
        /// <summary>
        /// RequestControl that can be waited for (yield return, OnCompleted): Succeeded once
        /// this client has control; Refused with the room's reason (LeaseBusy: another player
        /// holds it; Forbidden: an object this client cannot control, such as a server object
        /// or another player's object); Abandoned when the request is withdrawn
        /// (ReleaseControl, the entity disabled or unbound). This is the way to take a leased
        /// object; SharedObject.Acquire is the manual lease, without control behaviours or renewal.
        /// </summary>
        public ControlRequest AcquireControl()
        {
            var request = new ControlRequest();
            RequestControl();
            if (hasControl)
                request.Complete(RequestStatus.Succeeded, CommandResult.Success);
            else if (!Bound)
                request.Complete(RequestStatus.Abandoned, default);
            else
                control.Await(request);
            return request;
        }
        // Completes a request outside network callbacks (it may send).
        void Settle(ControlRequest request, RequestStatus status, CommandResult result)
        {
            if (World != null)
                World.Defer(() => request.Complete(status, result));
            else
                request.Complete(status, result);
        }
        /// <summary>
        /// Withdraws RequestControl: a held lease is given back on a later world update and
        /// waiting AcquireControl requests complete as Abandoned. Owned objects stay controlled.
        /// </summary>
        public void ReleaseControl()
        {
            control.Release();
            RefreshControl();
        }
        void ServiceControl()
        {
            RefreshControl();
            if (Bound && Client.Ready)
                control.Service();
        }

        // How this client comes to drive the bound object. Owned objects (player,
        // simulated) are driven by their owner; a leased object by whoever holds its lease,
        // which this client acquires, renews and releases as the game asks; server objects
        // by nobody.
        abstract class Control
        {
            public static Control For(NetworkEntity entity, NetworkObject state)
            {
                if (state.Kind.Owned)
                    return new OwnedControl(entity, state);
                if (state.Kind.Leased)
                    return new LeaseControl(entity, state);
                return NoControl.Instance;
            }
            // Wanted once held: an owned object always, a leased one while requested.
            public abstract bool Wants { get; }
            public virtual bool IsRequesting => false;
            public virtual void Request() { }
            // An AcquireControl that this client does not have control for yet: only a
            // lease can be acquired; other objects refuse it.
            public virtual void Await(ControlRequest request) => request.Complete(RequestStatus.Refused, CommandResult.Forbidden);
            // This client gained control (RefreshControl).
            public virtual void Gained() { }
            public virtual void Release() { }
            // Each world Update while bound and the room's state is applied.
            public virtual void Service() { }
            // Unbinding: let go of the object and its notifications.
            public virtual void Detach() { }
        }
        sealed class NoControl : Control
        {
            public static readonly NoControl Instance = new NoControl();
            public override bool Wants => false;
        }
        sealed class OwnedControl : Control
        {
            readonly NetworkEntity entity;
            readonly NetworkObject state;
            public OwnedControl(NetworkEntity entity, NetworkObject state)
            {
                this.entity = entity;
                this.state = state;
                state.AuthorityChanged += entity.OnOwnerChanged;
            }
            public override bool Wants => true;
            public override void Detach() => state.AuthorityChanged -= entity.OnOwnerChanged;
        }
        sealed class LeaseControl : Control
        {
            // What the game wants of the lease (RequestControl / ReleaseControl).
            enum Intent : byte
            {
                None,
                Hold,    // acquire it, and renew it while held (Auto Renew)
                Release, // give it back
            }
            // AcquireControl requests waiting for control.
            readonly System.Collections.Generic.List<ControlRequest> waiting = new System.Collections.Generic.List<ControlRequest>();
            readonly NetworkEntity entity;
            readonly NetworkObject state;
            Intent intent;
            bool awaitingResult;
            uint request;
            float renewAt, retryAt;
            public LeaseControl(NetworkEntity entity, NetworkObject state)
            {
                this.entity = entity;
                this.state = state;
                intent = entity.acquireOnEnable && entity.isActiveAndEnabled ? Intent.Hold : Intent.None;
                state.AuthorityChanged += OnHolderChanged;
                state.CommandCompleted += OnResult;
                ScheduleRenew();
            }
            public override bool Wants => intent == Intent.Hold;
            public override bool IsRequesting => awaitingResult;
            public override void Request()
            {
                intent = Intent.Hold;
                retryAt = 0;
            }
            public override void Release()
            {
                intent = Intent.Release;
                retryAt = 0;
                Settle(RequestStatus.Abandoned, default);
            }
            public override void Await(ControlRequest request) => waiting.Add(request);
            public override void Gained() => Settle(RequestStatus.Succeeded, CommandResult.Success);
            void Settle(RequestStatus status, CommandResult result)
            {
                foreach (var request in waiting)
                    entity.Settle(request, status, result);
                waiting.Clear();
            }
            public override void Service()
            {
                if (!entity.isActiveAndEnabled)
                {
                    intent = Intent.Release; // a disabled entity gives its lease back
                    Settle(RequestStatus.Abandoned, default);
                }
                if (awaitingResult || Time.unscaledTime < retryAt)
                    return;
                if (!NextAction(entity.HeldHere, out var action))
                    return;
                request = entity.Client.Lease(state.Id, action);
                retryAt = Time.unscaledTime + .25f;
                if (request != 0)
                {
                    awaitingResult = true;
                    if (action == LeaseAction.Release)
                        intent = Intent.None;
                    ScheduleRenew();
                }
            }
            public override void Detach()
            {
                if (entity.LeaseHeldHere)
                    entity.Client.Lease(state.Id, LeaseAction.Release);
                state.AuthorityChanged -= OnHolderChanged;
                state.CommandCompleted -= OnResult;
                Settle(RequestStatus.Abandoned, default);
            }
            // The lease request the intent calls for, given whether the lease is held.
            bool NextAction(bool held, out LeaseAction action)
            {
                bool renewDue = entity.autoRenew && Time.unscaledTime >= renewAt;
                switch (intent)
                {
                    case Intent.Release when held:
                        action = LeaseAction.Release;
                        return true;
                    case Intent.Hold when !held:
                        action = LeaseAction.Acquire;
                        return true;
                    case Intent.Hold when renewDue:
                        action = LeaseAction.Renew;
                        return true;
                    default:
                        action = default;
                        return false;
                }
            }
            void ScheduleRenew()
            {
                renewAt = Time.unscaledTime + Mathf.Max(.05f, Mathf.Min(2f, state.LeaseMilliseconds / 3000f));
            }
            // The lease changed hands (acquired, released, expired or taken).
            void OnHolderChanged()
            {
                bool lostIt = entity.hasControl && !state.IsMine;
                if (lostIt && intent == Intent.Hold)
                    intent = Intent.None;
                entity.RefreshControl();
                ScheduleRenew();
            }
            void OnResult(uint requestId, CommandResult result)
            {
                if (requestId != request)
                    return;
                awaitingResult = false;
                entity.LastResult = result;
                bool accepted = result == CommandResult.Success || result == CommandResult.AlreadyApplied;
                if (!accepted && intent == Intent.Hold)
                {
                    intent = Intent.None;
                    Settle(RequestStatus.Refused, result);
                }
                ScheduleRenew(); // a renewal updates the remaining lease
                entity.commandCompleted.Invoke((int)result);
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
            if (observables.Length == 0 || !WritableHere)
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
            // Calls or a despawn taken before the room confirmed this Instantiate that the
            // outbox refused then.
            if (Bound && (pendingRpcs.Count > 0 || destroying))
                SendQueued();
            ServiceControl();
            foreach (var tick in ticks)
                tick.NetworkTick();
        }
        // Control follows the component's state at once (an unconfirmed Instantiate is not
        // serviced by the world until the room confirms it).
        void OnEnable()
        {
            if (acquireOnEnable)
                RequestControl();
            else
                RefreshControl();
        }
        void OnDisable()
        {
            ReleaseControl();
        }

        /// <summary>
        /// Detaches from the room's object: releases a held lease, drops queued calls,
        /// disables the controlled behaviours and unbinds the INetworkBinding components.
        /// </summary>
        public void Unbind()
        {
            control.Detach();
            control = NoControl.Instance;
            if (Object != null)
            {
                Object.Changed -= PublishProperties;
                Object.Changed -= ReadObservables;
            }
            observables = Array.Empty<INetworkObservable>();
            propertyFields = Array.Empty<NetworkSchema.Field>();
            rpcById = null;
            rpcByName = null;
            pendingRpcs.Clear();
            destroying = despawnSent = false;
            Client = null;
            Object = null;
            World = null;
            phase = EntityPhase.Unbound;
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
    /// <summary>
    /// The outcome of NetworkEntity.AcquireControl: completes once this client has control,
    /// or with the reason it will not (see AcquireControl). Wait for it in a coroutine
    /// (yield return request) or with OnCompleted; callbacks run outside network callbacks,
    /// so they may send.
    /// </summary>
    public sealed class ControlRequest : NetworkRequest<ControlRequest, RequestStatus>
    {
        internal ControlRequest() : base(0)
        {
        }
        /// <summary>The room's answer to the lease request (when Refused: the reason, e.g. LeaseBusy).</summary>
        public CommandResult Result { get; private set; }
        internal void Complete(RequestStatus status, CommandResult result)
        {
            Finish(status, () => Result = result);
        }
    }
}
#endif
