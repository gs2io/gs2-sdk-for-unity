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
	public class SalesItemGroup
	{

        /** 商品グループ名 */
        public string name { set; get; }

        /**
         * 商品グループ名を設定
         *
         * @param name 商品グループ名
         * @return this
         */
        public SalesItemGroup WithName(string name) {
            this.name = name;
            return this;
        }

        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public SalesItemGroup WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 商品リスト */
        public List<SalesItem> salesItems { set; get; }

        /**
         * 商品リストを設定
         *
         * @param salesItems 商品リスト
         * @return this
         */
        public SalesItemGroup WithSalesItems(List<SalesItem> salesItems) {
            this.salesItems = salesItems;
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
            if(this.salesItems != null)
            {
                writer.WritePropertyName("salesItems");
                writer.WriteArrayStart();
                foreach(var item in this.salesItems)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SalesItemGroup FromDict(JsonData data)
        {
            return new SalesItemGroup()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithSalesItems(data.Keys.Contains("salesItems") && data["salesItems"] != null ? data["salesItems"].Cast<JsonData>().Select(value =>
                    {
                        return SalesItem.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}