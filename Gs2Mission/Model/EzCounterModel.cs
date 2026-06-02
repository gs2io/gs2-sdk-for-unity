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
using Gs2.Gs2Mission.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCounterModel
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
		public List<Gs2.Unity.Gs2Mission.Model.EzCounterScopeModel> Scopes;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ChallengePeriodEventId;

        public Gs2.Gs2Mission.Model.CounterModel ToModel()
        {
            return new Gs2.Gs2Mission.Model.CounterModel {
                Name = Name,
                Metadata = Metadata,
                Scopes = Scopes?.Select(v => {
                    return v.ToModel();
                }).ToArray(),
                ChallengePeriodEventId = ChallengePeriodEventId,
            };
        }

        public static EzCounterModel FromModel(Gs2.Gs2Mission.Model.CounterModel model)
        {
            return new EzCounterModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                Scopes = model.Scopes == null ? new List<Gs2.Unity.Gs2Mission.Model.EzCounterScopeModel>() : model.Scopes.Select(v => {
                    return Gs2.Unity.Gs2Mission.Model.EzCounterScopeModel.FromModel(v);
                }).ToList(),
                ChallengePeriodEventId = model.ChallengePeriodEventId == null ? null : model.ChallengePeriodEventId,
            };
        }
    }
}