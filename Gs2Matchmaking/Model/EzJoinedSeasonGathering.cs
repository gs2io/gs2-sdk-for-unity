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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Matchmaking.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzJoinedSeasonGathering
	{
		[SerializeField]
		public string SeasonName;
		[SerializeField]
		public long Season;
		[SerializeField]
		public long Tier;
		[SerializeField]
		public string SeasonGatheringName;

        public Gs2.Gs2Matchmaking.Model.JoinedSeasonGathering ToModel()
        {
            return new Gs2.Gs2Matchmaking.Model.JoinedSeasonGathering {
                SeasonName = SeasonName,
                Season = Season,
                Tier = Tier,
                SeasonGatheringName = SeasonGatheringName,
            };
        }

        public static EzJoinedSeasonGathering FromModel(Gs2.Gs2Matchmaking.Model.JoinedSeasonGathering model)
        {
            return new EzJoinedSeasonGathering {
                SeasonName = model.SeasonName == null ? null : model.SeasonName,
                Season = model.Season ?? 0,
                Tier = model.Tier ?? 0,
                SeasonGatheringName = model.SeasonGatheringName == null ? null : model.SeasonGatheringName,
            };
        }
    }
}