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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Schedule.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Schedule.Request
{
	[Preserve]
	public class UpdateEventMasterRequest : Gs2Request<UpdateEventMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateEventMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** イベントの種類名 */
        public string eventName { set; get; }

        /**
         * イベントの種類名を設定
         *
         * @param eventName イベントの種類名
         * @return this
         */
        public UpdateEventMasterRequest WithEventName(string eventName) {
            this.eventName = eventName;
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
        public UpdateEventMasterRequest WithDescription(string description) {
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
        public UpdateEventMasterRequest WithMetadata(string metadata) {
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
        public UpdateEventMasterRequest WithScheduleType(string scheduleType) {
            this.scheduleType = scheduleType;
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
        public UpdateEventMasterRequest WithAbsoluteBegin(long? absoluteBegin) {
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
        public UpdateEventMasterRequest WithAbsoluteEnd(long? absoluteEnd) {
            this.absoluteEnd = absoluteEnd;
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
        public UpdateEventMasterRequest WithRepeatType(string repeatType) {
            this.repeatType = repeatType;
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
        public UpdateEventMasterRequest WithRepeatBeginDayOfMonth(int? repeatBeginDayOfMonth) {
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
        public UpdateEventMasterRequest WithRepeatEndDayOfMonth(int? repeatEndDayOfMonth) {
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
        public UpdateEventMasterRequest WithRepeatBeginDayOfWeek(string repeatBeginDayOfWeek) {
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
        public UpdateEventMasterRequest WithRepeatEndDayOfWeek(string repeatEndDayOfWeek) {
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
        public UpdateEventMasterRequest WithRepeatBeginHour(int? repeatBeginHour) {
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
        public UpdateEventMasterRequest WithRepeatEndHour(int? repeatEndHour) {
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
        public UpdateEventMasterRequest WithRelativeTriggerName(string relativeTriggerName) {
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
        public UpdateEventMasterRequest WithRelativeDuration(int? relativeDuration) {
            this.relativeDuration = relativeDuration;
            return this;
        }


    	[Preserve]
        public static UpdateEventMasterRequest FromDict(JsonData data)
        {
            return new UpdateEventMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                eventName = data.Keys.Contains("eventName") && data["eventName"] != null ? data["eventName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                scheduleType = data.Keys.Contains("scheduleType") && data["scheduleType"] != null ? data["scheduleType"].ToString(): null,
                absoluteBegin = data.Keys.Contains("absoluteBegin") && data["absoluteBegin"] != null ? (long?)long.Parse(data["absoluteBegin"].ToString()) : null,
                absoluteEnd = data.Keys.Contains("absoluteEnd") && data["absoluteEnd"] != null ? (long?)long.Parse(data["absoluteEnd"].ToString()) : null,
                repeatType = data.Keys.Contains("repeatType") && data["repeatType"] != null ? data["repeatType"].ToString(): null,
                repeatBeginDayOfMonth = data.Keys.Contains("repeatBeginDayOfMonth") && data["repeatBeginDayOfMonth"] != null ? (int?)int.Parse(data["repeatBeginDayOfMonth"].ToString()) : null,
                repeatEndDayOfMonth = data.Keys.Contains("repeatEndDayOfMonth") && data["repeatEndDayOfMonth"] != null ? (int?)int.Parse(data["repeatEndDayOfMonth"].ToString()) : null,
                repeatBeginDayOfWeek = data.Keys.Contains("repeatBeginDayOfWeek") && data["repeatBeginDayOfWeek"] != null ? data["repeatBeginDayOfWeek"].ToString(): null,
                repeatEndDayOfWeek = data.Keys.Contains("repeatEndDayOfWeek") && data["repeatEndDayOfWeek"] != null ? data["repeatEndDayOfWeek"].ToString(): null,
                repeatBeginHour = data.Keys.Contains("repeatBeginHour") && data["repeatBeginHour"] != null ? (int?)int.Parse(data["repeatBeginHour"].ToString()) : null,
                repeatEndHour = data.Keys.Contains("repeatEndHour") && data["repeatEndHour"] != null ? (int?)int.Parse(data["repeatEndHour"].ToString()) : null,
                relativeTriggerName = data.Keys.Contains("relativeTriggerName") && data["relativeTriggerName"] != null ? data["relativeTriggerName"].ToString(): null,
                relativeDuration = data.Keys.Contains("relativeDuration") && data["relativeDuration"] != null ? (int?)int.Parse(data["relativeDuration"].ToString()) : null,
            };
        }

	}
}