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
	public class IssueStampSheetLogCount
	{

        /** マイクロサービスの種類 */
        public string service { set; get; }

        /**
         * マイクロサービスの種類を設定
         *
         * @param service マイクロサービスの種類
         * @return this
         */
        public IssueStampSheetLogCount WithService(string service) {
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
        public IssueStampSheetLogCount WithMethod(string method) {
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
        public IssueStampSheetLogCount WithUserId(string userId) {
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
        public IssueStampSheetLogCount WithAction(string action) {
            this.action = action;
            return this;
        }

        /** 回数 */
        public long? count { set; get; }

        /**
         * 回数を設定
         *
         * @param count 回数
         * @return this
         */
        public IssueStampSheetLogCount WithCount(long? count) {
            this.count = count;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
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
            if(this.count.HasValue)
            {
                writer.WritePropertyName("count");
                writer.Write(this.count.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static IssueStampSheetLogCount FromDict(JsonData data)
        {
            return new IssueStampSheetLogCount()
                .WithService(data.Keys.Contains("service") && data["service"] != null ? data["service"].ToString() : null)
                .WithMethod(data.Keys.Contains("method") && data["method"] != null ? data["method"].ToString() : null)
                .WithUserId(data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString() : null)
                .WithAction(data.Keys.Contains("action") && data["action"] != null ? data["action"].ToString() : null)
                .WithCount(data.Keys.Contains("count") && data["count"] != null ? (long?)long.Parse(data["count"].ToString()) : null);
        }
	}
}