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

namespace Gs2.Gs2Friend.Model
{
	[Preserve]
	public class Namespace
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

        /** フォローされたときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting followScript { set; get; }

        /**
         * フォローされたときに実行するスクリプトを設定
         *
         * @param followScript フォローされたときに実行するスクリプト
         * @return this
         */
        public Namespace WithFollowScript(Gs2.Gs2Friend.Model.ScriptSetting followScript) {
            this.followScript = followScript;
            return this;
        }

        /** アンフォローされたときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting unfollowScript { set; get; }

        /**
         * アンフォローされたときに実行するスクリプトを設定
         *
         * @param unfollowScript アンフォローされたときに実行するスクリプト
         * @return this
         */
        public Namespace WithUnfollowScript(Gs2.Gs2Friend.Model.ScriptSetting unfollowScript) {
            this.unfollowScript = unfollowScript;
            return this;
        }

        /** フレンドリクエストを発行したときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting sendRequestScript { set; get; }

        /**
         * フレンドリクエストを発行したときに実行するスクリプトを設定
         *
         * @param sendRequestScript フレンドリクエストを発行したときに実行するスクリプト
         * @return this
         */
        public Namespace WithSendRequestScript(Gs2.Gs2Friend.Model.ScriptSetting sendRequestScript) {
            this.sendRequestScript = sendRequestScript;
            return this;
        }

        /** フレンドリクエストをキャンセルしたときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting cancelRequestScript { set; get; }

        /**
         * フレンドリクエストをキャンセルしたときに実行するスクリプトを設定
         *
         * @param cancelRequestScript フレンドリクエストをキャンセルしたときに実行するスクリプト
         * @return this
         */
        public Namespace WithCancelRequestScript(Gs2.Gs2Friend.Model.ScriptSetting cancelRequestScript) {
            this.cancelRequestScript = cancelRequestScript;
            return this;
        }

        /** フレンドリクエストを承諾したときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting acceptRequestScript { set; get; }

        /**
         * フレンドリクエストを承諾したときに実行するスクリプトを設定
         *
         * @param acceptRequestScript フレンドリクエストを承諾したときに実行するスクリプト
         * @return this
         */
        public Namespace WithAcceptRequestScript(Gs2.Gs2Friend.Model.ScriptSetting acceptRequestScript) {
            this.acceptRequestScript = acceptRequestScript;
            return this;
        }

        /** フレンドリクエストを拒否したときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting rejectRequestScript { set; get; }

        /**
         * フレンドリクエストを拒否したときに実行するスクリプトを設定
         *
         * @param rejectRequestScript フレンドリクエストを拒否したときに実行するスクリプト
         * @return this
         */
        public Namespace WithRejectRequestScript(Gs2.Gs2Friend.Model.ScriptSetting rejectRequestScript) {
            this.rejectRequestScript = rejectRequestScript;
            return this;
        }

        /** フレンドを削除したときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting deleteFriendScript { set; get; }

        /**
         * フレンドを削除したときに実行するスクリプトを設定
         *
         * @param deleteFriendScript フレンドを削除したときに実行するスクリプト
         * @return this
         */
        public Namespace WithDeleteFriendScript(Gs2.Gs2Friend.Model.ScriptSetting deleteFriendScript) {
            this.deleteFriendScript = deleteFriendScript;
            return this;
        }

        /** プロフィールを更新したときに実行するスクリプト */
        public Gs2.Gs2Friend.Model.ScriptSetting updateProfileScript { set; get; }

        /**
         * プロフィールを更新したときに実行するスクリプトを設定
         *
         * @param updateProfileScript プロフィールを更新したときに実行するスクリプト
         * @return this
         */
        public Namespace WithUpdateProfileScript(Gs2.Gs2Friend.Model.ScriptSetting updateProfileScript) {
            this.updateProfileScript = updateProfileScript;
            return this;
        }

        /** フォローされたときのプッシュ通知 */
        public Gs2.Gs2Friend.Model.NotificationSetting followNotification { set; get; }

        /**
         * フォローされたときのプッシュ通知を設定
         *
         * @param followNotification フォローされたときのプッシュ通知
         * @return this
         */
        public Namespace WithFollowNotification(Gs2.Gs2Friend.Model.NotificationSetting followNotification) {
            this.followNotification = followNotification;
            return this;
        }

        /** フレンドリクエストが届いたときのプッシュ通知 */
        public Gs2.Gs2Friend.Model.NotificationSetting receiveRequestNotification { set; get; }

        /**
         * フレンドリクエストが届いたときのプッシュ通知を設定
         *
         * @param receiveRequestNotification フレンドリクエストが届いたときのプッシュ通知
         * @return this
         */
        public Namespace WithReceiveRequestNotification(Gs2.Gs2Friend.Model.NotificationSetting receiveRequestNotification) {
            this.receiveRequestNotification = receiveRequestNotification;
            return this;
        }

