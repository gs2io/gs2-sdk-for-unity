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
	public class RecoverIntervalTableMaster
	{

        /** スタミナ回復間隔テーブルマスター */
        public string recoverIntervalTableId { set; get; }

        /**
         * スタミナ回復間隔テーブルマスターを設定
         *
         * @param recoverIntervalTableId スタミナ回復間隔テーブルマスター
         * @return this
         */
        public RecoverIntervalTableMaster WithRecoverIntervalTableId(string recoverIntervalTableId) {
            this.recoverIntervalTableId = recoverIntervalTableId;
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
        public RecoverIntervalTableMaster WithName(string name) {
            this.name = name;
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
        public RecoverIntervalTableMaster WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public RecoverIntervalTableMaster WithDescription(string description) {
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
        public RecoverIntervalTableMaster WithExperienceModelId(string experienceModelId) {
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
        public RecoverIntervalTableMaster WithValues(List<int?> values) {
            this.values = values;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.recoverIntervalTableId != null)
            {
                writer.WritePropertyName("recoverIntervalTableId");
                writer.Write(this.recoverIntervalTableId);
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