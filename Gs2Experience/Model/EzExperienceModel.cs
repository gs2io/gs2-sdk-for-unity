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

using Gs2.Gs2Experience.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Experience.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzExperienceModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long DefaultExperience;
		[SerializeField]
		public long DefaultRankCap;
		[SerializeField]
		public long MaxRankCap;
		[SerializeField]
		public Gs2.Unity.Gs2Experience.Model.EzThreshold RankThreshold;

        public Gs2.Gs2Experience.Model.ExperienceModel ToModel()
        {
            return new Gs2.Gs2Experience.Model.ExperienceModel {
                Name = Name,
                Metadata = Metadata,
                DefaultExperience = DefaultExperience,
                DefaultRankCap = DefaultRankCap,
                MaxRankCap = MaxRankCap,
                RankThreshold = RankThreshold?.ToModel(),
            };
        }

        public static EzExperienceModel FromModel(Gs2.Gs2Experience.Model.ExperienceModel model)
        {
            return new EzExperienceModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                DefaultExperience = model.DefaultExperience ?? 0,
                DefaultRankCap = model.DefaultRankCap ?? 0,
                MaxRankCap = model.MaxRankCap ?? 0,
                RankThreshold = model.RankThreshold == null ? null : Gs2.Unity.Gs2Experience.Model.EzThreshold.FromModel(model.RankThreshold),
            };
        }
    }
}