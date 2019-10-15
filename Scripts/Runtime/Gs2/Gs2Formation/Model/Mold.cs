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
	public class Mold
	{

        /** 保存したフォーム */
        public string moldId { set; get; }

        /**
         * 保存したフォームを設定
         *
         * @param moldId 保存したフォーム
         * @return this
         */
        public Mold WithMoldId(string moldId) {
            this.moldId = moldId;
            return this;
        }

        /** フォームの保存領域の名前 */
        public string name { set; get; }

        /**
         * フォームの保存領域の名前を設定
         *
         * @param name フォームの保存領域の名前
         * @return this
         */
        public Mold WithName(string name) {
            this.name = name;
            return this;
        }

        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public Mold WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 現在のキャパシティ */
        public int? capacity { set; get; }

        /**
         * 現在のキャパシティを設定
         *
         * @param capacity 現在のキャパシティ
         * @return this
         */
        public Mold WithCapacity(int? capacity) {
            this.capacity = capacity;
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
        public Mold WithCreatedAt(long? createdAt) {
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
        public Mold WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.moldId != null)
            {
                writer.WritePropertyName("moldId");
                writer.Write(this.moldId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.capacity.HasValue)
            {
                writer.WritePropertyName("capacity");
                writer.Write(this.capacity.Value);
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
        public static Mold FromDict(JsonData data)
        {
            return new Mold()
                .WithMoldId(data.Keys.Contains("moldId") && data["moldId"] != null ? data["moldId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithCapacity(data.Keys.Contains("capacity") && data["capacity"] != null ? (int?)int.Parse(data["capacity"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}