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
	public class Probability
	{

        /** 景品の種類 */
        public DrawnPrize prize { set; get; }

        /**
         * 景品の種類を設定
         *
         * @param prize 景品の種類
         * @return this
         */
        public Probability WithPrize(DrawnPrize prize) {
            this.prize = prize;
            return this;
        }

        /** 排出確率(0.0〜1.0) */
        public float? rate { set; get; }

        /**
         * 排出確率(0.0〜1.0)を設定
         *
         * @param rate 排出確率(0.0〜1.0)
         * @return this
         */
        public Probability WithRate(float? rate) {
            this.rate = rate;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.prize != null)
            {
                writer.WritePropertyName("prize");
                this.prize.WriteJson(writer);
            }
            if(this.rate.HasValue)
            {
                writer.WritePropertyName("rate");
                writer.Write(this.rate.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Probability FromDict(JsonData data)
        {
            return new Probability()
                .WithPrize(data.Keys.Contains("prize") && data["prize"] != null ? DrawnPrize.FromDict(data["prize"]) : null)
                .WithRate(data.Keys.Contains("rate") && data["rate"] != null ? (float?)float.Parse(data["rate"].ToString()) : null);
        }
	}
}