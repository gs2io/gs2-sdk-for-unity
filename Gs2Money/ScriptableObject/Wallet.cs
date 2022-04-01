using UnityEngine;

namespace Gs2.Unity.Gs2Money.ScriptableObject
{
    [CreateAssetMenu(fileName = "Wallet", menuName = "Game Server Services/Gs2Money/Wallet")]
    public class Wallet : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int slot;
    }
}