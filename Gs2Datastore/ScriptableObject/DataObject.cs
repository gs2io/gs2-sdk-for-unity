using UnityEngine;

namespace Gs2.Unity.Gs2Datastore.ScriptableObject
{
    public class DataObject : UnityEngine.ScriptableObject
    {
        public User user;
        public string dataObjectName;

        public static DataObject New(
            User user,
            string dataObjectName
        )
        {
            var instance = CreateInstance<DataObject>();
            instance.name = "Runtime";
            instance.user = user;
            instance.dataObjectName = dataObjectName;
            return instance;
        }

        public DataObject Clone()
        {
            var instance = CreateInstance<DataObject>();
            instance.name = "Runtime";
            instance.user = user;
            instance.dataObjectName = dataObjectName;
            return instance;
        }
    }
}