using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Game Server Services/Gs2Inventory/Inventory")]
    public class Inventory : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string inventoryName;
    }
}