#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
namespace Gs2.Realtime.Sync.Editor
{
    // Builds the player-object entries of the room's object catalog from a prefab catalog,
    // so field masks and RPC IDs never have to be copied by hand. Shared (leased/server)
    // prefabs are defined by the room rules and are left out.
    public static class NetworkCatalogExport
    {
        public static string ToJson(NetworkPrefabCatalog catalog)
        {
            if (catalog == null)
                throw new ArgumentNullException(nameof(catalog));
            var entries = new SortedDictionary<uint, string>();
            foreach (var entry in catalog.Prefabs)
            {
                if (entry == null || entry.Prefab == null || entry.Id == 0)
                    throw new InvalidOperationException("Catalog entry without a prefab or ID");
                var prefab = entry.Prefab;
                if (prefab.GetComponent<SharedObject>() != null)
                    continue;
                var entity = prefab.GetComponent<NetworkEntity>();
                uint fields = Fields(prefab, entity);
                if (fields == 0)
                    throw new InvalidOperationException(prefab.name + " synchronizes no fields; add a NetworkTransform or assign a schema");
                var rpcs = entity.DescribeRpcs();
                var json = new StringBuilder();
                json.Append("{\"fields\":").Append(fields).Append(",\"mode\":\"player\"");
                var owner = rpcs.Where(r => !r.AnyCaller).Select(r => r.Id).ToArray();
                var anyone = rpcs.Where(r => r.AnyCaller).Select(r => r.Id).ToArray();
                if (owner.Length > 0)
                    json.Append(",\"methods\":[").Append(string.Join(",", owner)).Append(']');
                if (anyone.Length > 0)
                    json.Append(",\"publicMethods\":[").Append(string.Join(",", anyone)).Append(']');
                json.Append('}');
                entries[entry.Id] = json.ToString();
            }
            return "{" + string.Join(",", entries.Select(e => "\"" + e.Key + "\":" + e.Value)) + "}";
        }

        // The schema's mask, or the standard transform fields the NetworkTransform selects.
        static uint Fields(GameObject prefab, NetworkEntity entity)
        {
            if (entity != null && entity.Schema != null)
                return entity.Schema.Validate();
            var motion = prefab.GetComponent<NetworkTransform>();
            if (entity == null)
                throw new InvalidOperationException(prefab.name + " needs a NetworkEntity on its root");
            if (motion == null)
                return 0;
            uint mask = 0;
            for (int axis = 0; axis < 3; axis++)
            {
                if (((int)motion.PositionAxes & (1 << axis)) != 0)
                    mask |= 1u << axis;
                if (((int)motion.RotationAxes & (1 << axis)) != 0)
                    mask |= 1u << (axis + 3);
            }
            return mask;
        }

        [MenuItem("CONTEXT/NetworkPrefabCatalog/Copy Object Catalog JSON")]
        static void Copy(MenuCommand command)
        {
            var json = ToJson((NetworkPrefabCatalog)command.context);
            EditorGUIUtility.systemCopyBuffer = json;
            Debug.Log("Copied object catalog for the namespace's syncSetting.objectCatalog:\n" + json);
        }
    }
}
#endif
