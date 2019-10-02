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
	public class EzMissionTaskModel
	{
		/** タスク名 */
		public string Name { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** カウンター名 */
		public string CounterName { get; set; }
		/** リセットタイミング */
		public string ResetType { get; set; }
		/** 目標値 */
		public long TargetValue { get; set; }
		/** ミッション達成時の報酬 */
		public List<EzAcquireAction> CompleteAcquireActions { get; set; }
		/** 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN */
		public string ChallengePeriodEventId { get; set; }
		/** このタスクに挑戦するために達成しておく必要のあるタスクの名前 */
		public string PremiseMissionTaskName { get; set; }

		public EzMissionTaskModel()
		{

		}

		public EzMissionTaskModel(Gs2.Gs2Mission.Model.MissionTaskModel @missionTaskModel)
		{
			Name = @missionTaskModel.name;
			Metadata = @missionTaskModel.metadata;
			CounterName = @missionTaskModel.counterName;
			ResetType = @missionTaskModel.resetType;
			TargetValue = @missionTaskModel.targetValue.HasValue ? @missionTaskModel.targetValue.Value : 0;
			CompleteAcquireActions = @missionTaskModel.completeAcquireActions != null ? @missionTaskModel.completeAcquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
			ChallengePeriodEventId = @missionTaskModel.challengePeriodEventId;
			PremiseMissionTaskName = @missionTaskModel.premiseMissionTaskName;
		}

        public MissionTaskModel ToModel()
        {
            return new MissionTaskModel {
                name = Name,
                metadata = Metadata,
                counterName = CounterName,
                resetType = ResetType,
                targetValue = TargetValue,
                completeAcquireActions = CompleteAcquireActions != null ? CompleteAcquireActions.Select(Value0 =>
                        {
                            return new AcquireAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                challengePeriodEventId = ChallengePeriodEventId,
                premiseMissionTaskName = PremiseMissionTaskName,
            };
        }
	}
}
