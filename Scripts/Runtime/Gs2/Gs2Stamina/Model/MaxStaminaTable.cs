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

namespace Gs2.Gs2Stamina.Model
{
	[Preserve]
	public class MaxStaminaTable
	{

        /** スタミナの最大値テーブルマスター */
        public string maxStaminaTableId { set; get; }

        /**
         * スタミナの最大値テーブルマスターを設定
         *
         * @param maxStaminaTableId スタミナの最大値テーブルマスター
         * @return this
         */
        public MaxStaminaTable WithMaxStaminaTableId(string maxStaminaTableId) {
            this.maxStaminaTableId = maxStaminaTableId;
            return this;
        }

        /** 最大スタミナ値テーブル名 */
        public string name { set; get; }

        /**
         * 最大スタミナ値テーブル名を設定
         *
         * @param name 最大スタミナ値テーブル名
         * @return this
         */
        public MaxStaminaTable WithName(string name) {
            this.name = name;
            return this;
        }

        /** 最大スタミナ値テーブルのメタデータ */
        public string metadata { set; get; }

        /**
         * 最大スタミナ値テーブルのメタデータを設定
         *
         * @param metadata 最大スタミナ値テーブルのメタデータ
         * @return this
         */
        public MaxStaminaTable WithMetadata(string metadata) {
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
        public MaxStaminaTable WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }

        /** ランク毎のスタミナの最大値テーブル */
        public List<int?> values { set; get; }

        /**
         * ランク毎のスタミナの最大値テーブルを設定
         *
         * @param values ランク毎のスタミナの最大値テーブル
         * @return this
         */
        public MaxStaminaTable WithValues(List<int?> values) {
            this.values = values;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.maxStaminaTableId != null)
            {
                writer.WritePropertyName("maxStaminaTableId");
                writer.Write(this.maxStaminaTableId);
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
            if(this.experienceModelId != null)
            {
                writer.WritePropertyName("experienceModelId");
                writer.Write(this.experienceModelId);
            }
            if(this.values != null)
            {
                writer.WritePropertyName("values");
                writer.WriteArrayStart();
                foreach(var item in this.values)
                {
                    writer.Write(item.Value);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static MaxStaminaTable FromDict(JsonData data)
        {
            return new MaxStaminaTable()
                .WithMaxStaminaTableId(data.Keys.Contains("maxStaminaTableId") && data["maxStaminaTableId"] != null ? data["maxStaminaTableId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithExperienceModelId(data.Keys.Contains("experienceModelId") && data["experienceModelId"] != null ? data["experienceModelId"].ToString() : null)
                .WithValues(data.Keys.Contains("values") && data["values"] != null ? data["values"].Cast<JsonData>().Select(value =>
                    {
                        return (int?)int.Parse(value.ToString());
                    }
                ).ToList() : null);
        }
	}
}