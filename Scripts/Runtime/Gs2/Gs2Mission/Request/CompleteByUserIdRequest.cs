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
using Gs2.Gs2Mission.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Mission.Request
{
	[Preserve]
	public class CompleteByUserIdRequest : Gs2Request<CompleteByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CompleteByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ミッショングループ名 */
        public string missionGroupName { set; get; }

        /**
         * ミッショングループ名を設定
         *
         * @param missionGroupName ミッショングループ名
         * @return this
         */
        public CompleteByUserIdRequest WithMissionGroupName(string missionGroupName) {
            this.missionGroupName = missionGroupName;
            return this;
        }


        /** タスク名 */
        public string missionTaskName { set; get; }

        /**
         * タスク名を設定
         *
         * @param missionTaskName タスク名
         * @return this
         */
        public CompleteByUserIdRequest WithMissionTaskName(string missionTaskName) {
            this.missionTaskName = missionTaskName;
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
        public CompleteByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
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
        public CompleteByUserIdRequest WithConfig(List<Config> config) {
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
        public CompleteByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


    	[Preserve]
        public static CompleteByUserIdRequest FromDict(JsonData data)
        {
            return new CompleteByUserIdRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                missionGroupName = data.Keys.Contains("missionGroupName") && data["missionGroupName"] != null ? data["missionGroupName"].ToString(): null,
                missionTaskName = data.Keys.Contains("missionTaskName") && data["missionTaskName"] != null ? data["missionTaskName"].ToString(): null,
                userId = data.Keys.Contains("userId") && data["userId"] != null ? data["userId"].ToString(): null,
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