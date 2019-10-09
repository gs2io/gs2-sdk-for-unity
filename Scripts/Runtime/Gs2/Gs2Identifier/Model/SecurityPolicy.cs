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
	public class SecurityPolicy
	{

        /** セキュリティポリシー */
        public string securityPolicyId { set; get; }

        /**
         * セキュリティポリシーを設定
         *
         * @param securityPolicyId セキュリティポリシー
         * @return this
         */
        public SecurityPolicy WithSecurityPolicyId(string securityPolicyId) {
            this.securityPolicyId = securityPolicyId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public SecurityPolicy WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** セキュリティポリシー名 */
        public string name { set; get; }

        /**
         * セキュリティポリシー名を設定
         *
         * @param name セキュリティポリシー名
         * @return this
         */
        public SecurityPolicy WithName(string name) {
            this.name = name;
            return this;
        }

        /** セキュリティポリシーの説明 */
        public string description { set; get; }

        /**
         * セキュリティポリシーの説明を設定
         *
         * @param description セキュリティポリシーの説明
         * @return this
         */
        public SecurityPolicy WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** ポリシードキュメント */
        public string policy { set; get; }

        /**
         * ポリシードキュメントを設定
         *
         * @param policy ポリシードキュメント
         * @return this
         */
        public SecurityPolicy WithPolicy(string policy) {
            this.policy = policy;
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
        public SecurityPolicy WithCreatedAt(long? createdAt) {
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
        public SecurityPolicy WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.securityPolicyId != null)
            {
                writer.WritePropertyName("securityPolicyId");
                writer.Write(this.securityPolicyId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
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
            if(this.policy != null)
            {
                writer.WritePropertyName("policy");
                writer.Write(this.policy);
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
        public static SecurityPolicy FromDict(JsonData data)
        {
            return new SecurityPolicy()
                .WithSecurityPolicyId(data.Keys.Contains("securityPolicyId") && data["securityPolicyId"] != null ? data["securityPolicyId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithPolicy(data.Keys.Contains("policy") && data["policy"] != null ? data["policy"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}