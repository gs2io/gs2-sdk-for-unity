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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Showcase.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRandomDisplayItem
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeAction> ConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> AcquireActions;
		[SerializeField]
		public int CurrentPurchaseCount;
		[SerializeField]
		public int MaximumPurchaseCount;

        public Gs2.Gs2Showcase.Model.RandomDisplayItem ToModel()
        {
            return new Gs2.Gs2Showcase.Model.RandomDisplayItem {
                Name = Name,
                Metadata = Metadata,
                ConsumeActions = ConsumeActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                AcquireActions = AcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                CurrentPurchaseCount = CurrentPurchaseCount,
                MaximumPurchaseCount = MaximumPurchaseCount,
            };
        }

        public static EzRandomDisplayItem FromModel(Gs2.Gs2Showcase.Model.RandomDisplayItem model)
        {
            return new EzRandomDisplayItem {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ConsumeActions = model.ConsumeActions == null ? new List<Gs2.Unity.Core.Model.EzConsumeAction>() : model.ConsumeActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
                CurrentPurchaseCount = model.CurrentPurchaseCount ?? 0,
                MaximumPurchaseCount = model.MaximumPurchaseCount ?? 0,
            };
        }
    }
}