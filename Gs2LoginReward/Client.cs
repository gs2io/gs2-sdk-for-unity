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

using Gs2.Gs2LoginReward;
using Gs2.Unity.Gs2LoginReward.Model;
using Gs2.Unity.Gs2LoginReward.Result;
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
namespace Gs2.Unity.Gs2LoginReward
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
		private readonly Gs2LoginRewardWebSocketClient _client;
		private readonly Gs2LoginRewardRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2LoginRewardWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2LoginRewardRestClient(connection.RestSession);
		}

        public IEnumerator MissedReceive(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzMissedReceiveResult>> callback,
		        GameSession session,
                string namespaceName,
                string bonusModelName,
                int stepNumber,
                List<Gs2.Unity.Gs2LoginReward.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.MissedReceive(
                    new Gs2.Gs2LoginReward.Request.MissedReceiveRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBonusModelName(bonusModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStepNumber(stepNumber)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzMissedReceiveResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzMissedReceiveResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Receive(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzReceiveResult>> callback,
		        GameSession session,
                string namespaceName,
                string bonusModelName,
                List<Gs2.Unity.Gs2LoginReward.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Receive(
                    new Gs2.Gs2LoginReward.Request.ReceiveRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBonusModelName(bonusModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzReceiveResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzReceiveResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBonusModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzGetBonusModelResult>> callback,
                string namespaceName,
                string bonusModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetBonusModel(
                    new Gs2.Gs2LoginReward.Request.GetBonusModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBonusModelName(bonusModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzGetBonusModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzGetBonusModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBonusModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzListBonusModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeBonusModels(
                    new Gs2.Gs2LoginReward.Request.DescribeBonusModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzListBonusModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzListBonusModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetReceiveStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzGetReceiveStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string bonusModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetReceiveStatus(
                    new Gs2.Gs2LoginReward.Request.GetReceiveStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBonusModelName(bonusModelName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzGetReceiveStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzGetReceiveStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListReceiveStatuss(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzListReceiveStatussResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeReceiveStatuses(
                    new Gs2.Gs2LoginReward.Request.DescribeReceiveStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2LoginReward.Result.EzListReceiveStatussResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2LoginReward.Result.EzListReceiveStatussResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}