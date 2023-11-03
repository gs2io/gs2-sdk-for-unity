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
namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "PrizeLimit", menuName = "Game Server Services/Gs2Lottery/PrizeLimit")]
    public class PrizeLimit : UnityEngine.ScriptableObject
    {
        public PrizeTable PrizeTable;
        public string prizeId;

        public string NamespaceName => this.PrizeTable?.NamespaceName;
        public string PrizeTableName => this.PrizeTable?.PrizeTableName;
        public string PrizeId => this.prizeId;

#if UNITY_INCLUDE_TESTS
        public static PrizeLimit Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<PrizeLimit>(assetPath)
            );
        }
#endif

        public static PrizeLimit New(
            PrizeTable PrizeTable,
            string prizeId
        )
        {
            var instance = CreateInstance<PrizeLimit>();
            instance.name = "Runtime";
            instance.PrizeTable = PrizeTable;
            instance.prizeId = prizeId;
            return instance;
        }

        public PrizeLimit Clone()
        {
            var instance = CreateInstance<PrizeLimit>();
            instance.name = "Runtime";
            instance.PrizeTable = PrizeTable;
            instance.prizeId = prizeId;
            return instance;
        }
    }
}