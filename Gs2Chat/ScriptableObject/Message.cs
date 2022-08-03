using UnityEngine;

namespace Gs2.Unity.Gs2Chat.ScriptableObject
{
    public class Message : UnityEngine.ScriptableObject
    {
        public Room room;
        public string messageName;
        
        public static Message New(
            Room room,
            string messageName
        )
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.room = room;
            instance.messageName = messageName;
            return instance;
        }

        public Message Clone()
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.room = room;
            instance.messageName = messageName;
            return instance;
        }
    }
}