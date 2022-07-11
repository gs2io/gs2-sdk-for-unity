#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Exchange.ScriptableObject
{
    [CreateAssetMenu(fileName = "Rate", menuName = "Game Server Services/Gs2Exchange/Rate")]
    public class Rate : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string rateName;
        
#if UNITY_INCLUDE_TESTS
        public static Rate Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Rate>(assetPath));
        }
        
        public static Rate New(
            Namespace Namespace,
            string rateName
        )
        {
            var instance = CreateInstance<Rate>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.rateName = rateName;
            return instance;
        }
#endif
    }
}