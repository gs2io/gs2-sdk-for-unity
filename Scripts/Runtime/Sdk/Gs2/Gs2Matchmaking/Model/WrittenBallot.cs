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
	public class WrittenBallot : IComparable
	{

        /** 投票用紙 */
        public Gs2.Gs2Matchmaking.Model.Ballot ballot { set; get; }

        /**
         * 投票用紙を設定
         *
         * @param ballot 投票用紙
         * @return this
         */
        public WrittenBallot WithBallot(Gs2.Gs2Matchmaking.Model.Ballot ballot) {
            this.ballot = ballot;
            return this;
        }

        /** 投票内容。対戦結果のリスト */
        public List<GameResult> gameResults { set; get; }

        /**
         * 投票内容。対戦結果のリストを設定
         *
         * @param gameResults 投票内容。対戦結果のリスト
         * @return this
         */
        public WrittenBallot WithGameResults(List<GameResult> gameResults) {
            this.gameResults = gameResults;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ballot != null)
            {
                writer.WritePropertyName("ballot");
                this.ballot.WriteJson(writer);
            }
            if(this.gameResults != null)
            {
                writer.WritePropertyName("gameResults");
                writer.WriteArrayStart();
                foreach(var item in this.gameResults)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static WrittenBallot FromDict(JsonData data)
        {
            return new WrittenBallot()
                .WithBallot(data.Keys.Contains("ballot") && data["ballot"] != null ? Gs2.Gs2Matchmaking.Model.Ballot.FromDict(data["ballot"]) : null)
                .WithGameResults(data.Keys.Contains("gameResults") && data["gameResults"] != null ? data["gameResults"].Cast<JsonData>().Select(value =>
                    {
                        return Gs2.Gs2Matchmaking.Model.GameResult.FromDict(value);
                    }
                ).ToList() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as WrittenBallot;
            var diff = 0;
            if (ballot == null && ballot == other.ballot)
            {
                // null and null
            }
            else
            {
                diff += ballot.CompareTo(other.ballot);
            }
            if (gameResults == null && gameResults == other.gameResults)
            {
                // null and null
            }
            else
            {
                diff += gameResults.Count - other.gameResults.Count;
                for (var i = 0; i < gameResults.Count; i++)
                {
                    diff += gameResults[i].CompareTo(other.gameResults[i]);
                }
            }
            return diff;
        }
	}
}