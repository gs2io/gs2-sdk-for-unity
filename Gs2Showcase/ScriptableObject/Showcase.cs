using UnityEngine;

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "Showcase", menuName = "Game Server Services/Gs2Showcase/Showcase")]
    public class Showcase : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string showcaseName;
    }
}