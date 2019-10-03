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
using Gs2.Gs2Auth.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Auth.Result
{
	[Preserve]
	public class LoginResult
	{
        /** アクセストークン */
        public string token { set; get; }

        /** ユーザーID */
        public string userId { set; get; }

        /** 有効期限 */
        public long? expire { set; get; }


    	[Preserve]
        public static LoginResult FromDict(JsonData data)
        {
            return new LoginResult {
                token = data.Keys.Contains("token") && data["token"] != null ? (string) data["token"] : null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? (string) data["userId"] : null,
                expire = data.Keys.Contains("expire") && data["expire"] != null ? (long?) data["expire"] : null,
            };
        }
	}
}