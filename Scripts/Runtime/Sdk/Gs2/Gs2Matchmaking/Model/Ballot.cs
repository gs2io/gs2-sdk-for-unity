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
	public class Ballot : IComparable
	{

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Ballot WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** レーティング計算に使用するレーティング名 */
        public string ratingName { set; get; }

        /**
         * レーティング計算に使用するレーティング名を設定
         *
         * @param ratingName レーティング計算に使用するレーティング名
         * @return this
         */
        public Ballot WithRatingName(string ratingName) {
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
        public Ballot WithGatheringName(string gatheringName) {
            this.gatheringName = gatheringName;
            return this;
        }

        /** 参加人数 */
        public int? numberOfPlayer { set; get; }

        /**
         * 参加人数を設定
         *
         * @param numberOfPlayer 参加人数
         * @return this
         */
        public Ballot WithNumberOfPlayer(int? numberOfPlayer) {
            this.numberOfPlayer = numberOfPlayer;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
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
            if(this.numberOfPlayer.HasValue)
            {
                writer.WritePropertyName("numberOfPlayer");
                writer.Write(this.numberOfPlayer.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Ballot FromDict(JsonData data)
        {
            return new Ballot()
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithRatingName(data.Keys.Contains("ratingName") && data["ratingName"] != null ? data["ratingName"].ToString() : null)
                .WithGatheringName(data.Keys.Contains("gatheringName") && data["gatheringName"] != null ? data["gatheringName"].ToString() : null)
                .WithNumberOfPlayer(data.Keys.Contains("numberOfPlayer") && data["numberOfPlayer"] != null ? (int?)int.Parse(data["numberOfPlayer"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Ballot;
            var diff = 0;
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
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
            if (numberOfPlayer == null && numberOfPlayer == other.numberOfPlayer)
            {
                // null and null
            }
            else
            {
                diff += (int)(numberOfPlayer - other.numberOfPlayer);
            }
            return diff;
        }
	}
}