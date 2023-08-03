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

using Gs2.Gs2Ranking;
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Gs2Ranking.Result;
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
namespace Gs2.Unity.Gs2Ranking
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
		private readonly Gs2RankingWebSocketClient _client;
		private readonly Gs2RankingRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2RankingWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2RankingRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2RankingRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetCategory(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetCategoryResult>> callback,
                string namespaceName,
                string categoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetCategoryModel(
                    new Gs2.Gs2Ranking.Request.GetCategoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetCategoryResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzGetCategoryResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListCategories(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListCategoriesResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeCategoryModels(
                    new Gs2.Gs2Ranking.Request.DescribeCategoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListCategoriesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzListCategoriesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListSubscribesResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribesByCategoryName(
                    new Gs2.Gs2Ranking.Request.DescribeSubscribesByCategoryNameRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListSubscribesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzListSubscribesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Subscribe(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzSubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Subscribe(
                    new Gs2.Gs2Ranking.Request.SubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzSubscribeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzSubscribeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Unsubscribe(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzUnsubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Unsubscribe(
                    new Gs2.Gs2Ranking.Request.UnsubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzUnsubscribeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzUnsubscribeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetNearRanking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetNearRankingResult>> callback,
                string namespaceName,
                string categoryName,
                long score,
                string additionalScopeName = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeNearRankings(
                    new Gs2.Gs2Ranking.Request.DescribeNearRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAdditionalScopeName(additionalScopeName)
                        .WithScore(score),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetNearRankingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzGetNearRankingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRank(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetRankResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                string additionalScopeName = null,
                string uniqueId = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetRanking(
                    new Gs2.Gs2Ranking.Request.GetRankingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAdditionalScopeName(additionalScopeName)
                        .WithScorerUserId(scorerUserId)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithUniqueId(uniqueId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetRankResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzGetRankResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRanking(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetRankingResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string additionalScopeName = null,
                int? limit = null,
                string pageToken = null,
                long? startIndex = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeRankings(
                    new Gs2.Gs2Ranking.Request.DescribeRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAdditionalScopeName(additionalScopeName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithLimit(limit)
                        .WithPageToken(pageToken)
                        .WithStartIndex(startIndex),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetRankingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzGetRankingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PutScore(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzPutScoreResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                long score,
                string metadata = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PutScore(
                    new Gs2.Gs2Ranking.Request.PutScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScore(score)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzPutScoreResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzPutScoreResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetScore(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetScoreResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                string uniqueId = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetScore(
                    new Gs2.Gs2Ranking.Request.GetScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScorerUserId(scorerUserId)
                        .WithUniqueId(uniqueId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzGetScoreResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzGetScoreResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListScores(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListScoresResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                int? limit = null,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeScores(
                    new Gs2.Gs2Ranking.Request.DescribeScoresRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScorerUserId(scorerUserId)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithLimit(limit)
                        .WithPageToken(pageToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Ranking.Result.EzListScoresResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Ranking.Result.EzListScoresResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}