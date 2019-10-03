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
	public class GetRecoverValueTableMasterRequest : Gs2Request<GetRecoverValueTableMasterRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public GetRecoverValueTableMasterRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナ回復量テーブル名 */
        public string recoverValueTableName { set; get; }

        /**
         * スタミナ回復量テーブル名を設定
         *
         * @param recoverValueTableName スタミナ回復量テーブル名
         * @return this
         */
        public GetRecoverValueTableMasterRequest WithRecoverValueTableName(string recoverValueTableName) {
            this.recoverValueTableName = recoverValueTableName;
            return this;
        }


	}
}