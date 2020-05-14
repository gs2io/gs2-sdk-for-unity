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
	[System.Serializable]
	public class CreateShowcaseMasterRequest : Gs2Request<CreateShowcaseMasterRequest>
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
        public CreateShowcaseMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 陳列棚名 */
		[UnityEngine.SerializeField]
        public string name;

        /**
         * 陳列棚名を設定
         *
         * @param name 陳列棚名
         * @return this
         */
        public CreateShowcaseMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** 陳列棚マスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 陳列棚マスターの説明を設定
         *
         * @param description 陳列棚マスターの説明
         * @return this
         */
        public CreateShowcaseMasterRequest WithDescription(string description) {
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
        public CreateShowcaseMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 陳列する商品モデル一覧 */
		[UnityEngine.SerializeField]
        public List<DisplayItemMaster> displayItems;

        /**
         * 陳列する商品モデル一覧を設定
         *
         * @param displayItems 陳列する商品モデル一覧
         * @return this
         */
        public CreateShowcaseMasterRequest WithDisplayItems(List<DisplayItemMaster> displayItems) {
            this.displayItems = displayItems;
            return this;
        }


        /** 販売期間とするイベントマスター のGRN */
		[UnityEngine.SerializeField]
        public string salesPeriodEventId;

        /**
         * 販売期間とするイベントマスター のGRNを設定
         *
         * @param salesPeriodEventId 販売期間とするイベントマスター のGRN
         * @return this
         */
        public CreateShowcaseMasterRequest WithSalesPeriodEventId(string salesPeriodEventId) {
            this.salesPeriodEventId = salesPeriodEventId;
            return this;
        }


    	[Preserve]
        public static CreateShowcaseMasterRequest FromDict(JsonData data)
        {
            return new CreateShowcaseMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                displayItems = data.Keys.Contains("displayItems") && data["displayItems"] != null ? data["displayItems"].Cast<JsonData>().Select(value =>
                    {
                        return DisplayItemMaster.FromDict(value);
                    }
                ).ToList() : null,
                salesPeriodEventId = data.Keys.Contains("salesPeriodEventId") && data["salesPeriodEventId"] != null ? data["salesPeriodEventId"].ToString(): null,
            };
        }

	}
}