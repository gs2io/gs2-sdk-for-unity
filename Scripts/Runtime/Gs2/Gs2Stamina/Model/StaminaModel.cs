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
	public class StaminaModel
	{

        /** スタミナモデルマスター */
        public string staminaModelId { set; get; }

        /**
         * スタミナモデルマスターを設定
         *
         * @param staminaModelId スタミナモデルマスター
         * @return this
         */
        public StaminaModel WithStaminaModelId(string staminaModelId) {
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
        public StaminaModel WithName(string name) {
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
        public StaminaModel WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public StaminaModel WithRecoverIntervalMinutes(int? recoverIntervalMinutes) {
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
        public StaminaModel WithRecoverValue(int? recoverValue) {
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
        public StaminaModel WithInitialCapacity(int? initialCapacity) {
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
        public StaminaModel WithIsOverflow(bool? isOverflow) {
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
        public StaminaModel WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
            return this;
        }

        /** GS2-Experience と連携する際に使用するスタミナ最大値テーブル */
        public MaxStaminaTable maxStaminaTable { set; get; }

        /**
         * GS2-Experience と連携する際に使用するスタミナ最大値テーブルを設定
         *
         * @param maxStaminaTable GS2-Experience と連携する際に使用するスタミナ最大値テーブル
         * @return this
         */
        public StaminaModel WithMaxStaminaTable(MaxStaminaTable maxStaminaTable) {
            this.maxStaminaTable = maxStaminaTable;
            return this;
        }

        /** GS2-Experience と連携する際に使用する回復間隔テーブル */
        public RecoverIntervalTable recoverIntervalTable { set; get; }

        /**
         * GS2-Experience と連携する際に使用する回復間隔テーブルを設定
         *
         * @param recoverIntervalTable GS2-Experience と連携する際に使用する回復間隔テーブル
         * @return this
         */
        public StaminaModel WithRecoverIntervalTable(RecoverIntervalTable recoverIntervalTable) {
            this.recoverIntervalTable = recoverIntervalTable;
            return this;
        }

        /** GS2-Experience と連携する際に使用する回復量テーブル */
        public RecoverValueTable recoverValueTable { set; get; }

        /**
         * GS2-Experience と連携する際に使用する回復量テーブルを設定
         *
         * @param recoverValueTable GS2-Experience と連携する際に使用する回復量テーブル
         * @return this
         */
        public StaminaModel WithRecoverValueTable(RecoverValueTable recoverValueTable) {
            this.recoverValueTable = recoverValueTable;
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
            if(this.maxStaminaTable != null)
            {
                writer.WritePropertyName("maxStaminaTable");
                this.maxStaminaTable.WriteJson(writer);
            }
            if(this.recoverIntervalTable != null)
            {
                writer.WritePropertyName("recoverIntervalTable");
                this.recoverIntervalTable.WriteJson(writer);
            }
            if(this.recoverValueTable != null)
            {
                writer.WritePropertyName("recoverValueTable");
                this.recoverValueTable.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static StaminaModel FromDict(JsonData data)
        {
            return new StaminaModel()
                .WithStaminaModelId(data.Keys.Contains("staminaModelId") && data["staminaModelId"] != null ? data["staminaModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithRecoverIntervalMinutes(data.Keys.Contains("recoverIntervalMinutes") && data["recoverIntervalMinutes"] != null ? (int?)int.Parse(data["recoverIntervalMinutes"].ToString()) : null)
                .WithRecoverValue(data.Keys.Contains("recoverValue") && data["recoverValue"] != null ? (int?)int.Parse(data["recoverValue"].ToString()) : null)
                .WithInitialCapacity(data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null)
                .WithIsOverflow(data.Keys.Contains("isOverflow") && data["isOverflow"] != null ? (bool?)bool.Parse(data["isOverflow"].ToString()) : null)
                .WithMaxCapacity(data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null)
                .WithMaxStaminaTable(data.Keys.Contains("maxStaminaTable") && data["maxStaminaTable"] != null ? MaxStaminaTable.FromDict(data["maxStaminaTable"]) : null)
                .WithRecoverIntervalTable(data.Keys.Contains("recoverIntervalTable") && data["recoverIntervalTable"] != null ? RecoverIntervalTable.FromDict(data["recoverIntervalTable"]) : null)
                .WithRecoverValueTable(data.Keys.Contains("recoverValueTable") && data["recoverValueTable"] != null ? RecoverValueTable.FromDict(data["recoverValueTable"]) : null);
        }
	}
}