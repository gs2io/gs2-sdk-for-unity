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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Inventory.Model;

namespace Gs2.Gs2Inventory.Request
{
	public class AcquireItemSetByUserIdRequest : Gs2Request<AcquireItemSetByUserIdRequest>
	{

        /** カテゴリー名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public AcquireItemSetByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public AcquireItemSetByUserIdRequest WithInventoryName(string inventoryName) {
            this.inventoryName = inventoryName;
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
        public AcquireItemSetByUserIdRequest WithItemName(string itemName) {
            this.itemName = itemName;
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
        public AcquireItemSetByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 入手する量 */
        public long? acquireCount { set; get; }

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
        public long? expiresAt { set; get; }

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


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

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


	}
}