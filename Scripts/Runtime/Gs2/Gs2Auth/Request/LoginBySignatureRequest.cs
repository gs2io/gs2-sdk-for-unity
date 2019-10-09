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
using Gs2.Gs2Auth.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Auth.Request
{
	[Preserve]
	public class LoginBySignatureRequest : Gs2Request<LoginBySignatureRequest>
	{

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public LoginBySignatureRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 署名の作成に使用した暗号鍵 のGRN */
        public string keyId { set; get; }

        /**
         * 署名の作成に使用した暗号鍵 のGRNを設定
         *
         * @param keyId 署名の作成に使用した暗号鍵 のGRN
         * @return this
         */
        public LoginBySignatureRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** アカウント認証情報の署名対象 */
        public string body { set; get; }

        /**
         * アカウント認証情報の署名対象を設定
         *
         * @param body アカウント認証情報の署名対象
         * @return this
         */
        public LoginBySignatureRequest WithBody(string body) {
            this.body = body;
            return this;
        }


        /** 署名 */
        public string signature { set; get; }

        /**
         * 署名を設定
         *
         * @param signature 署名
         * @return this
         */
        public LoginBySignatureRequest WithSignature(string signature) {
            this.signature = signature;
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
        public LoginBySignatureRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static LoginBySignatureRequest FromDict(JsonData data)
        {
            return new LoginBySignatureRequest {
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                body = data.Keys.Contains("body") && data["body"] != null ? data["body"].ToString(): null,
                signature = data.Keys.Contains("signature") && data["signature"] != null ? data["signature"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}