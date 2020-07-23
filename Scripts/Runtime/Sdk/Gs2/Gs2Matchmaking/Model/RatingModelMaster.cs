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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class RatingModelMaster : IComparable
	{

        /** レーティングモデルマスター */
        public string ratingModelId { set; get; }

        /**
         * レーティングモデルマスターを設定
         *
         * @param ratingModelId レーティングモデルマスター
         * @return this
         */
        public RatingModelMaster WithRatingModelId(string ratingModelId) {
            this.ratingModelId = ratingModelId;
            return this;
        }

        /** レーティングの種類名 */
        public string name { set; get; }

        /**
         * レーティングの種類名を設定
         *
         * @param name レーティングの種類名
         * @return this
         */
        public RatingModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** レーティングの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * レーティングの種類のメタデータを設定
         *
         * @param metadata レーティングの種類のメタデータ
         * @return this
         */
        public RatingModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** レーティングモデルマスターの説明 */
        public string description { set; get; }

        /**
         * レーティングモデルマスターの説明を設定
         *
         * @param description レーティングモデルマスターの説明
         * @return this
         */
        public RatingModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** レート値の変動の大きさ */
        public int? volatility { set; get; }

        /**
         * レート値の変動の大きさを設定
         *
         * @param volatility レート値の変動の大きさ
         * @return this
         */
        public RatingModelMaster WithVolatility(int? volatility) {
            this.volatility = volatility;
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
        public RatingModelMaster WithCreatedAt(long? createdAt) {
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
        public RatingModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ratingModelId != null)
            {
                writer.WritePropertyName("ratingModelId");
                writer.Write(this.ratingModelId);
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.volatility.HasValue)
            {
                writer.WritePropertyName("volatility");
                writer.Write(this.volatility.Value);
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

    public static string GetRatingNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):model:(?<ratingName>.*)");
        if (!match.Groups["ratingName"].Success)
        {
            return null;
        }
        return match.Groups["ratingName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):model:(?<ratingName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):model:(?<ratingName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):model:(?<ratingName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static RatingModelMaster FromDict(JsonData data)
        {
            return new RatingModelMaster()
                .WithRatingModelId(data.Keys.Contains("ratingModelId") && data["ratingModelId"] != null ? data["ratingModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithVolatility(data.Keys.Contains("volatility") && data["volatility"] != null ? (int?)int.Parse(data["volatility"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as RatingModelMaster;
            var diff = 0;
            if (ratingModelId == null && ratingModelId == other.ratingModelId)
            {
                // null and null
            }
            else
            {
                diff += ratingModelId.CompareTo(other.ratingModelId);
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
            if (description == null && description == other.description)
            {
                // null and null
            }
            else
            {
                diff += description.CompareTo(other.description);
            }
            if (volatility == null && volatility == other.volatility)
            {
                // null and null
            }
            else
            {
                diff += (int)(volatility - other.volatility);
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