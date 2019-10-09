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
using Gs2.Gs2Experience.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Experience.Request
{
	[Preserve]
	public class GetStatusWithSignatureRequest : Gs2Request<GetStatusWithSignatureRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetStatusWithSignatureRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 経験値の種類の名前 */
        public string experienceName { set; get; }

        /**
         * 経験値の種類の名前を設定
         *
         * @param experienceName 経験値の種類の名前
         * @return this
         */
        public GetStatusWithSignatureRequest WithExperienceName(string experienceName) {
            this.experienceName = experienceName;
            return this;
        }


        /** プロパティID */
        public string propertyId { set; get; }

        /**
         * プロパティIDを設定
         *
         * @param propertyId プロパティID
         * @return this
         */
        public GetStatusWithSignatureRequest WithPropertyId(string propertyId) {
            this.propertyId = propertyId;
            return this;
        }


        /** 署名の作成に使用する 暗号鍵 のGRN */
        public string keyId { set; get; }

        /**
         * 署名の作成に使用する 暗号鍵 のGRNを設定
         *
         * @param keyId 署名の作成に使用する 暗号鍵 のGRN
         * @return this
         */
        public GetStatusWithSignatureRequest WithKeyId(string keyId) {
            this.keyId = keyId;
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
        public GetStatusWithSignatureRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public GetStatusWithSignatureRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static GetStatusWithSignatureRequest FromDict(JsonData data)
        {
            return new GetStatusWithSignatureRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                experienceName = data.Keys.Contains("experienceName") && data["experienceName"] != null ? data["experienceName"].ToString(): null,
                propertyId = data.Keys.Contains("propertyId") && data["propertyId"] != null ? data["propertyId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}