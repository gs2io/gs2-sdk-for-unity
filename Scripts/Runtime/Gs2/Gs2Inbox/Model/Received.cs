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
	public class Received
	{

        /** 受信済みグローバルメッセージ名 */
        public string receivedId { set; get; }

        /**
         * 受信済みグローバルメッセージ名を設定
         *
         * @param receivedId 受信済みグローバルメッセージ名
         * @return this
         */
        public Received WithReceivedId(string receivedId) {
            this.receivedId = receivedId;
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
        public Received WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 受信したグローバルメッセージ名 */
        public List<string> receivedGlobalMessageNames { set; get; }

        /**
         * 受信したグローバルメッセージ名を設定
         *
         * @param receivedGlobalMessageNames 受信したグローバルメッセージ名
         * @return this
         */
        public Received WithReceivedGlobalMessageNames(List<string> receivedGlobalMessageNames) {
            this.receivedGlobalMessageNames = receivedGlobalMessageNames;
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
        public Received WithCreatedAt(long? createdAt) {
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
        public Received WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.receivedId != null)
            {
                writer.WritePropertyName("receivedId");
                writer.Write(this.receivedId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.receivedGlobalMessageNames != null)
            {
                writer.WritePropertyName("receivedGlobalMessageNames");
                writer.WriteArrayStart();
                foreach(var item in this.receivedGlobalMessageNames)
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
        public static Received FromDict(JsonData data)
        {
            return new Received()
                .WithReceivedId(data.Keys.Contains("receivedId") && data["receivedId"] != null ? data["receivedId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithReceivedGlobalMessageNames(data.Keys.Contains("receivedGlobalMessageNames") && data["receivedGlobalMessageNames"] != null ? data["receivedGlobalMessageNames"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}