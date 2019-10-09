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
	public class DebugInvokeRequest : Gs2Request<DebugInvokeRequest>
	{

        /** スクリプト */
        public string script { set; get; }

        /**
         * スクリプトを設定
         *
         * @param script スクリプト
         * @return this
         */
        public DebugInvokeRequest WithScript(string script) {
            this.script = script;
            return this;
        }


        /** None */
        public string args { set; get; }

        /**
         * Noneを設定
         *
         * @param args None
         * @return this
         */
        public DebugInvokeRequest WithArgs(string args) {
            this.args = args;
            return this;
        }


    	[Preserve]
        public static DebugInvokeRequest FromDict(JsonData data)
        {
            return new DebugInvokeRequest {
                script = data.Keys.Contains("script") && data["script"] != null ? data["script"].ToString(): null,
                args = data.Keys.Contains("args") && data["args"] != null ? data["args"].ToString(): null,
            };
        }

	}
}