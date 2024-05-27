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
using Gs2.Gs2Ranking.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Ranking.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGlobalRankingSetting
	{
		[SerializeField]
		public int CalculateIntervalMinutes;
		[SerializeField]
		public List<Gs2.Unity.Gs2Ranking.Model.EzScope> AdditionalScopes;

        public Gs2.Gs2Ranking.Model.GlobalRankingSetting ToModel()
        {
            return new Gs2.Gs2Ranking.Model.GlobalRankingSetting {
                CalculateIntervalMinutes = CalculateIntervalMinutes,
                AdditionalScopes = AdditionalScopes?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzGlobalRankingSetting FromModel(Gs2.Gs2Ranking.Model.GlobalRankingSetting model)
        {
            return new EzGlobalRankingSetting {
                CalculateIntervalMinutes = model.CalculateIntervalMinutes ?? 0,
                AdditionalScopes = model.AdditionalScopes == null ? new List<Gs2.Unity.Gs2Ranking.Model.EzScope>() : model.AdditionalScopes.Select(v => {
                    return Gs2.Unity.Gs2Ranking.Model.EzScope.FromModel(v);
                }).ToList(),
            };
        }
    }
}