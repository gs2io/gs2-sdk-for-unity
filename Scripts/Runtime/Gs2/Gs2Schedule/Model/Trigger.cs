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

namespace Gs2.Gs2Schedule.Model
{
	[Preserve]
	public class Trigger
	{

        /** トリガー */
        public string triggerId { set; get; }

        /**
         * トリガーを設定
         *
         * @param triggerId トリガー
         * @return this
         */
        public Trigger WithTriggerId(string triggerId) {
            this.triggerId = triggerId;
            return this;
        }

        /** トリガーの名前 */
        public string name { set; get; }

        /**
         * トリガーの名前を設定
         *
         * @param name トリガーの名前
         * @return this
         */
        public Trigger WithName(string name) {
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
        public Trigger WithUserId(string userId) {
            this.userId = userId;
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
        public Trigger WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** トリガーの有効期限 */
        public long? expiresAt { set; get; }

        /**
         * トリガーの有効期限を設定
         *
         * @param expiresAt トリガーの有効期限
         * @return this
         */
        public Trigger WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.triggerId != null)
            {
                writer.WritePropertyName("triggerId");
                writer.Write(this.triggerId);
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

    	[Preserve]
        public static Trigger FromDict(JsonData data)
        {
            return new Trigger()
                .WithTriggerId(data.Keys.Contains("triggerId") && data["triggerId"] != null ? data["triggerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithExpiresAt(data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null);
        }
	}
}