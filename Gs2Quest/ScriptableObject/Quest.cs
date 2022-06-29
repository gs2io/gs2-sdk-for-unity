using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Game Server Services/Gs2Quest/Quest")]
    public class Quest : UnityEngine.ScriptableObject
    {
        public QuestGroup questGroup;
        public string questName;
        
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
    }
}