using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    public class ItemSet : UnityEngine.ScriptableObject
    {
        public Item item;
        public string userId;
        public string itemSetName;
        
        public string Grn => $"grn:gs2:{{region}}:{{ownerId}}:inventory:{item.inventory.Namespace.namespaceName}:user:{userId}:inventory:{item.inventory.inventoryName}:item:{item.itemName}:itemSet:{itemSetName}";
    }
}