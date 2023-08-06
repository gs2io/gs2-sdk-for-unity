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
    [CreateAssetMenu(fileName = "OwnReceiveFriendRequest", menuName = "Game Server Services/Gs2Friend/OwnReceiveFriendRequest")]
    public class OwnReceiveFriendRequest : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string targetUserId;
        public string fromUserId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string TargetUserId => this.targetUserId;
        public string FromUserId => this.fromUserId;

#if UNITY_INCLUDE_TESTS
        public static OwnReceiveFriendRequest Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnReceiveFriendRequest>(assetPath)
            );
        }
#endif

        public static OwnReceiveFriendRequest New(
            Namespace Namespace,
            string targetUserId,
            string fromUserId
        )
        {
            var instance = CreateInstance<OwnReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.targetUserId = targetUserId;
            instance.fromUserId = fromUserId;
            return instance;
        }

        public OwnReceiveFriendRequest Clone()
        {
            var instance = CreateInstance<OwnReceiveFriendRequest>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.targetUserId = targetUserId;
            instance.fromUserId = fromUserId;
            return instance;
        }
    }
}