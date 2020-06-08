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
	public class EntryModel : IComparable
	{

        /** エントリーモデルマスター */
        public string entryModelId { set; get; }

        /**
         * エントリーモデルマスターを設定
         *
         * @param entryModelId エントリーモデルマスター
         * @return this
         */
        public EntryModel WithEntryModelId(string entryModelId) {
            this.entryModelId = entryModelId;
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
        public EntryModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** エントリーの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * エントリーの種類のメタデータを設定
         *
         * @param metadata エントリーの種類のメタデータ
         * @return this
         */
        public EntryModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.entryModelId != null)
            {
                writer.WritePropertyName("entryModelId");
                writer.Write(this.entryModelId);
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
            writer.WriteObjectEnd();
        }

    public static string GetEntryNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):model:(?<entryName>.*)");
        if (!match.Groups["entryName"].Success)
        {
            return null;
        }
        return match.Groups["entryName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):model:(?<entryName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):model:(?<entryName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):dictionary:(?<namespaceName>.*):model:(?<entryName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static EntryModel FromDict(JsonData data)
        {
            return new EntryModel()
                .WithEntryModelId(data.Keys.Contains("entryModelId") && data["entryModelId"] != null ? data["entryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as EntryModel;
            var diff = 0;
            if (entryModelId == null && entryModelId == other.entryModelId)
            {
                // null and null
            }
            else
            {
                diff += entryModelId.CompareTo(other.entryModelId);
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
            return diff;
        }
	}
}