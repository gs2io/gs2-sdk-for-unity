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
    public class OwnBoxItem : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string prizeTableName;
        public int index;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string PrizeTableName => this.prizeTableName;
        public int Index => this.index;

#if UNITY_INCLUDE_TESTS
        public static OwnBoxItem Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnBoxItem>(assetPath)
            );
        }
#endif

        public static OwnBoxItem New(
            Namespace Namespace,
            string prizeTableName,
            int index
        )
        {
            var instance = CreateInstance<OwnBoxItem>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.prizeTableName = prizeTableName;
            instance.index = index;
            return instance;
        }

        public OwnBoxItem Clone()
        {
            var instance = CreateInstance<OwnBoxItem>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.prizeTableName = prizeTableName;
            instance.index = index;
            return instance;
        }
    }
}