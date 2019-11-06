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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Log.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Log.Request
{
	[Preserve]
	public class CountIssueStampSheetLogRequest : Gs2Request<CountIssueStampSheetLogRequest>
	{

        /** カテゴリー名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public CountIssueStampSheetLogRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** マイクロサービスの種類を集計軸に使用するか */
        public bool? service { set; get; }

        /**
         * マイクロサービスの種類を集計軸に使用するかを設定
         *
         * @param service マイクロサービスの種類を集計軸に使用するか
         * @return this
         */
        public CountIssueStampSheetLogRequest WithService(bool? service) {
            this.service = service;
            return this;
        }


        /** マイクロサービスのメソッドを集計軸に使用するか */
        public bool? method { set; get; }

        /**
         * マイクロサービスのメソッドを集計軸に使用するかを設定
         *
         * @param method マイクロサービスのメソッドを集計軸に使用するか
         * @return this
         */
        public CountIssueStampSheetLogRequest WithMethod(bool? method) {
            this.method = method;
            return this;
        }


        /** ユーザIDを集計軸に使用するか */
        public bool? userId { set; get; }

        /**
         * ユーザIDを集計軸に使用するかを設定
         *
         * @param userId ユーザIDを集計軸に使用するか
         * @return this
         */
        public CountIssueStampSheetLogRequest WithUserId(bool? userId) {
            this.userId = userId;
            return this;
        }


        /** 報酬アクションの種類を集計軸に使用するか */
        public bool? action { set; get; }

        /**
         * 報酬アクションの種類を集計軸に使用するかを設定
         *
         * @param action 報酬アクションの種類を集計軸に使用するか
         * @return this
         */
        public CountIssueStampSheetLogRequest WithAction(bool? action) {
            this.action = action;
            return this;
        }


        /** データの取得を開始する位置を指定するトークン */
        public string pageToken { set; get; }

        /**
         * データの取得を開始する位置を指定するトークンを設定
         *
         * @param pageToken データの取得を開始する位置を指定するトークン
         * @return this
         */
        public CountIssueStampSheetLogRequest WithPageToken(string pageToken) {
            this.pageToken = pageToken;
            return this;
        }


        /** データの取得件数 */
        public long? limit { set; get; }

        /**
         * データの取得件数を設定
         *
         * @param limit データの取得件数
         * @return this
         */
        public CountIssueStampSheetLogRequest WithLimit(long? limit) {
            this.limit = limit;
            return this;
        }


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public CountIssueStampSheetLogRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static CountIssueStampSheetLogRequest FromDict(JsonData data)
        {
            return new CountIssueStampSheetLogRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                service = data.Keys.Contains("service") && data["service"] != null ? (bool?)bool.Parse(data["service"].ToString()) : null,
                method = data.Keys.Contains("method") && data["method"] != null ? (bool?)bool.Parse(data["method"].ToString()) : null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? (bool?)bool.Parse(data["userId"].ToString()) : null,
                action = data.Keys.Contains("action") && data["action"] != null ? (bool?)bool.Parse(data["action"].ToString()) : null,
                pageToken = data.Keys.Contains("pageToken") && data["pageToken"] != null ? data["pageToken"].ToString(): null,
                limit = data.Keys.Contains("limit") && data["limit"] != null ? (long?)long.Parse(data["limit"].ToString()) : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}