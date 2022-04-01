using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionTask", menuName = "Game Server Services/Gs2Mission/MissionTask")]
    public class MissionTask : UnityEngine.ScriptableObject
    {
        public MissionGroup missionGroup;
        public string missionTaskName;
    }
}