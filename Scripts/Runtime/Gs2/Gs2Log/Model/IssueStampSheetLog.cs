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

namespace Gs2.Gs2Log.Model
{
	[Preserve]
	public class IssueStampSheetLog
	{

        /** 日時 */
        public long? timestamp { set; get; }

        /**
         * 日時を設定
         *
         * @param timestamp 日時
         * @return this
         */
        public IssueStampSheetLog WithTimestamp(long? timestamp) {
            this.timestamp = timestamp;
            return this;
        }

        /** トランザクションID */
        public string transactionId { set; get; }

        /**
         * トランザクションIDを設定
         *
         * @param transactionId トランザクションID
         * @return this
         */
        public IssueStampSheetLog WithTransactionId(string transactionId) {
            this.transactionId = transactionId;
            return this;
        }

        /** マイクロサービスの種類 */
        public string service { set; get; }

        /**
         * マイクロサービスの種類を設定
         *
         * @param service マイクロサービスの種類
         * @return this
         */
        public IssueStampSheetLog WithService(string service) {
            this.service = service;
            return this;
        }

        /** マイクロサービスのメソッド */
        public string method { set; get; }

        /**
         * マイクロサービスのメソッドを設定
         *
         * @param method マイクロサービスのメソッド
         * @return this
         */
        public IssueStampSheetLog WithMethod(string method) {
            this.method = method;
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
        public IssueStampSheetLog WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** 報酬アクション */
        public string action { set; get; }

        /**
         * 報酬アクションを設定
         *
         * @param action 報酬アクション
         * @return this
         */
        public IssueStampSheetLog WithAction(string action) {
            this.action = action;
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
        public IssueStampSheetLog WithArgs(string args) {
            this.args = args;
            return this;
        }

        /** スタンプタスク */
        public string tasks { set; get; }

        /**
         * スタンプタスクを設定
         *
         * @param tasks スタンプタスク
         * @return this
         */
        public IssueStampSheetLog WithTasks(string tasks) {
            this.tasks = tasks;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.timestamp.HasValue)
            {
                writer.WritePropertyName("timestamp");
                writer.Write(this.timestamp.Value);
            }
            if(this.transactionId != null)
            {
                writer.WritePropertyName("transactionId");
                writer.Write(this.transactionId);
            }
            if(this.service != null)
            {
                writer.WritePropertyName("service");
                writer.Write(this.service);
            }
            if(this.method != null)
            {
                writer.WritePropertyName("method");
                writer.Write(this.method);
            }
            if(this.userId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.userId);
            }
            if(this.action != null)
            {
                writer.WritePropertyName("action");
                writer.Write(this.action);
            }
            if(this.args != null)
            {
                writer.WritePropertyName("args");
                writer.Write(this.args);
            }
            if(this.tasks != null)
            {
                writer.WritePropertyName("tasks");
                writer.Write(this.tasks);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static IssueStampSheetLog FromDict(JsonData data)
        {
            return new IssueStampSheetLog()
                .WithTimestamp(data.Keys.Contains("timestamp") && data["timestamp"] != null ? (long?)long.Parse(data["timestamp"].ToString()) : null)
                .WithTransactionId(data.Keys.Contains("transactionId") && data["transactionId"] != null ? data["transactionId"].ToString() : null)
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithAction(data.Keys.Contains("action") && data["action"] != null ? data["action"].ToString() : null)
                .WithArgs(data.Keys.Contains("args") && data["args"] != null ? data["args"].ToString() : null)
                .WithTasks(data.Keys.Contains("tasks") && data["tasks"] != null ? data["tasks"].ToString() : null);
        }
	}
}