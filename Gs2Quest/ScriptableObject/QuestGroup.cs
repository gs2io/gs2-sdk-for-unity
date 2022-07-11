#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "QuestGroup", menuName = "Game Server Services/Gs2Quest/QuestGroup")]
    public class QuestGroup : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string questGroupName;
        
#if UNITY_INCLUDE_TESTS
        public static QuestGroup Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<QuestGroup>(assetPath));
        }
        
        public static QuestGroup New(
            Namespace Namespace,
            string questGroupName
        )
        {
            var instance = CreateInstance<QuestGroup>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.questGroupName = questGroupName;
            return instance;
        }
#endif
    }
}