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
	public class EzMissionTaskModel
	{
		/** タスク名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** カウンター名 */
		[UnityEngine.SerializeField]
		public string CounterName;
		/** 目標値 */
		[UnityEngine.SerializeField]
		public long TargetValue;
		/** ミッション達成時の報酬 */
		[UnityEngine.SerializeField]
		public List<EzAcquireAction> CompleteAcquireActions;
		/** 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN */
		[UnityEngine.SerializeField]
		public string ChallengePeriodEventId;
		/** このタスクに挑戦するために達成しておく必要のあるタスクの名前 */
		[UnityEngine.SerializeField]
		public string PremiseMissionTaskName;

		public EzMissionTaskModel()
		{

		}

		public EzMissionTaskModel(Gs2.Gs2Mission.Model.MissionTaskModel @missionTaskModel)
		{
			Name = @missionTaskModel.name;
			Metadata = @missionTaskModel.metadata;
			CounterName = @missionTaskModel.counterName;
			TargetValue = @missionTaskModel.targetValue.HasValue ? @missionTaskModel.targetValue.Value : 0;
			CompleteAcquireActions = @missionTaskModel.completeAcquireActions != null ? @missionTaskModel.completeAcquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
			ChallengePeriodEventId = @missionTaskModel.challengePeriodEventId;
			PremiseMissionTaskName = @missionTaskModel.premiseMissionTaskName;
		}

        public virtual MissionTaskModel ToModel()
        {
            return new MissionTaskModel {
                name = Name,
                metadata = Metadata,
                counterName = CounterName,
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
            if(this.CounterName != null)
            {
                writer.WritePropertyName("counterName");
                writer.Write(this.CounterName);
            }
            writer.WritePropertyName("targetValue");
            writer.Write(this.TargetValue);
            if(this.CompleteAcquireActions != null)
            {
                writer.WritePropertyName("completeAcquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.CompleteAcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.ChallengePeriodEventId != null)
            {
                writer.WritePropertyName("challengePeriodEventId");
                writer.Write(this.ChallengePeriodEventId);
            }
            if(this.PremiseMissionTaskName != null)
            {
                writer.WritePropertyName("premiseMissionTaskName");
                writer.Write(this.PremiseMissionTaskName);
            }
            writer.WriteObjectEnd();
        }
	}
}
