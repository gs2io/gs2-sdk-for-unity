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
    [CreateAssetMenu(fileName = "FriendUser", menuName = "Game Server Services/Gs2Friend/FriendUser")]
    public class FriendUser : UnityEngine.ScriptableObject
    {
        public Friend Friend;
        public bool withProfile;

        public string NamespaceName => this.Friend.NamespaceName;
        public string UserId => this.Friend.UserId;
        public bool WithProfile => this.withProfile;

#if UNITY_INCLUDE_TESTS
        public static FriendUser Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<FriendUser>(assetPath)
            );
        }
#endif

        public static FriendUser New(
            Friend Friend,
            bool withProfile
        )
        {
            var instance = CreateInstance<FriendUser>();
            instance.name = "Runtime";
            instance.Friend = Friend;
            instance.withProfile = withProfile;
            return instance;
        }

        public FriendUser Clone()
        {
            var instance = CreateInstance<FriendUser>();
            instance.name = "Runtime";
            instance.Friend = Friend;
            instance.withProfile = withProfile;
            return instance;
        }
    }
}