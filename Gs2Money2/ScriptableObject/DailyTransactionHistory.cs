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
namespace Gs2.Unity.Gs2Money2.ScriptableObject
{
    [CreateAssetMenu(fileName = "DailyTransactionHistory", menuName = "Game Server Services/Gs2Money2/DailyTransactionHistory")]
    public class DailyTransactionHistory : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int year;
        public int month;
        public int day;
        public string currency;

        public string NamespaceName => this.Namespace?.NamespaceName;
        public int Year => this.year;
        public int Month => this.month;
        public int Day => this.day;
        public string Currency => this.currency;

#if UNITY_INCLUDE_TESTS
        public static DailyTransactionHistory Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<DailyTransactionHistory>(assetPath)
            );
        }
#endif

        public static DailyTransactionHistory New(
            Namespace Namespace,
            int year,
            int month,
            int day,
            string currency
        )
        {
            var instance = CreateInstance<DailyTransactionHistory>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.year = year;
            instance.month = month;
            instance.day = day;
            instance.currency = currency;
            return instance;
        }

        public DailyTransactionHistory Clone()
        {
            var instance = CreateInstance<DailyTransactionHistory>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.year = year;
            instance.month = month;
            instance.day = day;
            instance.currency = currency;
            return instance;
        }
    }
}