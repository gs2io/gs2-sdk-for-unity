using UnityEngine;

namespace Gs2.Unity.Gs2Version.ScriptableObject
{
    [CreateAssetMenu(fileName = "Version", menuName = "Game Server Services/Gs2Version/Version")]
    public class Version : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string versionName;
    }
}