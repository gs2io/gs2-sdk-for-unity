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
using Gs2.Gs2SkillTree.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SkillTree.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzNodeModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeAction> ReleaseConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> ReturnAcquireActions;
		[SerializeField]
		public float RestrainReturnRate;

        public Gs2.Gs2SkillTree.Model.NodeModel ToModel()
        {
            return new Gs2.Gs2SkillTree.Model.NodeModel {
                Name = Name,
                Metadata = Metadata,
                ReleaseConsumeActions = ReleaseConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ReturnAcquireActions = ReturnAcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                RestrainReturnRate = RestrainReturnRate,
            };
        }

        public static EzNodeModel FromModel(Gs2.Gs2SkillTree.Model.NodeModel model)
        {
            return new EzNodeModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ReleaseConsumeActions = model.ReleaseConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.ReleaseConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                ReturnAcquireActions = model.ReturnAcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.ReturnAcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
                RestrainReturnRate = model.RestrainReturnRate ?? 0,
            };
        }
    }
}