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
	[System.Serializable]
	public class DeleteScriptRequest : Gs2Request<DeleteScriptRequest>
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
        public DeleteScriptRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スクリプト名 */
		[UnityEngine.SerializeField]
        public string scriptName;

        /**
         * スクリプト名を設定
         *
         * @param scriptName スクリプト名
         * @return this
         */
        public DeleteScriptRequest WithScriptName(string scriptName) {
            this.scriptName = scriptName;
            return this;
        }


    	[Preserve]
        public static DeleteScriptRequest FromDict(JsonData data)
        {
            return new DeleteScriptRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                scriptName = data.Keys.Contains("scriptName") && data["scriptName"] != null ? data["scriptName"].ToString(): null,
            };
        }

	}
}