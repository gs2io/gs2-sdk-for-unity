#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "Lottery", menuName = "Game Server Services/Gs2Lottery/Lottery")]
    public class Lottery : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string lotteryName;
        
#if UNITY_INCLUDE_TESTS
        public static Lottery Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Lottery>(assetPath));
        }
#endif
        
        public static Lottery New(
            Namespace Namespace,
            string lotteryName
        )
        {
            var instance = CreateInstance<Lottery>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.lotteryName = lotteryName;
            return instance;
        }

        public Lottery Clone()
        {
            var instance = CreateInstance<Lottery>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.lotteryName = lotteryName;
            return instance;
        }
    }
}