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
	public class CompletedQuestList
	{

        /** クエスト進行 */
        public string completedQuestListId { set; get; }

        /**
         * クエスト進行を設定
         *
         * @param completedQuestListId クエスト進行
         * @return this
         */
        public CompletedQuestList WithCompletedQuestListId(string completedQuestListId) {
            this.completedQuestListId = completedQuestListId;
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
        public CompletedQuestList WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** クエストグループ名 */
        public string questGroupName { set; get; }

        /**
         * クエストグループ名を設定
         *
         * @param questGroupName クエストグループ名
         * @return this
         */
        public CompletedQuestList WithQuestGroupName(string questGroupName) {
            this.questGroupName = questGroupName;
            return this;
        }

        /** 攻略済みのクエスト名一覧のリスト */
        public List<string> completeQuestNames { set; get; }

        /**
         * 攻略済みのクエスト名一覧のリストを設定
         *
         * @param completeQuestNames 攻略済みのクエスト名一覧のリスト
         * @return this
         */
        public CompletedQuestList WithCompleteQuestNames(List<string> completeQuestNames) {
            this.completeQuestNames = completeQuestNames;
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
        public CompletedQuestList WithCreatedAt(long? createdAt) {
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
        public CompletedQuestList WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.completedQuestListId != null)
            {
                writer.WritePropertyName("completedQuestListId");
                writer.Write(this.completedQuestListId);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.questGroupName != null)
            {
                writer.WritePropertyName("questGroupName");
                writer.Write(this.questGroupName);
            }
            if(this.completeQuestNames != null)
            {
                writer.WritePropertyName("completeQuestNames");
                writer.WriteArrayStart();
                foreach(var item in this.completeQuestNames)
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
        public static CompletedQuestList FromDict(JsonData data)
        {
            return new CompletedQuestList()
                .WithCompletedQuestListId(data.Keys.Contains("completedQuestListId") && data["completedQuestListId"] != null ? data["completedQuestListId"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithQuestGroupName(data.Keys.Contains("questGroupName") && data["questGroupName"] != null ? data["questGroupName"].ToString() : null)
                .WithCompleteQuestNames(data.Keys.Contains("completeQuestNames") && data["completeQuestNames"] != null ? data["completeQuestNames"].Cast<JsonData>().Select(value =>
                    {
                        return value.ToString();
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}