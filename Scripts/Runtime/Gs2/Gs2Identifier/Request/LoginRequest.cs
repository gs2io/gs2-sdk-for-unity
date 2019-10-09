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
using Gs2.Gs2Identifier.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Identifier.Request
{
	[Preserve]
	public class LoginRequest : Gs2Request<LoginRequest>
	{

        /** クライアントID */
        public string clientId { set; get; }

        /**
         * クライアントIDを設定
         *
         * @param clientId クライアントID
         * @return this
         */
        public LoginRequest WithClientId(string clientId) {
            this.clientId = clientId;
            return this;
        }


        /** クライアントシークレット */
        public string clientSecret { set; get; }

        /**
         * クライアントシークレットを設定
         *
         * @param clientSecret クライアントシークレット
         * @return this
         */
        public LoginRequest WithClientSecret(string clientSecret) {
            this.clientSecret = clientSecret;
            return this;
        }


    	[Preserve]
        public static LoginRequest FromDict(JsonData data)
        {
            return new LoginRequest {
                clientId = data.Keys.Contains("clientId") && data["clientId"] != null ? data["clientId"].ToString(): null,
                clientSecret = data.Keys.Contains("clientSecret") && data["clientSecret"] != null ? data["clientSecret"].ToString(): null,
            };
        }

	}
}