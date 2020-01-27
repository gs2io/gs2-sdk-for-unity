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
using Gs2.Gs2Limit.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Limit.Request
{
	[Preserve]
	public class CountUpRequest : Gs2Request<CountUpRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CountUpRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 回数制限の種類の名前 */
        public string limitName { set; get; }

        /**
         * 回数制限の種類の名前を設定
         *
         * @param limitName 回数制限の種類の名前
         * @return this
         */
        public CountUpRequest WithLimitName(string limitName) {
            this.limitName = limitName;
            return this;
        }


        /** カウンターの名前 */
        public string counterName { set; get; }

        /**
         * カウンターの名前を設定
         *
         * @param counterName カウンターの名前
         * @return this
         */
        public CountUpRequest WithCounterName(string counterName) {
            this.counterName = counterName;
            return this;
        }


        /** カウントアップする量 */
        public int? countUpValue { set; get; }

        /**
         * カウントアップする量を設定
         *
         * @param countUpValue カウントアップする量
         * @return this
         */
        public CountUpRequest WithCountUpValue(int? countUpValue) {
            this.countUpValue = countUpValue;
            return this;
        }


        /** カウントアップを許容する最大値 を入力してください */
        public int? maxValue { set; get; }

        /**
         * カウントアップを許容する最大値 を入力してくださいを設定
         *
         * @param maxValue カウントアップを許容する最大値 を入力してください
         * @return this
         */
        public CountUpRequest WithMaxValue(int? maxValue) {
            this.maxValue = maxValue;
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
        public CountUpRequest WithDuplicationAvoider(string duplicationAvoider) {
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
        public CountUpRequest WithAccessToken(string accessToken) {
            this.accessToken = accessToken;
            return this;
        }

    	[Preserve]
        public static CountUpRequest FromDict(JsonData data)
        {
            return new CountUpRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                limitName = data.Keys.Contains("limitName") && data["limitName"] != null ? data["limitName"].ToString(): null,
                counterName = data.Keys.Contains("counterName") && data["counterName"] != null ? data["counterName"].ToString(): null,
                countUpValue = data.Keys.Contains("countUpValue") && data["countUpValue"] != null ? (int?)int.Parse(data["countUpValue"].ToString()) : null,
                maxValue = data.Keys.Contains("maxValue") && data["maxValue"] != null ? (int?)int.Parse(data["maxValue"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}