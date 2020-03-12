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

namespace Gs2.Gs2Ranking.Model
{
	[Preserve]
	public class Subscribe
	{

        /** 購読 */
        public string subscribeId { set; get; }

        /**
         * 購読を設定
         *
         * @param subscribeId 購読
         * @return this
         */
        public Subscribe WithSubscribeId(string subscribeId) {
            this.subscribeId = subscribeId;
            return this;
        }

        /** カテゴリ名 */
        public string categoryName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param categoryName カテゴリ名
         * @return this
         */
        public Subscribe WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }

        /** 購読するユーザID */
        public string userId { set; get; }

        /**
         * 購読するユーザIDを設定
         *
         * @param userId 購読するユーザID
         * @return this
         */
        public Subscribe WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 購読されるユーザIDリスト */
        public List<string> targetUserIds { set; get; }

        /**
         * 購読されるユーザIDリストを設定
         *
         * @param targetUserIds 購読されるユーザIDリスト
         * @return this
         */
        public Subscribe WithTargetUserIds(List<string> targetUserIds) {
            this.targetUserIds = targetUserIds;
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
        public Subscribe WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.subscribeId != null)
            {
                writer.WritePropertyName("subscribeId");
                writer.Write(this.subscribeId);
            }
            if(this.categoryName != null)
            {
                writer.WritePropertyName("categoryName");
                writer.Write(this.categoryName);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.targetUserIds != null)
            {
                writer.WritePropertyName("targetUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.targetUserIds)
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Subscribe FromDict(JsonData data)
        {
            return new Subscribe()
                .WithSubscribeId(data.Keys.Contains("subscribeId") && data["subscribeId"] != null ? data["subscribeId"].ToString() : null)
                .WithCategoryName(data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithTargetUserIds(data.Keys.Contains("targetUserIds") && data["targetUserIds"] != null ? data["targetUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}