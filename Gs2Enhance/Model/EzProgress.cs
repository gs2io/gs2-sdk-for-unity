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
	public class EzProgress
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string RateName;
		[SerializeField]
		public string PropertyId;
		[SerializeField]
		public int ExperienceValue;
		[SerializeField]
		public float Rate;

        public Gs2.Gs2Enhance.Model.Progress ToModel()
        {
            return new Gs2.Gs2Enhance.Model.Progress {
                Name = Name,
                RateName = RateName,
                PropertyId = PropertyId,
                ExperienceValue = ExperienceValue,
                Rate = Rate,
            };
        }

        public static EzProgress FromModel(Gs2.Gs2Enhance.Model.Progress model)
        {
            return new EzProgress {
                Name = model.Name == null ? null : model.Name,
                RateName = model.RateName == null ? null : model.RateName,
                PropertyId = model.PropertyId == null ? null : model.PropertyId,
                ExperienceValue = model.ExperienceValue ?? 0,
                Rate = model.Rate ?? 0,
            };
        }
    }
}