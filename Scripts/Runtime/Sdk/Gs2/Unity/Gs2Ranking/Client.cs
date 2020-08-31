/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0(the "License").
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Gs2Ranking;
using Gs2.Gs2Ranking.Model;
using Gs2.Gs2Ranking.Request;
using Gs2.Gs2Ranking.Result;
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Gs2Ranking.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Ranking
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2RankingWebSocketClient _client;
		private readonly Gs2RankingRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2RankingWebSocketClient(profile.Gs2Session);
			_restClient = new Gs2RankingRestClient(profile.Gs2RestSession);
		}

		/// <summary>
		///  カテゴリの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListCategories(
		        UnityAction<AsyncResult<EzListCategoriesResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeCategoryModels(
                    new DescribeCategoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListCategoriesResult>(
                            r.Result == null ? null : new EzListCategoriesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  カテゴリの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリモデル名</param>
		public IEnumerator GetCategory(
		        UnityAction<AsyncResult<EzGetCategoryResult>> callback,
                string namespaceName,
                string categoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetCategoryModel(
                    new GetCategoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetCategoryResult>(
                            r.Result == null ? null : new EzGetCategoryResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  購読しているユーザIDの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		public IEnumerator ListSubscribes(
		        UnityAction<AsyncResult<EzListSubscribesResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribesByCategoryName(
                    new DescribeSubscribesByCategoryNameRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListSubscribesResult>(
                            r.Result == null ? null : new EzListSubscribesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ユーザIDを購読<br />
		///    <br />
		///    ユーザIDを購読することで、そのユーザIDに関する新着メッセージ投稿の通知を受けることができます<br />
		///    購読する際のオプションとして、「メッセージに付加されたカテゴリが特定の値のものだけ通知する」といった設定や<br />
		///    「通知を受けたときにオフラインだった場合、モバイルプッシュ通知に転送する」といった設定ができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="targetUserId">購読されるユーザID</param>
		public IEnumerator Subscribe(
		        UnityAction<AsyncResult<EzSubscribeResult>> callback,
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
                    new SubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzSubscribeResult>(
                            r.Result == null ? null : new EzSubscribeResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  購読の解除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="targetUserId">購読されるユーザID</param>
		public IEnumerator Unsubscribe(
		        UnityAction<AsyncResult<EzUnsubscribeResult>> callback,
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
                    new UnsubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUnsubscribeResult>(
                            r.Result == null ? null : new EzUnsubscribeResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ゲームプレイヤーが登録したスコアの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="scorerUserId">スコアを獲得したユーザID</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListScores(
		        UnityAction<AsyncResult<EzListScoresResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeScores(
                    new DescribeScoresRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScorerUserId(scorerUserId)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListScoresResult>(
                            r.Result == null ? null : new EzListScoresResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ゲームプレイヤーが登録したスコアを取得<br />
		///    <br />
		///    ユーザID毎にスコアを1つしか登録できないカテゴリを指定する場合、ユニークIDは省略可能です<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="scorerUserId">スコアを獲得したユーザID</param>
		/// <param name="uniqueId">スコアのユニークID</param>
		public IEnumerator GetScore(
		        UnityAction<AsyncResult<EzGetScoreResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                string uniqueId=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetScore(
                    new GetScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScorerUserId(scorerUserId)
                        .WithUniqueId(uniqueId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetScoreResult>(
                            r.Result == null ? null : new EzGetScoreResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ランキングの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="score">スコア</param>
		/// <param name="metadata">メタデータ</param>
		public IEnumerator PutScore(
		        UnityAction<AsyncResult<EzPutScoreResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                long score,
                string metadata=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PutScore(
                    new PutScoreRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScore(score)
                        .WithMetadata(metadata)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPutScoreResult>(
                            r.Result == null ? null : new EzPutScoreResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ランキングの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		/// <param name="startIndex">ランキングの取得を開始するインデックス</param>
		public IEnumerator GetRanking(
		        UnityAction<AsyncResult<EzGetRankingResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string pageToken=null,
                long? limit=null,
                long? startIndex=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeRankings(
                    new DescribeRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithStartIndex(startIndex)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRankingResult>(
                            r.Result == null ? null : new EzGetRankingResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ランキングの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="score">スコア</param>
		public IEnumerator GetNearRanking(
		        UnityAction<AsyncResult<EzGetNearRankingResult>> callback,
                string namespaceName,
                string categoryName,
                long score
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeNearRankings(
                    new DescribeNearRankingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScore(score),
                    r => cb.Invoke(
                        new AsyncResult<EzGetNearRankingResult>(
                            r.Result == null ? null : new EzGetNearRankingResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ランキングを取得<br />
		///    <br />
		///    ユーザID毎にスコアを1つしか登録できないカテゴリを指定する場合、ユニークIDは省略可能です<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="categoryName">カテゴリ名</param>
		/// <param name="scorerUserId">スコアを獲得したユーザID</param>
		/// <param name="uniqueId">スコアのユニークID</param>
		public IEnumerator GetRank(
		        UnityAction<AsyncResult<EzGetRankResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string scorerUserId,
                string uniqueId=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetRanking(
                    new GetRankingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCategoryName(categoryName)
                        .WithScorerUserId(scorerUserId)
                        .WithUniqueId(uniqueId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRankResult>(
                            r.Result == null ? null : new EzGetRankResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}