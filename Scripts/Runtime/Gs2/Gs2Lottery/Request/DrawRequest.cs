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
using Gs2.Gs2Lottery.Model;

namespace Gs2.Gs2Lottery.Request
{
	public class DrawRequest : Gs2Request<DrawRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DrawRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 抽選モデルの種類名 */
        public string lotteryName { set; get; }

        /**
         * 抽選モデルの種類名を設定
         *
         * @param lotteryName 抽選モデルの種類名
         * @return this
         */
        public DrawRequest WithLotteryName(string lotteryName) {
            this.lotteryName = lotteryName;
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
        public DrawRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


        /** アクセストークン */
        public string accessToken { set; get; }

        /**
         * アクセストークンを設定
         *
         * @param accessToken アクセストークン
         * @return this
         */
        public DrawRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

	}
}