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
using Gs2.Gs2Stamina.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Stamina.Request
{
	[Preserve]
	public class DeleteMaxStaminaTableMasterRequest : Gs2Request<DeleteMaxStaminaTableMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public DeleteMaxStaminaTableMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 最大スタミナ値テーブル名 */
        public string maxStaminaTableName { set; get; }

        /**
         * 最大スタミナ値テーブル名を設定
         *
         * @param maxStaminaTableName 最大スタミナ値テーブル名
         * @return this
         */
        public DeleteMaxStaminaTableMasterRequest WithMaxStaminaTableName(string maxStaminaTableName) {
            this.maxStaminaTableName = maxStaminaTableName;
            return this;
        }


    	[Preserve]
        public static DeleteMaxStaminaTableMasterRequest FromDict(JsonData data)
        {
            return new DeleteMaxStaminaTableMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                maxStaminaTableName = data.Keys.Contains("maxStaminaTableName") && data["maxStaminaTableName"] != null ? data["maxStaminaTableName"].ToString(): null,
            };
        }

	}
}