#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    public class Form : UnityEngine.ScriptableObject
    {
        public Mold mold;
        public int index;

#if UNITY_INCLUDE_TESTS
        public static Form Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Form>(assetPath));
        }
#endif
        
        public static Form New(
            Mold mold,
            int index
        )
        {
            var instance = CreateInstance<Form>();
            instance.name = "Runtime";
            instance.mold = mold;
            instance.index = index;
            return instance;
        }

        public Form Clone()
        {
            var instance = CreateInstance<Form>();
            instance.name = "Runtime";
            instance.mold = mold;
            instance.index = index;
            return instance;
        }
    }
}