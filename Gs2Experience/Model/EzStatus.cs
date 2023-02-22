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
using Gs2.Gs2Experience.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Experience.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzStatus
	{
		[SerializeField]
		public string ExperienceName;
		[SerializeField]
		public string PropertyId;
		[SerializeField]
		public long ExperienceValue;
		[SerializeField]
		public long RankValue;
		[SerializeField]
		public long RankCapValue;

        public Gs2.Gs2Experience.Model.Status ToModel()
        {
            return new Gs2.Gs2Experience.Model.Status {
                ExperienceName = ExperienceName,
                PropertyId = PropertyId,
                ExperienceValue = ExperienceValue,
                RankValue = RankValue,
                RankCapValue = RankCapValue,
            };
        }

        public static EzStatus FromModel(Gs2.Gs2Experience.Model.Status model)
        {
            return new EzStatus {
                ExperienceName = model.ExperienceName == null ? null : model.ExperienceName,
                PropertyId = model.PropertyId == null ? null : model.PropertyId,
                ExperienceValue = model.ExperienceValue ?? 0,
                RankValue = model.RankValue ?? 0,
                RankCapValue = model.RankCapValue ?? 0,
            };
        }
    }
}