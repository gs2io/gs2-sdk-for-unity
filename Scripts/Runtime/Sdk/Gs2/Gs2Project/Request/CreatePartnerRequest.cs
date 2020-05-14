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
using Gs2.Gs2Project.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Request
{
	[Preserve]
	public class CreatePartnerRequest : Gs2Request<CreatePartnerRequest>
	{

        /** メールアドレス */
        public string email { set; get; }

        /**
         * メールアドレスを設定
         *
         * @param email メールアドレス
         * @return this
         */
        public CreatePartnerRequest WithEmail(string email) {
            this.email = email;
            return this;
        }


        /** 会社名 */
        public string companyName { set; get; }

        /**
         * 会社名を設定
         *
         * @param companyName 会社名
         * @return this
         */
        public CreatePartnerRequest WithCompanyName(string companyName) {
            this.companyName = companyName;
            return this;
        }


        /** パスワード */
        public string password { set; get; }

        /**
         * パスワードを設定
         *
         * @param password パスワード
         * @return this
         */
        public CreatePartnerRequest WithPassword(string password) {
            this.password = password;
            return this;
        }


    	[Preserve]
        public static CreatePartnerRequest FromDict(JsonData data)
        {
            return new CreatePartnerRequest {
                email = data.Keys.Contains("email") && data["email"] != null ? data["email"].ToString(): null,
                companyName = data.Keys.Contains("companyName") && data["companyName"] != null ? data["companyName"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
            };
        }

	}
}