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
	public class EzItemSet
	{
		[SerializeField]
		public string ItemSetId;
		[SerializeField]
		public string Name;
		[SerializeField]
		public string InventoryName;
		[SerializeField]
		public string ItemName;
		[SerializeField]
		public long Count;
		[SerializeField]
		public int SortValue;
		[SerializeField]
		public long ExpiresAt;
		[SerializeField]
		public List<string> ReferenceOf;

        public Gs2.Gs2Inventory.Model.ItemSet ToModel()
        {
            return new Gs2.Gs2Inventory.Model.ItemSet {
                ItemSetId = ItemSetId,
                Name = Name,
                InventoryName = InventoryName,
                ItemName = ItemName,
                Count = Count,
                SortValue = SortValue,
                ExpiresAt = ExpiresAt,
                ReferenceOf = ReferenceOf?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzItemSet FromModel(Gs2.Gs2Inventory.Model.ItemSet model)
        {
            return new EzItemSet {
                ItemSetId = model.ItemSetId == null ? null : model.ItemSetId,
                Name = model.Name == null ? null : model.Name,
                InventoryName = model.InventoryName == null ? null : model.InventoryName,
                ItemName = model.ItemName == null ? null : model.ItemName,
                Count = model.Count ?? 0,
                SortValue = model.SortValue ?? 0,
                ExpiresAt = model.ExpiresAt ?? 0,
                ReferenceOf = model.ReferenceOf == null ? new List<string>() : model.ReferenceOf.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}