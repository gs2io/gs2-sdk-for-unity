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
	public class EzInventoryModel
	{
		/** インベントリの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** インベントリの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** インベントリの初期サイズ */
		[UnityEngine.SerializeField]
		public int InitialCapacity;
		/** インベントリの最大サイズ */
		[UnityEngine.SerializeField]
		public int MaxCapacity;

		public EzInventoryModel()
		{

		}

		public EzInventoryModel(Gs2.Gs2Inventory.Model.InventoryModel @inventoryModel)
		{
			Name = @inventoryModel.name;
			Metadata = @inventoryModel.metadata;
			InitialCapacity = @inventoryModel.initialCapacity.HasValue ? @inventoryModel.initialCapacity.Value : 0;
			MaxCapacity = @inventoryModel.maxCapacity.HasValue ? @inventoryModel.maxCapacity.Value : 0;
		}

        public virtual InventoryModel ToModel()
        {
            return new InventoryModel {
                name = Name,
                metadata = Metadata,
                initialCapacity = InitialCapacity,
                maxCapacity = MaxCapacity,
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
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WritePropertyName("initialCapacity");
            writer.Write(this.InitialCapacity);
            writer.WritePropertyName("maxCapacity");
            writer.Write(this.MaxCapacity);
            writer.WriteObjectEnd();
        }
	}
}
