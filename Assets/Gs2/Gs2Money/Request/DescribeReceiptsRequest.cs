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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Money.Model;

namespace Gs2.Gs2Money.Request
{
	public class DescribeReceiptsRequest : Gs2Request<DescribeReceiptsRequest>
	{

        /** ネームスペースの名前 */
        public string namespaceName { set; get; }

        /**
         * ネームスペースの名前を設定
         *
         * @param namespaceName ネームスペースの名前
         * @return this
         */
        public DescribeReceiptsRequest WithNamespaceName(string namespaceName) {
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
        public DescribeReceiptsRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** None */
        public int? slot { set; get; }

        /**
         * Noneを設定
         *
         * @param slot None
         * @return this
         */
        public DescribeReceiptsRequest WithSlot(int? slot) {
            this.slot = slot;
            return this;
        }


        /** None */
        public long? begin { set; get; }

        /**
         * Noneを設定
         *
         * @param begin None
         * @return this
         */
        public DescribeReceiptsRequest WithBegin(long? begin) {
            this.begin = begin;
            return this;
        }


        /** None */
        public long? end { set; get; }

        /**
         * Noneを設定
         *
         * @param end None
         * @return this
         */
        public DescribeReceiptsRequest WithEnd(long? end) {
            this.end = end;
            return this;
        }


        /** データの取得を開始する位置を指定するトークン */
        public string pageToken { set; get; }

        /**
         * データの取得を開始する位置を指定するトークンを設定
         *
         * @param pageToken データの取得を開始する位置を指定するトークン
         * @return this
         */
        public DescribeReceiptsRequest WithPageToken(string pageToken) {
            this.pageToken = pageToken;
            return this;
        }


        /** データの取得件数 */
        public long? limit { set; get; }

        /**
         * データの取得件数を設定
         *
         * @param limit データの取得件数
         * @return this
         */
        public DescribeReceiptsRequest WithLimit(long? limit) {
            this.limit = limit;
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
        public DescribeReceiptsRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}