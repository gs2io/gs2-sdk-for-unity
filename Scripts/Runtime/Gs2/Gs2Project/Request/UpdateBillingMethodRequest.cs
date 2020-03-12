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
	public class UpdateBillingMethodRequest : Gs2Request<UpdateBillingMethodRequest>
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
        public UpdateBillingMethodRequest WithAccountToken(string accountToken) {
            this.accountToken = accountToken;
            return this;
        }


        /** 名前 */
		[UnityEngine.SerializeField]
        public string billingMethodName;

        /**
         * 名前を設定
         *
         * @param billingMethodName 名前
         * @return this
         */
        public UpdateBillingMethodRequest WithBillingMethodName(string billingMethodName) {
            this.billingMethodName = billingMethodName;
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
        public UpdateBillingMethodRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


    	[Preserve]
        public static UpdateBillingMethodRequest FromDict(JsonData data)
        {
            return new UpdateBillingMethodRequest {
                accountToken = data.Keys.Contains("accountToken") && data["accountToken"] != null ? data["accountToken"].ToString(): null,
                billingMethodName = data.Keys.Contains("billingMethodName") && data["billingMethodName"] != null ? data["billingMethodName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
            };
        }

	}
}