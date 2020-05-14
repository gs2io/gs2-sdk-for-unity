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
	public class UpdateMissionTaskModelMasterRequest : Gs2Request<UpdateMissionTaskModelMasterRequest>
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
        public UpdateMissionTaskModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ミッショングループ名 */
		[UnityEngine.SerializeField]
        public string missionGroupName;

        /**
         * ミッショングループ名を設定
         *
         * @param missionGroupName ミッショングループ名
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithMissionGroupName(string missionGroupName) {
            this.missionGroupName = missionGroupName;
            return this;
        }


        /** タスク名 */
		[UnityEngine.SerializeField]
        public string missionTaskName;

        /**
         * タスク名を設定
         *
         * @param missionTaskName タスク名
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithMissionTaskName(string missionTaskName) {
            this.missionTaskName = missionTaskName;
            return this;
        }


        /** メタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** ミッションタスクの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * ミッションタスクの説明を設定
         *
         * @param description ミッションタスクの説明
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** カウンター名 */
		[UnityEngine.SerializeField]
        public string counterName;

        /**
         * カウンター名を設定
         *
         * @param counterName カウンター名
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithCounterName(string counterName) {
            this.counterName = counterName;
            return this;
        }


        /** 目標値 */
		[UnityEngine.SerializeField]
        public long? targetValue;

        /**
         * 目標値を設定
         *
         * @param targetValue 目標値
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithTargetValue(long? targetValue) {
            this.targetValue = targetValue;
            return this;
        }


        /** ミッション達成時の報酬 */
		[UnityEngine.SerializeField]
        public List<AcquireAction> completeAcquireActions;

        /**
         * ミッション達成時の報酬を設定
         *
         * @param completeAcquireActions ミッション達成時の報酬
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithCompleteAcquireActions(List<AcquireAction> completeAcquireActions) {
            this.completeAcquireActions = completeAcquireActions;
            return this;
        }


        /** 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN */
		[UnityEngine.SerializeField]
        public string challengePeriodEventId;

        /**
         * 達成報酬の受け取り可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId 達成報酬の受け取り可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }


        /** このタスクに挑戦するために達成しておく必要のあるタスクの名前 */
		[UnityEngine.SerializeField]
        public string premiseMissionTaskName;

        /**
         * このタスクに挑戦するために達成しておく必要のあるタスクの名前を設定
         *
         * @param premiseMissionTaskName このタスクに挑戦するために達成しておく必要のあるタスクの名前
         * @return this
         */
        public UpdateMissionTaskModelMasterRequest WithPremiseMissionTaskName(string premiseMissionTaskName) {
            this.premiseMissionTaskName = premiseMissionTaskName;
            return this;
        }


    	[Preserve]
        public static UpdateMissionTaskModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateMissionTaskModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                missionGroupName = data.Keys.Contains("missionGroupName") && data["missionGroupName"] != null ? data["missionGroupName"].ToString(): null,
                missionTaskName = data.Keys.Contains("missionTaskName") && data["missionTaskName"] != null ? data["missionTaskName"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                counterName = data.Keys.Contains("counterName") && data["counterName"] != null ? data["counterName"].ToString(): null,
                targetValue = data.Keys.Contains("targetValue") && data["targetValue"] != null ? (long?)long.Parse(data["targetValue"].ToString()) : null,
                completeAcquireActions = data.Keys.Contains("completeAcquireActions") && data["completeAcquireActions"] != null ? data["completeAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
                challengePeriodEventId = data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString(): null,
                premiseMissionTaskName = data.Keys.Contains("premiseMissionTaskName") && data["premiseMissionTaskName"] != null ? data["premiseMissionTaskName"].ToString(): null,
            };
        }

	}
}