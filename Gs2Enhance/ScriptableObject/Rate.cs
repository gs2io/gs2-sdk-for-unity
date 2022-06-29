using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Enhance.ScriptableObject
{
    [CreateAssetMenu(fileName = "Rate", menuName = "Game Server Services/Gs2Enhance/Rate")]
    public class Rate : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string rateName;
        
        public static Rate Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Rate>(assetPath));
        }
        
        public static Rate New(
            Namespace @namespace,
            string rateName
        )
        {
            var instance = CreateInstance<Rate>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.rateName = rateName;
            return instance;
        }
    }
}