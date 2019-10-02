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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Mission.Model
{
	[Preserve]
	public class EzCounterScopeModel
	{
		/** リセットタイミング */
		public string ResetType { get; set; }
		/** リセットをする日にち */
		public int ResetDayOfMonth { get; set; }
		/** リセットする曜日 */
		public string ResetDayOfWeek { get; set; }
		/** リセット時刻 */
		public int ResetHour { get; set; }

		public EzCounterScopeModel()
		{

		}

		public EzCounterScopeModel(Gs2.Gs2Mission.Model.CounterScopeModel @counterScopeModel)
		{
			ResetType = @counterScopeModel.resetType;
			ResetDayOfMonth = @counterScopeModel.resetDayOfMonth.HasValue ? @counterScopeModel.resetDayOfMonth.Value : 0;
			ResetDayOfWeek = @counterScopeModel.resetDayOfWeek;
			ResetHour = @counterScopeModel.resetHour.HasValue ? @counterScopeModel.resetHour.Value : 0;
		}

        public CounterScopeModel ToModel()
        {
            return new CounterScopeModel {
                resetType = ResetType,
                resetDayOfMonth = ResetDayOfMonth,
                resetDayOfWeek = ResetDayOfWeek,
                resetHour = ResetHour,
            };
        }
	}
}
