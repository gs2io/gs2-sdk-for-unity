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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Inventory.Model
{
	[Preserve]
	[System.Serializable]
	public class EzInventory
	{
		/** インベントリ */
		[UnityEngine.SerializeField]
		public string InventoryId;
		/** インベントリモデル名 */
		[UnityEngine.SerializeField]
		public string InventoryName;
		/** 現在のインベントリのキャパシティ使用量 */
		[UnityEngine.SerializeField]
		public int CurrentInventoryCapacityUsage;
		/** 現在のインベントリの最大キャパシティ */
		[UnityEngine.SerializeField]
		public int CurrentInventoryMaxCapacity;

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

        public virtual Inventory ToModel()
        {
            return new Inventory {
                inventoryId = InventoryId,
                inventoryName = InventoryName,
                currentInventoryCapacityUsage = CurrentInventoryCapacityUsage,
                currentInventoryMaxCapacity = CurrentInventoryMaxCapacity,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.InventoryId != null)
            {
                writer.WritePropertyName("inventoryId");
                writer.Write(this.InventoryId);
            }
            if(this.InventoryName != null)
            {
                writer.WritePropertyName("inventoryName");
                writer.Write(this.InventoryName);
            }
            writer.WritePropertyName("currentInventoryCapacityUsage");
            writer.Write(this.CurrentInventoryCapacityUsage);
            writer.WritePropertyName("currentInventoryMaxCapacity");
            writer.Write(this.CurrentInventoryMaxCapacity);
            writer.WriteObjectEnd();
        }
	}
}
