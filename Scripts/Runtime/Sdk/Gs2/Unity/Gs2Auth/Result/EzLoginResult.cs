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
using Gs2.Core.Model;
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Gs2Auth.Result;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Auth.Result
{
	[Preserve]
	public class EzLoginResult
	{
        /** アクセストークン */
        public string Token { get; private set; }

        /** ユーザーID */
        public string UserId { get; private set; }

        /** 有効期限 */
        public long Expire { get; private set; }


        public EzLoginResult(
            LoginBySignatureResult result
        )
        {
            Token = result.token;
            UserId = result.userId;
            if(result.expire.HasValue)
            {
                Expire = result.expire.Value;
            }
        }
	}
}