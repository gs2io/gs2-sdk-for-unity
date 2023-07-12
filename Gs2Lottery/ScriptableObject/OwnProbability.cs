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
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Lottery.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnProbability", menuName = "Game Server Services/Gs2Lottery/OwnProbability")]
    public class OwnProbability : UnityEngine.ScriptableObject
    {
        public LotteryModel LotteryModel;
        public string prizeId;

        public string NamespaceName => this.LotteryModel.NamespaceName;
        public string LotteryName => this.LotteryModel.LotteryName;
        public string PrizeId => this.prizeId;

#if UNITY_INCLUDE_TESTS
        public static OwnProbability Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnProbability>(assetPath)
            );
        }
#endif

        public static OwnProbability New(
            LotteryModel LotteryModel,
            string prizeId
        )
        {
            var instance = CreateInstance<OwnProbability>();
            instance.name = "Runtime";
            instance.LotteryModel = LotteryModel;
            instance.prizeId = prizeId;
            return instance;
        }

        public OwnProbability Clone()
        {
            var instance = CreateInstance<OwnProbability>();
            instance.name = "Runtime";
            instance.LotteryModel = LotteryModel;
            instance.prizeId = prizeId;
            return instance;
        }
    }
}