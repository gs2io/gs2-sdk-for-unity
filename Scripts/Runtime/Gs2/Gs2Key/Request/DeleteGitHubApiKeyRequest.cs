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
using Gs2.Gs2Key.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Key.Request
{
	[Preserve]
	public class DeleteGitHubApiKeyRequest : Gs2Request<DeleteGitHubApiKeyRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DeleteGitHubApiKeyRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** GitHub APIキー名 */
        public string apiKeyName { set; get; }

        /**
         * GitHub APIキー名を設定
         *
         * @param apiKeyName GitHub APIキー名
         * @return this
         */
        public DeleteGitHubApiKeyRequest WithApiKeyName(string apiKeyName) {
            this.apiKeyName = apiKeyName;
            return this;
        }


    	[Preserve]
        public static DeleteGitHubApiKeyRequest FromDict(JsonData data)
        {
            return new DeleteGitHubApiKeyRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                apiKeyName = data.Keys.Contains("apiKeyName") && data["apiKeyName"] != null ? data["apiKeyName"].ToString(): null,
            };
        }

	}
}