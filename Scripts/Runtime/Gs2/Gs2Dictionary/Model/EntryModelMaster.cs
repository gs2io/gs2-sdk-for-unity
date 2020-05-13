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

namespace Gs2.Gs2Dictionary.Model
{
	[Preserve]
	public class EntryModelMaster
	{

        /** エントリーモデルマスター */
        public string entryModelId { set; get; }

        /**
         * エントリーモデルマスターを設定
         *
         * @param entryModelId エントリーモデルマスター
         * @return this
         */
        public EntryModelMaster WithEntryModelId(string entryModelId) {
            this.entryModelId = entryModelId;
            return this;
        }

        /** エントリーモデル名 */
        public string name { set; get; }

        /**
         * エントリーモデル名を設定
         *
         * @param name エントリーモデル名
         * @return this
         */
        public EntryModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** エントリーモデルマスターの説明 */
        public string description { set; get; }

        /**
         * エントリーモデルマスターの説明を設定
         *
         * @param description エントリーモデルマスターの説明
         * @return this
         */
        public EntryModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** エントリーモデルのメタデータ */
        public string metadata { set; get; }

        /**
         * エントリーモデルのメタデータを設定
         *
         * @param metadata エントリーモデルのメタデータ
         * @return this
         */
        public EntryModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public EntryModelMaster WithCreatedAt(long? createdAt) {
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
        public EntryModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
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
        public static EntryModelMaster FromDict(JsonData data)
        {
            return new EntryModelMaster()
                .WithEntryModelId(data.Keys.Contains("entryModelId") && data["entryModelId"] != null ? data["entryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}