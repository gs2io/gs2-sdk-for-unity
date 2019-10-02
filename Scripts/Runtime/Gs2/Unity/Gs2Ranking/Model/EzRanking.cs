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
	public class EzRanking
	{
		/** 順位 */
		public long Rank { get; set; }
		/** 1位からのインデックス */
		public long Index { get; set; }
		/** ユーザID */
		public string UserId { get; set; }
		/** スコア */
		public long Score { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }

		public EzRanking()
		{

		}

		public EzRanking(Gs2.Gs2Ranking.Model.Ranking @ranking)
		{
			Rank = @ranking.rank.HasValue ? @ranking.rank.Value : 0;
			Index = @ranking.index.HasValue ? @ranking.index.Value : 0;
			UserId = @ranking.userId;
			Score = @ranking.score.HasValue ? @ranking.score.Value : 0;
			Metadata = @ranking.metadata;
			CreatedAt = @ranking.createdAt.HasValue ? @ranking.createdAt.Value : 0;
		}

        public Ranking ToModel()
        {
            return new Ranking {
                rank = Rank,
                index = Index,
                userId = UserId,
                score = Score,
                metadata = Metadata,
                createdAt = CreatedAt,
            };
        }
	}
}