        /** フレンドリクエストが承認されたときのプッシュ通知 */
        public Gs2.Gs2Friend.Model.NotificationSetting acceptRequestNotification { set; get; }

        /**
         * フレンドリクエストが承認されたときのプッシュ通知を設定
         *
         * @param acceptRequestNotification フレンドリクエストが承認されたときのプッシュ通知
         * @return this
         */
        public Namespace WithAcceptRequestNotification(Gs2.Gs2Friend.Model.NotificationSetting acceptRequestNotification) {
            this.acceptRequestNotification = acceptRequestNotification;
            return this;
        }

        /** ログの出力設定 */
        public Gs2.Gs2Friend.Model.LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public Namespace WithLogSetting(Gs2.Gs2Friend.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
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
            if(this.followScript != null)
            {
                writer.WritePropertyName("followScript");
                this.followScript.WriteJson(writer);
            }
            if(this.unfollowScript != null)
            {
                writer.WritePropertyName("unfollowScript");
                this.unfollowScript.WriteJson(writer);
            }
            if(this.sendRequestScript != null)
            {
                writer.WritePropertyName("sendRequestScript");
                this.sendRequestScript.WriteJson(writer);
            }
            if(this.cancelRequestScript != null)
            {
                writer.WritePropertyName("cancelRequestScript");
                this.cancelRequestScript.WriteJson(writer);
            }
            if(this.acceptRequestScript != null)
            {
                writer.WritePropertyName("acceptRequestScript");
                this.acceptRequestScript.WriteJson(writer);
            }
            if(this.rejectRequestScript != null)
            {
                writer.WritePropertyName("rejectRequestScript");
                this.rejectRequestScript.WriteJson(writer);
            }
            if(this.deleteFriendScript != null)
            {
                writer.WritePropertyName("deleteFriendScript");
                this.deleteFriendScript.WriteJson(writer);
            }
            if(this.updateProfileScript != null)
            {
                writer.WritePropertyName("updateProfileScript");
                this.updateProfileScript.WriteJson(writer);
            }
            if(this.followNotification != null)
            {
                writer.WritePropertyName("followNotification");
                this.followNotification.WriteJson(writer);
            }
            if(this.receiveRequestNotification != null)
            {
                writer.WritePropertyName("receiveRequestNotification");
                this.receiveRequestNotification.WriteJson(writer);
            }
            if(this.acceptRequestNotification != null)
            {
                writer.WritePropertyName("acceptRequestNotification");
                this.acceptRequestNotification.WriteJson(writer);
            }
            if(this.logSetting != null)
            {
                writer.WritePropertyName("logSetting");
                this.logSetting.WriteJson(writer);
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):friend:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):friend:(?<namespaceName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):friend:(?<namespaceName>.*)");
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
                .WithFollowScript(data.Keys.Contains("followScript") && data["followScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["followScript"]) : null)
                .WithUnfollowScript(data.Keys.Contains("unfollowScript") && data["unfollowScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["unfollowScript"]) : null)
                .WithSendRequestScript(data.Keys.Contains("sendRequestScript") && data["sendRequestScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["sendRequestScript"]) : null)
                .WithCancelRequestScript(data.Keys.Contains("cancelRequestScript") && data["cancelRequestScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["cancelRequestScript"]) : null)
                .WithAcceptRequestScript(data.Keys.Contains("acceptRequestScript") && data["acceptRequestScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["acceptRequestScript"]) : null)
                .WithRejectRequestScript(data.Keys.Contains("rejectRequestScript") && data["rejectRequestScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["rejectRequestScript"]) : null)
                .WithDeleteFriendScript(data.Keys.Contains("deleteFriendScript") && data["deleteFriendScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["deleteFriendScript"]) : null)
                .WithUpdateProfileScript(data.Keys.Contains("updateProfileScript") && data["updateProfileScript"] != null ? Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["updateProfileScript"]) : null)
                .WithFollowNotification(data.Keys.Contains("followNotification") && data["followNotification"] != null ? Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["followNotification"]) : null)
                .WithReceiveRequestNotification(data.Keys.Contains("receiveRequestNotification") && data["receiveRequestNotification"] != null ? Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["receiveRequestNotification"]) : null)
                .WithAcceptRequestNotification(data.Keys.Contains("acceptRequestNotification") && data["acceptRequestNotification"] != null ? Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["acceptRequestNotification"]) : null)
                .WithLogSetting(data.Keys.Contains("logSetting") && data["logSetting"] != null ? Gs2.Gs2Friend.Model.LogSetting.FromDict(data["logSetting"]) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}