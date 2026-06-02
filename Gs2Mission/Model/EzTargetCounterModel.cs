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
	public class EzTargetCounterModel
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string CounterName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ScopeType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ResetType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string ConditionName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long Value;

        public Gs2.Gs2Mission.Model.TargetCounterModel ToModel()
        {
            return new Gs2.Gs2Mission.Model.TargetCounterModel {
                CounterName = CounterName,
                ScopeType = ScopeType,
                ResetType = ResetType,
                ConditionName = ConditionName,
                Value = Value,
            };
        }

        public static EzTargetCounterModel FromModel(Gs2.Gs2Mission.Model.TargetCounterModel model)
        {
            return new EzTargetCounterModel {
                CounterName = model.CounterName == null ? null : model.CounterName,
                ScopeType = model.ScopeType == null ? null : model.ScopeType,
                ResetType = model.ResetType == null ? null : model.ResetType,
                ConditionName = model.ConditionName == null ? null : model.ConditionName,
                Value = model.Value ?? 0,
            };
        }
    }
}