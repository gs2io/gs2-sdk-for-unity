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
	[System.Serializable]
	public class SetRecoverValueByStatusRequest : Gs2Request<SetRecoverValueByStatusRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public SetRecoverValueByStatusRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナの種類名 */
		[UnityEngine.SerializeField]
        public string staminaName;

        /**
         * スタミナの種類名を設定
         *
         * @param staminaName スタミナの種類名
         * @return this
         */
        public SetRecoverValueByStatusRequest WithStaminaName(string staminaName) {
            this.staminaName = staminaName;
            return this;
        }


        /** 署名をつけるのに使用した暗号鍵 のGRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * 署名をつけるのに使用した暗号鍵 のGRNを設定
         *
         * @param keyId 署名をつけるのに使用した暗号鍵 のGRN
         * @return this
         */
        public SetRecoverValueByStatusRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** 署名対象のステータスボディ */
		[UnityEngine.SerializeField]
        public string signedStatusBody;

        /**
         * 署名対象のステータスボディを設定
         *
         * @param signedStatusBody 署名対象のステータスボディ
         * @return this
         */
        public SetRecoverValueByStatusRequest WithSignedStatusBody(string signedStatusBody) {
            this.signedStatusBody = signedStatusBody;
            return this;
        }


        /** ステータスの署名 */
		[UnityEngine.SerializeField]
        public string signedStatusSignature;

        /**
         * ステータスの署名を設定
         *
         * @param signedStatusSignature ステータスの署名
         * @return this
         */
        public SetRecoverValueByStatusRequest WithSignedStatusSignature(string signedStatusSignature) {
            this.signedStatusSignature = signedStatusSignature;
            return this;
        }


        /** 重複実行回避機能に使用するID */
		[UnityEngine.SerializeField]
        public string duplicationAvoider;

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public SetRecoverValueByStatusRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public SetRecoverValueByStatusRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static SetRecoverValueByStatusRequest FromDict(JsonData data)
        {
            return new SetRecoverValueByStatusRequest {
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