using UnityEngine;

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    public class Friend : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;
        
        public static Friend New(
            Namespace @namespace,
            string userId
        )
        {
            var instance = CreateInstance<Friend>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.userId = userId;
            return instance;
        }

        public Friend Clone()
        {
            var instance = CreateInstance<Friend>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}