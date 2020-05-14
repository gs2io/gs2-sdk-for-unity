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
using Gs2.Gs2Watch.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Watch.Request
{
	[Preserve]
	[System.Serializable]
	public class DescribeBillingActivitiesRequest : Gs2Request<DescribeBillingActivitiesRequest>
	{

        /** イベントの発生年 */
		[UnityEngine.SerializeField]
        public int? year;

        /**
         * イベントの発生年を設定
         *
         * @param year イベントの発生年
         * @return this
         */
        public DescribeBillingActivitiesRequest WithYear(int? year) {
            this.year = year;
            return this;
        }


        /** イベントの発生月 */
		[UnityEngine.SerializeField]
        public int? month;

        /**
         * イベントの発生月を設定
         *
         * @param month イベントの発生月
         * @return this
         */
        public DescribeBillingActivitiesRequest WithMonth(int? month) {
            this.month = month;
            return this;
        }


        /** サービスの種類 */
		[UnityEngine.SerializeField]
        public string service;

        /**
         * サービスの種類を設定
         *
         * @param service サービスの種類
         * @return this
         */
        public DescribeBillingActivitiesRequest WithService(string service) {
            this.service = service;
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
        public DescribeBillingActivitiesRequest WithPageToken(string pageToken) {
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
        public DescribeBillingActivitiesRequest WithLimit(long? limit) {
            this.limit = limit;
            return this;
        }


    	[Preserve]
        public static DescribeBillingActivitiesRequest FromDict(JsonData data)
        {
            return new DescribeBillingActivitiesRequest {
                year = data.Keys.Contains("year") && data["year"] != null ? (int?)int.Parse(data["year"].ToString()) : null,
                month = data.Keys.Contains("month") && data["month"] != null ? (int?)int.Parse(data["month"].ToString()) : null,
                service = data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString(): null,
                pageToken = data.Keys.Contains("pageToken") && data["pageToken"] != null ? data["pageToken"].ToString(): null,
                limit = data.Keys.Contains("limit") && data["limit"] != null ? (long?)long.Parse(data["limit"].ToString()) : null,
            };
        }

	}
}