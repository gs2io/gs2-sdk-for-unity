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
using Gs2.Gs2Gateway.Model;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Gateway.Model
{
	[Preserve]
	[System.Serializable]
	public class EzWebSocketSession
	{
		/** コネクションID */
		[UnityEngine.SerializeField]
		public string ConnectionId;
		/** ネームスペース名 */
		[UnityEngine.SerializeField]
		public string NamespaceName;
		/** ユーザーID */
		[UnityEngine.SerializeField]
		public string UserId;

		public EzWebSocketSession()
		{

		}

		public EzWebSocketSession(Gs2.Gs2Gateway.Model.WebSocketSession @webSocketSession)
		{
			ConnectionId = @webSocketSession.connectionId;
			NamespaceName = @webSocketSession.namespaceName;
			UserId = @webSocketSession.userId;
		}

        public virtual WebSocketSession ToModel()
        {
            return new WebSocketSession {
                connectionId = ConnectionId,
                namespaceName = NamespaceName,
                userId = UserId,
            };
        }

        public virtual void WriteJson(JsonWriter writer)
        {
            writer.WriteObjectStart();
            if(this.ConnectionId != null)
            {
                writer.WritePropertyName("connectionId");
                writer.Write(this.ConnectionId);
            }
            if(this.NamespaceName != null)
            {
                writer.WritePropertyName("namespaceName");
                writer.Write(this.NamespaceName);
            }
            if(this.UserId != null)
            {
                writer.WritePropertyName("userId");
                writer.Write(this.UserId);
            }
            writer.WriteObjectEnd();
        }
	}
}
