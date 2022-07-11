#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Mission.ScriptableObject
{
    [CreateAssetMenu(fileName = "MissionCounter", menuName = "Game Server Services/Gs2Mission/MissionCounter")]
    public class MissionCounter : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string missionCounterName;
        
#if UNITY_INCLUDE_TESTS
        public static MissionCounter Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<MissionCounter>(assetPath));
        }
        
        public static MissionCounter New(
            Namespace Namespace,
            string missionCounterName
        )
        {
            var instance = CreateInstance<MissionCounter>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.missionCounterName = missionCounterName;
            return instance;
        }
#endif
    }
}