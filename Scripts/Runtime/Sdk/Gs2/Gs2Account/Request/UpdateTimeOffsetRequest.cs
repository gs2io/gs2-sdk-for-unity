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
using Gs2.Gs2Account.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Account.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateTimeOffsetRequest : Gs2Request<UpdateTimeOffsetRequest>
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
        public UpdateTimeOffsetRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** アカウントID */
		[UnityEngine.SerializeField]
        public string userId;

        /**
         * アカウントIDを設定
         *
         * @param userId アカウントID
         * @return this
         */
        public UpdateTimeOffsetRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 現在時刻に対する補正値（現在時刻を起点とした秒数） */
		[UnityEngine.SerializeField]
        public int? timeOffset;

        /**
         * 現在時刻に対する補正値（現在時刻を起点とした秒数）を設定
         *
         * @param timeOffset 現在時刻に対する補正値（現在時刻を起点とした秒数）
         * @return this
         */
        public UpdateTimeOffsetRequest WithTimeOffset(int? timeOffset) {
            this.timeOffset = timeOffset;
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
        public UpdateTimeOffsetRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static UpdateTimeOffsetRequest FromDict(JsonData data)
        {
            return new UpdateTimeOffsetRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                timeOffset = data.Keys.Contains("timeOffset") && data["timeOffset"] != null ? (int?)int.Parse(data["timeOffset"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}