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
namespace Gs2.Unity.Gs2Schedule.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzEvent
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
		public string ScheduleType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string RepeatType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long AbsoluteBegin;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long AbsoluteEnd;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RepeatBeginDayOfMonth;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RepeatEndDayOfMonth;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string RepeatBeginDayOfWeek;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string RepeatEndDayOfWeek;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RepeatBeginHour;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int RepeatEndHour;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string RelativeTriggerName;

        public Gs2.Gs2Schedule.Model.Event ToModel()
        {
            return new Gs2.Gs2Schedule.Model.Event {
                Name = Name,
                Metadata = Metadata,
                ScheduleType = ScheduleType,
                RepeatType = RepeatType,
                AbsoluteBegin = AbsoluteBegin,
                AbsoluteEnd = AbsoluteEnd,
                RepeatBeginDayOfMonth = RepeatBeginDayOfMonth,
                RepeatEndDayOfMonth = RepeatEndDayOfMonth,
                RepeatBeginDayOfWeek = RepeatBeginDayOfWeek,
                RepeatEndDayOfWeek = RepeatEndDayOfWeek,
                RepeatBeginHour = RepeatBeginHour,
                RepeatEndHour = RepeatEndHour,
                RelativeTriggerName = RelativeTriggerName,
            };
        }

        public static EzEvent FromModel(Gs2.Gs2Schedule.Model.Event model)
        {
            return new EzEvent {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                ScheduleType = model.ScheduleType == null ? null : model.ScheduleType,
                RepeatType = model.RepeatType == null ? null : model.RepeatType,
                AbsoluteBegin = model.AbsoluteBegin ?? 0,
                AbsoluteEnd = model.AbsoluteEnd ?? 0,
                RepeatBeginDayOfMonth = model.RepeatBeginDayOfMonth ?? 0,
                RepeatEndDayOfMonth = model.RepeatEndDayOfMonth ?? 0,
                RepeatBeginDayOfWeek = model.RepeatBeginDayOfWeek == null ? null : model.RepeatBeginDayOfWeek,
                RepeatEndDayOfWeek = model.RepeatEndDayOfWeek == null ? null : model.RepeatEndDayOfWeek,
                RepeatBeginHour = model.RepeatBeginHour ?? 0,
                RepeatEndHour = model.RepeatEndHour ?? 0,
                RelativeTriggerName = model.RelativeTriggerName == null ? null : model.RelativeTriggerName,
            };
        }
    }
}