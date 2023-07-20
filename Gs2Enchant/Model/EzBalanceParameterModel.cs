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
using Gs2.Gs2Enchant.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enchant.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBalanceParameterModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public long TotalValue;
		[SerializeField]
		public string InitialValueStrategy;
		[SerializeField]
		public List<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterValueModel> Parameters;

        public Gs2.Gs2Enchant.Model.BalanceParameterModel ToModel()
        {
            return new Gs2.Gs2Enchant.Model.BalanceParameterModel {
                Name = Name,
                Metadata = Metadata,
                TotalValue = TotalValue,
                InitialValueStrategy = InitialValueStrategy,
                Parameters = Parameters?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzBalanceParameterModel FromModel(Gs2.Gs2Enchant.Model.BalanceParameterModel model)
        {
            return new EzBalanceParameterModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                TotalValue = model.TotalValue ?? 0,
                InitialValueStrategy = model.InitialValueStrategy == null ? null : model.InitialValueStrategy,
                Parameters = model.Parameters == null ? new List<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterValueModel>() : model.Parameters.Select(v => {
                    return Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterValueModel.FromModel(v);
                }).ToList(),
            };
        }
    }
}