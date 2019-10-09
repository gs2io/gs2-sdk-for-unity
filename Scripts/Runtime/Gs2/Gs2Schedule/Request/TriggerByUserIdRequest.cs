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
using Gs2.Gs2Schedule.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Schedule.Request
{
	[Preserve]
	public class TriggerByUserIdRequest : Gs2Request<TriggerByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public TriggerByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** トリガーの名前 */
        public string triggerName { set; get; }

        /**
         * トリガーの名前を設定
         *
         * @param triggerName トリガーの名前
         * @return this
         */
        public TriggerByUserIdRequest WithTriggerName(string triggerName) {
            this.triggerName = triggerName;
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
        public TriggerByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** トリガーの引き方の方針 */
        public string triggerStrategy { set; get; }

        /**
         * トリガーの引き方の方針を設定
         *
         * @param triggerStrategy トリガーの引き方の方針
         * @return this
         */
        public TriggerByUserIdRequest WithTriggerStrategy(string triggerStrategy) {
            this.triggerStrategy = triggerStrategy;
            return this;
        }


        /** トリガーの有効期限(秒) */
        public int? ttl { set; get; }

        /**
         * トリガーの有効期限(秒)を設定
         *
         * @param ttl トリガーの有効期限(秒)
         * @return this
         */
        public TriggerByUserIdRequest WithTtl(int? ttl) {
            this.ttl = ttl;
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
        public TriggerByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static TriggerByUserIdRequest FromDict(JsonData data)
        {
            return new TriggerByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                triggerName = data.Keys.Contains("triggerName") && data["triggerName"] != null ? data["triggerName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                triggerStrategy = data.Keys.Contains("triggerStrategy") && data["triggerStrategy"] != null ? data["triggerStrategy"].ToString(): null,
                ttl = data.Keys.Contains("ttl") && data["ttl"] != null ? (int?)int.Parse(data["ttl"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}