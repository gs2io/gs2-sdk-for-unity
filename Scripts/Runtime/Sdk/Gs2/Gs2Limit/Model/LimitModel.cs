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

namespace Gs2.Gs2Limit.Model
{
	[Preserve]
	public class LimitModel : IComparable
	{

        /** 回数制限の種類 */
        public string limitModelId { set; get; }

        /**
         * 回数制限の種類を設定
         *
         * @param limitModelId 回数制限の種類
         * @return this
         */
        public LimitModel WithLimitModelId(string limitModelId) {
            this.limitModelId = limitModelId;
            return this;
        }

        /** 回数制限の種類名 */
        public string name { set; get; }

        /**
         * 回数制限の種類名を設定
         *
         * @param name 回数制限の種類名
         * @return this
         */
        public LimitModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** 回数制限の種類のメタデータ */
        public string metadata { set; get; }

        /**
         * 回数制限の種類のメタデータを設定
         *
         * @param metadata 回数制限の種類のメタデータ
         * @return this
         */
        public LimitModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** リセットタイミング */
        public string resetType { set; get; }

        /**
         * リセットタイミングを設定
         *
         * @param resetType リセットタイミング
         * @return this
         */
        public LimitModel WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }

        /** リセットをする日にち */
        public int? resetDayOfMonth { set; get; }

        /**
         * リセットをする日にちを設定
         *
         * @param resetDayOfMonth リセットをする日にち
         * @return this
         */
        public LimitModel WithResetDayOfMonth(int? resetDayOfMonth) {
            this.resetDayOfMonth = resetDayOfMonth;
            return this;
        }

        /** リセットする曜日 */
        public string resetDayOfWeek { set; get; }

        /**
         * リセットする曜日を設定
         *
         * @param resetDayOfWeek リセットする曜日
         * @return this
         */
        public LimitModel WithResetDayOfWeek(string resetDayOfWeek) {
            this.resetDayOfWeek = resetDayOfWeek;
            return this;
        }

        /** リセット時刻 */
        public int? resetHour { set; get; }

        /**
         * リセット時刻を設定
         *
         * @param resetHour リセット時刻
         * @return this
         */
        public LimitModel WithResetHour(int? resetHour) {
            this.resetHour = resetHour;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.limitModelId != null)
            {
                writer.WritePropertyName("limitModelId");
                writer.Write(this.limitModelId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.resetType != null)
            {
                writer.WritePropertyName("resetType");
                writer.Write(this.resetType);
            }
            if(this.resetDayOfMonth.HasValue)
            {
                writer.WritePropertyName("resetDayOfMonth");
                writer.Write(this.resetDayOfMonth.Value);
            }
            if(this.resetDayOfWeek != null)
            {
                writer.WritePropertyName("resetDayOfWeek");
                writer.Write(this.resetDayOfWeek);
            }
            if(this.resetHour.HasValue)
            {
                writer.WritePropertyName("resetHour");
                writer.Write(this.resetHour.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetLimitNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):limit:(?<namespaceName>.*):limit:(?<limitName>.*)");
        if (!match.Groups["limitName"].Success)
        {
            return null;
        }
        return match.Groups["limitName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):limit:(?<namespaceName>.*):limit:(?<limitName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):limit:(?<namespaceName>.*):limit:(?<limitName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):limit:(?<namespaceName>.*):limit:(?<limitName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static LimitModel FromDict(JsonData data)
        {
            return new LimitModel()
                .WithLimitModelId(data.Keys.Contains("limitModelId") && data["limitModelId"] != null ? data["limitModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithResetType(data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString() : null)
                .WithResetDayOfMonth(data.Keys.Contains("resetDayOfMonth") && data["resetDayOfMonth"] != null ? (int?)int.Parse(data["resetDayOfMonth"].ToString()) : null)
                .WithResetDayOfWeek(data.Keys.Contains("resetDayOfWeek") && data["resetDayOfWeek"] != null ? data["resetDayOfWeek"].ToString() : null)
                .WithResetHour(data.Keys.Contains("resetHour") && data["resetHour"] != null ? (int?)int.Parse(data["resetHour"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as LimitModel;
            var diff = 0;
            if (limitModelId == null && limitModelId == other.limitModelId)
            {
                // null and null
            }
            else
            {
                diff += limitModelId.CompareTo(other.limitModelId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (metadata == null && metadata == other.metadata)
            {
                // null and null
            }
            else
            {
                diff += metadata.CompareTo(other.metadata);
            }
            if (resetType == null && resetType == other.resetType)
            {
                // null and null
            }
            else
            {
                diff += resetType.CompareTo(other.resetType);
            }
            if (resetDayOfMonth == null && resetDayOfMonth == other.resetDayOfMonth)
            {
                // null and null
            }
            else
            {
                diff += (int)(resetDayOfMonth - other.resetDayOfMonth);
            }
            if (resetDayOfWeek == null && resetDayOfWeek == other.resetDayOfWeek)
            {
                // null and null
            }
            else
            {
                diff += resetDayOfWeek.CompareTo(other.resetDayOfWeek);
            }
            if (resetHour == null && resetHour == other.resetHour)
            {
                // null and null
            }
            else
            {
                diff += (int)(resetHour - other.resetHour);
            }
            return diff;
        }
	}
}