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
using Gs2.Gs2Friend.Model;

namespace Gs2.Gs2Friend.Request
{
	public class UpdateProfileByUserIdRequest : Gs2Request<UpdateProfileByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateProfileByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public UpdateProfileByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** 公開されるプロフィール */
        public string publicProfile { set; get; }

        /**
         * 公開されるプロフィールを設定
         *
         * @param publicProfile 公開されるプロフィール
         * @return this
         */
        public UpdateProfileByUserIdRequest WithPublicProfile(string publicProfile) {
            this.publicProfile = publicProfile;
            return this;
        }


        /** フォロワー向けに公開されるプロフィール */
        public string followerProfile { set; get; }

        /**
         * フォロワー向けに公開されるプロフィールを設定
         *
         * @param followerProfile フォロワー向けに公開されるプロフィール
         * @return this
         */
        public UpdateProfileByUserIdRequest WithFollowerProfile(string followerProfile) {
            this.followerProfile = followerProfile;
            return this;
        }


        /** フレンド向けに公開されるプロフィール */
        public string friendProfile { set; get; }

        /**
         * フレンド向けに公開されるプロフィールを設定
         *
         * @param friendProfile フレンド向けに公開されるプロフィール
         * @return this
         */
        public UpdateProfileByUserIdRequest WithFriendProfile(string friendProfile) {
            this.friendProfile = friendProfile;
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
        public UpdateProfileByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}