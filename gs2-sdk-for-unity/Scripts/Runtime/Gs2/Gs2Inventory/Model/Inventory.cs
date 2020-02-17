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
	public class Inventory
	{

        /** インベントリ */
        public string inventoryId { set; get; }

        /**
         * インベントリを設定
         *
         * @param inventoryId インベントリ
         * @return this
         */
        public Inventory WithInventoryId(string inventoryId) {
            this.inventoryId = inventoryId;
            return this;
        }

        /** インベントリモデル名 */
        public string inventoryName { set; get; }

        /**
         * インベントリモデル名を設定
         *
         * @param inventoryName インベントリモデル名
         * @return this
         */
        public Inventory WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
            return this;
        }

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Inventory WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 現在のインベントリのキャパシティ使用量 */
        public int? currentInventoryCapacityUsage { set; get; }

        /**
         * 現在のインベントリのキャパシティ使用量を設定
         *
         * @param currentInventoryCapacityUsage 現在のインベントリのキャパシティ使用量
         * @return this
         */
        public Inventory WithCurrentInventoryCapacityUsage(int? currentInventoryCapacityUsage) {
            this.currentInventoryCapacityUsage = currentInventoryCapacityUsage;
            return this;
        }

        /** 現在のインベントリの最大キャパシティ */
        public int? currentInventoryMaxCapacity { set; get; }

        /**
         * 現在のインベントリの最大キャパシティを設定
         *
         * @param currentInventoryMaxCapacity 現在のインベントリの最大キャパシティ
         * @return this
         */
        public Inventory WithCurrentInventoryMaxCapacity(int? currentInventoryMaxCapacity) {
            this.currentInventoryMaxCapacity = currentInventoryMaxCapacity;
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
        public Inventory WithCreatedAt(long? createdAt) {
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
        public Inventory WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.inventoryId != null)
            {
                writer.WritePropertyName("inventoryId");
                writer.Write(this.inventoryId);
            }
            if(this.inventoryName != null)
            {
                writer.WritePropertyName("inventoryName");
                writer.Write(this.inventoryName);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.currentInventoryCapacityUsage.HasValue)
            {
                writer.WritePropertyName("currentInventoryCapacityUsage");
                writer.Write(this.currentInventoryCapacityUsage.Value);
            }
            if(this.currentInventoryMaxCapacity.HasValue)
            {
                writer.WritePropertyName("currentInventoryMaxCapacity");
                writer.Write(this.currentInventoryMaxCapacity.Value);
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
        public static Inventory FromDict(JsonData data)
        {
            return new Inventory()
                .WithInventoryId(data.Keys.Contains("inventoryId") && data["inventoryId"] != null ? data["inventoryId"].ToString() : null)
                .WithInventoryName(data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithCurrentInventoryCapacityUsage(data.Keys.Contains("currentInventoryCapacityUsage") && data["currentInventoryCapacityUsage"] != null ? (int?)int.Parse(data["currentInventoryCapacityUsage"].ToString()) : null)
                .WithCurrentInventoryMaxCapacity(data.Keys.Contains("currentInventoryMaxCapacity") && data["currentInventoryMaxCapacity"] != null ? (int?)int.Parse(data["currentInventoryMaxCapacity"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}