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
	[System.Serializable]
	public class UpdateGitHubApiKeyRequest : Gs2Request<UpdateGitHubApiKeyRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateGitHubApiKeyRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** GitHub APIキー名 */
		[UnityEngine.SerializeField]
        public string apiKeyName;

        /**
         * GitHub APIキー名を設定
         *
         * @param apiKeyName GitHub APIキー名
         * @return this
         */
        public UpdateGitHubApiKeyRequest WithApiKeyName(string apiKeyName) {
            this.apiKeyName = apiKeyName;
            return this;
        }


        /** 説明文 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public UpdateGitHubApiKeyRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** APIキー */
		[UnityEngine.SerializeField]
        public string apiKey;

        /**
         * APIキーを設定
         *
         * @param apiKey APIキー
         * @return this
         */
        public UpdateGitHubApiKeyRequest WithApiKey(string apiKey) {
            this.apiKey = apiKey;
            return this;
        }


        /** APIキーの暗号化に使用する暗号鍵名 */
		[UnityEngine.SerializeField]
        public string encryptionKeyName;

        /**
         * APIキーの暗号化に使用する暗号鍵名を設定
         *
         * @param encryptionKeyName APIキーの暗号化に使用する暗号鍵名
         * @return this
         */
        public UpdateGitHubApiKeyRequest WithEncryptionKeyName(string encryptionKeyName) {
            this.encryptionKeyName = encryptionKeyName;
            return this;
        }


    	[Preserve]
        public static UpdateGitHubApiKeyRequest FromDict(JsonData data)
        {
            return new UpdateGitHubApiKeyRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                apiKeyName = data.Keys.Contains("apiKeyName") && data["apiKeyName"] != null ? data["apiKeyName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                apiKey = data.Keys.Contains("apiKey") && data["apiKey"] != null ? data["apiKey"].ToString(): null,
                encryptionKeyName = data.Keys.Contains("encryptionKeyName") && data["encryptionKeyName"] != null ? data["encryptionKeyName"].ToString(): null,
            };
        }

	}
}