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
	public class CommitVoteRequest : Gs2Request<CommitVoteRequest>
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
        public CommitVoteRequest WithNamespaceName(string namespaceName) {
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
        public CommitVoteRequest WithRatingName(string ratingName) {
            this.ratingName = ratingName;
            return this;
        }


        /** 投票対象のギャザリング名 */
		[UnityEngine.SerializeField]
        public string gatheringName;

        /**
         * 投票対象のギャザリング名を設定
         *
         * @param gatheringName 投票対象のギャザリング名
         * @return this
         */
        public CommitVoteRequest WithGatheringName(string gatheringName) {
            this.gatheringName = gatheringName;
            return this;
        }


    	[Preserve]
        public static CommitVoteRequest FromDict(JsonData data)
        {
            return new CommitVoteRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                ratingName = data.Keys.Contains("ratingName") && data["ratingName"] != null ? data["ratingName"].ToString(): null,
                gatheringName = data.Keys.Contains("gatheringName") && data["gatheringName"] != null ? data["gatheringName"].ToString(): null,
            };
        }

	}
}