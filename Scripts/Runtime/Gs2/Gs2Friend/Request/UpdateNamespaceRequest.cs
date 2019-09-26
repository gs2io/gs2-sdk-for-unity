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
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** フォローされたときに実行するスクリプト */
        public ScriptSetting followScript { set; get; }

        /**
         * フォローされたときに実行するスクリプトを設定
         *
         * @param followScript フォローされたときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithFollowScript(ScriptSetting followScript) {
            this.followScript = followScript;
            return this;
        }


        /** アンフォローされたときに実行するスクリプト */
        public ScriptSetting unfollowScript { set; get; }

        /**
         * アンフォローされたときに実行するスクリプトを設定
         *
         * @param unfollowScript アンフォローされたときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithUnfollowScript(ScriptSetting unfollowScript) {
            this.unfollowScript = unfollowScript;
            return this;
        }


        /** フレンドリクエストを発行したときに実行するスクリプト */
        public ScriptSetting sendRequestScript { set; get; }

        /**
         * フレンドリクエストを発行したときに実行するスクリプトを設定
         *
         * @param sendRequestScript フレンドリクエストを発行したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithSendRequestScript(ScriptSetting sendRequestScript) {
            this.sendRequestScript = sendRequestScript;
            return this;
        }


        /** フレンドリクエストをキャンセルしたときに実行するスクリプト */
        public ScriptSetting cancelRequestScript { set; get; }

        /**
         * フレンドリクエストをキャンセルしたときに実行するスクリプトを設定
         *
         * @param cancelRequestScript フレンドリクエストをキャンセルしたときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCancelRequestScript(ScriptSetting cancelRequestScript) {
            this.cancelRequestScript = cancelRequestScript;
            return this;
        }


        /** フレンドリクエストを承諾したときに実行するスクリプト */
        public ScriptSetting acceptRequestScript { set; get; }

        /**
         * フレンドリクエストを承諾したときに実行するスクリプトを設定
         *
         * @param acceptRequestScript フレンドリクエストを承諾したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithAcceptRequestScript(ScriptSetting acceptRequestScript) {
            this.acceptRequestScript = acceptRequestScript;
            return this;
        }


        /** フレンドリクエストを拒否したときに実行するスクリプト */
        public ScriptSetting rejectRequestScript { set; get; }

        /**
         * フレンドリクエストを拒否したときに実行するスクリプトを設定
         *
         * @param rejectRequestScript フレンドリクエストを拒否したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithRejectRequestScript(ScriptSetting rejectRequestScript) {
            this.rejectRequestScript = rejectRequestScript;
            return this;
        }


        /** フレンドを削除したときに実行するスクリプト */
        public ScriptSetting deleteFriendScript { set; get; }

        /**
         * フレンドを削除したときに実行するスクリプトを設定
         *
         * @param deleteFriendScript フレンドを削除したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithDeleteFriendScript(ScriptSetting deleteFriendScript) {
            this.deleteFriendScript = deleteFriendScript;
            return this;
        }


        /** プロフィールを更新したときに実行するスクリプト */
        public ScriptSetting updateProfileScript { set; get; }

        /**
         * プロフィールを更新したときに実行するスクリプトを設定
         *
         * @param updateProfileScript プロフィールを更新したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithUpdateProfileScript(ScriptSetting updateProfileScript) {
            this.updateProfileScript = updateProfileScript;
            return this;
        }


        /** フォローされたときのプッシュ通知 */
        public NotificationSetting followNotification { set; get; }

        /**
         * フォローされたときのプッシュ通知を設定
         *
         * @param followNotification フォローされたときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithFollowNotification(NotificationSetting followNotification) {
            this.followNotification = followNotification;
            return this;
        }


        /** フレンドリクエストが届いたときのプッシュ通知 */
        public NotificationSetting receiveRequestNotification { set; get; }

        /**
         * フレンドリクエストが届いたときのプッシュ通知を設定
         *
         * @param receiveRequestNotification フレンドリクエストが届いたときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveRequestNotification(NotificationSetting receiveRequestNotification) {
            this.receiveRequestNotification = receiveRequestNotification;
            return this;
        }


        /** フレンドリクエストが承認されたときのプッシュ通知 */
        public NotificationSetting acceptRequestNotification { set; get; }

        /**
         * フレンドリクエストが承認されたときのプッシュ通知を設定
         *
         * @param acceptRequestNotification フレンドリクエストが承認されたときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithAcceptRequestNotification(NotificationSetting acceptRequestNotification) {
            this.acceptRequestNotification = acceptRequestNotification;
            return this;
        }


	}
}