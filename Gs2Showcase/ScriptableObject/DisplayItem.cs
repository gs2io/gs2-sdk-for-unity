#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "DisplayItem", menuName = "Game Server Services/Gs2Showcase/DisplayItem")]
    public class DisplayItem : UnityEngine.ScriptableObject
    {
        public Showcase showcase;
        public string displayItemId;
        
#if UNITY_INCLUDE_TESTS
        public static DisplayItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<DisplayItem>(assetPath));
        }
#endif
        
        public static DisplayItem New(
            Showcase showcase,
            string displayItemId
        )
        {
            var instance = CreateInstance<DisplayItem>();
            instance.name = "Runtime";
            instance.showcase = showcase;
            instance.displayItemId = displayItemId;
            return instance;
        }

        public DisplayItem Clone()
        {
            var instance = CreateInstance<DisplayItem>();
            instance.name = "Runtime";
            instance.showcase = showcase;
            instance.displayItemId = displayItemId;
            return instance;
        }
    }
}