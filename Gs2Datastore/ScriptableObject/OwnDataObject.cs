using UnityEngine;

namespace Gs2.Unity.Gs2Datastore.ScriptableObject
{
    public class OwnDataObject : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string dataObjectName;

        public static OwnDataObject New(
            Namespace @namespace,
            string dataObjectName
        )
        {
            var instance = CreateInstance<OwnDataObject>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.dataObjectName = dataObjectName;
            return instance;
        }

        public OwnDataObject Clone()
        {
            var instance = CreateInstance<OwnDataObject>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.dataObjectName = dataObjectName;
            return instance;
        }
    }
}