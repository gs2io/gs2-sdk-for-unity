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
	public class EzStatus
	{
		/** 経験値の種類の名前 */
		public string ExperienceName { get; set; }
		/** プロパティID */
		public string PropertyId { get; set; }
		/** 累計獲得経験値 */
		public long ExperienceValue { get; set; }
		/** 現在のランク */
		public long RankValue { get; set; }
		/** 現在のランクキャップ */
		public long RankCapValue { get; set; }

		public EzStatus()
		{

		}

		public EzStatus(Gs2.Gs2Experience.Model.Status @status)
		{
			ExperienceName = @status.experienceName;
			PropertyId = @status.propertyId;
			ExperienceValue = @status.experienceValue.HasValue ? @status.experienceValue.Value : 0;
			RankValue = @status.rankValue.HasValue ? @status.rankValue.Value : 0;
			RankCapValue = @status.rankCapValue.HasValue ? @status.rankCapValue.Value : 0;
		}

        public Status ToModel()
        {
            return new Status {
                experienceName = ExperienceName,
                propertyId = PropertyId,
                experienceValue = ExperienceValue,
                rankValue = RankValue,
                rankCapValue = RankCapValue,
            };
        }
	}
}
