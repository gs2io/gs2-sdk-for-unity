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
using Gs2.Gs2Showcase.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Showcase.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSalesItem
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
		public List<Gs2.Unity.Core.Model.EzVerifyAction> VerifyActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzConsumeAction> ConsumeActions;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzAcquireAction> AcquireActions;

        public Gs2.Gs2Showcase.Model.SalesItem ToModel()
        {
            return new Gs2.Gs2Showcase.Model.SalesItem {
                Name = Name,
                Metadata = Metadata,
                VerifyActions = VerifyActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ConsumeActions = ConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                AcquireActions = AcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzSalesItem FromModel(Gs2.Gs2Showcase.Model.SalesItem model)
        {
            return new EzSalesItem {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                VerifyActions = model.VerifyActions == null ? new List<Gs2.Unity.Core.Model.EzVerifyAction>() : model.VerifyActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzVerifyAction.FromModel(v);
                }).ToList(),
                ConsumeActions = model.ConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.ConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}