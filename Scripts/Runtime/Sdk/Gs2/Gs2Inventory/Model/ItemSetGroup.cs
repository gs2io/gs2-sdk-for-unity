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
 *
 * deny overwrite
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inventory.Model
{
	[Preserve]
	public class ItemSetGroup : IComparable
	{

        /** 有効期限ごとのアイテム所持数量 (このモデルは SDK では使用されません) */
        public string itemSetGroupId { set; get; }

        /**
         * 有効期限ごとのアイテム所持数量 (このモデルは SDK では使用されません)を設定
         *
         * @param itemSetGroupId 有効期限ごとのアイテム所持数量 (このモデルは SDK では使用されません)
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

        /** 参照元のリストのリスト */
        public List<List<string>> itemSetReferenceOfList { set; get; }

        /**
         * 参照元のリストのリストを設定
         *
         * @param itemSetReferenceOfList 参照元のリストのリスト
         * @return this
         */
        public ItemSetGroup WithItemSetReferenceOfList(List<List<string>> itemSetReferenceOfList) {
            this.itemSetReferenceOfList = itemSetReferenceOfList;
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
            if(this.itemSetReferenceOfList != null)
            {
                writer.WritePropertyName("itemSetReferenceOfList");
                writer.WriteArrayStart();
                foreach(var item in this.itemSetReferenceOfList)
                {
                    writer.WriteArrayStart();
                    foreach(var item2 in item)
                    {
                        writer.Write(item2);
                    }
                    writer.WriteArrayEnd();
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

    public static string GetItemNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["itemName"].Success)
        {
            return null;
        }
        return match.Groups["itemName"].Value;
    }

    public static string GetInventoryNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["inventoryName"].Success)
        {
            return null;
        }
        return match.Groups["inventoryName"].Value;
    }

    public static string GetUserIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["userId"].Success)
        {
            return null;
        }
        return match.Groups["userId"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["namespaceName"].Success)
        {
            return null;
        }
        return match.Groups["namespaceName"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["ownerId"].Success)
        {
            return null;
        }
        return match.Groups["ownerId"].Value;
    }

    public static string GetRegionFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):inventory:(?<namespaceName>.*):user:(?<userId>.*):inventory:(?<inventoryName>.*):item:(?<itemName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
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
                .WithItemSetReferenceOfList(data.Keys.Contains("itemSetReferenceOfList") && data["itemSetReferenceOfList"] != null ? data["itemSetReferenceOfList"].Cast<List<JsonData>>().Select(value =>
                    {
                        return value.Select(value2 =>
                            {
                                return value2.ToString();
                            }
                        ).ToList();
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

        public int CompareTo(object obj)
        {
            var other = obj as ItemSetGroup;
            var diff = 0;
            if (itemSetGroupId == null && itemSetGroupId == other.itemSetGroupId)
            {
                // null and null
            }
            else
            {
                diff += itemSetGroupId.CompareTo(other.itemSetGroupId);
            }
            if (inventoryName == null && inventoryName == other.inventoryName)
            {
                // null and null
            }
            else
            {
                diff += inventoryName.CompareTo(other.inventoryName);
            }
            if (userId == null && userId == other.userId)
            {
                // null and null
            }
            else
            {
                diff += userId.CompareTo(other.userId);
            }
            if (itemName == null && itemName == other.itemName)
            {
                // null and null
            }
            else
            {
                diff += itemName.CompareTo(other.itemName);
            }
            if (sortValue == null && sortValue == other.sortValue)
            {
                // null and null
            }
            else
            {
                diff += (int)(sortValue - other.sortValue);
            }
            if (itemSetItemSetIdList == null && itemSetItemSetIdList == other.itemSetItemSetIdList)
            {
                // null and null
            }
            else
            {
                diff += itemSetItemSetIdList.Count - other.itemSetItemSetIdList.Count;
                for (var i = 0; i < itemSetItemSetIdList.Count; i++)
                {
                    diff += itemSetItemSetIdList[i].CompareTo(other.itemSetItemSetIdList[i]);
                }
            }
            if (itemSetNameList == null && itemSetNameList == other.itemSetNameList)
            {
                // null and null
            }
            else
            {
                diff += itemSetNameList.Count - other.itemSetNameList.Count;
                for (var i = 0; i < itemSetNameList.Count; i++)
                {
                    diff += itemSetNameList[i].CompareTo(other.itemSetNameList[i]);
                }
            }
            if (itemSetCountList == null && itemSetCountList == other.itemSetCountList)
            {
                // null and null
            }
            else
            {
                diff += itemSetCountList.Count - other.itemSetCountList.Count;
                for (var i = 0; i < itemSetCountList.Count; i++)
                {
                    diff += (int)(itemSetCountList[i] - other.itemSetCountList[i]);
                }
            }
            if (itemSetReferenceOfList == null && itemSetReferenceOfList == other.itemSetReferenceOfList)
            {
                // null and null
            }
            else
            {
                diff += itemSetReferenceOfList.Count - other.itemSetReferenceOfList.Count;
                for (var i = 0; i < itemSetReferenceOfList.Count; i++)
                {
                    // diff += itemSetReferenceOfList[i].CompareTo(other.itemSetReferenceOfList[i]);
                }
            }
            if (itemSetExpiresAtList == null && itemSetExpiresAtList == other.itemSetExpiresAtList)
            {
                // null and null
            }
            else
            {
                diff += itemSetExpiresAtList.Count - other.itemSetExpiresAtList.Count;
                for (var i = 0; i < itemSetExpiresAtList.Count; i++)
                {
                    diff += (int)(itemSetExpiresAtList[i] - other.itemSetExpiresAtList[i]);
                }
            }
            if (itemSetCreatedAtList == null && itemSetCreatedAtList == other.itemSetCreatedAtList)
            {
                // null and null
            }
            else
            {
                diff += itemSetCreatedAtList.Count - other.itemSetCreatedAtList.Count;
                for (var i = 0; i < itemSetCreatedAtList.Count; i++)
                {
                    diff += (int)(itemSetCreatedAtList[i] - other.itemSetCreatedAtList[i]);
                }
            }
            if (itemSetUpdatedAtList == null && itemSetUpdatedAtList == other.itemSetUpdatedAtList)
            {
                // null and null
            }
            else
            {
                diff += itemSetUpdatedAtList.Count - other.itemSetUpdatedAtList.Count;
                for (var i = 0; i < itemSetUpdatedAtList.Count; i++)
                {
                    diff += (int)(itemSetUpdatedAtList[i] - other.itemSetUpdatedAtList[i]);
                }
            }
            if (createdAt == null && createdAt == other.createdAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(createdAt - other.createdAt);
            }
            if (updatedAt == null && updatedAt == other.updatedAt)
            {
                // null and null
            }
            else
            {
                diff += (int)(updatedAt - other.updatedAt);
            }
            return diff;
        }
	}
}