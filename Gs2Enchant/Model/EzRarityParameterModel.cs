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
	public class EzRarityParameterModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public int MaximumParameterCount;
		[SerializeField]
		public List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterCountModel> ParameterCounts;
		[SerializeField]
		public List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterValueModel> Parameters;

        public Gs2.Gs2Enchant.Model.RarityParameterModel ToModel()
        {
            return new Gs2.Gs2Enchant.Model.RarityParameterModel {
                Name = Name,
                Metadata = Metadata,
                MaximumParameterCount = MaximumParameterCount,
                ParameterCounts = ParameterCounts?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                Parameters = Parameters?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzRarityParameterModel FromModel(Gs2.Gs2Enchant.Model.RarityParameterModel model)
        {
            return new EzRarityParameterModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                MaximumParameterCount = model.MaximumParameterCount ?? 0,
                ParameterCounts = model.ParameterCounts == null ? new List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterCountModel>() : model.ParameterCounts.Select(v => {
                    return Gs2.Unity.Gs2Enchant.Model.EzRarityParameterCountModel.FromModel(v);
                }).ToList(),
                Parameters = model.Parameters == null ? new List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterValueModel>() : model.Parameters.Select(v => {
                    return Gs2.Unity.Gs2Enchant.Model.EzRarityParameterValueModel.FromModel(v);
                }).ToList(),
            };
        }
    }
}