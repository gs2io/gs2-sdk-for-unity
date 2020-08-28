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
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Enhance.Model
{
	[Preserve]
	public class BonusRate : IComparable
	{

        /** 経験値ボーナスの倍率(1.0=ボーナスなし) */
        public float? rate { set; get; }

        /**
         * 経験値ボーナスの倍率(1.0=ボーナスなし)を設定
         *
         * @param rate 経験値ボーナスの倍率(1.0=ボーナスなし)
         * @return this
         */
        public BonusRate WithRate(float? rate) {
            this.rate = rate;
            return this;
        }

        /** 抽選重み */
        public int? weight { set; get; }

        /**
         * 抽選重みを設定
         *
         * @param weight 抽選重み
         * @return this
         */
        public BonusRate WithWeight(int? weight) {
            this.weight = weight;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.rate.HasValue)
            {
                writer.WritePropertyName("rate");
                writer.Write(this.rate.Value);
            }
            if(this.weight.HasValue)
            {
                writer.WritePropertyName("weight");
                writer.Write(this.weight.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static BonusRate FromDict(JsonData data)
        {
            return new BonusRate()
                .WithRate(data.Keys.Contains("rate") && data["rate"] != null ? (float?)float.Parse(data["rate"].ToString()) : null)
                .WithWeight(data.Keys.Contains("weight") && data["weight"] != null ? (int?)int.Parse(data["weight"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as BonusRate;
            var diff = 0;
            if (rate == null && rate == other.rate)
            {
                // null and null
            }
            else
            {
                diff += (int)(rate - other.rate);
            }
            if (weight == null && weight == other.weight)
            {
                // null and null
            }
            else
            {
                diff += (int)(weight - other.weight);
            }
            return diff;
        }
	}
}