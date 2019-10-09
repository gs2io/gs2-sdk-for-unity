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
using Gs2.Gs2Script.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Script.Request
{
	[Preserve]
	public class UpdateScriptFromGitHubRequest : Gs2Request<UpdateScriptFromGitHubRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateScriptFromGitHubRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スクリプト名 */
        public string scriptName { set; get; }

        /**
         * スクリプト名を設定
         *
         * @param scriptName スクリプト名
         * @return this
         */
        public UpdateScriptFromGitHubRequest WithScriptName(string scriptName) {
            this.scriptName = scriptName;
            return this;
        }


        /** 説明文 */
        public string description { set; get; }

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public UpdateScriptFromGitHubRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** GitHubからソースコードをチェックアウトしてくる設定 */
        public GitHubCheckoutSetting checkoutSetting { set; get; }

        /**
         * GitHubからソースコードをチェックアウトしてくる設定を設定
         *
         * @param checkoutSetting GitHubからソースコードをチェックアウトしてくる設定
         * @return this
         */
        public UpdateScriptFromGitHubRequest WithCheckoutSetting(GitHubCheckoutSetting checkoutSetting) {
            this.checkoutSetting = checkoutSetting;
            return this;
        }


    	[Preserve]
        public static UpdateScriptFromGitHubRequest FromDict(JsonData data)
        {
            return new UpdateScriptFromGitHubRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                scriptName = data.Keys.Contains("scriptName") && data["scriptName"] != null ? data["scriptName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                checkoutSetting = data.Keys.Contains("checkoutSetting") && data["checkoutSetting"] != null ? GitHubCheckoutSetting.FromDict(data["checkoutSetting"]) : null,
            };
        }

	}
}