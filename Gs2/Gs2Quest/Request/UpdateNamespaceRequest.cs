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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Quest.Model;

namespace Gs2.Gs2Quest.Request
{
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
	{

        /** カテゴリ名 */
        public string namespaceName { set; get; }

        /**
         * カテゴリ名を設定
         *
         * @param namespaceName カテゴリ名
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


        /** クエスト開始したときに実行するスクリプト */
        public ScriptSetting startQuestScript { set; get; }

        /**
         * クエスト開始したときに実行するスクリプトを設定
         *
         * @param startQuestScript クエスト開始したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithStartQuestScript(ScriptSetting startQuestScript) {
            this.startQuestScript = startQuestScript;
            return this;
        }


        /** クエストクリアしたときに実行するスクリプト */
        public ScriptSetting completeQuestScript { set; get; }

        /**
         * クエストクリアしたときに実行するスクリプトを設定
         *
         * @param completeQuestScript クエストクリアしたときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithCompleteQuestScript(ScriptSetting completeQuestScript) {
            this.completeQuestScript = completeQuestScript;
            return this;
        }


        /** クエスト失敗したときに実行するスクリプト */
        public ScriptSetting failedQuestScript { set; get; }

        /**
         * クエスト失敗したときに実行するスクリプトを設定
         *
         * @param failedQuestScript クエスト失敗したときに実行するスクリプト
         * @return this
         */
        public UpdateNamespaceRequest WithFailedQuestScript(ScriptSetting failedQuestScript) {
            this.failedQuestScript = failedQuestScript;
            return this;
        }


        /** 報酬付与処理をジョブとして追加するキューのネームスペース のGRN */
        public string queueNamespaceId { set; get; }

        /**
         * 報酬付与処理をジョブとして追加するキューのネームスペース のGRNを設定
         *
         * @param queueNamespaceId 報酬付与処理をジョブとして追加するキューのネームスペース のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithQueueNamespaceId(string queueNamespaceId) {
            this.queueNamespaceId = queueNamespaceId;
            return this;
        }


        /** 報酬付与処理のスタンプシートで使用する暗号鍵GRN */
        public string keyId { set; get; }

        /**
         * 報酬付与処理のスタンプシートで使用する暗号鍵GRNを設定
         *
         * @param keyId 報酬付与処理のスタンプシートで使用する暗号鍵GRN
         * @return this
         */
        public UpdateNamespaceRequest WithKeyId(string keyId) {
            this.keyId = keyId;
            return this;
        }


	}
}