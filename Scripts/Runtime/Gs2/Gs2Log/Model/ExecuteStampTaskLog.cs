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
	public class ExecuteStampTaskLog
	{

        /** 日時 */
        public long? timestamp { set; get; }

        /**
         * 日時を設定
         *
         * @param timestamp 日時
         * @return this
         */
        public ExecuteStampTaskLog WithTimestamp(long? timestamp) {
            this.timestamp = timestamp;
            return this;
        }

        /** タスクID */
        public string taskId { set; get; }

        /**
         * タスクIDを設定
         *
         * @param taskId タスクID
         * @return this
         */
        public ExecuteStampTaskLog WithTaskId(string taskId) {
            this.taskId = taskId;
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
        public ExecuteStampTaskLog WithService(string service) {
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
        public ExecuteStampTaskLog WithMethod(string method) {
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
        public ExecuteStampTaskLog WithUserId(string userId) {
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
        public ExecuteStampTaskLog WithAction(string action) {
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
        public ExecuteStampTaskLog WithArgs(string args) {
            this.args = args;
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
            if(this.taskId != null)
            {
                writer.WritePropertyName("taskId");
                writer.Write(this.taskId);
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
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static ExecuteStampTaskLog FromDict(JsonData data)
        {
            return new ExecuteStampTaskLog()
                .WithTimestamp(data.Keys.Contains("timestamp") && data["timestamp"] != null ? (long?)long.Parse(data["timestamp"].ToString()) : null)
                .WithTaskId(data.Keys.Contains("taskId") && data["taskId"] != null ? data["taskId"].ToString() : null)
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithAction(data.Keys.Contains("action") && data["action"] != null ? data["action"].ToString() : null)
                .WithArgs(data.Keys.Contains("args") && data["args"] != null ? data["args"].ToString() : null);
        }
	}
}