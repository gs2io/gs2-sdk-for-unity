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

namespace Gs2.Gs2News.Model
{
	[Preserve]
	public class News
	{

        /** セクション名 */
        public string section { set; get; }

        /**
         * セクション名を設定
         *
         * @param section セクション名
         * @return this
         */
        public News WithSection(string section) {
            this.section = section;
            return this;
        }

        /** コンテンツ名 */
        public string content { set; get; }

        /**
         * コンテンツ名を設定
         *
         * @param content コンテンツ名
         * @return this
         */
        public News WithContent(string content) {
            this.content = content;
            return this;
        }

        /** 記事見出し */
        public string title { set; get; }

        /**
         * 記事見出しを設定
         *
         * @param title 記事見出し
         * @return this
         */
        public News WithTitle(string title) {
            this.title = title;
            return this;
        }

        /** 配信期間を決定する GS2-Schedule のイベントID */
        public string scheduleEventId { set; get; }

        /**
         * 配信期間を決定する GS2-Schedule のイベントIDを設定
         *
         * @param scheduleEventId 配信期間を決定する GS2-Schedule のイベントID
         * @return this
         */
        public News WithScheduleEventId(string scheduleEventId) {
            this.scheduleEventId = scheduleEventId;
            return this;
        }

        /** タイムスタンプ */
        public long? timestamp { set; get; }

        /**
         * タイムスタンプを設定
         *
         * @param timestamp タイムスタンプ
         * @return this
         */
        public News WithTimestamp(long? timestamp) {
            this.timestamp = timestamp;
            return this;
        }

        /** Front Matter */
        public string frontMatter { set; get; }

        /**
         * Front Matterを設定
         *
         * @param frontMatter Front Matter
         * @return this
         */
        public News WithFrontMatter(string frontMatter) {
            this.frontMatter = frontMatter;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.section != null)
            {
                writer.WritePropertyName("section");
                writer.Write(this.section);
            }
            if(this.content != null)
            {
                writer.WritePropertyName("content");
                writer.Write(this.content);
            }
            if(this.title != null)
            {
                writer.WritePropertyName("title");
                writer.Write(this.title);
            }
            if(this.scheduleEventId != null)
            {
                writer.WritePropertyName("scheduleEventId");
                writer.Write(this.scheduleEventId);
            }
            if(this.timestamp.HasValue)
            {
                writer.WritePropertyName("timestamp");
                writer.Write(this.timestamp.Value);
            }
            if(this.frontMatter != null)
            {
                writer.WritePropertyName("frontMatter");
                writer.Write(this.frontMatter);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static News FromDict(JsonData data)
        {
            return new News()
                .WithSection(data.Keys.Contains("section") && data["section"] != null ? data["section"].ToString() : null)
                .WithContent(data.Keys.Contains("content") && data["content"] != null ? data["content"].ToString() : null)
                .WithTitle(data.Keys.Contains("title") && data["title"] != null ? data["title"].ToString() : null)
                .WithScheduleEventId(data.Keys.Contains("scheduleEventId") && data["scheduleEventId"] != null ? data["scheduleEventId"].ToString() : null)
                .WithTimestamp(data.Keys.Contains("timestamp") && data["timestamp"] != null ? (long?)long.Parse(data["timestamp"].ToString()) : null)
                .WithFrontMatter(data.Keys.Contains("frontMatter") && data["frontMatter"] != null ? data["frontMatter"].ToString() : null);
        }
	}
}