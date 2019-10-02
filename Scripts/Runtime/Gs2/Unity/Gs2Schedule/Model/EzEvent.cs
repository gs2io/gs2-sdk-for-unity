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
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Schedule.Model
{
	[Preserve]
	public class EzEvent
	{
		/** イベントの種類名 */
		public string Name { get; set; }
		/** イベントの種類のメタデータ */
		public string Metadata { get; set; }
		/** イベント期間の種類 */
		public string ScheduleType { get; set; }
		/** 繰り返しの種類 */
		public string RepeatType { get; set; }
		/** イベントの開始日時 */
		public long AbsoluteBegin { get; set; }
		/** イベントの終了日時 */
		public long AbsoluteEnd { get; set; }
		/** イベントの繰り返し開始日 */
		public int RepeatBeginDayOfMonth { get; set; }
		/** イベントの繰り返し終了日 */
		public int RepeatEndDayOfMonth { get; set; }
		/** イベントの繰り返し開始曜日 */
		public string RepeatBeginDayOfWeek { get; set; }
		/** イベントの繰り返し終了曜日 */
		public string RepeatEndDayOfWeek { get; set; }
		/** イベントの繰り返し開始時間 */
		public int RepeatBeginHour { get; set; }
		/** イベントの繰り返し終了時間 */
		public int RepeatEndHour { get; set; }
		/** イベントの開始トリガー */
		public string RelativeTriggerName { get; set; }
		/** イベントの開催期間(秒) */
		public int RelativeDuration { get; set; }

		public EzEvent()
		{

		}

		public EzEvent(Gs2.Gs2Schedule.Model.Event @event)
		{
			Name = @event.name;
			Metadata = @event.metadata;
			ScheduleType = @event.scheduleType;
			RepeatType = @event.repeatType;
			AbsoluteBegin = @event.absoluteBegin.HasValue ? @event.absoluteBegin.Value : 0;
			AbsoluteEnd = @event.absoluteEnd.HasValue ? @event.absoluteEnd.Value : 0;
			RepeatBeginDayOfMonth = @event.repeatBeginDayOfMonth.HasValue ? @event.repeatBeginDayOfMonth.Value : 0;
			RepeatEndDayOfMonth = @event.repeatEndDayOfMonth.HasValue ? @event.repeatEndDayOfMonth.Value : 0;
			RepeatBeginDayOfWeek = @event.repeatBeginDayOfWeek;
			RepeatEndDayOfWeek = @event.repeatEndDayOfWeek;
			RepeatBeginHour = @event.repeatBeginHour.HasValue ? @event.repeatBeginHour.Value : 0;
			RepeatEndHour = @event.repeatEndHour.HasValue ? @event.repeatEndHour.Value : 0;
			RelativeTriggerName = @event.relativeTriggerName;
			RelativeDuration = @event.relativeDuration.HasValue ? @event.relativeDuration.Value : 0;
		}

        public Event ToModel()
        {
            return new Event {
                name = Name,
                metadata = Metadata,
                scheduleType = ScheduleType,
                repeatType = RepeatType,
                absoluteBegin = AbsoluteBegin,
                absoluteEnd = AbsoluteEnd,
                repeatBeginDayOfMonth = RepeatBeginDayOfMonth,
                repeatEndDayOfMonth = RepeatEndDayOfMonth,
                repeatBeginDayOfWeek = RepeatBeginDayOfWeek,
                repeatEndDayOfWeek = RepeatEndDayOfWeek,
                repeatBeginHour = RepeatBeginHour,
                repeatEndHour = RepeatEndHour,
                relativeTriggerName = RelativeTriggerName,
                relativeDuration = RelativeDuration,
            };
        }
	}
}
