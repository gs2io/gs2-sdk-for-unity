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

namespace Gs2.Gs2Account.Model
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

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public Namespace WithName(string name) {
            this.name = name;
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
        public Namespace WithDescription(string description) {
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
        public Namespace WithChangePasswordIfTakeOver(bool? changePasswordIfTakeOver) {
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
        public Namespace WithCreateAccountScript(ScriptSetting createAccountScript) {
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
        public Namespace WithAuthenticationScript(ScriptSetting authenticationScript) {
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
        public Namespace WithCreateTakeOverScript(ScriptSetting createTakeOverScript) {
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
        public Namespace WithDoTakeOverScript(ScriptSetting doTakeOverScript) {
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
            if(this.changePasswordIfTakeOver.HasValue)
            {
                writer.WritePropertyName("changePasswordIfTakeOver");
                writer.Write(this.changePasswordIfTakeOver.Value);
            }
            if(this.createAccountScript != null)
            {
                writer.WritePropertyName("createAccountScript");
                this.createAccountScript.WriteJson(writer);
            }
            if(this.authenticationScript != null)
            {
                writer.WritePropertyName("authenticationScript");
                this.authenticationScript.WriteJson(writer);
            }
            if(this.createTakeOverScript != null)
            {
                writer.WritePropertyName("createTakeOverScript");
                this.createTakeOverScript.WriteJson(writer);
            }
            if(this.doTakeOverScript != null)
            {
                writer.WritePropertyName("doTakeOverScript");
                this.doTakeOverScript.WriteJson(writer);
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
                .WithChangePasswordIfTakeOver(data.Keys.Contains("changePasswordIfTakeOver") && data["changePasswordIfTakeOver"] != null ? (bool?)bool.Parse(data["changePasswordIfTakeOver"].ToString()) : null)
                .WithCreateAccountScript(data.Keys.Contains("createAccountScript") && data["createAccountScript"] != null ? ScriptSetting.FromDict(data["createAccountScript"]) : null)
                .WithAuthenticationScript(data.Keys.Contains("authenticationScript") && data["authenticationScript"] != null ? ScriptSetting.FromDict(data["authenticationScript"]) : null)
                .WithCreateTakeOverScript(data.Keys.Contains("createTakeOverScript") && data["createTakeOverScript"] != null ? ScriptSetting.FromDict(data["createTakeOverScript"]) : null)
                .WithDoTakeOverScript(data.Keys.Contains("doTakeOverScript") && data["doTakeOverScript"] != null ? ScriptSetting.FromDict(data["doTakeOverScript"]) : null)
                .WithLogSetting(data.Keys.Contains("logSetting") && data["logSetting"] != null ? LogSetting.FromDict(data["logSetting"]) : null)
                .WithCreatedAt(data.Keys.Contains("createdAt") && data["createdAt"] != null ? (long?)long.Parse(data["createdAt"].ToString()) : null)
                .WithUpdatedAt(data.Keys.Contains("updatedAt") && data["updatedAt"] != null ? (long?)long.Parse(data["updatedAt"].ToString()) : null);
        }
	}
}