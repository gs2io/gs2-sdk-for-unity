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

namespace Gs2.Unity.Gs2LoginReward.ScriptableObject
{
    [CreateAssetMenu(fileName = "ReceiveStatus", menuName = "Game Server Services/Gs2LoginReward/ReceiveStatus")]
    public class ReceiveStatus : UnityEngine.ScriptableObject
    {
        public User User;
        public string bonusModelName;

        public string NamespaceName => this.User?.NamespaceName;
        public string UserId => this.User?.UserId;
        public string BonusModelName => this.bonusModelName;

#if UNITY_INCLUDE_TESTS
        public static ReceiveStatus Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ReceiveStatus>(assetPath)
            );
        }
#endif

        public static ReceiveStatus New(
            User User,
            string bonusModelName
        )
        {
            var instance = CreateInstance<ReceiveStatus>();
            instance.name = "Runtime";
            instance.User = User;
            instance.bonusModelName = bonusModelName;
            return instance;
        }

        public ReceiveStatus Clone()
        {
            var instance = CreateInstance<ReceiveStatus>();
            instance.name = "Runtime";
            instance.User = User;
            instance.bonusModelName = bonusModelName;
            return instance;
        }
    }
}