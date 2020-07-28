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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class Namespace : IComparable
	{

        /** ネームスペース */
        public string namespaceId { set; get; }

        /**
         * ネームスペースを設定
         *
         * @param namespaceId ネームスペース
         * @return this
         */
        public Namespace WithNamespaceId(string namespaceId) {
            this.namespaceId = namespaceId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public Namespace WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public Namespace WithName(string name) {
            this.name = name;
            return this;
        }

        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public Namespace WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** レーティング計算機能を使用するか */
        public bool? enableRating { set; get; }

        /**
         * レーティング計算機能を使用するかを設定
         *
         * @param enableRating レーティング計算機能を使用するか
         * @return this
         */
        public Namespace WithEnableRating(bool? enableRating) {
            this.enableRating = enableRating;
            return this;
        }

        /** ギャザリング新規作成時のアクション */
        public string createGatheringTriggerType { set; get; }

        /**
         * ギャザリング新規作成時のアクションを設定
         *
         * @param createGatheringTriggerType ギャザリング新規作成時のアクション
         * @return this
         */
        public Namespace WithCreateGatheringTriggerType(string createGatheringTriggerType) {
            this.createGatheringTriggerType = createGatheringTriggerType;
            return this;
        }

        /** ギャザリング新規作成時 にルームを作成するネームスペース のGRN */
        public string createGatheringTriggerRealtimeNamespaceId { set; get; }

        /**
         * ギャザリング新規作成時 にルームを作成するネームスペース のGRNを設定
         *
         * @param createGatheringTriggerRealtimeNamespaceId ギャザリング新規作成時 にルームを作成するネームスペース のGRN
         * @return this
         */
        public Namespace WithCreateGatheringTriggerRealtimeNamespaceId(string createGatheringTriggerRealtimeNamespaceId) {
            this.createGatheringTriggerRealtimeNamespaceId = createGatheringTriggerRealtimeNamespaceId;
            return this;
        }

        /** ギャザリング新規作成時 に実行されるスクリプト のGRN */
        public string createGatheringTriggerScriptId { set; get; }

        /**
         * ギャザリング新規作成時 に実行されるスクリプト のGRNを設定
         *
         * @param createGatheringTriggerScriptId ギャザリング新規作成時 に実行されるスクリプト のGRN
         * @return this
         */
        public Namespace WithCreateGatheringTriggerScriptId(string createGatheringTriggerScriptId) {
            this.createGatheringTriggerScriptId = createGatheringTriggerScriptId;
            return this;
        }

        /** マッチメイキング完了時のアクション */
        public string completeMatchmakingTriggerType { set; get; }

        /**
         * マッチメイキング完了時のアクションを設定
         *
         * @param completeMatchmakingTriggerType マッチメイキング完了時のアクション
         * @return this
         */
        public Namespace WithCompleteMatchmakingTriggerType(string completeMatchmakingTriggerType) {
            this.completeMatchmakingTriggerType = completeMatchmakingTriggerType;
            return this;
        }

        /** マッチメイキング完了時 にルームを作成するネームスペース のGRN */
        public string completeMatchmakingTriggerRealtimeNamespaceId { set; get; }

        /**
         * マッチメイキング完了時 にルームを作成するネームスペース のGRNを設定
         *
         * @param completeMatchmakingTriggerRealtimeNamespaceId マッチメイキング完了時 にルームを作成するネームスペース のGRN
         * @return this
         */
        public Namespace WithCompleteMatchmakingTriggerRealtimeNamespaceId(string completeMatchmakingTriggerRealtimeNamespaceId) {
            this.completeMatchmakingTriggerRealtimeNamespaceId = completeMatchmakingTriggerRealtimeNamespaceId;
            return this;
        }

        /** マッチメイキング完了時 に実行されるスクリプト のGRN */
        public string completeMatchmakingTriggerScriptId { set; get; }

        /**
         * マッチメイキング完了時 に実行されるスクリプト のGRNを設定
         *
         * @param completeMatchmakingTriggerScriptId マッチメイキング完了時 に実行されるスクリプト のGRN
         * @return this
         */
        public Namespace WithCompleteMatchmakingTriggerScriptId(string completeMatchmakingTriggerScriptId) {
            this.completeMatchmakingTriggerScriptId = completeMatchmakingTriggerScriptId;
            return this;
        }

        /** ギャザリングに新規プレイヤーが参加したときのプッシュ通知 */
        public Gs2.Gs2Matchmaking.Model.NotificationSetting joinNotification { set; get; }

        /**
         * ギャザリングに新規プレイヤーが参加したときのプッシュ通知を設定
         *
         * @param joinNotification ギャザリングに新規プレイヤーが参加したときのプッシュ通知
         * @return this
         */
        public Namespace WithJoinNotification(Gs2.Gs2Matchmaking.Model.NotificationSetting joinNotification) {
            this.joinNotification = joinNotification;
            return this;
        }

        /** ギャザリングからプレイヤーが離脱したときのプッシュ通知 */
        public Gs2.Gs2Matchmaking.Model.NotificationSetting leaveNotification { set; get; }

        /**
         * ギャザリングからプレイヤーが離脱したときのプッシュ通知を設定
         *
         * @param leaveNotification ギャザリングからプレイヤーが離脱したときのプッシュ通知
         * @return this
         */
        public Namespace WithLeaveNotification(Gs2.Gs2Matchmaking.Model.NotificationSetting leaveNotification) {
            this.leaveNotification = leaveNotification;
            return this;
        }

        /** マッチメイキングが完了したときのプッシュ通知 */
        public Gs2.Gs2Matchmaking.Model.NotificationSetting completeNotification { set; get; }

        /**
         * マッチメイキングが完了したときのプッシュ通知を設定
         *
         * @param completeNotification マッチメイキングが完了したときのプッシュ通知
         * @return this
         */
        public Namespace WithCompleteNotification(Gs2.Gs2Matchmaking.Model.NotificationSetting completeNotification) {
            this.completeNotification = completeNotification;
            return this;
        }

        /** ログの出力設定 */
        public Gs2.Gs2Matchmaking.Model.LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public Namespace WithLogSetting(Gs2.Gs2Matchmaking.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }

        /** None */
        public string status { set; get; }

        /**
         * Noneを設定
         *
         * @param status None
         * @return this
         */
        public Namespace WithStatus(string status) {
            this.status = status;
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
        public Namespace WithCreatedAt(long? createdAt) {
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
        public Namespace WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.namespaceId != null)
            {
                writer.WritePropertyName("namespaceId");
                writer.Write(this.namespaceId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.enableRating.HasValue)
            {
                writer.WritePropertyName("enableRating");
                writer.Write(this.enableRating.Value);
            }
            if(this.createGatheringTriggerType != null)
            {
                writer.WritePropertyName("createGatheringTriggerType");
                writer.Write(this.createGatheringTriggerType);
            }
            if(this.createGatheringTriggerRealtimeNamespaceId != null)
            {
                writer.WritePropertyName("createGatheringTriggerRealtimeNamespaceId");
                writer.Write(this.createGatheringTriggerRealtimeNamespaceId);
            }
            if(this.createGatheringTriggerScriptId != null)
            {
                writer.WritePropertyName("createGatheringTriggerScriptId");
                writer.Write(this.createGatheringTriggerScriptId);
            }
            if(this.completeMatchmakingTriggerType != null)
            {
                writer.WritePropertyName("completeMatchmakingTriggerType");
                writer.Write(this.completeMatchmakingTriggerType);
            }
            if(this.completeMatchmakingTriggerRealtimeNamespaceId != null)
            {
                writer.WritePropertyName("completeMatchmakingTriggerRealtimeNamespaceId");
                writer.Write(this.completeMatchmakingTriggerRealtimeNamespaceId);
            }
            if(this.completeMatchmakingTriggerScriptId != null)
            {
                writer.WritePropertyName("completeMatchmakingTriggerScriptId");
                writer.Write(this.completeMatchmakingTriggerScriptId);
            }
            if(this.joinNotification != null)
            {
                writer.WritePropertyName("joinNotification");
                this.joinNotification.WriteJson(writer);
            }
            if(this.leaveNotification != null)
            {
                writer.WritePropertyName("leaveNotification");
                this.leaveNotification.WriteJson(writer);
            }
            if(this.completeNotification != null)
            {
                writer.WritePropertyName("completeNotification");
                this.completeNotification.WriteJson(writer);
            }
            if(this.logSetting != null)
            {
                writer.WritePropertyName("logSetting");
                this.logSetting.WriteJson(writer);
            }
            if(this.status != null)
            {
                writer.WritePropertyName("status");
                writer.Write(this.status);
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

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Namespace FromDict(JsonData data)
        {
            return new Namespace()
                .WithNamespaceId(data.Keys.Contains("namespaceId") && data["namespaceId"] != null ? data["namespaceId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithEnableRating(data.Keys.Contains("enableRating") && data["enableRating"] != null ? (bool?)bool.Parse(data["enableRating"].ToString()) : null)
                .WithCreateGatheringTriggerType(data.Keys.Contains("createGatheringTriggerType") && data["createGatheringTriggerType"] != null ? data["createGatheringTriggerType"].ToString() : null)
                .WithCreateGatheringTriggerRealtimeNamespaceId(data.Keys.Contains("createGatheringTriggerRealtimeNamespaceId") && data["createGatheringTriggerRealtimeNamespaceId"] != null ? data["createGatheringTriggerRealtimeNamespaceId"].ToString() : null)
                .WithCreateGatheringTriggerScriptId(data.Keys.Contains("createGatheringTriggerScriptId") && data["createGatheringTriggerScriptId"] != null ? data["createGatheringTriggerScriptId"].ToString() : null)
                .WithCompleteMatchmakingTriggerType(data.Keys.Contains("completeMatchmakingTriggerType") && data["completeMatchmakingTriggerType"] != null ? data["completeMatchmakingTriggerType"].ToString() : null)
                .WithCompleteMatchmakingTriggerRealtimeNamespaceId(data.Keys.Contains("completeMatchmakingTriggerRealtimeNamespaceId") && data["completeMatchmakingTriggerRealtimeNamespaceId"] != null ? data["completeMatchmakingTriggerRealtimeNamespaceId"].ToString() : null)
                .WithCompleteMatchmakingTriggerScriptId(data.Keys.Contains("completeMatchmakingTriggerScriptId") && data["completeMatchmakingTriggerScriptId"] != null ? data["completeMatchmakingTriggerScriptId"].ToString() : null)
                .WithJoinNotification(data.Keys.Contains("joinNotification") && data["joinNotification"] != null ? Gs2.Gs2Matchmaking.Model.NotificationSetting.FromDict(data["joinNotification"]) : null)
                .WithLeaveNotification(data.Keys.Contains("leaveNotification") && data["leaveNotification"] != null ? Gs2.Gs2Matchmaking.Model.NotificationSetting.FromDict(data["leaveNotification"]) : null)
                .WithCompleteNotification(data.Keys.Contains("completeNotification") && data["completeNotification"] != null ? Gs2.Gs2Matchmaking.Model.NotificationSetting.FromDict(data["completeNotification"]) : null)
                .WithLogSetting(data.Keys.Contains("logSetting") && data["logSetting"] != null ? Gs2.Gs2Matchmaking.Model.LogSetting.FromDict(data["logSetting"]) : null)
                .WithStatus(data.Keys.Contains("status") && data["status"] != null ? data["status"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Namespace;
            var diff = 0;
            if (namespaceId == null && namespaceId == other.namespaceId)
            {
                // null and null
            }
            else
            {
                diff += namespaceId.CompareTo(other.namespaceId);
            }
            if (ownerId == null && ownerId == other.ownerId)
            {
                // null and null
            }
            else
            {
                diff += ownerId.CompareTo(other.ownerId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (description == null && description == other.description)
            {
                // null and null
            }
            else
            {
                diff += description.CompareTo(other.description);
            }
            if (enableRating == null && enableRating == other.enableRating)
            {
                // null and null
            }
            else
            {
                diff += enableRating == other.enableRating ? 0 : 1;
            }
            if (createGatheringTriggerType == null && createGatheringTriggerType == other.createGatheringTriggerType)
            {
                // null and null
            }
            else
            {
                diff += createGatheringTriggerType.CompareTo(other.createGatheringTriggerType);
            }
            if (createGatheringTriggerRealtimeNamespaceId == null && createGatheringTriggerRealtimeNamespaceId == other.createGatheringTriggerRealtimeNamespaceId)
            {
                // null and null
            }
            else
            {
                diff += createGatheringTriggerRealtimeNamespaceId.CompareTo(other.createGatheringTriggerRealtimeNamespaceId);
            }
            if (createGatheringTriggerScriptId == null && createGatheringTriggerScriptId == other.createGatheringTriggerScriptId)
            {
                // null and null
            }
            else
            {
                diff += createGatheringTriggerScriptId.CompareTo(other.createGatheringTriggerScriptId);
            }
            if (completeMatchmakingTriggerType == null && completeMatchmakingTriggerType == other.completeMatchmakingTriggerType)
            {
                // null and null
            }
            else
            {
                diff += completeMatchmakingTriggerType.CompareTo(other.completeMatchmakingTriggerType);
            }
            if (completeMatchmakingTriggerRealtimeNamespaceId == null && completeMatchmakingTriggerRealtimeNamespaceId == other.completeMatchmakingTriggerRealtimeNamespaceId)
            {
                // null and null
            }
            else
            {
                diff += completeMatchmakingTriggerRealtimeNamespaceId.CompareTo(other.completeMatchmakingTriggerRealtimeNamespaceId);
            }
            if (completeMatchmakingTriggerScriptId == null && completeMatchmakingTriggerScriptId == other.completeMatchmakingTriggerScriptId)
            {
                // null and null
            }
            else
            {
                diff += completeMatchmakingTriggerScriptId.CompareTo(other.completeMatchmakingTriggerScriptId);
            }
            if (joinNotification == null && joinNotification == other.joinNotification)
            {
                // null and null
            }
            else
            {
                diff += joinNotification.CompareTo(other.joinNotification);
            }
            if (leaveNotification == null && leaveNotification == other.leaveNotification)
            {
                // null and null
            }
            else
            {
                diff += leaveNotification.CompareTo(other.leaveNotification);
            }
            if (completeNotification == null && completeNotification == other.completeNotification)
            {
                // null and null
            }
            else
            {
                diff += completeNotification.CompareTo(other.completeNotification);
            }
            if (logSetting == null && logSetting == other.logSetting)
            {
                // null and null
            }
            else
            {
                diff += logSetting.CompareTo(other.logSetting);
            }
            if (status == null && status == other.status)
            {
                // null and null
            }
            else
            {
                diff += status.CompareTo(other.status);
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