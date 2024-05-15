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
// ReSharper disable InconsistentNaming
// ReSharper disable Unity.NoNullPropagation

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
#pragma warning disable CS0108, CS0114

#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Guild.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnReceiveMemberRequest", menuName = "Game Server Services/Gs2Guild/OwnReceiveMemberRequest")]
    public class OwnReceiveMemberRequest : UnityEngine.ScriptableObject
    {
        public Guild Guild;
        public string targetGuildName;
        public string fromUserId;

        public string NamespaceName => this.Guild.NamespaceName;
        public string GuildModelName => this.Guild.GuildModelName;
        public string GuildName => this.Guild.GuildName;
        public string TargetGuildName => this.targetGuildName;
        public string FromUserId => this.fromUserId;

#if UNITY_INCLUDE_TESTS
        public static OwnReceiveMemberRequest Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnReceiveMemberRequest>(assetPath)
            );
        }
#endif
        public static OwnReceiveMemberRequest New(
            Guild @guild,
            string targetGuildName,
            string fromUserId
        )
        {
            var instance = CreateInstance<OwnReceiveMemberRequest>();
            instance.name = "Runtime";
            instance.Guild = @guild;
            instance.targetGuildName = targetGuildName;
            instance.fromUserId = fromUserId;
            return instance;
        }
        public OwnReceiveMemberRequest Clone()
        {
            var instance = CreateInstance<OwnReceiveMemberRequest>();
            instance.name = "Runtime";
            instance.Guild = Guild;
            instance.targetGuildName = targetGuildName;
            instance.fromUserId = fromUserId;
            return instance;
        }
    }
}