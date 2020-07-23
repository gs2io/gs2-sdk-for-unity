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
	public class VoteMultipleRequest : Gs2Request<VoteMultipleRequest>
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
        public VoteMultipleRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 署名付の投票用紙リスト */
		[UnityEngine.SerializeField]
        public List<SignedBallot> signedBallots;

        /**
         * 署名付の投票用紙リストを設定
         *
         * @param signedBallots 署名付の投票用紙リスト
         * @return this
         */
        public VoteMultipleRequest WithSignedBallots(List<SignedBallot> signedBallots) {
            this.signedBallots = signedBallots;
            return this;
        }


        /** 投票内容。対戦を行ったプレイヤーグループ1に所属するユーザIDのリスト */
		[UnityEngine.SerializeField]
        public List<GameResult> gameResults;

        /**
         * 投票内容。対戦を行ったプレイヤーグループ1に所属するユーザIDのリストを設定
         *
         * @param gameResults 投票内容。対戦を行ったプレイヤーグループ1に所属するユーザIDのリスト
         * @return this
         */
        public VoteMultipleRequest WithGameResults(List<GameResult> gameResults) {
            this.gameResults = gameResults;
            return this;
        }


        /** 投票用紙の署名検証に使用する暗号鍵 のGRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * 投票用紙の署名検証に使用する暗号鍵 のGRNを設定
         *
         * @param keyId 投票用紙の署名検証に使用する暗号鍵 のGRN
         * @return this
         */
        public VoteMultipleRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


    	[Preserve]
        public static VoteMultipleRequest FromDict(JsonData data)
        {
            return new VoteMultipleRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                signedBallots = data.Keys.Contains("signedBallots") && data["signedBallots"] != null ? data["signedBallots"].Cast<JsonData>().Select(value =>
                    {
                        return SignedBallot.FromDict(value);
                    }
                ).ToList() : null,
                gameResults = data.Keys.Contains("gameResults") && data["gameResults"] != null ? data["gameResults"].Cast<JsonData>().Select(value =>
                    {
                        return GameResult.FromDict(value);
                    }
                ).ToList() : null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
            };
        }

	}
}