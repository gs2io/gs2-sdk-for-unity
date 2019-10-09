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

namespace Gs2.Gs2Deploy.Model
{
	[Preserve]
	public class Event
	{

        /** 発生したイベント */
        public string eventId { set; get; }

        /**
         * 発生したイベントを設定
         *
         * @param eventId 発生したイベント
         * @return this
         */
        public Event WithEventId(string eventId) {
            this.eventId = eventId;
            return this;
        }

        /** イベント名 */
        public string name { set; get; }

        /**
         * イベント名を設定
         *
         * @param name イベント名
         * @return this
         */
        public Event WithName(string name) {
            this.name = name;
            return this;
        }

        /** イベントの種類 */
        public string resourceName { set; get; }

        /**
         * イベントの種類を設定
         *
         * @param resourceName イベントの種類
         * @return this
         */
        public Event WithResourceName(string resourceName) {
            this.resourceName = resourceName;
            return this;
        }

        /** イベントの種類 */
        public string type { set; get; }

        /**
         * イベントの種類を設定
         *
         * @param type イベントの種類
         * @return this
         */
        public Event WithType(string type) {
            this.type = type;
            return this;
        }

        /** メッセージ */
        public string message { set; get; }

        /**
         * メッセージを設定
         *
         * @param message メッセージ
         * @return this
         */
        public Event WithMessage(string message) {
            this.message = message;
            return this;
        }

        /** 日時 */
        public long? eventAt { set; get; }

        /**
         * 日時を設定
         *
         * @param eventAt 日時
         * @return this
         */
        public Event WithEventAt(long? eventAt) {
            this.eventAt = eventAt;
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
            if(this.resourceName != null)
            {
                writer.WritePropertyName("resourceName");
                writer.Write(this.resourceName);
            }
            if(this.type != null)
            {
                writer.WritePropertyName("type");
                writer.Write(this.type);
            }
            if(this.message != null)
            {
                writer.WritePropertyName("message");
                writer.Write(this.message);
            }
            if(this.eventAt.HasValue)
            {
                writer.WritePropertyName("eventAt");
                writer.Write(this.eventAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Event FromDict(JsonData data)
        {
            return new Event()
                .WithEventId(data.Keys.Contains("eventId") && data["eventId"] != null ? data["eventId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithResourceName(data.Keys.Contains("resourceName") && data["resourceName"] != null ? data["resourceName"].ToString() : null)
                .WithType(data.Keys.Contains("type") && data["type"] != null ? data["type"].ToString() : null)
                .WithMessage(data.Keys.Contains("message") && data["message"] != null ? data["message"].ToString() : null)
                .WithEventAt(data.Keys.Contains("eventAt") && data["eventAt"] != null ? (long?)long.Parse(data["eventAt"].ToString()) : null);
        }
	}
}