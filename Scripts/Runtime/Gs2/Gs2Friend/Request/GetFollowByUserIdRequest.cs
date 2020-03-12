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
using Gs2.Gs2Friend.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Friend.Request
{
	[Preserve]
	public class GetFollowByUserIdRequest : Gs2Request<GetFollowByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetFollowByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** フォローしているユーザID */
        public string userId { set; get; }

        /**
         * フォローしているユーザIDを設定
         *
         * @param userId フォローしているユーザID
         * @return this
         */
        public GetFollowByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** フォローされているユーザID */
        public string targetUserId { set; get; }

        /**
         * フォローされているユーザIDを設定
         *
         * @param targetUserId フォローされているユーザID
         * @return this
         */
        public GetFollowByUserIdRequest WithTargetUserId(string targetUserId) {
            this.targetUserId = targetUserId;
            return this;
        }


        /** プロフィールも一緒に取得するか */
        public bool? withProfile { set; get; }

        /**
         * プロフィールも一緒に取得するかを設定
         *
         * @param withProfile プロフィールも一緒に取得するか
         * @return this
         */
        public GetFollowByUserIdRequest WithWithProfile(bool? withProfile) {
            this.withProfile = withProfile;
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
        public GetFollowByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static GetFollowByUserIdRequest FromDict(JsonData data)
        {
            return new GetFollowByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                targetUserId = data.Keys.Contains("targetUserId") && data["targetUserId"] != null ? data["targetUserId"].ToString(): null,
                withProfile = data.Keys.Contains("withProfile") && data["withProfile"] != null ? (bool?)bool.Parse(data["withProfile"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}