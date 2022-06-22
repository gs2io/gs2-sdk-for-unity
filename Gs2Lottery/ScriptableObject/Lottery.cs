using UnityEngine;

namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "Lottery", menuName = "Game Server Services/Gs2Lottery/Lottery")]
    public class Lottery : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string lotteryName;
    }
}