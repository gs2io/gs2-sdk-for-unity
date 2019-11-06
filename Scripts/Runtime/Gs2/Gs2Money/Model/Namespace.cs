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

namespace Gs2.Gs2Money.Model
{
	[Preserve]
	public class Namespace
	{

        /** ネームスペース */
        public string namespaceId { set; get; }

        /**
         * ネームスペースを設定
         *
         * @param namespaceId ネームスペース
         * @return this
         */
        public Namespace WithNamespaceId(string namespaceId) {
            this.namespaceId = namespaceId;
            return this;
        }

        /** オーナーID */
        public string ownerId { set; get; }

        /**
         * オーナーIDを設定
         *
         * @param ownerId オーナーID
         * @return this
         */
        public Namespace WithOwnerId(string ownerId) {
            this.ownerId = ownerId;
            return this;
        }

        /** ネームスペースの名前 */
        public string name { set; get; }

        /**
         * ネームスペースの名前を設定
         *
         * @param name ネームスペースの名前
         * @return this
         */
        public Namespace WithName(string name) {
            this.name = name;
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
        public Namespace WithDescription(string description) {
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
        public Namespace WithPriority(string priority) {
            this.priority = priority;
            return this;
        }

        /** 無償課金通貨を異なるスロットで共有するか */
        public bool? shareFree { set; get; }

        /**
         * 無償課金通貨を異なるスロットで共有するかを設定
         *
         * @param shareFree 無償課金通貨を異なるスロットで共有するか
         * @return this
         */
        public Namespace WithShareFree(bool? shareFree) {
            this.shareFree = shareFree;
            return this;
        }

        /** 通貨の種類 */
        public string currency { set; get; }

        /**
         * 通貨の種類を設定
         *
         * @param currency 通貨の種類
         * @return this
         */
        public Namespace WithCurrency(string currency) {
            this.currency = currency;
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
        public Namespace WithAppleKey(string appleKey) {
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
        public Namespace WithGoogleKey(string googleKey) {
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
        public Namespace WithEnableFakeReceipt(bool? enableFakeReceipt) {
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
        public Namespace WithCreateWalletScript(ScriptSetting createWalletScript) {
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
        public Namespace WithDepositScript(ScriptSetting depositScript) {
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
        public Namespace WithWithdrawScript(ScriptSetting withdrawScript) {
            this.withdrawScript = withdrawScript;
            return this;
        }

        /** 未使用残高 */
        public double? balance { set; get; }

        /**
         * 未使用残高を設定
         *
         * @param balance 未使用残高
         * @return this
         */
        public Namespace WithBalance(double? balance) {
            this.balance = balance;
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
        public Namespace WithLogSetting(LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }

        /** 作成日時 */
        public long? createdAt { set; get; }

        /**
         * 作成日時を設定
         *
         * @param createdAt 作成日時
         * @return this
         */
        public Namespace WithCreatedAt(long? createdAt) {
            this.createdAt = createdAt;
            return this;
        }

        /** 最終更新日時 */
        public long? updatedAt { set; get; }

        /**
         * 最終更新日時を設定
         *
         * @param updatedAt 最終更新日時
         * @return this
         */
        public Namespace WithUpdatedAt(long? updatedAt) {
            this.updatedAt = updatedAt;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.namespaceId != null)
            {
                writer.WritePropertyName("namespaceId");
                writer.Write(this.namespaceId);
            }
            if(this.ownerId != null)
            {
                writer.WritePropertyName("ownerId");
                writer.Write(this.ownerId);
            }
            if(this.name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.name);
            }
            if(this.description != null)
            {
                writer.WritePropertyName("description");
                writer.Write(this.description);
            }
            if(this.priority != null)
            {
                writer.WritePropertyName("priority");
                writer.Write(this.priority);
            }
            if(this.shareFree.HasValue)
            {
                writer.WritePropertyName("shareFree");
                writer.Write(this.shareFree.Value);
            }
            if(this.currency != null)
            {
                writer.WritePropertyName("currency");
                writer.Write(this.currency);
            }
            if(this.appleKey != null)
            {
                writer.WritePropertyName("appleKey");
                writer.Write(this.appleKey);
            }
            if(this.googleKey != null)
            {
                writer.WritePropertyName("googleKey");
                writer.Write(this.googleKey);
            }
            if(this.enableFakeReceipt.HasValue)
            {
                writer.WritePropertyName("enableFakeReceipt");
                writer.Write(this.enableFakeReceipt.Value);
            }
            if(this.createWalletScript != null)
            {
                writer.WritePropertyName("createWalletScript");
                this.createWalletScript.WriteJson(writer);
            }
            if(this.depositScript != null)
            {
                writer.WritePropertyName("depositScript");
                this.depositScript.WriteJson(writer);
            }
            if(this.withdrawScript != null)
            {
                writer.WritePropertyName("withdrawScript");
                this.withdrawScript.WriteJson(writer);
            }
            if(this.balance.HasValue)
            {
                writer.WritePropertyName("balance");
                writer.Write(this.balance.Value);
            }
            if(this.logSetting != null)
            {
                writer.WritePropertyName("logSetting");
                this.logSetting.WriteJson(writer);
            }
            if(this.createdAt.HasValue)
            {
                writer.WritePropertyName("createdAt");
                writer.Write(this.createdAt.Value);
            }
            if(this.updatedAt.HasValue)
            {
                writer.WritePropertyName("updatedAt");
                writer.Write(this.updatedAt.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static Namespace FromDict(JsonData data)
        {
            return new Namespace()
                .WithNamespaceId(data.Keys.Contains("namespaceId") && data["namespaceId"] != null ? data["namespaceId"].ToString() : null)
                .WithOwnerId(data.Keys.Contains("ownerId") && data["ownerId"] != null ? data["ownerId"].ToString() : null)
                .WithName(data.Keys.Contains("name") && data["name"] != null ? data["name"].ToString() : null)
                .WithDescription(data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString() : null)
                .WithPriority(data.Keys.Contains("priority") && data["priority"] != null ? data["priority"].ToString() : null)
                .WithShareFree(data.Keys.Contains("shareFree") && data["shareFree"] != null ? (bool?)bool.Parse(data["shareFree"].ToString()) : null)
                .WithCurrency(data.Keys.Contains("currency") && data["currency"] != null ? data["currency"].ToString() : null)
                .WithAppleKey(data.Keys.Contains("appleKey") && data["appleKey"] != null ? data["appleKey"].ToString() : null)
                .WithGoogleKey(data.Keys.Contains("googleKey") && data["googleKey"] != null ? data["googleKey"].ToString() : null)
                .WithEnableFakeReceipt(data.Keys.Contains("enableFakeReceipt") && data["enableFakeReceipt"] != null ? (bool?)bool.Parse(data["enableFakeReceipt"].ToString()) : null)
                .WithCreateWalletScript(data.Keys.Contains("createWalletScript") && data["createWalletScript"] != null ? ScriptSetting.FromDict(data["createWalletScript"]) : null)
                .WithDepositScript(data.Keys.Contains("depositScript") && data["depositScript"] != null ? ScriptSetting.FromDict(data["depositScript"]) : null)
                .WithWithdrawScript(data.Keys.Contains("withdrawScript") && data["withdrawScript"] != null ? ScriptSetting.FromDict(data["withdrawScript"]) : null)
                .WithBalance(data.Keys.Contains("balance") && data["balance"] != null ? (double?)double.Parse(data["balance"].ToString()) : null)
                .WithLogSetting(data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}