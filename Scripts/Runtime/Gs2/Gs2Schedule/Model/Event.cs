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
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Schedule.Model
{
	[Preserve]
	public class Event
	{

        /** イベントマスター */
        public string eventId { set; get; }

        /**
         * イベントマスターを設定
         *
         * @param eventId イベントマスター
         * @return this
         */
        public Event WithEventId(string eventId) {
            this.eventId = eventId;
            return this;
        }

        /** イベントの種類名 */
        public string name { set; get; }

        /**
         * イベントの種類名を設定
         *
         * @param name イベントの種類名
         * @return this
         */
        public Event WithName(string name) {
            this.name = name;
            return this;
        }

        /** イベントの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * イベントの種類のメタデータを設定
         *
         * @param metadata イベントの種類のメタデータ
         * @return this
         */
        public Event WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** イベント期間の種類 */
        public string scheduleType { set; get; }

        /**
         * イベント期間の種類を設定
         *
         * @param scheduleType イベント期間の種類
         * @return this
         */
        public Event WithScheduleType(string scheduleType) {
            this.scheduleType = scheduleType;
            return this;
        }

        /** 繰り返しの種類 */
        public string repeatType { set; get; }

        /**
         * 繰り返しの種類を設定
         *
         * @param repeatType 繰り返しの種類
         * @return this
         */
        public Event WithRepeatType(string repeatType) {
            this.repeatType = repeatType;
            return this;
        }

        /** イベントの開始日時 */
        public long? absoluteBegin { set; get; }

        /**
         * イベントの開始日時を設定
         *
         * @param absoluteBegin イベントの開始日時
         * @return this
         */
        public Event WithAbsoluteBegin(long? absoluteBegin) {
            this.absoluteBegin = absoluteBegin;
            return this;
        }

        /** イベントの終了日時 */
        public long? absoluteEnd { set; get; }

        /**
         * イベントの終了日時を設定
         *
         * @param absoluteEnd イベントの終了日時
         * @return this
         */
        public Event WithAbsoluteEnd(long? absoluteEnd) {
            this.absoluteEnd = absoluteEnd;
            return this;
        }

        /** イベントの繰り返し開始日 */
        public int? repeatBeginDayOfMonth { set; get; }

        /**
         * イベントの繰り返し開始日を設定
         *
         * @param repeatBeginDayOfMonth イベントの繰り返し開始日
         * @return this
         */
        public Event WithRepeatBeginDayOfMonth(int? repeatBeginDayOfMonth) {
            this.repeatBeginDayOfMonth = repeatBeginDayOfMonth;
            return this;
        }

        /** イベントの繰り返し終了日 */
        public int? repeatEndDayOfMonth { set; get; }

        /**
         * イベントの繰り返し終了日を設定
         *
         * @param repeatEndDayOfMonth イベントの繰り返し終了日
         * @return this
         */
        public Event WithRepeatEndDayOfMonth(int? repeatEndDayOfMonth) {
            this.repeatEndDayOfMonth = repeatEndDayOfMonth;
            return this;
        }

        /** イベントの繰り返し開始曜日 */
        public string repeatBeginDayOfWeek { set; get; }

        /**
         * イベントの繰り返し開始曜日を設定
         *
         * @param repeatBeginDayOfWeek イベントの繰り返し開始曜日
         * @return this
         */
        public Event WithRepeatBeginDayOfWeek(string repeatBeginDayOfWeek) {
            this.repeatBeginDayOfWeek = repeatBeginDayOfWeek;
            return this;
        }

        /** イベントの繰り返し終了曜日 */
        public string repeatEndDayOfWeek { set; get; }

        /**
         * イベントの繰り返し終了曜日を設定
         *
         * @param repeatEndDayOfWeek イベントの繰り返し終了曜日
         * @return this
         */
        public Event WithRepeatEndDayOfWeek(string repeatEndDayOfWeek) {
            this.repeatEndDayOfWeek = repeatEndDayOfWeek;
            return this;
        }

        /** イベントの繰り返し開始時間 */
        public int? repeatBeginHour { set; get; }

        /**
         * イベントの繰り返し開始時間を設定
         *
         * @param repeatBeginHour イベントの繰り返し開始時間
         * @return this
         */
        public Event WithRepeatBeginHour(int? repeatBeginHour) {
            this.repeatBeginHour = repeatBeginHour;
            return this;
        }

        /** イベントの繰り返し終了時間 */
        public int? repeatEndHour { set; get; }

        /**
         * イベントの繰り返し終了時間を設定
         *
         * @param repeatEndHour イベントの繰り返し終了時間
         * @return this
         */
        public Event WithRepeatEndHour(int? repeatEndHour) {
            this.repeatEndHour = repeatEndHour;
            return this;
        }

        /** イベントの開始トリガー */
        public string relativeTriggerName { set; get; }

        /**
         * イベントの開始トリガーを設定
         *
         * @param relativeTriggerName イベントの開始トリガー
         * @return this
         */
        public Event WithRelativeTriggerName(string relativeTriggerName) {
            this.relativeTriggerName = relativeTriggerName;
            return this;
        }

        /** イベントの開催期間(秒) */
        public int? relativeDuration { set; get; }

        /**
         * イベントの開催期間(秒)を設定
         *
         * @param relativeDuration イベントの開催期間(秒)
         * @return this
         */
        public Event WithRelativeDuration(int? relativeDuration) {
            this.relativeDuration = relativeDuration;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.eventId != null)
            {
                writer.WritePropertyName("eventId");
                writer.Write(this.eventId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.scheduleType != null)
            {
                writer.WritePropertyName("scheduleType");
                writer.Write(this.scheduleType);
            }
            if(this.repeatType != null)
            {
                writer.WritePropertyName("repeatType");
                writer.Write(this.repeatType);
            }
            if(this.absoluteBegin.HasValue)
            {
                writer.WritePropertyName("absoluteBegin");
                writer.Write(this.absoluteBegin.Value);
            }
            if(this.absoluteEnd.HasValue)
            {
                writer.WritePropertyName("absoluteEnd");
                writer.Write(this.absoluteEnd.Value);
            }
            if(this.repeatBeginDayOfMonth.HasValue)
            {
                writer.WritePropertyName("repeatBeginDayOfMonth");
                writer.Write(this.repeatBeginDayOfMonth.Value);
            }
            if(this.repeatEndDayOfMonth.HasValue)
            {
                writer.WritePropertyName("repeatEndDayOfMonth");
                writer.Write(this.repeatEndDayOfMonth.Value);
            }
            if(this.repeatBeginDayOfWeek != null)
            {
                writer.WritePropertyName("repeatBeginDayOfWeek");
                writer.Write(this.repeatBeginDayOfWeek);
            }
            if(this.repeatEndDayOfWeek != null)
            {
                writer.WritePropertyName("repeatEndDayOfWeek");
                writer.Write(this.repeatEndDayOfWeek);
            }
            if(this.repeatBeginHour.HasValue)
            {
                writer.WritePropertyName("repeatBeginHour");
                writer.Write(this.repeatBeginHour.Value);
            }
            if(this.repeatEndHour.HasValue)
            {
                writer.WritePropertyName("repeatEndHour");
                writer.Write(this.repeatEndHour.Value);
            }
            if(this.relativeTriggerName != null)
            {
                writer.WritePropertyName("relativeTriggerName");
                writer.Write(this.relativeTriggerName);
            }
            if(this.relativeDuration.HasValue)
            {
                writer.WritePropertyName("relativeDuration");
                writer.Write(this.relativeDuration.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Event FromDict(JsonData data)
        {
            return new Event()
                .WithEventId(data.Keys.Contains("eventId") && data["eventId"] != null ? data["eventId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithScheduleType(data.Keys.Contains("scheduleType") && data["scheduleType"] != null ? data["scheduleType"].ToString() : null)
                .WithRepeatType(data.Keys.Contains("repeatType") && data["repeatType"] != null ? data["repeatType"].ToString() : null)
                .WithAbsoluteBegin(data.Keys.Contains("absoluteBegin") && data["absoluteBegin"] != null ? (long?)long.Parse(data["absoluteBegin"].ToString()) : null)
                .WithAbsoluteEnd(data.Keys.Contains("absoluteEnd") && data["absoluteEnd"] != null ? (long?)long.Parse(data["absoluteEnd"].ToString()) : null)
                .WithRepeatBeginDayOfMonth(data.Keys.Contains("repeatBeginDayOfMonth") && data["repeatBeginDayOfMonth"] != null ? (int?)int.Parse(data["repeatBeginDayOfMonth"].ToString()) : null)
                .WithRepeatEndDayOfMonth(data.Keys.Contains("repeatEndDayOfMonth") && data["repeatEndDayOfMonth"] != null ? (int?)int.Parse(data["repeatEndDayOfMonth"].ToString()) : null)
                .WithRepeatBeginDayOfWeek(data.Keys.Contains("repeatBeginDayOfWeek") && data["repeatBeginDayOfWeek"] != null ? data["repeatBeginDayOfWeek"].ToString() : null)
                .WithRepeatEndDayOfWeek(data.Keys.Contains("repeatEndDayOfWeek") && data["repeatEndDayOfWeek"] != null ? data["repeatEndDayOfWeek"].ToString() : null)
                .WithRepeatBeginHour(data.Keys.Contains("repeatBeginHour") && data["repeatBeginHour"] != null ? (int?)int.Parse(data["repeatBeginHour"].ToString()) : null)
                .WithRepeatEndHour(data.Keys.Contains("repeatEndHour") && data["repeatEndHour"] != null ? (int?)int.Parse(data["repeatEndHour"].ToString()) : null)
                .WithRelativeTriggerName(data.Keys.Contains("relativeTriggerName") && data["relativeTriggerName"] != null ? data["relativeTriggerName"].ToString() : null)
                .WithRelativeDuration(data.Keys.Contains("relativeDuration") && data["relativeDuration"] != null ? (int?)int.Parse(data["relativeDuration"].ToString()) : null);
        }
	}
}