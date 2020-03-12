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
using Gs2.Gs2Key.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Key.Request
{
	[Preserve]
	public class EncryptRequest : Gs2Request<EncryptRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public EncryptRequest WithNamespaceName(string namespaceName) {
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
        public EncryptRequest WithKeyName(string keyName) {
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
        public EncryptRequest WithData(string data) {
            this.data = data;
            return this;
        }


    	[Preserve]
        public static EncryptRequest FromDict(JsonData data)
        {
            return new EncryptRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                keyName = data.Keys.Contains("keyName") && data["keyName"] != null ? data["keyName"].ToString(): null,
                data = data.Keys.Contains("data") && data["data"] != null ? data["data"].ToString(): null,
            };
        }

	}
}