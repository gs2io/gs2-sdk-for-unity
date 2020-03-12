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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Money.Model
{
	[Preserve]
	public class WalletDetail
	{

        /** 単価 */
        public float? price { set; get; }

        /**
         * 単価を設定
         *
         * @param price 単価
         * @return this
         */
        public WalletDetail WithPrice(float? price) {
            this.price = price;
            return this;
        }

        /** 所持量 */
        public int? count { set; get; }

        /**
         * 所持量を設定
         *
         * @param count 所持量
         * @return this
         */
        public WalletDetail WithCount(int? count) {
            this.count = count;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.price.HasValue)
            {
                writer.WritePropertyName("price");
                writer.Write(this.price.Value);
            }
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static WalletDetail FromDict(JsonData data)
        {
            return new WalletDetail()
                .WithPrice(data.Keys.Contains("price") && data["price"] != null ? (float?)float.Parse(data["price"].ToString()) : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null);
        }
	}
}