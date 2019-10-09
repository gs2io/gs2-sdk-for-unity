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
	public class CreateItemModelMasterRequest : Gs2Request<CreateItemModelMasterRequest>
	{

        /** カテゴリー名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public CreateItemModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** アイテムの種類名 */
        public string inventoryName { set; get; }

        /**
         * アイテムの種類名を設定
         *
         * @param inventoryName アイテムの種類名
         * @return this
         */
        public CreateItemModelMasterRequest WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
            return this;
        }


        /** アイテムモデルの種類名 */
        public string name { set; get; }

        /**
         * アイテムモデルの種類名を設定
         *
         * @param name アイテムモデルの種類名
         * @return this
         */
        public CreateItemModelMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** アイテムモデルマスターの説明 */
        public string description { set; get; }

        /**
         * アイテムモデルマスターの説明を設定
         *
         * @param description アイテムモデルマスターの説明
         * @return this
         */
        public CreateItemModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** アイテムモデルの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * アイテムモデルの種類のメタデータを設定
         *
         * @param metadata アイテムモデルの種類のメタデータ
         * @return this
         */
        public CreateItemModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** スタック可能な最大数量 */
        public long? stackingLimit { set; get; }

        /**
         * スタック可能な最大数量を設定
         *
         * @param stackingLimit スタック可能な最大数量
         * @return this
         */
        public CreateItemModelMasterRequest WithStackingLimit(long? stackingLimit) {
            this.stackingLimit = stackingLimit;
            return this;
        }


        /** スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか */
        public bool? allowMultipleStacks { set; get; }

        /**
         * スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すかを設定
         *
         * @param allowMultipleStacks スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか
         * @return this
         */
        public CreateItemModelMasterRequest WithAllowMultipleStacks(bool? allowMultipleStacks) {
            this.allowMultipleStacks = allowMultipleStacks;
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
        public CreateItemModelMasterRequest WithSortValue(int? sortValue) {
            this.sortValue = sortValue;
            return this;
        }


    	[Preserve]
        public static CreateItemModelMasterRequest FromDict(JsonData data)
        {
            return new CreateItemModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                inventoryName = data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                stackingLimit = data.Keys.Contains("stackingLimit") && data["stackingLimit"] != null ? (long?)long.Parse(data["stackingLimit"].ToString()) : null,
                allowMultipleStacks = data.Keys.Contains("allowMultipleStacks") && data["allowMultipleStacks"] != null ? (bool?)bool.Parse(data["allowMultipleStacks"].ToString()) : null,
                sortValue = data.Keys.Contains("sortValue") && data["sortValue"] != null ? (int?)int.Parse(data["sortValue"].ToString()) : null,
            };
        }

	}
}