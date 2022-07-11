#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Stamina.ScriptableObject
{
    [CreateAssetMenu(fileName = "Stamina", menuName = "Game Server Services/Gs2Stamina/Stamina")]
    public class Stamina : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string staminaName;
        
#if UNITY_INCLUDE_TESTS
        public static Stamina Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Stamina>(assetPath));
        }
        
        public static Stamina New(
            Namespace Namespace,
            string staminaName
        )
        {
            var instance = CreateInstance<Stamina>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.staminaName = staminaName;
            return instance;
        }
#endif
    }
}