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
 *
 * deny overwrite
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Script.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Script.Request
{
	[Preserve]
	[System.Serializable]
	public class InvokeScriptRequest : Gs2Request<InvokeScriptRequest>
	{

        /** スクリプト */
		[UnityEngine.SerializeField]
        public string scriptId;

        /**
         * スクリプトを設定
         *
         * @param scriptId スクリプト
         * @return this
         */
        public InvokeScriptRequest WithScriptId(string scriptId) {
            this.scriptId = scriptId;
            return this;
        }


        /** None */
		[UnityEngine.SerializeField]
        public Dictionary<string, object> args;

        /**
         * Noneを設定
         *
         * @param args None
         * @return this
         */
        public InvokeScriptRequest WithArgs(Dictionary<string, object> args) {
            this.args = args;
            return this;
        }


    	[Preserve]
        public static InvokeScriptRequest FromDict(JsonData data)
        {
            return new InvokeScriptRequest {
                scriptId = data.Keys.Contains("scriptId") && data["scriptId"] != null ? data["scriptId"].ToString(): null,
                // args = data.Keys.Contains("args") && data["args"] != null ? (Dictionary<string, object>)Dictionary<string, object>.Parse(data["args"].ToString()) : null,
            };
        }

	}
}