using UnityEngine;

namespace Gs2.Unity.Gs2Quest.ScriptableObject
{
    [CreateAssetMenu(fileName = "QuestGroup", menuName = "Game Server Services/Gs2Quest/QuestGroup")]
    public class QuestGroup : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string questGroupName;
    }
}