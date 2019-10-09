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
using Gs2.Gs2Showcase.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Showcase.Request
{
	[Preserve]
	public class GetSalesItemGroupMasterRequest : Gs2Request<GetSalesItemGroupMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetSalesItemGroupMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 商品名 */
        public string salesItemGroupName { set; get; }

        /**
         * 商品名を設定
         *
         * @param salesItemGroupName 商品名
         * @return this
         */
        public GetSalesItemGroupMasterRequest WithSalesItemGroupName(string salesItemGroupName) {
            this.salesItemGroupName = salesItemGroupName;
            return this;
        }


    	[Preserve]
        public static GetSalesItemGroupMasterRequest FromDict(JsonData data)
        {
            return new GetSalesItemGroupMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                salesItemGroupName = data.Keys.Contains("salesItemGroupName") && data["salesItemGroupName"] != null ? data["salesItemGroupName"].ToString(): null,
            };
        }

	}
}