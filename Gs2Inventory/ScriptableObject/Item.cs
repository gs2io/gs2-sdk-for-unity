#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "Item", menuName = "Game Server Services/Gs2Inventory/Item")]
    public class Item : UnityEngine.ScriptableObject
    {
        public Inventory inventory;
        public string itemName;
        
#if UNITY_INCLUDE_TESTS
        public static Item Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Item>(assetPath));
        }
#endif
        
        public static Item New(
            Inventory inventory,
            string itemName
        )
        {
            var instance = CreateInstance<Item>();
            instance.name = "Runtime";
            instance.inventory = inventory;
            instance.itemName = itemName;
            return instance;
        }

        public Item Clone()
        {
            var instance = CreateInstance<Item>();
            instance.name = "Runtime";
            instance.inventory = inventory;
            instance.itemName = itemName;
            return instance;
        }
    }
}