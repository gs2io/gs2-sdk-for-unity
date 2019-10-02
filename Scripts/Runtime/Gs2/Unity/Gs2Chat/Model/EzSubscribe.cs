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
using Gs2.Gs2Chat.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Chat.Model
{
	[Preserve]
	public class EzSubscribe
	{
		/** 購読するユーザID */
		public string UserId { get; set; }
		/** 購読するルーム名 */
		public string RoomName { get; set; }
		/** 新着メッセージ通知を受け取るカテゴリリスト */
		public List<EzNotificationType> NotificationTypes { get; set; }

		public EzSubscribe()
		{

		}

		public EzSubscribe(Gs2.Gs2Chat.Model.Subscribe @subscribe)
		{
			UserId = @subscribe.userId;
			RoomName = @subscribe.roomName;
			NotificationTypes = @subscribe.notificationTypes != null ? @subscribe.notificationTypes.Select(value =>
                {
                    return new EzNotificationType(value);
                }
			).ToList() : new List<EzNotificationType>(new EzNotificationType[] {});
		}

        public Subscribe ToModel()
        {
            return new Subscribe {
                userId = UserId,
                roomName = RoomName,
                notificationTypes = NotificationTypes != null ? NotificationTypes.Select(Value0 =>
                        {
                            return new NotificationType
                            {
                                category = Value0.Category,
                                enableTransferMobilePushNotification = Value0.EnableTransferMobilePushNotification,
                            };
                        }
                ).ToList() : new List<NotificationType>(new NotificationType[] {}),
            };
        }
	}
}
