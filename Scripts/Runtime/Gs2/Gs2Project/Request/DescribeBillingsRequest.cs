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
	public class DescribeBillingsRequest : Gs2Request<DescribeBillingsRequest>
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
        public DescribeBillingsRequest WithAccountToken(string accountToken) {
            this.accountToken = accountToken;
            return this;
        }


        /** プロジェクト名 */
		[UnityEngine.SerializeField]
        public string projectName;

        /**
         * プロジェクト名を設定
         *
         * @param projectName プロジェクト名
         * @return this
         */
        public DescribeBillingsRequest WithProjectName(string projectName) {
            this.projectName = projectName;
            return this;
        }


        /** イベントの発生年 */
		[UnityEngine.SerializeField]
        public int? year;

        /**
         * イベントの発生年を設定
         *
         * @param year イベントの発生年
         * @return this
         */
        public DescribeBillingsRequest WithYear(int? year) {
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
        public DescribeBillingsRequest WithMonth(int? month) {
            this.month = month;
            return this;
        }


        /** サービスの種類 */
		[UnityEngine.SerializeField]
        public string region;

        /**
         * サービスの種類を設定
         *
         * @param region サービスの種類
         * @return this
         */
        public DescribeBillingsRequest WithRegion(string region) {
            this.region = region;
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
        public DescribeBillingsRequest WithService(string service) {
            this.service = service;
            return this;
        }


    	[Preserve]
        public static DescribeBillingsRequest FromDict(JsonData data)
        {
            return new DescribeBillingsRequest {
                accountToken = data.Keys.Contains("accountToken") && data["accountToken"] != null ? data["accountToken"].ToString(): null,
                projectName = data.Keys.Contains("projectName") && data["projectName"] != null ? data["projectName"].ToString(): null,
                year = data.Keys.Contains("year") && data["year"] != null ? (int?)int.Parse(data["year"].ToString()) : null,
                month = data.Keys.Contains("month") && data["month"] != null ? (int?)int.Parse(data["month"].ToString()) : null,
                region = data.Keys.Contains("region") && data["region"] != null ? data["region"].ToString(): null,
                service = data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString(): null,
            };
        }

	}
}