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
using Gs2.Gs2Quest.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Quest.Request
{
	[Preserve]
	public class StartByUserIdRequest : Gs2Request<StartByUserIdRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
         * @return this
         */
        public StartByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
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
        public StartByUserIdRequest WithQuestGroupName(string questGroupName) {
            this.questGroupName = questGroupName;
            return this;
        }


        /** クエストモデル名 */
        public string questName { set; get; }

        /**
         * クエストモデル名を設定
         *
         * @param questName クエストモデル名
         * @return this
         */
        public StartByUserIdRequest WithQuestName(string questName) {
            this.questName = questName;
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
        public StartByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** すでに開始しているクエストがある場合にそれを破棄して開始するか */
        public bool? force { set; get; }

        /**
         * すでに開始しているクエストがある場合にそれを破棄して開始するかを設定
         *
         * @param force すでに開始しているクエストがある場合にそれを破棄して開始するか
         * @return this
         */
        public StartByUserIdRequest WithForce(bool? force) {
            this.force = force;
            return this;
        }


        /** スタンプシートの変数に適用する設定値 */
        public List<Config> config { set; get; }

        /**
         * スタンプシートの変数に適用する設定値を設定
         *
         * @param config スタンプシートの変数に適用する設定値
         * @return this
         */
        public StartByUserIdRequest WithConfig(List<Config> config) {
            this.config = config;
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
        public StartByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static StartByUserIdRequest FromDict(JsonData data)
        {
            return new StartByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                questGroupName = data.Keys.Contains("questGroupName") && data["questGroupName"] != null ? data["questGroupName"].ToString(): null,
                questName = data.Keys.Contains("questName") && data["questName"] != null ? data["questName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
                force = data.Keys.Contains("force") && data["force"] != null ? (bool?)bool.Parse(data["force"].ToString()) : null,
                config = data.Keys.Contains("config") && data["config"] != null ? data["config"].Cast<JsonData>().Select(value =>
                    {
                        return Config.FromDict(value);
                    }
                ).ToList() : null,
                duplicationAvoider = data.Keys.Contains("duplicationAvoider") && data["duplicationAvoider"] != null ? data["duplicationAvoider"].ToString(): null,
            };
        }

	}
}