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
	public class NotificationType
	{

        /** 新着メッセージ通知を受け取るカテゴリ */
        public int? category { set; get; }

        /**
         * 新着メッセージ通知を受け取るカテゴリを設定
         *
         * @param category 新着メッセージ通知を受け取るカテゴリ
         * @return this
         */
        public NotificationType WithCategory(int? category) {
            this.category = category;
            return this;
        }

        /** オフラインだった時にモバイルプッシュ通知に転送するか */
        public bool? enableTransferMobilePushNotification { set; get; }

        /**
         * オフラインだった時にモバイルプッシュ通知に転送するかを設定
         *
         * @param enableTransferMobilePushNotification オフラインだった時にモバイルプッシュ通知に転送するか
         * @return this
         */
        public NotificationType WithEnableTransferMobilePushNotification(bool? enableTransferMobilePushNotification) {
            this.enableTransferMobilePushNotification = enableTransferMobilePushNotification;
            return this;
        }

        public void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.category.HasValue)
            {
                writer.WritePropertyName("category");
                writer.Write(this.category.Value);
            }
            if(this.enableTransferMobilePushNotification.HasValue)
            {
                writer.WritePropertyName("enableTransferMobilePushNotification");
                writer.Write(this.enableTransferMobilePushNotification.Value);
            }
            writer.WriteObjectEnd();
        }

    	[Preserve]
        public static NotificationType FromDict(JsonData data)
        {
            return new NotificationType()
                .WithCategory(data.Keys.Contains("category") && data["category"] != null ? (int?)int.Parse(data["category"].ToString()) : null)
                .WithEnableTransferMobilePushNotification(data.Keys.Contains("enableTransferMobilePushNotification") && data["enableTransferMobilePushNotification"] != null ? (bool?)bool.Parse(data["enableTransferMobilePushNotification"].ToString()) : null);
        }
	}
}