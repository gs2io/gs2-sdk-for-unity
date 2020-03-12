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
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Ranking.Model
{
	[Preserve]
	public class Ranking
	{

        /** 順位 */
        public long? rank { set; get; }

        /**
         * 順位を設定
         *
         * @param rank 順位
         * @return this
         */
        public Ranking WithRank(long? rank) {
            this.rank = rank;
            return this;
        }

        /** 1位からのインデックス */
        public long? index { set; get; }

        /**
         * 1位からのインデックスを設定
         *
         * @param index 1位からのインデックス
         * @return this
         */
        public Ranking WithIndex(long? index) {
            this.index = index;
            return this;
        }

        /** ユーザID */
        public string userId { set; get; }

        /**
         * ユーザIDを設定
         *
         * @param userId ユーザID
         * @return this
         */
        public Ranking WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** スコア */
        public long? score { set; get; }

        /**
         * スコアを設定
         *
         * @param score スコア
         * @return this
         */
        public Ranking WithScore(long? score) {
            this.score = score;
            return this;
        }

        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public Ranking WithMetadata(string metadata) {
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
        public Ranking WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
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
            if(this.index.HasValue)
            {
                writer.WritePropertyName("index");
                writer.Write(this.index.Value);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.score.HasValue)
            {
                writer.WritePropertyName("score");
                writer.Write(this.score.Value);
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Ranking FromDict(JsonData data)
        {
            return new Ranking()
                .WithRank(data.Keys.Contains("rank") && data["rank"] != null ? (long?)long.Parse(data["rank"].ToString()) : null)
                .WithIndex(data.Keys.Contains("index") && data["index"] != null ? (long?)long.Parse(data["index"].ToString()) : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithScore(data.Keys.Contains("score") && data["score"] != null ? (long?)long.Parse(data["score"].ToString()) : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}