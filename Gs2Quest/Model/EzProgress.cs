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

using System;
using Gs2.Gs2Quest.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Quest.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzProgress
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ProgressId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string TransactionId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string QuestModelId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long RandomSeed;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2Quest.Model.EzReward> Rewards;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Gs2Quest.Model.EzReward> FailedRewards;

        public Gs2.Gs2Quest.Model.Progress ToModel()
        {
            return new Gs2.Gs2Quest.Model.Progress {
                ProgressId = ProgressId,
                TransactionId = TransactionId,
                QuestModelId = QuestModelId,
                RandomSeed = RandomSeed,
                Metadata = Metadata,
                Rewards = Rewards?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                FailedRewards = FailedRewards?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzProgress FromModel(Gs2.Gs2Quest.Model.Progress model)
        {
            return new EzProgress {
                ProgressId = model.ProgressId == null ? null : model.ProgressId,
                TransactionId = model.TransactionId == null ? null : model.TransactionId,
                QuestModelId = model.QuestModelId == null ? null : model.QuestModelId,
                RandomSeed = model.RandomSeed ?? 0,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Rewards = model.Rewards == null ? new List<Gs2.Unity.Gs2Quest.Model.EzReward>() : model.Rewards.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzReward.FromModel(v);
                }).ToList(),
                FailedRewards = model.FailedRewards == null ? new List<Gs2.Unity.Gs2Quest.Model.EzReward>() : model.FailedRewards.Select(v => {
                    return Gs2.Unity.Gs2Quest.Model.EzReward.FromModel(v);
                }).ToList(),
            };
        }
    }
}