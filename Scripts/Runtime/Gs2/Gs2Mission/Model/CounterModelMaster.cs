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
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Model
{
	[Preserve]
	public class CounterModelMaster
	{

        /** カウンターの種類マスター */
        public string counterId { set; get; }

        /**
         * カウンターの種類マスターを設定
         *
         * @param counterId カウンターの種類マスター
         * @return this
         */
        public CounterModelMaster WithCounterId(string counterId) {
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
        public CounterModelMaster WithName(string name) {
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
        public CounterModelMaster WithMetadata(string metadata) {
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
        public CounterModelMaster WithDescription(string description) {
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
        public CounterModelMaster WithScopes(List<CounterScopeModel> scopes) {
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
        public CounterModelMaster WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public CounterModelMaster WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public CounterModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
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
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
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
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static CounterModelMaster FromDict(JsonData data)
        {
            return new CounterModelMaster()
                .WithCounterId(data.Keys.Contains("counterId") && data["counterId"] != null ? data["counterId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithScopes(data.Keys.Contains("scopes") && data["scopes"] != null ? data["scopes"].Cast<JsonData>().Select(value =>
                    {
                        return CounterScopeModel.FromDict(value);
                    }
                ).ToList() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}