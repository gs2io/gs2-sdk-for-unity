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

namespace Gs2.Gs2Deploy.Model
{
	[Preserve]
	public class Output
	{

        /** アウトプット */
        public string outputId { set; get; }

        /**
         * アウトプットを設定
         *
         * @param outputId アウトプット
         * @return this
         */
        public Output WithOutputId(string outputId) {
            this.outputId = outputId;
            return this;
        }

        /** アウトプット名 */
        public string name { set; get; }

        /**
         * アウトプット名を設定
         *
         * @param name アウトプット名
         * @return this
         */
        public Output WithName(string name) {
            this.name = name;
            return this;
        }

        /** 値 */
        public string value { set; get; }

        /**
         * 値を設定
         *
         * @param value 値
         * @return this
         */
        public Output WithValue(string value) {
            this.value = value;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public Output WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.outputId != null)
            {
                writer.WritePropertyName("outputId");
                writer.Write(this.outputId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.value != null)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Output FromDict(JsonData data)
        {
            return new Output()
                .WithOutputId(data.Keys.Contains("outputId") && data["outputId"] != null ? data["outputId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? data["value"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}