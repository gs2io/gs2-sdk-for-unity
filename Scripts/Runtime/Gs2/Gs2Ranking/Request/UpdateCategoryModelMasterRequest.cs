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
using Gs2.Gs2Ranking.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Ranking.Request
{
	[Preserve]
	public class UpdateCategoryModelMasterRequest : Gs2Request<UpdateCategoryModelMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** カテゴリモデル名 */
        public string categoryName { set; get; }

        /**
         * カテゴリモデル名を設定
         *
         * @param categoryName カテゴリモデル名
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }


        /** カテゴリマスターの説明 */
        public string description { set; get; }

        /**
         * カテゴリマスターの説明を設定
         *
         * @param description カテゴリマスターの説明
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** カテゴリマスターのメタデータ */
        public string metadata { set; get; }

        /**
         * カテゴリマスターのメタデータを設定
         *
         * @param metadata カテゴリマスターのメタデータ
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }


        /** スコアの最小値 */
        public long? minimumValue { set; get; }

        /**
         * スコアの最小値を設定
         *
         * @param minimumValue スコアの最小値
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithMinimumValue(long? minimumValue) {
            this.minimumValue = minimumValue;
            return this;
        }


        /** スコアの最大値 */
        public long? maximumValue { set; get; }

        /**
         * スコアの最大値を設定
         *
         * @param maximumValue スコアの最大値
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithMaximumValue(long? maximumValue) {
            this.maximumValue = maximumValue;
            return this;
        }


        /** スコアのソート方向 */
        public string orderDirection { set; get; }

        /**
         * スコアのソート方向を設定
         *
         * @param orderDirection スコアのソート方向
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithOrderDirection(string orderDirection) {
            this.orderDirection = orderDirection;
            return this;
        }


        /** ランキングの種類 */
        public string scope { set; get; }

        /**
         * ランキングの種類を設定
         *
         * @param scope ランキングの種類
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithScope(string scope) {
            this.scope = scope;
            return this;
        }


        /** ユーザID毎にスコアを1つしか登録されないようにする */
        public bool? uniqueByUserId { set; get; }

        /**
         * ユーザID毎にスコアを1つしか登録されないようにするを設定
         *
         * @param uniqueByUserId ユーザID毎にスコアを1つしか登録されないようにする
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithUniqueByUserId(bool? uniqueByUserId) {
            this.uniqueByUserId = uniqueByUserId;
            return this;
        }


        /** スコアの集計間隔(分) */
        public int? calculateIntervalMinutes { set; get; }

        /**
         * スコアの集計間隔(分)を設定
         *
         * @param calculateIntervalMinutes スコアの集計間隔(分)
         * @return this
         */
        public UpdateCategoryModelMasterRequest WithCalculateIntervalMinutes(int? calculateIntervalMinutes) {
            this.calculateIntervalMinutes = calculateIntervalMinutes;
            return this;
        }


    	[Preserve]
        public static UpdateCategoryModelMasterRequest FromDict(JsonData data)
        {
            return new UpdateCategoryModelMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                categoryName = data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                metadata = data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString(): null,
                minimumValue = data.Keys.Contains("minimumValue") && data["minimumValue"] != null ? (long?)long.Parse(data["minimumValue"].ToString()) : null,
                maximumValue = data.Keys.Contains("maximumValue") && data["maximumValue"] != null ? (long?)long.Parse(data["maximumValue"].ToString()) : null,
                orderDirection = data.Keys.Contains("orderDirection") && data["orderDirection"] != null ? data["orderDirection"].ToString(): null,
                scope = data.Keys.Contains("scope") && data["scope"] != null ? data["scope"].ToString(): null,
                uniqueByUserId = data.Keys.Contains("uniqueByUserId") && data["uniqueByUserId"] != null ? (bool?)bool.Parse(data["uniqueByUserId"].ToString()) : null,
                calculateIntervalMinutes = data.Keys.Contains("calculateIntervalMinutes") && data["calculateIntervalMinutes"] != null ? (int?)int.Parse(data["calculateIntervalMinutes"].ToString()) : null,
            };
        }

	}
}