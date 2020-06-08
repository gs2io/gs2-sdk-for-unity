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

namespace Gs2.Gs2Version.Model
{
	[Preserve]
	public class Version_ : IComparable
	{

        /** メジャーバージョン */
        public int? major { set; get; }

        /**
         * メジャーバージョンを設定
         *
         * @param major メジャーバージョン
         * @return this
         */
        public Version_ WithMajor(int? major) {
            this.major = major;
            return this;
        }

        /** マイナーバージョン */
        public int? minor { set; get; }

        /**
         * マイナーバージョンを設定
         *
         * @param minor マイナーバージョン
         * @return this
         */
        public Version_ WithMinor(int? minor) {
            this.minor = minor;
            return this;
        }

        /** マイクロバージョン */
        public int? micro { set; get; }

        /**
         * マイクロバージョンを設定
         *
         * @param micro マイクロバージョン
         * @return this
         */
        public Version_ WithMicro(int? micro) {
            this.micro = micro;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.major.HasValue)
            {
                writer.WritePropertyName("major");
                writer.Write(this.major.Value);
            }
            if(this.minor.HasValue)
            {
                writer.WritePropertyName("minor");
                writer.Write(this.minor.Value);
            }
            if(this.micro.HasValue)
            {
                writer.WritePropertyName("micro");
                writer.Write(this.micro.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Version_ FromDict(JsonData data)
        {
            return new Version_()
                .WithMajor(data.Keys.Contains("major") && data["major"] != null ? (int?)int.Parse(data["major"].ToString()) : null)
                .WithMinor(data.Keys.Contains("minor") && data["minor"] != null ? (int?)int.Parse(data["minor"].ToString()) : null)
                .WithMicro(data.Keys.Contains("micro") && data["micro"] != null ? (int?)int.Parse(data["micro"].ToString()) : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as Version_;
            var diff = 0;
            if (major == null && major == other.major)
            {
                // null and null
            }
            else
            {
                diff += (int)(major - other.major);
            }
            if (minor == null && minor == other.minor)
            {
                // null and null
            }
            else
            {
                diff += (int)(minor - other.minor);
            }
            if (micro == null && micro == other.micro)
            {
                // null and null
            }
            else
            {
                diff += (int)(micro - other.micro);
            }
            return diff;
        }
	}
}