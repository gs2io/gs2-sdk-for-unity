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
using Gs2.Gs2Mission.Model;

namespace Gs2.Gs2Mission.Request
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


        /** ミッション達成時 に実行されるスクリプト のGRN */
        public string missionCompleteTriggerScriptId { set; get; }

        /**
         * ミッション達成時 に実行されるスクリプト のGRNを設定
         *
         * @param missionCompleteTriggerScriptId ミッション達成時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithMissionCompleteTriggerScriptId(string missionCompleteTriggerScriptId) {
            this.missionCompleteTriggerScriptId = missionCompleteTriggerScriptId;
            return this;
        }


        /** ミッション達成完了時 に実行されるスクリプト のGRN */
        public string missionCompleteDoneTriggerScriptId { set; get; }

        /**
         * ミッション達成完了時 に実行されるスクリプト のGRNを設定
         *
         * @param missionCompleteDoneTriggerScriptId ミッション達成完了時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithMissionCompleteDoneTriggerScriptId(string missionCompleteDoneTriggerScriptId) {
            this.missionCompleteDoneTriggerScriptId = missionCompleteDoneTriggerScriptId;
            return this;
        }


        /** ミッション達成完了時 にジョブが登録されるネームスペース のGRN */
        public string missionCompleteDoneTriggerQueueNamespaceId { set; get; }

        /**
         * ミッション達成完了時 にジョブが登録されるネームスペース のGRNを設定
         *
         * @param missionCompleteDoneTriggerQueueNamespaceId ミッション達成完了時 にジョブが登録されるネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithMissionCompleteDoneTriggerQueueNamespaceId(string missionCompleteDoneTriggerQueueNamespaceId) {
            this.missionCompleteDoneTriggerQueueNamespaceId = missionCompleteDoneTriggerQueueNamespaceId;
            return this;
        }


        /** カウンター上昇時 に実行されるスクリプト のGRN */
        public string counterIncrementTriggerScriptId { set; get; }

        /**
         * カウンター上昇時 に実行されるスクリプト のGRNを設定
         *
         * @param counterIncrementTriggerScriptId カウンター上昇時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithCounterIncrementTriggerScriptId(string counterIncrementTriggerScriptId) {
            this.counterIncrementTriggerScriptId = counterIncrementTriggerScriptId;
            return this;
        }


        /** カウンター上昇完了時 に実行されるスクリプト のGRN */
        public string counterIncrementDoneTriggerScriptId { set; get; }

        /**
         * カウンター上昇完了時 に実行されるスクリプト のGRNを設定
         *
         * @param counterIncrementDoneTriggerScriptId カウンター上昇完了時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithCounterIncrementDoneTriggerScriptId(string counterIncrementDoneTriggerScriptId) {
            this.counterIncrementDoneTriggerScriptId = counterIncrementDoneTriggerScriptId;
            return this;
        }


        /** カウンター上昇完了時 にジョブが登録されるネームスペース のGRN */
        public string counterIncrementDoneTriggerQueueNamespaceId { set; get; }

        /**
         * カウンター上昇完了時 にジョブが登録されるネームスペース のGRNを設定
         *
         * @param counterIncrementDoneTriggerQueueNamespaceId カウンター上昇完了時 にジョブが登録されるネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithCounterIncrementDoneTriggerQueueNamespaceId(string counterIncrementDoneTriggerQueueNamespaceId) {
            this.counterIncrementDoneTriggerQueueNamespaceId = counterIncrementDoneTriggerQueueNamespaceId;
            return this;
        }


        /** 報酬受け取り時 に実行されるスクリプト のGRN */
        public string receiveRewardsTriggerScriptId { set; get; }

        /**
         * 報酬受け取り時 に実行されるスクリプト のGRNを設定
         *
         * @param receiveRewardsTriggerScriptId 報酬受け取り時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveRewardsTriggerScriptId(string receiveRewardsTriggerScriptId) {
            this.receiveRewardsTriggerScriptId = receiveRewardsTriggerScriptId;
            return this;
        }


        /** 報酬受け取り完了時 に実行されるスクリプト のGRN */
        public string receiveRewardsDoneTriggerScriptId { set; get; }

        /**
         * 報酬受け取り完了時 に実行されるスクリプト のGRNを設定
         *
         * @param receiveRewardsDoneTriggerScriptId 報酬受け取り完了時 に実行されるスクリプト のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveRewardsDoneTriggerScriptId(string receiveRewardsDoneTriggerScriptId) {
            this.receiveRewardsDoneTriggerScriptId = receiveRewardsDoneTriggerScriptId;
            return this;
        }


        /** 報酬受け取り完了時 にジョブが登録されるネームスペース のGRN */
        public string receiveRewardsDoneTriggerQueueNamespaceId { set; get; }

        /**
         * 報酬受け取り完了時 にジョブが登録されるネームスペース のGRNを設定
         *
         * @param receiveRewardsDoneTriggerQueueNamespaceId 報酬受け取り完了時 にジョブが登録されるネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithReceiveRewardsDoneTriggerQueueNamespaceId(string receiveRewardsDoneTriggerQueueNamespaceId) {
            this.receiveRewardsDoneTriggerQueueNamespaceId = receiveRewardsDoneTriggerQueueNamespaceId;
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
        public UpdateNamespaceRequest WithQueueNamespaceId(string queueNamespaceId) {
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
        public UpdateNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


        /** ミッションのタスクを達成したときのプッシュ通知 */
        public NotificationSetting completeNotification { set; get; }

        /**
         * ミッションのタスクを達成したときのプッシュ通知を設定
         *
         * @param completeNotification ミッションのタスクを達成したときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithCompleteNotification(NotificationSetting completeNotification) {
            this.completeNotification = completeNotification;
            return this;
        }


	}
}