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

namespace Gs2.Gs2Stamina.Model
{
	public class RecoverValueTableMaster
	{

        /** スタミナ回復量テーブルマスター */
        public string recoverValueTableId { set; get; }

        /**
         * スタミナ回復量テーブルマスターを設定
         *
         * @param recoverValueTableId スタミナ回復量テーブルマスター
         * @return this
         */
        public RecoverValueTableMaster WithRecoverValueTableId(string recoverValueTableId) {
            this.recoverValueTableId = recoverValueTableId;
            return this;
        }

        /** スタミナ回復量テーブル名 */
        public string name { set; get; }

        /**
         * スタミナ回復量テーブル名を設定
         *
         * @param name スタミナ回復量テーブル名
         * @return this
         */
        public RecoverValueTableMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** スタミナ回復量テーブルのメタデータ */
        public string metadata { set; get; }

        /**
         * スタミナ回復量テーブルのメタデータを設定
         *
         * @param metadata スタミナ回復量テーブルのメタデータ
         * @return this
         */
        public RecoverValueTableMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** スタミナ回復量テーブルマスターの説明 */
        public string description { set; get; }

        /**
         * スタミナ回復量テーブルマスターの説明を設定
         *
         * @param description スタミナ回復量テーブルマスターの説明
         * @return this
         */
        public RecoverValueTableMaster WithDescription(string description) {
            this.description = description;
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
        public RecoverValueTableMaster WithExperienceModelId(string experienceModelId) {
            this.experienceModelId = experienceModelId;
            return this;
        }

        /** ランク毎のスタミナ回復量テーブル */
        public List<int?> values { set; get; }

        /**
         * ランク毎のスタミナ回復量テーブルを設定
         *
         * @param values ランク毎のスタミナ回復量テーブル
         * @return this
         */
        public RecoverValueTableMaster WithValues(List<int?> values) {
            this.values = values;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.recoverValueTableId != null)
            {
                writer.WritePropertyName("recoverValueTableId");
                writer.Write(this.recoverValueTableId);
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
	}
}