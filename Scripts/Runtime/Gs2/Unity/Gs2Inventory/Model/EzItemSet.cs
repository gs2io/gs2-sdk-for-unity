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
using Gs2.Gs2Inventory.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Inventory.Model
{
	[Preserve]
	public class EzItemSet
	{
		/** アイテムセットを識別する名前 */
		public string Name { get; set; }
		/** インベントリの名前 */
		public string InventoryName { get; set; }
		/** アイテムマスターの名前 */
		public string ItemName { get; set; }
		/** 所持数量 */
		public long Count { get; set; }
		/** 有効期限 */
		public long ExpiresAt { get; set; }

		public EzItemSet()
		{

		}

		public EzItemSet(Gs2.Gs2Inventory.Model.ItemSet @itemSet)
		{
			Name = @itemSet.name;
			InventoryName = @itemSet.inventoryName;
			ItemName = @itemSet.itemName;
			Count = @itemSet.count.HasValue ? @itemSet.count.Value : 0;
			ExpiresAt = @itemSet.expiresAt.HasValue ? @itemSet.expiresAt.Value : 0;
		}

        public ItemSet ToModel()
        {
            return new ItemSet {
                name = Name,
                inventoryName = InventoryName,
                itemName = ItemName,
                count = Count,
                expiresAt = ExpiresAt,
            };
        }
	}
}
