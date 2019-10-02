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
	public class EventMaster
	{

        /** イベントマスター */
        public string eventId { set; get; }

        /**
         * イベントマスターを設定
         *
         * @param eventId イベントマスター
         * @return this
         */
        public EventMaster WithEventId(string eventId) {
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
        public EventMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** イベントマスターの説明 */
        public string description { set; get; }

        /**
         * イベントマスターの説明を設定
         *
         * @param description イベントマスターの説明
         * @return this
         */
        public EventMaster WithDescription(string description) {
            this.description = description;
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
        public EventMaster WithMetadata(string metadata) {
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
        public EventMaster WithScheduleType(string scheduleType) {
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
        public EventMaster WithRepeatType(string repeatType) {
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
        public EventMaster WithAbsoluteBegin(long? absoluteBegin) {
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
        public EventMaster WithAbsoluteEnd(long? absoluteEnd) {
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
        public EventMaster WithRepeatBeginDayOfMonth(int? repeatBeginDayOfMonth) {
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
        public EventMaster WithRepeatEndDayOfMonth(int? repeatEndDayOfMonth) {
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
        public EventMaster WithRepeatBeginDayOfWeek(string repeatBeginDayOfWeek) {
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
        public EventMaster WithRepeatEndDayOfWeek(string repeatEndDayOfWeek) {
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
        public EventMaster WithRepeatBeginHour(int? repeatBeginHour) {
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
        public EventMaster WithRepeatEndHour(int? repeatEndHour) {
            this.repeatEndHour = repeatEndHour;
            return this;
        }

        /** イベントの開始トリガー名 */
        public string relativeTriggerName { set; get; }

        /**
         * イベントの開始トリガー名を設定
         *
         * @param relativeTriggerName イベントの開始トリガー名
         * @return this
         */
        public EventMaster WithRelativeTriggerName(string relativeTriggerName) {
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
        public EventMaster WithRelativeDuration(int? relativeDuration) {
            this.relativeDuration = relativeDuration;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public EventMaster WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public EventMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
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
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static EventMaster FromDict(JsonData data)
        {
            return new EventMaster()
                .WithEventId(data.Keys.Contains("eventId") && data["eventId"] != null ? (string) data["eventId"] : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? (string) data["name"] : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? (string) data["description"] : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? (string) data["metadata"] : null)
                .WithScheduleType(data.Keys.Contains("scheduleType") && data["scheduleType"] != null ? (string) data["scheduleType"] : null)
                .WithRepeatType(data.Keys.Contains("repeatType") && data["repeatType"] != null ? (string) data["repeatType"] : null)
                .WithAbsoluteBegin(data.Keys.Contains("absoluteBegin") && data["absoluteBegin"] != null ? (long?) data["absoluteBegin"] : null)
                .WithAbsoluteEnd(data.Keys.Contains("absoluteEnd") && data["absoluteEnd"] != null ? (long?) data["absoluteEnd"] : null)
                .WithRepeatBeginDayOfMonth(data.Keys.Contains("repeatBeginDayOfMonth") && data["repeatBeginDayOfMonth"] != null ? (int?) data["repeatBeginDayOfMonth"] : null)
                .WithRepeatEndDayOfMonth(data.Keys.Contains("repeatEndDayOfMonth") && data["repeatEndDayOfMonth"] != null ? (int?) data["repeatEndDayOfMonth"] : null)
                .WithRepeatBeginDayOfWeek(data.Keys.Contains("repeatBeginDayOfWeek") && data["repeatBeginDayOfWeek"] != null ? (string) data["repeatBeginDayOfWeek"] : null)
                .WithRepeatEndDayOfWeek(data.Keys.Contains("repeatEndDayOfWeek") && data["repeatEndDayOfWeek"] != null ? (string) data["repeatEndDayOfWeek"] : null)
                .WithRepeatBeginHour(data.Keys.Contains("repeatBeginHour") && data["repeatBeginHour"] != null ? (int?) data["repeatBeginHour"] : null)
                .WithRepeatEndHour(data.Keys.Contains("repeatEndHour") && data["repeatEndHour"] != null ? (int?) data["repeatEndHour"] : null)
                .WithRelativeTriggerName(data.Keys.Contains("relativeTriggerName") && data["relativeTriggerName"] != null ? (string) data["relativeTriggerName"] : null)
                .WithRelativeDuration(data.Keys.Contains("relativeDuration") && data["relativeDuration"] != null ? (int?) data["relativeDuration"] : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?) data["createdAt"] : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?) data["updatedAt"] : null);
        }
	}
}