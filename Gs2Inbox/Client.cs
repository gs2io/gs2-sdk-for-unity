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

using Gs2.Gs2Inbox;
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Inbox
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	[Preserve]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public partial class Client
	{
		private readonly Gs2.Unity.Util.Gs2Connection _connection;
		private readonly Gs2InboxWebSocketClient _client;
		private readonly Gs2InboxRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2InboxWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2InboxRestClient(connection.RestSession);
		}

        public IEnumerator Delete(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzDeleteResult>> callback,
		        IGameSession session,
                string namespaceName,
                string messageName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DeleteMessage(
                    new Gs2.Gs2Inbox.Request.DeleteMessageRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithMessageName(messageName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzDeleteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inbox.Result.EzDeleteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Get(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzGetResult>> callback,
		        IGameSession session,
                string namespaceName,
                string messageName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetMessage(
                    new Gs2.Gs2Inbox.Request.GetMessageRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMessageName(messageName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzGetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inbox.Result.EzGetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator List(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzListResult>> callback,
		        IGameSession session,
                string namespaceName,
                bool? isRead = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeMessages(
                    new Gs2.Gs2Inbox.Request.DescribeMessagesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithIsRead(isRead)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inbox.Result.EzListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Read(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzReadResult>> callback,
		        IGameSession session,
                string namespaceName,
                string messageName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ReadMessage(
                    new Gs2.Gs2Inbox.Request.ReadMessageRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMessageName(messageName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzReadResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inbox.Result.EzReadResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ReceiveGlobalMessage(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzReceiveGlobalMessageResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ReceiveGlobalMessage(
                    new Gs2.Gs2Inbox.Request.ReceiveGlobalMessageRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inbox.Result.EzReceiveGlobalMessageResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inbox.Result.EzReceiveGlobalMessageResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}