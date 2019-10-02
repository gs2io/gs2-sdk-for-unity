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
	public class EzCounterModel
	{
		/** カウンター名 */
		public string Name { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** カウンターのリセットタイミング */
		public List<EzCounterScopeModel> Scopes { get; set; }
		/** カウントアップ可能な期間を指定するイベントマスター のGRN */
		public string ChallengePeriodEventId { get; set; }

		public EzCounterModel()
		{

		}

		public EzCounterModel(Gs2.Gs2Mission.Model.CounterModel @counterModel)
		{
			Name = @counterModel.name;
			Metadata = @counterModel.metadata;
			Scopes = @counterModel.scopes != null ? @counterModel.scopes.Select(value =>
                {
                    return new EzCounterScopeModel(value);
                }
			).ToList() : new List<EzCounterScopeModel>(new EzCounterScopeModel[] {});
			ChallengePeriodEventId = @counterModel.challengePeriodEventId;
		}

        public CounterModel ToModel()
        {
            return new CounterModel {
                name = Name,
                metadata = Metadata,
                scopes = Scopes != null ? Scopes.Select(Value0 =>
                        {
                            return new CounterScopeModel
                            {
                                resetType = Value0.ResetType,
                                resetDayOfMonth = Value0.ResetDayOfMonth,
                                resetDayOfWeek = Value0.ResetDayOfWeek,
                                resetHour = Value0.ResetHour,
                            };
                        }
                ).ToList() : new List<CounterScopeModel>(new CounterScopeModel[] {}),
                challengePeriodEventId = ChallengePeriodEventId,
            };
        }
	}
}
