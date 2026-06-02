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
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Exchange.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzIncrementalRateModel
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
		public string CalculateType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Core.Model.EzConsumeAction ConsumeAction;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long BaseValue;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long CoefficientValue;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ExchangeCountId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int MaximumExchangeCount;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public List<Gs2.Unity.Core.Model.EzAcquireAction> AcquireActions;

        public Gs2.Gs2Exchange.Model.IncrementalRateModel ToModel()
        {
            return new Gs2.Gs2Exchange.Model.IncrementalRateModel {
                Name = Name,
                Metadata = Metadata,
                CalculateType = CalculateType,
                ConsumeAction = ConsumeAction?.ToModel(),
                BaseValue = BaseValue,
                CoefficientValue = CoefficientValue,
                ExchangeCountId = ExchangeCountId,
                MaximumExchangeCount = MaximumExchangeCount,
                AcquireActions = AcquireActions?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
            };
        }

        public static EzIncrementalRateModel FromModel(Gs2.Gs2Exchange.Model.IncrementalRateModel model)
        {
            return new EzIncrementalRateModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                CalculateType = model.CalculateType == null ? null : model.CalculateType,
                ConsumeAction = model.ConsumeAction == null ? null : Gs2.Unity.Core.Model.EzConsumeAction.FromModel(model.ConsumeAction),
                BaseValue = model.BaseValue ?? 0,
                CoefficientValue = model.CoefficientValue ?? 0,
                ExchangeCountId = model.ExchangeCountId == null ? null : model.ExchangeCountId,
                MaximumExchangeCount = model.MaximumExchangeCount ?? 0,
                AcquireActions = model.AcquireActions == null ? new List<Gs2.Unity.Core.Model.EzAcquireAction>() : model.AcquireActions.Select(v => {
                    return Gs2.Unity.Core.Model.EzAcquireAction.FromModel(v);
                }).ToList(),
            };
        }
    }
}