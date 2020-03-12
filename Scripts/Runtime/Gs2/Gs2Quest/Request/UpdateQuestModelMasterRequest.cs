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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateQuestModelMasterRequest : Gs2Request<UpdateQuestModelMasterRequest>
	{

        /** カテゴリ名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public UpdateQuestModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** クエストグループモデル名 */
		[UnityEngine.SerializeField]
        public string questGroupName;

        /**
         * クエストグループモデル名を設定
         *
         * @param questGroupName クエストグループモデル名
         * @return this
         */
        public UpdateQuestModelMasterRequest WithQuestGroupName(string questGroupName) {
            this.questGroupName = questGroupName;
            return this;
        }


        /** クエスト名 */
		[UnityEngine.SerializeField]
        public string questName;

        /**
         * クエスト名を設定
         *
         * @param questName クエスト名
         * @return this
         */
        public UpdateQuestModelMasterRequest WithQuestName(string questName) {
            this.questName = questName;
            return this;
        }


        /** クエストモデルの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * クエストモデルの説明を設定
         *
         * @param description クエストモデルの説明
         * @return this
         */
        public UpdateQuestModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** クエストのメタデータ */
		[UnityEngine.SerializeField]
        public string metadata;

        /**
         * クエストのメタデータを設定
         *
         * @param metadata クエストのメタデータ
         * @return this
         */
        public UpdateQuestModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** クエストの内容 */
		[UnityEngine.SerializeField]
        public List<Contents> contents;

        /**
         * クエストの内容を設定
         *
         * @param contents クエストの内容
         * @return this
         */
        public UpdateQuestModelMasterRequest WithContents(List<Contents> contents) {
            this.contents = contents;
            return this;
        }


        /** 挑戦可能な期間を指定するイベントマスター のGRN */
		[UnityEngine.SerializeField]
        public string challengePeriodEventId;

        /**
         * 挑戦可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId 挑戦可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public UpdateQuestModelMasterRequest WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }


        /** クエストの参加料 */
		[UnityEngine.SerializeField]
        public List<ConsumeAction> consumeActions;

        /**
         * クエストの参加料を設定
         *
         * @param consumeActions クエストの参加料
         * @return this
         */
        public UpdateQuestModelMasterRequest WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }


        /** クエスト失敗時の報酬 */
		[UnityEngine.SerializeField]
        public List<AcquireAction> failedAcquireActions;

        /**
         * クエスト失敗時の報酬を設定
         *
         * @param failedAcquireActions クエスト失敗時の報酬
         * @return this
         */
        public UpdateQuestModelMasterRequest WithFailedAcquireActions(List<AcquireAction> failedAcquireActions) {
            this.failedAcquireActions = failedAcquireActions;
            return this;
        }


        /** クエストに挑戦するためにクリアしておく必要のあるクエスト名 */
		[UnityEngine.SerializeField]
        public List<string> premiseQuestNames;

        /**
         * クエストに挑戦するためにクリアしておく必要のあるクエスト名を設定
         *
         * @param premiseQuestNames クエストに挑戦するためにクリアしておく必要のあるクエスト名
         * @return this
         */
        public UpdateQuestModelMasterRequest WithPremiseQuestNames(List<string> premiseQuestNames) {
            this.premiseQuestNames = premiseQuestNames;
            return this;
        }


    	[Preserve]
        public static UpdateQuestModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateQuestModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                questGroupName = data.Keys.Contains("questGroupName") && data["questGroupName"] != null ? data["questGroupName"].ToString(): null,
                questName = data.Keys.Contains("questName") && data["questName"] != null ? data["questName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                contents = data.Keys.Contains("contents") && data["contents"] != null ? data["contents"].Cast<JsonData>().Select(value =>
                    {
                        return Contents.FromDict(value);
                    }
                ).ToList() : null,
                challengePeriodEventId = data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString(): null,
                consumeActions = data.Keys.Contains("consumeActions") && data["consumeActions"] != null ? data["consumeActions"].Cast<JsonData>().Select(value =>
                    {
                        return ConsumeAction.FromDict(value);
                    }
                ).ToList() : null,
                failedAcquireActions = data.Keys.Contains("failedAcquireActions") && data["failedAcquireActions"] != null ? data["failedAcquireActions"].Cast<JsonData>().Select(value =>
                    {
                        return AcquireAction.FromDict(value);
                    }
                ).ToList() : null,
                premiseQuestNames = data.Keys.Contains("premiseQuestNames") && data["premiseQuestNames"] != null ? data["premiseQuestNames"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null,
            };
        }

	}
}