using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionCounter", menuName = "Game Server Services/Gs2Mission/MissionCounter")]
    public class MissionCounter : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string missionCounterName;
    }
}