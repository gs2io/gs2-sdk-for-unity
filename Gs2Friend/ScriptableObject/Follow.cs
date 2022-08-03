using UnityEngine;

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    public class Follow : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;
        
        public static Follow New(
            Namespace @namespace,
            string userId
        )
        {
            var instance = CreateInstance<Follow>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.userId = userId;
            return instance;
        }

        public Follow Clone()
        {
            var instance = CreateInstance<Follow>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}