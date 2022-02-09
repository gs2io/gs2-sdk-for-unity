using UnityEngine;

namespace Gs2.Unity.Gs2Experience.ScriptableObject
{
    [CreateAssetMenu(fileName = "Experience", menuName = "Game Server Services/Gs2Experience/Experience")]
    public class Experience : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string experienceName;
    }
}