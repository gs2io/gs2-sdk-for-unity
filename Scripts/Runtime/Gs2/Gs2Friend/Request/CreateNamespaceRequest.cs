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
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
            this.name = name;
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
        public CreateNamespaceRequest WithDescription(string description) {
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
        public CreateNamespaceRequest WithFollowScript(ScriptSetting followScript) {
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
        public CreateNamespaceRequest WithUnfollowScript(ScriptSetting unfollowScript) {
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
        public CreateNamespaceRequest WithSendRequestScript(ScriptSetting sendRequestScript) {
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
        public CreateNamespaceRequest WithCancelRequestScript(ScriptSetting cancelRequestScript) {
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
        public CreateNamespaceRequest WithAcceptRequestScript(ScriptSetting acceptRequestScript) {
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
        public CreateNamespaceRequest WithRejectRequestScript(ScriptSetting rejectRequestScript) {
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
        public CreateNamespaceRequest WithDeleteFriendScript(ScriptSetting deleteFriendScript) {
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
        public CreateNamespaceRequest WithUpdateProfileScript(ScriptSetting updateProfileScript) {
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
        public CreateNamespaceRequest WithFollowNotification(NotificationSetting followNotification) {
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
        public CreateNamespaceRequest WithReceiveRequestNotification(NotificationSetting receiveRequestNotification) {
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
        public CreateNamespaceRequest WithAcceptRequestNotification(NotificationSetting acceptRequestNotification) {
            this.acceptRequestNotification = acceptRequestNotification;
            return this;
        }


        /** ログの出力設定 */
        public LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public CreateNamespaceRequest WithLogSetting(LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static CreateNamespaceRequest FromDict(JsonData data)
        {
            return new CreateNamespaceRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                followScript = data.Keys.Contains("followScript") && data["followScript"] != null ? ScriptSetting.FromDict(data["followScript"]) : null,
                unfollowScript = data.Keys.Contains("unfollowScript") && data["unfollowScript"] != null ? ScriptSetting.FromDict(data["unfollowScript"]) : null,
                sendRequestScript = data.Keys.Contains("sendRequestScript") && data["sendRequestScript"] != null ? ScriptSetting.FromDict(data["sendRequestScript"]) : null,
                cancelRequestScript = data.Keys.Contains("cancelRequestScript") && data["cancelRequestScript"] != null ? ScriptSetting.FromDict(data["cancelRequestScript"]) : null,
                acceptRequestScript = data.Keys.Contains("acceptRequestScript") && data["acceptRequestScript"] != null ? ScriptSetting.FromDict(data["acceptRequestScript"]) : null,
                rejectRequestScript = data.Keys.Contains("rejectRequestScript") && data["rejectRequestScript"] != null ? ScriptSetting.FromDict(data["rejectRequestScript"]) : null,
                deleteFriendScript = data.Keys.Contains("deleteFriendScript") && data["deleteFriendScript"] != null ? ScriptSetting.FromDict(data["deleteFriendScript"]) : null,
                updateProfileScript = data.Keys.Contains("updateProfileScript") && data["updateProfileScript"] != null ? ScriptSetting.FromDict(data["updateProfileScript"]) : null,
                followNotification = data.Keys.Contains("followNotification") && data["followNotification"] != null ? NotificationSetting.FromDict(data["followNotification"]) : null,
                receiveRequestNotification = data.Keys.Contains("receiveRequestNotification") && data["receiveRequestNotification"] != null ? NotificationSetting.FromDict(data["receiveRequestNotification"]) : null,
                acceptRequestNotification = data.Keys.Contains("acceptRequestNotification") && data["acceptRequestNotification"] != null ? NotificationSetting.FromDict(data["acceptRequestNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}