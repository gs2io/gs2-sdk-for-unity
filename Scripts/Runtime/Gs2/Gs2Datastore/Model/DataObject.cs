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

namespace Gs2.Gs2Datastore.Model
{
	[Preserve]
	public class DataObject
	{

        /** データオブジェクト */
        public string dataObjectId { set; get; }

        /**
         * データオブジェクトを設定
         *
         * @param dataObjectId データオブジェクト
         * @return this
         */
        public DataObject WithDataObjectId(string dataObjectId) {
            this.dataObjectId = dataObjectId;
            return this;
        }

        /** データの名前 */
        public string name { set; get; }

        /**
         * データの名前を設定
         *
         * @param name データの名前
         * @return this
         */
        public DataObject WithName(string name) {
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
        public DataObject WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** ファイルのアクセス権 */
        public string scope { set; get; }

        /**
         * ファイルのアクセス権を設定
         *
         * @param scope ファイルのアクセス権
         * @return this
         */
        public DataObject WithScope(string scope) {
            this.scope = scope;
            return this;
        }

        /** 公開するユーザIDリスト */
        public List<string> allowUserIds { set; get; }

        /**
         * 公開するユーザIDリストを設定
         *
         * @param allowUserIds 公開するユーザIDリスト
         * @return this
         */
        public DataObject WithAllowUserIds(List<string> allowUserIds) {
            this.allowUserIds = allowUserIds;
            return this;
        }

        /** プラットフォーム */
        public string platform { set; get; }

        /**
         * プラットフォームを設定
         *
         * @param platform プラットフォーム
         * @return this
         */
        public DataObject WithPlatform(string platform) {
            this.platform = platform;
            return this;
        }

        /** 状態 */
        public string status { set; get; }

        /**
         * 状態を設定
         *
         * @param status 状態
         * @return this
         */
        public DataObject WithStatus(string status) {
            this.status = status;
            return this;
        }

        /** データの世代 */
        public string generation { set; get; }

        /**
         * データの世代を設定
         *
         * @param generation データの世代
         * @return this
         */
        public DataObject WithGeneration(string generation) {
            this.generation = generation;
            return this;
        }

        /** 以前有効だったデータの世代 */
        public string previousGeneration { set; get; }

        /**
         * 以前有効だったデータの世代を設定
         *
         * @param previousGeneration 以前有効だったデータの世代
         * @return this
         */
        public DataObject WithPreviousGeneration(string previousGeneration) {
            this.previousGeneration = previousGeneration;
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
        public DataObject WithCreatedAt(long? createdAt) {
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
        public DataObject WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.dataObjectId != null)
            {
                writer.WritePropertyName("dataObjectId");
                writer.Write(this.dataObjectId);
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
            if(this.scope != null)
            {
                writer.WritePropertyName("scope");
                writer.Write(this.scope);
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
            if(this.platform != null)
            {
                writer.WritePropertyName("platform");
                writer.Write(this.platform);
            }
            if(this.status != null)
            {
                writer.WritePropertyName("status");
                writer.Write(this.status);
            }
            if(this.generation != null)
            {
                writer.WritePropertyName("generation");
                writer.Write(this.generation);
            }
            if(this.previousGeneration != null)
            {
                writer.WritePropertyName("previousGeneration");
                writer.Write(this.previousGeneration);
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
        public static DataObject FromDict(JsonData data)
        {
            return new DataObject()
                .WithDataObjectId(data.Keys.Contains("dataObjectId") && data["dataObjectId"] != null ? data["dataObjectId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithScope(data.Keys.Contains("scope") && data["scope"] != null ? data["scope"].ToString() : null)
                .WithAllowUserIds(data.Keys.Contains("allowUserIds") && data["allowUserIds"] != null ? data["allowUserIds"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithPlatform(data.Keys.Contains("platform") && data["platform"] != null ? data["platform"].ToString() : null)
                .WithStatus(data.Keys.Contains("status") && data["status"] != null ? data["status"].ToString() : null)
                .WithGeneration(data.Keys.Contains("generation") && data["generation"] != null ? data["generation"].ToString() : null)
                .WithPreviousGeneration(data.Keys.Contains("previousGeneration") && data["previousGeneration"] != null ? data["previousGeneration"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}