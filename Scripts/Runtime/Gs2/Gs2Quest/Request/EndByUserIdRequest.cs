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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	public class EndByUserIdRequest : Gs2Request<EndByUserIdRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public EndByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public EndByUserIdRequest WithUserId(string userId) {
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
        public EndByUserIdRequest WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
            return this;
        }


        /** 実際にクエストで得た報酬 */
        public List<Reward> rewards { set; get; }

        /**
         * 実際にクエストで得た報酬を設定
         *
         * @param rewards 実際にクエストで得た報酬
         * @return this
         */
        public EndByUserIdRequest WithRewards(List<Reward> rewards) {
            this.rewards = rewards;
            return this;
        }


        /** クエストをクリアしたか */
        public bool? isComplete { set; get; }

        /**
         * クエストをクリアしたかを設定
         *
         * @param isComplete クエストをクリアしたか
         * @return this
         */
        public EndByUserIdRequest WithIsComplete(bool? isComplete) {
            this.isComplete = isComplete;
            return this;
        }


        /** スタンプシートの変数に適用する設定値 */
        public List<Config> config { set; get; }

        /**
         * スタンプシートの変数に適用する設定値を設定
         *
         * @param config スタンプシートの変数に適用する設定値
         * @return this
         */
        public EndByUserIdRequest WithConfig(List<Config> config) {
            this.config = config;
            return this;
        }


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public EndByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static EndByUserIdRequest FromDict(JsonData data)
        {
            return new EndByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                transactionId = data.Keys.Contains("transactionId") && data["transactionId"] != null ? data["transactionId"].ToString(): null,
                rewards = data.Keys.Contains("rewards") && data["rewards"] != null ? data["rewards"].Cast<JsonData>().Select(value =>
                    {
                        return Reward.FromDict(value);
                    }
                ).ToList() : null,
                isComplete = data.Keys.Contains("isComplete") && data["isComplete"] != null ? (bool?)bool.Parse(data["isComplete"].ToString()) : null,
                config = data.Keys.Contains("config") && data["config"] != null ? data["config"].Cast<JsonData>().Select(value =>
                    {
                        return Config.FromDict(value);
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}