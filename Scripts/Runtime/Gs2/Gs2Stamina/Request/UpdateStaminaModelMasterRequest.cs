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
	public class UpdateStaminaModelMasterRequest : Gs2Request<UpdateStaminaModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateStaminaModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナの種類名 */
        public string staminaName { set; get; }

        /**
         * スタミナの種類名を設定
         *
         * @param staminaName スタミナの種類名
         * @return this
         */
        public UpdateStaminaModelMasterRequest WithStaminaName(string staminaName) {
            this.staminaName = staminaName;
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
        public UpdateStaminaModelMasterRequest WithDescription(string description) {
            this.description = description;
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
        public UpdateStaminaModelMasterRequest WithMetadata(string metadata) {
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
        public UpdateStaminaModelMasterRequest WithRecoverIntervalMinutes(int? recoverIntervalMinutes) {
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
        public UpdateStaminaModelMasterRequest WithRecoverValue(int? recoverValue) {
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
        public UpdateStaminaModelMasterRequest WithInitialCapacity(int? initialCapacity) {
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
        public UpdateStaminaModelMasterRequest WithIsOverflow(bool? isOverflow) {
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
        public UpdateStaminaModelMasterRequest WithMaxCapacity(int? maxCapacity) {
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
        public UpdateStaminaModelMasterRequest WithMaxStaminaTableId(string maxStaminaTableId) {
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
        public UpdateStaminaModelMasterRequest WithRecoverIntervalTableId(string recoverIntervalTableId) {
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
        public UpdateStaminaModelMasterRequest WithRecoverValueTableId(string recoverValueTableId) {
            this.recoverValueTableId = recoverValueTableId;
            return this;
        }


    	[Preserve]
        public static UpdateStaminaModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateStaminaModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                staminaName = data.Keys.Contains("staminaName") && data["staminaName"] != null ? data["staminaName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                recoverIntervalMinutes = data.Keys.Contains("recoverIntervalMinutes") && data["recoverIntervalMinutes"] != null ? (int?)int.Parse(data["recoverIntervalMinutes"].ToString()) : null,
                recoverValue = data.Keys.Contains("recoverValue") && data["recoverValue"] != null ? (int?)int.Parse(data["recoverValue"].ToString()) : null,
                initialCapacity = data.Keys.Contains("initialCapacity") && data["initialCapacity"] != null ? (int?)int.Parse(data["initialCapacity"].ToString()) : null,
                isOverflow = data.Keys.Contains("isOverflow") && data["isOverflow"] != null ? (bool?)bool.Parse(data["isOverflow"].ToString()) : null,
                maxCapacity = data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null,
                maxStaminaTableId = data.Keys.Contains("maxStaminaTableId") && data["maxStaminaTableId"] != null ? data["maxStaminaTableId"].ToString(): null,
                recoverIntervalTableId = data.Keys.Contains("recoverIntervalTableId") && data["recoverIntervalTableId"] != null ? data["recoverIntervalTableId"].ToString(): null,
                recoverValueTableId = data.Keys.Contains("recoverValueTableId") && data["recoverValueTableId"] != null ? data["recoverValueTableId"].ToString(): null,
            };
        }

	}
}