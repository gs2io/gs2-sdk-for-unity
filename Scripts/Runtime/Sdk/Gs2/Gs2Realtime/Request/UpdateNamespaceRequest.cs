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
using Gs2.Gs2Realtime.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Realtime.Request
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


        /** ネームスペースの説明 */
		[UnityEngine.SerializeField]
        public string description;

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public UpdateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** サーバの種類 */
		[UnityEngine.SerializeField]
        public string serverType;

        /**
         * サーバの種類を設定
         *
         * @param serverType サーバの種類
         * @return this
         */
        public UpdateNamespaceRequest WithServerType(string serverType) {
            this.serverType = serverType;
            return this;
        }


        /** サーバのスペック */
		[UnityEngine.SerializeField]
        public string serverSpec;

        /**
         * サーバのスペックを設定
         *
         * @param serverSpec サーバのスペック
         * @return this
         */
        public UpdateNamespaceRequest WithServerSpec(string serverSpec) {
            this.serverSpec = serverSpec;
            return this;
        }


        /** ルームの作成が終わったときのプッシュ通知 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Realtime.Model.NotificationSetting createNotification;

        /**
         * ルームの作成が終わったときのプッシュ通知を設定
         *
         * @param createNotification ルームの作成が終わったときのプッシュ通知
         * @return this
         */
        public UpdateNamespaceRequest WithCreateNotification(global::Gs2.Gs2Realtime.Model.NotificationSetting createNotification) {
            this.createNotification = createNotification;
            return this;
        }


        /** ログの出力設定 */
		[UnityEngine.SerializeField]
        public global::Gs2.Gs2Realtime.Model.LogSetting logSetting;

        /**
         * ログの出力設定を設定
         *
         * @param logSetting ログの出力設定
         * @return this
         */
        public UpdateNamespaceRequest WithLogSetting(global::Gs2.Gs2Realtime.Model.LogSetting logSetting) {
            this.logSetting = logSetting;
            return this;
        }


    	[Preserve]
        public static UpdateNamespaceRequest FromDict(JsonData data)
        {
            return new UpdateNamespaceRequest {
                namespaceName = data.Keys.Contains("namespaceName") && data["namespaceName"] != null ? data["namespaceName"].ToString(): null,
                description = data.Keys.Contains("description") && data["description"] != null ? data["description"].ToString(): null,
                serverType = data.Keys.Contains("serverType") && data["serverType"] != null ? data["serverType"].ToString(): null,
                serverSpec = data.Keys.Contains("serverSpec") && data["serverSpec"] != null ? data["serverSpec"].ToString(): null,
                createNotification = data.Keys.Contains("createNotification") && data["createNotification"] != null ? global::Gs2.Gs2Realtime.Model.NotificationSetting.FromDict(data["createNotification"]) : null,
                logSetting = data.Keys.Contains("logSetting") && data["logSetting"] != null ? global::Gs2.Gs2Realtime.Model.LogSetting.FromDict(data["logSetting"]) : null,
            };
        }

	}
}