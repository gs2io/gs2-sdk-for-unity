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
using Gs2.Gs2Exchange.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Exchange.Request
{
	[Preserve]
	public class GetRateModelRequest : Gs2Request<GetRateModelRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetRateModelRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 交換レート名 */
        public string rateName { set; get; }

        /**
         * 交換レート名を設定
         *
         * @param rateName 交換レート名
         * @return this
         */
        public GetRateModelRequest WithRateName(string rateName) {
            this.rateName = rateName;
            return this;
        }


    	[Preserve]
        public static GetRateModelRequest FromDict(JsonData data)
        {
            return new GetRateModelRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                rateName = data.Keys.Contains("rateName") && data["rateName"] != null ? data["rateName"].ToString(): null,
            };
        }

	}
}