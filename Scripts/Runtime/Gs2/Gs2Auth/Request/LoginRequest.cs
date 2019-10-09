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
using Gs2.Gs2Auth.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Auth.Request
{
	[Preserve]
	public class LoginRequest : Gs2Request<LoginRequest>
	{

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public LoginRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 現在時刻に対する補正値（現在時刻を起点とした秒数） */
        public int? timeOffset { set; get; }

        /**
         * 現在時刻に対する補正値（現在時刻を起点とした秒数）を設定
         *
         * @param timeOffset 現在時刻に対する補正値（現在時刻を起点とした秒数）
         * @return this
         */
        public LoginRequest WithTimeOffset(int? timeOffset) {
            this.timeOffset = timeOffset;
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
        public LoginRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static LoginRequest FromDict(JsonData data)
        {
            return new LoginRequest {
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                timeOffset = data.Keys.Contains("timeOffset") && data["timeOffset"] != null ? (int?)int.Parse(data["timeOffset"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}