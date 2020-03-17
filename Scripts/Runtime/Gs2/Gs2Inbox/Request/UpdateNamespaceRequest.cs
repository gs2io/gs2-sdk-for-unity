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
using Gs2.Gs2Inbox.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
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
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 説明文 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 開封したメッセージを自動的に削除するか */
		[UnityEngine.SerializeField]
        public bool? isAutomaticDeletingEnabled;

        /**
         * 開封したメッセージを自動的に削除するかを設定
         *
         * @param isAutomaticDeletingEnabled 開封したメッセージを自動的に削除するか
         * @return this
         */
        public UpdateNamespaceRequest WithIsAutomaticDeletingEnabled(bool? isAutomaticDeletingEnabled) {
            this.isAutomaticDeletingEnabled = isAutomaticDeletingEnabled;
            return this;
        }


        /** メッセージ受信したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.ScriptSetting receiveMessageScript;

        /**
         * メッセージ受信したときに実行するスクリプトを設定
         *
         * @param receiveMessageScript メッセージ受信したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveMessageScript(global::Gs2.Gs2Inbox.Model.ScriptSetting receiveMessageScript) {
            this.receiveMessageScript = receiveMessageScript;
            return this;
        }


        /** メッセージ開封したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.ScriptSetting readMessageScript;

        /**
         * メッセージ開封したときに実行するスクリプトを設定
         *
         * @param readMessageScript メッセージ開封したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithReadMessageScript(global::Gs2.Gs2Inbox.Model.ScriptSetting readMessageScript) {
            this.readMessageScript = readMessageScript;
            return this;
        }


        /** メッセージ削除したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.ScriptSetting deleteMessageScript;

        /**
         * メッセージ削除したときに実行するスクリプトを設定
         *
         * @param deleteMessageScript メッセージ削除したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithDeleteMessageScript(global::Gs2.Gs2Inbox.Model.ScriptSetting deleteMessageScript) {
            this.deleteMessageScript = deleteMessageScript;
            return this;
        }


        /** 報酬付与処理をジョブとして追加するキューネームスペース のGRN */
		[UnityEngine.SerializeField]
        public string queueNamespaceId;

        /**
         * 報酬付与処理をジョブとして追加するキューネームスペース のGRNを設定
         *
         * @param queueNamespaceId 報酬付与処理をジョブとして追加するキューネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithQueueNamespaceId(string queueNamespaceId) {
            this.queueNamespaceId = queueNamespaceId;
            return this;
        }


        /** 報酬付与処理のスタンプシートで使用する暗号鍵GRN */
		[UnityEngine.SerializeField]
        public string keyId;

        /**
         * 報酬付与処理のスタンプシートで使用する暗号鍵GRNを設定
         *
         * @param keyId 報酬付与処理のスタンプシートで使用する暗号鍵GRN
         * @return this
         */
        public UpdateNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** メッセージを受信したときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.NotificationSetting receiveNotification;

        /**
         * メッセージを受信したときのプッシュ通知を設定
         *
         * @param receiveNotification メッセージを受信したときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveNotification(global::Gs2.Gs2Inbox.Model.NotificationSetting receiveNotification) {
            this.receiveNotification = receiveNotification;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Inbox.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(global::Gs2.Gs2Inbox.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                isAutomaticDeletingEnabled = data.Keys.Contains("isAutomaticDeletingEnabled") && data["isAutomaticDeletingEnabled"] != null ? (bool?)bool.Parse(data["isAutomaticDeletingEnabled"].ToString()) : null,
                receiveMessageScript = data.Keys.Contains("receiveMessageScript") && data["receiveMessageScript"] != null ? global::Gs2.Gs2Inbox.Model.ScriptSetting.FromDict(data["receiveMessageScript"]) : null,
                readMessageScript = data.Keys.Contains("readMessageScript") && data["readMessageScript"] != null ? global::Gs2.Gs2Inbox.Model.ScriptSetting.FromDict(data["readMessageScript"]) : null,
                deleteMessageScript = data.Keys.Contains("deleteMessageScript") && data["deleteMessageScript"] != null ? global::Gs2.Gs2Inbox.Model.ScriptSetting.FromDict(data["deleteMessageScript"]) : null,
                queueNamespaceId = data.Keys.Contains("queueNamespaceId") && data["queueNamespaceId"] != null ? data["queueNamespaceId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                receiveNotification = data.Keys.Contains("receiveNotification") && data["receiveNotification"] != null ? global::Gs2.Gs2Inbox.Model.NotificationSetting.FromDict(data["receiveNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Inbox.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}