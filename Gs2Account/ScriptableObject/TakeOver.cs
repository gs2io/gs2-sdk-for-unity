using UnityEngine;

namespace Gs2.Unity.Gs2Account.ScriptableObject
{
    [CreateAssetMenu(fileName = "TakeOver", menuName = "Game Server Services/Gs2Account/TakeOver")]
    public class TakeOver : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int type;
    }
}