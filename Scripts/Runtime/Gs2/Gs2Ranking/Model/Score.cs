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
	public class Score
	{

        /** スコア */
        public string scoreId { set; get; }

        /**
         * スコアを設定
         *
         * @param scoreId スコア
         * @return this
         */
        public Score WithScoreId(string scoreId) {
            this.scoreId = scoreId;
            return this;
        }

        /** カテゴリ名 */
        public string categoryName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param categoryName カテゴリ名
         * @return this
         */
        public Score WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
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
        public Score WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** スコアのユニークID */
        public string uniqueId { set; get; }

        /**
         * スコアのユニークIDを設定
         *
         * @param uniqueId スコアのユニークID
         * @return this
         */
        public Score WithUniqueId(string uniqueId) {
            this.uniqueId = uniqueId;
            return this;
        }

        /** スコアを獲得したユーザID */
        public string scorerUserId { set; get; }

        /**
         * スコアを獲得したユーザIDを設定
         *
         * @param scorerUserId スコアを獲得したユーザID
         * @return this
         */
        public Score WithScorerUserId(string scorerUserId) {
            this.scorerUserId = scorerUserId;
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
        public Score WithScore(long? score) {
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
        public Score WithMetadata(string metadata) {
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
        public Score WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.scoreId != null)
            {
                writer.WritePropertyName("scoreId");
                writer.Write(this.scoreId);
            }
            if(this.categoryName != null)
            {
                writer.WritePropertyName("categoryName");
                writer.Write(this.categoryName);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.uniqueId != null)
            {
                writer.WritePropertyName("uniqueId");
                writer.Write(this.uniqueId);
            }
            if(this.scorerUserId != null)
            {
                writer.WritePropertyName("scorerUserId");
                writer.Write(this.scorerUserId);
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
        public static Score FromDict(JsonData data)
        {
            return new Score()
                .WithScoreId(data.Keys.Contains("scoreId") && data["scoreId"] != null ? data["scoreId"].ToString() : null)
                .WithCategoryName(data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithUniqueId(data.Keys.Contains("uniqueId") && data["uniqueId"] != null ? data["uniqueId"].ToString() : null)
                .WithScorerUserId(data.Keys.Contains("scorerUserId") && data["scorerUserId"] != null ? data["scorerUserId"].ToString() : null)
                .WithScore(data.Keys.Contains("score") && data["score"] != null ? (long?)long.Parse(data["score"].ToString()) : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}