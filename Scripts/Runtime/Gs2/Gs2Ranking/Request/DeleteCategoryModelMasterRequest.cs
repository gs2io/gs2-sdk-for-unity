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
using Gs2.Gs2Ranking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Ranking.Request
{
	[Preserve]
	public class DeleteCategoryModelMasterRequest : Gs2Request<DeleteCategoryModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DeleteCategoryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** カテゴリモデル名 */
        public string categoryName { set; get; }

        /**
         * カテゴリモデル名を設定
         *
         * @param categoryName カテゴリモデル名
         * @return this
         */
        public DeleteCategoryModelMasterRequest WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }


    	[Preserve]
        public static DeleteCategoryModelMasterRequest FromDict(JsonData data)
        {
            return new DeleteCategoryModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                categoryName = data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString(): null,
            };
        }

	}
}