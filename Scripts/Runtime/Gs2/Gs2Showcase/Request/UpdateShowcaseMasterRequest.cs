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
	public class UpdateShowcaseMasterRequest : Gs2Request<UpdateShowcaseMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateShowcaseMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 陳列棚名 */
        public string showcaseName { set; get; }

        /**
         * 陳列棚名を設定
         *
         * @param showcaseName 陳列棚名
         * @return this
         */
        public UpdateShowcaseMasterRequest WithShowcaseName(string showcaseName) {
            this.showcaseName = showcaseName;
            return this;
        }


        /** 陳列棚マスターの説明 */
        public string description { set; get; }

        /**
         * 陳列棚マスターの説明を設定
         *
         * @param description 陳列棚マスターの説明
         * @return this
         */
        public UpdateShowcaseMasterRequest WithDescription(string description) {
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
        public UpdateShowcaseMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 陳列する商品モデル一覧 */
        public List<DisplayItemMaster> displayItems { set; get; }

        /**
         * 陳列する商品モデル一覧を設定
         *
         * @param displayItems 陳列する商品モデル一覧
         * @return this
         */
        public UpdateShowcaseMasterRequest WithDisplayItems(List<DisplayItemMaster> displayItems) {
            this.displayItems = displayItems;
            return this;
        }


        /** 販売期間とするイベントマスター のGRN */
        public string salesPeriodEventId { set; get; }

        /**
         * 販売期間とするイベントマスター のGRNを設定
         *
         * @param salesPeriodEventId 販売期間とするイベントマスター のGRN
         * @return this
         */
        public UpdateShowcaseMasterRequest WithSalesPeriodEventId(string salesPeriodEventId) {
            this.salesPeriodEventId = salesPeriodEventId;
            return this;
        }


    	[Preserve]
        public static UpdateShowcaseMasterRequest FromDict(JsonData data)
        {
            return new UpdateShowcaseMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                showcaseName = data.Keys.Contains("showcaseName") && data["showcaseName"] != null ? data["showcaseName"].ToString(): null,
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