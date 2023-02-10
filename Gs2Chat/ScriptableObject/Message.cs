/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 *
 * deny overwrite
 */
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Chat.ScriptableObject
{
    [CreateAssetMenu(fileName = "Message", menuName = "Game Server Services/Gs2Chat/Message")]
    public class Message : UnityEngine.ScriptableObject
    {
        public Room Room;
        public string messageName;

        public string NamespaceName => this.Room.NamespaceName;
        public string RoomName => this.Room.RoomName;
        public string Password => this.Room.Password;
        public string MessageName => this.messageName;

#if UNITY_INCLUDE_TESTS
        public static Message Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Message>(assetPath)
            );
        }
#endif

        public static Message New(
            Room Room,
            string messageName
        )
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.Room = Room;
            instance.messageName = messageName;
            return instance;
        }

        public Message Clone()
        {
            var instance = CreateInstance<Message>();
            instance.name = "Runtime";
            instance.Room = Room;
            instance.messageName = messageName;
            return instance;
        }
    }
}