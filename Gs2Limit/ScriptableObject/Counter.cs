#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Limit.ScriptableObject
{
    [CreateAssetMenu(fileName = "Counter", menuName = "Game Server Services/Gs2Limit/Counter")]
    public class Counter : UnityEngine.ScriptableObject
    {
        public Limit limit;
        public string counterName;
        
#if UNITY_INCLUDE_TESTS
        public static Limit Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Limit>(assetPath));
        }
#endif
        
        public static Counter New(
            Limit limit,
            string counterName
        )
        {
            var instance = CreateInstance<Counter>();
            instance.name = "Runtime";
            instance.limit = limit;
            instance.counterName = counterName;
            return instance;
        }

        public Counter Clone()
        {
            var instance = CreateInstance<Counter>();
            instance.name = "Runtime";
            instance.limit = limit;
            instance.counterName = counterName;
            return instance;
        }
    }
}