using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Inventory/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
    }
}