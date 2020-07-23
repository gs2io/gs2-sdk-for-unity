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
	public class GameResult : IComparable
	{

        /** 順位 */
        public int? rank { set; get; }

        /**
         * 順位を設定
         *
         * @param rank 順位
         * @return this
         */
        public GameResult WithRank(int? rank) {
            this.rank = rank;
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
        public GameResult WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.rank.HasValue)
            {
                writer.WritePropertyName("rank");
                writer.Write(this.rank.Value);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static GameResult FromDict(JsonData data)
        {
            return new GameResult()
                .WithRank(data.Keys.Contains("rank") && data["rank"] != null ? (int?)int.Parse(data["rank"].ToString()) : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as GameResult;
            var diff = 0;
            if (rank == null && rank == other.rank)
            {
                // null and null
            }
            else
            {
                diff += (int)(rank - other.rank);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            return diff;
        }
	}
}