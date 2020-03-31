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
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Inbox.Model
{
	[Preserve]
	[System.Serializable]
	public class EzMessage
	{
		/** メッセージ */
		[UnityEngine.SerializeField]
		public string MessageId;
		/** メッセージID */
		[UnityEngine.SerializeField]
		public string Name;
		/** メッセージの内容に相当するメタデータ */
		[UnityEngine.SerializeField]
		public string Metadata;
		/** 既読状態 */
		[UnityEngine.SerializeField]
		public bool IsRead;
		/** 開封時に実行する入手アクション */
		[UnityEngine.SerializeField]
		public List<EzAcquireAction> ReadAcquireActions;
		/** 作成日時 */
		[UnityEngine.SerializeField]
		public long ReceivedAt;
		/** 最終更新日時 */
		[UnityEngine.SerializeField]
		public long ReadAt;
		/** メッセージの有効期限 */
		[UnityEngine.SerializeField]
		public long ExpiresAt;

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
			ExpiresAt = @message.expiresAt.HasValue ? @message.expiresAt.Value : 0;
		}

        public virtual Message ToModel()
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
                expiresAt = ExpiresAt,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.MessageId != null)
            {
                writer.WritePropertyName("messageId");
                writer.Write(this.MessageId);
            }
            if(this.Name != null)
            {
                writer.WritePropertyName("name");
                writer.Write(this.Name);
            }
            if(this.Metadata != null)
            {
                writer.WritePropertyName("metadata");
                writer.Write(this.Metadata);
            }
            writer.WritePropertyName("isRead");
            writer.Write(this.IsRead);
            if(this.ReadAcquireActions != null)
            {
                writer.WritePropertyName("readAcquireActions");
                writer.WriteArrayStart();
                foreach(var item in this.ReadAcquireActions)
                {
                    item.WriteJson(writer);
                }
                writer.WriteArrayEnd();
            }
            writer.WritePropertyName("receivedAt");
            writer.Write(this.ReceivedAt);
            writer.WritePropertyName("readAt");
            writer.Write(this.ReadAt);
            writer.WritePropertyName("expiresAt");
            writer.Write(this.ExpiresAt);
            writer.WriteObjectEnd();
        }
	}
}
