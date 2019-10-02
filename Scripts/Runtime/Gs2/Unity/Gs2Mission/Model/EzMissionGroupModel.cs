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
	public class EzMissionGroupModel
	{
		/** グループ名 */
		public string Name { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** タスクリスト */
		public List<EzMissionTaskModel> Tasks { get; set; }
		/** ミッションを達成したときの通知先ネームスペース のGRN */
		public string CompleteNotificationNamespaceId { get; set; }

		public EzMissionGroupModel()
		{

		}

		public EzMissionGroupModel(Gs2.Gs2Mission.Model.MissionGroupModel @missionGroupModel)
		{
			Name = @missionGroupModel.name;
			Metadata = @missionGroupModel.metadata;
			Tasks = @missionGroupModel.tasks != null ? @missionGroupModel.tasks.Select(value =>
                {
                    return new EzMissionTaskModel(value);
                }
			).ToList() : new List<EzMissionTaskModel>(new EzMissionTaskModel[] {});
			CompleteNotificationNamespaceId = @missionGroupModel.completeNotificationNamespaceId;
		}

        public MissionGroupModel ToModel()
        {
            return new MissionGroupModel {
                name = Name,
                metadata = Metadata,
                tasks = Tasks != null ? Tasks.Select(Value0 =>
                        {
                            return new MissionTaskModel
                            {
                                name = Value0.Name,
                                metadata = Value0.Metadata,
                                counterName = Value0.CounterName,
                                resetType = Value0.ResetType,
                                targetValue = Value0.TargetValue,
                                completeAcquireActions = Value0.CompleteAcquireActions != null ? Value0.CompleteAcquireActions.Select(Value1 =>
                                        {
                                            return new AcquireAction
                                            {
                                                action = Value1.Action,
                                                request = Value1.Request,
                                            };
                                        }
                                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                                challengePeriodEventId = Value0.ChallengePeriodEventId,
                                premiseMissionTaskName = Value0.PremiseMissionTaskName,
                            };
                        }
                ).ToList() : new List<MissionTaskModel>(new MissionTaskModel[] {}),
                completeNotificationNamespaceId = CompleteNotificationNamespaceId,
            };
        }
	}
}
