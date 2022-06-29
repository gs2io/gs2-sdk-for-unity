using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Key.ScriptableObject
{
    [CreateAssetMenu(fileName = "Key", menuName = "Game Server Services/Gs2Key/Key")]
    public class Key : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string keyName;

        public string Grn => $"grn:gs2:{{region}}:{{ownerId}}:key:{Namespace.namespaceName}:key:{keyName}";
        
        public static Key Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Key>(assetPath));
        }
        
        public static Key New(
            Namespace Namespace,
            string keyName
        )
        {
            var instance = CreateInstance<Key>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.keyName = keyName;
            return instance;
        }
    }
}