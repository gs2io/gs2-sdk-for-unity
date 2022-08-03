using UnityEngine;

namespace Gs2.Unity.Gs2Experience.ScriptableObject
{
    [CreateAssetMenu(fileName = "Status", menuName = "Game Server Services/Gs2Experience/Status")]
    public class Status : UnityEngine.ScriptableObject
    {
        public Experience experience;
        public string propertyId;
        
        public static Status New(
            Experience experience,
            string propertyId
        )
        {
            var instance = CreateInstance<Status>();
            instance.name = "Runtime";
            instance.experience = experience;
            instance.propertyId = propertyId;
            return instance;
        }

        public Status Clone()
        {
            var instance = CreateInstance<Status>();
            instance.name = "Runtime";
            instance.experience = experience;
            instance.propertyId = propertyId;
            return instance;
        }
    }
}