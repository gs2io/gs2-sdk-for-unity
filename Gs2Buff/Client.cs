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

using Gs2.Gs2Buff;
using Gs2.Unity.Gs2Buff.Model;
using Gs2.Unity.Gs2Buff.Result;
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
namespace Gs2.Unity.Gs2Buff
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
		private readonly Gs2BuffWebSocketClient _client;
		private readonly Gs2BuffRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2BuffWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2BuffRestClient(connection.RestSession);
		}

        public IEnumerator GetBuffEntryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Buff.Result.EzGetBuffEntryModelResult>> callback,
                string namespaceName,
                string buffEntryName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetBuffEntryModel(
                    new Gs2.Gs2Buff.Request.GetBuffEntryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBuffEntryName(buffEntryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Buff.Result.EzGetBuffEntryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Buff.Result.EzGetBuffEntryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBuffEntryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Buff.Result.EzListBuffEntryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeBuffEntryModels(
                    new Gs2.Gs2Buff.Request.DescribeBuffEntryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Buff.Result.EzListBuffEntryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Buff.Result.EzListBuffEntryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ApplyBuff(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Buff.Result.EzApplyBuffResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ApplyBuff(
                    new Gs2.Gs2Buff.Request.ApplyBuffRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Buff.Result.EzApplyBuffResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Buff.Result.EzApplyBuffResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}