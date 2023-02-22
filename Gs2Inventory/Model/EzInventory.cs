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
	public class EzInventory
	{
		[SerializeField]
		public string InventoryId;
		[SerializeField]
		public string InventoryName;
		[SerializeField]
		public int CurrentInventoryCapacityUsage;
		[SerializeField]
		public int CurrentInventoryMaxCapacity;

        public Gs2.Gs2Inventory.Model.Inventory ToModel()
        {
            return new Gs2.Gs2Inventory.Model.Inventory {
                InventoryId = InventoryId,
                InventoryName = InventoryName,
                CurrentInventoryCapacityUsage = CurrentInventoryCapacityUsage,
                CurrentInventoryMaxCapacity = CurrentInventoryMaxCapacity,
            };
        }

        public static EzInventory FromModel(Gs2.Gs2Inventory.Model.Inventory model)
        {
            return new EzInventory {
                InventoryId = model.InventoryId == null ? null : model.InventoryId,
                InventoryName = model.InventoryName == null ? null : model.InventoryName,
                CurrentInventoryCapacityUsage = model.CurrentInventoryCapacityUsage ?? 0,
                CurrentInventoryMaxCapacity = model.CurrentInventoryMaxCapacity ?? 0,
            };
        }
    }
}