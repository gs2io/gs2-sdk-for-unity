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
using Gs2.Gs2Watch.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Watch.Request
{
	[Preserve]
	public class GetCumulativeRequest : Gs2Request<GetCumulativeRequest>
	{

        /** 名前 */
        public string name { set; get; }

        /**
         * 名前を設定
         *
         * @param name 名前
         * @return this
         */
        public GetCumulativeRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** リソースのGRN */
        public string resourceGrn { set; get; }

        /**
         * リソースのGRNを設定
         *
         * @param resourceGrn リソースのGRN
         * @return this
         */
        public GetCumulativeRequest WithResourceGrn(string resourceGrn) {
            this.resourceGrn = resourceGrn;
            return this;
        }


    	[Preserve]
        public static GetCumulativeRequest FromDict(JsonData data)
        {
            return new GetCumulativeRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                resourceGrn = data.Keys.Contains("resourceGrn") && data["resourceGrn"] != null ? data["resourceGrn"].ToString(): null,
            };
        }

	}
}