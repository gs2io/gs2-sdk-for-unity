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
	public class VoteRequest : Gs2Request<VoteRequest>
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
        public VoteRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 投票用紙の署名対象のデータ */
		[UnityEngine.SerializeField]
        public string ballotBody;

        /**
         * 投票用紙の署名対象のデータを設定
         *
         * @param ballotBody 投票用紙の署名対象のデータ
         * @return this
         */
        public VoteRequest WithBallotBody(string ballotBody) {
            this.ballotBody = ballotBody;
            return this;
        }


        /** 投票用紙の署名 */
		[UnityEngine.SerializeField]
        public string ballotSignature;

        /**
         * 投票用紙の署名を設定
         *
         * @param ballotSignature 投票用紙の署名
         * @return this
         */
        public VoteRequest WithBallotSignature(string ballotSignature) {
            this.ballotSignature = ballotSignature;
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
        public VoteRequest WithGameResults(List<GameResult> gameResults) {
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
        public VoteRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


    	[Preserve]
        public static VoteRequest FromDict(JsonData data)
        {
            return new VoteRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                ballotBody = data.Keys.Contains("ballotBody") && data["ballotBody"] != null ? data["ballotBody"].ToString(): null,
                ballotSignature = data.Keys.Contains("ballotSignature") && data["ballotSignature"] != null ? data["ballotSignature"].ToString(): null,
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