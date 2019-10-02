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
	public class EzInventory
	{
		/** インベントリ */
		public string InventoryId { get; set; }
		/** インベントリモデル名 */
		public string InventoryName { get; set; }
		/** 現在のインベントリのキャパシティ使用量 */
		public int CurrentInventoryCapacityUsage { get; set; }
		/** 現在のインベントリの最大キャパシティ */
		public int CurrentInventoryMaxCapacity { get; set; }

		public EzInventory()
		{

		}

		public EzInventory(Gs2.Gs2Inventory.Model.Inventory @inventory)
		{
			InventoryId = @inventory.inventoryId;
			InventoryName = @inventory.inventoryName;
			CurrentInventoryCapacityUsage = @inventory.currentInventoryCapacityUsage.HasValue ? @inventory.currentInventoryCapacityUsage.Value : 0;
			CurrentInventoryMaxCapacity = @inventory.currentInventoryMaxCapacity.HasValue ? @inventory.currentInventoryMaxCapacity.Value : 0;
		}

        public Inventory ToModel()
        {
            return new Inventory {
                inventoryId = InventoryId,
                inventoryName = InventoryName,
                currentInventoryCapacityUsage = CurrentInventoryCapacityUsage,
                currentInventoryMaxCapacity = CurrentInventoryMaxCapacity,
            };
        }
	}
}
