using UnityEngine;

namespace Gs2.Unity.Gs2Chat.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Chat/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
    }
}