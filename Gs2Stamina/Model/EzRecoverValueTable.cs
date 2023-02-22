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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Stamina.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRecoverValueTable
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string ExperienceModelId;
		[SerializeField]
		public List<int> Values;

        public Gs2.Gs2Stamina.Model.RecoverValueTable ToModel()
        {
            return new Gs2.Gs2Stamina.Model.RecoverValueTable {
                Name = Name,
                Metadata = Metadata,
                ExperienceModelId = ExperienceModelId,
                Values = Values?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzRecoverValueTable FromModel(Gs2.Gs2Stamina.Model.RecoverValueTable model)
        {
            return new EzRecoverValueTable {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ExperienceModelId = model.ExperienceModelId == null ? null : model.ExperienceModelId,
                Values = model.Values == null ? new List<int>() : model.Values.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}