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
	public class Material : IComparable
	{

        /** 強化対象の GS2-Inventory アイテムセットGRN */
        public string materialItemSetId { set; get; }

        /**
         * 強化対象の GS2-Inventory アイテムセットGRNを設定
         *
         * @param materialItemSetId 強化対象の GS2-Inventory アイテムセットGRN
         * @return this
         */
        public Material WithMaterialItemSetId(string materialItemSetId) {
            this.materialItemSetId = materialItemSetId;
            return this;
        }

        /** 消費数量 */
        public int? count { set; get; }

        /**
         * 消費数量を設定
         *
         * @param count 消費数量
         * @return this
         */
        public Material WithCount(int? count) {
            this.count = count;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.materialItemSetId != null)
            {
                writer.WritePropertyName("materialItemSetId");
                writer.Write(this.materialItemSetId);
            }
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Material FromDict(JsonData data)
        {
            return new Material()
                .WithMaterialItemSetId(data.Keys.Contains("materialItemSetId") && data["materialItemSetId"] != null ? data["materialItemSetId"].ToString() : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (int?)int.Parse(data["count"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Material;
            var diff = 0;
            if (materialItemSetId == null && materialItemSetId == other.materialItemSetId)
            {
                // null and null
            }
            else
            {
                diff += materialItemSetId.CompareTo(other.materialItemSetId);
            }
            if (count == null && count == other.count)
            {
                // null and null
            }
            else
            {
                diff += (int)(count - other.count);
            }
            return diff;
        }
	}
}