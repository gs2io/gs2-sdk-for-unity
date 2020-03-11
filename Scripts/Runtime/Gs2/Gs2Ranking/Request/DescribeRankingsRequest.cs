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
using Gs2.Gs2Ranking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Ranking.Request
{
	[Preserve]
	[System.Serializable]
	public class DescribeRankingsRequest : Gs2Request<DescribeRankingsRequest>
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
        public DescribeRankingsRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** カテゴリ名 */
		[UnityEngine.SerializeField]
        public string categoryName;

        /**
         * カテゴリ名を設定
         *
         * @param categoryName カテゴリ名
         * @return this
         */
        public DescribeRankingsRequest WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }


        /** ランキングの取得を開始するインデックス */
		[UnityEngine.SerializeField]
        public long? startIndex;

        /**
         * ランキングの取得を開始するインデックスを設定
         *
         * @param startIndex ランキングの取得を開始するインデックス
         * @return this
         */
        public DescribeRankingsRequest WithStartIndex(long? startIndex) {
            this.startIndex = startIndex;
            return this;
        }


        /** データの取得を開始する位置を指定するトークン */
		[UnityEngine.SerializeField]
        public string pageToken;

        /**
         * データの取得を開始する位置を指定するトークンを設定
         *
         * @param pageToken データの取得を開始する位置を指定するトークン
         * @return this
         */
        public DescribeRankingsRequest WithPageToken(string pageToken) {
            this.pageToken = pageToken;
            return this;
        }


        /** データの取得件数 */
		[UnityEngine.SerializeField]
        public long? limit;

        /**
         * データの取得件数を設定
         *
         * @param limit データの取得件数
         * @return this
         */
        public DescribeRankingsRequest WithLimit(long? limit) {
            this.limit = limit;
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
        public DescribeRankingsRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public DescribeRankingsRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static DescribeRankingsRequest FromDict(JsonData data)
        {
            return new DescribeRankingsRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                categoryName = data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString(): null,
                startIndex = data.Keys.Contains("startIndex") && data["startIndex"] != null ? (long?)long.Parse(data["startIndex"].ToString()) : null,
                pageToken = data.Keys.Contains("pageToken") && data["pageToken"] != null ? data["pageToken"].ToString(): null,
                limit = data.Keys.Contains("limit") && data["limit"] != null ? (long?)long.Parse(data["limit"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}