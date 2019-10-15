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
	public class SignTargetVersion
	{

        /** None */
        public string region { set; get; }

        /**
         * Noneを設定
         *
         * @param region None
         * @return this
         */
        public SignTargetVersion WithRegion(string region) {
            this.region = region;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public SignTargetVersion WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public SignTargetVersion WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }

        /** バージョンの種類名 */
        public string versionName { set; get; }

        /**
         * バージョンの種類名を設定
         *
         * @param versionName バージョンの種類名
         * @return this
         */
        public SignTargetVersion WithVersionName(string versionName) {
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
        public SignTargetVersion WithVersion(Version_ version) {
            this.version = version;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.region != null)
            {
                writer.WritePropertyName("region");
                writer.Write(this.region);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.namespaceName != null)
            {
                writer.WritePropertyName("namespaceName");
                writer.Write(this.namespaceName);
            }
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static SignTargetVersion FromDict(JsonData data)
        {
            return new SignTargetVersion()
                .WithRegion(data.Keys.Contains("region") && data["region"] != null ? data["region"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithNamespaceName(data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString() : null)
                .WithVersionName(data.Keys.Contains("versionName") && data["versionName"] != null ? data["versionName"].ToString() : null)
                .WithVersion(data.Keys.Contains("version") && data["version"] != null ? Version_.FromDict(data["version"]) : null);
        }
	}
}