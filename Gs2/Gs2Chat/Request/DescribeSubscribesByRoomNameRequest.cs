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
using Gs2.Gs2Chat.Model;

namespace Gs2.Gs2Chat.Request
{
	public class DescribeSubscribesByRoomNameRequest : Gs2Request<DescribeSubscribesByRoomNameRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DescribeSubscribesByRoomNameRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 購読するユーザID */
        public string roomName { set; get; }

        /**
         * 購読するユーザIDを設定
         *
         * @param roomName 購読するユーザID
         * @return this
         */
        public DescribeSubscribesByRoomNameRequest WithRoomName(string roomName) {
            this.roomName = roomName;
            return this;
        }


        /** データの取得を開始する位置を指定するトークン */
        public string pageToken { set; get; }

        /**
         * データの取得を開始する位置を指定するトークンを設定
         *
         * @param pageToken データの取得を開始する位置を指定するトークン
         * @return this
         */
        public DescribeSubscribesByRoomNameRequest WithPageToken(string pageToken) {
            this.pageToken = pageToken;
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
        public DescribeSubscribesByRoomNameRequest WithLimit(long? limit) {
            this.limit = limit;
            return this;
        }


	}
}