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

namespace Gs2.Gs2Inbox.Model
{
	[Preserve]
	public class GlobalMessageMaster
	{

        /** 全ユーザに向けたメッセージ */
        public string globalMessageId { set; get; }

        /**
         * 全ユーザに向けたメッセージを設定
         *
         * @param globalMessageId 全ユーザに向けたメッセージ
         * @return this
         */
        public GlobalMessageMaster WithGlobalMessageId(string globalMessageId) {
            this.globalMessageId = globalMessageId;
            return this;
        }

        /** 全ユーザに向けたメッセージ名 */
        public string name { set; get; }

        /**
         * 全ユーザに向けたメッセージ名を設定
         *
         * @param name 全ユーザに向けたメッセージ名
         * @return this
         */
        public GlobalMessageMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 全ユーザに向けたメッセージの内容に相当するメタデータ */
        public string metadata { set; get; }

        /**
         * 全ユーザに向けたメッセージの内容に相当するメタデータを設定
         *
         * @param metadata 全ユーザに向けたメッセージの内容に相当するメタデータ
         * @return this
         */
        public GlobalMessageMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 開封時に実行する入手アクション */
        public List<AcquireAction> readAcquireActions { set; get; }

        /**
         * 開封時に実行する入手アクションを設定
         *
         * @param readAcquireActions 開封時に実行する入手アクション
         * @return this
         */
        public GlobalMessageMaster WithReadAcquireActions(List<AcquireAction> readAcquireActions) {
            this.readAcquireActions = readAcquireActions;
            return this;
        }

        /** メッセージを受信したあとメッセージが削除されるまでの期間 */
        public Gs2.Gs2Inbox.Model.TimeSpan expiresTimeSpan { set; get; }

        /**
         * メッセージを受信したあとメッセージが削除されるまでの期間を設定
         *
         * @param expiresTimeSpan メッセージを受信したあとメッセージが削除されるまでの期間
         * @return this
         */
        public GlobalMessageMaster WithExpiresTimeSpan(Gs2.Gs2Inbox.Model.TimeSpan expiresTimeSpan) {
            this.expiresTimeSpan = expiresTimeSpan;
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
        public GlobalMessageMaster WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 全ユーザに向けたメッセージの受信期限 */
        public long? expiresAt { set; get; }

        /**
         * 全ユーザに向けたメッセージの受信期限を設定
         *
         * @param expiresAt 全ユーザに向けたメッセージの受信期限
         * @return this
         */
        public GlobalMessageMaster WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.globalMessageId != null)
            {
                writer.WritePropertyName("globalMessageId");
                writer.Write(this.globalMessageId);
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
            if(this.readAcquireActions != null)
            {
                writer.WritePropertyName("readAcquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.readAcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.expiresTimeSpan != null)
            {
                writer.WritePropertyName("expiresTimeSpan");
                this.expiresTimeSpan.WriteJson(writer);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.expiresAt.HasValue)
            {
                writer.WritePropertyName("expiresAt");
                writer.Write(this.expiresAt.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetGlobalMessageNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inbox:(?<namespaceName>.*):master:globalMessage:(?<globalMessageName>.*)");
        if (!match.Groups["globalMessageName"].Success)
        {
            return null;
        }
        return match.Groups["globalMessageName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inbox:(?<namespaceName>.*):master:globalMessage:(?<globalMessageName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inbox:(?<namespaceName>.*):master:globalMessage:(?<globalMessageName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inbox:(?<namespaceName>.*):master:globalMessage:(?<globalMessageName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static GlobalMessageMaster FromDict(JsonData data)
        {
            return new GlobalMessageMaster()
                .WithGlobalMessageId(data.Keys.Contains("globalMessageId") && data["globalMessageId"] != null ? data["globalMessageId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithReadAcquireActions(data.Keys.Contains("readAcquireActions") && data["readAcquireActions"] != null ? data["readAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Inbox.Model.AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithExpiresTimeSpan(data.Keys.Contains("expiresTimeSpan") && data["expiresTimeSpan"] != null ? Gs2.Gs2Inbox.Model.TimeSpan.FromDict(data["expiresTimeSpan"]) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithExpiresAt(data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null);
        }
	}
}