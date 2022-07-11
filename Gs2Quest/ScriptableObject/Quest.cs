#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Game Server Services/Gs2Quest/Quest")]
    public class Quest : UnityEngine.ScriptableObject
    {
        public QuestGroup questGroup;
        public string questName;
        
#if UNITY_INCLUDE_TESTS
        public static Quest Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Quest>(assetPath));
        }
        
        public static Quest New(
            QuestGroup questGroup,
            string questName
        )
        {
            var instance = CreateInstance<Quest>();
            instance.name = "Runtime";
            instance.questGroup = questGroup;
            instance.questName = questName;
            return instance;
        }
#endif
    }
}