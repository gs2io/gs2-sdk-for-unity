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

using Gs2.Gs2StateMachine;
using Gs2.Unity.Gs2StateMachine.Model;
using Gs2.Unity.Gs2StateMachine.Result;
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
namespace Gs2.Unity.Gs2StateMachine
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
		private readonly Gs2StateMachineWebSocketClient _client;
		private readonly Gs2StateMachineRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2StateMachineWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2StateMachineRestClient(connection.RestSession);
		}

        public IEnumerator Emit(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzEmitResult>> callback,
		        IGameSession session,
                string namespaceName,
                string statusName,
                string eventName,
                string args = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Emit(
                    new Gs2.Gs2StateMachine.Request.EmitRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatusName(statusName)
                        .WithEventName(eventName)
                        .WithArgs(args),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzEmitResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2StateMachine.Result.EzEmitResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Exit(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzExitResult>> callback,
		        IGameSession session,
                string namespaceName,
                string statusName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ExitStateMachine(
                    new Gs2.Gs2StateMachine.Request.ExitStateMachineRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatusName(statusName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzExitResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2StateMachine.Result.EzExitResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzGetStatusResult>> callback,
		        IGameSession session,
                string namespaceName,
                string statusName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetStatus(
                    new Gs2.Gs2StateMachine.Request.GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatusName(statusName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzGetStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2StateMachine.Result.EzGetStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzListStatusesResult>> callback,
		        IGameSession session,
                string namespaceName,
                string status = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeStatuses(
                    new Gs2.Gs2StateMachine.Request.DescribeStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatus(status)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzListStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2StateMachine.Result.EzListStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Report(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzReportResult>> callback,
		        IGameSession session,
                string namespaceName,
                string statusName,
                List<Gs2.Unity.Gs2StateMachine.Model.EzEvent> events = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Report(
                    new Gs2.Gs2StateMachine.Request.ReportRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatusName(statusName)
                        .WithEvents(events?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2StateMachine.Result.EzReportResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2StateMachine.Result.EzReportResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}