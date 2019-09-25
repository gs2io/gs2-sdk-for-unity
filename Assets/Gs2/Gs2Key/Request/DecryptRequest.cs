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
	public class DecryptRequest : Gs2Request<DecryptRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DecryptRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 暗号鍵名 */
        public string keyName { set; get; }

        /**
         * 暗号鍵名を設定
         *
         * @param keyName 暗号鍵名
         * @return this
         */
        public DecryptRequest WithKeyName(string keyName) {
            this.keyName = keyName;
            return this;
        }


        /** None */
        public string data { set; get; }

        /**
         * Noneを設定
         *
         * @param data None
         * @return this
         */
        public DecryptRequest WithData(string data) {
            this.data = data;
            return this;
        }


	}
}