#if UNITY_EDITOR && UNITY_2021_3_OR_NEWER
using UnityEditor;
namespace Gs2.Realtime.Sync.Editor
{
    [CustomEditor(typeof(NetworkTransform))]
    [CanEditMultipleObjects]
    public sealed class NetworkTransformEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "positionAxes", "rotationAxes");
            foreach (var name in new[] { "positionAxes", "rotationAxes" })
            {
                var property = serializedObject.FindProperty(name);
                EditorGUI.showMixedValue = property.hasMultipleDifferentValues;
                EditorGUI.BeginChangeCheck();
                var value = (SyncAxes)EditorGUILayout.EnumFlagsField(property.displayName, (SyncAxes)property.intValue);
                if (EditorGUI.EndChangeCheck())
                    property.intValue = (int)value & 7;
            }
            EditorGUI.showMixedValue = false;
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.HelpBox("Position and rotation use the SDK standard fields. An optional schema can override precision. Only server-enabled axes may be synchronized; unselected axes remain local.", MessageType.Info);
        }
    }
}
#endif
