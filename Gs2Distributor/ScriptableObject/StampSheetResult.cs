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

namespace Gs2.Unity.Gs2Distributor.ScriptableObject
{
    [CreateAssetMenu(fileName = "StampSheetResult", menuName = "Game Server Services/Gs2Distributor/StampSheetResult")]
    public class StampSheetResult : UnityEngine.ScriptableObject
    {
        public User User;
        public string transactionId;

        public string NamespaceName => this.User?.NamespaceName;
        public string UserId => this.User?.UserId;
        public string TransactionId => this.transactionId;

#if UNITY_INCLUDE_TESTS
        public static StampSheetResult Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<StampSheetResult>(assetPath)
            );
        }
#endif

        public static StampSheetResult New(
            User User,
            string transactionId
        )
        {
            var instance = CreateInstance<StampSheetResult>();
            instance.name = "Runtime";
            instance.User = User;
            instance.transactionId = transactionId;
            return instance;
        }

        public StampSheetResult Clone()
        {
            var instance = CreateInstance<StampSheetResult>();
            instance.name = "Runtime";
            instance.User = User;
            instance.transactionId = transactionId;
            return instance;
        }
    }
}