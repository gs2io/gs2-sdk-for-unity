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
	public class EzGameResult
	{
		/** 順位 */
		[UnityEngine.SerializeField]
		public int Rank;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;

		public EzGameResult()
		{

		}

		public EzGameResult(Gs2.Gs2Matchmaking.Model.GameResult @gameResult)
		{
			Rank = @gameResult.rank.HasValue ? @gameResult.rank.Value : 0;
			UserId = @gameResult.userId;
		}

        public virtual GameResult ToModel()
        {
            return new GameResult {
                rank = Rank,
                userId = UserId,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            writer.WritePropertyName("rank");
            writer.Write(this.Rank);
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WriteObjectEnd();
        }
	}
}
