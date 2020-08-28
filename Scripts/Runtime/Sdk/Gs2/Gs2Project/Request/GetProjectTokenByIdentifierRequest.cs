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
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Project.Request
{
	[Preserve]
	[System.Serializable]
	public class GetProjectTokenByIdentifierRequest : Gs2Request<GetProjectTokenByIdentifierRequest>
	{

        /** GS2アカウントの名前 */
		[UnityEngine.SerializeField]
        public string accountName;

        /**
         * GS2アカウントの名前を設定
         *
         * @param accountName GS2アカウントの名前
         * @return this
         */
        public GetProjectTokenByIdentifierRequest WithAccountName(string accountName) {
            this.accountName = accountName;
            return this;
        }


        /** プロジェクト名 */
		[UnityEngine.SerializeField]
        public string projectName;

        /**
         * プロジェクト名を設定
         *
         * @param projectName プロジェクト名
         * @return this
         */
        public GetProjectTokenByIdentifierRequest WithProjectName(string projectName) {
            this.projectName = projectName;
            return this;
        }


        /** ユーザ名 */
		[UnityEngine.SerializeField]
        public string userName;

        /**
         * ユーザ名を設定
         *
         * @param userName ユーザ名
         * @return this
         */
        public GetProjectTokenByIdentifierRequest WithUserName(string userName) {
            this.userName = userName;
            return this;
        }


        /** パスワード */
		[UnityEngine.SerializeField]
        public string password;

        /**
         * パスワードを設定
         *
         * @param password パスワード
         * @return this
         */
        public GetProjectTokenByIdentifierRequest WithPassword(string password) {
            this.password = password;
            return this;
        }


    	[Preserve]
        public static GetProjectTokenByIdentifierRequest FromDict(JsonData data)
        {
            return new GetProjectTokenByIdentifierRequest {
                accountName = data.Keys.Contains("accountName") && data["accountName"] != null ? data["accountName"].ToString(): null,
                projectName = data.Keys.Contains("projectName") && data["projectName"] != null ? data["projectName"].ToString(): null,
                userName = data.Keys.Contains("userName") && data["userName"] != null ? data["userName"].ToString(): null,
                password = data.Keys.Contains("password") && data["password"] != null ? data["password"].ToString(): null,
            };
        }

	}
}