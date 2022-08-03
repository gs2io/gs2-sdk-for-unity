using Gs2.Unity.Gs2Inventory.ScriptableObject;
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    public class Material : UnityEngine.ScriptableObject
    {
        public ItemSet material;
        public int count;

#if UNITY_INCLUDE_TESTS
        public static Material Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Material>(assetPath));
        }
#endif

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

        public Material Clone()
        {
            var instance = CreateInstance<Material>();
            instance.name = "Runtime";
            instance.material = material;
            instance.count = count;
            return instance;
        }
    }
}