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

using Gs2.Gs2Matchmaking;
using Gs2.Unity.Gs2Matchmaking.Model;
using Gs2.Unity.Gs2Matchmaking.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Gs2Quest;
using Gs2.Gs2Quest.Model;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Matchmaking
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
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MatchmakingWebSocketClient _client;
		private readonly Gs2MatchmakingRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MatchmakingWebSocketClient(profile.Gs2Session);
			if (profile.CheckRevokeCertificate)
			{
				_restClient = new Gs2MatchmakingRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2MatchmakingRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator CancelMatchmaking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCancelMatchmakingResult>> callback,
		        GameSession session,
                string namespaceName,
                string gatheringName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.CancelMatchmaking(
                    new Gs2.Gs2Matchmaking.Request.CancelMatchmakingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCancelMatchmakingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzCancelMatchmakingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CreateGathering(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCreateGatheringResult>> callback,
		        GameSession session,
                string namespaceName,
                Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> attributeRanges = null,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole> capacityOfRoles = null,
                List<string> allowUserIds = null,
                long? expiresAt = null,
                Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan expiresAtTimeSpan = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.CreateGathering(
                    new Gs2.Gs2Matchmaking.Request.CreateGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPlayer(player?.ToModel())
                        .WithAttributeRanges(attributeRanges?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithCapacityOfRoles(capacityOfRoles?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithAllowUserIds(allowUserIds?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithExpiresAt(expiresAt)
                        .WithExpiresAtTimeSpan(expiresAtTimeSpan?.ToModel()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCreateGatheringResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzCreateGatheringResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DoMatchmaking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzDoMatchmakingResult>> callback,
		        GameSession session,
                string namespaceName,
                Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player,
                string matchmakingContextToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DoMatchmaking(
                    new Gs2.Gs2Matchmaking.Request.DoMatchmakingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPlayer(player?.ToModel())
                        .WithMatchmakingContextToken(matchmakingContextToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzDoMatchmakingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzDoMatchmakingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetGathering(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetGatheringResult>> callback,
                string namespaceName,
                string gatheringName = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetGathering(
                    new Gs2.Gs2Matchmaking.Request.GetGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetGatheringResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzGetGatheringResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateGathering(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzUpdateGatheringResult>> callback,
		        GameSession session,
                string namespaceName,
                string gatheringName = null,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> attributeRanges = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.UpdateGathering(
                    new Gs2.Gs2Matchmaking.Request.UpdateGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithAttributeRanges(attributeRanges?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzUpdateGatheringResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzUpdateGatheringResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRatingModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingModelResult>> callback,
                string namespaceName,
                string ratingName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRatingModel(
                    new Gs2.Gs2Matchmaking.Request.GetRatingModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRatingName(ratingName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRatingModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzListRatingModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeRatingModels(
                    new Gs2.Gs2Matchmaking.Request.DescribeRatingModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzListRatingModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzListRatingModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRating(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingResult>> callback,
		        GameSession session,
                string namespaceName,
                string ratingName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetRating(
                    new Gs2.Gs2Matchmaking.Request.GetRatingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithRatingName(ratingName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzGetRatingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRatings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzListRatingsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeRatings(
                    new Gs2.Gs2Matchmaking.Request.DescribeRatingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzListRatingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzListRatingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CreateVote(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCreateVoteResult>> callback,
		        GameSession session,
                string namespaceName,
                string ratingName,
                string gatheringName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetBallot(
                    new Gs2.Gs2Matchmaking.Request.GetBallotRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRatingName(ratingName)
                        .WithGatheringName(gatheringName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzCreateVoteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzCreateVoteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Vote(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzVoteResult>> callback,
                string namespaceName,
                string ballotBody,
                string ballotSignature,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> gameResults = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.Vote(
                    new Gs2.Gs2Matchmaking.Request.VoteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzVoteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzVoteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator VoteMultiple(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzVoteMultipleResult>> callback,
                string namespaceName,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot> signedBallots = null,
                List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> gameResults = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.VoteMultiple(
                    new Gs2.Gs2Matchmaking.Request.VoteMultipleRequest()
                        .WithNamespaceName(namespaceName)
                        .WithSignedBallots(signedBallots?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithGameResults(gameResults?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Matchmaking.Result.EzVoteMultipleResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Matchmaking.Result.EzVoteMultipleResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}