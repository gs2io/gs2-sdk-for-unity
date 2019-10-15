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
	public class Status
	{

        /** バージョン設定 */
        public VersionModel versionModel { set; get; }

        /**
         * バージョン設定を設定
         *
         * @param versionModel バージョン設定
         * @return this
         */
        public Status WithVersionModel(VersionModel versionModel) {
            this.versionModel = versionModel;
            return this;
        }

        /** 現在のバージョン */
        public Version_ currentVersion { set; get; }

        /**
         * 現在のバージョンを設定
         *
         * @param currentVersion 現在のバージョン
         * @return this
         */
        public Status WithCurrentVersion(Version_ currentVersion) {
            this.currentVersion = currentVersion;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.versionModel != null)
            {
                writer.WritePropertyName("versionModel");
                this.versionModel.WriteJson(writer);
            }
            if(this.currentVersion != null)
            {
                writer.WritePropertyName("currentVersion");
                this.currentVersion.WriteJson(writer);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Status FromDict(JsonData data)
        {
            return new Status()
                .WithVersionModel(data.Keys.Contains("versionModel") && data["versionModel"] != null ? VersionModel.FromDict(data["versionModel"]) : null)
                .WithCurrentVersion(data.Keys.Contains("currentVersion") && data["currentVersion"] != null ? Version_.FromDict(data["currentVersion"]) : null);
        }
	}
}