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
	public class UpdateSecurityPolicyRequest : Gs2Request<UpdateSecurityPolicyRequest>
	{

        /** セキュリティポリシー名 */
        public string securityPolicyName { set; get; }

        /**
         * セキュリティポリシー名を設定
         *
         * @param securityPolicyName セキュリティポリシー名
         * @return this
         */
        public UpdateSecurityPolicyRequest WithSecurityPolicyName(string securityPolicyName) {
            this.securityPolicyName = securityPolicyName;
            return this;
        }


        /** セキュリティポリシーの説明 */
        public string description { set; get; }

        /**
         * セキュリティポリシーの説明を設定
         *
         * @param description セキュリティポリシーの説明
         * @return this
         */
        public UpdateSecurityPolicyRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** ポリシードキュメント */
        public string policy { set; get; }

        /**
         * ポリシードキュメントを設定
         *
         * @param policy ポリシードキュメント
         * @return this
         */
        public UpdateSecurityPolicyRequest WithPolicy(string policy) {
            this.policy = policy;
            return this;
        }


    	[Preserve]
        public static UpdateSecurityPolicyRequest FromDict(JsonData data)
        {
            return new UpdateSecurityPolicyRequest {
                securityPolicyName = data.Keys.Contains("securityPolicyName") && data["securityPolicyName"] != null ? data["securityPolicyName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                policy = data.Keys.Contains("policy") && data["policy"] != null ? data["policy"].ToString(): null,
            };
        }

	}
}