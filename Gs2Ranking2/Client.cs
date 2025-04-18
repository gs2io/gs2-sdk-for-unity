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

using Gs2.Gs2Ranking2;
using Gs2.Unity.Gs2Ranking2.Model;
using Gs2.Unity.Gs2Ranking2.Result;
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
namespace Gs2.Unity.Gs2Ranking2
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
		private readonly Gs2Ranking2WebSocketClient _client;
		private readonly Gs2Ranking2RestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2Ranking2WebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2Ranking2RestClient(connection.RestSession);
		}

        public IEnumerator GetGlobalRankingRank(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingRankResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetGlobalRanking(
                    new Gs2.Gs2Ranking2.Request.GetGlobalRankingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingRankResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingRankResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGlobalRankings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeGlobalRankings(
                    new Gs2.Gs2Ranking2.Request.DescribeGlobalRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetGlobalRankingModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingModelResult>> callback,
                string namespaceName,
                string rankingName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetGlobalRankingModel(
                    new Gs2.Gs2Ranking2.Request.GetGlobalRankingModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGlobalRankingModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeGlobalRankingModels(
                    new Gs2.Gs2Ranking2.Request.DescribeGlobalRankingModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetGlobalRankingReceivedReward(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingReceivedRewardResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetGlobalRankingReceivedReward(
                    new Gs2.Gs2Ranking2.Request.GetGlobalRankingReceivedRewardRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingReceivedRewardResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingReceivedRewardResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGlobalRankingReceivedRewards(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingReceivedRewardsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName = null,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeGlobalRankingReceivedRewards(
                    new Gs2.Gs2Ranking2.Request.DescribeGlobalRankingReceivedRewardsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingReceivedRewardsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingReceivedRewardsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ReceiveGlobalRankingReward(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzReceiveGlobalRankingRewardResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null,
                List<Gs2.Unity.Gs2Ranking2.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ReceiveGlobalRankingReceivedReward(
                    new Gs2.Gs2Ranking2.Request.ReceiveGlobalRankingReceivedRewardRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzReceiveGlobalRankingRewardResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzReceiveGlobalRankingRewardResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetGlobalRankingScore(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingScoreResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetGlobalRankingScore(
                    new Gs2.Gs2Ranking2.Request.GetGlobalRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingScoreResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetGlobalRankingScoreResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGlobalRankingScores(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingScoresResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeGlobalRankingScores(
                    new Gs2.Gs2Ranking2.Request.DescribeGlobalRankingScoresRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingScoresResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListGlobalRankingScoresResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PutGlobalRanking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutGlobalRankingResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long score,
                string metadata = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.PutGlobalRankingScore(
                    new Gs2.Gs2Ranking2.Request.PutGlobalRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScore(score)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutGlobalRankingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzPutGlobalRankingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetClusterRankingRank(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingRankResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetClusterRanking(
                    new Gs2.Gs2Ranking2.Request.GetClusterRankingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingRankResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingRankResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListClusterRankings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeClusterRankings(
                    new Gs2.Gs2Ranking2.Request.DescribeClusterRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetClusterRankingModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingModelResult>> callback,
                string namespaceName,
                string rankingName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetClusterRankingModel(
                    new Gs2.Gs2Ranking2.Request.GetClusterRankingModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListClusterRankingModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeClusterRankingModels(
                    new Gs2.Gs2Ranking2.Request.DescribeClusterRankingModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetClusterRankingReceivedReward(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingReceivedRewardResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetClusterRankingReceivedReward(
                    new Gs2.Gs2Ranking2.Request.GetClusterRankingReceivedRewardRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingReceivedRewardResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingReceivedRewardResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListClusterRankingReceivedRewards(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingReceivedRewardsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName = null,
                string clusterName = null,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeClusterRankingReceivedRewards(
                    new Gs2.Gs2Ranking2.Request.DescribeClusterRankingReceivedRewardsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingReceivedRewardsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingReceivedRewardsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ReceiveClusterRankingReward(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzReceiveClusterRankingRewardResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long? season = null,
                List<Gs2.Unity.Gs2Ranking2.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.ReceiveClusterRankingReceivedReward(
                    new Gs2.Gs2Ranking2.Request.ReceiveClusterRankingReceivedRewardRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzReceiveClusterRankingRewardResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzReceiveClusterRankingRewardResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetClusterRankingScore(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingScoreResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetClusterRankingScore(
                    new Gs2.Gs2Ranking2.Request.GetClusterRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingScoreResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetClusterRankingScoreResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListClusterRankingScores(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingScoresResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName = null,
                string clusterName = null,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeClusterRankingScores(
                    new Gs2.Gs2Ranking2.Request.DescribeClusterRankingScoresRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingScoresResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListClusterRankingScoresResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PutClusterRanking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutClusterRankingResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string clusterName,
                long score,
                string metadata = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.PutClusterRankingScore(
                    new Gs2.Gs2Ranking2.Request.PutClusterRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScore(score)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutClusterRankingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzPutClusterRankingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSubscribe(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string targetUserId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetSubscribe(
                    new Gs2.Gs2Ranking2.Request.GetSubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribesResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribes(
                    new Gs2.Gs2Ranking2.Request.DescribeSubscribesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListSubscribesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSubscribeRankingRank(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingRankResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetSubscribeRanking(
                    new Gs2.Gs2Ranking2.Request.GetSubscribeRankingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingRankResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingRankResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribeRankings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribeRankings(
                    new Gs2.Gs2Ranking2.Request.DescribeSubscribeRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSubscribeRankingModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingModelResult>> callback,
                string namespaceName,
                string rankingName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetSubscribeRankingModel(
                    new Gs2.Gs2Ranking2.Request.GetSubscribeRankingModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribeRankingModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeSubscribeRankingModels(
                    new Gs2.Gs2Ranking2.Request.DescribeSubscribeRankingModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSubscribeRankingScore(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingScoreResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long? season = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetSubscribeRankingScore(
                    new Gs2.Gs2Ranking2.Request.GetSubscribeRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithSeason(season)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingScoreResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzGetSubscribeRankingScoreResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribeRankingScores(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingScoresResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribeRankingScores(
                    new Gs2.Gs2Ranking2.Request.DescribeSubscribeRankingScoresRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingScoresResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzListSubscribeRankingScoresResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PutSubscribeRanking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutSubscribeRankingResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rankingName,
                long score,
                string metadata = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.PutSubscribeRankingScore(
                    new Gs2.Gs2Ranking2.Request.PutSubscribeRankingScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRankingName(rankingName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScore(score)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking2.Result.EzPutSubscribeRankingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking2.Result.EzPutSubscribeRankingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}