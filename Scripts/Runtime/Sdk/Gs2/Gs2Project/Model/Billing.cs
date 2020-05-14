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
	public class Billing
	{

        /** 利用状況 */
        public string billingId { set; get; }

        /**
         * 利用状況を設定
         *
         * @param billingId 利用状況
         * @return this
         */
        public Billing WithBillingId(string billingId) {
            this.billingId = billingId;
            return this;
        }

        /** プロジェクト名 */
        public string projectName { set; get; }

        /**
         * プロジェクト名を設定
         *
         * @param projectName プロジェクト名
         * @return this
         */
        public Billing WithProjectName(string projectName) {
            this.projectName = projectName;
            return this;
        }

        /** イベントの発生年 */
        public int? year { set; get; }

        /**
         * イベントの発生年を設定
         *
         * @param year イベントの発生年
         * @return this
         */
        public Billing WithYear(int? year) {
            this.year = year;
            return this;
        }

        /** イベントの発生月 */
        public int? month { set; get; }

        /**
         * イベントの発生月を設定
         *
         * @param month イベントの発生月
         * @return this
         */
        public Billing WithMonth(int? month) {
            this.month = month;
            return this;
        }

        /** リージョン */
        public string region { set; get; }

        /**
         * リージョンを設定
         *
         * @param region リージョン
         * @return this
         */
        public Billing WithRegion(string region) {
            this.region = region;
            return this;
        }

        /** サービスの種類 */
        public string service { set; get; }

        /**
         * サービスの種類を設定
         *
         * @param service サービスの種類
         * @return this
         */
        public Billing WithService(string service) {
            this.service = service;
            return this;
        }

        /** イベントの種類 */
        public string activityType { set; get; }

        /**
         * イベントの種類を設定
         *
         * @param activityType イベントの種類
         * @return this
         */
        public Billing WithActivityType(string activityType) {
            this.activityType = activityType;
            return this;
        }

        /** 回数 */
        public long? unit { set; get; }

        /**
         * 回数を設定
         *
         * @param unit 回数
         * @return this
         */
        public Billing WithUnit(long? unit) {
            this.unit = unit;
            return this;
        }

        /** 単位 */
        public string unitName { set; get; }

        /**
         * 単位を設定
         *
         * @param unitName 単位
         * @return this
         */
        public Billing WithUnitName(string unitName) {
            this.unitName = unitName;
            return this;
        }

        /** 料金 */
        public long? price { set; get; }

        /**
         * 料金を設定
         *
         * @param price 料金
         * @return this
         */
        public Billing WithPrice(long? price) {
            this.price = price;
            return this;
        }

        /** 通貨 */
        public string currency { set; get; }

        /**
         * 通貨を設定
         *
         * @param currency 通貨
         * @return this
         */
        public Billing WithCurrency(string currency) {
            this.currency = currency;
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
        public Billing WithCreatedAt(long? createdAt) {
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
        public Billing WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.billingId != null)
            {
                writer.WritePropertyName("billingId");
                writer.Write(this.billingId);
            }
            if(this.projectName != null)
            {
                writer.WritePropertyName("projectName");
                writer.Write(this.projectName);
            }
            if(this.year.HasValue)
            {
                writer.WritePropertyName("year");
                writer.Write(this.year.Value);
            }
            if(this.month.HasValue)
            {
                writer.WritePropertyName("month");
                writer.Write(this.month.Value);
            }
            if(this.region != null)
            {
                writer.WritePropertyName("region");
                writer.Write(this.region);
            }
            if(this.service != null)
            {
                writer.WritePropertyName("service");
                writer.Write(this.service);
            }
            if(this.activityType != null)
            {
                writer.WritePropertyName("activityType");
                writer.Write(this.activityType);
            }
            if(this.unit.HasValue)
            {
                writer.WritePropertyName("unit");
                writer.Write(this.unit.Value);
            }
            if(this.unitName != null)
            {
                writer.WritePropertyName("unitName");
                writer.Write(this.unitName);
            }
            if(this.price.HasValue)
            {
                writer.WritePropertyName("price");
                writer.Write(this.price.Value);
            }
            if(this.currency != null)
            {
                writer.WritePropertyName("currency");
                writer.Write(this.currency);
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

    public static string GetBillingNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):project:(?<projectName>.*):billing:(?<year>.*):(?<month>.*):(?<region>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["billingName"].Success)
        {
            return null;
        }
        return match.Groups["billingName"].Value;
    }

    public static string GetProjectNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):project:(?<projectName>.*):billing:(?<year>.*):(?<month>.*):(?<region>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["projectName"].Success)
        {
            return null;
        }
        return match.Groups["projectName"].Value;
    }

    public static string GetAccountNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:::gs2:account:(?<accountName>.*):project:(?<projectName>.*):billing:(?<year>.*):(?<month>.*):(?<region>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["accountName"].Success)
        {
            return null;
        }
        return match.Groups["accountName"].Value;
    }

    	[Preserve]
        public static Billing FromDict(JsonData data)
        {
            return new Billing()
                .WithBillingId(data.Keys.Contains("billingId") && data["billingId"] != null ? data["billingId"].ToString() : null)
                .WithProjectName(data.Keys.Contains("projectName") && data["projectName"] != null ? data["projectName"].ToString() : null)
                .WithYear(data.Keys.Contains("year") && data["year"] != null ? (int?)int.Parse(data["year"].ToString()) : null)
                .WithMonth(data.Keys.Contains("month") && data["month"] != null ? (int?)int.Parse(data["month"].ToString()) : null)
                .WithRegion(data.Keys.Contains("region") && data["region"] != null ? data["region"].ToString() : null)
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithActivityType(data.Keys.Contains("activityType") && data["activityType"] != null ? data["activityType"].ToString() : null)
                .WithUnit(data.Keys.Contains("unit") && data["unit"] != null ? (long?)long.Parse(data["unit"].ToString()) : null)
                .WithUnitName(data.Keys.Contains("unitName") && data["unitName"] != null ? data["unitName"].ToString() : null)
                .WithPrice(data.Keys.Contains("price") && data["price"] != null ? (long?)long.Parse(data["price"].ToString()) : null)
                .WithCurrency(data.Keys.Contains("currency") && data["currency"] != null ? data["currency"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}