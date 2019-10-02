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
using Gs2.Gs2Limit.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Limit.Model
{
	[Preserve]
	public class EzLimitModel
	{
		/** 回数制限の種類 */
		public string LimitModelId { get; set; }
		/** 回数制限の種類名 */
		public string Name { get; set; }
		/** 回数制限の種類のメタデータ */
		public string Metadata { get; set; }
		/** リセットタイミング */
		public string ResetType { get; set; }
		/** リセットをする日にち */
		public int ResetDayOfMonth { get; set; }
		/** リセットする曜日 */
		public string ResetDayOfWeek { get; set; }
		/** リセット時刻 */
		public int ResetHour { get; set; }

		public EzLimitModel()
		{

		}

		public EzLimitModel(Gs2.Gs2Limit.Model.LimitModel @limitModel)
		{
			LimitModelId = @limitModel.limitModelId;
			Name = @limitModel.name;
			Metadata = @limitModel.metadata;
			ResetType = @limitModel.resetType;
			ResetDayOfMonth = @limitModel.resetDayOfMonth.HasValue ? @limitModel.resetDayOfMonth.Value : 0;
			ResetDayOfWeek = @limitModel.resetDayOfWeek;
			ResetHour = @limitModel.resetHour.HasValue ? @limitModel.resetHour.Value : 0;
		}

        public LimitModel ToModel()
        {
            return new LimitModel {
                limitModelId = LimitModelId,
                name = Name,
                metadata = Metadata,
                resetType = ResetType,
                resetDayOfMonth = ResetDayOfMonth,
                resetDayOfWeek = ResetDayOfWeek,
                resetHour = ResetHour,
            };
        }
	}
}
