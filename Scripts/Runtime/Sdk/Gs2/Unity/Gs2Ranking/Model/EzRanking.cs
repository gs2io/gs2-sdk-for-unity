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
	public class EzRanking
	{
		/** 順位 */
		[UnityEngine.SerializeField]
		public long Rank;
		/** 1位からのインデックス */
		[UnityEngine.SerializeField]
		public long Index;
		/** ユーザID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** スコア */
		[UnityEngine.SerializeField]
		public long Score;
		/** メタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;

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

        public virtual Ranking ToModel()
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

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("rank");
            writer.Write(this.Rank);
            writer.WritePropertyName("index");
            writer.Write(this.Index);
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WritePropertyName("score");
            writer.Write(this.Score);
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WriteObjectEnd();
        }
	}
}
