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
	[System.Serializable]
	public class DeleteRecoverIntervalTableMasterRequest : Gs2Request<DeleteRecoverIntervalTableMasterRequest>
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
        public DeleteRecoverIntervalTableMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナ回復間隔テーブル名 */
		[UnityEngine.SerializeField]
        public string recoverIntervalTableName;

        /**
         * スタミナ回復間隔テーブル名を設定
         *
         * @param recoverIntervalTableName スタミナ回復間隔テーブル名
         * @return this
         */
        public DeleteRecoverIntervalTableMasterRequest WithRecoverIntervalTableName(string recoverIntervalTableName) {
            this.recoverIntervalTableName = recoverIntervalTableName;
            return this;
        }


    	[Preserve]
        public static DeleteRecoverIntervalTableMasterRequest FromDict(JsonData data)
        {
            return new DeleteRecoverIntervalTableMasterRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                recoverIntervalTableName = data.Keys.Contains("recoverIntervalTableName") && data["recoverIntervalTableName"] != null ? data["recoverIntervalTableName"].ToString(): null,
            };
        }

	}
}