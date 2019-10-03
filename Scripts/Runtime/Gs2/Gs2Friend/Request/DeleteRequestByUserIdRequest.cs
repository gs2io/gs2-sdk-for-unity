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
using Gs2.Gs2Friend.Model;

namespace Gs2.Gs2Friend.Request
{
	public class DeleteRequestByUserIdRequest : Gs2Request<DeleteRequestByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DeleteRequestByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** リクエストの送信元ユーザID */
        public string userId { set; get; }

        /**
         * リクエストの送信元ユーザIDを設定
         *
         * @param userId リクエストの送信元ユーザID
         * @return this
         */
        public DeleteRequestByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** リクエストの送信先ユーザID */
        public string targetUserId { set; get; }

        /**
         * リクエストの送信先ユーザIDを設定
         *
         * @param targetUserId リクエストの送信先ユーザID
         * @return this
         */
        public DeleteRequestByUserIdRequest WithTargetUserId(string targetUserId) {
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
        public DeleteRequestByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}