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

namespace Gs2.Gs2Limit.Model
{
	[Preserve]
	public class Counter
	{

        /** カウンター */
        public string counterId { set; get; }

        /**
         * カウンターを設定
         *
         * @param counterId カウンター
         * @return this
         */
        public Counter WithCounterId(string counterId) {
            this.counterId = counterId;
            return this;
        }

        /** 回数制限の種類の名前 */
        public string limitName { set; get; }

        /**
         * 回数制限の種類の名前を設定
         *
         * @param limitName 回数制限の種類の名前
         * @return this
         */
        public Counter WithLimitName(string limitName) {
            this.limitName = limitName;
            return this;
        }

        /** カウンターの名前 */
        public string name { set; get; }

        /**
         * カウンターの名前を設定
         *
         * @param name カウンターの名前
         * @return this
         */
        public Counter WithName(string name) {
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
        public Counter WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** カウント値 */
        public int? count { set; get; }

        /**
         * カウント値を設定
         *
         * @param count カウント値
         * @return this
         */
        public Counter WithCount(int? count) {
            this.count = count;
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
        public Counter WithCreatedAt(long? createdAt) {
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
        public Counter WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.counterId != null)
            {
                writer.WritePropertyName("counterId");
                writer.Write(this.counterId);
            }
            if(this.limitName != null)
            {
                writer.WritePropertyName("limitName");
                writer.Write(this.limitName);
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
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
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
        public static Counter FromDict(JsonData data)
        {
            return new Counter()
                .WithCounterId(data.Keys.Contains("counterId") && data["counterId"] != null ? data["counterId"].ToString() : null)
                .WithLimitName(data.Keys.Contains("limitName") && data["limitName"] != null ? data["limitName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}