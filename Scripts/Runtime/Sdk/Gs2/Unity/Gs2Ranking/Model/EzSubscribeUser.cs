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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Ranking.Model
{
	[Preserve]
	[System.Serializable]
	public class EzSubscribeUser
	{
		/** 購読するユーザID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** 購読されるユーザID */
		[UnityEngine.SerializeField]
		public string TargetUserId;

		public EzSubscribeUser()
		{

		}

		public EzSubscribeUser(Gs2.Gs2Ranking.Model.SubscribeUser @subscribeUser)
		{
			UserId = @subscribeUser.userId;
			TargetUserId = @subscribeUser.targetUserId;
		}

        public virtual SubscribeUser ToModel()
        {
            return new SubscribeUser {
                userId = UserId,
                targetUserId = TargetUserId,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            if(this.TargetUserId != null)
            {
                writer.WritePropertyName("targetUserId");
                writer.Write(this.TargetUserId);
            }
            writer.WriteObjectEnd();
        }
	}
}
