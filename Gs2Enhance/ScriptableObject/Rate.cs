#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    [CreateAssetMenu(fileName = "Rate", menuName = "Game Server Services/Gs2Enhance/Rate")]
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
#endif
        
        public static Rate New(
            Namespace @namespace,
            string rateName
        )
        {
            var instance = CreateInstance<Rate>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.rateName = rateName;
            return instance;
        }

        public Rate Clone()
        {
            var instance = CreateInstance<Rate>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.rateName = rateName;
            return instance;
        }
    }
}