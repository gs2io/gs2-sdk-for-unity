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
using Gs2.Gs2Stamina.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Stamina.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStaminaModel
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RecoverIntervalMinutes;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RecoverValue;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int InitialCapacity;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool IsOverflow;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int MaxCapacity;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Stamina.Model.EzMaxStaminaTable MaxStaminaTable;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Stamina.Model.EzRecoverIntervalTable RecoverIntervalTable;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
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