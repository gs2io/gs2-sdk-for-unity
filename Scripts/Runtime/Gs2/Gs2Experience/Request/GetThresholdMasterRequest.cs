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
using Gs2.Gs2Experience.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Experience.Request
{
	[Preserve]
	public class GetThresholdMasterRequest : Gs2Request<GetThresholdMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetThresholdMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ランクアップ閾値名 */
        public string thresholdName { set; get; }

        /**
         * ランクアップ閾値名を設定
         *
         * @param thresholdName ランクアップ閾値名
         * @return this
         */
        public GetThresholdMasterRequest WithThresholdName(string thresholdName) {
            this.thresholdName = thresholdName;
            return this;
        }


    	[Preserve]
        public static GetThresholdMasterRequest FromDict(JsonData data)
        {
            return new GetThresholdMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                thresholdName = data.Keys.Contains("thresholdName") && data["thresholdName"] != null ? data["thresholdName"].ToString(): null,
            };
        }

	}
}