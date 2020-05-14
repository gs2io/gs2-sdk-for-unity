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


        /** ネームスペースの説明 */
		[UnityEngine.SerializeField]
        public string description;

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


        /** ミッションを達成したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Mission.Model.ScriptSetting missionCompleteScript;

        /**
         * ミッションを達成したときに実行するスクリプトを設定
         *
         * @param missionCompleteScript ミッションを達成したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithMissionCompleteScript(global::Gs2.Gs2Mission.Model.ScriptSetting missionCompleteScript) {
            this.missionCompleteScript = missionCompleteScript;
            return this;
        }


        /** カウンターを上昇したときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Mission.Model.ScriptSetting counterIncrementScript;

        /**
         * カウンターを上昇したときに実行するスクリプトを設定
         *
         * @param counterIncrementScript カウンターを上昇したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCounterIncrementScript(global::Gs2.Gs2Mission.Model.ScriptSetting counterIncrementScript) {
            this.counterIncrementScript = counterIncrementScript;
            return this;
        }


        /** 報酬を受け取ったときに実行するスクリプト */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Mission.Model.ScriptSetting receiveRewardsScript;

        /**
         * 報酬を受け取ったときに実行するスクリプトを設定
         *
         * @param receiveRewardsScript 報酬を受け取ったときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveRewardsScript(global::Gs2.Gs2Mission.Model.ScriptSetting receiveRewardsScript) {
            this.receiveRewardsScript = receiveRewardsScript;
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


        /** ミッションのタスクを達成したときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Mission.Model.NotificationSetting completeNotification;

        /**
         * ミッションのタスクを達成したときのプッシュ通知を設定
         *
         * @param completeNotification ミッションのタスクを達成したときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithCompleteNotification(global::Gs2.Gs2Mission.Model.NotificationSetting completeNotification) {
            this.completeNotification = completeNotification;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Mission.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(global::Gs2.Gs2Mission.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                missionCompleteScript = data.Keys.Contains("missionCompleteScript") && data["missionCompleteScript"] != null ? global::Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["missionCompleteScript"]) : null,
                counterIncrementScript = data.Keys.Contains("counterIncrementScript") && data["counterIncrementScript"] != null ? global::Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["counterIncrementScript"]) : null,
                receiveRewardsScript = data.Keys.Contains("receiveRewardsScript") && data["receiveRewardsScript"] != null ? global::Gs2.Gs2Mission.Model.ScriptSetting.FromDict(data["receiveRewardsScript"]) : null,
                queueNamespaceId = data.Keys.Contains("queueNamespaceId") && data["queueNamespaceId"] != null ? data["queueNamespaceId"].ToString(): null,
                keyId = data.Keys.Contains("keyId") && data["keyId"] != null ? data["keyId"].ToString(): null,
                completeNotification = data.Keys.Contains("completeNotification") && data["completeNotification"] != null ? global::Gs2.Gs2Mission.Model.NotificationSetting.FromDict(data["completeNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Mission.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}