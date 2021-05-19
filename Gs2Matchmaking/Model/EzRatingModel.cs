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
	public class EzRatingModel
	{
		/** レーティングの種類名 */
		[UnityEngine.SerializeField]
		public string Name;
		/** レーティングの種類のメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** レート値の変動の大きさ */
		[UnityEngine.SerializeField]
		public int Volatility;

		public EzRatingModel()
		{

		}

		public EzRatingModel(Gs2.Gs2Matchmaking.Model.RatingModel @ratingModel)
		{
			Name = @ratingModel.name;
			Metadata = @ratingModel.metadata;
			Volatility = @ratingModel.volatility.HasValue ? @ratingModel.volatility.Value : 0;
		}

        public virtual RatingModel ToModel()
        {
            return new RatingModel {
                name = Name,
                metadata = Metadata,
                volatility = Volatility,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WritePropertyName("volatility");
            writer.Write(this.Volatility);
            writer.WriteObjectEnd();
        }
	}
}
