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

using Gs2.Gs2Mission;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.Result;
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
namespace Gs2.Unity.Gs2Mission
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
		private readonly Gs2MissionWebSocketClient _client;
		private readonly Gs2MissionRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2MissionWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2MissionRestClient(connection.RestSession);
		}

        public IEnumerator BatchReceiveRewards(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzBatchReceiveRewardsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string missionGroupName,
                List<string> missionTaskNames
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.BatchComplete(
                    new Gs2.Gs2Mission.Request.BatchCompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithMissionTaskNames(missionTaskNames?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzBatchReceiveRewardsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzBatchReceiveRewardsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator EvaluateComplete(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzEvaluateCompleteResult>> callback,
		        IGameSession session,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.EvaluateComplete(
                    new Gs2.Gs2Mission.Request.EvaluateCompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzEvaluateCompleteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzEvaluateCompleteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetComplete(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCompleteResult>> callback,
		        IGameSession session,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetComplete(
                    new Gs2.Gs2Mission.Request.GetCompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCompleteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzGetCompleteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCompletes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCompletesResult>> callback,
		        IGameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeCompletes(
                    new Gs2.Gs2Mission.Request.DescribeCompletesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCompletesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzListCompletesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ReceiveRewards(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzReceiveRewardsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string missionGroupName,
                string missionTaskName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Complete(
                    new Gs2.Gs2Mission.Request.CompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithMissionTaskName(missionTaskName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzReceiveRewardsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzReceiveRewardsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteCounter(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzDeleteCounterResult>> callback,
		        IGameSession session,
                string namespaceName,
                string counterName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteCounter(
                    new Gs2.Gs2Mission.Request.DeleteCounterRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithCounterName(counterName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzDeleteCounterResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzDeleteCounterResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetCounter(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCounterResult>> callback,
		        IGameSession session,
                string namespaceName,
                string counterName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetCounter(
                    new Gs2.Gs2Mission.Request.GetCounterRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithCounterName(counterName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCounterResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzGetCounterResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCounters(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCountersResult>> callback,
		        IGameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeCounters(
                    new Gs2.Gs2Mission.Request.DescribeCountersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCountersResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzListCountersResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetCounterModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCounterModelResult>> callback,
                string namespaceName,
                string counterName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetCounterModel(
                    new Gs2.Gs2Mission.Request.GetCounterModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCounterName(counterName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetCounterModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzGetCounterModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCounterModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCounterModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeCounterModels(
                    new Gs2.Gs2Mission.Request.DescribeCounterModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListCounterModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzListCounterModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetMissionGroupModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetMissionGroupModelResult>> callback,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetMissionGroupModel(
                    new Gs2.Gs2Mission.Request.GetMissionGroupModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetMissionGroupModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzGetMissionGroupModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMissionGroupModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListMissionGroupModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeMissionGroupModels(
                    new Gs2.Gs2Mission.Request.DescribeMissionGroupModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListMissionGroupModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzListMissionGroupModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetMissionTaskModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetMissionTaskModelResult>> callback,
                string namespaceName,
                string missionGroupName,
                string missionTaskName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetMissionTaskModel(
                    new Gs2.Gs2Mission.Request.GetMissionTaskModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithMissionTaskName(missionTaskName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzGetMissionTaskModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzGetMissionTaskModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMissionTaskModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListMissionTaskModelsResult>> callback,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeMissionTaskModels(
                    new Gs2.Gs2Mission.Request.DescribeMissionTaskModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Mission.Result.EzListMissionTaskModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Mission.Result.EzListMissionTaskModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}