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
using Gs2.Gs2Enhance.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enhance.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzUnleashRateModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string TargetInventoryModelId;
		[SerializeField]
		public string GradeModelId;
		[SerializeField]
		public List<Gs2.Unity.Gs2Enhance.Model.EzUnleashRateEntryModel> GradeEntries;

        public Gs2.Gs2Enhance.Model.UnleashRateModel ToModel()
        {
            return new Gs2.Gs2Enhance.Model.UnleashRateModel {
                Name = Name,
                Metadata = Metadata,
                TargetInventoryModelId = TargetInventoryModelId,
                GradeModelId = GradeModelId,
                GradeEntries = GradeEntries?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzUnleashRateModel FromModel(Gs2.Gs2Enhance.Model.UnleashRateModel model)
        {
            return new EzUnleashRateModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                TargetInventoryModelId = model.TargetInventoryModelId == null ? null : model.TargetInventoryModelId,
                GradeModelId = model.GradeModelId == null ? null : model.GradeModelId,
                GradeEntries = model.GradeEntries == null ? new List<Gs2.Unity.Gs2Enhance.Model.EzUnleashRateEntryModel>() : model.GradeEntries.Select(v => {
                    return Gs2.Unity.Gs2Enhance.Model.EzUnleashRateEntryModel.FromModel(v);
                }).ToList(),
            };
        }
    }
}