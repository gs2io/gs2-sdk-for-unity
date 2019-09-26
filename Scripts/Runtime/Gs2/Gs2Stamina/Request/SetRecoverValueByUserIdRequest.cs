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
	public class SetRecoverValueByUserIdRequest : Gs2Request<SetRecoverValueByUserIdRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public SetRecoverValueByUserIdRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** スタミナの種類名 */
        public string staminaName { set; get; }

        /**
         * スタミナの種類名を設定
         *
         * @param staminaName スタミナの種類名
         * @return this
         */
        public SetRecoverValueByUserIdRequest WithStaminaName(string staminaName) {
            this.staminaName = staminaName;
            return this;
        }


        /** ユーザーID */
        public string userId { set; get; }

        /**
         * ユーザーIDを設定
         *
         * @param userId ユーザーID
         * @return this
         */
        public SetRecoverValueByUserIdRequest WithUserId(string userId) {
            this.userId = userId;
            return this;
        }


        /** スタミナの回復量 */
        public int? recoverValue { set; get; }

        /**
         * スタミナの回復量を設定
         *
         * @param recoverValue スタミナの回復量
         * @return this
         */
        public SetRecoverValueByUserIdRequest WithRecoverValue(int? recoverValue) {
            this.recoverValue = recoverValue;
            return this;
        }


        /** 重複実行回避機能に使用するID */
        public string duplicationAvoider { set; get; }

        /**
         * 重複実行回避機能に使用するIDを設定
         *
         * @param duplicationAvoider 重複実行回避機能に使用するID
         * @return this
         */
        public SetRecoverValueByUserIdRequest WithDuplicationAvoider(string duplicationAvoider) {
            this.duplicationAvoider = duplicationAvoider;
            return this;
        }


	}
}