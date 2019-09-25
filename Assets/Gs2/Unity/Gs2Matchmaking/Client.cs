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
using Gs2.Gs2Matchmaking;
using Gs2.Gs2Matchmaking.Model;
using Gs2.Gs2Matchmaking.Request;
using Gs2.Gs2Matchmaking.Result;
using Gs2.Unity.Gs2Matchmaking.Model;
using Gs2.Unity.Gs2Matchmaking.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Matchmaking
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MatchmakingWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MatchmakingWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  ギャザリングを新規作成<br />
		///    <br />
		///    Player に指定する自身のプレイヤー情報のユーザIDは省略できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="player">自身のプレイヤー情報</param>
		/// <param name="attributeRanges">募集条件</param>
		/// <param name="capacityOfRoles">参加者</param>
		/// <param name="allowUserIds">参加を許可するユーザIDリスト</param>
		public IEnumerator CreateGathering(
		        UnityAction<AsyncResult<EzCreateGatheringResult>> callback,
		        GameSession session,
                string namespaceName,
                EzPlayer player,
                List<EzCapacityOfRole> capacityOfRoles,
                List<string> allowUserIds,
                List<EzAttributeRange> attributeRanges=null
        )
		{
            yield return _client.CreateGathering(
                new CreateGatheringRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPlayer(player.ToModel())
                    .WithAttributeRanges(attributeRanges != null ? attributeRanges.Select(item => item.ToModel()).ToList() : new List<AttributeRange>(new AttributeRange[]{}))
                    .WithCapacityOfRoles(capacityOfRoles != null ? capacityOfRoles.Select(item => item.ToModel()).ToList() : new List<CapacityOfRole>(new CapacityOfRole[]{}))
                    .WithAllowUserIds(allowUserIds)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzCreateGatheringResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzCreateGatheringResult>(
                                new EzCreateGatheringResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  ギャザリングの募集条件を変更<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="gatheringName">ギャザリング名</param>
		/// <param name="attributeRanges">募集条件</param>
		public IEnumerator UpdateGathering(
		        UnityAction<AsyncResult<EzUpdateGatheringResult>> callback,
		        GameSession session,
                string namespaceName,
                string gatheringName,
                List<EzAttributeRange> attributeRanges=null
        )
		{
            yield return _client.UpdateGathering(
                new UpdateGatheringRequest()
                    .WithNamespaceName(namespaceName)
                    .WithGatheringName(gatheringName)
                    .WithAttributeRanges(attributeRanges != null ? attributeRanges.Select(item => item.ToModel()).ToList() : new List<AttributeRange>(new AttributeRange[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateGatheringResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateGatheringResult>(
                                new EzUpdateGatheringResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  すでに存在する中で、自分が参加できるギャザリングを探して参加<br />
		///    <br />
		///    一定時間 検索を行い、対象が見つからなかったときには `マッチメイキングの状態を保持するトークン` を返す。<br />
		///    次回 `マッチメイキングの状態を保持するトークン` をつけて再度リクエストを出すことで、前回の続きから検索処理を再開できる。<br />
		///    すべてのギャザリングを検索したが、参加できるギャザリングが存在しなかった場合はギャザリングもトークンもどちらも `null` が応答される。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="player">自身のプレイヤー情報</param>
		/// <param name="matchmakingContextToken">検索の再開に使用する マッチメイキングの状態を保持するトークン</param>
		public IEnumerator DoMatchmaking(
		        UnityAction<AsyncResult<EzDoMatchmakingResult>> callback,
		        GameSession session,
                string namespaceName,
                EzPlayer player,
                string matchmakingContextToken=null
        )
		{
            yield return _client.DoMatchmaking(
                new DoMatchmakingRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPlayer(player.ToModel())
                    .WithMatchmakingContextToken(matchmakingContextToken)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoMatchmakingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoMatchmakingResult>(
                                new EzDoMatchmakingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  最新のギャザリングの状態を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="gatheringName">ギャザリング名</param>
		public IEnumerator GetGathering(
		        UnityAction<AsyncResult<EzGetGatheringResult>> callback,
                string namespaceName,
                string gatheringName
        )
		{
            yield return _client.GetGathering(
                new GetGatheringRequest()
                    .WithNamespaceName(namespaceName)
                    .WithGatheringName(gatheringName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetGatheringResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetGatheringResult>(
                                new EzGetGatheringResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  マッチメイキングをキャンセルし、参加中のギャザリングから離脱。<br />
		///    <br />
		///    ギャザリングから離脱する前にマッチメイキングが完了した場合は、NotFoundException(404エラー) が発生し失敗します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="gatheringName">ギャザリング名</param>
		public IEnumerator CancelMatchmaking(
		        UnityAction<AsyncResult<EzCancelMatchmakingResult>> callback,
		        GameSession session,
                string namespaceName,
                string gatheringName
        )
		{
            yield return _client.CancelMatchmaking(
                new CancelMatchmakingRequest()
                    .WithNamespaceName(namespaceName)
                    .WithGatheringName(gatheringName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzCancelMatchmakingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzCancelMatchmakingResult>(
                                new EzCancelMatchmakingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}