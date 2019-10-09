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
	public class OutputField
	{

        /** 名前 */
        public string name { set; get; }

        /**
         * 名前を設定
         *
         * @param name 名前
         * @return this
         */
        public OutputField WithName(string name) {
            this.name = name;
            return this;
        }

        /** フィールド名 */
        public string fieldName { set; get; }

        /**
         * フィールド名を設定
         *
         * @param fieldName フィールド名
         * @return this
         */
        public OutputField WithFieldName(string fieldName) {
            this.fieldName = fieldName;
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
            if(this.fieldName != null)
            {
                writer.WritePropertyName("fieldName");
                writer.Write(this.fieldName);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static OutputField FromDict(JsonData data)
        {
            return new OutputField()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithFieldName(data.Keys.Contains("fieldName") && data["fieldName"] != null ? data["fieldName"].ToString() : null);
        }
	}
}