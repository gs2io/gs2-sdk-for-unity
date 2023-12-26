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
using Gs2.Gs2Grade.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Grade.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGradeModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string ExperienceModelId;
		[SerializeField]
		public List<Gs2.Unity.Gs2Grade.Model.EzGradeEntryModel> GradeEntries;
		[SerializeField]
		public List<Gs2.Unity.Gs2Grade.Model.EzAcquireActionRate> AcquireActionRates;

        public Gs2.Gs2Grade.Model.GradeModel ToModel()
        {
            return new Gs2.Gs2Grade.Model.GradeModel {
                Name = Name,
                Metadata = Metadata,
                ExperienceModelId = ExperienceModelId,
                GradeEntries = GradeEntries?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                AcquireActionRates = AcquireActionRates?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzGradeModel FromModel(Gs2.Gs2Grade.Model.GradeModel model)
        {
            return new EzGradeModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ExperienceModelId = model.ExperienceModelId == null ? null : model.ExperienceModelId,
                GradeEntries = model.GradeEntries == null ? new List<Gs2.Unity.Gs2Grade.Model.EzGradeEntryModel>() : model.GradeEntries.Select(v => {
                    return Gs2.Unity.Gs2Grade.Model.EzGradeEntryModel.FromModel(v);
                }).ToList(),
                AcquireActionRates = model.AcquireActionRates == null ? new List<Gs2.Unity.Gs2Grade.Model.EzAcquireActionRate>() : model.AcquireActionRates.Select(v => {
                    return Gs2.Unity.Gs2Grade.Model.EzAcquireActionRate.FromModel(v);
                }).ToList(),
            };
        }
    }
}