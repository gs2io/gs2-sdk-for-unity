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
	public class EzGlobalRankingScore
	{
		[SerializeField]
		public string RankingName;
		[SerializeField]
		public long Season;
		[SerializeField]
		public string UserId;
		[SerializeField]
		public long Score;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long UpdatedAt;

        public Gs2.Gs2Ranking2.Model.GlobalRankingScore ToModel()
        {
            return new Gs2.Gs2Ranking2.Model.GlobalRankingScore {
                RankingName = RankingName,
                Season = Season,
                UserId = UserId,
                Score = Score,
                Metadata = Metadata,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzGlobalRankingScore FromModel(Gs2.Gs2Ranking2.Model.GlobalRankingScore model)
        {
            return new EzGlobalRankingScore {
                RankingName = model.RankingName == null ? null : model.RankingName,
                Season = model.Season ?? 0,
                UserId = model.UserId == null ? null : model.UserId,
                Score = model.Score ?? 0,
                Metadata = model.Metadata == null ? null : model.Metadata,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}