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
using Gs2.Gs2Limit.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Limit.Result
{
	[Preserve]
	public class CountUpByStampTaskResult
	{
        /** カウントを増やしたカウンター */
        public Counter item { set; get; }

        /** スタンプタスクの実行結果を記録したコンテキスト */
        public string newContextStack { set; get; }


    	[Preserve]
        public static CountUpByStampTaskResult FromDict(JsonData data)
        {
            return new CountUpByStampTaskResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Counter.FromDict(data["item"]) : null,
                newContextStack = data.Keys.Contains("newContextStack") && data["newContextStack"] != null ? data["newContextStack"].ToString() : null,
            };
        }
	}
}