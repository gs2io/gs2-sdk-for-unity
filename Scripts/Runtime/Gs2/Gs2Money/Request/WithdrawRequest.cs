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
using Gs2.Gs2Money.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Money.Request
{
	[Preserve]
	public class WithdrawRequest : Gs2Request<WithdrawRequest>
	{

        /** ネームスペースの名前 */
        public string namespaceName { set; get; }

        /**
         * ネームスペースの名前を設定
         *
         * @param namespaceName ネームスペースの名前
         * @return this
         */
        public WithdrawRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スロット番号 */
        public int? slot { set; get; }

        /**
         * スロット番号を設定
         *
         * @param slot スロット番号
         * @return this
         */
        public WithdrawRequest WithSlot(int? slot) {
            this.slot = slot;
            return this;
        }


        /** 消費する課金通貨の数量 */
        public int? count { set; get; }

        /**
         * 消費する課金通貨の数量を設定
         *
         * @param count 消費する課金通貨の数量
         * @return this
         */
        public WithdrawRequest WithCount(int? count) {
            this.count = count;
            return this;
        }


        /** 有償課金通貨のみを対象とするか */
        public bool? paidOnly { set; get; }

        /**
         * 有償課金通貨のみを対象とするかを設定
         *
         * @param paidOnly 有償課金通貨のみを対象とするか
         * @return this
         */
        public WithdrawRequest WithPaidOnly(bool? paidOnly) {
            this.paidOnly = paidOnly;
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
        public WithdrawRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public WithdrawRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static WithdrawRequest FromDict(JsonData data)
        {
            return new WithdrawRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                slot = data.Keys.Contains("slot") && data["slot"] != null ? (int?)int.Parse(data["slot"].ToString()) : null,
                count = data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null,
                paidOnly = data.Keys.Contains("paidOnly") && data["paidOnly"] != null ? (bool?)bool.Parse(data["paidOnly"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}