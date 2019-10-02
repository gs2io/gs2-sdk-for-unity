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

namespace Gs2.Gs2Deploy.Model
{
	[Preserve]
	public class WorkingStack
	{

        /** 実行中のスタック */
        public string stackId { set; get; }

        /**
         * 実行中のスタックを設定
         *
         * @param stackId 実行中のスタック
         * @return this
         */
        public WorkingStack WithStackId(string stackId) {
            this.stackId = stackId;
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
        public WorkingStack WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** 実行中のスタック名 */
        public string name { set; get; }

        /**
         * 実行中のスタック名を設定
         *
         * @param name 実行中のスタック名
         * @return this
         */
        public WorkingStack WithName(string name) {
            this.name = name;
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
        public WorkingStack WithCreatedAt(long? createdAt) {
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
        public WorkingStack WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.stackId != null)
            {
                writer.WritePropertyName("stackId");
                writer.Write(this.stackId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
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

        public static WorkingStack FromDict(JsonData data)
        {
            return new WorkingStack()
                .WithStackId(data.Keys.Contains("stackId") ? (string) data["stackId"] : null)
                .WithOwnerId(data.Keys.Contains("ownerId") ? (string) data["ownerId"] : null)
                .WithName(data.Keys.Contains("name") ? (string) data["name"] : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") ? (long?) data["createdAt"] : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") ? (long?) data["updatedAt"] : null);
        }
	}
}