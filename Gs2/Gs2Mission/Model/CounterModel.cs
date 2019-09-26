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
using Gs2.Core.Model;
using LitJson;

namespace Gs2.Gs2Mission.Model
{
	public class CounterModel
	{

        /** カウンターの種類 */
        public string counterId { set; get; }

        /**
         * カウンターの種類を設定
         *
         * @param counterId カウンターの種類
         * @return this
         */
        public CounterModel WithCounterId(string counterId) {
            this.counterId = counterId;
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
        public CounterModel WithName(string name) {
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
        public CounterModel WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public CounterModel WithScopes(List<CounterScopeModel> scopes) {
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
        public CounterModel WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.counterId != null)
            {
                writer.WritePropertyName("counterId");
                writer.Write(this.counterId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.scopes != null)
            {
                writer.WritePropertyName("scopes");
                writer.WriteArrayStart();
                foreach(var item in this.scopes)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.challengePeriodEventId != null)
            {
                writer.WritePropertyName("challengePeriodEventId");
                writer.Write(this.challengePeriodEventId);
            }
            writer.WriteObjectEnd();
        }
	}
}