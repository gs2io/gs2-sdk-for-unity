using UnityEngine;

namespace Gs2.Unity.Gs2Distributor.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Distributor/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
    }
}