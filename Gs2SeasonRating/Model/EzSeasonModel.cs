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
	public class EzSeasonModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Gs2SeasonRating.Model.EzTierModel> Tiers;
		[SerializeField]
		public string ExperienceModelId;

        public Gs2.Gs2SeasonRating.Model.SeasonModel ToModel()
        {
            return new Gs2.Gs2SeasonRating.Model.SeasonModel {
                Name = Name,
                Metadata = Metadata,
                Tiers = Tiers?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ExperienceModelId = ExperienceModelId,
            };
        }

        public static EzSeasonModel FromModel(Gs2.Gs2SeasonRating.Model.SeasonModel model)
        {
            return new EzSeasonModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Tiers = model.Tiers == null ? new List<Gs2.Unity.Gs2SeasonRating.Model.EzTierModel>() : model.Tiers.Select(v => {
                    return Gs2.Unity.Gs2SeasonRating.Model.EzTierModel.FromModel(v);
                }).ToList(),
                ExperienceModelId = model.ExperienceModelId == null ? null : model.ExperienceModelId,
            };
        }
    }
}