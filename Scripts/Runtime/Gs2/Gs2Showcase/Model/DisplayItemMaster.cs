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
	public class DisplayItemMaster
	{

        /** 陳列商品ID */
        public string displayItemId { set; get; }

        /**
         * 陳列商品IDを設定
         *
         * @param displayItemId 陳列商品ID
         * @return this
         */
        public DisplayItemMaster WithDisplayItemId(string displayItemId) {
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
        public DisplayItemMaster WithType(string type) {
            this.type = type;
            return this;
        }

        /** 陳列する商品の名前 */
        public string salesItemName { set; get; }

        /**
         * 陳列する商品の名前を設定
         *
         * @param salesItemName 陳列する商品の名前
         * @return this
         */
        public DisplayItemMaster WithSalesItemName(string salesItemName) {
            this.salesItemName = salesItemName;
            return this;
        }

        /** 陳列する商品グループの名前 */
        public string salesItemGroupName { set; get; }

        /**
         * 陳列する商品グループの名前を設定
         *
         * @param salesItemGroupName 陳列する商品グループの名前
         * @return this
         */
        public DisplayItemMaster WithSalesItemGroupName(string salesItemGroupName) {
            this.salesItemGroupName = salesItemGroupName;
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
        public DisplayItemMaster WithSalesPeriodEventId(string salesPeriodEventId) {
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
            if(this.salesItemName != null)
            {
                writer.WritePropertyName("salesItemName");
                writer.Write(this.salesItemName);
            }
            if(this.salesItemGroupName != null)
            {
                writer.WritePropertyName("salesItemGroupName");
                writer.Write(this.salesItemGroupName);
            }
            if(this.salesPeriodEventId != null)
            {
                writer.WritePropertyName("salesPeriodEventId");
                writer.Write(this.salesPeriodEventId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static DisplayItemMaster FromDict(JsonData data)
        {
            return new DisplayItemMaster()
                .WithDisplayItemId(data.Keys.Contains("displayItemId") && data["displayItemId"] != null ? data["displayItemId"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithSalesItemName(data.Keys.Contains("salesItemName") && data["salesItemName"] != null ? data["salesItemName"].ToString() : null)
                .WithSalesItemGroupName(data.Keys.Contains("salesItemGroupName") && data["salesItemGroupName"] != null ? data["salesItemGroupName"].ToString() : null)
                .WithSalesPeriodEventId(data.Keys.Contains("salesPeriodEventId") && data["salesPeriodEventId"] != null ? data["salesPeriodEventId"].ToString() : null);
        }
	}
}