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
using Gs2.Gs2Friend.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Friend.Request
{
	[Preserve]
	public class AcceptRequestByUserIdRequest : Gs2Request<AcceptRequestByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public AcceptRequestByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フレンドリクエストを受け取ったユーザID */
        public string userId { set; get; }

        /**
         * フレンドリクエストを受け取ったユーザIDを設定
         *
         * @param userId フレンドリクエストを受け取ったユーザID
         * @return this
         */
        public AcceptRequestByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** フレンドリクエストを送信したユーザID */
        public string fromUserId { set; get; }

        /**
         * フレンドリクエストを送信したユーザIDを設定
         *
         * @param fromUserId フレンドリクエストを送信したユーザID
         * @return this
         */
        public AcceptRequestByUserIdRequest WithFromUserId(string fromUserId) {
            this.fromUserId = fromUserId;
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
        public AcceptRequestByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static AcceptRequestByUserIdRequest FromDict(JsonData data)
        {
            return new AcceptRequestByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                fromUserId = data.Keys.Contains("fromUserId") && data["fromUserId"] != null ? data["fromUserId"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}