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

namespace Gs2.Gs2Formation.Model
{
	[Preserve]
	public class SlotModel
	{

        /** スロットモデル名 */
        public string name { set; get; }

        /**
         * スロットモデル名を設定
         *
         * @param name スロットモデル名
         * @return this
         */
        public SlotModel WithName(string name) {
            this.name = name;
            return this;
        }

        /** プロパティとして設定可能な値の正規表現 */
        public string propertyRegex { set; get; }

        /**
         * プロパティとして設定可能な値の正規表現を設定
         *
         * @param propertyRegex プロパティとして設定可能な値の正規表現
         * @return this
         */
        public SlotModel WithPropertyRegex(string propertyRegex) {
            this.propertyRegex = propertyRegex;
            return this;
        }

        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public SlotModel WithMetadata(string metadata) {
            this.metadata = metadata;
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
            if(this.propertyRegex != null)
            {
                writer.WritePropertyName("propertyRegex");
                writer.Write(this.propertyRegex);
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SlotModel FromDict(JsonData data)
        {
            return new SlotModel()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithPropertyRegex(data.Keys.Contains("propertyRegex") && data["propertyRegex"] != null ? data["propertyRegex"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null);
        }
	}
}