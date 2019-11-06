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
using Gs2.Gs2Inventory.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inventory.Request
{
	[Preserve]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** カテゴリー名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリー名を設定
         *
         * @param namespaceName カテゴリー名
         * @return this
         */
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** アイテム入手したときに実行するスクリプト */
        public ScriptSetting acquireScript { set; get; }

        /**
         * アイテム入手したときに実行するスクリプトを設定
         *
         * @param acquireScript アイテム入手したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithAcquireScript(ScriptSetting acquireScript) {
            this.acquireScript = acquireScript;
            return this;
        }


        /** 入手上限に当たって入手できなかったときに実行するスクリプト */
        public ScriptSetting overflowScript { set; get; }

        /**
         * 入手上限に当たって入手できなかったときに実行するスクリプトを設定
         *
         * @param overflowScript 入手上限に当たって入手できなかったときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithOverflowScript(ScriptSetting overflowScript) {
            this.overflowScript = overflowScript;
            return this;
        }


        /** アイテム消費するときに実行するスクリプト */
        public ScriptSetting consumeScript { set; get; }

        /**
         * アイテム消費するときに実行するスクリプトを設定
         *
         * @param consumeScript アイテム消費するときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithConsumeScript(ScriptSetting consumeScript) {
            this.consumeScript = consumeScript;
            return this;
        }


        /** ログの出力設定 */
        public LogSetting logSetting { set; get; }

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                acquireScript = data.Keys.Contains("acquireScript") && data["acquireScript"] != null ? ScriptSetting.FromDict(data["acquireScript"]) : null,
                overflowScript = data.Keys.Contains("overflowScript") && data["overflowScript"] != null ? ScriptSetting.FromDict(data["overflowScript"]) : null,
                consumeScript = data.Keys.Contains("consumeScript") && data["consumeScript"] != null ? ScriptSetting.FromDict(data["consumeScript"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}