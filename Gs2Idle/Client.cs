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

using Gs2.Gs2Idle;
using Gs2.Unity.Gs2Idle.Model;
using Gs2.Unity.Gs2Idle.Result;
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
namespace Gs2.Unity.Gs2Idle
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
		private readonly Gs2IdleWebSocketClient _client;
		private readonly Gs2IdleRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2IdleWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2IdleRestClient(connection.RestSession);
		}

        public IEnumerator GetCategoryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzGetCategoryModelResult>> callback,
                string namespaceName,
                string categoryName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetCategoryModel(
                    new Gs2.Gs2Idle.Request.GetCategoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzGetCategoryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzGetCategoryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCategoryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzListCategoryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeCategoryModels(
                    new Gs2.Gs2Idle.Request.DescribeCategoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzListCategoryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzListCategoryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzGetStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetStatus(
                    new Gs2.Gs2Idle.Request.GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzGetStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzGetStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzListStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeStatuses(
                    new Gs2.Gs2Idle.Request.DescribeStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzListStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzListStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Prediction(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzPredictionResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Prediction(
                    new Gs2.Gs2Idle.Request.PredictionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzPredictionResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzPredictionResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Receive(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Idle.Result.EzReceiveResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Receive(
                    new Gs2.Gs2Idle.Request.ReceiveRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Idle.Result.EzReceiveResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Idle.Result.EzReceiveResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}