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
	public class EzItemModel
	{
		/** アイテムモデルの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** アイテムモデルの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** スタック可能な最大数量 */
		[UnityEngine.SerializeField]
		public long StackingLimit;
		/** スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか */
		[UnityEngine.SerializeField]
		public bool AllowMultipleStacks;
		/** 表示順番 */
		[UnityEngine.SerializeField]
		public int SortValue;

		public EzItemModel()
		{

		}

		public EzItemModel(Gs2.Gs2Inventory.Model.ItemModel @itemModel)
		{
			Name = @itemModel.name;
			Metadata = @itemModel.metadata;
			StackingLimit = @itemModel.stackingLimit.HasValue ? @itemModel.stackingLimit.Value : 0;
			AllowMultipleStacks = @itemModel.allowMultipleStacks.HasValue ? @itemModel.allowMultipleStacks.Value : false;
			SortValue = @itemModel.sortValue.HasValue ? @itemModel.sortValue.Value : 0;
		}

        public virtual ItemModel ToModel()
        {
            return new ItemModel {
                name = Name,
                metadata = Metadata,
                stackingLimit = StackingLimit,
                allowMultipleStacks = AllowMultipleStacks,
                sortValue = SortValue,
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
            writer.WritePropertyName("stackingLimit");
            writer.Write(this.StackingLimit);
            writer.WritePropertyName("allowMultipleStacks");
            writer.Write(this.AllowMultipleStacks);
            writer.WritePropertyName("sortValue");
            writer.Write(this.SortValue);
            writer.WriteObjectEnd();
        }
	}
}
