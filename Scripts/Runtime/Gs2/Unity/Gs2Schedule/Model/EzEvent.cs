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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Schedule.Model
{
	[Preserve]
	[System.Serializable]
	public class EzEvent
	{
		/** イベントの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** イベントの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** イベント期間の種類 */
		[UnityEngine.SerializeField]
		public string ScheduleType;
		/** 繰り返しの種類 */
		[UnityEngine.SerializeField]
		public string RepeatType;
		/** イベントの開始日時 */
		[UnityEngine.SerializeField]
		public long AbsoluteBegin;
		/** イベントの終了日時 */
		[UnityEngine.SerializeField]
		public long AbsoluteEnd;
		/** イベントの繰り返し開始日 */
		[UnityEngine.SerializeField]
		public int RepeatBeginDayOfMonth;
		/** イベントの繰り返し終了日 */
		[UnityEngine.SerializeField]
		public int RepeatEndDayOfMonth;
		/** イベントの繰り返し開始曜日 */
		[UnityEngine.SerializeField]
		public string RepeatBeginDayOfWeek;
		/** イベントの繰り返し終了曜日 */
		[UnityEngine.SerializeField]
		public string RepeatEndDayOfWeek;
		/** イベントの繰り返し開始時間 */
		[UnityEngine.SerializeField]
		public int RepeatBeginHour;
		/** イベントの繰り返し終了時間 */
		[UnityEngine.SerializeField]
		public int RepeatEndHour;
		/** イベントの開始トリガー */
		[UnityEngine.SerializeField]
		public string RelativeTriggerName;
		/** イベントの開催期間(秒) */
		[UnityEngine.SerializeField]
		public int RelativeDuration;

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

        public virtual Event ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            if(this.ScheduleType != null)
            {
                writer.WritePropertyName("scheduleType");
                writer.Write(this.ScheduleType);
            }
            if(this.RepeatType != null)
            {
                writer.WritePropertyName("repeatType");
                writer.Write(this.RepeatType);
            }
            writer.WritePropertyName("absoluteBegin");
            writer.Write(this.AbsoluteBegin);
            writer.WritePropertyName("absoluteEnd");
            writer.Write(this.AbsoluteEnd);
            writer.WritePropertyName("repeatBeginDayOfMonth");
            writer.Write(this.RepeatBeginDayOfMonth);
            writer.WritePropertyName("repeatEndDayOfMonth");
            writer.Write(this.RepeatEndDayOfMonth);
            if(this.RepeatBeginDayOfWeek != null)
            {
                writer.WritePropertyName("repeatBeginDayOfWeek");
                writer.Write(this.RepeatBeginDayOfWeek);
            }
            if(this.RepeatEndDayOfWeek != null)
            {
                writer.WritePropertyName("repeatEndDayOfWeek");
                writer.Write(this.RepeatEndDayOfWeek);
            }
            writer.WritePropertyName("repeatBeginHour");
            writer.Write(this.RepeatBeginHour);
            writer.WritePropertyName("repeatEndHour");
            writer.Write(this.RepeatEndHour);
            if(this.RelativeTriggerName != null)
            {
                writer.WritePropertyName("relativeTriggerName");
                writer.Write(this.RelativeTriggerName);
            }
            writer.WritePropertyName("relativeDuration");
            writer.Write(this.RelativeDuration);
            writer.WriteObjectEnd();
        }
	}
}
