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

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    [CreateAssetMenu(fileName = "ReceiveFriendRequest", menuName = "Game Server Services/Gs2Friend/ReceiveFriendRequest")]
    public class ReceiveFriendRequest : UnityEngine.ScriptableObject
    {
        public User User;
        public string fromUserId;

        public string NamespaceName => this.User.NamespaceName;
        public string FromUserId => fromUserId;
        public string UserId => this.User.UserId;

#if UNITY_INCLUDE_TESTS
        public static ReceiveFriendRequest Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ReceiveFriendRequest>(assetPath)
            );
        }
#endif

        public static ReceiveFriendRequest New(
            User User,
            string fromUserId
        )
        {
            var instance = CreateInstance<ReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.User = User;
            instance.fromUserId = fromUserId;
            return instance;
        }

        public ReceiveFriendRequest Clone()
        {
            var instance = CreateInstance<ReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.User = User;
            instance.fromUserId = fromUserId;
            return instance;
        }
    }
}