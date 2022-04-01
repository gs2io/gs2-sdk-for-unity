using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    [CreateAssetMenu(fileName = "Mold", menuName = "Game Server Services/Gs2Formation/Mold")]
    public class Mold : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string moldName;
    }
}