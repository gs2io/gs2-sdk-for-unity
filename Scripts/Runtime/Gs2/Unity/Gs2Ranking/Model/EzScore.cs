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
	public class EzScore
	{
		/** カテゴリ名 */
		public string CategoryName { get; set; }
		/** ユーザID */
		public string UserId { get; set; }
		/** スコアを獲得したユーザID */
		public string ScorerUserId { get; set; }
		/** スコア */
		public long Score { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }

		public EzScore()
		{

		}

		public EzScore(Gs2.Gs2Ranking.Model.Score @score)
		{
			CategoryName = @score.categoryName;
			UserId = @score.userId;
			ScorerUserId = @score.scorerUserId;
			Score = @score.score.HasValue ? @score.score.Value : 0;
			Metadata = @score.metadata;
		}

        public Score ToModel()
        {
            return new Score {
                categoryName = CategoryName,
                userId = UserId,
                scorerUserId = ScorerUserId,
                score = Score,
                metadata = Metadata,
            };
        }
	}
}
