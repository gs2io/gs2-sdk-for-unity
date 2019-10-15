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

namespace Gs2.Gs2Version.Model
{
	[Preserve]
	public class TargetVersion
	{

        /** バージョンの名前 */
        public string versionName { set; get; }

        /**
         * バージョンの名前を設定
         *
         * @param versionName バージョンの名前
         * @return this
         */
        public TargetVersion WithVersionName(string versionName) {
            this.versionName = versionName;
            return this;
        }

        /** バージョン */
        public Version_ version { set; get; }

        /**
         * バージョンを設定
         *
         * @param version バージョン
         * @return this
         */
        public TargetVersion WithVersion(Version_ version) {
            this.version = version;
            return this;
        }

        /** ボディ */
        public string body { set; get; }

        /**
         * ボディを設定
         *
         * @param body ボディ
         * @return this
         */
        public TargetVersion WithBody(string body) {
            this.body = body;
            return this;
        }

        /** 署名 */
        public string signature { set; get; }

        /**
         * 署名を設定
         *
         * @param signature 署名
         * @return this
         */
        public TargetVersion WithSignature(string signature) {
            this.signature = signature;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.versionName != null)
            {
                writer.WritePropertyName("versionName");
                writer.Write(this.versionName);
            }
            if(this.version != null)
            {
                writer.WritePropertyName("version");
                this.version.WriteJson(writer);
            }
            if(this.body != null)
            {
                writer.WritePropertyName("body");
                writer.Write(this.body);
            }
            if(this.signature != null)
            {
                writer.WritePropertyName("signature");
                writer.Write(this.signature);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static TargetVersion FromDict(JsonData data)
        {
            return new TargetVersion()
                .WithVersionName(data.Keys.Contains("versionName") && data["versionName"] != null ? data["versionName"].ToString() : null)
                .WithVersion(data.Keys.Contains("version") && data["version"] != null ? Version_.FromDict(data["version"]) : null)
                .WithBody(data.Keys.Contains("body") && data["body"] != null ? data["body"].ToString() : null)
                .WithSignature(data.Keys.Contains("signature") && data["signature"] != null ? data["signature"].ToString() : null);
        }
	}
}