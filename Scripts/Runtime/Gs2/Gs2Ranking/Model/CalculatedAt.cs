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
	public class CalculatedAt
	{

        /** カテゴリ名 */
        public string categoryName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param categoryName カテゴリ名
         * @return this
         */
        public CalculatedAt WithCategoryName(string categoryName) {
            this.categoryName = categoryName;
            return this;
        }

        /** 集計日時 */
        public long? calculatedAt { set; get; }

        /**
         * 集計日時を設定
         *
         * @param calculatedAt 集計日時
         * @return this
         */
        public CalculatedAt WithCalculatedAt(long? calculatedAt) {
            this.calculatedAt = calculatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.categoryName != null)
            {
                writer.WritePropertyName("categoryName");
                writer.Write(this.categoryName);
            }
            if(this.calculatedAt.HasValue)
            {
                writer.WritePropertyName("calculatedAt");
                writer.Write(this.calculatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static CalculatedAt FromDict(JsonData data)
        {
            return new CalculatedAt()
                .WithCategoryName(data.Keys.Contains("categoryName") && data["categoryName"] != null ? data["categoryName"].ToString() : null)
                .WithCalculatedAt(data.Keys.Contains("calculatedAt") && data["calculatedAt"] != null ? (long?)long.Parse(data["calculatedAt"].ToString()) : null);
        }
	}
}