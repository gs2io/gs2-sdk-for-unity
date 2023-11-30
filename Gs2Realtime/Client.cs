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
 *
 * deny overwrite
 */

using Gs2.Gs2Realtime;
using Gs2.Unity.Gs2Realtime.Model;
using Gs2.Unity.Gs2Realtime.Result;
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
namespace Gs2.Unity.Gs2Realtime
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
		private readonly Gs2RealtimeWebSocketClient _client;
		private readonly Gs2RealtimeRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
            _restClient = new Gs2RealtimeRestClient(connection.RestSession);
		}

        public IEnumerator Now(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Realtime.Result.EzNowResult>> callback
        )
		{
            yield return _connection.Run(
                callback,
		        null,
                cb => _client.Now(
                    new Gs2.Gs2Realtime.Request.NowRequest(),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Realtime.Result.EzNowResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Realtime.Result.EzNowResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRoom(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Realtime.Result.EzGetRoomResult>> callback,
                string namespaceName,
                string roomName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetRoom(
                    new Gs2.Gs2Realtime.Request.GetRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Realtime.Result.EzGetRoomResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Realtime.Result.EzGetRoomResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}