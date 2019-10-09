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

namespace Gs2.Gs2Identifier.Model
{
	[Preserve]
	public class Identifier
	{

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public Identifier WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** クライアントID */
        public string clientId { set; get; }

        /**
         * クライアントIDを設定
         *
         * @param clientId クライアントID
         * @return this
         */
        public Identifier WithClientId(string clientId) {
            this.clientId = clientId;
            return this;
        }

        /** ユーザー名 */
        public string userName { set; get; }

        /**
         * ユーザー名を設定
         *
         * @param userName ユーザー名
         * @return this
         */
        public Identifier WithUserName(string userName) {
            this.userName = userName;
            return this;
        }

        /** クライアントシークレット */
        public string clientSecret { set; get; }

        /**
         * クライアントシークレットを設定
         *
         * @param clientSecret クライアントシークレット
         * @return this
         */
        public Identifier WithClientSecret(string clientSecret) {
            this.clientSecret = clientSecret;
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
        public Identifier WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
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
            if(this.clientId != null)
            {
                writer.WritePropertyName("clientId");
                writer.Write(this.clientId);
            }
            if(this.userName != null)
            {
                writer.WritePropertyName("userName");
                writer.Write(this.userName);
            }
            if(this.clientSecret != null)
            {
                writer.WritePropertyName("clientSecret");
                writer.Write(this.clientSecret);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Identifier FromDict(JsonData data)
        {
            return new Identifier()
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithClientId(data.Keys.Contains("clientId") && data["clientId"] != null ? data["clientId"].ToString() : null)
                .WithUserName(data.Keys.Contains("userName") && data["userName"] != null ? data["userName"].ToString() : null)
                .WithClientSecret(data.Keys.Contains("clientSecret") && data["clientSecret"] != null ? data["clientSecret"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}