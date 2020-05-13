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
using Gs2.Gs2Dictionary.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Dictionary.Request
{
	[Preserve]
	[System.Serializable]
	public class GetEntryWithSignatureRequest : Gs2Request<GetEntryWithSignatureRequest>
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
        public GetEntryWithSignatureRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** エントリー名 */
		[UnityEngine.SerializeField]
        public string entryModelName;

        /**
         * エントリー名を設定
         *
         * @param entryModelName エントリー名
         * @return this
         */
        public GetEntryWithSignatureRequest WithEntryModelName(string entryModelName) {
            this.entryModelName = entryModelName;
            return this;
        }


        /** 署名の発行に使用する暗号鍵 のGRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * 署名の発行に使用する暗号鍵 のGRNを設定
         *
         * @param keyId 署名の発行に使用する暗号鍵 のGRN
         * @return this
         */
        public GetEntryWithSignatureRequest WithKeyId(string keyId) {
            this.keyId = keyId;
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
        public GetEntryWithSignatureRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public GetEntryWithSignatureRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static GetEntryWithSignatureRequest FromDict(JsonData data)
        {
            return new GetEntryWithSignatureRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                entryModelName = data.Keys.Contains("entryModelName") && data["entryModelName"] != null ? data["entryModelName"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}