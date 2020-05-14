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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Mission.Model
{
	[Preserve]
	[System.Serializable]
	public class EzMissionGroupModel
	{
		/** グループ名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** タスクリスト */
		[UnityEngine.SerializeField]
		public List<EzMissionTaskModel> Tasks;
		/** リセットタイミング */
		[UnityEngine.SerializeField]
		public string ResetType;
		/** リセットをする日にち */
		[UnityEngine.SerializeField]
		public int ResetDayOfMonth;
		/** リセットする曜日 */
		[UnityEngine.SerializeField]
		public string ResetDayOfWeek;
		/** リセット時刻 */
		[UnityEngine.SerializeField]
		public int ResetHour;
		/** ミッションを達成したときの通知先ネームスペース のGRN */
		[UnityEngine.SerializeField]
		public string CompleteNotificationNamespaceId;

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
			ResetType = @missionGroupModel.resetType;
			ResetDayOfMonth = @missionGroupModel.resetDayOfMonth.HasValue ? @missionGroupModel.resetDayOfMonth.Value : 0;
			ResetDayOfWeek = @missionGroupModel.resetDayOfWeek;
			ResetHour = @missionGroupModel.resetHour.HasValue ? @missionGroupModel.resetHour.Value : 0;
			CompleteNotificationNamespaceId = @missionGroupModel.completeNotificationNamespaceId;
		}

        public virtual MissionGroupModel ToModel()
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
                resetType = ResetType,
                resetDayOfMonth = ResetDayOfMonth,
                resetDayOfWeek = ResetDayOfWeek,
                resetHour = ResetHour,
                completeNotificationNamespaceId = CompleteNotificationNamespaceId,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            if(this.Tasks != null)
            {
                writer.WritePropertyName("tasks");
                writer.WriteArrayStart();
                foreach(var item in this.Tasks)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.ResetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.ResetType);
            }
            writer.WritePropertyName("resetDayOfMonth");
            writer.Write(this.ResetDayOfMonth);
            if(this.ResetDayOfWeek != null)
            {
                writer.WritePropertyName("resetDayOfWeek");
                writer.Write(this.ResetDayOfWeek);
            }
            writer.WritePropertyName("resetHour");
            writer.Write(this.ResetHour);
            if(this.CompleteNotificationNamespaceId != null)
            {
                writer.WritePropertyName("completeNotificationNamespaceId");
                writer.Write(this.CompleteNotificationNamespaceId);
            }
            writer.WriteObjectEnd();
        }
	}
}
