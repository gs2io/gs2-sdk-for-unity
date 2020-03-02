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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Auth.Model
{
	[Preserve]
	[System.Serializable]
	public class EzAccessToken
	{
		/** アクセストークン */
		[UnityEngine.SerializeField]
		public string Token;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** 有効期限 */
		[UnityEngine.SerializeField]
		public long Expire;

		public EzAccessToken()
		{

		}

		public EzAccessToken(Gs2.Gs2Auth.Model.AccessToken @accessToken)
		{
			Token = @accessToken.token;
			UserId = @accessToken.userId;
			Expire = @accessToken.expire.HasValue ? @accessToken.expire.Value : 0;
		}

        public virtual AccessToken ToModel()
        {
            return new AccessToken {
                token = Token,
                userId = UserId,
                expire = Expire,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Token != null)
            {
                writer.WritePropertyName("token");
                writer.Write(this.Token);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WritePropertyName("expire");
            writer.Write(this.Expire);
            writer.WriteObjectEnd();
        }
	}
}
