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

using Gs2.Gs2AdReward;
using Gs2.Unity.Gs2AdReward.Model;
using Gs2.Unity.Gs2AdReward.Result;
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
namespace Gs2.Unity.Gs2AdReward
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
		private readonly Gs2AdRewardWebSocketClient _client;
		private readonly Gs2AdRewardRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2AdRewardWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2AdRewardRestClient(connection.RestSession);
		}

        public IEnumerator GetPoint(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2AdReward.Result.EzGetPointResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetPoint(
                    new Gs2.Gs2AdReward.Request.GetPointRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2AdReward.Result.EzGetPointResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2AdReward.Result.EzGetPointResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}