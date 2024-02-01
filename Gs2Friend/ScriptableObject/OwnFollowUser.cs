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
// ReSharper disable InconsistentNaming
// ReSharper disable Unity.NoNullPropagation

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning disable CS0108, CS0114

#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Friend.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnFollowUser", menuName = "Game Server Services/Gs2Friend/OwnFollowUser")]
    public class OwnFollowUser : UnityEngine.ScriptableObject
    {
        public OwnFollow Follow;
        public string targetUserId;

        public string NamespaceName => this.Follow.NamespaceName;
        public bool WithProfile => this.Follow.WithProfile;
        public string TargetUserId => this.targetUserId;

#if UNITY_INCLUDE_TESTS
        public static OwnFollowUser Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnFollowUser>(assetPath)
            );
        }
#endif
        public static OwnFollowUser New(
            OwnFollow @follow,
            string targetUserId
        )
        {
            var instance = CreateInstance<OwnFollowUser>();
            instance.name = "Runtime";
            instance.Follow = @follow;
            instance.targetUserId = targetUserId;
            return instance;
        }
        public OwnFollowUser Clone()
        {
            var instance = CreateInstance<OwnFollowUser>();
            instance.name = "Runtime";
            instance.Follow = Follow;
            instance.targetUserId = targetUserId;
            return instance;
        }
    }
}