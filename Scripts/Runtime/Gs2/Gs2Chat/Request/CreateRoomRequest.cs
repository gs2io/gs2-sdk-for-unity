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
using Gs2.Gs2Chat.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Request
{
	[Preserve]
	public class CreateRoomRequest : Gs2Request<CreateRoomRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateRoomRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public CreateRoomRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** メッセージを投稿するために必要となるパスワード */
        public string password { set; get; }

        /**
         * メッセージを投稿するために必要となるパスワードを設定
         *
         * @param password メッセージを投稿するために必要となるパスワード
         * @return this
         */
        public CreateRoomRequest WithPassword(string password) {
            this.password = password;
            return this;
        }


        /** ルームに参加可能なユーザIDリスト */
        public List<string> whiteListUserIds { set; get; }

        /**
         * ルームに参加可能なユーザIDリストを設定
         *
         * @param whiteListUserIds ルームに参加可能なユーザIDリスト
         * @return this
         */
        public CreateRoomRequest WithWhiteListUserIds(List<string> whiteListUserIds) {
            this.whiteListUserIds = whiteListUserIds;
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
        public CreateRoomRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


        /** アクセストークン */
        public string accessToken { set; get; }

        /**
         * アクセストークンを設定
         *
         * @param accessToken アクセストークン
         * @return this
         */
        public CreateRoomRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static CreateRoomRequest FromDict(JsonData data)
        {
            return new CreateRoomRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
                whiteListUserIds = data.Keys.Contains("whiteListUserIds") && data["whiteListUserIds"] != null ? data["whiteListUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}