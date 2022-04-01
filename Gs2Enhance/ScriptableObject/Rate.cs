using UnityEngine;

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    [CreateAssetMenu(fileName = "Rate", menuName = "Game Server Services/Gs2Enhance/Rate")]
    public class Rate : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string rateName;
    }
}