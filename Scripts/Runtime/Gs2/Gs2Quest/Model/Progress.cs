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

namespace Gs2.Gs2Quest.Model
{
	[Preserve]
	public class Progress
	{

        /** クエスト挑戦 */
        public string progressId { set; get; }

        /**
         * クエスト挑戦を設定
         *
         * @param progressId クエスト挑戦
         * @return this
         */
        public Progress WithProgressId(string progressId) {
            this.progressId = progressId;
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
        public Progress WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** トランザクションID */
        public string transactionId { set; get; }

        /**
         * トランザクションIDを設定
         *
         * @param transactionId トランザクションID
         * @return this
         */
        public Progress WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
            return this;
        }

        /** クエストモデル */
        public string questModelId { set; get; }

        /**
         * クエストモデルを設定
         *
         * @param questModelId クエストモデル
         * @return this
         */
        public Progress WithQuestModelId(string questModelId) {
            this.questModelId = questModelId;
            return this;
        }

        /** 乱数シード */
        public long? randomSeed { set; get; }

        /**
         * 乱数シードを設定
         *
         * @param randomSeed 乱数シード
         * @return this
         */
        public Progress WithRandomSeed(long? randomSeed) {
            this.randomSeed = randomSeed;
            return this;
        }

        /** クエストで得られる報酬の上限 */
        public List<Reward> rewards { set; get; }

        /**
         * クエストで得られる報酬の上限を設定
         *
         * @param rewards クエストで得られる報酬の上限
         * @return this
         */
        public Progress WithRewards(List<Reward> rewards) {
            this.rewards = rewards;
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
        public Progress WithCreatedAt(long? createdAt) {
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
        public Progress WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.progressId != null)
            {
                writer.WritePropertyName("progressId");
                writer.Write(this.progressId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.transactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.transactionId);
            }
            if(this.questModelId != null)
            {
                writer.WritePropertyName("questModelId");
                writer.Write(this.questModelId);
            }
            if(this.randomSeed.HasValue)
            {
                writer.WritePropertyName("randomSeed");
                writer.Write(this.randomSeed.Value);
            }
            if(this.rewards != null)
            {
                writer.WritePropertyName("rewards");
                writer.WriteArrayStart();
                foreach(var item in this.rewards)
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

    	[Preserve]
        public static Progress FromDict(JsonData data)
        {
            return new Progress()
                .WithProgressId(data.Keys.Contains("progressId") && data["progressId"] != null ? data["progressId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithTransactionId(data.Keys.Contains("transactionId") && data["transactionId"] != null ? data["transactionId"].ToString() : null)
                .WithQuestModelId(data.Keys.Contains("questModelId") && data["questModelId"] != null ? data["questModelId"].ToString() : null)
                .WithRandomSeed(data.Keys.Contains("randomSeed") && data["randomSeed"] != null ? (long?)long.Parse(data["randomSeed"].ToString()) : null)
                .WithRewards(data.Keys.Contains("rewards") && data["rewards"] != null ? data["rewards"].Cast<JsonData>().Select(value =>
                    {
                        return Reward.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}