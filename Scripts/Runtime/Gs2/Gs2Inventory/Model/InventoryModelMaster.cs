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
	public class InventoryModelMaster
	{

        /** インベントリモデルマスター */
        public string inventoryModelId { set; get; }

        /**
         * インベントリモデルマスターを設定
         *
         * @param inventoryModelId インベントリモデルマスター
         * @return this
         */
        public InventoryModelMaster WithInventoryModelId(string inventoryModelId) {
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
        public InventoryModelMaster WithName(string name) {
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
        public InventoryModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** インベントリモデルマスターの説明 */
        public string description { set; get; }

        /**
         * インベントリモデルマスターの説明を設定
         *
         * @param description インベントリモデルマスターの説明
         * @return this
         */
        public InventoryModelMaster WithDescription(string description) {
            this.description = description;
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
        public InventoryModelMaster WithInitialCapacity(int? initialCapacity) {
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
        public InventoryModelMaster WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public InventoryModelMaster WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public InventoryModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
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
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static InventoryModelMaster FromDict(JsonData data)
        {
            return new InventoryModelMaster()
                .WithInventoryModelId(data.Keys.Contains("inventoryModelId") && data["inventoryModelId"] != null ? data["inventoryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithInitialCapacity(data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null)
                .WithMaxCapacity(data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}