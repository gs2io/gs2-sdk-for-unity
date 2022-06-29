using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionTask", menuName = "Game Server Services/Gs2Mission/MissionTask")]
    public class MissionTask : UnityEngine.ScriptableObject
    {
        public MissionGroup missionGroup;
        public string missionTaskName;
        
        public static MissionTask Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<MissionTask>(assetPath));
        }
        
        public static MissionTask New(
            MissionGroup missionGroup,
            string missionTaskName
        )
        {
            var instance = CreateInstance<MissionTask>();
            instance.name = "Runtime";
            instance.missionGroup = missionGroup;
            instance.missionTaskName = missionTaskName;
            return instance;
        }
    }
}