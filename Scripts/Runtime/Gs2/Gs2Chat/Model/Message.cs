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

namespace Gs2.Gs2Chat.Model
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

        /** ルーム名 */
        public string roomName { set; get; }

        /**
         * ルーム名を設定
         *
         * @param roomName ルーム名
         * @return this
         */
        public Message WithRoomName(string roomName) {
            this.roomName = roomName;
            return this;
        }

        /** メッセージ名 */
        public string name { set; get; }

        /**
         * メッセージ名を設定
         *
         * @param name メッセージ名
         * @return this
         */
        public Message WithName(string name) {
            this.name = name;
            return this;
        }

        /** 発言したユーザID */
        public string userId { set; get; }

        /**
         * 発言したユーザIDを設定
         *
         * @param userId 発言したユーザID
         * @return this
         */
        public Message WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** メッセージの種類を分類したい時の種類番号 */
        public int? category { set; get; }

        /**
         * メッセージの種類を分類したい時の種類番号を設定
         *
         * @param category メッセージの種類を分類したい時の種類番号
         * @return this
         */
        public Message WithCategory(int? category) {
            this.category = category;
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
        public Message WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public Message WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
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
            if(this.roomName != null)
            {
                writer.WritePropertyName("roomName");
                writer.Write(this.roomName);
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
            if(this.category.HasValue)
            {
                writer.WritePropertyName("category");
                writer.Write(this.category.Value);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Message FromDict(JsonData data)
        {
            return new Message()
                .WithMessageId(data.Keys.Contains("messageId") && data["messageId"] != null ? data["messageId"].ToString() : null)
                .WithRoomName(data.Keys.Contains("roomName") && data["roomName"] != null ? data["roomName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithCategory(data.Keys.Contains("category") && data["category"] != null ? (int?)int.Parse(data["category"].ToString()) : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}