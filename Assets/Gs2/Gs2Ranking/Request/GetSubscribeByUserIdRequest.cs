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
using Gs2.Gs2Ranking.Model;

namespace Gs2.Gs2Ranking.Request
{
	public class GetSubscribeByUserIdRequest : Gs2Request<GetSubscribeByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetSubscribeByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public GetSubscribeByUserIdRequest WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }


        /** 購読するユーザID */
        public string userId { set; get; }

        /**
         * 購読するユーザIDを設定
         *
         * @param userId 購読するユーザID
         * @return this
         */
        public GetSubscribeByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 購読されるユーザID */
        public string targetUserId { set; get; }

        /**
         * 購読されるユーザIDを設定
         *
         * @param targetUserId 購読されるユーザID
         * @return this
         */
        public GetSubscribeByUserIdRequest WithTargetUserId(string targetUserId) {
            this.targetUserId = targetUserId;
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
        public GetSubscribeByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}