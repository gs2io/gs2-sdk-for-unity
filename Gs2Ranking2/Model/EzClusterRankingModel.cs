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
using Gs2.Gs2Ranking2.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Ranking2.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzClusterRankingModel
	{
		[SerializeField]
		public string ClusterRankingModelId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string ClusterType;
		[SerializeField]
		public List<Gs2.Unity.Gs2Ranking2.Model.EzRankingReward> RankingRewards;
		[SerializeField]
		public string EntryPeriodEventId;
		[SerializeField]
		public string AccessPeriodEventId;

        public Gs2.Gs2Ranking2.Model.ClusterRankingModel ToModel()
        {
            return new Gs2.Gs2Ranking2.Model.ClusterRankingModel {
                ClusterRankingModelId = ClusterRankingModelId,
                Name = Name,
                Metadata = Metadata,
                ClusterType = ClusterType,
                RankingRewards = RankingRewards?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                EntryPeriodEventId = EntryPeriodEventId,
                AccessPeriodEventId = AccessPeriodEventId,
            };
        }

        public static EzClusterRankingModel FromModel(Gs2.Gs2Ranking2.Model.ClusterRankingModel model)
        {
            return new EzClusterRankingModel {
                ClusterRankingModelId = model.ClusterRankingModelId == null ? null : model.ClusterRankingModelId,
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ClusterType = model.ClusterType == null ? null : model.ClusterType,
                RankingRewards = model.RankingRewards == null ? new List<Gs2.Unity.Gs2Ranking2.Model.EzRankingReward>() : model.RankingRewards.Select(v => {
                    return Gs2.Unity.Gs2Ranking2.Model.EzRankingReward.FromModel(v);
                }).ToList(),
                EntryPeriodEventId = model.EntryPeriodEventId == null ? null : model.EntryPeriodEventId,
                AccessPeriodEventId = model.AccessPeriodEventId == null ? null : model.AccessPeriodEventId,
            };
        }
    }
}