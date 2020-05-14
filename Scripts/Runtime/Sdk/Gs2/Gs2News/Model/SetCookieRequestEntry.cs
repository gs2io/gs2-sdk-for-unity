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

namespace Gs2.Gs2News.Model
{
	[Preserve]
	public class SetCookieRequestEntry
	{

        /** 記事を閲覧できるようにするために設定してほしい Cookie のキー値 */
        public string key { set; get; }

        /**
         * 記事を閲覧できるようにするために設定してほしい Cookie のキー値を設定
         *
         * @param key 記事を閲覧できるようにするために設定してほしい Cookie のキー値
         * @return this
         */
        public SetCookieRequestEntry WithKey(string key) {
            this.key = key;
            return this;
        }

        /** 記事を閲覧できるようにするために設定してほしい Cookie の値 */
        public string value { set; get; }

        /**
         * 記事を閲覧できるようにするために設定してほしい Cookie の値を設定
         *
         * @param value 記事を閲覧できるようにするために設定してほしい Cookie の値
         * @return this
         */
        public SetCookieRequestEntry WithValue(string value) {
            this.value = value;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.key != null)
            {
                writer.WritePropertyName("key");
                writer.Write(this.key);
            }
            if(this.value != null)
            {
                writer.WritePropertyName("value");
                writer.Write(this.value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SetCookieRequestEntry FromDict(JsonData data)
        {
            return new SetCookieRequestEntry()
                .WithKey(data.Keys.Contains("key") && data["key"] != null ? data["key"].ToString() : null)
                .WithValue(data.Keys.Contains("value") && data["value"] != null ? data["value"].ToString() : null);
        }
	}
}