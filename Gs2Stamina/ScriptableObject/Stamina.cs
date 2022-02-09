using UnityEngine;

namespace Gs2.Unity.Gs2Stamina.ScriptableObject
{
    [CreateAssetMenu(fileName = "Stamina", menuName = "Game Server Services/Gs2Stamina/Stamina")]
    public class Stamina : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string staminaName;
    }
}