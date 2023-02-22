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
    [CreateAssetMenu(fileName = "TakeOver", menuName = "Game Server Services/Gs2Account/TakeOver")]
    public class OwnTakeOver : UnityEngine.ScriptableObject
    {
        public OwnAccount Account;
        public int type;

        public string NamespaceName => this.Account.NamespaceName;
        public int Type => this.type;

#if UNITY_INCLUDE_TESTS
        public static TakeOver Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<TakeOver>(assetPath)
            );
        }
#endif

        public static OwnTakeOver New(
            OwnAccount Account,
            int type
        )
        {
            var instance = CreateInstance<OwnTakeOver>();
            instance.name = "Runtime";
            instance.Account = Account;
            instance.type = type;
            return instance;
        }

        public OwnTakeOver Clone()
        {
            var instance = CreateInstance<OwnTakeOver>();
            instance.name = "Runtime";
            instance.Account = Account;
            instance.type = type;
            return instance;
        }
    }
}