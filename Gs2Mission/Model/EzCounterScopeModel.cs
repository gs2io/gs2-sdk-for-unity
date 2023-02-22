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
	public class EzCounterScopeModel
	{
		[SerializeField]
		public string ResetType;
		[SerializeField]
		public int ResetDayOfMonth;
		[SerializeField]
		public string ResetDayOfWeek;
		[SerializeField]
		public int ResetHour;

        public Gs2.Gs2Mission.Model.CounterScopeModel ToModel()
        {
            return new Gs2.Gs2Mission.Model.CounterScopeModel {
                ResetType = ResetType,
                ResetDayOfMonth = ResetDayOfMonth,
                ResetDayOfWeek = ResetDayOfWeek,
                ResetHour = ResetHour,
            };
        }

        public static EzCounterScopeModel FromModel(Gs2.Gs2Mission.Model.CounterScopeModel model)
        {
            return new EzCounterScopeModel {
                ResetType = model.ResetType == null ? null : model.ResetType,
                ResetDayOfMonth = model.ResetDayOfMonth ?? 0,
                ResetDayOfWeek = model.ResetDayOfWeek == null ? null : model.ResetDayOfWeek,
                ResetHour = model.ResetHour ?? 0,
            };
        }
    }
}