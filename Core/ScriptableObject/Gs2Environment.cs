using Gs2.Core.Model;
using UnityEngine;

namespace Gs2.Unity.Core.ScriptableObject
{
    [CreateAssetMenu(fileName = "Environment", menuName = "Game Server Services/Core/Environment")]
    public class Gs2Environment : UnityEngine.ScriptableObject
    {
        public string name;
        public Region region;
        public string clientId;
        public string clientSecret;
    }
}