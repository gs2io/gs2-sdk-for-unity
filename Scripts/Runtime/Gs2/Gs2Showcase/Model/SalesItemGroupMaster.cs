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

namespace Gs2.Gs2Showcase.Model
{
	[Preserve]
	public class SalesItemGroupMaster
	{

        /** 商品グループマスター */
        public string salesItemGroupId { set; get; }

        /**
         * 商品グループマスターを設定
         *
         * @param salesItemGroupId 商品グループマスター
         * @return this
         */
        public SalesItemGroupMaster WithSalesItemGroupId(string salesItemGroupId) {
            this.salesItemGroupId = salesItemGroupId;
            return this;
        }

        /** 商品名 */
        public string name { set; get; }

        /**
         * 商品名を設定
         *
         * @param name 商品名
         * @return this
         */
        public SalesItemGroupMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** 商品グループマスターの説明 */
        public string description { set; get; }

        /**
         * 商品グループマスターの説明を設定
         *
         * @param description 商品グループマスターの説明
         * @return this
         */
        public SalesItemGroupMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** 商品のメタデータ */
        public string metadata { set; get; }

        /**
         * 商品のメタデータを設定
         *
         * @param metadata 商品のメタデータ
         * @return this
         */
        public SalesItemGroupMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 商品グループに含める商品リスト */
        public List<string> salesItemNames { set; get; }

        /**
         * 商品グループに含める商品リストを設定
         *
         * @param salesItemNames 商品グループに含める商品リスト
         * @return this
         */
        public SalesItemGroupMaster WithSalesItemNames(List<string> salesItemNames) {
            this.salesItemNames = salesItemNames;
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
        public SalesItemGroupMaster WithCreatedAt(long? createdAt) {
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
        public SalesItemGroupMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.salesItemGroupId != null)
            {
                writer.WritePropertyName("salesItemGroupId");
                writer.Write(this.salesItemGroupId);
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
            if(this.salesItemNames != null)
            {
                writer.WritePropertyName("salesItemNames");
                writer.WriteArrayStart();
                foreach(var item in this.salesItemNames)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
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
        public static SalesItemGroupMaster FromDict(JsonData data)
        {
            return new SalesItemGroupMaster()
                .WithSalesItemGroupId(data.Keys.Contains("salesItemGroupId") && data["salesItemGroupId"] != null ? data["salesItemGroupId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithSalesItemNames(data.Keys.Contains("salesItemNames") && data["salesItemNames"] != null ? data["salesItemNames"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}