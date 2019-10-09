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
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Model
{
	[Preserve]
	public class MissionTaskModel
	{

        /** ミッションタスク */
        public string missionTaskId { set; get; }

        /**
         * ミッションタスクを設定
         *
         * @param missionTaskId ミッションタスク
         * @return this
         */
        public MissionTaskModel WithMissionTaskId(string missionTaskId) {
            this.missionTaskId = missionTaskId;
            return this;
        }

        /** タスク名 */
        public string name { set; get; }

        /**
         * タスク名を設定
         *
         * @param name タスク名
         * @return this
         */
        public MissionTaskModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public MissionTaskModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** カウンター名 */
        public string counterName { set; get; }

        /**
         * カウンター名を設定
         *
         * @param counterName カウンター名
         * @return this
         */
        public MissionTaskModel WithCounterName(string counterName) {
            this.counterName = counterName;
            return this;
        }

        /** リセットタイミング */
        public string resetType { set; get; }

        /**
         * リセットタイミングを設定
         *
         * @param resetType リセットタイミング
         * @return this
         */
        public MissionTaskModel WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }

        /** 目標値 */
        public long? targetValue { set; get; }

        /**
         * 目標値を設定
         *
         * @param targetValue 目標値
         * @return this
         */
        public MissionTaskModel WithTargetValue(long? targetValue) {
            this.targetValue = targetValue;
            return this;
        }

        /** ミッション達成時の報酬 */
        public List<AcquireAction> completeAcquireActions { set; get; }

        /**
         * ミッション達成時の報酬を設定
         *
         * @param completeAcquireActions ミッション達成時の報酬
         * @return this
         */
        public MissionTaskModel WithCompleteAcquireActions(List<AcquireAction> completeAcquireActions) {
            this.completeAcquireActions = completeAcquireActions;
            return this;
        }

        /** 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN */
        public string challengePeriodEventId { set; get; }

        /**
         * 達成報酬の受け取り可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public MissionTaskModel WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }

        /** このタスクに挑戦するために達成しておく必要のあるタスクの名前 */
        public string premiseMissionTaskName { set; get; }

        /**
         * このタスクに挑戦するために達成しておく必要のあるタスクの名前を設定
         *
         * @param premiseMissionTaskName このタスクに挑戦するために達成しておく必要のあるタスクの名前
         * @return this
         */
        public MissionTaskModel WithPremiseMissionTaskName(string premiseMissionTaskName) {
            this.premiseMissionTaskName = premiseMissionTaskName;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.missionTaskId != null)
            {
                writer.WritePropertyName("missionTaskId");
                writer.Write(this.missionTaskId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.counterName != null)
            {
                writer.WritePropertyName("counterName");
                writer.Write(this.counterName);
            }
            if(this.resetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.resetType);
            }
            if(this.targetValue.HasValue)
            {
                writer.WritePropertyName("targetValue");
                writer.Write(this.targetValue.Value);
            }
            if(this.completeAcquireActions != null)
            {
                writer.WritePropertyName("completeAcquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.completeAcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.challengePeriodEventId != null)
            {
                writer.WritePropertyName("challengePeriodEventId");
                writer.Write(this.challengePeriodEventId);
            }
            if(this.premiseMissionTaskName != null)
            {
                writer.WritePropertyName("premiseMissionTaskName");
                writer.Write(this.premiseMissionTaskName);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static MissionTaskModel FromDict(JsonData data)
        {
            return new MissionTaskModel()
                .WithMissionTaskId(data.Keys.Contains("missionTaskId") && data["missionTaskId"] != null ? data["missionTaskId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCounterName(data.Keys.Contains("counterName") && data["counterName"] != null ? data["counterName"].ToString() : null)
                .WithResetType(data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString() : null)
                .WithTargetValue(data.Keys.Contains("targetValue") && data["targetValue"] != null ? (long?)long.Parse(data["targetValue"].ToString()) : null)
                .WithCompleteAcquireActions(data.Keys.Contains("completeAcquireActions") && data["completeAcquireActions"] != null ? data["completeAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null)
                .WithPremiseMissionTaskName(data.Keys.Contains("premiseMissionTaskName") && data["premiseMissionTaskName"] != null ? data["premiseMissionTaskName"].ToString() : null);
        }
	}
}