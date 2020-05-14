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
	[System.Serializable]
	public class WithdrawByUserIdRequest : Gs2Request<WithdrawByUserIdRequest>
	{

        /** ネームスペースの名前 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペースの名前を設定
         *
         * @param namespaceName ネームスペースの名前
         * @return this
         */
        public WithdrawByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ユーザーID */
		[UnityEngine.SerializeField]
        public string userId;

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public WithdrawByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** スロット番号 */
		[UnityEngine.SerializeField]
        public int? slot;

        /**
         * スロット番号を設定
         *
         * @param slot スロット番号
         * @return this
         */
        public WithdrawByUserIdRequest WithSlot(int? slot) {
            this.slot = slot;
            return this;
        }


        /** 消費する課金通貨の数量 */
		[UnityEngine.SerializeField]
        public int? count;

        /**
         * 消費する課金通貨の数量を設定
         *
         * @param count 消費する課金通貨の数量
         * @return this
         */
        public WithdrawByUserIdRequest WithCount(int? count) {
            this.count = count;
            return this;
        }


        /** 有償課金通貨のみを対象とするか */
		[UnityEngine.SerializeField]
        public bool? paidOnly;

        /**
         * 有償課金通貨のみを対象とするかを設定
         *
         * @param paidOnly 有償課金通貨のみを対象とするか
         * @return this
         */
        public WithdrawByUserIdRequest WithPaidOnly(bool? paidOnly) {
            this.paidOnly = paidOnly;
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
        public WithdrawByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static WithdrawByUserIdRequest FromDict(JsonData data)
        {
            return new WithdrawByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                slot = data.Keys.Contains("slot") && data["slot"] != null ? (int?)int.Parse(data["slot"].ToString()) : null,
                count = data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null,
                paidOnly = data.Keys.Contains("paidOnly") && data["paidOnly"] != null ? (bool?)bool.Parse(data["paidOnly"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}