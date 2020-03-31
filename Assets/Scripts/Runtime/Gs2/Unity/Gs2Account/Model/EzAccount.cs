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
using Gs2.Gs2Account.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Account.Model
{
	[Preserve]
	[System.Serializable]
	public class EzAccount
	{
		/** アカウントID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** パスワード */
		[UnityEngine.SerializeField]
		public string Password;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;

		public EzAccount()
		{

		}

		public EzAccount(Gs2.Gs2Account.Model.Account @account)
		{
			UserId = @account.userId;
			Password = @account.password;
			CreatedAt = @account.createdAt.HasValue ? @account.createdAt.Value : 0;
		}

        public virtual Account ToModel()
        {
            return new Account {
                userId = UserId,
                password = Password,
                createdAt = CreatedAt,
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
            if(this.Password != null)
            {
                writer.WritePropertyName("password");
                writer.Write(this.Password);
            }
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WriteObjectEnd();
        }
	}
}
