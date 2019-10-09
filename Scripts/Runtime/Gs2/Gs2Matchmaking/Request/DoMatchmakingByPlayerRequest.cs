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
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Matchmaking.Request
{
	[Preserve]
	public class DoMatchmakingByPlayerRequest : Gs2Request<DoMatchmakingByPlayerRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DoMatchmakingByPlayerRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** プレイヤー情報 */
        public Player player { set; get; }

        /**
         * プレイヤー情報を設定
         *
         * @param player プレイヤー情報
         * @return this
         */
        public DoMatchmakingByPlayerRequest WithPlayer(Player player) {
            this.player = player;
            return this;
        }


        /** 検索の再開に使用する マッチメイキングの状態を保持するトークン */
        public string matchmakingContextToken { set; get; }

        /**
         * 検索の再開に使用する マッチメイキングの状態を保持するトークンを設定
         *
         * @param matchmakingContextToken 検索の再開に使用する マッチメイキングの状態を保持するトークン
         * @return this
         */
        public DoMatchmakingByPlayerRequest WithMatchmakingContextToken(string matchmakingContextToken) {
            this.matchmakingContextToken = matchmakingContextToken;
            return this;
        }


    	[Preserve]
        public static DoMatchmakingByPlayerRequest FromDict(JsonData data)
        {
            return new DoMatchmakingByPlayerRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                player = data.Keys.Contains("player") && data["player"] != null ? Player.FromDict(data["player"]) : null,
                matchmakingContextToken = data.Keys.Contains("matchmakingContextToken") && data["matchmakingContextToken"] != null ? data["matchmakingContextToken"].ToString(): null,
            };
        }

	}
}