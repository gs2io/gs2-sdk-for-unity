using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionGroup", menuName = "Game Server Services/Gs2Mission/MissionGroup")]
    public class MissionGroup : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string missionGroupName;
    }
}