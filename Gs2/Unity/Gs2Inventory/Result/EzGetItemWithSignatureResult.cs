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
using Gs2.Core.Model;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Gs2Inventory.Result;

namespace Gs2.Unity.Gs2Inventory.Result
{
	public class EzGetItemWithSignatureResult
	{
        /** 有効期限毎の{model_name} */
        public List<EzItemSet> Items { get; private set; }

        /** アイテムモデル */
        public EzItemModel ItemModel { get; private set; }

        /** インベントリ */
        public EzInventory Inventory { get; private set; }

        /** 署名対象のアイテムセット情報 */
        public string Body { get; private set; }

        /** 署名 */
        public string Signature { get; private set; }


        public EzGetItemWithSignatureResult(
            GetItemWithSignatureResult result
        )
        {
            Items = new List<EzItemSet>();
            foreach (var item_ in result.items)
            {
                Items.Add(new EzItemSet(item_));
            }
            if(result.itemModel != null)
            {
                ItemModel = new EzItemModel(result.itemModel);
            }
            if(result.inventory != null)
            {
                Inventory = new EzInventory(result.inventory);
            }
            Body = result.body;
            Signature = result.signature;
        }
	}
}