using UnityEngine;

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Game Server Services/Gs2Quest/Quest")]
    public class Quest : UnityEngine.ScriptableObject
    {
        public QuestGroup questGroup;
        public string questName;
    }
}