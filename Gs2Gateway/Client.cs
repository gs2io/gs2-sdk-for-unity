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

using Gs2.Gs2Gateway;
using Gs2.Unity.Gs2Gateway.Model;
using Gs2.Unity.Gs2Gateway.Result;
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
namespace Gs2.Unity.Gs2Gateway
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
		private readonly Gs2GatewayWebSocketClient _client;
		private readonly Gs2GatewayRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2GatewayWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2GatewayRestClient(connection.RestSession);
		}

        public IEnumerator SetUserId(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzSetUserIdResult>> callback,
		        IGameSession session,
                string namespaceName,
                bool? allowConcurrentAccess = null,
                string sessionId = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.SetUserId(
                    new Gs2.Gs2Gateway.Request.SetUserIdRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithAllowConcurrentAccess(allowConcurrentAccess)
                        .WithSessionId(sessionId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzSetUserIdResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Gateway.Result.EzSetUserIdResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteFirebaseToken(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzDeleteFirebaseTokenResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteFirebaseToken(
                    new Gs2.Gs2Gateway.Request.DeleteFirebaseTokenRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzDeleteFirebaseTokenResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Gateway.Result.EzDeleteFirebaseTokenResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetFirebaseToken(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzGetFirebaseTokenResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetFirebaseToken(
                    new Gs2.Gs2Gateway.Request.GetFirebaseTokenRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzGetFirebaseTokenResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Gateway.Result.EzGetFirebaseTokenResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SetFirebaseToken(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzSetFirebaseTokenResult>> callback,
		        IGameSession session,
                string namespaceName,
                string token,
                string locale = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.SetFirebaseToken(
                    new Gs2.Gs2Gateway.Request.SetFirebaseTokenRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithToken(token)
                        .WithLocale(locale),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Gateway.Result.EzSetFirebaseTokenResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Gateway.Result.EzSetFirebaseTokenResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}