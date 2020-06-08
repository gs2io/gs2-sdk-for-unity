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
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Model
{
	[Preserve]
	public class QuestGroupModel : IComparable
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

    public static string GetQuestGroupNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):quest:(?<namespaceName>.*):group:(?<questGroupName>.*)");
        if (!match.Groups["questGroupName"].Success)
        {
            return null;
        }
        return match.Groups["questGroupName"].Value;
    }

    public static string GetNamespaceNameFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):quest:(?<namespaceName>.*):group:(?<questGroupName>.*)");
        if (!match.Groups["namespaceName"].Success)
        {
            return null;
        }
        return match.Groups["namespaceName"].Value;
    }

    public static string GetOwnerIdFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):quest:(?<namespaceName>.*):group:(?<questGroupName>.*)");
        if (!match.Groups["ownerId"].Success)
        {
            return null;
        }
        return match.Groups["ownerId"].Value;
    }

    public static string GetRegionFromGrn(
        string grn
    )
    {
        var match = Regex.Match(grn, "grn:gs2:(?<region>.*):(?<ownerId>.*):quest:(?<namespaceName>.*):group:(?<questGroupName>.*)");
        if (!match.Groups["region"].Success)
        {
            return null;
        }
        return match.Groups["region"].Value;
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
                        return Gs2.Gs2Quest.Model.QuestModel.FromDict(value);
                    }
                ).ToList() : null)
                .WithChallengePeriodEventId(data.Keys.Contains("challengePeriodEventId") && data["challengePeriodEventId"] != null ? data["challengePeriodEventId"].ToString() : null);
        }

        public int CompareTo(object obj)
        {
            var other = obj as QuestGroupModel;
            var diff = 0;
            if (questGroupModelId == null && questGroupModelId == other.questGroupModelId)
            {
                // null and null
            }
            else
            {
                diff += questGroupModelId.CompareTo(other.questGroupModelId);
            }
            if (name == null && name == other.name)
            {
                // null and null
            }
            else
            {
                diff += name.CompareTo(other.name);
            }
            if (metadata == null && metadata == other.metadata)
            {
                // null and null
            }
            else
            {
                diff += metadata.CompareTo(other.metadata);
            }
            if (quests == null && quests == other.quests)
            {
                // null and null
            }
            else
            {
                diff += quests.Count - other.quests.Count;
                for (var i = 0; i < quests.Count; i++)
                {
                    diff += quests[i].CompareTo(other.quests[i]);
                }
            }
            if (challengePeriodEventId == null && challengePeriodEventId == other.challengePeriodEventId)
            {
                // null and null
            }
            else
            {
                diff += challengePeriodEventId.CompareTo(other.challengePeriodEventId);
            }
            return diff;
        }
	}
}