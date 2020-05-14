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
using Gs2.Gs2Project.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Request
{
	[Preserve]
	[System.Serializable]
	public class CreateBillingMethodRequest : Gs2Request<CreateBillingMethodRequest>
	{

        /** GS2アカウントトークン */
		[UnityEngine.SerializeField]
        public string accountToken;

        /**
         * GS2アカウントトークンを設定
         *
         * @param accountToken GS2アカウントトークン
         * @return this
         */
        public CreateBillingMethodRequest WithAccountToken(string accountToken) {
            this.accountToken = accountToken;
            return this;
        }


        /** 名前 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 名前を設定
         *
         * @param description 名前
         * @return this
         */
        public CreateBillingMethodRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 支払い方法 */
		[UnityEngine.SerializeField]
        public string methodType;

        /**
         * 支払い方法を設定
         *
         * @param methodType 支払い方法
         * @return this
         */
        public CreateBillingMethodRequest WithMethodType(string methodType) {
            this.methodType = methodType;
            return this;
        }


        /** クレジットカードカスタマーID */
		[UnityEngine.SerializeField]
        public string cardCustomerId;

        /**
         * クレジットカードカスタマーIDを設定
         *
         * @param cardCustomerId クレジットカードカスタマーID
         * @return this
         */
        public CreateBillingMethodRequest WithCardCustomerId(string cardCustomerId) {
            this.cardCustomerId = cardCustomerId;
            return this;
        }


        /** パートナーID */
		[UnityEngine.SerializeField]
        public string partnerId;

        /**
         * パートナーIDを設定
         *
         * @param partnerId パートナーID
         * @return this
         */
        public CreateBillingMethodRequest WithPartnerId(string partnerId) {
            this.partnerId = partnerId;
            return this;
        }


    	[Preserve]
        public static CreateBillingMethodRequest FromDict(JsonData data)
        {
            return new CreateBillingMethodRequest {
                accountToken = data.Keys.Contains("accountToken") && data["accountToken"] != null ? data["accountToken"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                methodType = data.Keys.Contains("methodType") && data["methodType"] != null ? data["methodType"].ToString(): null,
                cardCustomerId = data.Keys.Contains("cardCustomerId") && data["cardCustomerId"] != null ? data["cardCustomerId"].ToString(): null,
                partnerId = data.Keys.Contains("partnerId") && data["partnerId"] != null ? data["partnerId"].ToString(): null,
            };
        }

	}
}