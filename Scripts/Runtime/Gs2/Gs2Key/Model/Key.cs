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

namespace Gs2.Gs2Key.Model
{
	[Preserve]
	public class Key
	{

        /** 暗号鍵 */
        public string keyId { set; get; }

        /**
         * 暗号鍵を設定
         *
         * @param keyId 暗号鍵
         * @return this
         */
        public Key WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }

        /** 暗号鍵名 */
        public string name { set; get; }

        /**
         * 暗号鍵名を設定
         *
         * @param name 暗号鍵名
         * @return this
         */
        public Key WithName(string name) {
            this.name = name;
            return this;
        }

        /** 説明文 */
        public string description { set; get; }

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public Key WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 暗号鍵 */
        public string secret { set; get; }

        /**
         * 暗号鍵を設定
         *
         * @param secret 暗号鍵
         * @return this
         */
        public Key WithSecret(string secret) {
            this.secret = secret;
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
        public Key WithCreatedAt(long? createdAt) {
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
        public Key WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.keyId != null)
            {
                writer.WritePropertyName("keyId");
                writer.Write(this.keyId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.secret != null)
            {
                writer.WritePropertyName("secret");
                writer.Write(this.secret);
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
        public static Key FromDict(JsonData data)
        {
            return new Key()
                .WithKeyId(data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithSecret(data.Keys.Contains("secret") && data["secret"] != null ? data["secret"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}