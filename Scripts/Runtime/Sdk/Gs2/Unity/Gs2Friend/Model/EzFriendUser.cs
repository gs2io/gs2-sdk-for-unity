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
using Gs2.Gs2Friend.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Friend.Model
{
	[Preserve]
	[System.Serializable]
	public class EzFriendUser
	{
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** 公開されるプロフィール */
		[UnityEngine.SerializeField]
		public string PublicProfile;
		/** フレンド向けに公開されるプロフィール */
		[UnityEngine.SerializeField]
		public string FriendProfile;

		public EzFriendUser()
		{

		}

		public EzFriendUser(Gs2.Gs2Friend.Model.FriendUser @friendUser)
		{
			UserId = @friendUser.userId;
			PublicProfile = @friendUser.publicProfile;
			FriendProfile = @friendUser.friendProfile;
		}

        public virtual FriendUser ToModel()
        {
            return new FriendUser {
                userId = UserId,
                publicProfile = PublicProfile,
                friendProfile = FriendProfile,
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
            if(this.PublicProfile != null)
            {
                writer.WritePropertyName("publicProfile");
                writer.Write(this.PublicProfile);
            }
            if(this.FriendProfile != null)
            {
                writer.WritePropertyName("friendProfile");
                writer.Write(this.FriendProfile);
            }
            writer.WriteObjectEnd();
        }
	}
}
