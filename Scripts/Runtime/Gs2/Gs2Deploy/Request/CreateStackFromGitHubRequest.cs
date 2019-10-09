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
using Gs2.Gs2Deploy.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Deploy.Request
{
	[Preserve]
	public class CreateStackFromGitHubRequest : Gs2Request<CreateStackFromGitHubRequest>
	{

        /** スタック名 */
        public string name { set; get; }

        /**
         * スタック名を設定
         *
         * @param name スタック名
         * @return this
         */
        public CreateStackFromGitHubRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** スタックの説明 */
        public string description { set; get; }

        /**
         * スタックの説明を設定
         *
         * @param description スタックの説明
         * @return this
         */
        public CreateStackFromGitHubRequest WithDescription(string description) {
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
        public CreateStackFromGitHubRequest WithCheckoutSetting(GitHubCheckoutSetting checkoutSetting) {
            this.checkoutSetting = checkoutSetting;
            return this;
        }


    	[Preserve]
        public static CreateStackFromGitHubRequest FromDict(JsonData data)
        {
            return new CreateStackFromGitHubRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                checkoutSetting = data.Keys.Contains("checkoutSetting") && data["checkoutSetting"] != null ? GitHubCheckoutSetting.FromDict(data["checkoutSetting"]) : null,
            };
        }

	}
}