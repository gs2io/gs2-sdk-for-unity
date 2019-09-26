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

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2RankingWebSocketClient(profile.Gs2Session);
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
            yield return _client.DescribeCategoryModels(
                new DescribeCategoryModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListCategoriesResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListCategoriesResult>(
                                new EzListCategoriesResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.GetCategoryModel(
                new GetCategoryModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCategoryResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCategoryResult>(
                                new EzGetCategoryResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DescribeSubscribesByCategoryName(
                new DescribeSubscribesByCategoryNameRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListSubscribesResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListSubscribesResult>(
                                new EzListSubscribesResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.Subscribe(
                new SubscribeRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithTargetUserId(targetUserId)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzSubscribeResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzSubscribeResult>(
                                new EzSubscribeResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.Unsubscribe(
                new UnsubscribeRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithTargetUserId(targetUserId)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzUnsubscribeResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzUnsubscribeResult>(
                                new EzUnsubscribeResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.PutScore(
                new PutScoreRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithScore(score)
                    .WithMetadata(metadata)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzPutScoreResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzPutScoreResult>(
                                new EzPutScoreResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
		public IEnumerator GetRanking(
		        UnityAction<AsyncResult<EzGetRankingResult>> callback,
		        GameSession session,
                string namespaceName,
                string categoryName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _client.DescribeRankings(
                new DescribeRankingsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRankingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRankingResult>(
                                new EzGetRankingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DescribeNearRankings(
                new DescribeNearRankingsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithCategoryName(categoryName)
                    .WithScore(score),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetNearRankingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetNearRankingResult>(
                                new EzGetNearRankingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}