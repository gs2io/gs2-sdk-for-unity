#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(100)]
    public sealed class NetworkWorld : MonoBehaviour
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
        float nextFlush;
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
        public event Action<NetworkEntity> Spawned;
        public event Action<NetworkEntity> Despawned;
        public NetworkEntity Find(uint id)
        {
            instances.TryGetValue(id, out var entity);
            return entity;
        }
        internal static void AddPrefab(Dictionary<uint, GameObject> result, PrefabEntry entry)
        {
            if (entry == null || entry.Id == 0 || entry.Prefab == null || result.ContainsKey(entry.Id))
                throw new ArgumentException("Invalid or duplicate prefab ID");
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
            nextFlush = Time.unscaledTime;
        }
        public void Receive(byte[] payload)
        {
            if (Client == null)
                throw new InvalidOperationException("Attach first");
            Client.Receive(payload, payload.Length);
        }
        void Spawn(NetworkObject value)
        {
            if (!catalog.TryGetValue(value.Prefab, out var prefab))
                throw new InvalidOperationException("Unknown prefab ID: " + value.Prefab);
            var instance = Instantiate(prefab, transform);
            try
            {
                var entity = instance.GetComponent<NetworkEntity>();
                // Compatibility with the first NetworkWorld's transform-only prefabs.
                if (entity == null)
                {
                    entity = instance.AddComponent<NetworkEntity>();
                    if (instance.GetComponent<NetworkTransform>() == null)
                        instance.AddComponent<NetworkTransform>();
                    if (value.Mode != ObjectMode.Player && instance.GetComponent<SharedObject>() == null)
                        instance.AddComponent<SharedObject>();
                }
                entity.Bind(Client, value);
                instances.Add(value.Id, entity);
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
            if (!instances.TryGetValue(value.Id, out var entity))
                return;
            instances.Remove(value.Id);
            Remove(entity);
        }
        void Update()
        {
            if (Client == null || !Client.CanSend)
                return;
            foreach (var entity in instances.Values)
                entity.Service();
        }
        // All transform writes in Update precede this batched flush.
        void LateUpdate()
        {
            if (Client == null || !Client.CanSend || Time.unscaledTime < nextFlush)
                return;
            nextFlush = Time.unscaledTime + 1f / Mathf.Clamp(sendRate, 1, 60);
            Client.Flush();
        }
        public void Detach()
        {
            if (Client != null)
            {
                Client.Spawned -= Spawn;
                Client.Despawned -= Despawn;
            }
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
