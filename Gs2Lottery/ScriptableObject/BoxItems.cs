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

namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "BoxItems", menuName = "Game Server Services/Gs2Lottery/BoxItems")]
    public class BoxItems : UnityEngine.ScriptableObject
    {
        public User User;
        public string prizeTableName;

        public string NamespaceName => this.User?.NamespaceName;
        public string UserId => this.User?.UserId;
        public string PrizeTableName => this.prizeTableName;

#if UNITY_INCLUDE_TESTS
        public static BoxItems Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<BoxItems>(assetPath)
            );
        }
#endif

        public static BoxItems New(
            User User,
            string prizeTableName
        )
        {
            var instance = CreateInstance<BoxItems>();
            instance.name = "Runtime";
            instance.User = User;
            instance.prizeTableName = prizeTableName;
            return instance;
        }

        public BoxItems Clone()
        {
            var instance = CreateInstance<BoxItems>();
            instance.name = "Runtime";
            instance.User = User;
            instance.prizeTableName = prizeTableName;
            return instance;
        }
    }
}