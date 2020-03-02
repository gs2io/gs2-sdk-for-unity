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
using Gs2.Gs2News.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2News.Model
{
	[Preserve]
	[System.Serializable]
	public class EzNews
	{
		/** セクション名 */
		[UnityEngine.SerializeField]
		public string Section;
		/** コンテンツ名 */
		[UnityEngine.SerializeField]
		public string Content;
		/** 記事見出し */
		[UnityEngine.SerializeField]
		public string Title;
		/** 配信期間を決定する GS2-Schedule のイベントID */
		[UnityEngine.SerializeField]
		public string ScheduleEventId;
		/** タイムスタンプ */
		[UnityEngine.SerializeField]
		public long Timestamp;
		/** Front Matter */
		[UnityEngine.SerializeField]
		public string FrontMatter;

		public EzNews()
		{

		}

		public EzNews(Gs2.Gs2News.Model.News @news)
		{
			Section = @news.section;
			Content = @news.content;
			Title = @news.title;
			ScheduleEventId = @news.scheduleEventId;
			Timestamp = @news.timestamp.HasValue ? @news.timestamp.Value : 0;
			FrontMatter = @news.frontMatter;
		}

        public virtual News ToModel()
        {
            return new News {
                section = Section,
                content = Content,
                title = Title,
                scheduleEventId = ScheduleEventId,
                timestamp = Timestamp,
                frontMatter = FrontMatter,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Section != null)
            {
                writer.WritePropertyName("section");
                writer.Write(this.Section);
            }
            if(this.Content != null)
            {
                writer.WritePropertyName("content");
                writer.Write(this.Content);
            }
            if(this.Title != null)
            {
                writer.WritePropertyName("title");
                writer.Write(this.Title);
            }
            if(this.ScheduleEventId != null)
            {
                writer.WritePropertyName("scheduleEventId");
                writer.Write(this.ScheduleEventId);
            }
            writer.WritePropertyName("timestamp");
            writer.Write(this.Timestamp);
            if(this.FrontMatter != null)
            {
                writer.WritePropertyName("frontMatter");
                writer.Write(this.FrontMatter);
            }
            writer.WriteObjectEnd();
        }
	}
}
