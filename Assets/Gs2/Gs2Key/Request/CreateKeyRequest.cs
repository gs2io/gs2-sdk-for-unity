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
using Gs2.Gs2Key.Model;

namespace Gs2.Gs2Key.Request
{
	public class CreateKeyRequest : Gs2Request<CreateKeyRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateKeyRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 暗号鍵名 */
        public string name { set; get; }

        /**
         * 暗号鍵名を設定
         *
         * @param name 暗号鍵名
         * @return this
         */
        public CreateKeyRequest WithName(string name) {
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
        public CreateKeyRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


	}
}