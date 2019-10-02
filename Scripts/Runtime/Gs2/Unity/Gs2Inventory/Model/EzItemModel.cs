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
	public class EzItemModel
	{
		/** アイテムモデルの種類名 */
		public string Name { get; set; }
		/** アイテムモデルの種類のメタデータ */
		public string Metadata { get; set; }
		/** スタック可能な最大数量 */
		public long StackingLimit { get; set; }
		/** スタック可能な最大数量を超えた時複数枠にアイテムを保管することを許すか */
		public bool AllowMultipleStacks { get; set; }
		/** 表示順番 */
		public int SortValue { get; set; }

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

        public ItemModel ToModel()
        {
            return new ItemModel {
                name = Name,
                metadata = Metadata,
                stackingLimit = StackingLimit,
                allowMultipleStacks = AllowMultipleStacks,
                sortValue = SortValue,
            };
        }
	}
}
