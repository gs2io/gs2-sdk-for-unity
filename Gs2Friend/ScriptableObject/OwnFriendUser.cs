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
    [CreateAssetMenu(fileName = "OwnFriendUser", menuName = "Game Server Services/Gs2Friend/OwnFriendUser")]
    public class OwnFriendUser : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string targetUserId;
        public bool withProfile;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string TargetUserId => this.targetUserId;
        public bool WithProfile => this.withProfile;

#if UNITY_INCLUDE_TESTS
        public static OwnFriendUser Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnFriendUser>(assetPath)
            );
        }
#endif

        public static OwnFriendUser New(
            Namespace Namespace,
            string targetUserId,
            bool withProfile
        )
        {
            var instance = CreateInstance<OwnFriendUser>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.targetUserId = targetUserId;
            instance.withProfile = withProfile;
            return instance;
        }

        public OwnFriendUser Clone()
        {
            var instance = CreateInstance<OwnFriendUser>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.targetUserId = targetUserId;
            instance.withProfile = withProfile;
            return instance;
        }
    }
}