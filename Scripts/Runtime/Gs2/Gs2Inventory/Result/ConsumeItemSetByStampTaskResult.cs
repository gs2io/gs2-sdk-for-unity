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
	public class ConsumeItemSetByStampTaskResult
	{
        /** 消費後の有効期限ごとのアイテム所持数量のリスト */
        public List<ItemSet> items { set; get; }

        /** アイテムモデル */
        public ItemModel itemModel { set; get; }

        /** インベントリ */
        public Inventory inventory { set; get; }

        /** スタンプタスクの実行結果を記録したコンテキスト */
        public string newContextStack { set; get; }


    	[Preserve]
        public static ConsumeItemSetByStampTaskResult FromDict(JsonData data)
        {
            return new ConsumeItemSetByStampTaskResult {
                items = data.Keys.Contains("items") && data["items"] != null ? data["items"].Cast<JsonData>().Select(value =>
                    {
                        return ItemSet.FromDict(value);
                    }
                ).ToList() : null,
                itemModel = data.Keys.Contains("itemModel") && data["itemModel"] != null ? ItemModel.FromDict(data["itemModel"]) : null,
                inventory = data.Keys.Contains("inventory") && data["inventory"] != null ? Inventory.FromDict(data["inventory"]) : null,
                newContextStack = data.Keys.Contains("newContextStack") && data["newContextStack"] != null ? (string) data["newContextStack"] : null,
            };
        }
	}
}