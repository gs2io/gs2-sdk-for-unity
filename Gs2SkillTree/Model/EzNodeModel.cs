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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SkillTree.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzNodeModel
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
		public List<Gs2.Unity.Core.Model.EzVerifyAction> ReleaseVerifyActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzConsumeAction> ReleaseConsumeActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzAcquireAction> ReturnAcquireActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public float RestrainReturnRate;

        public Gs2.Gs2SkillTree.Model.NodeModel ToModel()
        {
            return new Gs2.Gs2SkillTree.Model.NodeModel {
                Name = Name,
                Metadata = Metadata,
                ReleaseVerifyActions = ReleaseVerifyActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
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
                ReleaseVerifyActions = model.ReleaseVerifyActions == null ? new List<Gs2.Unity.Core.Model.EzVerifyAction>() : model.ReleaseVerifyActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzVerifyAction.FromModel(v);
                }).ToList(),
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