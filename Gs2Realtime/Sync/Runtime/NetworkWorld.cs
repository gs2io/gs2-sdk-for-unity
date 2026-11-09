#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// Presents a synchronized room in the scene: instantiates the registered prefabs for the
    /// room's objects, sends batched updates in LateUpdate and runs RPCs and room callbacks.
    /// User callbacks run outside network callbacks, so they may send.
    /// </summary>
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(100)]
    public sealed partial class NetworkWorld : MonoBehaviour, ISyncPresenter
    {
        /// <summary>A network prefab and the prefab ID the room uses for it.</summary>
        [Serializable]
        public sealed class PrefabEntry
        {
            public uint Id = 1;
            public GameObject Prefab;
            /// <summary>True when the entry has a prefab ID other than 0 and a prefab.</summary>
            public bool IsComplete => Id != 0 && Prefab != null;
            // The NetworkEntity on the prefab's root, or null.
            internal NetworkEntity Entity
            {
                get
                {
                    var entity = Prefab != null ? Prefab.GetComponent<NetworkEntity>() : null;
                    return entity != null ? entity : null; // turns Unity's fake null into null
                }
            }
            // The prefab's schema was generated for another prefab ID.
            internal bool UsesSchemaOfAnotherPrefab
            {
                get
                {
                    var schema = Entity?.Schema;
                    return schema != null && schema.Prefab != Id;
                }
            }
        }
        [SerializeField] NetworkPrefabCatalog prefabCatalog;
        [SerializeField] PrefabEntry[] prefabs;
        [SerializeField, Range(1, 60)] int sendRate = 20;
        readonly Dictionary<uint, GameObject> catalog = new Dictionary<uint, GameObject>();
        readonly Dictionary<uint, NetworkEntity> instances = new Dictionary<uint, NetworkEntity>();
        // This connection's spawn requests waiting for the room, by request ID, with the
        // instance an Instantiate shows meanwhile. Predicted spawns keep theirs in
        // shownSpawns.
        sealed class PendingSpawn
        {
            public SpawnRequest Request;
            public NetworkEntity Shown; // null for Spawn
        }
        readonly Dictionary<uint, PendingSpawn> spawns = new Dictionary<uint, PendingSpawn>();
        // This connection's shared-object requests waiting for the room, by request ID. They
        // outlive their objects: an interaction that removes its object (picking up an
        // item) is answered after the despawn.
        readonly Dictionary<uint, CommandRequest> commandRequests = new Dictionary<uint, CommandRequest>();
        // User code (RPC methods, spawn callbacks) runs after the packet is applied, outside
        // SyncClient.Receive, so it may send commands and set properties directly.
        readonly List<Action> deferred = new List<Action>();
        readonly List<Action> running = new List<Action>();
        float nextFlush, nextRefresh;
        /// <summary>
        /// Optional unreliable sender for the periodic state updates (for example
        /// RealtimeUdpChannel.TrySend while it is Active); null keeps everything reliable.
        /// Requires an attached world.
        /// </summary>
        public TrySend Unreliable
        {
            get => Client?.Unreliable;
            set
            {
                if (Client == null)
                    throw new InvalidOperationException("Attach first");
                Client.Unreliable = value;
            }
        }
        // Container.lifeTimeMilliSeconds stamps every message with the room server's
        // uptime. serverOffset = server - local, tracking the least-delayed sample.
        double serverOffset;
        bool hasServerTime;
        static double LocalSeconds => Time.realtimeSinceStartupAsDouble;
        /// <summary>
        /// Estimated room server time in seconds, like PhotonNetwork.Time. Shared by all
        /// members of the room; 0 until HasServerTime (after a timestamped Receive).
        /// </summary>
        public double ServerTime => hasServerTime ? LocalSeconds + serverOffset : 0;
        /// <summary>True once a Receive with a server timestamp has set ServerTime.</summary>
        public bool HasServerTime => hasServerTime;
        /// <summary>
        /// Server time in seconds of the packet being applied; NaN outside Receive or
        /// without a timestamp.
        /// </summary>
        public double ReceivedServerTime
        {
            get; private set;
        } = double.NaN;
        /// <summary>The underlying sync client while attached; null after Detach.</summary>
        public SyncClient Client
        {
            get; private set;
        }
        /// <summary>
        /// Shared prefab catalog, combined with this world's own entries at Attach. Change it
        /// only while detached.
        /// </summary>
        public NetworkPrefabCatalog Catalog
        {
            get => prefabCatalog;
            set
            {
                if (Client != null)
                    throw new InvalidOperationException("Detach before changing catalog");
                prefabCatalog = value;
            }
        }
        /// <summary>Number of room objects currently shown by this world.</summary>
        public int InstanceCount => instances.Count;
        /// <summary>Batched sends per second (1-60), like PhotonNetwork.SendRate.</summary>
        public int SendRate
        {
            get => sendRate;
            set => sendRate = Mathf.Clamp(value, 1, 60);
        }
        /// <summary>Raised when an instance is bound to a room object, own or remote.</summary>
        public event Action<NetworkEntity> Spawned;
        /// <summary>Raised when a room object is removed (or on Detach), before destruction.</summary>
        public event Action<NetworkEntity> Despawned;
        /// <summary>
        /// An Instantiate the room refused (e.g. capacity), with the room's reason; the instance
        /// is destroyed after the handlers run.
        /// </summary>
        public event Action<NetworkEntity, CommandResult> SpawnFailed;
        /// <summary>The instance of a room object ID, or null if none is shown.</summary>
        public NetworkEntity Find(uint id)
        {
            instances.TryGetValue(id, out var entity);
            return entity;
        }
        /// <summary>
        /// Describes what is wrong with prefab entries: IDs that are 0 or repeated, missing
        /// prefabs or NetworkEntity components, and schemas generated for another prefab ID.
        /// Empty when all entries are usable.
        /// </summary>
        public static List<string> Problems(IEnumerable<PrefabEntry> entries)
        {
            var problems = new List<string>();
            var seen = new HashSet<uint>();
            foreach (var entry in entries)
            {
                if (entry == null)
                    continue;
                if (entry.Id == 0)
                    problems.Add("an entry has prefab ID 0");
                else if (!seen.Add(entry.Id))
                    problems.Add("prefab ID " + entry.Id + " is used more than once");
                if (entry.Prefab == null)
                {
                    problems.Add("prefab ID " + entry.Id + " has no prefab");
                    continue;
                }
                var entity = entry.Entity;
                if (entity == null)
                    problems.Add(entry.Prefab.name + " needs a NetworkEntity on its root");
                else if (entry.UsesSchemaOfAnotherPrefab)
                    problems.Add(entry.Prefab.name + " (ID " + entry.Id + ") uses schema " + entity.Schema.name + ", generated for prefab " + entity.Schema.Prefab);
            }
            return problems;
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            var all = new List<PrefabEntry>();
            if (prefabCatalog != null)
                all.AddRange(prefabCatalog.Prefabs);
            if (prefabs != null)
                all.AddRange(prefabs);
            foreach (var problem in Problems(all))
                Debug.LogWarning(name + ": " + problem, this);
        }
#endif
        internal static void AddPrefab(Dictionary<uint, GameObject> result, PrefabEntry entry)
        {
            if (entry == null || !entry.IsComplete || result.ContainsKey(entry.Id))
                throw new ArgumentException("Invalid or duplicate prefab ID");
            if (entry.Entity == null)
                throw new ArgumentException("Network prefab " + entry.Prefab.name + " needs a NetworkEntity on its root");
            result.Add(entry.Id, entry.Prefab);
        }
        /// <summary>
        /// Connects the world to a room session. Call after HelloResult and before passing
        /// buffered snapshot messages to Receive. Detaches from any previous session first.
        /// </summary>
        /// <param name="connectionId">This client's connection ID from HelloResult.</param>
        /// <param name="send">Reliable sender for outgoing packets.</param>
        public void Attach(uint connectionId, TrySend send)
        {
            Detach();
            catalog.Clear();
            if (prefabCatalog != null)
                prefabCatalog.AddTo(catalog);
            if (prefabs != null)
                foreach (var p in prefabs)
                    AddPrefab(catalog, p);
            Client = new SyncClient(connectionId, send);
            Client.Presenter = this;
            ResetClock();
            AttachRoom();
            nextFlush = Time.unscaledTime;
            hasServerTime = false;
        }
        /// <summary>Applies one received room message. Requires an attached world.</summary>
        public void Receive(byte[] payload)
        {
            Apply(payload, double.NaN);
        }
        /// <summary>
        /// Applies one received room message with its server timestamp. Pass
        /// MessageMetadata.LifeTimeMilliSeconds (RealtimeSession.OnMessageWithMetadata) to
        /// enable ServerTime and timestamped interpolation of remote motion.
        /// </summary>
        public void Receive(byte[] payload, uint serverMilliseconds)
        {
            double server = serverMilliseconds / 1000.0;
            double sample = server - LocalSeconds;
            // A smaller delay shows up as a larger offset; adopt it at once and drift
            // slowly otherwise, so congestion spikes do not move the clock backwards much.
            if (!hasServerTime || sample > serverOffset)
                serverOffset = sample;
            else
                serverOffset += (sample - serverOffset) * .02;
            hasServerTime = true;
            Apply(payload, server);
        }
        void Apply(byte[] payload, double serverTime)
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
            ReceivedServerTime = serverTime;
            try
            {
                Client.Receive(payload, payload.Length);
            }
            catch
            {
                deferred.Clear();
                throw;
            }
            finally
            {
                ReceivedServerTime = double.NaN;
            }
            RunDeferred();
        }
        // Completes a request outside network callbacks: after the packet being received, at
        // the end of this frame's Update, or at the end of Detach. Unlike notifications (which
        // Detach drops), a completion always runs, so whoever waits learns how it ended.
        internal void Defer(Action completion) => completions.Add(completion);
        readonly List<Action> completions = new List<Action>();
        // Runs each batch's notifications, then its completions, until none is left.
        // Re-entered from a callback it returns at once: the outer run takes up what was
        // added, so nothing runs twice. After a callback detaches the world, the rest of its
        // batch's notifications (of the room it left) are dropped; completions still run.
        bool runningDeferred;
        int detachments;
        void RunDeferred()
        {
            if (runningDeferred)
                return;
            runningDeferred = true;
            try
            {
                while (deferred.Count > 0 || completions.Count > 0)
                {
                    int detached = detachments;
                    running.AddRange(deferred);
                    deferred.Clear();
                    int notifications = running.Count;
                    running.AddRange(completions);
                    completions.Clear();
                    for (int i = 0; i < running.Count; i++)
                    {
                        if (i < notifications && detachments != detached)
                            continue;
                        try
                        {
                            running[i]();
                        }
                        catch (Exception e)
                        {
                            Debug.LogException(e, this);
                        }
                    }
                    running.Clear();
                }
            }
            finally
            {
                running.Clear();
                runningDeferred = false;
            }
        }
        void OnRpc(NetworkObject value, uint method, uint sender, byte[] args, int offset, int count)
        {
            if (!instances.TryGetValue(value.Id, out var entity))
                return;
            var call = entity.PrepareRpc(method, sender, args, offset, count);
            if (call != null)
                deferred.Add(call);
        }

        /// <summary>
        /// Requests a player object owned by this connection. The instance appears only once
        /// the room confirms it; use Instantiate to show it at once. Position and rotation fill
        /// the fields the prefab's NetworkTransform synchronizes, which the room must permit.
        /// </summary>
        /// <returns>
        /// A request that completes once: Spawned (with the entity), Rejected (with the room's
        /// reason), NotSent (the transport refused the packet) or Abandoned (the world detached
        /// or the room was rejoined).
        /// </returns>
        public SpawnRequest Spawn(uint prefabId, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefabId, Encode(PrefabOf(prefabId), position, rotation));
        }
        /// <summary>
        /// Like PhotonNetwork.Instantiate: returns the instance at once, owned by this
        /// connection (IsMine), so it can move and send RPCs immediately; those are queued
        /// until the room confirms the object. A refused request raises SpawnFailed and
        /// destroys the instance. Throws if the packet could not be sent.
        /// </summary>
        public NetworkEntity Instantiate(uint prefabId, Vector3 position, Quaternion rotation)
        {
            var prefab = PrefabOf(prefabId);
            var values = Encode(prefab, position, rotation);
            var instance = UnityEngine.Object.Instantiate(prefab, position, rotation, transform);
            var entity = instance.GetComponent<NetworkEntity>();
            entity.BeginPlaceholder();
            var request = Request(prefabId, values, entity);
            if (request.Status == SpawnStatus.NotSent)
            {
                Discard(instance);
                throw new InvalidOperationException("Instantiate not sent: the outbox is full");
            }
            entity.SpawnRequest = request;
            return entity;
        }
        SpawnRequest Request(uint prefabId, SlotValues values, NetworkEntity shown)
        {
            var pending = new PendingSpawn { Shown = shown };
            uint id = Client.Spawn(prefabId, values, (status, spawned, result) => OnSpawnAnswered(pending, status, spawned, result));
            pending.Request = new SpawnRequest(id);
            if (id == 0)
                pending.Request.Complete(SpawnStatus.NotSent);
            else
                spawns[id] = pending;
            return pending.Request;
        }
        // Runs after Spawn bound the room's object. Requests no longer tracked (not sent, or
        // torn down) are ignored.
        void OnSpawnAnswered(PendingSpawn pending, RequestStatus status, NetworkObject spawned, CommandResult result)
        {
            if (pending.Request == null || !spawns.Remove(pending.Request.Id))
                return;
            var entity = pending.Shown;
            switch (status)
            {
                case RequestStatus.Succeeded:
                    instances.TryGetValue(spawned.Id, out var confirmed);
                    Defer(() => pending.Request.Complete(SpawnStatus.Spawned, confirmed));
                    return;
                case RequestStatus.Refused:
                    if (entity != null)
                        Debug.LogWarning("The room refused to spawn " + entity.name + ": " + result, entity);
                    Defer(() =>
                    {
                        pending.Request.Complete(SpawnStatus.Rejected, null, result);
                        if (entity == null)
                            return;
                        SpawnFailed?.Invoke(entity, result);
                        if (entity != null)
                            Discard(entity.gameObject);
                    });
                    return;
                default: // Abandoned: the room rejoined; the placeholder goes
                    if (entity != null)
                        Discard(entity.gameObject);
                    Defer(() => pending.Request.Complete(SpawnStatus.Abandoned));
                    return;
            }
        }
        static SlotValues Encode(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            // Declare every field of the schema: a room without an object catalog keeps
            // the fields chosen at spawn. Fields other than the transform start at 0.
            var values = new SlotValues();
            var schema = prefab.GetComponent<NetworkEntity>().Schema;
            if (schema != null)
            {
                schema.Validate();
                foreach (var field in schema.Fields)
                    values.Set(field.Slot, 0);
            }
            var motion = prefab.GetComponent<NetworkTransform>();
            if (motion != null)
            {
                var positionAxes = motion.PositionAxes;
                var rotationAxes = motion.RotationAxes;
                var euler = rotation.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                {
                    if (positionAxes.Includes(axis))
                        Fill(values, schema, "Position" + "XYZ"[axis], position[axis], false);
                    if (rotationAxes.Includes(axis))
                        Fill(values, schema, "Rotation" + "XYZ"[axis], euler[axis], true);
                }
            }
            return values;
        }
        /// <summary>
        /// Raw form of Spawn: values holds the wire integers of the fields to set.
        /// </summary>
        public SpawnRequest Spawn(uint prefabId, SlotValues values)
        {
            PrefabOf(prefabId);
            return Request(prefabId, values, null);
        }
        // send is a SyncClient call given its callback; the request is tracked until answered.
        internal CommandRequest Command(Func<CommandCallback, uint> send)
        {
            CommandRequest request = null;
            uint id = send((status, result) => OnCommandAnswered(request, status, result));
            request = new CommandRequest(id);
            if (id == 0)
                request.Complete(RequestStatus.NotSent, default);
            else
                commandRequests[id] = request;
            return request;
        }
        void OnCommandAnswered(CommandRequest request, RequestStatus status, CommandResult result)
        {
            if (request == null || !commandRequests.Remove(request.Id))
                return;
            Defer(() => request.Complete(status, result));
        }
        GameObject PrefabOf(uint prefabId)
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
            if (detaching)
                throw new InvalidOperationException("The world is detaching; spawn after attaching again");
            if (!catalog.TryGetValue(prefabId, out var prefab))
                throw new ArgumentException("Unknown prefab ID: " + prefabId);
            return prefab;
        }
        static void Fill(SlotValues values, NetworkSchema schema, string name, float value, bool angle)
        {
            var field = NetworkSchema.Resolve(schema, name);
            if (field == null || field.Type != NetworkValueType.Float)
                throw new ArgumentException("Prefab schema has no float field " + name);
            values.Set(field.Slot, angle ? NetworkTransform.QuantizeAngle(value, field.Precision) : SyncCodec.Quantize(value, field.Precision));
        }
        void ISyncPresenter.Spawned(NetworkObject o) => Spawn(o);
        void ISyncPresenter.Despawned(NetworkObject o) => Despawn(o);
        void ISyncPresenter.Updated(NetworkObject o, Record update, uint state) => OnUpdateReceived(o, update, state);
        void ISyncPresenter.RpcReceived(NetworkObject o, uint method, uint sender, byte[] args, int offset, int count) => OnRpc(o, method, sender, args, offset, count);
        void ISyncPresenter.ClockReceived(uint tick, uint rate, uint latest) => OnClock(tick, rate, latest);
        void Spawn(NetworkObject value)
        {
            predictionDirty = true; // a predicted spawn may hand over to this object
            if (!catalog.TryGetValue(value.Prefab, out var prefab))
                throw new InvalidOperationException("Unknown prefab ID: " + value.Prefab);
            NetworkEntity placeholder = null;
            PendingSpawn pending = null;
            bool predicted = value.HasPredictedSpawnKey;
            bool mine = value.IsMine;
            if (mine)
            {
                // The room's object takes over the instance shown for it: a predicted
                // spawn's (same prefab and key) or a spawn request's.
                if (predicted)
                    placeholder = TakeShownSpawn(value.Prefab, value.Request);
                else if (spawns.TryGetValue(value.Request, out pending))
                    placeholder = pending.Shown;
            }
            // A placeholder the game destroyed before confirmation (a Unity object that
            // compares equal to null): create the object normally and despawn it, so the
            // room does not keep an orphan. Objects the member cannot despawn (simulated,
            // including predicted spawns) belong to the rules.
            bool destroyed = placeholder == null && !ReferenceEquals(placeholder, null);
            bool orphan = destroyed && value.Kind.HolderDespawns;
            var instance = placeholder != null ? placeholder.gameObject : UnityEngine.Object.Instantiate(prefab, transform);
            try
            {
                var entity = instance.GetComponent<NetworkEntity>();
                entity.Bind(this, Client, value);
                instances.Add(value.Id, entity);
                if (orphan)
                    entity.Destroy();
                Spawned?.Invoke(entity);
            }
            catch
            {
                instances.Remove(value.Id);
                Discard(instance);
                throw;
            }
        }
        // Despawned observers run before the entity releases its bindings.
        void Remove(NetworkEntity entity)
        {
            Despawned?.Invoke(entity);
            Discard(entity.gameObject);
        }
        static void Discard(GameObject instance)
        {
            var entity = instance.GetComponent<NetworkEntity>();
            if (entity != null)
                entity.Unbind();
            instance.SetActive(false);
            Destroy(instance);
        }
        void Despawn(NetworkObject value)
        {
            predictionDirty = true;
            if (!instances.TryGetValue(value.Id, out var entity))
                return;
            instances.Remove(value.Id);
            Remove(entity);
        }
        void Update()
        {
            if (Client == null || !Client.Ready)
                return;
            ServiceTick();
            ServicePrediction();
            foreach (var entity in instances.Values)
                entity.Service();
            RunDeferred();
        }
        // All transform writes in Update precede this batched flush.
        void LateUpdate()
        {
            if (Client == null || !Client.Ready || Time.unscaledTime < nextFlush)
                return;
            nextFlush = Time.unscaledTime + 1f / Mathf.Clamp(sendRate, 1, 60);
            foreach (var entity in instances.Values)
                entity.WriteObservables();
            Client.Flush();
            // Values sent unreliably are sent again once per second, also after the
            // unreliable sender was removed (nothing is sent when there are none).
            if (Time.unscaledTime >= nextRefresh)
            {
                nextRefresh = Time.unscaledTime + 1;
                Client.Refresh();
            }
        }
        // Set while Detach tears the world down: observers it notifies (Despawned,
        // completed SpawnRequests) cannot start requests nothing would answer.
        bool detaching;
        /// <summary>
        /// Disconnects from the room session: destroys every instance (raising Despawned) and
        /// completes pending requests as Abandoned. Safe to call when not attached.
        /// </summary>
        public void Detach()
        {
            if (detaching)
                return;
            detaching = true;
            try
            {
                Teardown();
            }
            finally
            {
                detaching = false;
            }
        }
        void Teardown()
        {
            detachments++;
            if (Client != null)
            {
                Client.Presenter = null;
            }
            ResetClock();
            DetachRoom();
            var unanswered = new List<SpawnRequest>();
            foreach (var pending in spawns.Values)
            {
                unanswered.Add(pending.Request);
                if (pending.Shown != null)
                    Discard(pending.Shown.gameObject);
            }
            spawns.Clear();
            deferred.Clear();
            var unansweredCommands = new List<CommandRequest>(commandRequests.Values);
            commandRequests.Clear();
            Client?.AbandonRequests();
            foreach (var entity in instances.Values)
            {
                if (entity != null)
                    Remove(entity);
            }
            instances.Clear();
            Client = null;
            // Last, once detached: a callback that spawns again finds the world detached
            // (Spawn throws) instead of registering a request nothing would answer.
            foreach (var request in unanswered)
            {
                try
                {
                    request.Complete(SpawnStatus.Abandoned);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            foreach (var request in unansweredCommands)
            {
                try
                {
                    request.Complete(RequestStatus.Abandoned, default);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this);
                }
            }
            // Run completions queued before Detach or by the unbinding entities; drop the
            // detached room's notifications.
            deferred.Clear();
            RunDeferred();
        }
        void OnDestroy()
        {
            Detach();
        }
    }

    /// <summary>The state of a SpawnRequest.</summary>
    public enum SpawnStatus : byte
    {
        /// <summary>Waiting for the room.</summary>
        Pending,
        /// <summary>The room confirmed it; SpawnRequest.Entity is the instance.</summary>
        Spawned,
        /// <summary>The room refused it; SpawnRequest.Rejection gives the reason.</summary>
        Rejected,
        /// <summary>The transport refused the packet; nothing reached the room.</summary>
        NotSent,
        /// <summary>No answer will come: the world detached, or the room was rejoined.</summary>
        Abandoned,
    }
    /// <summary>
    /// The outcome of one NetworkWorld.Spawn or Instantiate. Wait for it in a coroutine
    /// (<c>yield return request</c>) or with OnCompleted; completion callbacks run outside
    /// network callbacks, so they may send.
    /// </summary>
    public sealed class SpawnRequest : NetworkRequest<SpawnRequest, SpawnStatus>
    {
        internal SpawnRequest(uint id) : base(id)
        {
        }
        /// <summary>The confirmed instance (Spawned; null once despawned or destroyed).</summary>
        public NetworkEntity Entity { get; private set; }
        /// <summary>The room's reason when Rejected.</summary>
        public CommandResult Rejection { get; private set; }
        internal void Complete(SpawnStatus status, NetworkEntity entity = null, CommandResult rejection = default)
        {
            Finish(status, () =>
            {
                Entity = entity;
                Rejection = rejection;
            });
        }
    }
}
#endif
