#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Dictionary.ScriptableObject
{
    [CreateAssetMenu(fileName = "Entry", menuName = "Game Server Services/Gs2Dictionary/Entry")]
    public class Entry : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string entryName;
        
#if UNITY_INCLUDE_TESTS
        public static Entry Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Entry>(assetPath));
        }
#endif

        public static Entry New(
            Namespace @namespace,
            string entryName
        )
        {
            var instance = CreateInstance<Entry>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.entryName = entryName;
            return instance;
        }

        public Entry Clone()
        {
            var instance = CreateInstance<Entry>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.entryName = entryName;
            return instance;
        }
    }
}