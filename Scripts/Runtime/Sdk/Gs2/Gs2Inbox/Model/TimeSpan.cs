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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Model
{
	[Preserve]
	public class TimeSpan
	{

        /** 現在時刻からの日数 */
        public int? days { set; get; }

        /**
         * 現在時刻からの日数を設定
         *
         * @param days 現在時刻からの日数
         * @return this
         */
        public TimeSpan WithDays(int? days) {
            this.days = days;
            return this;
        }

        /** 現在時刻からの時間 */
        public int? hours { set; get; }

        /**
         * 現在時刻からの時間を設定
         *
         * @param hours 現在時刻からの時間
         * @return this
         */
        public TimeSpan WithHours(int? hours) {
            this.hours = hours;
            return this;
        }

        /** 現在時刻からの分 */
        public int? minutes { set; get; }

        /**
         * 現在時刻からの分を設定
         *
         * @param minutes 現在時刻からの分
         * @return this
         */
        public TimeSpan WithMinutes(int? minutes) {
            this.minutes = minutes;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.days.HasValue)
            {
                writer.WritePropertyName("days");
                writer.Write(this.days.Value);
            }
            if(this.hours.HasValue)
            {
                writer.WritePropertyName("hours");
                writer.Write(this.hours.Value);
            }
            if(this.minutes.HasValue)
            {
                writer.WritePropertyName("minutes");
                writer.Write(this.minutes.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static TimeSpan FromDict(JsonData data)
        {
            return new TimeSpan()
                .WithDays(data.Keys.Contains("days") && data["days"] != null ? (int?)int.Parse(data["days"].ToString()) : null)
                .WithHours(data.Keys.Contains("hours") && data["hours"] != null ? (int?)int.Parse(data["hours"].ToString()) : null)
                .WithMinutes(data.Keys.Contains("minutes") && data["minutes"] != null ? (int?)int.Parse(data["minutes"].ToString()) : null);
        }
	}
}