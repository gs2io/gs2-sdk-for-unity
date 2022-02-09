using UnityEngine;

namespace Gs2.Unity.Gs2Account.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Account/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
    }
}