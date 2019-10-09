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

namespace Gs2.Gs2JobQueue.Model
{
	[Preserve]
	public class DeadLetterJob
	{

        /** デッドレタージョブ */
        public string deadLetterJobId { set; get; }

        /**
         * デッドレタージョブを設定
         *
         * @param deadLetterJobId デッドレタージョブ
         * @return this
         */
        public DeadLetterJob WithDeadLetterJobId(string deadLetterJobId) {
            this.deadLetterJobId = deadLetterJobId;
            return this;
        }

        /** ジョブの名前 */
        public string name { set; get; }

        /**
         * ジョブの名前を設定
         *
         * @param name ジョブの名前
         * @return this
         */
        public DeadLetterJob WithName(string name) {
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
        public DeadLetterJob WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** ジョブの実行に使用するスクリプト のGRN */
        public string scriptId { set; get; }

        /**
         * ジョブの実行に使用するスクリプト のGRNを設定
         *
         * @param scriptId ジョブの実行に使用するスクリプト のGRN
         * @return this
         */
        public DeadLetterJob WithScriptId(string scriptId) {
            this.scriptId = scriptId;
            return this;
        }

        /** 引数 */
        public string args { set; get; }

        /**
         * 引数を設定
         *
         * @param args 引数
         * @return this
         */
        public DeadLetterJob WithArgs(string args) {
            this.args = args;
            return this;
        }

        /** ジョブ実行結果 */
        public List<JobResultBody> result { set; get; }

        /**
         * ジョブ実行結果を設定
         *
         * @param result ジョブ実行結果
         * @return this
         */
        public DeadLetterJob WithResult(List<JobResultBody> result) {
            this.result = result;
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
        public DeadLetterJob WithCreatedAt(long? createdAt) {
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
        public DeadLetterJob WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.deadLetterJobId != null)
            {
                writer.WritePropertyName("deadLetterJobId");
                writer.Write(this.deadLetterJobId);
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
            if(this.scriptId != null)
            {
                writer.WritePropertyName("scriptId");
                writer.Write(this.scriptId);
            }
            if(this.args != null)
            {
                writer.WritePropertyName("args");
                writer.Write(this.args);
            }
            if(this.result != null)
            {
                writer.WritePropertyName("result");
                writer.WriteArrayStart();
                foreach(var item in this.result)
                {
                    item.WriteJson(writer);
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
        public static DeadLetterJob FromDict(JsonData data)
        {
            return new DeadLetterJob()
                .WithDeadLetterJobId(data.Keys.Contains("deadLetterJobId") && data["deadLetterJobId"] != null ? data["deadLetterJobId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithScriptId(data.Keys.Contains("scriptId") && data["scriptId"] != null ? data["scriptId"].ToString() : null)
                .WithArgs(data.Keys.Contains("args") && data["args"] != null ? data["args"].ToString() : null)
                .WithResult(data.Keys.Contains("result") && data["result"] != null ? data["result"].Cast<JsonData>().Select(value =>
                    {
                        return JobResultBody.FromDict(value);
                    }
                ).ToList() : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}