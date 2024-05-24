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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Mission.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzTargetCounterModel
	{
		[SerializeField]
		public string CounterName;
		[SerializeField]
		public string ResetType;
		[SerializeField]
		public long Value;

        public Gs2.Gs2Mission.Model.TargetCounterModel ToModel()
        {
            return new Gs2.Gs2Mission.Model.TargetCounterModel {
                CounterName = CounterName,
                ResetType = ResetType,
                Value = Value,
            };
        }

        public static EzTargetCounterModel FromModel(Gs2.Gs2Mission.Model.TargetCounterModel model)
        {
            return new EzTargetCounterModel {
                CounterName = model.CounterName == null ? null : model.CounterName,
                ResetType = model.ResetType == null ? null : model.ResetType,
                Value = model.Value ?? 0,
            };
        }
    }
}