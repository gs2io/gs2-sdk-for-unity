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
using Gs2.Gs2Experience.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Experience.Request
{
	[Preserve]
	public class CreateExperienceModelMasterRequest : Gs2Request<CreateExperienceModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateExperienceModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public CreateExperienceModelMasterRequest WithName(string name) {
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
        public CreateExperienceModelMasterRequest WithDescription(string description) {
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
        public CreateExperienceModelMasterRequest WithMetadata(string metadata) {
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
        public CreateExperienceModelMasterRequest WithDefaultExperience(long? defaultExperience) {
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
        public CreateExperienceModelMasterRequest WithDefaultRankCap(long? defaultRankCap) {
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
        public CreateExperienceModelMasterRequest WithMaxRankCap(long? maxRankCap) {
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
        public CreateExperienceModelMasterRequest WithRankThresholdId(string rankThresholdId) {
            this.rankThresholdId = rankThresholdId;
            return this;
        }


    	[Preserve]
        public static CreateExperienceModelMasterRequest FromDict(JsonData data)
        {
            return new CreateExperienceModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                defaultExperience = data.Keys.Contains("defaultExperience") && data["defaultExperience"] != null ? (long?)long.Parse(data["defaultExperience"].ToString()) : null,
                defaultRankCap = data.Keys.Contains("defaultRankCap") && data["defaultRankCap"] != null ? (long?)long.Parse(data["defaultRankCap"].ToString()) : null,
                maxRankCap = data.Keys.Contains("maxRankCap") && data["maxRankCap"] != null ? (long?)long.Parse(data["maxRankCap"].ToString()) : null,
                rankThresholdId = data.Keys.Contains("rankThresholdId") && data["rankThresholdId"] != null ? data["rankThresholdId"].ToString(): null,
            };
        }

	}
}