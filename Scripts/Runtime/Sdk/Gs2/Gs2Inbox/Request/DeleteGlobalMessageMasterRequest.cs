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
using Gs2.Gs2Inbox.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Inbox.Request
{
	[Preserve]
	[System.Serializable]
	public class DeleteGlobalMessageMasterRequest : Gs2Request<DeleteGlobalMessageMasterRequest>
	{

        /** ネームスペース名 */
		[UnityEngine.SerializeField]
        public string namespaceName;

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DeleteGlobalMessageMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 全ユーザに向けたメッセージ名 */
		[UnityEngine.SerializeField]
        public string globalMessageName;

        /**
         * 全ユーザに向けたメッセージ名を設定
         *
         * @param globalMessageName 全ユーザに向けたメッセージ名
         * @return this
         */
        public DeleteGlobalMessageMasterRequest WithGlobalMessageName(string globalMessageName) {
            this.globalMessageName = globalMessageName;
            return this;
        }


    	[Preserve]
        public static DeleteGlobalMessageMasterRequest FromDict(JsonData data)
        {
            return new DeleteGlobalMessageMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                globalMessageName = data.Keys.Contains("globalMessageName") && data["globalMessageName"] != null ? data["globalMessageName"].ToString(): null,
            };
        }

	}
}