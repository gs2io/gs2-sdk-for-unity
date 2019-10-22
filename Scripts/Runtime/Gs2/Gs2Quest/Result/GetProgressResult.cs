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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Result
{
	[Preserve]
	public class GetProgressResult
	{
        /** クエスト挑戦 */
        public Progress item { set; get; }

        /** クエストグループ */
        public QuestGroupModel questGroup { set; get; }

        /** クエストモデル */
        public QuestModel quest { set; get; }


    	[Preserve]
        public static GetProgressResult FromDict(JsonData data)
        {
            return new GetProgressResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Progress.FromDict(data["item"]) : null,
                questGroup = data.Keys.Contains("questGroup") && data["questGroup"] != null ? QuestGroupModel.FromDict(data["questGroup"]) : null,
                quest = data.Keys.Contains("quest") && data["quest"] != null ? QuestModel.FromDict(data["quest"]) : null,
            };
        }
	}
}