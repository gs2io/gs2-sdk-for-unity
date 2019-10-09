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
	public class CreateQuestModelMasterRequest : Gs2Request<CreateQuestModelMasterRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public CreateQuestModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** クエストグループモデル名 */
        public string questGroupName { set; get; }

        /**
         * クエストグループモデル名を設定
         *
         * @param questGroupName クエストグループモデル名
         * @return this
         */
        public CreateQuestModelMasterRequest WithQuestGroupName(string questGroupName) {
            this.questGroupName = questGroupName;
            return this;
        }


        /** クエスト名 */
        public string name { set; get; }

        /**
         * クエスト名を設定
         *
         * @param name クエスト名
         * @return this
         */
        public CreateQuestModelMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** クエストモデルの説明 */
        public string description { set; get; }

        /**
         * クエストモデルの説明を設定
         *
         * @param description クエストモデルの説明
         * @return this
         */
        public CreateQuestModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** クエストのメタデータ */
        public string metadata { set; get; }

        /**
         * クエストのメタデータを設定
         *
         * @param metadata クエストのメタデータ
         * @return this
         */
        public CreateQuestModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** クエストの内容 */
        public List<Contents> contents { set; get; }

        /**
         * クエストの内容を設定
         *
         * @param contents クエストの内容
         * @return this
         */
        public CreateQuestModelMasterRequest WithContents(List<Contents> contents) {
            this.contents = contents;
            return this;
        }


        /** 挑戦可能な期間を指定するイベントマスター のGRN */
        public string challengePeriodEventId { set; get; }

        /**
         * 挑戦可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId 挑戦可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public CreateQuestModelMasterRequest WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }


        /** クエストの参加料 */
        public List<ConsumeAction> consumeActions { set; get; }

        /**
         * クエストの参加料を設定
         *
         * @param consumeActions クエストの参加料
         * @return this
         */
        public CreateQuestModelMasterRequest WithConsumeActions(List<ConsumeAction> consumeActions) {
            this.consumeActions = consumeActions;
            return this;
        }


        /** クエスト失敗時の報酬 */
        public List<AcquireAction> failedAcquireActions { set; get; }

        /**
         * クエスト失敗時の報酬を設定
         *
         * @param failedAcquireActions クエスト失敗時の報酬
         * @return this
         */
        public CreateQuestModelMasterRequest WithFailedAcquireActions(List<AcquireAction> failedAcquireActions) {
            this.failedAcquireActions = failedAcquireActions;
            return this;
        }


        /** クエストに挑戦するためにクリアしておく必要のあるクエスト名 */
        public List<string> premiseQuestNames { set; get; }

        /**
         * クエストに挑戦するためにクリアしておく必要のあるクエスト名を設定
         *
         * @param premiseQuestNames クエストに挑戦するためにクリアしておく必要のあるクエスト名
         * @return this
         */
        public CreateQuestModelMasterRequest WithPremiseQuestNames(List<string> premiseQuestNames) {
            this.premiseQuestNames = premiseQuestNames;
            return this;
        }


    	[Preserve]
        public static CreateQuestModelMasterRequest FromDict(JsonData data)
        {
            return new CreateQuestModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                questGroupName = data.Keys.Contains("questGroupName") && data["questGroupName"] != null ? data["questGroupName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
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