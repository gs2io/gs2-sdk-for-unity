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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	public class UpdateCurrentQuestMasterFromGitHubRequest : Gs2Request<UpdateCurrentQuestMasterFromGitHubRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public UpdateCurrentQuestMasterFromGitHubRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** GitHubからマスターデータをチェックアウトしてくる設定 */
        public GitHubCheckoutSetting checkoutSetting { set; get; }

        /**
         * GitHubからマスターデータをチェックアウトしてくる設定を設定
         *
         * @param checkoutSetting GitHubからマスターデータをチェックアウトしてくる設定
         * @return this
         */
        public UpdateCurrentQuestMasterFromGitHubRequest WithCheckoutSetting(GitHubCheckoutSetting checkoutSetting) {
            this.checkoutSetting = checkoutSetting;
            return this;
        }


    	[Preserve]
        public static UpdateCurrentQuestMasterFromGitHubRequest FromDict(JsonData data)
        {
            return new UpdateCurrentQuestMasterFromGitHubRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                checkoutSetting = data.Keys.Contains("checkoutSetting") && data["checkoutSetting"] != null ? GitHubCheckoutSetting.FromDict(data["checkoutSetting"]) : null,
            };
        }

	}
}