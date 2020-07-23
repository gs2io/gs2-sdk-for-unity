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
	public class RatingModel : IComparable
	{

        /** レーティングモデル */
        public string ratingModelId { set; get; }

        /**
         * レーティングモデルを設定
         *
         * @param ratingModelId レーティングモデル
         * @return this
         */
        public RatingModel WithRatingModelId(string ratingModelId) {
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
        public RatingModel WithName(string name) {
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
        public RatingModel WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public RatingModel WithVolatility(int? volatility) {
            this.volatility = volatility;
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
            if(this.volatility.HasValue)
            {
                writer.WritePropertyName("volatility");
                writer.Write(this.volatility.Value);
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
        public static RatingModel FromDict(JsonData data)
        {
            return new RatingModel()
                .WithRatingModelId(data.Keys.Contains("ratingModelId") && data["ratingModelId"] != null ? data["ratingModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithVolatility(data.Keys.Contains("volatility") && data["volatility"] != null ? (int?)int.Parse(data["volatility"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as RatingModel;
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
            if (volatility == null && volatility == other.volatility)
            {
                // null and null
            }
            else
            {
                diff += (int)(volatility - other.volatility);
            }
            return diff;
        }
	}
}