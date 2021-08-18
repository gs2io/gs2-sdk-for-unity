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
	public class EzSalesItem
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public List<Gs2.Unity.Gs2Showcase.Model.EzConsumeAction> ConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Gs2Showcase.Model.EzAcquireAction> AcquireActions;

        public Gs2.Gs2Showcase.Model.SalesItem ToModel()
        {
            return new Gs2.Gs2Showcase.Model.SalesItem {
                Name = Name,
                Metadata = Metadata,
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
                ConsumeActions = model.ConsumeActions == null ? new List<Gs2.Unity.Gs2Showcase.Model.EzConsumeAction>() : model.ConsumeActions.Select(v => {
                    return Gs2.Unity.Gs2Showcase.Model.EzConsumeAction.FromModel(v);
                }).ToList(),
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Gs2Showcase.Model.EzAcquireAction>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Gs2Showcase.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}