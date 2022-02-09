using UnityEngine;

namespace Gs2.Unity.Gs2Chat.ScriptableObject
{
    [CreateAssetMenu(fileName = "Room", menuName = "Game Server Services/Gs2Chat/Room")]
    public class Room : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string roomName;
        public string password;
    }
}