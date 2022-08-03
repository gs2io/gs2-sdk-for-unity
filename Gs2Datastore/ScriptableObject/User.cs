using UnityEngine;

namespace Gs2.Unity.Gs2Datastore.ScriptableObject
{
    public class User : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;

        public static User New(
            Namespace @namespace,
            string userId
        )
        {
            var instance = CreateInstance<User>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.userId = userId;
            return instance;
        }

        public User Clone()
        {
            var instance = CreateInstance<User>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            return instance;
        }
    }
}