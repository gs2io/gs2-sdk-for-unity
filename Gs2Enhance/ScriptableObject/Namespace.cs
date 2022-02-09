using UnityEngine;

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Enhance/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
    }
}