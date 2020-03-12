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

namespace Gs2.Gs2Ranking.Model
{
	[Preserve]
	public class CategoryModel
	{

        /** カテゴリ */
        public string categoryModelId { set; get; }

        /**
         * カテゴリを設定
         *
         * @param categoryModelId カテゴリ
         * @return this
         */
        public CategoryModel WithCategoryModelId(string categoryModelId) {
            this.categoryModelId = categoryModelId;
            return this;
        }

        /** カテゴリ名 */
        public string name { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param name カテゴリ名
         * @return this
         */
        public CategoryModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** カテゴリのメタデータ */
        public string metadata { set; get; }

        /**
         * カテゴリのメタデータを設定
         *
         * @param metadata カテゴリのメタデータ
         * @return this
         */
        public CategoryModel WithMetadata(string metadata) {
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
        public CategoryModel WithMinimumValue(long? minimumValue) {
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
        public CategoryModel WithMaximumValue(long? maximumValue) {
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
        public CategoryModel WithOrderDirection(string orderDirection) {
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
        public CategoryModel WithScope(string scope) {
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
        public CategoryModel WithUniqueByUserId(bool? uniqueByUserId) {
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
        public CategoryModel WithCalculateIntervalMinutes(int? calculateIntervalMinutes) {
            this.calculateIntervalMinutes = calculateIntervalMinutes;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.categoryModelId != null)
            {
                writer.WritePropertyName("categoryModelId");
                writer.Write(this.categoryModelId);
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
            if(this.minimumValue.HasValue)
            {
                writer.WritePropertyName("minimumValue");
                writer.Write(this.minimumValue.Value);
            }
            if(this.maximumValue.HasValue)
            {
                writer.WritePropertyName("maximumValue");
                writer.Write(this.maximumValue.Value);
            }
            if(this.orderDirection != null)
            {
                writer.WritePropertyName("orderDirection");
                writer.Write(this.orderDirection);
            }
            if(this.scope != null)
            {
                writer.WritePropertyName("scope");
                writer.Write(this.scope);
            }
            if(this.uniqueByUserId.HasValue)
            {
                writer.WritePropertyName("uniqueByUserId");
                writer.Write(this.uniqueByUserId.Value);
            }
            if(this.calculateIntervalMinutes.HasValue)
            {
                writer.WritePropertyName("calculateIntervalMinutes");
                writer.Write(this.calculateIntervalMinutes.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static CategoryModel FromDict(JsonData data)
        {
            return new CategoryModel()
                .WithCategoryModelId(data.Keys.Contains("categoryModelId") && data["categoryModelId"] != null ? data["categoryModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithMinimumValue(data.Keys.Contains("minimumValue") && data["minimumValue"] != null ? (long?)long.Parse(data["minimumValue"].ToString()) : null)
                .WithMaximumValue(data.Keys.Contains("maximumValue") && data["maximumValue"] != null ? (long?)long.Parse(data["maximumValue"].ToString()) : null)
                .WithOrderDirection(data.Keys.Contains("orderDirection") && data["orderDirection"] != null ? data["orderDirection"].ToString() : null)
                .WithScope(data.Keys.Contains("scope") && data["scope"] != null ? data["scope"].ToString() : null)
                .WithUniqueByUserId(data.Keys.Contains("uniqueByUserId") && data["uniqueByUserId"] != null ? (bool?)bool.Parse(data["uniqueByUserId"].ToString()) : null)
                .WithCalculateIntervalMinutes(data.Keys.Contains("calculateIntervalMinutes") && data["calculateIntervalMinutes"] != null ? (int?)int.Parse(data["calculateIntervalMinutes"].ToString()) : null);
        }
	}
}