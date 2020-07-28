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
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Showcase.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateSalesItemMasterRequest : Gs2Request<UpdateSalesItemMasterRequest>
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
        public UpdateSalesItemMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 商品名 */
		[UnityEngine.SerializeField]
        public string salesItemName;

        /**
         * 商品名を設定
         *
         * @param salesItemName 商品名
         * @return this
         */
        public UpdateSalesItemMasterRequest WithSalesItemName(string salesItemName) {
            this.salesItemName = salesItemName;
            return this;
        }


        /** 商品マスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 商品マスターの説明を設定
         *
         * @param description 商品マスターの説明
         * @return this
         */
        public UpdateSalesItemMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 商品のメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * 商品のメタデータを設定
         *
         * @param metadata 商品のメタデータ
         * @return this
         */
        public UpdateSalesItemMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 消費アクションリスト */
		[UnityEngine.SerializeField]
        public List<ConsumeAction> consumeActions;

        /**
         * 消費アクションリストを設定
         *
         * @param consumeActions 消費アクションリスト
         * @return this
         */
        public UpdateSalesItemMasterRequest WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }


        /** 入手アクションリスト */
		[UnityEngine.SerializeField]
        public List<AcquireAction> acquireActions;

        /**
         * 入手アクションリストを設定
         *
         * @param acquireActions 入手アクションリスト
         * @return this
         */
        public UpdateSalesItemMasterRequest WithAcquireActions(List<AcquireAction> acquireActions) {
            this.acquireActions = acquireActions;
            return this;
        }


    	[Preserve]
        public static UpdateSalesItemMasterRequest FromDict(JsonData data)
        {
            return new UpdateSalesItemMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                salesItemName = data.Keys.Contains("salesItemName") && data["salesItemName"] != null ? data["salesItemName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                consumeActions = data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return ConsumeAction.FromDict(value);
                    }
                ).ToList() : null,
                acquireActions = data.Keys.Contains("acquireActions") && data["acquireActions"] != null ? data["acquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
            };
        }

	}
}