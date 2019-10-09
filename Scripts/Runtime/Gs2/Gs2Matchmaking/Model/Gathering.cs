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

namespace Gs2.Gs2Matchmaking.Model
{
	[Preserve]
	public class Gathering
	{

        /** ギャザリング */
        public string gatheringId { set; get; }

        /**
         * ギャザリングを設定
         *
         * @param gatheringId ギャザリング
         * @return this
         */
        public Gathering WithGatheringId(string gatheringId) {
            this.gatheringId = gatheringId;
            return this;
        }

        /** ギャザリング名 */
        public string name { set; get; }

        /**
         * ギャザリング名を設定
         *
         * @param name ギャザリング名
         * @return this
         */
        public Gathering WithName(string name) {
            this.name = name;
            return this;
        }

        /** 募集条件 */
        public List<AttributeRange> attributeRanges { set; get; }

        /**
         * 募集条件を設定
         *
         * @param attributeRanges 募集条件
         * @return this
         */
        public Gathering WithAttributeRanges(List<AttributeRange> attributeRanges) {
            this.attributeRanges = attributeRanges;
            return this;
        }

        /** 参加者 */
        public List<CapacityOfRole> capacityOfRoles { set; get; }

        /**
         * 参加者を設定
         *
         * @param capacityOfRoles 参加者
         * @return this
         */
        public Gathering WithCapacityOfRoles(List<CapacityOfRole> capacityOfRoles) {
            this.capacityOfRoles = capacityOfRoles;
            return this;
        }

        /** 参加を許可するユーザIDリスト */
        public List<string> allowUserIds { set; get; }

        /**
         * 参加を許可するユーザIDリストを設定
         *
         * @param allowUserIds 参加を許可するユーザIDリスト
         * @return this
         */
        public Gathering WithAllowUserIds(List<string> allowUserIds) {
            this.allowUserIds = allowUserIds;
            return this;
        }

        /** メタデータ */
        public string metadata { set; get; }

        /**
         * メタデータを設定
         *
         * @param metadata メタデータ
         * @return this
         */
        public Gathering WithMetadata(string metadata) {
            this.metadata = metadata;
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
        public Gathering WithCreatedAt(long? createdAt) {
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
        public Gathering WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.gatheringId != null)
            {
                writer.WritePropertyName("gatheringId");
                writer.Write(this.gatheringId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.attributeRanges != null)
            {
                writer.WritePropertyName("attributeRanges");
                writer.WriteArrayStart();
                foreach(var item in this.attributeRanges)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.capacityOfRoles != null)
            {
                writer.WritePropertyName("capacityOfRoles");
                writer.WriteArrayStart();
                foreach(var item in this.capacityOfRoles)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.allowUserIds != null)
            {
                writer.WritePropertyName("allowUserIds");
                writer.WriteArrayStart();
                foreach(var item in this.allowUserIds)
                {
                    writer.Write(item);
                }
                writer.WriteArrayEnd();
            }
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
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
        public static Gathering FromDict(JsonData data)
        {
            return new Gathering()
                .WithGatheringId(data.Keys.Contains("gatheringId") && data["gatheringId"] != null ? data["gatheringId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithAttributeRanges(data.Keys.Contains("attributeRanges") && data["attributeRanges"] != null ? data["attributeRanges"].Cast<JsonData>().Select(value =>
                    {
                        return AttributeRange.FromDict(value);
                    }
                ).ToList() : null)
                .WithCapacityOfRoles(data.Keys.Contains("capacityOfRoles") && data["capacityOfRoles"] != null ? data["capacityOfRoles"].Cast<JsonData>().Select(value =>
                    {
                        return CapacityOfRole.FromDict(value);
                    }
                ).ToList() : null)
                .WithAllowUserIds(data.Keys.Contains("allowUserIds") && data["allowUserIds"] != null ? data["allowUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}