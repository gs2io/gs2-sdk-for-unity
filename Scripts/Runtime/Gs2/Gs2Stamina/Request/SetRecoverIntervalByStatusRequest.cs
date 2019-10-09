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
using Gs2.Gs2Stamina.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Stamina.Request
{
	[Preserve]
	public class SetRecoverIntervalByStatusRequest : Gs2Request<SetRecoverIntervalByStatusRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public SetRecoverIntervalByStatusRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナの種類名 */
        public string staminaName { set; get; }

        /**
         * スタミナの種類名を設定
         *
         * @param staminaName スタミナの種類名
         * @return this
         */
        public SetRecoverIntervalByStatusRequest WithStaminaName(string staminaName) {
            this.staminaName = staminaName;
            return this;
        }


        /** 署名をつけるのに使用した暗号鍵 のGRN */
        public string keyId { set; get; }

        /**
         * 署名をつけるのに使用した暗号鍵 のGRNを設定
         *
         * @param keyId 署名をつけるのに使用した暗号鍵 のGRN
         * @return this
         */
        public SetRecoverIntervalByStatusRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** 署名対象のステータスボディ */
        public string signedStatusBody { set; get; }

        /**
         * 署名対象のステータスボディを設定
         *
         * @param signedStatusBody 署名対象のステータスボディ
         * @return this
         */
        public SetRecoverIntervalByStatusRequest WithSignedStatusBody(string signedStatusBody) {
            this.signedStatusBody = signedStatusBody;
            return this;
        }


        /** ステータスの署名 */
        public string signedStatusSignature { set; get; }

        /**
         * ステータスの署名を設定
         *
         * @param signedStatusSignature ステータスの署名
         * @return this
         */
        public SetRecoverIntervalByStatusRequest WithSignedStatusSignature(string signedStatusSignature) {
            this.signedStatusSignature = signedStatusSignature;
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
        public SetRecoverIntervalByStatusRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public SetRecoverIntervalByStatusRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static SetRecoverIntervalByStatusRequest FromDict(JsonData data)
        {
            return new SetRecoverIntervalByStatusRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                staminaName = data.Keys.Contains("staminaName") && data["staminaName"] != null ? data["staminaName"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                signedStatusBody = data.Keys.Contains("signedStatusBody") && data["signedStatusBody"] != null ? data["signedStatusBody"].ToString(): null,
                signedStatusSignature = data.Keys.Contains("signedStatusSignature") && data["signedStatusSignature"] != null ? data["signedStatusSignature"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}