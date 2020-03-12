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
	public class UpdateStackRequest : Gs2Request<UpdateStackRequest>
	{

        /** スタック名 */
        public string stackName { set; get; }

        /**
         * スタック名を設定
         *
         * @param stackName スタック名
         * @return this
         */
        public UpdateStackRequest WithStackName(string stackName) {
            this.stackName = stackName;
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
        public UpdateStackRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** テンプレートデータ */
        public string template { set; get; }

        /**
         * テンプレートデータを設定
         *
         * @param template テンプレートデータ
         * @return this
         */
        public UpdateStackRequest WithTemplate(string template) {
            this.template = template;
            return this;
        }


    	[Preserve]
        public static UpdateStackRequest FromDict(JsonData data)
        {
            return new UpdateStackRequest {
                stackName = data.Keys.Contains("stackName") && data["stackName"] != null ? data["stackName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                template = data.Keys.Contains("template") && data["template"] != null ? data["template"].ToString(): null,
            };
        }

	}
}