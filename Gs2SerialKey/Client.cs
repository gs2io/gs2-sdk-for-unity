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

using Gs2.Gs2SerialKey;
using Gs2.Unity.Gs2SerialKey.Model;
using Gs2.Unity.Gs2SerialKey.Result;
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
namespace Gs2.Unity.Gs2SerialKey
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
		private readonly Gs2SerialKeyWebSocketClient _client;
		private readonly Gs2SerialKeyRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2SerialKeyWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2SerialKeyRestClient(connection.RestSession);
		}

        public IEnumerator GetCampaignModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzGetCampaignModelResult>> callback,
                string namespaceName,
                string campaignModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetCampaignModel(
                    new Gs2.Gs2SerialKey.Request.GetCampaignModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCampaignModelName(campaignModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzGetCampaignModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SerialKey.Result.EzGetCampaignModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Get(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzGetResult>> callback,
                string namespaceName,
                string code
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetSerialKey(
                    new Gs2.Gs2SerialKey.Request.GetSerialKeyRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCode(code),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzGetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SerialKey.Result.EzGetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UseSerialCode(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzUseSerialCodeResult>> callback,
		        IGameSession session,
                string namespaceName,
                string code
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.Use(
                    new Gs2.Gs2SerialKey.Request.UseRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithCode(code),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SerialKey.Result.EzUseSerialCodeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SerialKey.Result.EzUseSerialCodeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}