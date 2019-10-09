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
using Gs2.Gs2Stamina.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Stamina.Request
{
	[Preserve]
	public class CreateRecoverIntervalTableMasterRequest : Gs2Request<CreateRecoverIntervalTableMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナ回復間隔テーブル名 */
        public string name { set; get; }

        /**
         * スタミナ回復間隔テーブル名を設定
         *
         * @param name スタミナ回復間隔テーブル名
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** スタミナ回復間隔テーブルマスターの説明 */
        public string description { set; get; }

        /**
         * スタミナ回復間隔テーブルマスターの説明を設定
         *
         * @param description スタミナ回復間隔テーブルマスターの説明
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** スタミナ回復間隔テーブルのメタデータ */
        public string metadata { set; get; }

        /**
         * スタミナ回復間隔テーブルのメタデータを設定
         *
         * @param metadata スタミナ回復間隔テーブルのメタデータ
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** 経験値の種類マスター のGRN */
        public string experienceModelId { set; get; }

        /**
         * 経験値の種類マスター のGRNを設定
         *
         * @param experienceModelId 経験値の種類マスター のGRN
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }


        /** ランク毎のスタミナ回復間隔テーブル */
        public List<int?> values { set; get; }

        /**
         * ランク毎のスタミナ回復間隔テーブルを設定
         *
         * @param values ランク毎のスタミナ回復間隔テーブル
         * @return this
         */
        public CreateRecoverIntervalTableMasterRequest WithValues(List<int?> values) {
            this.values = values;
            return this;
        }


    	[Preserve]
        public static CreateRecoverIntervalTableMasterRequest FromDict(JsonData data)
        {
            return new CreateRecoverIntervalTableMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                name = data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                experienceModelId = data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString(): null,
                values = data.Keys.Contains("values") && data["values"] != null ? data["values"].Cast<JsonData>().Select(value =>
                    {
                        return (int?)int.Parse(value.ToString());
                    }
                ).ToList() : null,
            };
        }

	}
}