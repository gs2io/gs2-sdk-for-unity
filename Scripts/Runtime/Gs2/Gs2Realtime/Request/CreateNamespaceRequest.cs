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
using Gs2.Gs2Realtime.Model;

namespace Gs2.Gs2Realtime.Request
{
	public class CreateNamespaceRequest : Gs2Request<CreateNamespaceRequest>
	{

        /** ネームスペース名 */
        public string name { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param name ネームスペース名
         * @return this
         */
        public CreateNamespaceRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** ネームスペースの説明 */
        public string description { set; get; }

        /**
         * ネームスペースの説明を設定
         *
         * @param description ネームスペースの説明
         * @return this
         */
        public CreateNamespaceRequest WithDescription(string description) {
            this.description = description;
            return this;
        }


        /** サーバの種類 */
        public string serverType { set; get; }

        /**
         * サーバの種類を設定
         *
         * @param serverType サーバの種類
         * @return this
         */
        public CreateNamespaceRequest WithServerType(string serverType) {
            this.serverType = serverType;
            return this;
        }


        /** サーバのスペック */
        public string serverSpec { set; get; }

        /**
         * サーバのスペックを設定
         *
         * @param serverSpec サーバのスペック
         * @return this
         */
        public CreateNamespaceRequest WithServerSpec(string serverSpec) {
            this.serverSpec = serverSpec;
            return this;
        }


        /** ルームの作成が終わったときのプッシュ通知 */
        public NotificationSetting createNotification { set; get; }

        /**
         * ルームの作成が終わったときのプッシュ通知を設定
         *
         * @param createNotification ルームの作成が終わったときのプッシュ通知
         * @return this
         */
        public CreateNamespaceRequest WithCreateNotification(NotificationSetting createNotification) {
            this.createNotification = createNotification;
            return this;
        }


	}
}