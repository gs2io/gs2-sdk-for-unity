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
using Gs2.Gs2Account.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Account.Request
{
	[Preserve]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 説明文 */
        public string description { set; get; }

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** アカウント引き継ぎ時にパスワードを変更するか */
        public bool? changePasswordIfTakeOver { set; get; }

        /**
         * アカウント引き継ぎ時にパスワードを変更するかを設定
         *
         * @param changePasswordIfTakeOver アカウント引き継ぎ時にパスワードを変更するか
         * @return this
         */
        public UpdateNamespaceRequest WithChangePasswordIfTakeOver(bool? changePasswordIfTakeOver) {
            this.changePasswordIfTakeOver = changePasswordIfTakeOver;
            return this;
        }


        /** アカウント新規作成したときに実行するスクリプト */
        public ScriptSetting createAccountScript { set; get; }

        /**
         * アカウント新規作成したときに実行するスクリプトを設定
         *
         * @param createAccountScript アカウント新規作成したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCreateAccountScript(ScriptSetting createAccountScript) {
            this.createAccountScript = createAccountScript;
            return this;
        }


        /** 認証したときに実行するスクリプト */
        public ScriptSetting authenticationScript { set; get; }

        /**
         * 認証したときに実行するスクリプトを設定
         *
         * @param authenticationScript 認証したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithAuthenticationScript(ScriptSetting authenticationScript) {
            this.authenticationScript = authenticationScript;
            return this;
        }


        /** 引き継ぎ情報登録したときに実行するスクリプト */
        public ScriptSetting createTakeOverScript { set; get; }

        /**
         * 引き継ぎ情報登録したときに実行するスクリプトを設定
         *
         * @param createTakeOverScript 引き継ぎ情報登録したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCreateTakeOverScript(ScriptSetting createTakeOverScript) {
            this.createTakeOverScript = createTakeOverScript;
            return this;
        }


        /** 引き継ぎ実行したときに実行するスクリプト */
        public ScriptSetting doTakeOverScript { set; get; }

        /**
         * 引き継ぎ実行したときに実行するスクリプトを設定
         *
         * @param doTakeOverScript 引き継ぎ実行したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithDoTakeOverScript(ScriptSetting doTakeOverScript) {
            this.doTakeOverScript = doTakeOverScript;
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
                changePasswordIfTakeOver = data.Keys.Contains("changePasswordIfTakeOver") && data["changePasswordIfTakeOver"] != null ? (bool?)bool.Parse(data["changePasswordIfTakeOver"].ToString()) : null,
                createAccountScript = data.Keys.Contains("createAccountScript") && data["createAccountScript"] != null ? ScriptSetting.FromDict(data["createAccountScript"]) : null,
                authenticationScript = data.Keys.Contains("authenticationScript") && data["authenticationScript"] != null ? ScriptSetting.FromDict(data["authenticationScript"]) : null,
                createTakeOverScript = data.Keys.Contains("createTakeOverScript") && data["createTakeOverScript"] != null ? ScriptSetting.FromDict(data["createTakeOverScript"]) : null,
                doTakeOverScript = data.Keys.Contains("doTakeOverScript") && data["doTakeOverScript"] != null ? ScriptSetting.FromDict(data["doTakeOverScript"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}