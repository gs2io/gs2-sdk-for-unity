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
using Gs2.Gs2Exchange.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Exchange.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzRateModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string TimingType;
		[SerializeField]
		public int LockTime;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzVerifyAction> VerifyActions;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzConsumeAction> ConsumeActions;
		[SerializeField]
		public List<Gs2.Unity.Core.Model.EzAcquireAction> AcquireActions;

        public Gs2.Gs2Exchange.Model.RateModel ToModel()
        {
            return new Gs2.Gs2Exchange.Model.RateModel {
                Name = Name,
                Metadata = Metadata,
                TimingType = TimingType,
                LockTime = LockTime,
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

        public static EzRateModel FromModel(Gs2.Gs2Exchange.Model.RateModel model)
        {
            return new EzRateModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                TimingType = model.TimingType == null ? null : model.TimingType,
                LockTime = model.LockTime ?? 0,
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