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
using Gs2.Gs2Gateway.Model;

namespace Gs2.Gs2Gateway.Request
{
	public class SendMobileNotificationByUserIdRequest : Gs2Request<SendMobileNotificationByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public SendMobileNotificationByUserIdRequest WithNamespaceName(string namespaceName) {
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
        public SendMobileNotificationByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** タイトル */
        public string subject { set; get; }

        /**
         * タイトルを設定
         *
         * @param subject タイトル
         * @return this
         */
        public SendMobileNotificationByUserIdRequest WithSubject(string subject) {
            this.subject = subject;
            return this;
        }


        /** ペイロード */
        public string payload { set; get; }

        /**
         * ペイロードを設定
         *
         * @param payload ペイロード
         * @return this
         */
        public SendMobileNotificationByUserIdRequest WithPayload(string payload) {
            this.payload = payload;
            return this;
        }


        /** 再生する音声ファイル名 */
        public string sound { set; get; }

        /**
         * 再生する音声ファイル名を設定
         *
         * @param sound 再生する音声ファイル名
         * @return this
         */
        public SendMobileNotificationByUserIdRequest WithSound(string sound) {
            this.sound = sound;
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
        public SendMobileNotificationByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}