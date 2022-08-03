#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionGroup", menuName = "Game Server Services/Gs2Mission/MissionGroup")]
    public class MissionGroup : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string missionGroupName;
        
#if UNITY_INCLUDE_TESTS
        public static MissionGroup Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<MissionGroup>(assetPath));
        }
#endif
        
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

        public MissionGroup Clone()
        {
            var instance = CreateInstance<MissionGroup>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.missionGroupName = missionGroupName;
            return instance;
        }
    }
}