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
using Gs2.Gs2Showcase.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Showcase.Request
{
	[Preserve]
	public class BuyRequest : Gs2Request<BuyRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public BuyRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 商品名 */
        public string showcaseName { set; get; }

        /**
         * 商品名を設定
         *
         * @param showcaseName 商品名
         * @return this
         */
        public BuyRequest WithShowcaseName(string showcaseName) {
            this.showcaseName = showcaseName;
            return this;
        }


        /** 陳列商品ID */
        public string displayItemId { set; get; }

        /**
         * 陳列商品IDを設定
         *
         * @param displayItemId 陳列商品ID
         * @return this
         */
        public BuyRequest WithDisplayItemId(string displayItemId) {
            this.displayItemId = displayItemId;
            return this;
        }


        /** 設定値 */
        public List<Config> config { set; get; }

        /**
         * 設定値を設定
         *
         * @param config 設定値
         * @return this
         */
        public BuyRequest WithConfig(List<Config> config) {
            this.config = config;
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
        public BuyRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public BuyRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static BuyRequest FromDict(JsonData data)
        {
            return new BuyRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                showcaseName = data.Keys.Contains("showcaseName") && data["showcaseName"] != null ? data["showcaseName"].ToString(): null,
                displayItemId = data.Keys.Contains("displayItemId") && data["displayItemId"] != null ? data["displayItemId"].ToString(): null,
                config = data.Keys.Contains("config") && data["config"] != null ? data["config"].Cast<JsonData>().Select(value =>
                    {
                        return Config.FromDict(value);
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}