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
	public class UpdateRoomRequest : Gs2Request<UpdateRoomRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateRoomRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ルーム名 */
        public string roomName { set; get; }

        /**
         * ルーム名を設定
         *
         * @param roomName ルーム名
         * @return this
         */
        public UpdateRoomRequest WithRoomName(string roomName) {
            this.roomName = roomName;
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
        public UpdateRoomRequest WithMetadata(string metadata) {
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
        public UpdateRoomRequest WithPassword(string password) {
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
        public UpdateRoomRequest WithWhiteListUserIds(List<string> whiteListUserIds) {
            this.whiteListUserIds = whiteListUserIds;
            return this;
        }


    	[Preserve]
        public static UpdateRoomRequest FromDict(JsonData data)
        {
            return new UpdateRoomRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                roomName = data.Keys.Contains("roomName") && data["roomName"] != null ? data["roomName"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
                whiteListUserIds = data.Keys.Contains("whiteListUserIds") && data["whiteListUserIds"] != null ? data["whiteListUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
            };
        }

	}
}