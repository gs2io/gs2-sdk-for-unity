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
using Gs2.Gs2Identifier.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Identifier.Result
{
	[Preserve]
	public class LoginResult
	{
        /** プロジェクトトークン */
        public string accessToken { set; get; }

        /** Bearer */
        public string tokenType { set; get; }

        /** 有効期間(秒) */
        public int? expiresIn { set; get; }


    	[Preserve]
        public static LoginResult FromDict(JsonData data)
        {
            return new LoginResult {
                accessToken = data.Keys.Contains("accessToken") && data["accessToken"] != null ? data["accessToken"].ToString() : null,
                tokenType = data.Keys.Contains("tokenType") && data["tokenType"] != null ? data["tokenType"].ToString() : null,
                expiresIn = data.Keys.Contains("expiresIn") && data["expiresIn"] != null ? (int?)int.Parse(data["expiresIn"].ToString()) : null,
            };
        }
	}
}