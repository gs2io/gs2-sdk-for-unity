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
using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Matchmaking.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Matchmaking.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateRatingModelMasterRequest : Gs2Request<UpdateRatingModelMasterRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateRatingModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** レーティングの種類名 */
		[UnityEngine.SerializeField]
        public string ratingName;

        /**
         * レーティングの種類名を設定
         *
         * @param ratingName レーティングの種類名
         * @return this
         */
        public UpdateRatingModelMasterRequest WithRatingName(string ratingName) {
            this.ratingName = ratingName;
            return this;
        }


        /** レーティングモデルマスターの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * レーティングモデルマスターの説明を設定
         *
         * @param description レーティングモデルマスターの説明
         * @return this
         */
        public UpdateRatingModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** レーティングの種類のメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * レーティングの種類のメタデータを設定
         *
         * @param metadata レーティングの種類のメタデータ
         * @return this
         */
        public UpdateRatingModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** レート値の変動の大きさ */
		[UnityEngine.SerializeField]
        public int? volatility;

        /**
         * レート値の変動の大きさを設定
         *
         * @param volatility レート値の変動の大きさ
         * @return this
         */
        public UpdateRatingModelMasterRequest WithVolatility(int? volatility) {
            this.volatility = volatility;
            return this;
        }


    	[Preserve]
        public static UpdateRatingModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateRatingModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                ratingName = data.Keys.Contains("ratingName") && data["ratingName"] != null ? data["ratingName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                volatility = data.Keys.Contains("volatility") && data["volatility"] != null ? (int?)int.Parse(data["volatility"].ToString()) : null,
            };
        }

	}
}