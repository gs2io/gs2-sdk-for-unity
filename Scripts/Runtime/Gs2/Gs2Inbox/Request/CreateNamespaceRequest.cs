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
using Gs2.Gs2Inbox.Model;

namespace Gs2.Gs2Inbox.Request
{
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** プレゼントボックス名 */
        public string name { set; get; }

        /**
         * プレゼントボックス名を設定
         *
         * @param name プレゼントボックス名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** 説明文 */
        public string description { set; get; }

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public CreateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** 開封したメッセージを自動的に削除するか */
        public bool? isAutomaticDeletingEnabled { set; get; }

        /**
         * 開封したメッセージを自動的に削除するかを設定
         *
         * @param isAutomaticDeletingEnabled 開封したメッセージを自動的に削除するか
         * @return this
         */
        public CreateNamespaceRequest WithIsAutomaticDeletingEnabled(bool? isAutomaticDeletingEnabled) {
            this.isAutomaticDeletingEnabled = isAutomaticDeletingEnabled;
            return this;
        }


        /** メッセージ受信したときに実行するスクリプト */
        public ScriptSetting receiveMessageScript { set; get; }

        /**
         * メッセージ受信したときに実行するスクリプトを設定
         *
         * @param receiveMessageScript メッセージ受信したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithReceiveMessageScript(ScriptSetting receiveMessageScript) {
            this.receiveMessageScript = receiveMessageScript;
            return this;
        }


        /** メッセージ開封したときに実行するスクリプト */
        public ScriptSetting readMessageScript { set; get; }

        /**
         * メッセージ開封したときに実行するスクリプトを設定
         *
         * @param readMessageScript メッセージ開封したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithReadMessageScript(ScriptSetting readMessageScript) {
            this.readMessageScript = readMessageScript;
            return this;
        }


        /** メッセージ削除したときに実行するスクリプト */
        public ScriptSetting deleteMessageScript { set; get; }

        /**
         * メッセージ削除したときに実行するスクリプトを設定
         *
         * @param deleteMessageScript メッセージ削除したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithDeleteMessageScript(ScriptSetting deleteMessageScript) {
            this.deleteMessageScript = deleteMessageScript;
            return this;
        }


        /** 報酬付与処理をジョブとして追加するキューネームスペース のGRN */
        public string queueNamespaceId { set; get; }

        /**
         * 報酬付与処理をジョブとして追加するキューネームスペース のGRNを設定
         *
         * @param queueNamespaceId 報酬付与処理をジョブとして追加するキューネームスペース のGRN
         * @return this
         */
        public CreateNamespaceRequest WithQueueNamespaceId(string queueNamespaceId) {
            this.queueNamespaceId = queueNamespaceId;
            return this;
        }


        /** 報酬付与処理のスタンプシートで使用する暗号鍵GRN */
        public string keyId { set; get; }

        /**
         * 報酬付与処理のスタンプシートで使用する暗号鍵GRNを設定
         *
         * @param keyId 報酬付与処理のスタンプシートで使用する暗号鍵GRN
         * @return this
         */
        public CreateNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** メッセージを受信したときのプッシュ通知 */
        public NotificationSetting receiveNotification { set; get; }

        /**
         * メッセージを受信したときのプッシュ通知を設定
         *
         * @param receiveNotification メッセージを受信したときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithReceiveNotification(NotificationSetting receiveNotification) {
            this.receiveNotification = receiveNotification;
            return this;
        }


	}
}