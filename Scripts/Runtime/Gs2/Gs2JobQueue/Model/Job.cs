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
	public class Job
	{

        /** ジョブ */
        public string jobId { set; get; }

        /**
         * ジョブを設定
         *
         * @param jobId ジョブ
         * @return this
         */
        public Job WithJobId(string jobId) {
            this.jobId = jobId;
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
        public Job WithName(string name) {
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
        public Job WithUserId(string userId) {
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
        public Job WithScriptId(string scriptId) {
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
        public Job WithArgs(string args) {
            this.args = args;
            return this;
        }

        /** 現在のリトライ回数 */
        public int? currentRetryCount { set; get; }

        /**
         * 現在のリトライ回数を設定
         *
         * @param currentRetryCount 現在のリトライ回数
         * @return this
         */
        public Job WithCurrentRetryCount(int? currentRetryCount) {
            this.currentRetryCount = currentRetryCount;
            return this;
        }

        /** 最大試行回数 */
        public int? maxTryCount { set; get; }

        /**
         * 最大試行回数を設定
         *
         * @param maxTryCount 最大試行回数
         * @return this
         */
        public Job WithMaxTryCount(int? maxTryCount) {
            this.maxTryCount = maxTryCount;
            return this;
        }

        /** ソート用インデックス(現在時刻(ミリ秒).登録時のインデックス) */
        public double? index { set; get; }

        /**
         * ソート用インデックス(現在時刻(ミリ秒).登録時のインデックス)を設定
         *
         * @param index ソート用インデックス(現在時刻(ミリ秒).登録時のインデックス)
         * @return this
         */
        public Job WithIndex(double? index) {
            this.index = index;
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
        public Job WithCreatedAt(long? createdAt) {
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
        public Job WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.jobId != null)
            {
                writer.WritePropertyName("jobId");
                writer.Write(this.jobId);
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
            if(this.currentRetryCount.HasValue)
            {
                writer.WritePropertyName("currentRetryCount");
                writer.Write(this.currentRetryCount.Value);
            }
            if(this.maxTryCount.HasValue)
            {
                writer.WritePropertyName("maxTryCount");
                writer.Write(this.maxTryCount.Value);
            }
            if(this.index.HasValue)
            {
                writer.WritePropertyName("index");
                writer.Write(this.index.Value);
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
        public static Job FromDict(JsonData data)
        {
            return new Job()
                .WithJobId(data.Keys.Contains("jobId") && data["jobId"] != null ? data["jobId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithScriptId(data.Keys.Contains("scriptId") && data["scriptId"] != null ? data["scriptId"].ToString() : null)
                .WithArgs(data.Keys.Contains("args") && data["args"] != null ? data["args"].ToString() : null)
                .WithCurrentRetryCount(data.Keys.Contains("currentRetryCount") && data["currentRetryCount"] != null ? (int?)int.Parse(data["currentRetryCount"].ToString()) : null)
                .WithMaxTryCount(data.Keys.Contains("maxTryCount") && data["maxTryCount"] != null ? (int?)int.Parse(data["maxTryCount"].ToString()) : null)
                .WithIndex(data.Keys.Contains("index") && data["index"] != null ? (double?)double.Parse(data["index"].ToString()) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}