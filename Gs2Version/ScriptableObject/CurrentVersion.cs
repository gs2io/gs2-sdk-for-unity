using UnityEngine;

namespace Gs2.Unity.Gs2Version.ScriptableObject
{
    [CreateAssetMenu(fileName = "CurrentVersion", menuName = "Game Server Services/Gs2Version/CurrentVersion")]
    public class CurrentVersion : UnityEngine.ScriptableObject
    {
        public Version version;
        public int major;
        public int minor;
        public int micro;
        public string body;
        public string signature;
    }
}