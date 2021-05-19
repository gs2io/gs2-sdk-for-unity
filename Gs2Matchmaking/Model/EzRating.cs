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
using Gs2.Gs2Matchmaking.Model;
using System.Collections.Generic;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Matchmaking.Model
{
	[Preserve]
	[System.Serializable]
	public class EzRating
	{
		/** レーティング */
		[UnityEngine.SerializeField]
		public string RatingId;
		/** レーティング名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** None */
		[UnityEngine.SerializeField]
		public float RateValue;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long CreatedAt;
		/** 最終更新日時 */
		[UnityEngine.SerializeField]
		public long UpdatedAt;

		public EzRating()
		{

		}

		public EzRating(Gs2.Gs2Matchmaking.Model.Rating @rating)
		{
			RatingId = @rating.ratingId;
			Name = @rating.name;
			UserId = @rating.userId;
			RateValue = @rating.rateValue.HasValue ? @rating.rateValue.Value : 0;
			CreatedAt = @rating.createdAt.HasValue ? @rating.createdAt.Value : 0;
			UpdatedAt = @rating.updatedAt.HasValue ? @rating.updatedAt.Value : 0;
		}

        public virtual Rating ToModel()
        {
            return new Rating {
                ratingId = RatingId,
                name = Name,
                userId = UserId,
                rateValue = RateValue,
                createdAt = CreatedAt,
                updatedAt = UpdatedAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.RatingId != null)
            {
                writer.WritePropertyName("ratingId");
                writer.Write(this.RatingId);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WritePropertyName("rateValue");
            writer.Write(this.RateValue);
            writer.WritePropertyName("createdAt");
            writer.Write(this.CreatedAt);
            writer.WritePropertyName("updatedAt");
            writer.Write(this.UpdatedAt);
            writer.WriteObjectEnd();
        }
	}
}
