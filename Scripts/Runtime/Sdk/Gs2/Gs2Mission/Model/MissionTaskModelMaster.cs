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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Model
{
	[Preserve]
	public class MissionTaskModelMaster : IComparable
	{

        /** ミッションタスクマスター */
        public string missionTaskId { set; get; }

        /**
         * ミッションタスクマスターを設定
         *
         * @param missionTaskId ミッションタスクマスター
         * @return this
         */
        public MissionTaskModelMaster WithMissionTaskId(string missionTaskId) {
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
        public MissionTaskModelMaster WithName(string name) {
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
        public MissionTaskModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** ミッションタスクの説明 */
        public string description { set; get; }

        /**
         * ミッションタスクの説明を設定
         *
         * @param description ミッションタスクの説明
         * @return this
         */
        public MissionTaskModelMaster WithDescription(string description) {
            this.description = description;
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
        public MissionTaskModelMaster WithCounterName(string counterName) {
            this.counterName = counterName;
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
        public MissionTaskModelMaster WithTargetValue(long? targetValue) {
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
        public MissionTaskModelMaster WithCompleteAcquireActions(List<AcquireAction> completeAcquireActions) {
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
        public MissionTaskModelMaster WithChallengePeriodEventId(string challengePeriodEventId) {
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
        public MissionTaskModelMaster WithPremiseMissionTaskName(string premiseMissionTaskName) {
            this.premiseMissionTaskName = premiseMissionTaskName;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public MissionTaskModelMaster WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public MissionTaskModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.counterName != null)
            {
                writer.WritePropertyName("counterName");
                writer.Write(this.counterName);
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
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetMissionTaskNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):group:(?<missionGroupName>.*):task:(?<missionTaskName>.*)");
        if (!match.Groups["missionTaskName"].Success)
        {
            return null;
        }
        return match.Groups["missionTaskName"].Value;
    }

    public static string GetMissionGroupNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):group:(?<missionGroupName>.*):task:(?<missionTaskName>.*)");
        if (!match.Groups["missionGroupName"].Success)
        {
            return null;
        }
        return match.Groups["missionGroupName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):group:(?<missionGroupName>.*):task:(?<missionTaskName>.*)");
        if (!match.Groups["namespaceName"].Success)
        {
            return null;
        }
        return match.Groups["namespaceName"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):group:(?<missionGroupName>.*):task:(?<missionTaskName>.*)");
        if (!match.Groups["ownerId"].Success)
        {
            return null;
        }
        return match.Groups["ownerId"].Value;
    }

    public static string GetRegionFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):group:(?<missionGroupName>.*):task:(?<missionTaskName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static MissionTaskModelMaster FromDict(JsonData data)
        {
            return new MissionTaskModelMaster()
                .WithMissionTaskId(data.Keys.Contains("missionTaskId") && data["missionTaskId"] != null ? data["missionTaskId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithCounterName(data.Keys.Contains("counterName") && data["counterName"] != null ? data["counterName"].ToString() : null)
                .WithTargetValue(data.Keys.Contains("targetValue") && data["targetValue"] != null ? (long?)long.Parse(data["targetValue"].ToString()) : null)
                .WithCompleteAcquireActions(data.Keys.Contains("completeAcquireActions") && data["completeAcquireActions"] != null ? data["completeAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Mission.Model.AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null)
                .WithPremiseMissionTaskName(data.Keys.Contains("premiseMissionTaskName") && data["premiseMissionTaskName"] != null ? data["premiseMissionTaskName"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as MissionTaskModelMaster;
            var diff = 0;
            if (missionTaskId == null && missionTaskId == other.missionTaskId)
            {
                // null and null
            }
            else
            {
                diff += missionTaskId.CompareTo(other.missionTaskId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (metadata == null && metadata == other.metadata)
            {
                // null and null
            }
            else
            {
                diff += metadata.CompareTo(other.metadata);
            }
            if (description == null && description == other.description)
            {
                // null and null
            }
            else
            {
                diff += description.CompareTo(other.description);
            }
            if (counterName == null && counterName == other.counterName)
            {
                // null and null
            }
            else
            {
                diff += counterName.CompareTo(other.counterName);
            }
            if (targetValue == null && targetValue == other.targetValue)
            {
                // null and null
            }
            else
            {
                diff += (int)(targetValue - other.targetValue);
            }
            if (completeAcquireActions == null && completeAcquireActions == other.completeAcquireActions)
            {
                // null and null
            }
            else
            {
                diff += completeAcquireActions.Count - other.completeAcquireActions.Count;
                for (var i = 0; i < completeAcquireActions.Count; i++)
                {
                    diff += completeAcquireActions[i].CompareTo(other.completeAcquireActions[i]);
                }
            }
            if (challengePeriodEventId == null && challengePeriodEventId == other.challengePeriodEventId)
            {
                // null and null
            }
            else
            {
                diff += challengePeriodEventId.CompareTo(other.challengePeriodEventId);
            }
            if (premiseMissionTaskName == null && premiseMissionTaskName == other.premiseMissionTaskName)
            {
                // null and null
            }
            else
            {
                diff += premiseMissionTaskName.CompareTo(other.premiseMissionTaskName);
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            if (updatedAt == null && updatedAt == other.updatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(updatedAt - other.updatedAt);
            }
            return diff;
        }
	}
}