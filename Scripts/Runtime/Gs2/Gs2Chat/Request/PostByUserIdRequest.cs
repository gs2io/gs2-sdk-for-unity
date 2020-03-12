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
	public class PostByUserIdRequest : Gs2Request<PostByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public PostByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public PostByUserIdRequest WithRoomName(string roomName) {
            this.roomName = roomName;
            return this;
        }


        /** 発言したユーザID */
        public string userId { set; get; }

        /**
         * 発言したユーザIDを設定
         *
         * @param userId 発言したユーザID
         * @return this
         */
        public PostByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** メッセージの種類を分類したい時の種類番号 */
        public int? category { set; get; }

        /**
         * メッセージの種類を分類したい時の種類番号を設定
         *
         * @param category メッセージの種類を分類したい時の種類番号
         * @return this
         */
        public PostByUserIdRequest WithCategory(int? category) {
            this.category = category;
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
        public PostByUserIdRequest WithMetadata(string metadata) {
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
        public PostByUserIdRequest WithPassword(string password) {
            this.password = password;
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
        public PostByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static PostByUserIdRequest FromDict(JsonData data)
        {
            return new PostByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                roomName = data.Keys.Contains("roomName") && data["roomName"] != null ? data["roomName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                category = data.Keys.Contains("category") && data["category"] != null ? (int?)int.Parse(data["category"].ToString()) : null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}