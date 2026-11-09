#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// A reusable list of prefab entries; assign it to a NetworkWorld's prefab catalog.
    /// </summary>
    [CreateAssetMenu(menuName = "GS2/Realtime/Prefab Catalog")]
    public sealed class NetworkPrefabCatalog : ScriptableObject
    {
        [SerializeField] NetworkWorld.PrefabEntry[] prefabs = Array.Empty<NetworkWorld.PrefabEntry>();
        /// <summary>The prefab entries (get returns a copy).</summary>
        public NetworkWorld.PrefabEntry[] Prefabs
        {
            get => (NetworkWorld.PrefabEntry[])prefabs.Clone();
            set => prefabs = value == null ? Array.Empty<NetworkWorld.PrefabEntry>() : (NetworkWorld.PrefabEntry[])value.Clone();
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            foreach (var problem in NetworkWorld.Problems(prefabs))
                Debug.LogWarning(name + ": " + problem, this);
        }
#endif
        internal void AddTo(Dictionary<uint, GameObject> result)
        {
            foreach (var p in prefabs)
                NetworkWorld.AddPrefab(result, p);
        }
    }
}
#endif
