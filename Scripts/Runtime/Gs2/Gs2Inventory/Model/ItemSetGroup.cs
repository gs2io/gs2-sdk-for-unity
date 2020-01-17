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
	public class ItemSetGroup
	{

        /** 有効期限ごとのアイテム所持数量 */
        public string itemSetGroupId { set; get; }

        /**
         * 有効期限ごとのアイテム所持数量を設定
         *
         * @param itemSetGroupId 有効期限ごとのアイテム所持数量
         * @return this
         */
        public ItemSetGroup WithItemSetGroupId(string itemSetGroupId) {
            this.itemSetGroupId = itemSetGroupId;
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
        public ItemSetGroup WithInventoryName(string inventoryName) {
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
        public ItemSetGroup WithUserId(string userId) {
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
        public ItemSetGroup WithItemName(string itemName) {
            this.itemName = itemName;
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
        public ItemSetGroup WithSortValue(int? sortValue) {
            this.sortValue = sortValue;
            return this;
        }

        /** アイテムセットIDのリスト */
        public List<string> itemSetItemSetIdList { set; get; }

        /**
         * アイテムセットIDのリストを設定
         *
         * @param itemSetItemSetIdList アイテムセットIDのリスト
         * @return this
         */
        public ItemSetGroup WithItemSetItemSetIdList(List<string> itemSetItemSetIdList) {
            this.itemSetItemSetIdList = itemSetItemSetIdList;
            return this;
        }

        /** アイテムセットを識別する名前のリスト */
        public List<string> itemSetNameList { set; get; }

        /**
         * アイテムセットを識別する名前のリストを設定
         *
         * @param itemSetNameList アイテムセットを識別する名前のリスト
         * @return this
         */
        public ItemSetGroup WithItemSetNameList(List<string> itemSetNameList) {
            this.itemSetNameList = itemSetNameList;
            return this;
        }

        /** 所持数量のリスト */
        public List<long?> itemSetCountList { set; get; }

        /**
         * 所持数量のリストを設定
         *
         * @param itemSetCountList 所持数量のリスト
         * @return this
         */
        public ItemSetGroup WithItemSetCountList(List<long?> itemSetCountList) {
            this.itemSetCountList = itemSetCountList;
            return this;
        }

        /** 有効期限のリスト */
        public List<long?> itemSetExpiresAtList { set; get; }

        /**
         * 有効期限のリストを設定
         *
         * @param itemSetExpiresAtList 有効期限のリスト
         * @return this
         */
        public ItemSetGroup WithItemSetExpiresAtList(List<long?> itemSetExpiresAtList) {
            this.itemSetExpiresAtList = itemSetExpiresAtList;
            return this;
        }

        /** 作成日時のリスト */
        public List<long?> itemSetCreatedAtList { set; get; }

        /**
         * 作成日時のリストを設定
         *
         * @param itemSetCreatedAtList 作成日時のリスト
         * @return this
         */
        public ItemSetGroup WithItemSetCreatedAtList(List<long?> itemSetCreatedAtList) {
            this.itemSetCreatedAtList = itemSetCreatedAtList;
            return this;
        }

        /** 更新日時のリスト */
        public List<long?> itemSetUpdatedAtList { set; get; }

        /**
         * 更新日時のリストを設定
         *
         * @param itemSetUpdatedAtList 更新日時のリスト
         * @return this
         */
        public ItemSetGroup WithItemSetUpdatedAtList(List<long?> itemSetUpdatedAtList) {
            this.itemSetUpdatedAtList = itemSetUpdatedAtList;
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
        public ItemSetGroup WithCreatedAt(long? createdAt) {
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
        public ItemSetGroup WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.itemSetGroupId != null)
            {
                writer.WritePropertyName("itemSetGroupId");
                writer.Write(this.itemSetGroupId);
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
            if(this.sortValue.HasValue)
            {
                writer.WritePropertyName("sortValue");
                writer.Write(this.sortValue.Value);
            }
            if(this.itemSetItemSetIdList != null)
            {
                writer.WritePropertyName("itemSetItemSetIdList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetItemSetIdList)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.itemSetNameList != null)
            {
                writer.WritePropertyName("itemSetNameList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetNameList)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.itemSetCountList != null)
            {
                writer.WritePropertyName("itemSetCountList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetCountList)
                {
                    writer.Write(item.Value);
                }
                writer.WriteArrayEnd();
            }
            if(this.itemSetExpiresAtList != null)
            {
                writer.WritePropertyName("itemSetExpiresAtList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetExpiresAtList)
                {
                    writer.Write(item.Value);
                }
                writer.WriteArrayEnd();
            }
            if(this.itemSetCreatedAtList != null)
            {
                writer.WritePropertyName("itemSetCreatedAtList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetCreatedAtList)
                {
                    writer.Write(item.Value);
                }
                writer.WriteArrayEnd();
            }
            if(this.itemSetUpdatedAtList != null)
            {
                writer.WritePropertyName("itemSetUpdatedAtList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetUpdatedAtList)
                {
                    writer.Write(item.Value);
                }
                writer.WriteArrayEnd();
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
        public static ItemSetGroup FromDict(JsonData data)
        {
            return new ItemSetGroup()
                .WithItemSetGroupId(data.Keys.Contains("itemSetGroupId") && data["itemSetGroupId"] != null ? data["itemSetGroupId"].ToString() : null)
                .WithInventoryName(data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithItemName(data.Keys.Contains("itemName") && data["itemName"] != null ? data["itemName"].ToString() : null)
                .WithSortValue(data.Keys.Contains("sortValue") && data["sortValue"] != null ? (int?)int.Parse(data["sortValue"].ToString()) : null)
                .WithItemSetItemSetIdList(data.Keys.Contains("itemSetItemSetIdList") && data["itemSetItemSetIdList"] != null ? data["itemSetItemSetIdList"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithItemSetNameList(data.Keys.Contains("itemSetNameList") && data["itemSetNameList"] != null ? data["itemSetNameList"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithItemSetCountList(data.Keys.Contains("itemSetCountList") && data["itemSetCountList"] != null ? data["itemSetCountList"].Cast<JsonData>().Select(value =>
                    {
                        return (long?)long.Parse(value.ToString());
                    }
                ).ToList() : null)
                .WithItemSetExpiresAtList(data.Keys.Contains("itemSetExpiresAtList") && data["itemSetExpiresAtList"] != null ? data["itemSetExpiresAtList"].Cast<JsonData>().Select(value =>
                    {
                        return (long?)long.Parse(value.ToString());
                    }
                ).ToList() : null)
                .WithItemSetCreatedAtList(data.Keys.Contains("itemSetCreatedAtList") && data["itemSetCreatedAtList"] != null ? data["itemSetCreatedAtList"].Cast<JsonData>().Select(value =>
                    {
                        return (long?)long.Parse(value.ToString());
                    }
                ).ToList() : null)
                .WithItemSetUpdatedAtList(data.Keys.Contains("itemSetUpdatedAtList") && data["itemSetUpdatedAtList"] != null ? data["itemSetUpdatedAtList"].Cast<JsonData>().Select(value =>
                    {
                        return (long?)long.Parse(value.ToString());
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}