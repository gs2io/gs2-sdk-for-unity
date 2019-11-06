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
using Gs2.Gs2Chat.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Request
{
	[Preserve]
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


        /** ゲームプレイヤーによるルームの作成を許可するか */
        public bool? allowCreateRoom { set; get; }

        /**
         * ゲームプレイヤーによるルームの作成を許可するかを設定
         *
         * @param allowCreateRoom ゲームプレイヤーによるルームの作成を許可するか
         * @return this
         */
        public UpdateNamespaceRequest WithAllowCreateRoom(bool? allowCreateRoom) {
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
        public UpdateNamespaceRequest WithPostMessageScript(ScriptSetting postMessageScript) {
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
        public UpdateNamespaceRequest WithCreateRoomScript(ScriptSetting createRoomScript) {
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
        public UpdateNamespaceRequest WithDeleteRoomScript(ScriptSetting deleteRoomScript) {
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
        public UpdateNamespaceRequest WithSubscribeRoomScript(ScriptSetting subscribeRoomScript) {
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
        public UpdateNamespaceRequest WithUnsubscribeRoomScript(ScriptSetting unsubscribeRoomScript) {
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
        public UpdateNamespaceRequest WithPostNotification(NotificationSetting postNotification) {
            this.postNotification = postNotification;
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
        public UpdateNamespaceRequest WithLogSetting(LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                allowCreateRoom = data.Keys.Contains("allowCreateRoom") && data["allowCreateRoom"] != null ? (bool?)bool.Parse(data["allowCreateRoom"].ToString()) : null,
                postMessageScript = data.Keys.Contains("postMessageScript") && data["postMessageScript"] != null ? ScriptSetting.FromDict(data["postMessageScript"]) : null,
                createRoomScript = data.Keys.Contains("createRoomScript") && data["createRoomScript"] != null ? ScriptSetting.FromDict(data["createRoomScript"]) : null,
                deleteRoomScript = data.Keys.Contains("deleteRoomScript") && data["deleteRoomScript"] != null ? ScriptSetting.FromDict(data["deleteRoomScript"]) : null,
                subscribeRoomScript = data.Keys.Contains("subscribeRoomScript") && data["subscribeRoomScript"] != null ? ScriptSetting.FromDict(data["subscribeRoomScript"]) : null,
                unsubscribeRoomScript = data.Keys.Contains("unsubscribeRoomScript") && data["unsubscribeRoomScript"] != null ? ScriptSetting.FromDict(data["unsubscribeRoomScript"]) : null,
                postNotification = data.Keys.Contains("postNotification") && data["postNotification"] != null ? NotificationSetting.FromDict(data["postNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}