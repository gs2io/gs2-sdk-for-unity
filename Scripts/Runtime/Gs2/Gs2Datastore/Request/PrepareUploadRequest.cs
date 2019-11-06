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
using Gs2.Gs2Datastore.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Datastore.Request
{
	[Preserve]
	public class PrepareUploadRequest : Gs2Request<PrepareUploadRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public PrepareUploadRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** アップロードするデータの MIME-Type */
        public string contentType { set; get; }

        /**
         * アップロードするデータの MIME-Typeを設定
         *
         * @param contentType アップロードするデータの MIME-Type
         * @return this
         */
        public PrepareUploadRequest WithContentType(string contentType) {
            this.contentType = contentType;
            return this;
        }


        /** ファイルのアクセス権 */
        public string scope { set; get; }

        /**
         * ファイルのアクセス権を設定
         *
         * @param scope ファイルのアクセス権
         * @return this
         */
        public PrepareUploadRequest WithScope(string scope) {
            this.scope = scope;
            return this;
        }


        /** 公開するユーザIDリスト */
        public List<string> allowUserIds { set; get; }

        /**
         * 公開するユーザIDリストを設定
         *
         * @param allowUserIds 公開するユーザIDリスト
         * @return this
         */
        public PrepareUploadRequest WithAllowUserIds(List<string> allowUserIds) {
            this.allowUserIds = allowUserIds;
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
        public PrepareUploadRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public PrepareUploadRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static PrepareUploadRequest FromDict(JsonData data)
        {
            return new PrepareUploadRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                contentType = data.Keys.Contains("contentType") && data["contentType"] != null ? data["contentType"].ToString(): null,
                scope = data.Keys.Contains("scope") && data["scope"] != null ? data["scope"].ToString(): null,
                allowUserIds = data.Keys.Contains("allowUserIds") && data["allowUserIds"] != null ? data["allowUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}