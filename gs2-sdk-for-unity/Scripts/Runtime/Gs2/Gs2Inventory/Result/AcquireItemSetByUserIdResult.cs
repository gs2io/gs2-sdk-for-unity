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
using Gs2.Gs2Inventory.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inventory.Result
{
	[Preserve]
	public class AcquireItemSetByUserIdResult
	{
        /** 加算後の有効期限ごとのアイテム所持数量のリスト */
        public List<ItemSet> items { set; get; }

        /** アイテムモデル */
        public ItemModel itemModel { set; get; }

        /** インベントリ */
        public Inventory inventory { set; get; }

        /** 所持数量の上限を超えて受け取れずに GS2-Inbox に転送したアイテムの数量 */
        public long? overflowCount { set; get; }


    	[Preserve]
        public static AcquireItemSetByUserIdResult FromDict(JsonData data)
        {
            return new AcquireItemSetByUserIdResult {
                items = data.Keys.Contains("items") && data["items"] != null ? data["items"].Cast<JsonData>().Select(value =>
                    {
                        return ItemSet.FromDict(value);
                    }
                ).ToList() : null,
                itemModel = data.Keys.Contains("itemModel") && data["itemModel"] != null ? ItemModel.FromDict(data["itemModel"]) : null,
                inventory = data.Keys.Contains("inventory") && data["inventory"] != null ? Inventory.FromDict(data["inventory"]) : null,
                overflowCount = data.Keys.Contains("overflowCount") && data["overflowCount"] != null ? (long?)long.Parse(data["overflowCount"].ToString()) : null,
            };
        }
	}
}