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
using Gs2.Gs2Distributor.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Distributor.Request
{
	[Preserve]
	public class DistributeRequest : Gs2Request<DistributeRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DistributeRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ディストリビューターの種類名 */
        public string distributorName { set; get; }

        /**
         * ディストリビューターの種類名を設定
         *
         * @param distributorName ディストリビューターの種類名
         * @return this
         */
        public DistributeRequest WithDistributorName(string distributorName) {
            this.distributorName = distributorName;
            return this;
        }


        /** 加算するリソース */
        public DistributeResource distributeResource { set; get; }

        /**
         * 加算するリソースを設定
         *
         * @param distributeResource 加算するリソース
         * @return this
         */
        public DistributeRequest WithDistributeResource(DistributeResource distributeResource) {
            this.distributeResource = distributeResource;
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
        public DistributeRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public DistributeRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static DistributeRequest FromDict(JsonData data)
        {
            return new DistributeRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                distributorName = data.Keys.Contains("distributorName") && data["distributorName"] != null ? data["distributorName"].ToString(): null,
                distributeResource = data.Keys.Contains("distributeResource") && data["distributeResource"] != null ? DistributeResource.FromDict(data["distributeResource"]) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}