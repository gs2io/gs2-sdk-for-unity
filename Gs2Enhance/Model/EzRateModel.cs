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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Enhance.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRateModel
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
		public string TargetInventoryModelId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string AcquireExperienceSuffix;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string MaterialInventoryModelId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<string> AcquireExperienceHierarchy;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ExperienceModelId;

        public Gs2.Gs2Enhance.Model.RateModel ToModel()
        {
            return new Gs2.Gs2Enhance.Model.RateModel {
                Name = Name,
                Metadata = Metadata,
                TargetInventoryModelId = TargetInventoryModelId,
                AcquireExperienceSuffix = AcquireExperienceSuffix,
                MaterialInventoryModelId = MaterialInventoryModelId,
                AcquireExperienceHierarchy = AcquireExperienceHierarchy?.Select(v => {
                    return v;
                }).ToArray(),
                ExperienceModelId = ExperienceModelId,
            };
        }

        public static EzRateModel FromModel(Gs2.Gs2Enhance.Model.RateModel model)
        {
            return new EzRateModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                TargetInventoryModelId = model.TargetInventoryModelId == null ? null : model.TargetInventoryModelId,
                AcquireExperienceSuffix = model.AcquireExperienceSuffix == null ? null : model.AcquireExperienceSuffix,
                MaterialInventoryModelId = model.MaterialInventoryModelId == null ? null : model.MaterialInventoryModelId,
                AcquireExperienceHierarchy = model.AcquireExperienceHierarchy == null ? new List<string>() : model.AcquireExperienceHierarchy.Select(v => {
                    return v;
                }).ToList(),
                ExperienceModelId = model.ExperienceModelId == null ? null : model.ExperienceModelId,
            };
        }
    }
}