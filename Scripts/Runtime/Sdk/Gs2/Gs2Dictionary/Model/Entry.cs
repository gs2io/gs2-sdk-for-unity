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

namespace Gs2.Gs2Dictionary.Model
{
	[Preserve]
	public class Entry : IComparable
	{

        /** エントリー のGRN */
        public string entryId { set; get; }

        /**
         * エントリー のGRNを設定
         *
         * @param entryId エントリー のGRN
         * @return this
         */
        public Entry WithEntryId(string entryId) {
            this.entryId = entryId;
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
        public Entry WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** エントリーの種類名 */
        public string name { set; get; }

        /**
         * エントリーの種類名を設定
         *
         * @param name エントリーの種類名
         * @return this
         */
        public Entry WithName(string name) {
            this.name = name;
            return this;
        }

        /** None */
        public long? acquiredAt { set; get; }

        /**
         * Noneを設定
         *
         * @param acquiredAt None
         * @return this
         */
        public Entry WithAcquiredAt(long? acquiredAt) {
            this.acquiredAt = acquiredAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.entryId != null)
            {
                writer.WritePropertyName("entryId");
                writer.Write(this.entryId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.acquiredAt.HasValue)
            {
                writer.WritePropertyName("acquiredAt");
                writer.Write(this.acquiredAt.Value);
            }
            writer.WriteObjectEnd();
        }

    public static string GetEntryModelNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):entry:(?<entryModelName>.*)");
        if (!match.Groups["entryModelName"].Success)
        {
            return null;
        }
        return match.Groups["entryModelName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):entry:(?<entryModelName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):entry:(?<entryModelName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):entry:(?<entryModelName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):entry:(?<entryModelName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Entry FromDict(JsonData data)
        {
            return new Entry()
                .WithEntryId(data.Keys.Contains("entryId") && data["entryId"] != null ? data["entryId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithAcquiredAt(data.Keys.Contains("acquiredAt") && data["acquiredAt"] != null ? (long?)long.Parse(data["acquiredAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Entry;
            var diff = 0;
            if (entryId == null && entryId == other.entryId)
            {
                // null and null
            }
            else
            {
                diff += entryId.CompareTo(other.entryId);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (acquiredAt == null && acquiredAt == other.acquiredAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(acquiredAt - other.acquiredAt);
            }
            return diff;
        }
	}
}