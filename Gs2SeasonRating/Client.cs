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

using Gs2.Gs2SeasonRating;
using Gs2.Unity.Gs2SeasonRating.Model;
using Gs2.Unity.Gs2SeasonRating.Result;
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
namespace Gs2.Unity.Gs2SeasonRating
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
		private readonly Gs2SeasonRatingWebSocketClient _client;
		private readonly Gs2SeasonRatingRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2SeasonRatingWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2SeasonRatingRestClient(connection.RestSession);
		}

        public IEnumerator GetSeasonModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzGetSeasonModelResult>> callback,
                string namespaceName,
                string seasonName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetSeasonModel(
                    new Gs2.Gs2SeasonRating.Request.GetSeasonModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithSeasonName(seasonName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzGetSeasonModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SeasonRating.Result.EzGetSeasonModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSeasonModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzListSeasonModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeSeasonModels(
                    new Gs2.Gs2SeasonRating.Request.DescribeSeasonModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzListSeasonModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SeasonRating.Result.EzListSeasonModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CreateVote(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzCreateVoteResult>> callback,
		        IGameSession session,
                string namespaceName,
                string seasonName,
                int numberOfPlayer,
                string sessionName = null,
                string keyId = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetBallot(
                    new Gs2.Gs2SeasonRating.Request.GetBallotRequest()
                        .WithNamespaceName(namespaceName)
                        .WithSeasonName(seasonName)
                        .WithSessionName(sessionName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithNumberOfPlayer(numberOfPlayer)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzCreateVoteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SeasonRating.Result.EzCreateVoteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Vote(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzVoteResult>> callback,
                string namespaceName,
                string ballotBody,
                string ballotSignature,
                List<Gs2.Unity.Gs2SeasonRating.Model.EzGameResult> gameResults = null,
                string keyId = null
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.Vote(
                    new Gs2.Gs2SeasonRating.Request.VoteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzVoteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SeasonRating.Result.EzVoteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator VoteMultiple(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzVoteMultipleResult>> callback,
                string namespaceName,
                List<Gs2.Unity.Gs2SeasonRating.Model.EzSignedBallot> signedBallots = null,
                List<Gs2.Unity.Gs2SeasonRating.Model.EzGameResult> gameResults = null,
                string keyId = null
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.VoteMultiple(
                    new Gs2.Gs2SeasonRating.Request.VoteMultipleRequest()
                        .WithNamespaceName(namespaceName)
                        .WithSignedBallots(signedBallots?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithGameResults(gameResults?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SeasonRating.Result.EzVoteMultipleResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SeasonRating.Result.EzVoteMultipleResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}