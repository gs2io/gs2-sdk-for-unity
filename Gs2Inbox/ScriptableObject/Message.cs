using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Inbox.ScriptableObject
{
    public class Message : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string messageName;
        
        public static Namespace Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Namespace>(assetPath));
        }
        
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
    }
}