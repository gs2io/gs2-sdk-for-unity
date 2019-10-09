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

namespace Gs2.Gs2Lottery.Model
{
	[Preserve]
	public class LotteryModelMaster
	{

        /** 抽選の種類マスター */
        public string lotteryModelId { set; get; }

        /**
         * 抽選の種類マスターを設定
         *
         * @param lotteryModelId 抽選の種類マスター
         * @return this
         */
        public LotteryModelMaster WithLotteryModelId(string lotteryModelId) {
            this.lotteryModelId = lotteryModelId;
            return this;
        }

        /** 抽選モデルの種類名 */
        public string name { set; get; }

        /**
         * 抽選モデルの種類名を設定
         *
         * @param name 抽選モデルの種類名
         * @return this
         */
        public LotteryModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 抽選モデルの種類のメタデータ */
        public string metadata { set; get; }

        /**
         * 抽選モデルの種類のメタデータを設定
         *
         * @param metadata 抽選モデルの種類のメタデータ
         * @return this
         */
        public LotteryModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 抽選の種類マスターの説明 */
        public string description { set; get; }

        /**
         * 抽選の種類マスターの説明を設定
         *
         * @param description 抽選の種類マスターの説明
         * @return this
         */
        public LotteryModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 抽選モード */
        public string mode { set; get; }

        /**
         * 抽選モードを設定
         *
         * @param mode 抽選モード
         * @return this
         */
        public LotteryModelMaster WithMode(string mode) {
            this.mode = mode;
            return this;
        }

        /** 抽選方法 */
        public string method { set; get; }

        /**
         * 抽選方法を設定
         *
         * @param method 抽選方法
         * @return this
         */
        public LotteryModelMaster WithMethod(string method) {
            this.method = method;
            return this;
        }

        /** 景品テーブルの名前 */
        public string prizeTableName { set; get; }

        /**
         * 景品テーブルの名前を設定
         *
         * @param prizeTableName 景品テーブルの名前
         * @return this
         */
        public LotteryModelMaster WithPrizeTableName(string prizeTableName) {
            this.prizeTableName = prizeTableName;
            return this;
        }

        /** 抽選テーブルを確定するスクリプト のGRN */
        public string choicePrizeTableScriptId { set; get; }

        /**
         * 抽選テーブルを確定するスクリプト のGRNを設定
         *
         * @param choicePrizeTableScriptId 抽選テーブルを確定するスクリプト のGRN
         * @return this
         */
        public LotteryModelMaster WithChoicePrizeTableScriptId(string choicePrizeTableScriptId) {
            this.choicePrizeTableScriptId = choicePrizeTableScriptId;
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
        public LotteryModelMaster WithCreatedAt(long? createdAt) {
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
        public LotteryModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.lotteryModelId != null)
            {
                writer.WritePropertyName("lotteryModelId");
                writer.Write(this.lotteryModelId);
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
            if(this.mode != null)
            {
                writer.WritePropertyName("mode");
                writer.Write(this.mode);
            }
            if(this.method != null)
            {
                writer.WritePropertyName("method");
                writer.Write(this.method);
            }
            if(this.prizeTableName != null)
            {
                writer.WritePropertyName("prizeTableName");
                writer.Write(this.prizeTableName);
            }
            if(this.choicePrizeTableScriptId != null)
            {
                writer.WritePropertyName("choicePrizeTableScriptId");
                writer.Write(this.choicePrizeTableScriptId);
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
        public static LotteryModelMaster FromDict(JsonData data)
        {
            return new LotteryModelMaster()
                .WithLotteryModelId(data.Keys.Contains("lotteryModelId") && data["lotteryModelId"] != null ? data["lotteryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMode(data.Keys.Contains("mode") && data["mode"] != null ? data["mode"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithPrizeTableName(data.Keys.Contains("prizeTableName") && data["prizeTableName"] != null ? data["prizeTableName"].ToString() : null)
                .WithChoicePrizeTableScriptId(data.Keys.Contains("choicePrizeTableScriptId") && data["choicePrizeTableScriptId"] != null ? data["choicePrizeTableScriptId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}