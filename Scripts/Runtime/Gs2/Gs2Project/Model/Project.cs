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

namespace Gs2.Gs2Project.Model
{
	[Preserve]
	public class Project
	{

        /** プロジェクト */
        public string projectId { set; get; }

        /**
         * プロジェクトを設定
         *
         * @param projectId プロジェクト
         * @return this
         */
        public Project WithProjectId(string projectId) {
            this.projectId = projectId;
            return this;
        }

        /** GS2アカウントの名前 */
        public string accountName { set; get; }

        /**
         * GS2アカウントの名前を設定
         *
         * @param accountName GS2アカウントの名前
         * @return this
         */
        public Project WithAccountName(string accountName) {
            this.accountName = accountName;
            return this;
        }

        /** プロジェクト名 */
        public string name { set; get; }

        /**
         * プロジェクト名を設定
         *
         * @param name プロジェクト名
         * @return this
         */
        public Project WithName(string name) {
            this.name = name;
            return this;
        }

        /** プロジェクトの説明 */
        public string description { set; get; }

        /**
         * プロジェクトの説明を設定
         *
         * @param description プロジェクトの説明
         * @return this
         */
        public Project WithDescription(string description) {
            this.description = description;
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
        public Project WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public Project WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.projectId != null)
            {
                writer.WritePropertyName("projectId");
                writer.Write(this.projectId);
            }
            if(this.accountName != null)
            {
                writer.WritePropertyName("accountName");
                writer.Write(this.accountName);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

        public static Project FromDict(JsonData data)
        {
            return new Project()
                .WithProjectId(data.Keys.Contains("projectId") ? (string) data["projectId"] : null)
                .WithAccountName(data.Keys.Contains("accountName") ? (string) data["accountName"] : null)
                .WithName(data.Keys.Contains("name") ? (string) data["name"] : null)
                .WithDescription(data.Keys.Contains("description") ? (string) data["description"] : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") ? (long?) data["createdAt"] : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") ? (long?) data["updatedAt"] : null);
        }
	}
}