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
using Gs2.Gs2Inbox.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Inbox.Model
{
	[Preserve]
	public class EzMessage
	{
		/** メッセージ */
		public string MessageId { get; set; }
		/** メッセージID */
		public string Name { get; set; }
		/** メッセージの内容に相当するメタデータ */
		public string Metadata { get; set; }
		/** 既読状態 */
		public bool IsRead { get; set; }
		/** 開封時に実行する入手アクション */
		public List<EzAcquireAction> ReadAcquireActions { get; set; }
		/** 作成日時 */
		public long ReceivedAt { get; set; }
		/** 最終更新日時 */
		public long ReadAt { get; set; }

		public EzMessage()
		{

		}

		public EzMessage(Gs2.Gs2Inbox.Model.Message @message)
		{
			MessageId = @message.messageId;
			Name = @message.name;
			Metadata = @message.metadata;
			IsRead = @message.isRead.HasValue ? @message.isRead.Value : false;
			ReadAcquireActions = @message.readAcquireActions != null ? @message.readAcquireActions.Select(value =>
                {
                    return new EzAcquireAction(value);
                }
			).ToList() : new List<EzAcquireAction>(new EzAcquireAction[] {});
			ReceivedAt = @message.receivedAt.HasValue ? @message.receivedAt.Value : 0;
			ReadAt = @message.readAt.HasValue ? @message.readAt.Value : 0;
		}

        public Message ToModel()
        {
            return new Message {
                messageId = MessageId,
                name = Name,
                metadata = Metadata,
                isRead = IsRead,
                readAcquireActions = ReadAcquireActions != null ? ReadAcquireActions.Select(Value0 =>
                        {
                            return new AcquireAction
                            {
                                action = Value0.Action,
                                request = Value0.Request,
                            };
                        }
                ).ToList() : new List<AcquireAction>(new AcquireAction[] {}),
                receivedAt = ReceivedAt,
                readAt = ReadAt,
            };
        }
	}
}
