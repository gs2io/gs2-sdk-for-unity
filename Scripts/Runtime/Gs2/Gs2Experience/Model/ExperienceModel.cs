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
	public class ExperienceModel
	{

        /** 経験値の種類マスター */
        public string experienceModelId { set; get; }

        /**
         * 経験値の種類マスターを設定
         *
         * @param experienceModelId 経験値の種類マスター
         * @return this
         */
        public ExperienceModel WithExperienceModelId(string experienceModelId) {
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
        public ExperienceModel WithName(string name) {
            this.name = name;
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
        public ExperienceModel WithMetadata(string metadata) {
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
        public ExperienceModel WithDefaultExperience(long? defaultExperience) {
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
        public ExperienceModel WithDefaultRankCap(long? defaultRankCap) {
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
        public ExperienceModel WithMaxRankCap(long? maxRankCap) {
            this.maxRankCap = maxRankCap;
            return this;
        }

        /** ランクアップ閾値 */
        public Threshold rankThreshold { set; get; }

        /**
         * ランクアップ閾値を設定
         *
         * @param rankThreshold ランクアップ閾値
         * @return this
         */
        public ExperienceModel WithRankThreshold(Threshold rankThreshold) {
            this.rankThreshold = rankThreshold;
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
            if(this.rankThreshold != null)
            {
                writer.WritePropertyName("rankThreshold");
                this.rankThreshold.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static ExperienceModel FromDict(JsonData data)
        {
            return new ExperienceModel()
                .WithExperienceModelId(data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDefaultExperience(data.Keys.Contains("defaultExperience") && data["defaultExperience"] != null ? (long?)long.Parse(data["defaultExperience"].ToString()) : null)
                .WithDefaultRankCap(data.Keys.Contains("defaultRankCap") && data["defaultRankCap"] != null ? (long?)long.Parse(data["defaultRankCap"].ToString()) : null)
                .WithMaxRankCap(data.Keys.Contains("maxRankCap") && data["maxRankCap"] != null ? (long?)long.Parse(data["maxRankCap"].ToString()) : null)
                .WithRankThreshold(data.Keys.Contains("rankThreshold") && data["rankThreshold"] != null ? Threshold.FromDict(data["rankThreshold"]) : null);
        }
	}
}