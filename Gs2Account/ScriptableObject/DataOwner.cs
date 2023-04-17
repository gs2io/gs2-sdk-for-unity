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

namespace Gs2.Unity.Gs2Account.ScriptableObject
{
    [CreateAssetMenu(fileName = "DataOwner", menuName = "Game Server Services/Gs2Account/DataOwner")]
    public class DataOwner : UnityEngine.ScriptableObject
    {
        public Account Account;
        public string dataOwnerName;

        public string NamespaceName => this.Account?.NamespaceName;
        public string DataOwnerName => this.dataOwnerName;

#if UNITY_INCLUDE_TESTS
        public static DataOwner Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<DataOwner>(assetPath)
            );
        }
#endif

        public static DataOwner New(
            Account Account,
            string dataOwnerName
        )
        {
            var instance = CreateInstance<DataOwner>();
            instance.name = "Runtime";
            instance.Account = Account;
            instance.dataOwnerName = dataOwnerName;
            return instance;
        }

        public DataOwner Clone()
        {
            var instance = CreateInstance<DataOwner>();
            instance.name = "Runtime";
            instance.Account = Account;
            instance.dataOwnerName = dataOwnerName;
            return instance;
        }
    }
}