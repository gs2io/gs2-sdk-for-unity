using UnityEngine;

namespace Gs2.Unity.Gs2Experience.ScriptableObject
{
    [CreateAssetMenu(fileName = "Status", menuName = "Game Server Services/Gs2Experience/Status")]
    public class Status : UnityEngine.ScriptableObject
    {
        public Experience experience;
        public string propertyId;
    }
}