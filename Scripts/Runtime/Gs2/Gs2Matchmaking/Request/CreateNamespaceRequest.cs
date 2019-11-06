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
using Gs2.Gs2Matchmaking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Matchmaking.Request
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


        /** ギャザリング新規作成時のアクション */
        public string createGatheringTriggerType { set; get; }

        /**
         * ギャザリング新規作成時のアクションを設定
         *
         * @param createGatheringTriggerType ギャザリング新規作成時のアクション
         * @return this
         */
        public CreateNamespaceRequest WithCreateGatheringTriggerType(string createGatheringTriggerType) {
            this.createGatheringTriggerType = createGatheringTriggerType;
            return this;
        }


        /** ギャザリング新規作成時 にルームを作成するネームスペース のGRN */
        public string createGatheringTriggerRealtimeNamespaceId { set; get; }

        /**
         * ギャザリング新規作成時 にルームを作成するネームスペース のGRNを設定
         *
         * @param createGatheringTriggerRealtimeNamespaceId ギャザリング新規作成時 にルームを作成するネームスペース のGRN
         * @return this
         */
        public CreateNamespaceRequest WithCreateGatheringTriggerRealtimeNamespaceId(string createGatheringTriggerRealtimeNamespaceId) {
            this.createGatheringTriggerRealtimeNamespaceId = createGatheringTriggerRealtimeNamespaceId;
            return this;
        }


        /** ギャザリング新規作成時 に実行されるスクリプト のGRN */
        public string createGatheringTriggerScriptId { set; get; }

        /**
         * ギャザリング新規作成時 に実行されるスクリプト のGRNを設定
         *
         * @param createGatheringTriggerScriptId ギャザリング新規作成時 に実行されるスクリプト のGRN
         * @return this
         */
        public CreateNamespaceRequest WithCreateGatheringTriggerScriptId(string createGatheringTriggerScriptId) {
            this.createGatheringTriggerScriptId = createGatheringTriggerScriptId;
            return this;
        }


        /** マッチメイキング完了時のアクション */
        public string completeMatchmakingTriggerType { set; get; }

        /**
         * マッチメイキング完了時のアクションを設定
         *
         * @param completeMatchmakingTriggerType マッチメイキング完了時のアクション
         * @return this
         */
        public CreateNamespaceRequest WithCompleteMatchmakingTriggerType(string completeMatchmakingTriggerType) {
            this.completeMatchmakingTriggerType = completeMatchmakingTriggerType;
            return this;
        }


        /** マッチメイキング完了時 にルームを作成するネームスペース のGRN */
        public string completeMatchmakingTriggerRealtimeNamespaceId { set; get; }

        /**
         * マッチメイキング完了時 にルームを作成するネームスペース のGRNを設定
         *
         * @param completeMatchmakingTriggerRealtimeNamespaceId マッチメイキング完了時 にルームを作成するネームスペース のGRN
         * @return this
         */
        public CreateNamespaceRequest WithCompleteMatchmakingTriggerRealtimeNamespaceId(string completeMatchmakingTriggerRealtimeNamespaceId) {
            this.completeMatchmakingTriggerRealtimeNamespaceId = completeMatchmakingTriggerRealtimeNamespaceId;
            return this;
        }


        /** マッチメイキング完了時 に実行されるスクリプト のGRN */
        public string completeMatchmakingTriggerScriptId { set; get; }

        /**
         * マッチメイキング完了時 に実行されるスクリプト のGRNを設定
         *
         * @param completeMatchmakingTriggerScriptId マッチメイキング完了時 に実行されるスクリプト のGRN
         * @return this
         */
        public CreateNamespaceRequest WithCompleteMatchmakingTriggerScriptId(string completeMatchmakingTriggerScriptId) {
            this.completeMatchmakingTriggerScriptId = completeMatchmakingTriggerScriptId;
            return this;
        }


        /** ギャザリングに新規プレイヤーが参加したときのプッシュ通知 */
        public NotificationSetting joinNotification { set; get; }

        /**
         * ギャザリングに新規プレイヤーが参加したときのプッシュ通知を設定
         *
         * @param joinNotification ギャザリングに新規プレイヤーが参加したときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithJoinNotification(NotificationSetting joinNotification) {
            this.joinNotification = joinNotification;
            return this;
        }


        /** ギャザリングからプレイヤーが離脱したときのプッシュ通知 */
        public NotificationSetting leaveNotification { set; get; }

        /**
         * ギャザリングからプレイヤーが離脱したときのプッシュ通知を設定
         *
         * @param leaveNotification ギャザリングからプレイヤーが離脱したときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithLeaveNotification(NotificationSetting leaveNotification) {
            this.leaveNotification = leaveNotification;
            return this;
        }


        /** マッチメイキングが完了したときのプッシュ通知 */
        public NotificationSetting completeNotification { set; get; }

        /**
         * マッチメイキングが完了したときのプッシュ通知を設定
         *
         * @param completeNotification マッチメイキングが完了したときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithCompleteNotification(NotificationSetting completeNotification) {
            this.completeNotification = completeNotification;
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
                createGatheringTriggerType = data.Keys.Contains("createGatheringTriggerType") && data["createGatheringTriggerType"] != null ? data["createGatheringTriggerType"].ToString(): null,
                createGatheringTriggerRealtimeNamespaceId = data.Keys.Contains("createGatheringTriggerRealtimeNamespaceId") && data["createGatheringTriggerRealtimeNamespaceId"] != null ? data["createGatheringTriggerRealtimeNamespaceId"].ToString(): null,
                createGatheringTriggerScriptId = data.Keys.Contains("createGatheringTriggerScriptId") && data["createGatheringTriggerScriptId"] != null ? data["createGatheringTriggerScriptId"].ToString(): null,
                completeMatchmakingTriggerType = data.Keys.Contains("completeMatchmakingTriggerType") && data["completeMatchmakingTriggerType"] != null ? data["completeMatchmakingTriggerType"].ToString(): null,
                completeMatchmakingTriggerRealtimeNamespaceId = data.Keys.Contains("completeMatchmakingTriggerRealtimeNamespaceId") && data["completeMatchmakingTriggerRealtimeNamespaceId"] != null ? data["completeMatchmakingTriggerRealtimeNamespaceId"].ToString(): null,
                completeMatchmakingTriggerScriptId = data.Keys.Contains("completeMatchmakingTriggerScriptId") && data["completeMatchmakingTriggerScriptId"] != null ? data["completeMatchmakingTriggerScriptId"].ToString(): null,
                joinNotification = data.Keys.Contains("joinNotification") && data["joinNotification"] != null ? NotificationSetting.FromDict(data["joinNotification"]) : null,
                leaveNotification = data.Keys.Contains("leaveNotification") && data["leaveNotification"] != null ? NotificationSetting.FromDict(data["leaveNotification"]) : null,
                completeNotification = data.Keys.Contains("completeNotification") && data["completeNotification"] != null ? NotificationSetting.FromDict(data["completeNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}