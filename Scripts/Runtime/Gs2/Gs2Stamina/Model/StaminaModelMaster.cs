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
	public class StaminaModelMaster
	{

        /** スタミナモデルマスター */
        public string staminaModelId { set; get; }

        /**
         * スタミナモデルマスターを設定
         *
         * @param staminaModelId スタミナモデルマスター
         * @return this
         */
        public StaminaModelMaster WithStaminaModelId(string staminaModelId) {
            this.staminaModelId = staminaModelId;
            return this;
        }

        /** スタミナの種類名 */
        public string name { set; get; }

        /**
         * スタミナの種類名を設定
         *
         * @param name スタミナの種類名
         * @return this
         */
        public StaminaModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** スタミナの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * スタミナの種類のメタデータを設定
         *
         * @param metadata スタミナの種類のメタデータ
         * @return this
         */
        public StaminaModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** スタミナモデルマスターの説明 */
        public string description { set; get; }

        /**
         * スタミナモデルマスターの説明を設定
         *
         * @param description スタミナモデルマスターの説明
         * @return this
         */
        public StaminaModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** スタミナを回復する速度(分) */
        public int? recoverIntervalMinutes { set; get; }

        /**
         * スタミナを回復する速度(分)を設定
         *
         * @param recoverIntervalMinutes スタミナを回復する速度(分)
         * @return this
         */
        public StaminaModelMaster WithRecoverIntervalMinutes(int? recoverIntervalMinutes) {
            this.recoverIntervalMinutes = recoverIntervalMinutes;
            return this;
        }

        /** 時間経過後に回復する量 */
        public int? recoverValue { set; get; }

        /**
         * 時間経過後に回復する量を設定
         *
         * @param recoverValue 時間経過後に回復する量
         * @return this
         */
        public StaminaModelMaster WithRecoverValue(int? recoverValue) {
            this.recoverValue = recoverValue;
            return this;
        }

        /** スタミナの最大値の初期値 */
        public int? initialCapacity { set; get; }

        /**
         * スタミナの最大値の初期値を設定
         *
         * @param initialCapacity スタミナの最大値の初期値
         * @return this
         */
        public StaminaModelMaster WithInitialCapacity(int? initialCapacity) {
            this.initialCapacity = initialCapacity;
            return this;
        }

        /** 最大値を超えて回復するか */
        public bool? isOverflow { set; get; }

        /**
         * 最大値を超えて回復するかを設定
         *
         * @param isOverflow 最大値を超えて回復するか
         * @return this
         */
        public StaminaModelMaster WithIsOverflow(bool? isOverflow) {
            this.isOverflow = isOverflow;
            return this;
        }

        /** 溢れた状況での最大値 */
        public int? maxCapacity { set; get; }

        /**
         * 溢れた状況での最大値を設定
         *
         * @param maxCapacity 溢れた状況での最大値
         * @return this
         */
        public StaminaModelMaster WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }

        /** GS2-Experience のランクによって最大スタミナ値を決定する */
        public string maxStaminaTableId { set; get; }

        /**
         * GS2-Experience のランクによって最大スタミナ値を決定するを設定
         *
         * @param maxStaminaTableId GS2-Experience のランクによって最大スタミナ値を決定する
         * @return this
         */
        public StaminaModelMaster WithMaxStaminaTableId(string maxStaminaTableId) {
            this.maxStaminaTableId = maxStaminaTableId;
            return this;
        }

        /** GS2-Experience のランクによってスタミナの回復間隔を決定する */
        public string recoverIntervalTableId { set; get; }

        /**
         * GS2-Experience のランクによってスタミナの回復間隔を決定するを設定
         *
         * @param recoverIntervalTableId GS2-Experience のランクによってスタミナの回復間隔を決定する
         * @return this
         */
        public StaminaModelMaster WithRecoverIntervalTableId(string recoverIntervalTableId) {
            this.recoverIntervalTableId = recoverIntervalTableId;
            return this;
        }

        /** GS2-Experience のランクによってスタミナの回復量を決定する */
        public string recoverValueTableId { set; get; }

        /**
         * GS2-Experience のランクによってスタミナの回復量を決定するを設定
         *
         * @param recoverValueTableId GS2-Experience のランクによってスタミナの回復量を決定する
         * @return this
         */
        public StaminaModelMaster WithRecoverValueTableId(string recoverValueTableId) {
            this.recoverValueTableId = recoverValueTableId;
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
        public StaminaModelMaster WithCreatedAt(long? createdAt) {
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
        public StaminaModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.staminaModelId != null)
            {
                writer.WritePropertyName("staminaModelId");
                writer.Write(this.staminaModelId);
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
            if(this.recoverIntervalMinutes.HasValue)
            {
                writer.WritePropertyName("recoverIntervalMinutes");
                writer.Write(this.recoverIntervalMinutes.Value);
            }
            if(this.recoverValue.HasValue)
            {
                writer.WritePropertyName("recoverValue");
                writer.Write(this.recoverValue.Value);
            }
            if(this.initialCapacity.HasValue)
            {
                writer.WritePropertyName("initialCapacity");
                writer.Write(this.initialCapacity.Value);
            }
            if(this.isOverflow.HasValue)
            {
                writer.WritePropertyName("isOverflow");
                writer.Write(this.isOverflow.Value);
            }
            if(this.maxCapacity.HasValue)
            {
                writer.WritePropertyName("maxCapacity");
                writer.Write(this.maxCapacity.Value);
            }
            if(this.maxStaminaTableId != null)
            {
                writer.WritePropertyName("maxStaminaTableId");
                writer.Write(this.maxStaminaTableId);
            }
            if(this.recoverIntervalTableId != null)
            {
                writer.WritePropertyName("recoverIntervalTableId");
                writer.Write(this.recoverIntervalTableId);
            }
            if(this.recoverValueTableId != null)
            {
                writer.WritePropertyName("recoverValueTableId");
                writer.Write(this.recoverValueTableId);
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
        public static StaminaModelMaster FromDict(JsonData data)
        {
            return new StaminaModelMaster()
                .WithStaminaModelId(data.Keys.Contains("staminaModelId") && data["staminaModelId"] != null ? data["staminaModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithRecoverIntervalMinutes(data.Keys.Contains("recoverIntervalMinutes") && data["recoverIntervalMinutes"] != null ? (int?)int.Parse(data["recoverIntervalMinutes"].ToString()) : null)
                .WithRecoverValue(data.Keys.Contains("recoverValue") && data["recoverValue"] != null ? (int?)int.Parse(data["recoverValue"].ToString()) : null)
                .WithInitialCapacity(data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null)
                .WithIsOverflow(data.Keys.Contains("isOverflow") && data["isOverflow"] != null ? (bool?)bool.Parse(data["isOverflow"].ToString()) : null)
                .WithMaxCapacity(data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null)
                .WithMaxStaminaTableId(data.Keys.Contains("maxStaminaTableId") && data["maxStaminaTableId"] != null ? data["maxStaminaTableId"].ToString() : null)
                .WithRecoverIntervalTableId(data.Keys.Contains("recoverIntervalTableId") && data["recoverIntervalTableId"] != null ? data["recoverIntervalTableId"].ToString() : null)
                .WithRecoverValueTableId(data.Keys.Contains("recoverValueTableId") && data["recoverValueTableId"] != null ? data["recoverValueTableId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}