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
	public class LotteryModel
	{

        /** 抽選の種類マスター */
        public string lotteryModelId { set; get; }

        /**
         * 抽選の種類マスターを設定
         *
         * @param lotteryModelId 抽選の種類マスター
         * @return this
         */
        public LotteryModel WithLotteryModelId(string lotteryModelId) {
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
        public LotteryModel WithName(string name) {
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
        public LotteryModel WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public LotteryModel WithMode(string mode) {
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
        public LotteryModel WithMethod(string method) {
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
        public LotteryModel WithPrizeTableName(string prizeTableName) {
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
        public LotteryModel WithChoicePrizeTableScriptId(string choicePrizeTableScriptId) {
            this.choicePrizeTableScriptId = choicePrizeTableScriptId;
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static LotteryModel FromDict(JsonData data)
        {
            return new LotteryModel()
                .WithLotteryModelId(data.Keys.Contains("lotteryModelId") && data["lotteryModelId"] != null ? data["lotteryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithMode(data.Keys.Contains("mode") && data["mode"] != null ? data["mode"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithPrizeTableName(data.Keys.Contains("prizeTableName") && data["prizeTableName"] != null ? data["prizeTableName"].ToString() : null)
                .WithChoicePrizeTableScriptId(data.Keys.Contains("choicePrizeTableScriptId") && data["choicePrizeTableScriptId"] != null ? data["choicePrizeTableScriptId"].ToString() : null);
        }
	}
}