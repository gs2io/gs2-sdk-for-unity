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

using Gs2.Gs2Enchant.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enchant.Result
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzListRarityParameterModelsResult
	{
		[SerializeField]
		public List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> Items;

        public static EzListRarityParameterModelsResult FromModel(Gs2.Gs2Enchant.Result.DescribeRarityParameterModelsResult model)
        {
            return new EzListRarityParameterModelsResult {
                Items = model.Items == null ? new List<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>() : model.Items.Select(v => {
                    return Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel.FromModel(v);
                }).ToList(),
            };
        }
    }
}