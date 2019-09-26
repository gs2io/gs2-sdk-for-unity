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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Exchange.Model;

namespace Gs2.Gs2Exchange.Request
{
	public class ExchangeByUserIdRequest : Gs2Request<ExchangeByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public ExchangeByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 交換レートの種類名 */
        public string rateName { set; get; }

        /**
         * 交換レートの種類名を設定
         *
         * @param rateName 交換レートの種類名
         * @return this
         */
        public ExchangeByUserIdRequest WithRateName(string rateName) {
            this.rateName = rateName;
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
        public ExchangeByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 交換するロット数 */
        public int? count { set; get; }

        /**
         * 交換するロット数を設定
         *
         * @param count 交換するロット数
         * @return this
         */
        public ExchangeByUserIdRequest WithCount(int? count) {
            this.count = count;
            return this;
        }


        /** 設定値 */
        public List<Config> config { set; get; }

        /**
         * 設定値を設定
         *
         * @param config 設定値
         * @return this
         */
        public ExchangeByUserIdRequest WithConfig(List<Config> config) {
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
        public ExchangeByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}