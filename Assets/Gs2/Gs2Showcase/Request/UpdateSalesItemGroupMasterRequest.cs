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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Showcase.Model;

namespace Gs2.Gs2Showcase.Request
{
	public class UpdateSalesItemGroupMasterRequest : Gs2Request<UpdateSalesItemGroupMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateSalesItemGroupMasterRequest WithNamespaceName(string namespaceName) {
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
        public UpdateSalesItemGroupMasterRequest WithSalesItemGroupName(string salesItemGroupName) {
            this.salesItemGroupName = salesItemGroupName;
            return this;
        }


        /** 商品グループマスターの説明 */
        public string description { set; get; }

        /**
         * 商品グループマスターの説明を設定
         *
         * @param description 商品グループマスターの説明
         * @return this
         */
        public UpdateSalesItemGroupMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 商品のメタデータ */
        public string metadata { set; get; }

        /**
         * 商品のメタデータを設定
         *
         * @param metadata 商品のメタデータ
         * @return this
         */
        public UpdateSalesItemGroupMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 商品グループに含める商品リスト */
        public List<string> salesItemNames { set; get; }

        /**
         * 商品グループに含める商品リストを設定
         *
         * @param salesItemNames 商品グループに含める商品リスト
         * @return this
         */
        public UpdateSalesItemGroupMasterRequest WithSalesItemNames(List<string> salesItemNames) {
            this.salesItemNames = salesItemNames;
            return this;
        }


	}
}