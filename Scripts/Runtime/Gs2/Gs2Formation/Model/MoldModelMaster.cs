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
	public class MoldModelMaster
	{

        /** フォームの保存領域マスター */
        public string moldModelId { set; get; }

        /**
         * フォームの保存領域マスターを設定
         *
         * @param moldModelId フォームの保存領域マスター
         * @return this
         */
        public MoldModelMaster WithMoldModelId(string moldModelId) {
            this.moldModelId = moldModelId;
            return this;
        }

        /** フォームの保存領域名 */
        public string name { set; get; }

        /**
         * フォームの保存領域名を設定
         *
         * @param name フォームの保存領域名
         * @return this
         */
        public MoldModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** フォームの保存領域マスターの説明 */
        public string description { set; get; }

        /**
         * フォームの保存領域マスターの説明を設定
         *
         * @param description フォームの保存領域マスターの説明
         * @return this
         */
        public MoldModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** フォームの保存領域のメタデータ */
        public string metadata { set; get; }

        /**
         * フォームの保存領域のメタデータを設定
         *
         * @param metadata フォームの保存領域のメタデータ
         * @return this
         */
        public MoldModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** フォームマスター */
        public string formModelId { set; get; }

        /**
         * フォームマスターを設定
         *
         * @param formModelId フォームマスター
         * @return this
         */
        public MoldModelMaster WithFormModelId(string formModelId) {
            this.formModelId = formModelId;
            return this;
        }

        /** フォームを保存できる初期キャパシティ */
        public int? initialMaxCapacity { set; get; }

        /**
         * フォームを保存できる初期キャパシティを設定
         *
         * @param initialMaxCapacity フォームを保存できる初期キャパシティ
         * @return this
         */
        public MoldModelMaster WithInitialMaxCapacity(int? initialMaxCapacity) {
            this.initialMaxCapacity = initialMaxCapacity;
            return this;
        }

        /** フォームを保存できるキャパシティ */
        public int? maxCapacity { set; get; }

        /**
         * フォームを保存できるキャパシティを設定
         *
         * @param maxCapacity フォームを保存できるキャパシティ
         * @return this
         */
        public MoldModelMaster WithMaxCapacity(int? maxCapacity) {
            this.maxCapacity = maxCapacity;
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
        public MoldModelMaster WithCreatedAt(long? createdAt) {
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
        public MoldModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.moldModelId != null)
            {
                writer.WritePropertyName("moldModelId");
                writer.Write(this.moldModelId);
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
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.formModelId != null)
            {
                writer.WritePropertyName("formModelId");
                writer.Write(this.formModelId);
            }
            if(this.initialMaxCapacity.HasValue)
            {
                writer.WritePropertyName("initialMaxCapacity");
                writer.Write(this.initialMaxCapacity.Value);
            }
            if(this.maxCapacity.HasValue)
            {
                writer.WritePropertyName("maxCapacity");
                writer.Write(this.maxCapacity.Value);
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

    	[Preserve]
        public static MoldModelMaster FromDict(JsonData data)
        {
            return new MoldModelMaster()
                .WithMoldModelId(data.Keys.Contains("moldModelId") && data["moldModelId"] != null ? data["moldModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithFormModelId(data.Keys.Contains("formModelId") && data["formModelId"] != null ? data["formModelId"].ToString() : null)
                .WithInitialMaxCapacity(data.Keys.Contains("initialMaxCapacity") && data["initialMaxCapacity"] != null ? (int?)int.Parse(data["initialMaxCapacity"].ToString()) : null)
                .WithMaxCapacity(data.Keys.Contains("maxCapacity") && data["maxCapacity"] != null ? (int?)int.Parse(data["maxCapacity"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}