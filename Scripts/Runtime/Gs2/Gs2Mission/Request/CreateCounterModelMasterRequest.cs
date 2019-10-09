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
	public class CreateCounterModelMasterRequest : Gs2Request<CreateCounterModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateCounterModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** カウンター名 */
        public string name { set; get; }

        /**
         * カウンター名を設定
         *
         * @param name カウンター名
         * @return this
         */
        public CreateCounterModelMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public CreateCounterModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** カウンターの種類マスターの説明 */
        public string description { set; get; }

        /**
         * カウンターの種類マスターの説明を設定
         *
         * @param description カウンターの種類マスターの説明
         * @return this
         */
        public CreateCounterModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** カウンターのリセットタイミング */
        public List<CounterScopeModel> scopes { set; get; }

        /**
         * カウンターのリセットタイミングを設定
         *
         * @param scopes カウンターのリセットタイミング
         * @return this
         */
        public CreateCounterModelMasterRequest WithScopes(List<CounterScopeModel> scopes) {
            this.scopes = scopes;
            return this;
        }


        /** カウントアップ可能な期間を指定するイベントマスター のGRN */
        public string challengePeriodEventId { set; get; }

        /**
         * カウントアップ可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId カウントアップ可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public CreateCounterModelMasterRequest WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }


    	[Preserve]
        public static CreateCounterModelMasterRequest FromDict(JsonData data)
        {
            return new CreateCounterModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                scopes = data.Keys.Contains("scopes") && data["scopes"] != null ? data["scopes"].Cast<JsonData>().Select(value =>
                    {
                        return CounterScopeModel.FromDict(value);
                    }
                ).ToList() : null,
                challengePeriodEventId = data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString(): null,
            };
        }

	}
}