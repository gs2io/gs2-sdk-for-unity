#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Money.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Money/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
        
#if UNITY_INCLUDE_TESTS
        public static Namespace Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Namespace>(assetPath));
        }
        
        public static Namespace New(
            string namespaceName
        )
        {
            var instance = CreateInstance<Namespace>();
            instance.name = "Runtime";
            instance.namespaceName = namespaceName;
            return instance;
        }
#endif
    }
}