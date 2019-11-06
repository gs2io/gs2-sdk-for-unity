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
using Gs2.Gs2Money.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Money.Request
{
	[Preserve]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** ネームスペースの名前 */
        public string namespaceName { set; get; }

        /**
         * ネームスペースの名前を設定
         *
         * @param namespaceName ネームスペースの名前
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


        /** 消費優先度 */
        public string priority { set; get; }

        /**
         * 消費優先度を設定
         *
         * @param priority 消費優先度
         * @return this
         */
        public UpdateNamespaceRequest WithPriority(string priority) {
            this.priority = priority;
            return this;
        }


        /** Apple AppStore のバンドルID */
        public string appleKey { set; get; }

        /**
         * Apple AppStore のバンドルIDを設定
         *
         * @param appleKey Apple AppStore のバンドルID
         * @return this
         */
        public UpdateNamespaceRequest WithAppleKey(string appleKey) {
            this.appleKey = appleKey;
            return this;
        }


        /** Google PlayStore の秘密鍵 */
        public string googleKey { set; get; }

        /**
         * Google PlayStore の秘密鍵を設定
         *
         * @param googleKey Google PlayStore の秘密鍵
         * @return this
         */
        public UpdateNamespaceRequest WithGoogleKey(string googleKey) {
            this.googleKey = googleKey;
            return this;
        }


        /** UnityEditorが出力する偽のレシートで決済できるようにするか */
        public bool? enableFakeReceipt { set; get; }

        /**
         * UnityEditorが出力する偽のレシートで決済できるようにするかを設定
         *
         * @param enableFakeReceipt UnityEditorが出力する偽のレシートで決済できるようにするか
         * @return this
         */
        public UpdateNamespaceRequest WithEnableFakeReceipt(bool? enableFakeReceipt) {
            this.enableFakeReceipt = enableFakeReceipt;
            return this;
        }


        /** ウォレット新規作成したときに実行するスクリプト */
        public ScriptSetting createWalletScript { set; get; }

        /**
         * ウォレット新規作成したときに実行するスクリプトを設定
         *
         * @param createWalletScript ウォレット新規作成したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCreateWalletScript(ScriptSetting createWalletScript) {
            this.createWalletScript = createWalletScript;
            return this;
        }


        /** ウォレット残高加算したときに実行するスクリプト */
        public ScriptSetting depositScript { set; get; }

        /**
         * ウォレット残高加算したときに実行するスクリプトを設定
         *
         * @param depositScript ウォレット残高加算したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithDepositScript(ScriptSetting depositScript) {
            this.depositScript = depositScript;
            return this;
        }


        /** ウォレット残高消費したときに実行するスクリプト */
        public ScriptSetting withdrawScript { set; get; }

        /**
         * ウォレット残高消費したときに実行するスクリプトを設定
         *
         * @param withdrawScript ウォレット残高消費したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithWithdrawScript(ScriptSetting withdrawScript) {
            this.withdrawScript = withdrawScript;
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
                priority = data.Keys.Contains("priority") && data["priority"] != null ? data["priority"].ToString(): null,
                appleKey = data.Keys.Contains("appleKey") && data["appleKey"] != null ? data["appleKey"].ToString(): null,
                googleKey = data.Keys.Contains("googleKey") && data["googleKey"] != null ? data["googleKey"].ToString(): null,
                enableFakeReceipt = data.Keys.Contains("enableFakeReceipt") && data["enableFakeReceipt"] != null ? (bool?)bool.Parse(data["enableFakeReceipt"].ToString()) : null,
                createWalletScript = data.Keys.Contains("createWalletScript") && data["createWalletScript"] != null ? ScriptSetting.FromDict(data["createWalletScript"]) : null,
                depositScript = data.Keys.Contains("depositScript") && data["depositScript"] != null ? ScriptSetting.FromDict(data["depositScript"]) : null,
                withdrawScript = data.Keys.Contains("withdrawScript") && data["withdrawScript"] != null ? ScriptSetting.FromDict(data["withdrawScript"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}