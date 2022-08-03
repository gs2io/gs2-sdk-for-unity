#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Inbox.ScriptableObject
{
    public class Message : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string messageName;
        
#if UNITY_INCLUDE_TESTS
        public static Namespace Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Namespace>(assetPath));
        }
#endif
        
        public static Message New(
            Namespace Namespace,
            string messageName
        )
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.messageName = messageName;
            return instance;
        }

        public Message Clone()
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.messageName = messageName;
            return instance;
        }
    }
}