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
using Gs2.Gs2Lottery.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Lottery.Request
{
	[Preserve]
	public class UpdateLotteryModelMasterRequest : Gs2Request<UpdateLotteryModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateLotteryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 抽選モデルの種類名 */
        public string lotteryName { set; get; }

        /**
         * 抽選モデルの種類名を設定
         *
         * @param lotteryName 抽選モデルの種類名
         * @return this
         */
        public UpdateLotteryModelMasterRequest WithLotteryName(string lotteryName) {
            this.lotteryName = lotteryName;
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
        public UpdateLotteryModelMasterRequest WithDescription(string description) {
            this.description = description;
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
        public UpdateLotteryModelMasterRequest WithMetadata(string metadata) {
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
        public UpdateLotteryModelMasterRequest WithMode(string mode) {
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
        public UpdateLotteryModelMasterRequest WithMethod(string method) {
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
        public UpdateLotteryModelMasterRequest WithPrizeTableName(string prizeTableName) {
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
        public UpdateLotteryModelMasterRequest WithChoicePrizeTableScriptId(string choicePrizeTableScriptId) {
            this.choicePrizeTableScriptId = choicePrizeTableScriptId;
            return this;
        }


    	[Preserve]
        public static UpdateLotteryModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateLotteryModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                lotteryName = data.Keys.Contains("lotteryName") && data["lotteryName"] != null ? data["lotteryName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                mode = data.Keys.Contains("mode") && data["mode"] != null ? data["mode"].ToString(): null,
                method = data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString(): null,
                prizeTableName = data.Keys.Contains("prizeTableName") && data["prizeTableName"] != null ? data["prizeTableName"].ToString(): null,
                choicePrizeTableScriptId = data.Keys.Contains("choicePrizeTableScriptId") && data["choicePrizeTableScriptId"] != null ? data["choicePrizeTableScriptId"].ToString(): null,
            };
        }

	}
}