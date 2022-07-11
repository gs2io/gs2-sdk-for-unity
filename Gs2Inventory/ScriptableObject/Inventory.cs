#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Inventory.ScriptableObject
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Game Server Services/Gs2Inventory/Inventory")]
    public class Inventory : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string inventoryName;
        
#if UNITY_INCLUDE_TESTS
        public static Inventory Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Inventory>(assetPath));
        }
        
        public static Inventory New(
            Namespace Namespace,
            string inventoryName
        )
        {
            var instance = CreateInstance<Inventory>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.inventoryName = inventoryName;
            return instance;
        }
#endif
    }
}