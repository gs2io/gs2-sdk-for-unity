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
using Gs2.Gs2Stamina.Model;

namespace Gs2.Gs2Stamina.Request
{
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
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
        public CreateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** スタミナオーバーフロー上限に当たって回復できなかったスタミナを通知する スクリプト のGRN */
        public string overflowTriggerScriptId { set; get; }

        /**
         * スタミナオーバーフロー上限に当たって回復できなかったスタミナを通知する スクリプト のGRNを設定
         *
         * @param overflowTriggerScriptId スタミナオーバーフロー上限に当たって回復できなかったスタミナを通知する スクリプト のGRN
         * @return this
         */
        public CreateNamespaceRequest WithOverflowTriggerScriptId(string overflowTriggerScriptId) {
            this.overflowTriggerScriptId = overflowTriggerScriptId;
            return this;
        }


        /** スタミナオーバーフロー上限に当たって回復できなかったスタミナを追加する ネームスペース のGRN */
        public string overflowTriggerNamespaceId { set; get; }

        /**
         * スタミナオーバーフロー上限に当たって回復できなかったスタミナを追加する ネームスペース のGRNを設定
         *
         * @param overflowTriggerNamespaceId スタミナオーバーフロー上限に当たって回復できなかったスタミナを追加する ネームスペース のGRN
         * @return this
         */
        public CreateNamespaceRequest WithOverflowTriggerNamespaceId(string overflowTriggerNamespaceId) {
            this.overflowTriggerNamespaceId = overflowTriggerNamespaceId;
            return this;
        }


	}
}