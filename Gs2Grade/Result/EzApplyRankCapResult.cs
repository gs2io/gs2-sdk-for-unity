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

using Gs2.Gs2Grade.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Grade.Result
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzApplyRankCapResult
	{
		[SerializeField]
		public Gs2.Unity.Gs2Grade.Model.EzStatus Item;
		[SerializeField]
		public string ExperienceNamespaceName;
		[SerializeField]
		public Gs2.Unity.Gs2Experience.Model.EzStatus ExperienceStatus;

        public static EzApplyRankCapResult FromModel(Gs2.Gs2Grade.Result.ApplyRankCapResult model)
        {
            return new EzApplyRankCapResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2Grade.Model.EzStatus.FromModel(model.Item),
                ExperienceNamespaceName = model.ExperienceNamespaceName == null ? null : model.ExperienceNamespaceName,
                ExperienceStatus = model.ExperienceStatus == null ? null : Gs2.Unity.Gs2Experience.Model.EzStatus.FromModel(model.ExperienceStatus),
            };
        }
    }
}