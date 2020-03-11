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
using Gs2.Gs2Version.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Version.Request
{
	[Preserve]
	[System.Serializable]
	public class UpdateNamespaceRequest : Gs2Request<UpdateNamespaceRequest>
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
        public UpdateNamespaceRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** 説明文 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * 説明文を設定
         *
         * @param description 説明文
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** バージョンチェック通過後に改めて発行するプロジェクトトークンの権限判定に使用する ユーザ のGRN */
		[UnityEngine.SerializeField]
        public string assumeUserId;

        /**
         * バージョンチェック通過後に改めて発行するプロジェクトトークンの権限判定に使用する ユーザ のGRNを設定
         *
         * @param assumeUserId バージョンチェック通過後に改めて発行するプロジェクトトークンの権限判定に使用する ユーザ のGRN
         * @return this
         */
        public UpdateNamespaceRequest WithAssumeUserId(string assumeUserId) {
            this.assumeUserId = assumeUserId;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Version.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(global::Gs2.Gs2Version.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                assumeUserId = data.Keys.Contains("assumeUserId") && data["assumeUserId"] != null ? data["assumeUserId"].ToString(): null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Version.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}