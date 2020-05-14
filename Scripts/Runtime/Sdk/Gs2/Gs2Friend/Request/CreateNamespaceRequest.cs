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
	[System.Serializable]
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string name;

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
		[UnityEngine.SerializeField]
        public string description;

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
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting followScript;

        /**
         * フォローされたときに実行するスクリプトを設定
         *
         * @param followScript フォローされたときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithFollowScript(global::Gs2.Gs2Friend.Model.ScriptSetting followScript) {
            this.followScript = followScript;
            return this;
        }


        /** アンフォローされたときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting unfollowScript;

        /**
         * アンフォローされたときに実行するスクリプトを設定
         *
         * @param unfollowScript アンフォローされたときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithUnfollowScript(global::Gs2.Gs2Friend.Model.ScriptSetting unfollowScript) {
            this.unfollowScript = unfollowScript;
            return this;
        }


        /** フレンドリクエストを発行したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting sendRequestScript;

        /**
         * フレンドリクエストを発行したときに実行するスクリプトを設定
         *
         * @param sendRequestScript フレンドリクエストを発行したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithSendRequestScript(global::Gs2.Gs2Friend.Model.ScriptSetting sendRequestScript) {
            this.sendRequestScript = sendRequestScript;
            return this;
        }


        /** フレンドリクエストをキャンセルしたときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting cancelRequestScript;

        /**
         * フレンドリクエストをキャンセルしたときに実行するスクリプトを設定
         *
         * @param cancelRequestScript フレンドリクエストをキャンセルしたときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithCancelRequestScript(global::Gs2.Gs2Friend.Model.ScriptSetting cancelRequestScript) {
            this.cancelRequestScript = cancelRequestScript;
            return this;
        }


        /** フレンドリクエストを承諾したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting acceptRequestScript;

        /**
         * フレンドリクエストを承諾したときに実行するスクリプトを設定
         *
         * @param acceptRequestScript フレンドリクエストを承諾したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithAcceptRequestScript(global::Gs2.Gs2Friend.Model.ScriptSetting acceptRequestScript) {
            this.acceptRequestScript = acceptRequestScript;
            return this;
        }


        /** フレンドリクエストを拒否したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting rejectRequestScript;

        /**
         * フレンドリクエストを拒否したときに実行するスクリプトを設定
         *
         * @param rejectRequestScript フレンドリクエストを拒否したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithRejectRequestScript(global::Gs2.Gs2Friend.Model.ScriptSetting rejectRequestScript) {
            this.rejectRequestScript = rejectRequestScript;
            return this;
        }


        /** フレンドを削除したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting deleteFriendScript;

        /**
         * フレンドを削除したときに実行するスクリプトを設定
         *
         * @param deleteFriendScript フレンドを削除したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithDeleteFriendScript(global::Gs2.Gs2Friend.Model.ScriptSetting deleteFriendScript) {
            this.deleteFriendScript = deleteFriendScript;
            return this;
        }


        /** プロフィールを更新したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.ScriptSetting updateProfileScript;

        /**
         * プロフィールを更新したときに実行するスクリプトを設定
         *
         * @param updateProfileScript プロフィールを更新したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithUpdateProfileScript(global::Gs2.Gs2Friend.Model.ScriptSetting updateProfileScript) {
            this.updateProfileScript = updateProfileScript;
            return this;
        }


        /** フォローされたときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.NotificationSetting followNotification;

        /**
         * フォローされたときのプッシュ通知を設定
         *
         * @param followNotification フォローされたときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithFollowNotification(global::Gs2.Gs2Friend.Model.NotificationSetting followNotification) {
            this.followNotification = followNotification;
            return this;
        }


        /** フレンドリクエストが届いたときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.NotificationSetting receiveRequestNotification;

        /**
         * フレンドリクエストが届いたときのプッシュ通知を設定
         *
         * @param receiveRequestNotification フレンドリクエストが届いたときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithReceiveRequestNotification(global::Gs2.Gs2Friend.Model.NotificationSetting receiveRequestNotification) {
            this.receiveRequestNotification = receiveRequestNotification;
            return this;
        }


        /** フレンドリクエストが承認されたときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.NotificationSetting acceptRequestNotification;

        /**
         * フレンドリクエストが承認されたときのプッシュ通知を設定
         *
         * @param acceptRequestNotification フレンドリクエストが承認されたときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithAcceptRequestNotification(global::Gs2.Gs2Friend.Model.NotificationSetting acceptRequestNotification) {
            this.acceptRequestNotification = acceptRequestNotification;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Friend.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public CreateNamespaceRequest WithLogSetting(global::Gs2.Gs2Friend.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static CreateNamespaceRequest FromDict(JsonData data)
        {
            return new CreateNamespaceRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                followScript = data.Keys.Contains("followScript") && data["followScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["followScript"]) : null,
                unfollowScript = data.Keys.Contains("unfollowScript") && data["unfollowScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["unfollowScript"]) : null,
                sendRequestScript = data.Keys.Contains("sendRequestScript") && data["sendRequestScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["sendRequestScript"]) : null,
                cancelRequestScript = data.Keys.Contains("cancelRequestScript") && data["cancelRequestScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["cancelRequestScript"]) : null,
                acceptRequestScript = data.Keys.Contains("acceptRequestScript") && data["acceptRequestScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["acceptRequestScript"]) : null,
                rejectRequestScript = data.Keys.Contains("rejectRequestScript") && data["rejectRequestScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["rejectRequestScript"]) : null,
                deleteFriendScript = data.Keys.Contains("deleteFriendScript") && data["deleteFriendScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["deleteFriendScript"]) : null,
                updateProfileScript = data.Keys.Contains("updateProfileScript") && data["updateProfileScript"] != null ? global::Gs2.Gs2Friend.Model.ScriptSetting.FromDict(data["updateProfileScript"]) : null,
                followNotification = data.Keys.Contains("followNotification") && data["followNotification"] != null ? global::Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["followNotification"]) : null,
                receiveRequestNotification = data.Keys.Contains("receiveRequestNotification") && data["receiveRequestNotification"] != null ? global::Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["receiveRequestNotification"]) : null,
                acceptRequestNotification = data.Keys.Contains("acceptRequestNotification") && data["acceptRequestNotification"] != null ? global::Gs2.Gs2Friend.Model.NotificationSetting.FromDict(data["acceptRequestNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Friend.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}