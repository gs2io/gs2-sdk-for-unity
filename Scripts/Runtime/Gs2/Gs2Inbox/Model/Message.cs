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

namespace Gs2.Gs2Inbox.Model
{
	[Preserve]
	public class Message
	{

        /** メッセージ */
        public string messageId { set; get; }

        /**
         * メッセージを設定
         *
         * @param messageId メッセージ
         * @return this
         */
        public Message WithMessageId(string messageId) {
            this.messageId = messageId;
            return this;
        }

        /** メッセージID */
        public string name { set; get; }

        /**
         * メッセージIDを設定
         *
         * @param name メッセージID
         * @return this
         */
        public Message WithName(string name) {
            this.name = name;
            return this;
        }

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Message WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** メッセージの内容に相当するメタデータ */
        public string metadata { set; get; }

        /**
         * メッセージの内容に相当するメタデータを設定
         *
         * @param metadata メッセージの内容に相当するメタデータ
         * @return this
         */
        public Message WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 既読状態 */
        public bool? isRead { set; get; }

        /**
         * 既読状態を設定
         *
         * @param isRead 既読状態
         * @return this
         */
        public Message WithIsRead(bool? isRead) {
            this.isRead = isRead;
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
        public Message WithReadAcquireActions(List<AcquireAction> readAcquireActions) {
            this.readAcquireActions = readAcquireActions;
            return this;
        }

        /** 作成日時 */
        public long? receivedAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param receivedAt 作成日時
         * @return this
         */
        public Message WithReceivedAt(long? receivedAt) {
            this.receivedAt = receivedAt;
            return this;
        }

        /** 最終更新日時 */
        public long? readAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param readAt 最終更新日時
         * @return this
         */
        public Message WithReadAt(long? readAt) {
            this.readAt = readAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.messageId != null)
            {
                writer.WritePropertyName("messageId");
                writer.Write(this.messageId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.isRead.HasValue)
            {
                writer.WritePropertyName("isRead");
                writer.Write(this.isRead.Value);
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
            if(this.receivedAt.HasValue)
            {
                writer.WritePropertyName("receivedAt");
                writer.Write(this.receivedAt.Value);
            }
            if(this.readAt.HasValue)
            {
                writer.WritePropertyName("readAt");
                writer.Write(this.readAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Message FromDict(JsonData data)
        {
            return new Message()
                .WithMessageId(data.Keys.Contains("messageId") && data["messageId"] != null ? data["messageId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithIsRead(data.Keys.Contains("isRead") && data["isRead"] != null ? (bool?)bool.Parse(data["isRead"].ToString()) : null)
                .WithReadAcquireActions(data.Keys.Contains("readAcquireActions") && data["readAcquireActions"] != null ? data["readAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null)
                .WithReceivedAt(data.Keys.Contains("receivedAt") && data["receivedAt"] != null ? (long?)long.Parse(data["receivedAt"].ToString()) : null)
                .WithReadAt(data.Keys.Contains("readAt") && data["readAt"] != null ? (long?)long.Parse(data["readAt"].ToString()) : null);
        }
	}
}