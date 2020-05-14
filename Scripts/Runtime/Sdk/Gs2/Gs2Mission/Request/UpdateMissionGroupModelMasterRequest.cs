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
	public class UpdateMissionGroupModelMasterRequest : Gs2Request<UpdateMissionGroupModelMasterRequest>
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
        public UpdateMissionGroupModelMasterRequest WithNamespaceName(string namespaceName) {
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
        public UpdateMissionGroupModelMasterRequest WithMissionGroupName(string missionGroupName) {
            this.missionGroupName = missionGroupName;
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
        public UpdateMissionGroupModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** ミッショングループの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * ミッショングループの説明を設定
         *
         * @param description ミッショングループの説明
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** リセットタイミング */
		[UnityEngine.SerializeField]
        public string resetType;

        /**
         * リセットタイミングを設定
         *
         * @param resetType リセットタイミング
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithResetType(string resetType) {
            this.resetType = resetType;
            return this;
        }


        /** リセットをする日にち */
		[UnityEngine.SerializeField]
        public int? resetDayOfMonth;

        /**
         * リセットをする日にちを設定
         *
         * @param resetDayOfMonth リセットをする日にち
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithResetDayOfMonth(int? resetDayOfMonth) {
            this.resetDayOfMonth = resetDayOfMonth;
            return this;
        }


        /** リセットする曜日 */
		[UnityEngine.SerializeField]
        public string resetDayOfWeek;

        /**
         * リセットする曜日を設定
         *
         * @param resetDayOfWeek リセットする曜日
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithResetDayOfWeek(string resetDayOfWeek) {
            this.resetDayOfWeek = resetDayOfWeek;
            return this;
        }


        /** リセット時刻 */
		[UnityEngine.SerializeField]
        public int? resetHour;

        /**
         * リセット時刻を設定
         *
         * @param resetHour リセット時刻
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithResetHour(int? resetHour) {
            this.resetHour = resetHour;
            return this;
        }


        /** ミッションを達成したときの通知先ネームスペース のGRN */
		[UnityEngine.SerializeField]
        public string completeNotificationNamespaceId;

        /**
         * ミッションを達成したときの通知先ネームスペース のGRNを設定
         *
         * @param completeNotificationNamespaceId ミッションを達成したときの通知先ネームスペース のGRN
         * @return this
         */
        public UpdateMissionGroupModelMasterRequest WithCompleteNotificationNamespaceId(string completeNotificationNamespaceId) {
            this.completeNotificationNamespaceId = completeNotificationNamespaceId;
            return this;
        }


    	[Preserve]
        public static UpdateMissionGroupModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateMissionGroupModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                missionGroupName = data.Keys.Contains("missionGroupName") && data["missionGroupName"] != null ? data["missionGroupName"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                resetType = data.Keys.Contains("resetType") && data["resetType"] != null ? data["resetType"].ToString(): null,
                resetDayOfMonth = data.Keys.Contains("resetDayOfMonth") && data["resetDayOfMonth"] != null ? (int?)int.Parse(data["resetDayOfMonth"].ToString()) : null,
                resetDayOfWeek = data.Keys.Contains("resetDayOfWeek") && data["resetDayOfWeek"] != null ? data["resetDayOfWeek"].ToString(): null,
                resetHour = data.Keys.Contains("resetHour") && data["resetHour"] != null ? (int?)int.Parse(data["resetHour"].ToString()) : null,
                completeNotificationNamespaceId = data.Keys.Contains("completeNotificationNamespaceId") && data["completeNotificationNamespaceId"] != null ? data["completeNotificationNamespaceId"].ToString(): null,
            };
        }

	}
}