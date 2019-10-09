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

namespace Gs2.Gs2Friend.Model
{
	[Preserve]
	public class Inbox
	{

        /** フレンドリクエストの受信ボックス */
        public string inboxId { set; get; }

        /**
         * フレンドリクエストの受信ボックスを設定
         *
         * @param inboxId フレンドリクエストの受信ボックス
         * @return this
         */
        public Inbox WithInboxId(string inboxId) {
            this.inboxId = inboxId;
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
        public Inbox WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** フレンドリクエストを送ってきたユーザーIDリスト */
        public List<string> fromUserIds { set; get; }

        /**
         * フレンドリクエストを送ってきたユーザーIDリストを設定
         *
         * @param fromUserIds フレンドリクエストを送ってきたユーザーIDリスト
         * @return this
         */
        public Inbox WithFromUserIds(List<string> fromUserIds) {
            this.fromUserIds = fromUserIds;
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
        public Inbox WithCreatedAt(long? createdAt) {
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
        public Inbox WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.inboxId != null)
            {
                writer.WritePropertyName("inboxId");
                writer.Write(this.inboxId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.fromUserIds != null)
            {
                writer.WritePropertyName("fromUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.fromUserIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
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

    	[Preserve]
        public static Inbox FromDict(JsonData data)
        {
            return new Inbox()
                .WithInboxId(data.Keys.Contains("inboxId") && data["inboxId"] != null ? data["inboxId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithFromUserIds(data.Keys.Contains("fromUserIds") && data["fromUserIds"] != null ? data["fromUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}