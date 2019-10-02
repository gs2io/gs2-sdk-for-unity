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
using Gs2.Gs2Quest.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Quest.Model
{
	[Preserve]
	public class EzReward
	{
		/** スタンプシートで実行するアクションの種類 */
		public string Action { get; set; }
		/** リクエストモデル */
		public string Request { get; set; }
		/** 入手するリソースGRN */
		public string ItemId { get; set; }
		/** 入手する数量 */
		public int Value { get; set; }

		public EzReward()
		{

		}

		public EzReward(Gs2.Gs2Quest.Model.Reward @reward)
		{
			Action = @reward.action;
			Request = @reward.request;
			ItemId = @reward.itemId;
			Value = @reward.value.HasValue ? @reward.value.Value : 0;
		}

        public Reward ToModel()
        {
            return new Reward {
                action = Action,
                request = Request,
                itemId = ItemId,
                value = Value,
            };
        }
	}
}
