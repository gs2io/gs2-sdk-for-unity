#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(100)]
    public sealed partial class NetworkWorld : MonoBehaviour
    {
        [Serializable]
        public sealed class PrefabEntry
        {
            public uint Id = 1;
            public GameObject Prefab;
        }
        [SerializeField] NetworkPrefabCatalog prefabCatalog;
        [SerializeField] PrefabEntry[] prefabs;
        [SerializeField, Range(1, 60)] int sendRate = 20;
        readonly Dictionary<uint, GameObject> catalog = new Dictionary<uint, GameObject>();
        readonly Dictionary<uint, NetworkEntity> instances = new Dictionary<uint, NetworkEntity>();
        // Spawn callbacks keyed by this connection's request ID.
        readonly Dictionary<uint, Action<NetworkEntity>> spawnCallbacks = new Dictionary<uint, Action<NetworkEntity>>();
        // Instances created by Instantiate before the server confirms them.
        readonly Dictionary<uint, NetworkEntity> placeholders = new Dictionary<uint, NetworkEntity>();
        readonly List<(NetworkEntity entity, uint prefab, uint mask, int[] values)> unsentSpawns = new List<(NetworkEntity, uint, uint, int[])>();
        // User code (RPC methods, spawn callbacks) runs after the packet is applied, outside
        // SyncClient.Receive, so it may send commands and set properties directly.
        readonly List<Action> deferred = new List<Action>();
        readonly List<Action> running = new List<Action>();
        float nextFlush, nextRefresh;
        // Optional unreliable sender for the periodic state updates (for example
        // RealtimeUdpChannel.TrySend while it is Active); null keeps everything reliable.
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
        // Server clock: Container.lifeTimeMilliSeconds stamps every message with the
        // room server's uptime. offset = server - local, tracking the least-delayed sample.
        double serverOffset;
        bool hasServerTime;
        static double LocalSeconds => Time.realtimeSinceStartupAsDouble;
        // Estimated room server time in seconds, like PhotonNetwork.Time. Shared by all
        // members of the room; valid once HasServerTime (after a timestamped Receive).
        public double ServerTime => hasServerTime ? LocalSeconds + serverOffset : 0;
        public bool HasServerTime => hasServerTime;
        // Server time of the packet being applied (NaN without a timestamp).
        public double ReceivedServerTime
        {
            get; private set;
        } = double.NaN;
        public SyncClient Client
        {
            get; private set;
        }
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
        public int InstanceCount => instances.Count;
        // Batched sends per second (1-60), like PhotonNetwork.SendRate.
        public int SendRate
        {
            get => sendRate;
            set => sendRate = Mathf.Clamp(value, 1, 60);
        }
        public event Action<NetworkEntity> Spawned;
        public event Action<NetworkEntity> Despawned;
        // An Instantiate the room refused (e.g. capacity); the instance is destroyed afterwards.
        public event Action<NetworkEntity, CommandResult> SpawnFailed;
        public NetworkEntity Find(uint id)
        {
            instances.TryGetValue(id, out var entity);
            return entity;
        }
        internal static void AddPrefab(Dictionary<uint, GameObject> result, PrefabEntry entry)
        {
            if (entry == null || entry.Id == 0 || entry.Prefab == null || result.ContainsKey(entry.Id))
                throw new ArgumentException("Invalid or duplicate prefab ID");
            if (entry.Prefab.GetComponent<NetworkEntity>() == null)
                throw new ArgumentException("Network prefab " + entry.Prefab.name + " needs a NetworkEntity on its root");
            result.Add(entry.Id, entry.Prefab);
        }
        // Attach after HelloResult, before draining buffered snapshot messages.
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
            Client.Spawned += Spawn;
            Client.Despawned += Despawn;
            Client.RpcReceived += OnRpc;
            Client.SpawnRejected += OnSpawnRejected;
            AttachRoom();
            nextFlush = Time.unscaledTime;
            hasServerTime = false;
        }
        public void Receive(byte[] payload)
        {
            Apply(payload, double.NaN);
        }
        // Pass MessageMetadata.LifeTimeMilliSeconds (RealtimeSession.OnMessageWithMetadata)
        // to enable ServerTime and timestamped interpolation of remote motion.
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
        void RunDeferred()
        {
            if (deferred.Count == 0)
                return;
            running.AddRange(deferred);
            deferred.Clear();
            try
            {
                foreach (var action in running)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e, this);
                    }
                }
            }
            finally
            {
                running.Clear();
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

        // Requests a player object owned by this connection, like PhotonNetwork.Instantiate.
        // Position/rotation fill the fields the prefab's NetworkTransform synchronizes (using
        // its schema's precision); the room must permit them for the prefab. The server assigns
        // the object ID, so the instance appears asynchronously: spawned runs with it once the
        // server confirms. Returns the request ID, or 0 when the transport refused the packet.
        public uint Spawn(uint prefabId, Vector3 position, Quaternion rotation, Action<NetworkEntity> spawned = null)
        {
            var values = new int[32];
            uint mask = Encode(PrefabOf(prefabId), position, rotation, values);
            return Spawn(prefabId, mask, values, spawned);
        }
        // Like PhotonNetwork.Instantiate: returns the instance at once, owned by this
        // connection (IsMine), so it can be moved and send RPCs immediately; those are
        // queued until the room confirms the object. Movement before the confirmation is
        // kept. A refused request raises SpawnFailed and destroys the instance.
        public NetworkEntity Instantiate(uint prefabId, Vector3 position, Quaternion rotation)
        {
            var prefab = PrefabOf(prefabId);
            var values = new int[32];
            uint mask = Encode(prefab, position, rotation, values);
            var instance = UnityEngine.Object.Instantiate(prefab, position, rotation, transform);
            var entity = instance.GetComponent<NetworkEntity>();
            entity.BeginPlaceholder();
            unsentSpawns.Add((entity, prefabId, mask, values));
            SendSpawns();
            return entity;
        }
        void SendSpawns()
        {
            while (unsentSpawns.Count > 0 && Client != null && Client.CanSend)
            {
                var (entity, prefab, mask, values) = unsentSpawns[0];
                if (entity == null)
                {
                    unsentSpawns.RemoveAt(0);
                    continue;
                }
                uint request = Client.Spawn(prefab, mask, values);
                if (request == 0)
                    return;
                unsentSpawns.RemoveAt(0);
                placeholders[request] = entity;
            }
        }
        void OnSpawnRejected(uint request, CommandResult result)
        {
            spawnCallbacks.Remove(request);
            if (!placeholders.TryGetValue(request, out var entity))
                return;
            placeholders.Remove(request);
            if (entity == null)
                return;
            Debug.LogWarning("The room refused to spawn " + entity.name + ": " + result, entity);
            deferred.Add(() =>
            {
                SpawnFailed?.Invoke(entity, result);
                if (entity != null)
                    Discard(entity.gameObject);
            });
        }
        uint Encode(GameObject prefab, Vector3 position, Quaternion rotation, int[] values)
        {
            // Declare every field of the schema: a room without an object catalog keeps
            // the fields chosen at spawn. Fields other than the transform start at 0.
            var schema = prefab.GetComponent<NetworkEntity>().Schema;
            uint mask = schema != null ? schema.Validate() : 0;
            var motion = prefab.GetComponent<NetworkTransform>();
            if (motion != null)
            {
                var positionAxes = motion.PositionAxes;
                var rotationAxes = motion.RotationAxes;
                var euler = rotation.eulerAngles;
                for (int axis = 0; axis < 3; axis++)
                {
                    if (((int)positionAxes & (1 << axis)) != 0)
                        mask |= Fill(values, schema, "Position" + "XYZ"[axis], position[axis], false);
                    if (((int)rotationAxes & (1 << axis)) != 0)
                        mask |= Fill(values, schema, "Rotation" + "XYZ"[axis], euler[axis], true);
                }
            }
            return mask;
        }
        // Raw form: values are wire integers for the slots selected by mask.
        public uint Spawn(uint prefabId, uint mask, int[] values, Action<NetworkEntity> spawned = null)
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
            PrefabOf(prefabId);
            if (!Client.CanSend)
                throw new InvalidOperationException("Spawn after the snapshot completes, outside network callbacks");
            uint request = Client.Spawn(prefabId, mask, values);
            if (request != 0 && spawned != null)
                spawnCallbacks[request] = spawned;
            return request;
        }
        GameObject PrefabOf(uint prefabId)
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
            if (!catalog.TryGetValue(prefabId, out var prefab))
                throw new ArgumentException("Unknown prefab ID: " + prefabId);
            return prefab;
        }
        static uint Fill(int[] values, NetworkSchema schema, string name, float value, bool angle)
        {
            var field = NetworkSchema.Resolve(schema, name);
            if (field == null || field.Type != NetworkValueType.Float)
                throw new ArgumentException("Prefab schema has no float field " + name);
            values[field.Slot] = angle ? NetworkTransform.QuantizeAngle(value, field.Precision) : SyncCodec.Quantize(value, field.Precision);
            return 1u << field.Slot;
        }
        void Spawn(NetworkObject value)
        {
            if (!catalog.TryGetValue(value.Prefab, out var prefab))
                throw new InvalidOperationException("Unknown prefab ID: " + value.Prefab);
            NetworkEntity placeholder = null;
            if (value.Owner == Client.ConnectionId && placeholders.TryGetValue(value.Request, out placeholder))
                placeholders.Remove(value.Request);
            // A placeholder destroyed by the game before confirmation: create the object
            // normally and despawn it, so the room does not keep an orphan.
            bool orphan = placeholder == null && value.Owner == Client.ConnectionId && value.Request != 0 && !ReferenceEquals(placeholder, null);
            var instance = placeholder != null ? placeholder.gameObject : UnityEngine.Object.Instantiate(prefab, transform);
            try
            {
                var entity = instance.GetComponent<NetworkEntity>();
                entity.Bind(Client, value);
                instances.Add(value.Id, entity);
                if (orphan)
                    entity.Destroy();
                Spawned?.Invoke(entity);
                if (value.Owner == Client.ConnectionId && spawnCallbacks.TryGetValue(value.Request, out var callback))
                {
                    spawnCallbacks.Remove(value.Request);
                    deferred.Add(() =>
                    {
                        if (entity != null && entity.Object != null)
                            callback(entity);
                    });
                }
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
            if (!instances.TryGetValue(value.Id, out var entity))
                return;
            instances.Remove(value.Id);
            Remove(entity);
        }
        void Update()
        {
            if (Client == null || !Client.CanSend)
                return;
            ServiceRoom();
            SendSpawns();
            foreach (var entity in instances.Values)
                entity.Service();
        }
        // All transform writes in Update precede this batched flush.
        void LateUpdate()
        {
            if (Client == null || !Client.CanSend || Time.unscaledTime < nextFlush)
                return;
            nextFlush = Time.unscaledTime + 1f / Mathf.Clamp(sendRate, 1, 60);
            foreach (var entity in instances.Values)
                entity.WriteObservables();
            Client.Flush();
            // Values sent unreliably are sent again once per second.
            if (Client.Unreliable != null && Time.unscaledTime >= nextRefresh)
            {
                nextRefresh = Time.unscaledTime + 1;
                Client.Refresh();
            }
        }
        public void Detach()
        {
            if (Client != null)
            {
                Client.Spawned -= Spawn;
                Client.Despawned -= Despawn;
                Client.RpcReceived -= OnRpc;
                Client.SpawnRejected -= OnSpawnRejected;
            }
            DetachRoom();
            foreach (var entity in placeholders.Values)
                if (entity != null)
                    Discard(entity.gameObject);
            foreach (var spawn in unsentSpawns)
                if (spawn.entity != null)
                    Discard(spawn.entity.gameObject);
            placeholders.Clear();
            unsentSpawns.Clear();
            spawnCallbacks.Clear();
            deferred.Clear();
            foreach (var entity in instances.Values)
            {
                if (entity != null)
                    Remove(entity);
            }
            instances.Clear();
            Client = null;
        }
        void OnDestroy()
        {
            Detach();
        }
    }
}
#endif
