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

namespace Gs2.Gs2Auth.Model
{
	[Preserve]
	public class AccessToken
	{

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public AccessToken WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** アクセストークン */
        public string token { set; get; }

        /**
         * アクセストークンを設定
         *
         * @param token アクセストークン
         * @return this
         */
        public AccessToken WithToken(string token) {
            this.token = token;
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
        public AccessToken WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 有効期限 */
        public long? expire { set; get; }

        /**
         * 有効期限を設定
         *
         * @param expire 有効期限
         * @return this
         */
        public AccessToken WithExpire(long? expire) {
            this.expire = expire;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.token != null)
            {
                writer.WritePropertyName("token");
                writer.Write(this.token);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.expire.HasValue)
            {
                writer.WritePropertyName("expire");
                writer.Write(this.expire.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static AccessToken FromDict(JsonData data)
        {
            return new AccessToken()
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithToken(data.Keys.Contains("token") && data["token"] != null ? data["token"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithExpire(data.Keys.Contains("expire") && data["expire"] != null ? (long?)long.Parse(data["expire"].ToString()) : null);
        }
	}
}