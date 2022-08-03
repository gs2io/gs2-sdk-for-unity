#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Limit.ScriptableObject
{
    [CreateAssetMenu(fileName = "Limit", menuName = "Game Server Services/Gs2Limit/Limit")]
    public class Limit : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string limitName;
        
#if UNITY_INCLUDE_TESTS
        public static Limit Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Limit>(assetPath));
        }
#endif
        
        public static Limit New(
            Namespace Namespace,
            string limitName
        )
        {
            var instance = CreateInstance<Limit>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.limitName = limitName;
            return instance;
        }

        public Limit Clone()
        {
            var instance = CreateInstance<Limit>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.limitName = limitName;
            return instance;
        }
    }
}