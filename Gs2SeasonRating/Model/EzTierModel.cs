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
using Gs2.Gs2SeasonRating.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SeasonRating.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzTierModel
	{
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public int RaiseRankBonus;
		[SerializeField]
		public int MinimumChangePoint;
		[SerializeField]
		public int MaximumChangePoint;

        public Gs2.Gs2SeasonRating.Model.TierModel ToModel()
        {
            return new Gs2.Gs2SeasonRating.Model.TierModel {
                Metadata = Metadata,
                RaiseRankBonus = RaiseRankBonus,
                MinimumChangePoint = MinimumChangePoint,
                MaximumChangePoint = MaximumChangePoint,
            };
        }

        public static EzTierModel FromModel(Gs2.Gs2SeasonRating.Model.TierModel model)
        {
            return new EzTierModel {
                Metadata = model.Metadata == null ? null : model.Metadata,
                RaiseRankBonus = model.RaiseRankBonus ?? 0,
                MinimumChangePoint = model.MinimumChangePoint ?? 0,
                MaximumChangePoint = model.MaximumChangePoint ?? 0,
            };
        }
    }
}