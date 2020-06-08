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
	public class Toc : IComparable
	{

        /** 見出し */
        public string tocId { set; get; }

        /**
         * 見出しを設定
         *
         * @param tocId 見出し
         * @return this
         */
        public Toc WithTocId(string tocId) {
            this.tocId = tocId;
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
        public Toc WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** インデックス */
        public int? index { set; get; }

        /**
         * インデックスを設定
         *
         * @param index インデックス
         * @return this
         */
        public Toc WithIndex(int? index) {
            this.index = index;
            return this;
        }

        /** エントリーのリスト */
        public List<Entry> entries { set; get; }

        /**
         * エントリーのリストを設定
         *
         * @param entries エントリーのリスト
         * @return this
         */
        public Toc WithEntries(List<Entry> entries) {
            this.entries = entries;
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
        public Toc WithCreatedAt(long? createdAt) {
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
        public Toc WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.tocId != null)
            {
                writer.WritePropertyName("tocId");
                writer.Write(this.tocId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.index.HasValue)
            {
                writer.WritePropertyName("index");
                writer.Write(this.index.Value);
            }
            if(this.entries != null)
            {
                writer.WritePropertyName("entries");
                writer.WriteArrayStart();
                foreach(var item in this.entries)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
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

    public static string GetIndexFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):toc:(?<index>.*)");
        if (!match.Groups["index"].Success)
        {
            return null;
        }
        return match.Groups["index"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):toc:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):toc:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):toc:(?<index>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):user:(?<userId>.*):toc:(?<index>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Toc FromDict(JsonData data)
        {
            return new Toc()
                .WithTocId(data.Keys.Contains("tocId") && data["tocId"] != null ? data["tocId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithIndex(data.Keys.Contains("index") && data["index"] != null ? (int?)int.Parse(data["index"].ToString()) : null)
                .WithEntries(data.Keys.Contains("entries") && data["entries"] != null ? data["entries"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Dictionary.Model.Entry.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Toc;
            var diff = 0;
            if (tocId == null && tocId == other.tocId)
            {
                // null and null
            }
            else
            {
                diff += tocId.CompareTo(other.tocId);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (index == null && index == other.index)
            {
                // null and null
            }
            else
            {
                diff += (int)(index - other.index);
            }
            if (entries == null && entries == other.entries)
            {
                // null and null
            }
            else
            {
                diff += entries.Count - other.entries.Count;
                for (var i = 0; i < entries.Count; i++)
                {
                    diff += entries[i].CompareTo(other.entries[i]);
                }
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