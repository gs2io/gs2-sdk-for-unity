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
using Gs2.Core.Model;
using LitJson;

namespace Gs2.Gs2Showcase.Model
{
	public class Showcase
	{

        /** 商品名 */
        public string name { set; get; }

        /**
         * 商品名を設定
         *
         * @param name 商品名
         * @return this
         */
        public Showcase WithName(string name) {
            this.name = name;
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
        public Showcase WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** インベントリに格納可能なアイテムモデル一覧 */
        public List<DisplayItem> displayItems { set; get; }

        /**
         * インベントリに格納可能なアイテムモデル一覧を設定
         *
         * @param displayItems インベントリに格納可能なアイテムモデル一覧
         * @return this
         */
        public Showcase WithDisplayItems(List<DisplayItem> displayItems) {
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
        public Showcase WithSalesPeriodEventId(string salesPeriodEventId) {
            this.salesPeriodEventId = salesPeriodEventId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.displayItems != null)
            {
                writer.WritePropertyName("displayItems");
                writer.WriteArrayStart();
                foreach(var item in this.displayItems)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.salesPeriodEventId != null)
            {
                writer.WritePropertyName("salesPeriodEventId");
                writer.Write(this.salesPeriodEventId);
            }
            writer.WriteObjectEnd();
        }
	}
}