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
	public class CreateProjectRequest : Gs2Request<CreateProjectRequest>
	{

        /** GS2アカウントトークン */
        public string accountToken { set; get; }

        /**
         * GS2アカウントトークンを設定
         *
         * @param accountToken GS2アカウントトークン
         * @return this
         */
        public CreateProjectRequest WithAccountToken(string accountToken) {
            this.accountToken = accountToken;
            return this;
        }


        /** プロジェクト名 */
        public string name { set; get; }

        /**
         * プロジェクト名を設定
         *
         * @param name プロジェクト名
         * @return this
         */
        public CreateProjectRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** プロジェクトの説明 */
        public string description { set; get; }

        /**
         * プロジェクトの説明を設定
         *
         * @param description プロジェクトの説明
         * @return this
         */
        public CreateProjectRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


    	[Preserve]
        public static CreateProjectRequest FromDict(JsonData data)
        {
            return new CreateProjectRequest {
                accountToken = data.Keys.Contains("accountToken") && data["accountToken"] != null ? data["accountToken"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
            };
        }

	}
}