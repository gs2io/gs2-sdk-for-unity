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
	public class AcquireItemSetByUserIdRequest : Gs2Request<AcquireItemSetByUserIdRequest>
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
        public AcquireItemSetByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public AcquireItemSetByUserIdRequest WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
            return this;
        }


        /** アイテムマスターの名前 */
		[UnityEngine.SerializeField]
        public string itemName;

        /**
         * アイテムマスターの名前を設定
         *
         * @param itemName アイテムマスターの名前
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithItemName(string itemName) {
            this.itemName = itemName;
            return this;
        }


        /** ユーザーID */
		[UnityEngine.SerializeField]
        public string userId;

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 入手する量 */
		[UnityEngine.SerializeField]
        public long? acquireCount;

        /**
         * 入手する量を設定
         *
         * @param acquireCount 入手する量
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithAcquireCount(long? acquireCount) {
            this.acquireCount = acquireCount;
            return this;
        }


        /** 有効期限 */
		[UnityEngine.SerializeField]
        public long? expiresAt;

        /**
         * 有効期限を設定
         *
         * @param expiresAt 有効期限
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithExpiresAt(long? expiresAt) {
            this.expiresAt = expiresAt;
            return this;
        }


        /** 既存の ItemSet に空きがあったとしても、新しい ItemSet を作成するか */
		[UnityEngine.SerializeField]
        public bool? createNewItemSet;

        /**
         * 既存の ItemSet に空きがあったとしても、新しい ItemSet を作成するかを設定
         *
         * @param createNewItemSet 既存の ItemSet に空きがあったとしても、新しい ItemSet を作成するか
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithCreateNewItemSet(bool? createNewItemSet) {
            this.createNewItemSet = createNewItemSet;
            return this;
        }


        /** 追加先のアイテムセットの名前 */
		[UnityEngine.SerializeField]
        public string itemSetName;

        /**
         * 追加先のアイテムセットの名前を設定
         *
         * @param itemSetName 追加先のアイテムセットの名前
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithItemSetName(string itemSetName) {
            this.itemSetName = itemSetName;
            return this;
        }


        /** 重複実行回避機能に使用するID */
		[UnityEngine.SerializeField]
        public string duplicationAvoider;

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static AcquireItemSetByUserIdRequest FromDict(JsonData data)
        {
            return new AcquireItemSetByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                inventoryName = data.Keys.Contains("inventoryName") && data["inventoryName"] != null ? data["inventoryName"].ToString(): null,
                itemName = data.Keys.Contains("itemName") && data["itemName"] != null ? data["itemName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                acquireCount = data.Keys.Contains("acquireCount") && data["acquireCount"] != null ? (long?)long.Parse(data["acquireCount"].ToString()) : null,
                expiresAt = data.Keys.Contains("expiresAt") && data["expiresAt"] != null ? (long?)long.Parse(data["expiresAt"].ToString()) : null,
                createNewItemSet = data.Keys.Contains("createNewItemSet") && data["createNewItemSet"] != null ? (bool?)bool.Parse(data["createNewItemSet"].ToString()) : null,
                itemSetName = data.Keys.Contains("itemSetName") && data["itemSetName"] != null ? data["itemSetName"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}