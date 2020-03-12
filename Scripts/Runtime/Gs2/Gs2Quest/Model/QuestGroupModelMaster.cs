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

namespace Gs2.Gs2Quest.Model
{
	[Preserve]
	public class QuestGroupModelMaster
	{

        /** クエストグループマスター */
        public string questGroupModelId { set; get; }

        /**
         * クエストグループマスターを設定
         *
         * @param questGroupModelId クエストグループマスター
         * @return this
         */
        public QuestGroupModelMaster WithQuestGroupModelId(string questGroupModelId) {
            this.questGroupModelId = questGroupModelId;
            return this;
        }

        /** クエストグループモデル名 */
        public string name { set; get; }

        /**
         * クエストグループモデル名を設定
         *
         * @param name クエストグループモデル名
         * @return this
         */
        public QuestGroupModelMaster WithName(string name) {
            this.name = name;
            return this;
        }

        /** クエストグループマスターの説明 */
        public string description { set; get; }

        /**
         * クエストグループマスターの説明を設定
         *
         * @param description クエストグループマスターの説明
         * @return this
         */
        public QuestGroupModelMaster WithDescription(string description) {
            this.description = description;
            return this;
        }

        /** クエストグループのメタデータ */
        public string metadata { set; get; }

        /**
         * クエストグループのメタデータを設定
         *
         * @param metadata クエストグループのメタデータ
         * @return this
         */
        public QuestGroupModelMaster WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** 挑戦可能な期間を指定するイベントマスター のGRN */
        public string challengePeriodEventId { set; get; }

        /**
         * 挑戦可能な期間を指定するイベントマスター のGRNを設定
         *
         * @param challengePeriodEventId 挑戦可能な期間を指定するイベントマスター のGRN
         * @return this
         */
        public QuestGroupModelMaster WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
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
        public QuestGroupModelMaster WithCreatedAt(long? createdAt) {
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
        public QuestGroupModelMaster WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.questGroupModelId != null)
            {
                writer.WritePropertyName("questGroupModelId");
                writer.Write(this.questGroupModelId);
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
            if(this.challengePeriodEventId != null)
            {
                writer.WritePropertyName("challengePeriodEventId");
                writer.Write(this.challengePeriodEventId);
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
        public static QuestGroupModelMaster FromDict(JsonData data)
        {
            return new QuestGroupModelMaster()
                .WithQuestGroupModelId(data.Keys.Contains("questGroupModelId") && data["questGroupModelId"] != null ? data["questGroupModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}