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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Model
{
	[Preserve]
	public class BillingMethod
	{

        /** 支払い方法 */
        public string billingMethodId { set; get; }

        /**
         * 支払い方法を設定
         *
         * @param billingMethodId 支払い方法
         * @return this
         */
        public BillingMethod WithBillingMethodId(string billingMethodId) {
            this.billingMethodId = billingMethodId;
            return this;
        }

        /** GS2アカウントの名前 */
        public string accountName { set; get; }

        /**
         * GS2アカウントの名前を設定
         *
         * @param accountName GS2アカウントの名前
         * @return this
         */
        public BillingMethod WithAccountName(string accountName) {
            this.accountName = accountName;
            return this;
        }

        /** 名前 */
        public string name { set; get; }

        /**
         * 名前を設定
         *
         * @param name 名前
         * @return this
         */
        public BillingMethod WithName(string name) {
            this.name = name;
            return this;
        }

        /** 名前 */
        public string description { set; get; }

        /**
         * 名前を設定
         *
         * @param description 名前
         * @return this
         */
        public BillingMethod WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 支払い方法 */
        public string methodType { set; get; }

        /**
         * 支払い方法を設定
         *
         * @param methodType 支払い方法
         * @return this
         */
        public BillingMethod WithMethodType(string methodType) {
            this.methodType = methodType;
            return this;
        }

        /** クレジットカードカスタマーID */
        public string cardCustomerId { set; get; }

        /**
         * クレジットカードカスタマーIDを設定
         *
         * @param cardCustomerId クレジットカードカスタマーID
         * @return this
         */
        public BillingMethod WithCardCustomerId(string cardCustomerId) {
            this.cardCustomerId = cardCustomerId;
            return this;
        }

        /** カード署名 */
        public string cardSignatureName { set; get; }

        /**
         * カード署名を設定
         *
         * @param cardSignatureName カード署名
         * @return this
         */
        public BillingMethod WithCardSignatureName(string cardSignatureName) {
            this.cardSignatureName = cardSignatureName;
            return this;
        }

        /** カードブランド */
        public string cardBrand { set; get; }

        /**
         * カードブランドを設定
         *
         * @param cardBrand カードブランド
         * @return this
         */
        public BillingMethod WithCardBrand(string cardBrand) {
            this.cardBrand = cardBrand;
            return this;
        }

        /** 末尾4桁 */
        public string cardLast4 { set; get; }

        /**
         * 末尾4桁を設定
         *
         * @param cardLast4 末尾4桁
         * @return this
         */
        public BillingMethod WithCardLast4(string cardLast4) {
            this.cardLast4 = cardLast4;
            return this;
        }

        /** パートナーID */
        public string partnerId { set; get; }

        /**
         * パートナーIDを設定
         *
         * @param partnerId パートナーID
         * @return this
         */
        public BillingMethod WithPartnerId(string partnerId) {
            this.partnerId = partnerId;
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
        public BillingMethod WithCreatedAt(long? createdAt) {
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
        public BillingMethod WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.billingMethodId != null)
            {
                writer.WritePropertyName("billingMethodId");
                writer.Write(this.billingMethodId);
            }
            if(this.accountName != null)
            {
                writer.WritePropertyName("accountName");
                writer.Write(this.accountName);
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
            if(this.methodType != null)
            {
                writer.WritePropertyName("methodType");
                writer.Write(this.methodType);
            }
            if(this.cardCustomerId != null)
            {
                writer.WritePropertyName("cardCustomerId");
                writer.Write(this.cardCustomerId);
            }
            if(this.cardSignatureName != null)
            {
                writer.WritePropertyName("cardSignatureName");
                writer.Write(this.cardSignatureName);
            }
            if(this.cardBrand != null)
            {
                writer.WritePropertyName("cardBrand");
                writer.Write(this.cardBrand);
            }
            if(this.cardLast4 != null)
            {
                writer.WritePropertyName("cardLast4");
                writer.Write(this.cardLast4);
            }
            if(this.partnerId != null)
            {
                writer.WritePropertyName("partnerId");
                writer.Write(this.partnerId);
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

    public static string GetBillingMethodNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):billingMethod:(?<billingMethodName>.*)");
        if (!match.Groups["billingMethodName"].Success)
        {
            return null;
        }
        return match.Groups["billingMethodName"].Value;
    }

    public static string GetAccountNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):billingMethod:(?<billingMethodName>.*)");
        if (!match.Groups["accountName"].Success)
        {
            return null;
        }
        return match.Groups["accountName"].Value;
    }

    	[Preserve]
        public static BillingMethod FromDict(JsonData data)
        {
            return new BillingMethod()
                .WithBillingMethodId(data.Keys.Contains("billingMethodId") && data["billingMethodId"] != null ? data["billingMethodId"].ToString() : null)
                .WithAccountName(data.Keys.Contains("accountName") && data["accountName"] != null ? data["accountName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMethodType(data.Keys.Contains("methodType") && data["methodType"] != null ? data["methodType"].ToString() : null)
                .WithCardCustomerId(data.Keys.Contains("cardCustomerId") && data["cardCustomerId"] != null ? data["cardCustomerId"].ToString() : null)
                .WithCardSignatureName(data.Keys.Contains("cardSignatureName") && data["cardSignatureName"] != null ? data["cardSignatureName"].ToString() : null)
                .WithCardBrand(data.Keys.Contains("cardBrand") && data["cardBrand"] != null ? data["cardBrand"].ToString() : null)
                .WithCardLast4(data.Keys.Contains("cardLast4") && data["cardLast4"] != null ? data["cardLast4"].ToString() : null)
                .WithPartnerId(data.Keys.Contains("partnerId") && data["partnerId"] != null ? data["partnerId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}