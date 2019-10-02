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
using Gs2.Gs2Ranking.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Ranking.Model
{
	[Preserve]
	public class EzSubscribeUser
	{
		/** 購読するユーザID */
		public string UserId { get; set; }
		/** 購読されるユーザID */
		public string TargetUserId { get; set; }

		public EzSubscribeUser()
		{

		}

		public EzSubscribeUser(Gs2.Gs2Ranking.Model.SubscribeUser @subscribeUser)
		{
			UserId = @subscribeUser.userId;
			TargetUserId = @subscribeUser.targetUserId;
		}

        public SubscribeUser ToModel()
        {
            return new SubscribeUser {
                userId = UserId,
                targetUserId = TargetUserId,
            };
        }
	}
}
