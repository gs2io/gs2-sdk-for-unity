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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Account.Model
{
	[Preserve]
	public class EzAccount
	{
		/** アカウントID */
		public string UserId { get; set; }
		/** パスワード */
		public string Password { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }

		public EzAccount()
		{

		}

		public EzAccount(Gs2.Gs2Account.Model.Account @account)
		{
			UserId = @account.userId;
			Password = @account.password;
			CreatedAt = @account.createdAt.HasValue ? @account.createdAt.Value : 0;
		}

        public Account ToModel()
        {
            return new Account {
                userId = UserId,
                password = Password,
                createdAt = CreatedAt,
            };
        }
	}
}
