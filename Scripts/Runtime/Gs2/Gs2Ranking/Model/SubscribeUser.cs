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
	public class SubscribeUser
	{

        /** カテゴリ名 */
        public string categoryName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param categoryName カテゴリ名
         * @return this
         */
        public SubscribeUser WithCategoryName(string categoryName) {
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
        public SubscribeUser WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 購読されるユーザID */
        public string targetUserId { set; get; }

        /**
         * 購読されるユーザIDを設定
         *
         * @param targetUserId 購読されるユーザID
         * @return this
         */
        public SubscribeUser WithTargetUserId(string targetUserId) {
            this.targetUserId = targetUserId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
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
            if(this.targetUserId != null)
            {
                writer.WritePropertyName("targetUserId");
                writer.Write(this.targetUserId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SubscribeUser FromDict(JsonData data)
        {
            return new SubscribeUser()
                .WithCategoryName(data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithTargetUserId(data.Keys.Contains("targetUserId") && data["targetUserId"] != null ? data["targetUserId"].ToString() : null);
        }
	}
}