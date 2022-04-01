using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game Server Services/Gs2Inventory/Item")]
    public class Item : UnityEngine.ScriptableObject
    {
        public Inventory inventory;
        public string itemName;
    }
}