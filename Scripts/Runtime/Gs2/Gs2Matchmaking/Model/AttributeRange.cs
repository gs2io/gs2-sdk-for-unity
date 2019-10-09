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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class AttributeRange
	{

        /** 属性名 */
        public string name { set; get; }

        /**
         * 属性名を設定
         *
         * @param name 属性名
         * @return this
         */
        public AttributeRange WithName(string name) {
            this.name = name;
            return this;
        }

        /** ギャザリング参加可能な属性値の最小値 */
        public int? min { set; get; }

        /**
         * ギャザリング参加可能な属性値の最小値を設定
         *
         * @param min ギャザリング参加可能な属性値の最小値
         * @return this
         */
        public AttributeRange WithMin(int? min) {
            this.min = min;
            return this;
        }

        /** ギャザリング参加可能な属性値の最大値 */
        public int? max { set; get; }

        /**
         * ギャザリング参加可能な属性値の最大値を設定
         *
         * @param max ギャザリング参加可能な属性値の最大値
         * @return this
         */
        public AttributeRange WithMax(int? max) {
            this.max = max;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.min.HasValue)
            {
                writer.WritePropertyName("min");
                writer.Write(this.min.Value);
            }
            if(this.max.HasValue)
            {
                writer.WritePropertyName("max");
                writer.Write(this.max.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static AttributeRange FromDict(JsonData data)
        {
            return new AttributeRange()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMin(data.Keys.Contains("min") && data["min"] != null ? (int?)int.Parse(data["min"].ToString()) : null)
                .WithMax(data.Keys.Contains("max") && data["max"] != null ? (int?)int.Parse(data["max"].ToString()) : null);
        }
	}
}