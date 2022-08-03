
using Gs2.Util.WebSocketSharp;
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    public class ItemSet : UnityEngine.ScriptableObject
    {
        public Item item;
        public string userId;
        public string itemSetName;
        
        public string Grn => $"grn:gs2:{{region}}:{{ownerId}}:inventory:{item.inventory.Namespace.namespaceName}:user:{userId}:inventory:{item.inventory.inventoryName}:item:{item.itemName}:itemSet:{itemSetName}";

#if UNITY_INCLUDE_TESTS
        public static ItemSet Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ItemSet>(assetPath));
        }
#endif

        public static ItemSet New(
            Item item,
            string userId,
            string itemSetName
        )
        {
            var instance = CreateInstance<ItemSet>();
            instance.name = "Runtime";
            instance.item = item;
            instance.userId = userId;
            instance.itemSetName = itemSetName;
            return instance;
        }

        public ItemSet Clone()
        {
            var instance = CreateInstance<ItemSet>();
            instance.name = "Runtime";
            instance.item = item;
            instance.userId = userId;
            instance.itemSetName = itemSetName;
            return instance;
        }

        public bool MatchGrn(string itemSetId)
        {
            var elements1 = Grn.Split(':');
            var elements2 = itemSetId.Split(':');
            for (var i = 4; i < elements1.Length; i++)
            {
                if (elements1[i] != elements2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}