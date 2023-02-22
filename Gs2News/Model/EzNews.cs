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
using Gs2.Gs2News.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2News.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzNews
	{
		[SerializeField]
		public string Section;
		[SerializeField]
		public string Content;
		[SerializeField]
		public string Title;
		[SerializeField]
		public string ScheduleEventId;
		[SerializeField]
		public long Timestamp;
		[SerializeField]
		public string FrontMatter;

        public Gs2.Gs2News.Model.News ToModel()
        {
            return new Gs2.Gs2News.Model.News {
                Section = Section,
                Content = Content,
                Title = Title,
                ScheduleEventId = ScheduleEventId,
                Timestamp = Timestamp,
                FrontMatter = FrontMatter,
            };
        }

        public static EzNews FromModel(Gs2.Gs2News.Model.News model)
        {
            return new EzNews {
                Section = model.Section == null ? null : model.Section,
                Content = model.Content == null ? null : model.Content,
                Title = model.Title == null ? null : model.Title,
                ScheduleEventId = model.ScheduleEventId == null ? null : model.ScheduleEventId,
                Timestamp = model.Timestamp ?? 0,
                FrontMatter = model.FrontMatter == null ? null : model.FrontMatter,
            };
        }
    }
}