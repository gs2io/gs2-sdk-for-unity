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
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Showcase.Model
{
	[Preserve]
	public class DisplayItem
	{

        /** 陳列商品ID */
        public string displayItemId { set; get; }

        /**
         * 陳列商品IDを設定
         *
         * @param displayItemId 陳列商品ID
         * @return this
         */
        public DisplayItem WithDisplayItemId(string displayItemId) {
            this.displayItemId = displayItemId;
            return this;
        }

        /** 種類 */
        public string type { set; get; }

        /**
         * 種類を設定
         *
         * @param type 種類
         * @return this
         */
        public DisplayItem WithType(string type) {
            this.type = type;
            return this;
        }

        /** 陳列する商品 */
        public SalesItem salesItem { set; get; }

        /**
         * 陳列する商品を設定
         *
         * @param salesItem 陳列する商品
         * @return this
         */
        public DisplayItem WithSalesItem(SalesItem salesItem) {
            this.salesItem = salesItem;
            return this;
        }

        /** 陳列する商品グループ */
        public SalesItemGroup salesItemGroup { set; get; }

        /**
         * 陳列する商品グループを設定
         *
         * @param salesItemGroup 陳列する商品グループ
         * @return this
         */
        public DisplayItem WithSalesItemGroup(SalesItemGroup salesItemGroup) {
            this.salesItemGroup = salesItemGroup;
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
        public DisplayItem WithSalesPeriodEventId(string salesPeriodEventId) {
            this.salesPeriodEventId = salesPeriodEventId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.displayItemId != null)
            {
                writer.WritePropertyName("displayItemId");
                writer.Write(this.displayItemId);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
            }
            if(this.salesItem != null)
            {
                writer.WritePropertyName("salesItem");
                this.salesItem.WriteJson(writer);
            }
            if(this.salesItemGroup != null)
            {
                writer.WritePropertyName("salesItemGroup");
                this.salesItemGroup.WriteJson(writer);
            }
            if(this.salesPeriodEventId != null)
            {
                writer.WritePropertyName("salesPeriodEventId");
                writer.Write(this.salesPeriodEventId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static DisplayItem FromDict(JsonData data)
        {
            return new DisplayItem()
                .WithDisplayItemId(data.Keys.Contains("displayItemId") && data["displayItemId"] != null ? data["displayItemId"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithSalesItem(data.Keys.Contains("salesItem") && data["salesItem"] != null ? SalesItem.FromDict(data["salesItem"]) : null)
                .WithSalesItemGroup(data.Keys.Contains("salesItemGroup") && data["salesItemGroup"] != null ? SalesItemGroup.FromDict(data["salesItemGroup"]) : null)
                .WithSalesPeriodEventId(data.Keys.Contains("salesPeriodEventId") && data["salesPeriodEventId"] != null ? data["salesPeriodEventId"].ToString() : null);
        }
	}
}