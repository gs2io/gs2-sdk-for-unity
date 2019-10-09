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
using Gs2.Gs2Mission.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Request
{
	[Preserve]
	public class IncreaseCounterByUserIdRequest : Gs2Request<IncreaseCounterByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public IncreaseCounterByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** カウンター名 */
        public string counterName { set; get; }

        /**
         * カウンター名を設定
         *
         * @param counterName カウンター名
         * @return this
         */
        public IncreaseCounterByUserIdRequest WithCounterName(string counterName) {
            this.counterName = counterName;
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
        public IncreaseCounterByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 加算する値 */
        public long? value { set; get; }

        /**
         * 加算する値を設定
         *
         * @param value 加算する値
         * @return this
         */
        public IncreaseCounterByUserIdRequest WithValue(long? value) {
            this.value = value;
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
        public IncreaseCounterByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static IncreaseCounterByUserIdRequest FromDict(JsonData data)
        {
            return new IncreaseCounterByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                counterName = data.Keys.Contains("counterName") && data["counterName"] != null ? data["counterName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                value = data.Keys.Contains("value") && data["value"] != null ? (long?)long.Parse(data["value"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}