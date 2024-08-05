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

using Gs2.Gs2Log;
using Gs2.Unity.Gs2Log.Model;
using Gs2.Unity.Gs2Log.Result;
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
namespace Gs2.Unity.Gs2Log
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
		private readonly Gs2LogWebSocketClient _client;
		private readonly Gs2LogRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2LogWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2LogRestClient(connection.RestSession);
		}

        public IEnumerator SendInGameLog(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Log.Result.EzSendInGameLogResult>> callback,
		        IGameSession session,
                string namespaceName,
                string payload,
                List<Gs2.Unity.Gs2Log.Model.EzInGameLogTag> tags = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.SendInGameLog(
                    new Gs2.Gs2Log.Request.SendInGameLogRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTags(tags?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithPayload(payload)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Log.Result.EzSendInGameLogResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Log.Result.EzSendInGameLogResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}