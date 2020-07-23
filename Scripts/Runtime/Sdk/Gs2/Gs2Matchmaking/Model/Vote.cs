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
	public class Vote : IComparable
	{

        /** 投票状況 */
        public string voteId { set; get; }

        /**
         * 投票状況を設定
         *
         * @param voteId 投票状況
         * @return this
         */
        public Vote WithVoteId(string voteId) {
            this.voteId = voteId;
            return this;
        }

        /** レーティング名 */
        public string ratingName { set; get; }

        /**
         * レーティング名を設定
         *
         * @param ratingName レーティング名
         * @return this
         */
        public Vote WithRatingName(string ratingName) {
            this.ratingName = ratingName;
            return this;
        }

        /** 投票対象のギャザリング名 */
        public string gatheringName { set; get; }

        /**
         * 投票対象のギャザリング名を設定
         *
         * @param gatheringName 投票対象のギャザリング名
         * @return this
         */
        public Vote WithGatheringName(string gatheringName) {
            this.gatheringName = gatheringName;
            return this;
        }

        /** 投票用紙のリスト */
        public List<WrittenBallot> writtenBallots { set; get; }

        /**
         * 投票用紙のリストを設定
         *
         * @param writtenBallots 投票用紙のリスト
         * @return this
         */
        public Vote WithWrittenBallots(List<WrittenBallot> writtenBallots) {
            this.writtenBallots = writtenBallots;
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
        public Vote WithCreatedAt(long? createdAt) {
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
        public Vote WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.voteId != null)
            {
                writer.WritePropertyName("voteId");
                writer.Write(this.voteId);
            }
            if(this.ratingName != null)
            {
                writer.WritePropertyName("ratingName");
                writer.Write(this.ratingName);
            }
            if(this.gatheringName != null)
            {
                writer.WritePropertyName("gatheringName");
                writer.Write(this.gatheringName);
            }
            if(this.writtenBallots != null)
            {
                writer.WritePropertyName("writtenBallots");
                writer.WriteArrayStart();
                foreach(var item in this.writtenBallots)
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

    public static string GetRatingNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):vote:(?<ratingName>.*):(?<gatheringName>.*)");
        if (!match.Groups["ratingName"].Success)
        {
            return null;
        }
        return match.Groups["ratingName"].Value;
    }

    public static string GetGatheringNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):vote:(?<ratingName>.*):(?<gatheringName>.*)");
        if (!match.Groups["gatheringName"].Success)
        {
            return null;
        }
        return match.Groups["gatheringName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):vote:(?<ratingName>.*):(?<gatheringName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):vote:(?<ratingName>.*):(?<gatheringName>.*)");
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
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):matchmaking:(?<namespaceName>.*):vote:(?<ratingName>.*):(?<gatheringName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
    }

    	[Preserve]
        public static Vote FromDict(JsonData data)
        {
            return new Vote()
                .WithVoteId(data.Keys.Contains("voteId") && data["voteId"] != null ? data["voteId"].ToString() : null)
                .WithRatingName(data.Keys.Contains("ratingName") && data["ratingName"] != null ? data["ratingName"].ToString() : null)
                .WithGatheringName(data.Keys.Contains("gatheringName") && data["gatheringName"] != null ? data["gatheringName"].ToString() : null)
                .WithWrittenBallots(data.Keys.Contains("writtenBallots") && data["writtenBallots"] != null ? data["writtenBallots"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Matchmaking.Model.WrittenBallot.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Vote;
            var diff = 0;
            if (voteId == null && voteId == other.voteId)
            {
                // null and null
            }
            else
            {
                diff += voteId.CompareTo(other.voteId);
            }
            if (ratingName == null && ratingName == other.ratingName)
            {
                // null and null
            }
            else
            {
                diff += ratingName.CompareTo(other.ratingName);
            }
            if (gatheringName == null && gatheringName == other.gatheringName)
            {
                // null and null
            }
            else
            {
                diff += gatheringName.CompareTo(other.gatheringName);
            }
            if (writtenBallots == null && writtenBallots == other.writtenBallots)
            {
                // null and null
            }
            else
            {
                diff += writtenBallots.Count - other.writtenBallots.Count;
                for (var i = 0; i < writtenBallots.Count; i++)
                {
                    diff += writtenBallots[i].CompareTo(other.writtenBallots[i]);
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