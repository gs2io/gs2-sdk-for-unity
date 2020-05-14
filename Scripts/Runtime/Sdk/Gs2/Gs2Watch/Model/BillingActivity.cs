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

namespace Gs2.Gs2Watch.Model
{
	[Preserve]
	public class BillingActivity
	{

        /** 請求にまつわるアクティビティ */
        public string billingActivityId { set; get; }

        /**
         * 請求にまつわるアクティビティを設定
         *
         * @param billingActivityId 請求にまつわるアクティビティ
         * @return this
         */
        public BillingActivity WithBillingActivityId(string billingActivityId) {
            this.billingActivityId = billingActivityId;
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
        public BillingActivity WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
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
        public BillingActivity WithYear(int? year) {
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
        public BillingActivity WithMonth(int? month) {
            this.month = month;
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
        public BillingActivity WithService(string service) {
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
        public BillingActivity WithActivityType(string activityType) {
            this.activityType = activityType;
            return this;
        }

        /** イベントの値 */
        public long? value { set; get; }

        /**
         * イベントの値を設定
         *
         * @param value イベントの値
         * @return this
         */
        public BillingActivity WithValue(long? value) {
            this.value = value;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.billingActivityId != null)
            {
                writer.WritePropertyName("billingActivityId");
                writer.Write(this.billingActivityId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
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
            if(this.value.HasValue)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetYearFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["year"].Success)
        {
            return null;
        }
        return match.Groups["year"].Value;
    }

    public static string GetMonthFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["month"].Success)
        {
            return null;
        }
        return match.Groups["month"].Value;
    }

    public static string GetServiceFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["service"].Success)
        {
            return null;
        }
        return match.Groups["service"].Value;
    }

    public static string GetActivityTypeFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["activityType"].Success)
        {
            return null;
        }
        return match.Groups["activityType"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["ownerId"].Success)
        {
            return null;
        }
        return match.Groups["ownerId"].Value;
    }

    public static string GetRegionFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):watch:billingActivity:(?<year>.*):(?<month>.*):(?<service>.*):(?<activityType>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static BillingActivity FromDict(JsonData data)
        {
            return new BillingActivity()
                .WithBillingActivityId(data.Keys.Contains("billingActivityId") && data["billingActivityId"] != null ? data["billingActivityId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithYear(data.Keys.Contains("year") && data["year"] != null ? (int?)int.Parse(data["year"].ToString()) : null)
                .WithMonth(data.Keys.Contains("month") && data["month"] != null ? (int?)int.Parse(data["month"].ToString()) : null)
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithActivityType(data.Keys.Contains("activityType") && data["activityType"] != null ? data["activityType"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? (long?)long.Parse(data["value"].ToString()) : null);
        }
	}
}