#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Chat.ScriptableObject
{
    [CreateAssetMenu(fileName = "Room", menuName = "Game Server Services/Gs2Chat/Room")]
    public class Room : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string roomName;
        public string password;
        
#if UNITY_INCLUDE_TESTS
        public static Room Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Room>(assetPath));
        }
#endif

        public static Room New(
            Namespace @namespace,
            string roomName,
            string password = null
        )
        {
            var instance = CreateInstance<Room>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.roomName = roomName;
            instance.password = password;
            return instance;
        }

        public Room Clone()
        {
            var instance = CreateInstance<Room>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.roomName = roomName;
            instance.password = password;
            return instance;
        }
    }
}