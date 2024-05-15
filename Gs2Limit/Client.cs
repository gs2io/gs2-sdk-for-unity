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

using Gs2.Gs2Limit;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Unity.Gs2Limit.Result;
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
namespace Gs2.Unity.Gs2Limit
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
		private readonly Gs2LimitWebSocketClient _client;
		private readonly Gs2LimitRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2LimitWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2LimitRestClient(connection.RestSession);
		}

        public IEnumerator CountUp(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Limit.Result.EzCountUpResult>> callback,
		        IGameSession session,
                string namespaceName,
                string limitName,
                string counterName,
                int? countUpValue = null,
                int? maxValue = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.CountUp(
                    new Gs2.Gs2Limit.Request.CountUpRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithCounterName(counterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithCountUpValue(countUpValue)
                        .WithMaxValue(maxValue),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Limit.Result.EzCountUpResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Limit.Result.EzCountUpResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetCounter(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Limit.Result.EzGetCounterResult>> callback,
		        IGameSession session,
                string namespaceName,
                string limitName,
                string counterName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetCounter(
                    new Gs2.Gs2Limit.Request.GetCounterRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithCounterName(counterName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Limit.Result.EzGetCounterResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Limit.Result.EzGetCounterResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCounters(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Limit.Result.EzListCountersResult>> callback,
		        IGameSession session,
                string namespaceName,
                string limitName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeCounters(
                    new Gs2.Gs2Limit.Request.DescribeCountersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Limit.Result.EzListCountersResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Limit.Result.EzListCountersResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetLimitModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Limit.Result.EzGetLimitModelResult>> callback,
                string namespaceName,
                string limitName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetLimitModel(
                    new Gs2.Gs2Limit.Request.GetLimitModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Limit.Result.EzGetLimitModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Limit.Result.EzGetLimitModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListLimitModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Limit.Result.EzListLimitModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeLimitModels(
                    new Gs2.Gs2Limit.Request.DescribeLimitModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Limit.Result.EzListLimitModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Limit.Result.EzListLimitModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}