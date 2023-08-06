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
 */
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnFriendRequest", menuName = "Game Server Services/Gs2Friend/OwnFriendRequest")]
    public class OwnFriendRequest : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string userId;
        public string targetUserId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string TargetUserId => this.targetUserId;

#if UNITY_INCLUDE_TESTS
        public static OwnFriendRequest Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnFriendRequest>(assetPath)
            );
        }
#endif

        public static OwnFriendRequest New(
            Namespace Namespace,
            string userId,
            string targetUserId
        )
        {
            var instance = CreateInstance<OwnFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            instance.targetUserId = targetUserId;
            return instance;
        }

        public OwnFriendRequest Clone()
        {
            var instance = CreateInstance<OwnFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.userId = userId;
            instance.targetUserId = targetUserId;
            return instance;
        }
    }
}