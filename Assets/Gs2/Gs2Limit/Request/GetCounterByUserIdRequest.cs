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
using Gs2.Gs2Limit.Model;

namespace Gs2.Gs2Limit.Request
{
	public class GetCounterByUserIdRequest : Gs2Request<GetCounterByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetCounterByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 回数制限の種類の名前 */
        public string limitName { set; get; }

        /**
         * 回数制限の種類の名前を設定
         *
         * @param limitName 回数制限の種類の名前
         * @return this
         */
        public GetCounterByUserIdRequest WithLimitName(string limitName) {
            this.limitName = limitName;
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
        public GetCounterByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** カウンターの名前 */
        public string counterName { set; get; }

        /**
         * カウンターの名前を設定
         *
         * @param counterName カウンターの名前
         * @return this
         */
        public GetCounterByUserIdRequest WithCounterName(string counterName) {
            this.counterName = counterName;
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
        public GetCounterByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}