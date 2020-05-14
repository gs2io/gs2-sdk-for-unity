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

namespace Gs2.Gs2Project.Model
{
	[Preserve]
	public class Partner
	{

        /** GS2パートナー */
        public string partnerId { set; get; }

        /**
         * GS2パートナーを設定
         *
         * @param partnerId GS2パートナー
         * @return this
         */
        public Partner WithPartnerId(string partnerId) {
            this.partnerId = partnerId;
            return this;
        }

        /** None */
        public string ownerId { set; get; }

        /**
         * Noneを設定
         *
         * @param ownerId None
         * @return this
         */
        public Partner WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** GS2パートナーの名前 */
        public string name { set; get; }

        /**
         * GS2パートナーの名前を設定
         *
         * @param name GS2パートナーの名前
         * @return this
         */
        public Partner WithName(string name) {
            this.name = name;
            return this;
        }

        /** メールアドレス */
        public string email { set; get; }

        /**
         * メールアドレスを設定
         *
         * @param email メールアドレス
         * @return this
         */
        public Partner WithEmail(string email) {
            this.email = email;
            return this;
        }

        /** 会社名 */
        public string companyName { set; get; }

        /**
         * 会社名を設定
         *
         * @param companyName 会社名
         * @return this
         */
        public Partner WithCompanyName(string companyName) {
            this.companyName = companyName;
            return this;
        }

        /** パスワード */
        public string password { set; get; }

        /**
         * パスワードを設定
         *
         * @param password パスワード
         * @return this
         */
        public Partner WithPassword(string password) {
            this.password = password;
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
        public Partner WithCreatedAt(long? createdAt) {
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
        public Partner WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.partnerId != null)
            {
                writer.WritePropertyName("partnerId");
                writer.Write(this.partnerId);
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
            if(this.email != null)
            {
                writer.WritePropertyName("email");
                writer.Write(this.email);
            }
            if(this.companyName != null)
            {
                writer.WritePropertyName("companyName");
                writer.Write(this.companyName);
            }
            if(this.password != null)
            {
                writer.WritePropertyName("password");
                writer.Write(this.password);
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
        public static Partner FromDict(JsonData data)
        {
            return new Partner()
                .WithPartnerId(data.Keys.Contains("partnerId") && data["partnerId"] != null ? data["partnerId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithEmail(data.Keys.Contains("email") && data["email"] != null ? data["email"].ToString() : null)
                .WithCompanyName(data.Keys.Contains("companyName") && data["companyName"] != null ? data["companyName"].ToString() : null)
                .WithPassword(data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}