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
	public class PutResultRequest : Gs2Request<PutResultRequest>
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
        public PutResultRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** レーティング名 */
		[UnityEngine.SerializeField]
        public string ratingName;

        /**
         * レーティング名を設定
         *
         * @param ratingName レーティング名
         * @return this
         */
        public PutResultRequest WithRatingName(string ratingName) {
            this.ratingName = ratingName;
            return this;
        }


        /** 対戦結果 */
		[UnityEngine.SerializeField]
        public List<GameResult> gameResults;

        /**
         * 対戦結果を設定
         *
         * @param gameResults 対戦結果
         * @return this
         */
        public PutResultRequest WithGameResults(List<GameResult> gameResults) {
            this.gameResults = gameResults;
            return this;
        }


    	[Preserve]
        public static PutResultRequest FromDict(JsonData data)
        {
            return new PutResultRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                ratingName = data.Keys.Contains("ratingName") && data["ratingName"] != null ? data["ratingName"].ToString(): null,
                gameResults = data.Keys.Contains("gameResults") && data["gameResults"] != null ? data["gameResults"].Cast<JsonData>().Select(value =>
                    {
                        return GameResult.FromDict(value);
                    }
                ).ToList() : null,
            };
        }

	}
}