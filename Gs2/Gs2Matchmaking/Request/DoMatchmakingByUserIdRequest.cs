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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Matchmaking.Model;

namespace Gs2.Gs2Matchmaking.Request
{
	public class DoMatchmakingByUserIdRequest : Gs2Request<DoMatchmakingByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DoMatchmakingByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public DoMatchmakingByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 自身のプレイヤー情報 */
        public Player player { set; get; }

        /**
         * 自身のプレイヤー情報を設定
         *
         * @param player 自身のプレイヤー情報
         * @return this
         */
        public DoMatchmakingByUserIdRequest WithPlayer(Player player) {
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
        public DoMatchmakingByUserIdRequest WithMatchmakingContextToken(string matchmakingContextToken) {
            this.matchmakingContextToken = matchmakingContextToken;
            return this;
        }


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public DoMatchmakingByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}