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
	public class EzBallot
	{
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;
		/** レーティング計算に使用するレーティング名 */
		[UnityEngine.SerializeField]
		public string RatingName;
		/** 投票対象のギャザリング名 */
		[UnityEngine.SerializeField]
		public string GatheringName;
		/** 参加人数 */
		[UnityEngine.SerializeField]
		public int NumberOfPlayer;

		public EzBallot()
		{

		}

		public EzBallot(Gs2.Gs2Matchmaking.Model.Ballot @ballot)
		{
			UserId = @ballot.userId;
			RatingName = @ballot.ratingName;
			GatheringName = @ballot.gatheringName;
			NumberOfPlayer = @ballot.numberOfPlayer.HasValue ? @ballot.numberOfPlayer.Value : 0;
		}

        public virtual Ballot ToModel()
        {
            return new Ballot {
                userId = UserId,
                ratingName = RatingName,
                gatheringName = GatheringName,
                numberOfPlayer = NumberOfPlayer,
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
            if(this.RatingName != null)
            {
                writer.WritePropertyName("ratingName");
                writer.Write(this.RatingName);
            }
            if(this.GatheringName != null)
            {
                writer.WritePropertyName("gatheringName");
                writer.Write(this.GatheringName);
            }
            writer.WritePropertyName("numberOfPlayer");
            writer.Write(this.NumberOfPlayer);
            writer.WriteObjectEnd();
        }
	}
}
