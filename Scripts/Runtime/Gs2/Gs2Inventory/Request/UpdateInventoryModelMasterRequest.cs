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
	public class UpdateInventoryModelMasterRequest : Gs2Request<UpdateInventoryModelMasterRequest>
	{

        /** カテゴリー名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public UpdateInventoryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** インベントリの種類名 */
        public string inventoryName { set; get; }

        /**
         * インベントリの種類名を設定
         *
         * @param inventoryName インベントリの種類名
         * @return this
         */
        public UpdateInventoryModelMasterRequest WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
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
        public UpdateInventoryModelMasterRequest WithDescription(string description) {
            this.description = description;
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
        public UpdateInventoryModelMasterRequest WithMetadata(string metadata) {
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
        public UpdateInventoryModelMasterRequest WithInitialCapacity(int? initialCapacity) {
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
        public UpdateInventoryModelMasterRequest WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }


    	[Preserve]
        public static UpdateInventoryModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateInventoryModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                inventoryName = data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                initialCapacity = data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null,
                maxCapacity = data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null,
            };
        }

	}
}