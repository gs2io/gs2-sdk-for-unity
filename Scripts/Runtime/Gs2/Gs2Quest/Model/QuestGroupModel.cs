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
	public class QuestGroupModel
	{

        /** クエストグループ */
        public string questGroupModelId { set; get; }

        /**
         * クエストグループを設定
         *
         * @param questGroupModelId クエストグループ
         * @return this
         */
        public QuestGroupModel WithQuestGroupModelId(string questGroupModelId) {
            this.questGroupModelId = questGroupModelId;
            return this;
        }

        /** クエストグループ名 */
        public string name { set; get; }

        /**
         * クエストグループ名を設定
         *
         * @param name クエストグループ名
         * @return this
         */
        public QuestGroupModel WithName(string name) {
            this.name = name;
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
        public QuestGroupModel WithMetadata(string metadata) {
            this.metadata = metadata;
            return this;
        }

        /** グループに属するクエスト */
        public List<QuestModel> quests { set; get; }

        /**
         * グループに属するクエストを設定
         *
         * @param quests グループに属するクエスト
         * @return this
         */
        public QuestGroupModel WithQuests(List<QuestModel> quests) {
            this.quests = quests;
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
        public QuestGroupModel WithChallengePeriodEventId(string challengePeriodEventId) {
            this.challengePeriodEventId = challengePeriodEventId;
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
            if(this.metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.metadata);
            }
            if(this.quests != null)
            {
                writer.WritePropertyName("quests");
                writer.WriteArrayStart();
                foreach(var item in this.quests)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            if(this.challengePeriodEventId != null)
            {
                writer.WritePropertyName("challengePeriodEventId");
                writer.Write(this.challengePeriodEventId);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static QuestGroupModel FromDict(JsonData data)
        {
            return new QuestGroupModel()
                .WithQuestGroupModelId(data.Keys.Contains("questGroupModelId") && data["questGroupModelId"] != null ? data["questGroupModelId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithMetadata(data.Keys.Contains("metadata") && data["metadata"] != null ? data["metadata"].ToString() : null)
                .WithQuests(data.Keys.Contains("quests") && data["quests"] != null ? data["quests"].Cast<JsonData>().Select(value =>
                    {
                        return QuestModel.FromDict(value);
                    }
                ).ToList() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null);
        }
	}
}