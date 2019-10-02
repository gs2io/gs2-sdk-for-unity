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
	public class EzTakeOver
	{
		/** ユーザーID */
		public string UserId { get; set; }
		/** スロット番号 */
		public int Type { get; set; }
		/** 引き継ぎ用ユーザーID */
		public string UserIdentifier { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }

		public EzTakeOver()
		{

		}

		public EzTakeOver(Gs2.Gs2Account.Model.TakeOver @takeOver)
		{
			UserId = @takeOver.userId;
			Type = @takeOver.type.HasValue ? @takeOver.type.Value : 0;
			UserIdentifier = @takeOver.userIdentifier;
			CreatedAt = @takeOver.createdAt.HasValue ? @takeOver.createdAt.Value : 0;
		}

        public TakeOver ToModel()
        {
            return new TakeOver {
                userId = UserId,
                type = Type,
                userIdentifier = UserIdentifier,
                createdAt = CreatedAt,
            };
        }
	}
}
