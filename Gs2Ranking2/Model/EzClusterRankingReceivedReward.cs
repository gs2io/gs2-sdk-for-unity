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
	public class EzClusterRankingReceivedReward
	{
		[SerializeField]
		public string RankingName;
		[SerializeField]
		public string ClusterName;
		[SerializeField]
		public long Season;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public long ReceivedAt;

        public Gs2.Gs2Ranking2.Model.ClusterRankingReceivedReward ToModel()
        {
            return new Gs2.Gs2Ranking2.Model.ClusterRankingReceivedReward {
                RankingName = RankingName,
                ClusterName = ClusterName,
                Season = Season,
                UserId = UserId,
                ReceivedAt = ReceivedAt,
            };
        }

        public static EzClusterRankingReceivedReward FromModel(Gs2.Gs2Ranking2.Model.ClusterRankingReceivedReward model)
        {
            return new EzClusterRankingReceivedReward {
                RankingName = model.RankingName == null ? null : model.RankingName,
                ClusterName = model.ClusterName == null ? null : model.ClusterName,
                Season = model.Season ?? 0,
                UserId = model.UserId == null ? null : model.UserId,
                ReceivedAt = model.ReceivedAt ?? 0,
            };
        }
    }
}