#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Money.ScriptableObject
{
    [CreateAssetMenu(fileName = "Wallet", menuName = "Game Server Services/Gs2Money/Wallet")]
    public class Wallet : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int slot;
        
#if UNITY_INCLUDE_TESTS
        public static Wallet Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Wallet>(assetPath));
        }
        
        public static Wallet New(
            Namespace Namespace,
            int slot
        )
        {
            var instance = CreateInstance<Wallet>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.slot = slot;
            return instance;
        }
#endif
    }
}