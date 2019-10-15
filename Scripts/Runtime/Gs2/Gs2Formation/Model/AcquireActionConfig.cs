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
	public class AcquireActionConfig
	{

        /** スロット名 */
        public string name { set; get; }

        /**
         * スロット名を設定
         *
         * @param name スロット名
         * @return this
         */
        public AcquireActionConfig WithName(string name) {
            this.name = name;
            return this;
        }

        /** スタンプシートに使用するコンフィグ */
        public List<Config> config { set; get; }

        /**
         * スタンプシートに使用するコンフィグを設定
         *
         * @param config スタンプシートに使用するコンフィグ
         * @return this
         */
        public AcquireActionConfig WithConfig(List<Config> config) {
            this.config = config;
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
            if(this.config != null)
            {
                writer.WritePropertyName("config");
                writer.WriteArrayStart();
                foreach(var item in this.config)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static AcquireActionConfig FromDict(JsonData data)
        {
            return new AcquireActionConfig()
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithConfig(data.Keys.Contains("config") && data["config"] != null ? data["config"].Cast<JsonData>().Select(value =>
                    {
                        return Config.FromDict(value);
                    }
                ).ToList() : null);
        }
	}
}