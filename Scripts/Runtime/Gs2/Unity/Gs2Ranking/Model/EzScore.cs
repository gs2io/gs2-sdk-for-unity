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
	public class EzScore
	{
		/** カテゴリ名 */
		[UnityEngine.SerializeField]
		public string CategoryName;
		/** ユーザID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** スコアを獲得したユーザID */
		[UnityEngine.SerializeField]
		public string ScorerUserId;
		/** スコア */
		[UnityEngine.SerializeField]
		public long Score;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;

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

        public virtual Score ToModel()
        {
            return new Score {
                categoryName = CategoryName,
                userId = UserId,
                scorerUserId = ScorerUserId,
                score = Score,
                metadata = Metadata,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.CategoryName != null)
            {
                writer.WritePropertyName("categoryName");
                writer.Write(this.CategoryName);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            if(this.ScorerUserId != null)
            {
                writer.WritePropertyName("scorerUserId");
                writer.Write(this.ScorerUserId);
            }
            writer.WritePropertyName("score");
            writer.Write(this.Score);
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WriteObjectEnd();
        }
	}
}
