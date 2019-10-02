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
	public class EzMessage
	{
		/** ルーム名 */
		public string RoomName { get; set; }
		/** 発言したユーザID */
		public string UserId { get; set; }
		/** メッセージの種類を分類したい時の種類番号 */
		public int Category { get; set; }
		/** メタデータ */
		public string Metadata { get; set; }
		/** 作成日時 */
		public long CreatedAt { get; set; }

		public EzMessage()
		{

		}

		public EzMessage(Gs2.Gs2Chat.Model.Message @message)
		{
			RoomName = @message.roomName;
			UserId = @message.userId;
			Category = @message.category.HasValue ? @message.category.Value : 0;
			Metadata = @message.metadata;
			CreatedAt = @message.createdAt.HasValue ? @message.createdAt.Value : 0;
		}

        public Message ToModel()
        {
            return new Message {
                roomName = RoomName,
                userId = UserId,
                category = Category,
                metadata = Metadata,
                createdAt = CreatedAt,
            };
        }
	}
}
