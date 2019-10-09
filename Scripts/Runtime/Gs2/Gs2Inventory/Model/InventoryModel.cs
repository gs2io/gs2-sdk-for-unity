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

namespace Gs2.Gs2Inventory.Model
{
	[Preserve]
	public class InventoryModel
	{

        /** インベントリモデルマスター */
        public string inventoryModelId { set; get; }

        /**
         * インベントリモデルマスターを設定
         *
         * @param inventoryModelId インベントリモデルマスター
         * @return this
         */
        public InventoryModel WithInventoryModelId(string inventoryModelId) {
            this.inventoryModelId = inventoryModelId;
            return this;
        }

        /** インベントリの種類名 */
        public string name { set; get; }

        /**
         * インベントリの種類名を設定
         *
         * @param name インベントリの種類名
         * @return this
         */
        public InventoryModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** インベントリの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * インベントリの種類のメタデータを設定
         *
         * @param metadata インベントリの種類のメタデータ
         * @return this
         */
        public InventoryModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** インベントリの初期サイズ */
        public int? initialCapacity { set; get; }

        /**
         * インベントリの初期サイズを設定
         *
         * @param initialCapacity インベントリの初期サイズ
         * @return this
         */
        public InventoryModel WithInitialCapacity(int? initialCapacity) {
            this.initialCapacity = initialCapacity;
            return this;
        }

        /** インベントリの最大サイズ */
        public int? maxCapacity { set; get; }

        /**
         * インベントリの最大サイズを設定
         *
         * @param maxCapacity インベントリの最大サイズ
         * @return this
         */
        public InventoryModel WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }

        /** インベントリに格納可能なアイテムモデル一覧 */
        public List<ItemModel> itemModels { set; get; }

        /**
         * インベントリに格納可能なアイテムモデル一覧を設定
         *
         * @param itemModels インベントリに格納可能なアイテムモデル一覧
         * @return this
         */
        public InventoryModel WithItemModels(List<ItemModel> itemModels) {
            this.itemModels = itemModels;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.inventoryModelId != null)
            {
                writer.WritePropertyName("inventoryModelId");
                writer.Write(this.inventoryModelId);
            }
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
            if(this.initialCapacity.HasValue)
            {
                writer.WritePropertyName("initialCapacity");
                writer.Write(this.initialCapacity.Value);
            }
            if(this.maxCapacity.HasValue)
            {
                writer.WritePropertyName("maxCapacity");
                writer.Write(this.maxCapacity.Value);
            }
            if(this.itemModels != null)
            {
                writer.WritePropertyName("itemModels");
                writer.WriteArrayStart();
                foreach(var item in this.itemModels)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static InventoryModel FromDict(JsonData data)
        {
            return new InventoryModel()
                .WithInventoryModelId(data.Keys.Contains("inventoryModelId") && data["inventoryModelId"] != null ? data["inventoryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithInitialCapacity(data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null)
                .WithMaxCapacity(data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null)
                .WithItemModels(data.Keys.Contains("itemModels") && data["itemModels"] != null ? data["itemModels"].Cast<JsonData>().Select(value =>
                    {
                        return ItemModel.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}