using UnityEngine;

namespace Gs2.Unity.Gs2Limit.ScriptableObject
{
    [CreateAssetMenu(fileName = "Limit", menuName = "Game Server Services/Gs2Limit/Limit")]
    public class Limit : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string limitName;
    }
}