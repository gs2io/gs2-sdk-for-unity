using UnityEngine;

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    public class SendFriendRequest : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;
        
        public static SendFriendRequest New(
            Namespace @namespace,
            string userId
        )
        {
            var instance = CreateInstance<SendFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.userId = userId;
            return instance;
        }

        public SendFriendRequest Clone()
        {
            var instance = CreateInstance<SendFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}