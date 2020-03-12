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
using Gs2.Core.Model;
using LitJson;
using UnityEngine.Scripting;

namespace Gs2.Gs2Chat.Model
{
	[Preserve]
	public class NotificationSetting
	{

        /** プッシュ通知に使用する GS2-Gateway のネームスペース のGRN */
        public string gatewayNamespaceId { set; get; }

        /**
         * プッシュ通知に使用する GS2-Gateway のネームスペース のGRNを設定
         *
         * @param gatewayNamespaceId プッシュ通知に使用する GS2-Gateway のネームスペース のGRN
         * @return this
         */
        public NotificationSetting WithGatewayNamespaceId(string gatewayNamespaceId) {
            this.gatewayNamespaceId = gatewayNamespaceId;
            return this;
        }

        /** モバイルプッシュ通知へ転送するか */
        public bool? enableTransferMobileNotification { set; get; }

        /**
         * モバイルプッシュ通知へ転送するかを設定
         *
         * @param enableTransferMobileNotification モバイルプッシュ通知へ転送するか
         * @return this
         */
        public NotificationSetting WithEnableTransferMobileNotification(bool? enableTransferMobileNotification) {
            this.enableTransferMobileNotification = enableTransferMobileNotification;
            return this;
        }

        /** モバイルプッシュ通知で使用するサウンドファイル名 */
        public string sound { set; get; }

        /**
         * モバイルプッシュ通知で使用するサウンドファイル名を設定
         *
         * @param sound モバイルプッシュ通知で使用するサウンドファイル名
         * @return this
         */
        public NotificationSetting WithSound(string sound) {
            this.sound = sound;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.gatewayNamespaceId != null)
            {
                writer.WritePropertyName("gatewayNamespaceId");
                writer.Write(this.gatewayNamespaceId);
            }
            if(this.enableTransferMobileNotification.HasValue)
            {
                writer.WritePropertyName("enableTransferMobileNotification");
                writer.Write(this.enableTransferMobileNotification.Value);
            }
            if(this.sound != null)
            {
                writer.WritePropertyName("sound");
                writer.Write(this.sound);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static NotificationSetting FromDict(JsonData data)
        {
            return new NotificationSetting()
                .WithGatewayNamespaceId(data.Keys.Contains("gatewayNamespaceId") && data["gatewayNamespaceId"] != null ? data["gatewayNamespaceId"].ToString() : null)
                .WithEnableTransferMobileNotification(data.Keys.Contains("enableTransferMobileNotification") && data["enableTransferMobileNotification"] != null ? (bool?)bool.Parse(data["enableTransferMobileNotification"].ToString()) : null)
                .WithSound(data.Keys.Contains("sound") && data["sound"] != null ? data["sound"].ToString() : null);
        }
	}
}