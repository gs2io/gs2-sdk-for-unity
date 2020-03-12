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
using Gs2.Gs2Lock.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Lock.Request
{
	[Preserve]
	[System.Serializable]
	public class LockRequest : Gs2Request<LockRequest>
	{

        /** カテゴリー名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public LockRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** プロパティID */
		[UnityEngine.SerializeField]
        public string propertyId;

        /**
         * プロパティIDを設定
         *
         * @param propertyId プロパティID
         * @return this
         */
        public LockRequest WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
            return this;
        }


        /** ロックを取得するトランザクションID */
		[UnityEngine.SerializeField]
        public string transactionId;

        /**
         * ロックを取得するトランザクションIDを設定
         *
         * @param transactionId ロックを取得するトランザクションID
         * @return this
         */
        public LockRequest WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
            return this;
        }


        /** ロックを取得する期限（秒） */
		[UnityEngine.SerializeField]
        public long? ttl;

        /**
         * ロックを取得する期限（秒）を設定
         *
         * @param ttl ロックを取得する期限（秒）
         * @return this
         */
        public LockRequest WithTtl(long? ttl) {
            this.ttl = ttl;
            return this;
        }


        /** 重複実行回避機能に使用するID */
		[UnityEngine.SerializeField]
        public string duplicationAvoider;

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public LockRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public LockRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static LockRequest FromDict(JsonData data)
        {
            return new LockRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                propertyId = data.Keys.Contains("propertyId") && data["propertyId"] != null ? data["propertyId"].ToString(): null,
                transactionId = data.Keys.Contains("transactionId") && data["transactionId"] != null ? data["transactionId"].ToString(): null,
                ttl = data.Keys.Contains("ttl") && data["ttl"] != null ? (long?)long.Parse(data["ttl"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}