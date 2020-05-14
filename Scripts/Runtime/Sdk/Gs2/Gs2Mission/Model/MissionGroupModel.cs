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
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Model
{
	[Preserve]
	public class MissionGroupModel
	{

        /** ミッショングループ */
        public string missionGroupId { set; get; }

        /**
         * ミッショングループを設定
         *
         * @param missionGroupId ミッショングループ
         * @return this
         */
        public MissionGroupModel WithMissionGroupId(string missionGroupId) {
            this.missionGroupId = missionGroupId;
            return this;
        }

        /** グループ名 */
        public string name { set; get; }

        /**
         * グループ名を設定
         *
         * @param name グループ名
         * @return this
         */
        public MissionGroupModel WithName(string name) {
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
        public MissionGroupModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** タスクリスト */
        public List<MissionTaskModel> tasks { set; get; }

        /**
         * タスクリストを設定
         *
         * @param tasks タスクリスト
         * @return this
         */
        public MissionGroupModel WithTasks(List<MissionTaskModel> tasks) {
            this.tasks = tasks;
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
        public MissionGroupModel WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }

        /** リセットをする日にち */
        public int? resetDayOfMonth { set; get; }

        /**
         * リセットをする日にちを設定
         *
         * @param resetDayOfMonth リセットをする日にち
         * @return this
         */
        public MissionGroupModel WithResetDayOfMonth(int? resetDayOfMonth) {
            this.resetDayOfMonth = resetDayOfMonth;
            return this;
        }

        /** リセットする曜日 */
        public string resetDayOfWeek { set; get; }

        /**
         * リセットする曜日を設定
         *
         * @param resetDayOfWeek リセットする曜日
         * @return this
         */
        public MissionGroupModel WithResetDayOfWeek(string resetDayOfWeek) {
            this.resetDayOfWeek = resetDayOfWeek;
            return this;
        }

        /** リセット時刻 */
        public int? resetHour { set; get; }

        /**
         * リセット時刻を設定
         *
         * @param resetHour リセット時刻
         * @return this
         */
        public MissionGroupModel WithResetHour(int? resetHour) {
            this.resetHour = resetHour;
            return this;
        }

        /** ミッションを達成したときの通知先ネームスペース のGRN */
        public string completeNotificationNamespaceId { set; get; }

        /**
         * ミッションを達成したときの通知先ネームスペース のGRNを設定
         *
         * @param completeNotificationNamespaceId ミッションを達成したときの通知先ネームスペース のGRN
         * @return this
         */
        public MissionGroupModel WithCompleteNotificationNamespaceId(string completeNotificationNamespaceId) {
            this.completeNotificationNamespaceId = completeNotificationNamespaceId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.missionGroupId != null)
            {
                writer.WritePropertyName("missionGroupId");
                writer.Write(this.missionGroupId);
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
            if(this.tasks != null)
            {
                writer.WritePropertyName("tasks");
                writer.WriteArrayStart();
                foreach(var item in this.tasks)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.resetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.resetType);
            }
            if(this.resetDayOfMonth.HasValue)
            {
                writer.WritePropertyName("resetDayOfMonth");
                writer.Write(this.resetDayOfMonth.Value);
            }
            if(this.resetDayOfWeek != null)
            {
                writer.WritePropertyName("resetDayOfWeek");
                writer.Write(this.resetDayOfWeek);
            }
            if(this.resetHour.HasValue)
            {
                writer.WritePropertyName("resetHour");
                writer.Write(this.resetHour.Value);
            }
            if(this.completeNotificationNamespaceId != null)
            {
                writer.WritePropertyName("completeNotificationNamespaceId");
                writer.Write(this.completeNotificationNamespaceId);
            }
            writer.WriteObjectEnd();
        }

    public static string GetMissionGroupNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):mission:(?<namespaceName>.*):");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static MissionGroupModel FromDict(JsonData data)
        {
            return new MissionGroupModel()
                .WithMissionGroupId(data.Keys.Contains("missionGroupId") && data["missionGroupId"] != null ? data["missionGroupId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithTasks(data.Keys.Contains("tasks") && data["tasks"] != null ? data["tasks"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Mission.Model.MissionTaskModel.FromDict(value);
                    }
                ).ToList() : null)
                .WithResetType(data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString() : null)
                .WithResetDayOfMonth(data.Keys.Contains("resetDayOfMonth") && data["resetDayOfMonth"] != null ? (int?)int.Parse(data["resetDayOfMonth"].ToString()) : null)
                .WithResetDayOfWeek(data.Keys.Contains("resetDayOfWeek") && data["resetDayOfWeek"] != null ? data["resetDayOfWeek"].ToString() : null)
                .WithResetHour(data.Keys.Contains("resetHour") && data["resetHour"] != null ? (int?)int.Parse(data["resetHour"].ToString()) : null)
                .WithCompleteNotificationNamespaceId(data.Keys.Contains("completeNotificationNamespaceId") && data["completeNotificationNamespaceId"] != null ? data["completeNotificationNamespaceId"].ToString() : null);
        }
	}
}