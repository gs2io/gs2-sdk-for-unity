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
using Gs2.Gs2Account.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Account.Request
{
	[Preserve]
	public class UpdateTakeOverByUserIdRequest : Gs2Request<UpdateTakeOverByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateTakeOverByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public UpdateTakeOverByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** スロット番号 */
        public int? type { set; get; }

        /**
         * スロット番号を設定
         *
         * @param type スロット番号
         * @return this
         */
        public UpdateTakeOverByUserIdRequest WithType(int? type) {
            this.type = type;
            return this;
        }


        /** 古いパスワード */
        public string oldPassword { set; get; }

        /**
         * 古いパスワードを設定
         *
         * @param oldPassword 古いパスワード
         * @return this
         */
        public UpdateTakeOverByUserIdRequest WithOldPassword(string oldPassword) {
            this.oldPassword = oldPassword;
            return this;
        }


        /** 新しいパスワード */
        public string password { set; get; }

        /**
         * 新しいパスワードを設定
         *
         * @param password 新しいパスワード
         * @return this
         */
        public UpdateTakeOverByUserIdRequest WithPassword(string password) {
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
        public UpdateTakeOverByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static UpdateTakeOverByUserIdRequest FromDict(JsonData data)
        {
            return new UpdateTakeOverByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                type = data.Keys.Contains("type") && data["type"] != null ? (int?)int.Parse(data["type"].ToString()) : null,
                oldPassword = data.Keys.Contains("oldPassword") && data["oldPassword"] != null ? data["oldPassword"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}