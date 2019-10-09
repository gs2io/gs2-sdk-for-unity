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
	public class DescribeMessagesRequest : Gs2Request<DescribeMessagesRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DescribeMessagesRequest WithNamespaceName(string namespaceName) {
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
        public DescribeMessagesRequest WithRoomName(string roomName) {
            this.roomName = roomName;
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
        public DescribeMessagesRequest WithPassword(string password) {
            this.password = password;
            return this;
        }


        /** メッセージの取得を開始する時間 */
        public long? startAt { set; get; }

        /**
         * メッセージの取得を開始する時間を設定
         *
         * @param startAt メッセージの取得を開始する時間
         * @return this
         */
        public DescribeMessagesRequest WithStartAt(long? startAt) {
            this.startAt = startAt;
            return this;
        }


        /** データの取得件数 */
        public long? limit { set; get; }

        /**
         * データの取得件数を設定
         *
         * @param limit データの取得件数
         * @return this
         */
        public DescribeMessagesRequest WithLimit(long? limit) {
            this.limit = limit;
            return this;
        }


    	[Preserve]
        public static DescribeMessagesRequest FromDict(JsonData data)
        {
            return new DescribeMessagesRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                roomName = data.Keys.Contains("roomName") && data["roomName"] != null ? data["roomName"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
                startAt = data.Keys.Contains("startAt") && data["startAt"] != null ? (long?)long.Parse(data["startAt"].ToString()) : null,
                limit = data.Keys.Contains("limit") && data["limit"] != null ? (long?)long.Parse(data["limit"].ToString()) : null,
            };
        }

	}
}