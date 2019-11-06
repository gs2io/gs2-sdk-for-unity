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
	public class DataObjectHistory
	{

        /** データオブジェクト履歴 */
        public string dataObjectHistoryId { set; get; }

        /**
         * データオブジェクト履歴を設定
         *
         * @param dataObjectHistoryId データオブジェクト履歴
         * @return this
         */
        public DataObjectHistory WithDataObjectHistoryId(string dataObjectHistoryId) {
            this.dataObjectHistoryId = dataObjectHistoryId;
            return this;
        }

        /** データオブジェクト名 */
        public string dataObjectName { set; get; }

        /**
         * データオブジェクト名を設定
         *
         * @param dataObjectName データオブジェクト名
         * @return this
         */
        public DataObjectHistory WithDataObjectName(string dataObjectName) {
            this.dataObjectName = dataObjectName;
            return this;
        }

        /** 世代ID */
        public string generation { set; get; }

        /**
         * 世代IDを設定
         *
         * @param generation 世代ID
         * @return this
         */
        public DataObjectHistory WithGeneration(string generation) {
            this.generation = generation;
            return this;
        }

        /** データサイズ */
        public long? contentLength { set; get; }

        /**
         * データサイズを設定
         *
         * @param contentLength データサイズ
         * @return this
         */
        public DataObjectHistory WithContentLength(long? contentLength) {
            this.contentLength = contentLength;
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
        public DataObjectHistory WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.dataObjectHistoryId != null)
            {
                writer.WritePropertyName("dataObjectHistoryId");
                writer.Write(this.dataObjectHistoryId);
            }
            if(this.dataObjectName != null)
            {
                writer.WritePropertyName("dataObjectName");
                writer.Write(this.dataObjectName);
            }
            if(this.generation != null)
            {
                writer.WritePropertyName("generation");
                writer.Write(this.generation);
            }
            if(this.contentLength.HasValue)
            {
                writer.WritePropertyName("contentLength");
                writer.Write(this.contentLength.Value);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static DataObjectHistory FromDict(JsonData data)
        {
            return new DataObjectHistory()
                .WithDataObjectHistoryId(data.Keys.Contains("dataObjectHistoryId") && data["dataObjectHistoryId"] != null ? data["dataObjectHistoryId"].ToString() : null)
                .WithDataObjectName(data.Keys.Contains("dataObjectName") && data["dataObjectName"] != null ? data["dataObjectName"].ToString() : null)
                .WithGeneration(data.Keys.Contains("generation") && data["generation"] != null ? data["generation"].ToString() : null)
                .WithContentLength(data.Keys.Contains("contentLength") && data["contentLength"] != null ? (long?)long.Parse(data["contentLength"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null);
        }
	}
}