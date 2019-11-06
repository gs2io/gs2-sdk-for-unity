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
	public class Log
	{

        /** None */
        public long? timestamp { set; get; }

        /**
         * Noneを設定
         *
         * @param timestamp None
         * @return this
         */
        public Log WithTimestamp(long? timestamp) {
            this.timestamp = timestamp;
            return this;
        }

        /** リクエストID */
        public string requestId { set; get; }

        /**
         * リクエストIDを設定
         *
         * @param requestId リクエストID
         * @return this
         */
        public Log WithRequestId(string requestId) {
            this.requestId = requestId;
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
        public Log WithService(string service) {
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
        public Log WithMethod(string method) {
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
        public Log WithUserId(string userId) {
            this.userId = userId;
            return this;
        }

        /** リクエストパラメータ */
        public string request { set; get; }

        /**
         * リクエストパラメータを設定
         *
         * @param request リクエストパラメータ
         * @return this
         */
        public Log WithRequest(string request) {
            this.request = request;
            return this;
        }

        /** 応答内容 */
        public string result { set; get; }

        /**
         * 応答内容を設定
         *
         * @param result 応答内容
         * @return this
         */
        public Log WithResult(string result) {
            this.result = result;
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
            if(this.requestId != null)
            {
                writer.WritePropertyName("requestId");
                writer.Write(this.requestId);
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
            if(this.request != null)
            {
                writer.WritePropertyName("request");
                writer.Write(this.request);
            }
            if(this.result != null)
            {
                writer.WritePropertyName("result");
                writer.Write(this.result);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Log FromDict(JsonData data)
        {
            return new Log()
                .WithTimestamp(data.Keys.Contains("timestamp") && data["timestamp"] != null ? (long?)long.Parse(data["timestamp"].ToString()) : null)
                .WithRequestId(data.Keys.Contains("requestId") && data["requestId"] != null ? data["requestId"].ToString() : null)
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithRequest(data.Keys.Contains("request") && data["request"] != null ? data["request"].ToString() : null)
                .WithResult(data.Keys.Contains("result") && data["result"] != null ? data["result"].ToString() : null);
        }
	}
}