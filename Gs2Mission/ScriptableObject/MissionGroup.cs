using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionGroup", menuName = "Game Server Services/Gs2Mission/MissionGroup")]
    public class MissionGroup : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string missionGroupName;
        
        public static MissionGroup Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<MissionGroup>(assetPath));
        }
        
        public static MissionGroup New(
            Namespace Namespace,
            string missionGroupName
        )
        {
            var instance = CreateInstance<MissionGroup>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.missionGroupName = missionGroupName;
            return instance;
        }
    }
}