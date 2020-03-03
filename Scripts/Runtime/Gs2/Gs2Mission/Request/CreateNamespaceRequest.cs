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
using Gs2.Gs2Mission.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Request
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


        /** ミッションを達成したときに実行するスクリプト */
        public Gs2.Gs2Mission.Model.ScriptSetting missionCompleteScript { set; get; }

        /**
         * ミッションを達成したときに実行するスクリプトを設定
         *
         * @param missionCompleteScript ミッションを達成したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithMissionCompleteScript(Gs2.Gs2Mission.Model.ScriptSetting missionCompleteScript) {
            this.missionCompleteScript = missionCompleteScript;
            return this;
        }


        /** カウンターを上昇したときに実行するスクリプト */
        public Gs2.Gs2Mission.Model.ScriptSetting counterIncrementScript { set; get; }

        /**
         * カウンターを上昇したときに実行するスクリプトを設定
         *
         * @param counterIncrementScript カウンターを上昇したときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithCounterIncrementScript(Gs2.Gs2Mission.Model.ScriptSetting counterIncrementScript) {
            this.counterIncrementScript = counterIncrementScript;
            return this;
        }


        /** 報酬を受け取ったときに実行するスクリプト */
        public Gs2.Gs2Mission.Model.ScriptSetting receiveRewardsScript { set; get; }

        /**
         * 報酬を受け取ったときに実行するスクリプトを設定
         *
         * @param receiveRewardsScript 報酬を受け取ったときに実行するスクリプト
         * @return this
         */
        public CreateNamespaceRequest WithReceiveRewardsScript(Gs2.Gs2Mission.Model.ScriptSetting receiveRewardsScript) {
            this.receiveRewardsScript = receiveRewardsScript;
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


        /** ミッションのタスクを達成したときのプッシュ通知 */
        public Gs2.Gs2Mission.Model.NotificationSetting completeNotification { set; get; }

        /**
         * ミッションのタスクを達成したときのプッシュ通知を設定
         *
         * @param completeNotification ミッションのタスクを達成したときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithCompleteNotification(Gs2.Gs2Mission.Model.NotificationSetting completeNotification) {
            this.completeNotification = completeNotification;
            return this;
        }


        /** ログの出力設定 */
        public Gs2.Gs2Mission.Model.LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public CreateNamespaceRequest WithLogSetting(Gs2.Gs2Mission.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static CreateNamespaceRequest FromDict(JsonData data)
        {
            return new CreateNamespaceRequest {
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                missionCompleteScript = data.Keys.Contains("missionCompleteScript") && data["missionCompleteScript"] != null ? Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["missionCompleteScript"]) : null,
                counterIncrementScript = data.Keys.Contains("counterIncrementScript") && data["counterIncrementScript"] != null ? Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["counterIncrementScript"]) : null,
                receiveRewardsScript = data.Keys.Contains("receiveRewardsScript") && data["receiveRewardsScript"] != null ? Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["receiveRewardsScript"]) : null,
                queueNamespaceId = data.Keys.Contains("queueNamespaceId") && data["queueNamespaceId"] != null ? data["queueNamespaceId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                completeNotification = data.Keys.Contains("completeNotification") && data["completeNotification"] != null ? Gs2.Gs2Mission.Model.NotificationSetting.FromDict(data["completeNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? Gs2.Gs2Mission.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}