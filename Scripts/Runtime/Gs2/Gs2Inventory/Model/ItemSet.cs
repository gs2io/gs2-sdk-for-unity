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
	public class ItemSet
	{

        /** 有効期限ごとのアイテム所持数量 */
        public string itemSetId { set; get; }

        /**
         * 有効期限ごとのアイテム所持数量を設定
         *
         * @param itemSetId 有効期限ごとのアイテム所持数量
         * @return this
         */
        public ItemSet WithItemSetId(string itemSetId) {
            this.itemSetId = itemSetId;
            return this;
        }

        /** アイテムセットを識別する名前 */
        public string name { set; get; }

        /**
         * アイテムセットを識別する名前を設定
         *
         * @param name アイテムセットを識別する名前
         * @return this
         */
        public ItemSet WithName(string name) {
            this.name = name;
            return this;
        }

        /** インベントリの名前 */
        public string inventoryName { set; get; }

        /**
         * インベントリの名前を設定
         *
         * @param inventoryName インベントリの名前
         * @return this
         */
        public ItemSet WithInventoryName(string inventoryName) {
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
        public ItemSet WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** アイテムマスターの名前 */
        public string itemName { set; get; }

        /**
         * アイテムマスターの名前を設定
         *
         * @param itemName アイテムマスターの名前
         * @return this
         */
        public ItemSet WithItemName(string itemName) {
            this.itemName = itemName;
            return this;
        }

        /** 所持数量 */
        public long? count { set; get; }

        /**
         * 所持数量を設定
         *
         * @param count 所持数量
         * @return this
         */
        public ItemSet WithCount(long? count) {
            this.count = count;
            return this;
        }

        /** 表示順番 */
        public int? sortValue { set; get; }

        /**
         * 表示順番を設定
         *
         * @param sortValue 表示順番
         * @return this
         */
        public ItemSet WithSortValue(int? sortValue) {
            this.sortValue = sortValue;
            return this;
        }

        /** 有効期限 */
        public long? expiresAt { set; get; }

        /**
         * 有効期限を設定
         *
         * @param expiresAt 有効期限
         * @return this
         */
        public ItemSet WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
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
        public ItemSet WithCreatedAt(long? createdAt) {
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
        public ItemSet WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.itemSetId != null)
            {
                writer.WritePropertyName("itemSetId");
                writer.Write(this.itemSetId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
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
            if(this.itemName != null)
            {
                writer.WritePropertyName("itemName");
                writer.Write(this.itemName);
            }
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
            }
            if(this.sortValue.HasValue)
            {
                writer.WritePropertyName("sortValue");
                writer.Write(this.sortValue.Value);
            }
            if(this.expiresAt.HasValue)
            {
                writer.WritePropertyName("expiresAt");
                writer.Write(this.expiresAt.Value);
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
        public static ItemSet FromDict(JsonData data)
        {
            return new ItemSet()
                .WithItemSetId(data.Keys.Contains("itemSetId") && data["itemSetId"] != null ? data["itemSetId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithInventoryName(data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithItemName(data.Keys.Contains("itemName") && data["itemName"] != null ? data["itemName"].ToString() : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (long?)long.Parse(data["count"].ToString()) : null)
                .WithSortValue(data.Keys.Contains("sortValue") && data["sortValue"] != null ? (int?)int.Parse(data["sortValue"].ToString()) : null)
                .WithExpiresAt(data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}