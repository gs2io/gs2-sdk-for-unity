using UnityEngine;

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    public class ReceiveFriendRequest : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;
        
        public static ReceiveFriendRequest New(
            Namespace @namespace,
            string userId
        )
        {
            var instance = CreateInstance<ReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.userId = userId;
            return instance;
        }

        public ReceiveFriendRequest Clone()
        {
            var instance = CreateInstance<ReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}