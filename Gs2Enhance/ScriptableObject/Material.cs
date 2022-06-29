using Gs2.Unity.Gs2Inventory.ScriptableObject;
using UnityEditor;

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    public class Material : UnityEngine.ScriptableObject
    {
        public ItemSet material;
        public int count;

        public static Material Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Material>(assetPath));
        }

        public static Material New(
            ItemSet material,
            int count
        )
        {
            var instance = CreateInstance<Material>();
            instance.name = "Runtime";
            instance.material = material;
            instance.count = count;
            return instance;
        }
    }
}