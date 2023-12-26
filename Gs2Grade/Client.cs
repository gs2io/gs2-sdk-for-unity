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

using Gs2.Gs2Grade;
using Gs2.Unity.Gs2Grade.Model;
using Gs2.Unity.Gs2Grade.Result;
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
namespace Gs2.Unity.Gs2Grade
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
		private readonly Gs2GradeWebSocketClient _client;
		private readonly Gs2GradeRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2GradeWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2GradeRestClient(connection.RestSession);
		}

        public IEnumerator GetGradeModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Grade.Result.EzGetGradeModelResult>> callback,
                string namespaceName,
                string gradeName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetGradeModel(
                    new Gs2.Gs2Grade.Request.GetGradeModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGradeName(gradeName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Grade.Result.EzGetGradeModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Grade.Result.EzGetGradeModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGradeModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Grade.Result.EzListGradeModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeGradeModels(
                    new Gs2.Gs2Grade.Request.DescribeGradeModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Grade.Result.EzListGradeModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Grade.Result.EzListGradeModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ApplyRankCap(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Grade.Result.EzApplyRankCapResult>> callback,
		        GameSession session,
                string namespaceName,
                string gradeName,
                string propertyId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.ApplyRankCap(
                    new Gs2.Gs2Grade.Request.ApplyRankCapRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGradeName(gradeName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Grade.Result.EzApplyRankCapResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Grade.Result.EzApplyRankCapResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Grade.Result.EzGetStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string gradeName,
                string propertyId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetStatus(
                    new Gs2.Gs2Grade.Request.GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGradeName(gradeName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Grade.Result.EzGetStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Grade.Result.EzGetStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Grade.Result.EzListStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                string gradeName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeStatuses(
                    new Gs2.Gs2Grade.Request.DescribeStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGradeName(gradeName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Grade.Result.EzListStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Grade.Result.EzListStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}