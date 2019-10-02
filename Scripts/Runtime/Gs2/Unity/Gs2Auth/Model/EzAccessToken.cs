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
using Gs2.Gs2Auth.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Auth.Model
{
	[Preserve]
	public class EzAccessToken
	{
		/** アクセストークン */
		public string Token { get; set; }
		/** ユーザーID */
		public string UserId { get; set; }
		/** 有効期限 */
		public long Expire { get; set; }

		public EzAccessToken()
		{

		}

		public EzAccessToken(Gs2.Gs2Auth.Model.AccessToken @accessToken)
		{
			Token = @accessToken.token;
			UserId = @accessToken.userId;
			Expire = @accessToken.expire.HasValue ? @accessToken.expire.Value : 0;
		}

        public AccessToken ToModel()
        {
            return new AccessToken {
                token = Token,
                userId = UserId,
                expire = Expire,
            };
        }
	}
}
