using UnityEngine;

namespace Gs2.Unity.Gs2Dictionary.ScriptableObject
{
    [CreateAssetMenu(fileName = "Entry", menuName = "Game Server Services/Gs2Dictionary/Entry")]
    public class Entry : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string entryName;
    }
}