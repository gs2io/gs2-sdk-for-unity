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
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Experience.Model
{
	[Preserve]
	public class EzExperienceModel
	{
		/** 経験値の種類名 */
		public string Name { get; set; }
		/** 経験値の種類のメタデータ */
		public string Metadata { get; set; }
		/** 経験値の初期値 */
		public long DefaultExperience { get; set; }
		/** ランクキャップの初期値 */
		public long DefaultRankCap { get; set; }
		/** ランクキャップの最大値 */
		public long MaxRankCap { get; set; }
		/** ランクアップ閾値 */
		public EzThreshold RankThreshold { get; set; }

		public EzExperienceModel()
		{

		}

		public EzExperienceModel(Gs2.Gs2Experience.Model.ExperienceModel @experienceModel)
		{
			Name = @experienceModel.name;
			Metadata = @experienceModel.metadata;
			DefaultExperience = @experienceModel.defaultExperience.HasValue ? @experienceModel.defaultExperience.Value : 0;
			DefaultRankCap = @experienceModel.defaultRankCap.HasValue ? @experienceModel.defaultRankCap.Value : 0;
			MaxRankCap = @experienceModel.maxRankCap.HasValue ? @experienceModel.maxRankCap.Value : 0;
			RankThreshold = @experienceModel.rankThreshold != null ? new EzThreshold(@experienceModel.rankThreshold) : null;
		}

        public ExperienceModel ToModel()
        {
            return new ExperienceModel {
                name = Name,
                metadata = Metadata,
                defaultExperience = DefaultExperience,
                defaultRankCap = DefaultRankCap,
                maxRankCap = MaxRankCap,
                rankThreshold = new Threshold {
                    metadata = RankThreshold.Metadata,
                    values = RankThreshold.Values != null ? RankThreshold.Values.Select(Value1 =>
                            {
                                return (long?)Value1;
                            }
                    ).ToList() : new List<long?>(new long?[] {}),
                },
            };
        }
	}
}
