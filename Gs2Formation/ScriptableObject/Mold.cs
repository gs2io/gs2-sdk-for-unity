#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    [CreateAssetMenu(fileName = "Mold", menuName = "Game Server Services/Gs2Formation/Mold")]
    public class Mold : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string moldName;
        
#if UNITY_INCLUDE_TESTS
        public static Mold Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Mold>(assetPath));
        }
        
        public static Mold New(
            Namespace Namespace,
            string moldName
        )
        {
            var instance = CreateInstance<Mold>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.moldName = moldName;
            return instance;
        }
#endif
    }
}