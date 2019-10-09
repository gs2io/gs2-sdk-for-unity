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

namespace Gs2.Gs2Experience.Model
{
	[Preserve]
	public class ExperienceModelMaster
	{

        /** 経験値の種類マスター */
        public string experienceModelId { set; get; }

        /**
         * 経験値の種類マスターを設定
         *
         * @param experienceModelId 経験値の種類マスター
         * @return this
         */
        public ExperienceModelMaster WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }

        /** 経験値の種類名 */
        public string name { set; get; }

        /**
         * 経験値の種類名を設定
         *
         * @param name 経験値の種類名
         * @return this
         */
        public ExperienceModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 経験値の種類マスターの説明 */
        public string description { set; get; }

        /**
         * 経験値の種類マスターの説明を設定
         *
         * @param description 経験値の種類マスターの説明
         * @return this
         */
        public ExperienceModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 経験値の種類のメタデータ */
        public string metadata { set; get; }

        /**
         * 経験値の種類のメタデータを設定
         *
         * @param metadata 経験値の種類のメタデータ
         * @return this
         */
        public ExperienceModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 経験値の初期値 */
        public long? defaultExperience { set; get; }

        /**
         * 経験値の初期値を設定
         *
         * @param defaultExperience 経験値の初期値
         * @return this
         */
        public ExperienceModelMaster WithDefaultExperience(long? defaultExperience) {
            this.defaultExperience = defaultExperience;
            return this;
        }

        /** ランクキャップの初期値 */
        public long? defaultRankCap { set; get; }

        /**
         * ランクキャップの初期値を設定
         *
         * @param defaultRankCap ランクキャップの初期値
         * @return this
         */
        public ExperienceModelMaster WithDefaultRankCap(long? defaultRankCap) {
            this.defaultRankCap = defaultRankCap;
            return this;
        }

        /** ランクキャップの最大値 */
        public long? maxRankCap { set; get; }

        /**
         * ランクキャップの最大値を設定
         *
         * @param maxRankCap ランクキャップの最大値
         * @return this
         */
        public ExperienceModelMaster WithMaxRankCap(long? maxRankCap) {
            this.maxRankCap = maxRankCap;
            return this;
        }

        /** ランク計算に用いる */
        public string rankThresholdId { set; get; }

        /**
         * ランク計算に用いるを設定
         *
         * @param rankThresholdId ランク計算に用いる
         * @return this
         */
        public ExperienceModelMaster WithRankThresholdId(string rankThresholdId) {
            this.rankThresholdId = rankThresholdId;
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
        public ExperienceModelMaster WithCreatedAt(long? createdAt) {
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
        public ExperienceModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.experienceModelId != null)
            {
                writer.WritePropertyName("experienceModelId");
                writer.Write(this.experienceModelId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.defaultExperience.HasValue)
            {
                writer.WritePropertyName("defaultExperience");
                writer.Write(this.defaultExperience.Value);
            }
            if(this.defaultRankCap.HasValue)
            {
                writer.WritePropertyName("defaultRankCap");
                writer.Write(this.defaultRankCap.Value);
            }
            if(this.maxRankCap.HasValue)
            {
                writer.WritePropertyName("maxRankCap");
                writer.Write(this.maxRankCap.Value);
            }
            if(this.rankThresholdId != null)
            {
                writer.WritePropertyName("rankThresholdId");
                writer.Write(this.rankThresholdId);
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
        public static ExperienceModelMaster FromDict(JsonData data)
        {
            return new ExperienceModelMaster()
                .WithExperienceModelId(data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDefaultExperience(data.Keys.Contains("defaultExperience") && data["defaultExperience"] != null ? (long?)long.Parse(data["defaultExperience"].ToString()) : null)
                .WithDefaultRankCap(data.Keys.Contains("defaultRankCap") && data["defaultRankCap"] != null ? (long?)long.Parse(data["defaultRankCap"].ToString()) : null)
                .WithMaxRankCap(data.Keys.Contains("maxRankCap") && data["maxRankCap"] != null ? (long?)long.Parse(data["maxRankCap"].ToString()) : null)
                .WithRankThresholdId(data.Keys.Contains("rankThresholdId") && data["rankThresholdId"] != null ? data["rankThresholdId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}