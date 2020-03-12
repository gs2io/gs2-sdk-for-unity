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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Inventory.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inventory.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateItemModelMasterRequest : Gs2Request<UpdateItemModelMasterRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateItemModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** インベントリの種類名 */
		[UnityEngine.SerializeField]
        public string inventoryName;

        /**
         * インベントリの種類名を設定
         *
         * @param inventoryName インベントリの種類名
         * @return this
         */
        public UpdateItemModelMasterRequest WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
            return this;
        }


        /** アイテムモデルの種類名 */
		[UnityEngine.SerializeField]
        public string itemName;

        /**
         * アイテムモデルの種類名を設定
         *
         * @param itemName アイテムモデルの種類名
         * @return this
         */
        public UpdateItemModelMasterRequest WithItemName(string itemName) {
            this.itemName = itemName;
            return this;
        }


        /** アイテムモデルマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * アイテムモデルマスターの説明を設定
         *
         * @param description アイテムモデルマスターの説明
         * @return this
         */
        public UpdateItemModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** アイテムモデルの種類のメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * アイテムモデルの種類のメタデータを設定
         *
         * @param metadata アイテムモデルの種類のメタデータ
         * @return this
         */
        public UpdateItemModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** スタック可能な最大数量 */
		[UnityEngine.SerializeField]
        public long? stackingLimit;

        /**
         * スタック可能な最大数量を設定
         *
         * @param stackingLimit スタック可能な最大数量
         * @return this
         */
        public UpdateItemModelMasterRequest WithStackingLimit(long? stackingLimit) {
            this.stackingLimit = stackingLimit;
            return this;
        }


        /** スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか */
		[UnityEngine.SerializeField]
        public bool? allowMultipleStacks;

        /**
         * スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すかを設定
         *
         * @param allowMultipleStacks スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか
         * @return this
         */
        public UpdateItemModelMasterRequest WithAllowMultipleStacks(bool? allowMultipleStacks) {
            this.allowMultipleStacks = allowMultipleStacks;
            return this;
        }


        /** 表示順番 */
		[UnityEngine.SerializeField]
        public int? sortValue;

        /**
         * 表示順番を設定
         *
         * @param sortValue 表示順番
         * @return this
         */
        public UpdateItemModelMasterRequest WithSortValue(int? sortValue) {
            this.sortValue = sortValue;
            return this;
        }


    	[Preserve]
        public static UpdateItemModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateItemModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                inventoryName = data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString(): null,
                itemName = data.Keys.Contains("itemName") && data["itemName"] != null ? data["itemName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                stackingLimit = data.Keys.Contains("stackingLimit") && data["stackingLimit"] != null ? (long?)long.Parse(data["stackingLimit"].ToString()) : null,
                allowMultipleStacks = data.Keys.Contains("allowMultipleStacks") && data["allowMultipleStacks"] != null ? (bool?)bool.Parse(data["allowMultipleStacks"].ToString()) : null,
                sortValue = data.Keys.Contains("sortValue") && data["sortValue"] != null ? (int?)int.Parse(data["sortValue"].ToString()) : null,
            };
        }

	}
}