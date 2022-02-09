using UnityEngine;

namespace Gs2.Unity.Gs2Exchange.ScriptableObject
{
    [CreateAssetMenu(fileName = "Rate", menuName = "Game Server Services/Gs2Exchange/Rate")]
    public class Rate : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string rateName;
    }
}