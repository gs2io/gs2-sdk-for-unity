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

namespace Gs2.Gs2Exchange.Model
{
	[Preserve]
	public class Await : IComparable
	{

        /** 交換待機 */
        public string awaitId { set; get; }

        /**
         * 交換待機を設定
         *
         * @param awaitId 交換待機
         * @return this
         */
        public Await WithAwaitId(string awaitId) {
            this.awaitId = awaitId;
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
        public Await WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 交換レート名 */
        public string rateName { set; get; }

        /**
         * 交換レート名を設定
         *
         * @param rateName 交換レート名
         * @return this
         */
        public Await WithRateName(string rateName) {
            this.rateName = rateName;
            return this;
        }

        /** 交換待機の名前 */
        public string name { set; get; }

        /**
         * 交換待機の名前を設定
         *
         * @param name 交換待機の名前
         * @return this
         */
        public Await WithName(string name) {
            this.name = name;
            return this;
        }

        /** 交換数 */
        public int? count { set; get; }

        /**
         * 交換数を設定
         *
         * @param count 交換数
         * @return this
         */
        public Await WithCount(int? count) {
            this.count = count;
            return this;
        }

        /** 作成日時 */
        public long? exchangedAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param exchangedAt 作成日時
         * @return this
         */
        public Await WithExchangedAt(long? exchangedAt) {
            this.exchangedAt = exchangedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.awaitId != null)
            {
                writer.WritePropertyName("awaitId");
                writer.Write(this.awaitId);
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
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
            }
            if(this.exchangedAt.HasValue)
            {
                writer.WritePropertyName("exchangedAt");
                writer.Write(this.exchangedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetAwaitNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):user:(?<userId>.*):await:(?<awaitName>.*)");
        if (!match.Groups["awaitName"].Success)
        {
            return null;
        }
        return match.Groups["awaitName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):user:(?<userId>.*):await:(?<awaitName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):user:(?<userId>.*):await:(?<awaitName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):user:(?<userId>.*):await:(?<awaitName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):exchange:(?<namespaceName>.*):user:(?<userId>.*):await:(?<awaitName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Await FromDict(JsonData data)
        {
            return new Await()
                .WithAwaitId(data.Keys.Contains("awaitId") && data["awaitId"] != null ? data["awaitId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithRateName(data.Keys.Contains("rateName") && data["rateName"] != null ? data["rateName"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null)
                .WithExchangedAt(data.Keys.Contains("exchangedAt") && data["exchangedAt"] != null ? (long?)long.Parse(data["exchangedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Await;
            var diff = 0;
            if (awaitId == null && awaitId == other.awaitId)
            {
                // null and null
            }
            else
            {
                diff += awaitId.CompareTo(other.awaitId);
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
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (count == null && count == other.count)
            {
                // null and null
            }
            else
            {
                diff += (int)(count - other.count);
            }
            if (exchangedAt == null && exchangedAt == other.exchangedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(exchangedAt - other.exchangedAt);
            }
            return diff;
        }
	}
}