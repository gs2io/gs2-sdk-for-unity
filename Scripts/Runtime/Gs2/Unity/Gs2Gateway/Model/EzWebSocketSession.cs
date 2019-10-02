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
using UnityEngine.Scripting;


namespace Gs2.Unity.Gs2Gateway.Model
{
	[Preserve]
	public class EzWebSocketSession
	{
		/** コネクションID */
		public string ConnectionId { get; set; }
		/** ネームスペース名 */
		public string NamespaceName { get; set; }
		/** ユーザーID */
		public string UserId { get; set; }

		public EzWebSocketSession()
		{

		}

		public EzWebSocketSession(Gs2.Gs2Gateway.Model.WebSocketSession @webSocketSession)
		{
			ConnectionId = @webSocketSession.connectionId;
			NamespaceName = @webSocketSession.namespaceName;
			UserId = @webSocketSession.userId;
		}

        public WebSocketSession ToModel()
        {
            return new WebSocketSession {
                connectionId = ConnectionId,
                namespaceName = NamespaceName,
                userId = UserId,
            };
        }
	}
}
