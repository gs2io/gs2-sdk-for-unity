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


namespace Gs2.Unity.Gs2Ranking.Model
{
	public class EzSubscribe
	{
		/** カテゴリ名 */
		public string CategoryName { get; set; }
		/** 購読するユーザID */
		public string UserId { get; set; }
		/** 購読されるユーザIDリスト */
		public List<string> TargetUserIds { get; set; }

		public EzSubscribe()
		{

		}

		public EzSubscribe(Gs2.Gs2Ranking.Model.Subscribe @subscribe)
		{
			CategoryName = @subscribe.categoryName;
			UserId = @subscribe.userId;
			TargetUserIds = @subscribe.targetUserIds != null ? @subscribe.targetUserIds.Select(value =>
                {
                    return value;
                }
			).ToList() : new List<string>(new string[] {});
		}

        public Subscribe ToModel()
        {
            return new Subscribe {
                categoryName = CategoryName,
                userId = UserId,
                targetUserIds = TargetUserIds != null ? TargetUserIds.Select(Value0 =>
                        {
                            return Value0;
                        }
                ).ToList() : new List<string>(new string[] {}),
            };
        }
	}
}
