#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    [CreateAssetMenu(fileName = "PropertyForm", menuName = "Game Server Services/Gs2Formation/PropertyForm")]
    public class PropertyForm : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string formName;
        
#if UNITY_INCLUDE_TESTS
        public static PropertyForm Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<PropertyForm>(assetPath));
        }
        
        public static PropertyForm New(
            Namespace Namespace,
            string formName
        )
        {
            var instance = CreateInstance<PropertyForm>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.formName = formName;
            return instance;
        }
#endif
    }
}