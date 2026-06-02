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

using Gs2.Gs2Schedule.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Schedule.Result
{
#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzGetEventResult
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Schedule.Model.EzEvent Item;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool InSchedule;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long ScheduleStartAt;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long ScheduleEndAt;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Schedule.Model.EzRepeatSchedule RepeatSchedule;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public bool IsGlobalSchedule;

        public static EzGetEventResult FromModel(Gs2.Gs2Schedule.Result.GetEventResult model)
        {
            return new EzGetEventResult {
                Item = model.Item == null ? null : Gs2.Unity.Gs2Schedule.Model.EzEvent.FromModel(model.Item),
                InSchedule = model.InSchedule ?? false,
                ScheduleStartAt = model.ScheduleStartAt ?? 0,
                ScheduleEndAt = model.ScheduleEndAt ?? 0,
                RepeatSchedule = model.RepeatSchedule == null ? null : Gs2.Unity.Gs2Schedule.Model.EzRepeatSchedule.FromModel(model.RepeatSchedule),
                IsGlobalSchedule = model.IsGlobalSchedule ?? false,
            };
        }
    }
}