using UnityEngine;

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "DisplayItem", menuName = "Game Server Services/Gs2Showcase/DisplayItem")]
    public class DisplayItem : UnityEngine.ScriptableObject
    {
        public Showcase showcase;
        public string displayItemId;
    }
}