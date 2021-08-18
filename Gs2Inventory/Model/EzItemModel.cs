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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Inventory.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzItemModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long StackingLimit;
		[SerializeField]
		public bool AllowMultipleStacks;
		[SerializeField]
		public int SortValue;

        public Gs2.Gs2Inventory.Model.ItemModel ToModel()
        {
            return new Gs2.Gs2Inventory.Model.ItemModel {
                Name = Name,
                Metadata = Metadata,
                StackingLimit = StackingLimit,
                AllowMultipleStacks = AllowMultipleStacks,
                SortValue = SortValue,
            };
        }

        public static EzItemModel FromModel(Gs2.Gs2Inventory.Model.ItemModel model)
        {
            return new EzItemModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                StackingLimit = model.StackingLimit ?? 0,
                AllowMultipleStacks = model.AllowMultipleStacks ?? false,
                SortValue = model.SortValue ?? 0,
            };
        }
    }
}