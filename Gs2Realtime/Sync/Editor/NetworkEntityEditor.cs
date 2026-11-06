#if UNITY_2021_3_OR_NEWER
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace Gs2.Realtime.Sync.Editor
{
    [CustomEditor(typeof(NetworkEntity))]
    public sealed class NetworkEntityEditor : UnityEditor.Editor
    {
        // NetworkPropertyBinding keeps one UnityEvent per field type.
        static readonly string[] EventProperties = { "intChanged", "boolChanged", "floatChanged" };
        static string EventProperty(NetworkValueType type)
        {
            return type == NetworkValueType.Int ? "intChanged" : type == NetworkValueType.Bool ? "boolChanged" : "floatChanged";
        }
        // A binding switched to another field must not keep listeners for the old value type.
        static void ClearEvents(SerializedProperty entry)
        {
            foreach (var key in EventProperties)
                entry.FindPropertyRelative(key).FindPropertyRelative("m_PersistentCalls.m_Calls").ClearArray();
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "m_Script", "propertyBindings");
            var schema = serializedObject.FindProperty("schema").objectReferenceValue as NetworkSchema;
            var entries = serializedObject.FindProperty("propertyBindings");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Property bindings", EditorStyles.boldLabel);
            if (schema == null)
                EditorGUILayout.HelpBox("Position and rotation use built-in fields. Assign a generated schema only for custom fields or precision.", MessageType.Info);
            for (int i = 0; i < entries.arraySize; i++)
            {
                var entry = entries.GetArrayElementAtIndex(i);
                var name = entry.FindPropertyRelative("field");
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    var names = NetworkSchema.FieldsOf(schema).Select(f => f.Name).ToArray();
                    var index = System.Array.IndexOf(names, name.stringValue);
                    var selected = EditorGUILayout.Popup("Field", index + 1, new[] { "(Select field)" }.Concat(names).ToArray()) - 1;
                    if (selected != index)
                    {
                        name.stringValue = selected < 0 ? "" : names[selected];
                        ClearEvents(entry);
                    }
                }
                var field = NetworkSchema.Resolve(schema, name.stringValue);
                if (field != null)
                    EditorGUILayout.PropertyField(entry.FindPropertyRelative(EventProperty(field.Type)), new GUIContent("Apply to"), true);
                else
                    EditorGUILayout.HelpBox("Select a field from the schema.", MessageType.Warning);
                if (GUILayout.Button("Remove binding"))
                {
                    entries.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndVertical();
                    break;
                }
                EditorGUILayout.EndVertical();
            }
            if (GUILayout.Button("Add binding"))
            {
                entries.InsertArrayElementAtIndex(entries.arraySize);
                var entry = entries.GetArrayElementAtIndex(entries.arraySize - 1);
                entry.FindPropertyRelative("field").stringValue = "";
                ClearEvents(entry);
            }
            serializedObject.ApplyModifiedProperties();
            DrawRpcs((NetworkEntity)target);
        }
        // Lists [NetworkRpc] methods so their IDs can be copied into the room's object catalog.
        static void DrawRpcs(NetworkEntity entity)
        {
            NetworkRpcDescription[] rpcs;
            try
            {
                rpcs = entity.DescribeRpcs();
            }
            catch (System.InvalidOperationException e)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Error);
                return;
            }
            if (rpcs.Length == 0)
                return;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Remote procedure calls", EditorStyles.boldLabel);
            foreach (var rpc in rpcs)
            {
                var parameters = string.Join(", ", rpc.Parameters.Select(p => p.Name));
                EditorGUILayout.LabelField(rpc.Id + (rpc.AnyCaller ? "  (any caller)  " : "  ") + rpc.Component.Name + "." + rpc.Name + "(" + parameters + ")");
            }
            EditorGUILayout.HelpBox("The room's object catalog must list these IDs (\"methods\", or \"publicMethods\" for any caller). Use the prefab catalog's context menu \"Copy Object Catalog JSON\".", MessageType.Info);
        }
    }
}
#endif
