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
using Gs2.Gs2Exchange.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Exchange.Result
{
	[Preserve]
	public class CreateAwaitByUserIdResult
	{
        /** 交換待機 */
        public Await item { set; get; }

        /** 取得できるようになる日時 */
        public long? unlockAt { set; get; }


    	[Preserve]
        public static CreateAwaitByUserIdResult FromDict(JsonData data)
        {
            return new CreateAwaitByUserIdResult {
                item = data.Keys.Contains("item") && data["item"] != null ? Gs2.Gs2Exchange.Model.Await.FromDict(data["item"]) : null,
                unlockAt = data.Keys.Contains("unlockAt") && data["unlockAt"] != null ? (long?)long.Parse(data["unlockAt"].ToString()) : null,
            };
        }
	}
}