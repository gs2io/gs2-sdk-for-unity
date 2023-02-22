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
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Schedule.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzEvent
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public string ScheduleType;
		[SerializeField]
		public string RepeatType;
		[SerializeField]
		public long AbsoluteBegin;
		[SerializeField]
		public long AbsoluteEnd;
		[SerializeField]
		public int RepeatBeginDayOfMonth;
		[SerializeField]
		public int RepeatEndDayOfMonth;
		[SerializeField]
		public string RepeatBeginDayOfWeek;
		[SerializeField]
		public string RepeatEndDayOfWeek;
		[SerializeField]
		public int RepeatBeginHour;
		[SerializeField]
		public int RepeatEndHour;
		[SerializeField]
		public string RelativeTriggerName;
		[SerializeField]
		public int RelativeDuration;

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
                RelativeDuration = RelativeDuration,
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
                RelativeDuration = model.RelativeDuration ?? 0,
            };
        }
    }
}