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
	public class GetBillingActivityRequest : Gs2Request<GetBillingActivityRequest>
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
        public GetBillingActivityRequest WithYear(int? year) {
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
        public GetBillingActivityRequest WithMonth(int? month) {
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
        public GetBillingActivityRequest WithService(string service) {
            this.service = service;
            return this;
        }


        /** イベントの種類 */
		[UnityEngine.SerializeField]
        public string activityType;

        /**
         * イベントの種類を設定
         *
         * @param activityType イベントの種類
         * @return this
         */
        public GetBillingActivityRequest WithActivityType(string activityType) {
            this.activityType = activityType;
            return this;
        }


    	[Preserve]
        public static GetBillingActivityRequest FromDict(JsonData data)
        {
            return new GetBillingActivityRequest {
                year = data.Keys.Contains("year") && data["year"] != null ? (int?)int.Parse(data["year"].ToString()) : null,
                month = data.Keys.Contains("month") && data["month"] != null ? (int?)int.Parse(data["month"].ToString()) : null,
                service = data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString(): null,
                activityType = data.Keys.Contains("activityType") && data["activityType"] != null ? data["activityType"].ToString(): null,
            };
        }

	}
}