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
using Gs2.Gs2Project.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Request
{
	[Preserve]
	[System.Serializable]
	public class DescribeReceiptsRequest : Gs2Request<DescribeReceiptsRequest>
	{

        /** GS2アカウントトークン */
		[UnityEngine.SerializeField]
        public string accountToken;

        /**
         * GS2アカウントトークンを設定
         *
         * @param accountToken GS2アカウントトークン
         * @return this
         */
        public DescribeReceiptsRequest WithAccountToken(string accountToken) {
            this.accountToken = accountToken;
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
        public DescribeReceiptsRequest WithPageToken(string pageToken) {
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
        public DescribeReceiptsRequest WithLimit(long? limit) {
            this.limit = limit;
            return this;
        }


    	[Preserve]
        public static DescribeReceiptsRequest FromDict(JsonData data)
        {
            return new DescribeReceiptsRequest {
                accountToken = data.Keys.Contains("accountToken") && data["accountToken"] != null ? data["accountToken"].ToString(): null,
                pageToken = data.Keys.Contains("pageToken") && data["pageToken"] != null ? data["pageToken"].ToString(): null,
                limit = data.Keys.Contains("limit") && data["limit"] != null ? (long?)long.Parse(data["limit"].ToString()) : null,
            };
        }

	}
}