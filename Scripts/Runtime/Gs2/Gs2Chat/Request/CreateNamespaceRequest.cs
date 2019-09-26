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
using Gs2.Gs2Chat.Model;

namespace Gs2.Gs2Chat.Request
{
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


        /** ゲームプレイヤーによるルームの作成を許可するか */
        public bool? allowCreateRoom { set; get; }

        /**
         * ゲームプレイヤーによるルームの作成を許可するかを設定
         *
         * @param allowCreateRoom ゲームプレイヤーによるルームの作成を許可するか
         * @return this
         */
        public CreateNamespaceRequest WithAllowCreateRoom(bool? allowCreateRoom) {
            this.allowCreateRoom = allowCreateRoom;
            return this;
        }


        /** メッセージを投稿したときに実行するスクリプト */
        public ScriptSetting postMessageScript { set; get; }

        /**
         * メッセージを投稿したときに実行するスクリプトを設定
         *
         * @param postMessageScript メッセージを投稿したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithPostMessageScript(ScriptSetting postMessageScript) {
            this.postMessageScript = postMessageScript;
            return this;
        }


        /** ルームを作成したときに実行するスクリプト */
        public ScriptSetting createRoomScript { set; get; }

        /**
         * ルームを作成したときに実行するスクリプトを設定
         *
         * @param createRoomScript ルームを作成したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithCreateRoomScript(ScriptSetting createRoomScript) {
            this.createRoomScript = createRoomScript;
            return this;
        }


        /** ルームを削除したときに実行するスクリプト */
        public ScriptSetting deleteRoomScript { set; get; }

        /**
         * ルームを削除したときに実行するスクリプトを設定
         *
         * @param deleteRoomScript ルームを削除したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithDeleteRoomScript(ScriptSetting deleteRoomScript) {
            this.deleteRoomScript = deleteRoomScript;
            return this;
        }


        /** ルームを購読したときに実行するスクリプト */
        public ScriptSetting subscribeRoomScript { set; get; }

        /**
         * ルームを購読したときに実行するスクリプトを設定
         *
         * @param subscribeRoomScript ルームを購読したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithSubscribeRoomScript(ScriptSetting subscribeRoomScript) {
            this.subscribeRoomScript = subscribeRoomScript;
            return this;
        }


        /** ルームの購読を解除したときに実行するスクリプト */
        public ScriptSetting unsubscribeRoomScript { set; get; }

        /**
         * ルームの購読を解除したときに実行するスクリプトを設定
         *
         * @param unsubscribeRoomScript ルームの購読を解除したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithUnsubscribeRoomScript(ScriptSetting unsubscribeRoomScript) {
            this.unsubscribeRoomScript = unsubscribeRoomScript;
            return this;
        }


        /** 購読しているルームに新しい投稿がきたときのプッシュ通知 */
        public NotificationSetting postNotification { set; get; }

        /**
         * 購読しているルームに新しい投稿がきたときのプッシュ通知を設定
         *
         * @param postNotification 購読しているルームに新しい投稿がきたときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithPostNotification(NotificationSetting postNotification) {
            this.postNotification = postNotification;
            return this;
        }


	}
}