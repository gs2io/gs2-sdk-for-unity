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

using Gs2.Gs2Stamina.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Stamina.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStaminaModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public int RecoverIntervalMinutes;
		[SerializeField]
		public int RecoverValue;
		[SerializeField]
		public int InitialCapacity;
		[SerializeField]
		public bool IsOverflow;
		[SerializeField]
		public int MaxCapacity;
		[SerializeField]
		public Gs2.Unity.Gs2Stamina.Model.EzMaxStaminaTable MaxStaminaTable;
		[SerializeField]
		public Gs2.Unity.Gs2Stamina.Model.EzRecoverIntervalTable RecoverIntervalTable;
		[SerializeField]
		public Gs2.Unity.Gs2Stamina.Model.EzRecoverValueTable RecoverValueTable;

        public Gs2.Gs2Stamina.Model.StaminaModel ToModel()
        {
            return new Gs2.Gs2Stamina.Model.StaminaModel {
                Name = Name,
                Metadata = Metadata,
                RecoverIntervalMinutes = RecoverIntervalMinutes,
                RecoverValue = RecoverValue,
                InitialCapacity = InitialCapacity,
                IsOverflow = IsOverflow,
                MaxCapacity = MaxCapacity,
                MaxStaminaTable = MaxStaminaTable?.ToModel(),
                RecoverIntervalTable = RecoverIntervalTable?.ToModel(),
                RecoverValueTable = RecoverValueTable?.ToModel(),
            };
        }

        public static EzStaminaModel FromModel(Gs2.Gs2Stamina.Model.StaminaModel model)
        {
            return new EzStaminaModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                RecoverIntervalMinutes = model.RecoverIntervalMinutes ?? 0,
                RecoverValue = model.RecoverValue ?? 0,
                InitialCapacity = model.InitialCapacity ?? 0,
                IsOverflow = model.IsOverflow ?? false,
                MaxCapacity = model.MaxCapacity ?? 0,
                MaxStaminaTable = model.MaxStaminaTable == null ? null : Gs2.Unity.Gs2Stamina.Model.EzMaxStaminaTable.FromModel(model.MaxStaminaTable),
                RecoverIntervalTable = model.RecoverIntervalTable == null ? null : Gs2.Unity.Gs2Stamina.Model.EzRecoverIntervalTable.FromModel(model.RecoverIntervalTable),
                RecoverValueTable = model.RecoverValueTable == null ? null : Gs2.Unity.Gs2Stamina.Model.EzRecoverValueTable.FromModel(model.RecoverValueTable),
            };
        }
    }
}