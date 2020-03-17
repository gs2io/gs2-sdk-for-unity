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
	public class EzItemSet
	{
		/** アイテムセットを識別する名前 */
		[UnityEngine.SerializeField]
		public string Name;
		/** インベントリの名前 */
		[UnityEngine.SerializeField]
		public string InventoryName;
		/** アイテムマスターの名前 */
		[UnityEngine.SerializeField]
		public string ItemName;
		/** 所持数量 */
		[UnityEngine.SerializeField]
		public long Count;
		/** 表示順番 */
		[UnityEngine.SerializeField]
		public int SortValue;
		/** 有効期限 */
		[UnityEngine.SerializeField]
		public long ExpiresAt;

		public EzItemSet()
		{

		}

		public EzItemSet(Gs2.Gs2Inventory.Model.ItemSet @itemSet)
		{
			Name = @itemSet.name;
			InventoryName = @itemSet.inventoryName;
			ItemName = @itemSet.itemName;
			Count = @itemSet.count.HasValue ? @itemSet.count.Value : 0;
			SortValue = @itemSet.sortValue.HasValue ? @itemSet.sortValue.Value : 0;
			ExpiresAt = @itemSet.expiresAt.HasValue ? @itemSet.expiresAt.Value : 0;
		}

        public virtual ItemSet ToModel()
        {
            return new ItemSet {
                name = Name,
                inventoryName = InventoryName,
                itemName = ItemName,
                count = Count,
                sortValue = SortValue,
                expiresAt = ExpiresAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.InventoryName != null)
            {
                writer.WritePropertyName("inventoryName");
                writer.Write(this.InventoryName);
            }
            if(this.ItemName != null)
            {
                writer.WritePropertyName("itemName");
                writer.Write(this.ItemName);
            }
            writer.WritePropertyName("count");
            writer.Write(this.Count);
            writer.WritePropertyName("sortValue");
            writer.Write(this.SortValue);
            writer.WritePropertyName("expiresAt");
            writer.Write(this.ExpiresAt);
            writer.WriteObjectEnd();
        }
	}
}
