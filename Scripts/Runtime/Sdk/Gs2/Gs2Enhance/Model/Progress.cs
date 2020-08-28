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
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Enhance.Model
{
	[Preserve]
	public class Progress : IComparable
	{

        /** 強化実行 */
        public string progressId { set; get; }

        /**
         * 強化実行を設定
         *
         * @param progressId 強化実行
         * @return this
         */
        public Progress WithProgressId(string progressId) {
            this.progressId = progressId;
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
        public Progress WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** レートモデル名 */
        public string rateName { set; get; }

        /**
         * レートモデル名を設定
         *
         * @param rateName レートモデル名
         * @return this
         */
        public Progress WithRateName(string rateName) {
            this.rateName = rateName;
            return this;
        }

        /** 強化対象のプロパティID */
        public string propertyId { set; get; }

        /**
         * 強化対象のプロパティIDを設定
         *
         * @param propertyId 強化対象のプロパティID
         * @return this
         */
        public Progress WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
            return this;
        }

        /** 入手できる経験値 */
        public int? experienceValue { set; get; }

        /**
         * 入手できる経験値を設定
         *
         * @param experienceValue 入手できる経験値
         * @return this
         */
        public Progress WithExperienceValue(int? experienceValue) {
            this.experienceValue = experienceValue;
            return this;
        }

        /** 経験値倍率 */
        public float? rate { set; get; }

        /**
         * 経験値倍率を設定
         *
         * @param rate 経験値倍率
         * @return this
         */
        public Progress WithRate(float? rate) {
            this.rate = rate;
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
        public Progress WithCreatedAt(long? createdAt) {
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
        public Progress WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.progressId != null)
            {
                writer.WritePropertyName("progressId");
                writer.Write(this.progressId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.rateName != null)
            {
                writer.WritePropertyName("rateName");
                writer.Write(this.rateName);
            }
            if(this.propertyId != null)
            {
                writer.WritePropertyName("propertyId");
                writer.Write(this.propertyId);
            }
            if(this.experienceValue.HasValue)
            {
                writer.WritePropertyName("experienceValue");
                writer.Write(this.experienceValue.Value);
            }
            if(this.rate.HasValue)
            {
                writer.WritePropertyName("rate");
                writer.Write(this.rate.Value);
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

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):user:(?<userId>.*):progress");
        if (!match.Groups["userId"].Success)
        {
            return null;
        }
        return match.Groups["userId"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):user:(?<userId>.*):progress");
        if (!match.Groups["namespaceName"].Success)
        {
            return null;
        }
        return match.Groups["namespaceName"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):user:(?<userId>.*):progress");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):enhance:(?<namespaceName>.*):user:(?<userId>.*):progress");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Progress FromDict(JsonData data)
        {
            return new Progress()
                .WithProgressId(data.Keys.Contains("progressId") && data["progressId"] != null ? data["progressId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithRateName(data.Keys.Contains("rateName") && data["rateName"] != null ? data["rateName"].ToString() : null)
                .WithPropertyId(data.Keys.Contains("propertyId") && data["propertyId"] != null ? data["propertyId"].ToString() : null)
                .WithExperienceValue(data.Keys.Contains("experienceValue") && data["experienceValue"] != null ? (int?)int.Parse(data["experienceValue"].ToString()) : null)
                .WithRate(data.Keys.Contains("rate") && data["rate"] != null ? (float?)float.Parse(data["rate"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Progress;
            var diff = 0;
            if (progressId == null && progressId == other.progressId)
            {
                // null and null
            }
            else
            {
                diff += progressId.CompareTo(other.progressId);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (rateName == null && rateName == other.rateName)
            {
                // null and null
            }
            else
            {
                diff += rateName.CompareTo(other.rateName);
            }
            if (propertyId == null && propertyId == other.propertyId)
            {
                // null and null
            }
            else
            {
                diff += propertyId.CompareTo(other.propertyId);
            }
            if (experienceValue == null && experienceValue == other.experienceValue)
            {
                // null and null
            }
            else
            {
                diff += (int)(experienceValue - other.experienceValue);
            }
            if (rate == null && rate == other.rate)
            {
                // null and null
            }
            else
            {
                diff += (int)(rate - other.rate);
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            if (updatedAt == null && updatedAt == other.updatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(updatedAt - other.updatedAt);
            }
            return diff;
        }
	}
}