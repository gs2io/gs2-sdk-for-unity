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
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Matchmaking
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MatchmakingWebSocketClient _client;
		private readonly Gs2MatchmakingRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MatchmakingWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2MatchmakingRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2MatchmakingRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  ギャザリングを新規作成<br />
		///    <br />
		///    Player に指定する自身のプレイヤー情報のユーザIDは省略できます。<br />
		///    expiresAtを指定することでギャザリングの有効期限を設定することができます。<br />
		///    有効期限を用いない場合、古いギャザリングが残り続けマッチングが成立したときには、<br />
		///    ユーザーがゲームから離脱している可能性があります。<br />
		///    有効期限を用いる場合は、有効期限が来るたびにユーザーにギャザリングの再作成を促す仕組みにしてください。<br />
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
		/// <param name="expiresAt">ギャザリングの有効期限</param>
		public IEnumerator CreateGathering(
		        UnityAction<AsyncResult<EzCreateGatheringResult>> callback,
		        GameSession session,
                string namespaceName,
                EzPlayer player,
                List<EzCapacityOfRole> capacityOfRoles,
                List<string> allowUserIds,
                List<EzAttributeRange> attributeRanges=null,
                long? expiresAt=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.CreateGathering(
                    new CreateGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPlayer(player.ToModel())
                        .WithAttributeRanges(attributeRanges != null ? attributeRanges.Select(item => item?.ToModel()).ToList() : new List<AttributeRange>(new AttributeRange[]{}))
                        .WithCapacityOfRoles(capacityOfRoles != null ? capacityOfRoles.Select(item => item?.ToModel()).ToList() : new List<CapacityOfRole>(new CapacityOfRole[]{}))
                        .WithAllowUserIds(allowUserIds)
                        .WithExpiresAt(expiresAt)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCreateGatheringResult>(
                            r.Result == null ? null : new EzCreateGatheringResult(r.Result),
                            r.Error
                        )
                    )
                )
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
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateGathering(
                    new UpdateGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName)
                        .WithAttributeRanges(attributeRanges != null ? attributeRanges.Select(item => item?.ToModel()).ToList() : new List<AttributeRange>(new AttributeRange[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUpdateGatheringResult>(
                            r.Result == null ? null : new EzUpdateGatheringResult(r.Result),
                            r.Error
                        )
                    )
                )
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
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DoMatchmaking(
                    new DoMatchmakingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPlayer(player.ToModel())
                        .WithMatchmakingContextToken(matchmakingContextToken)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDoMatchmakingResult>(
                            r.Result == null ? null : new EzDoMatchmakingResult(r.Result),
                            r.Error
                        )
                    )
                )
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
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetGathering(
                    new GetGatheringRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetGatheringResult>(
                            r.Result == null ? null : new EzGetGatheringResult(r.Result),
                            r.Error
                        )
                    )
                )
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
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.CancelMatchmaking(
                    new CancelMatchmakingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGatheringName(gatheringName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCancelMatchmakingResult>(
                            r.Result == null ? null : new EzCancelMatchmakingResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  レーティングモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListRatingModels(
		        UnityAction<AsyncResult<EzListRatingModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeRatingModels(
                    new DescribeRatingModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListRatingModelsResult>(
                            r.Result == null ? null : new EzListRatingModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  レーティング名を指定してレーティングモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="ratingName">レーティングの種類名</param>
		public IEnumerator GetRatingModel(
		        UnityAction<AsyncResult<EzGetRatingModelResult>> callback,
                string namespaceName,
                string ratingName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRatingModel(
                    new GetRatingModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRatingName(ratingName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRatingModelResult>(
                            r.Result == null ? null : new EzGetRatingModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  レーティング名を指定してレーティングを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListRatings(
		        UnityAction<AsyncResult<EzListRatingsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeRatings(
                    new DescribeRatingsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListRatingsResult>(
                            r.Result == null ? null : new EzListRatingsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  投票用紙を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="ratingName">レーティング名</param>
		public IEnumerator GetRating(
		        UnityAction<AsyncResult<EzGetRatingResult>> callback,
		        GameSession session,
                string namespaceName,
                string ratingName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetRating(
                    new GetRatingRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRatingName(ratingName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRatingResult>(
                            r.Result == null ? null : new EzGetRatingResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  投票用紙を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="ratingName">レーティング名</param>
		/// <param name="gatheringName">投票対象のギャザリング名</param>
		public IEnumerator CreateVote(
		        UnityAction<AsyncResult<EzCreateVoteResult>> callback,
		        GameSession session,
                string namespaceName,
                string ratingName,
                string gatheringName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetBallot(
                    new GetBallotRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRatingName(ratingName)
                        .WithGatheringName(gatheringName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCreateVoteResult>(
                            r.Result == null ? null : new EzCreateVoteResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  対戦結果を投票します。<br />
		///    <br />
		///    投票は最初の投票が行われてから5分以内に行う必要があります。<br />
		///    つまり、結果は即座に反映されず、投票開始からおよそ5分後または全てのプレイヤーが投票を行った際に結果が反映されます。<br />
		///    5分以内に全ての投票用紙を回収できなかった場合はその時点の投票内容で多数決をとって結果を決定します。<br />
		///    各結果の投票数が同一だった場合は結果は捨てられます（スクリプトで挙動を変更可）。<br />
		///    <br />
		///    結果を即座に反映したい場合は、勝利した側の代表プレイヤーが投票用紙を各プレイヤーから集めて voteMultiple を呼び出すことで結果を即座に反映できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="ballotBody">投票用紙の署名対象のデータ</param>
		/// <param name="ballotSignature">投票用紙の署名</param>
		/// <param name="gameResults">投票内容。対戦を行ったプレイヤーグループ1に所属するユーザIDのリスト</param>
		public IEnumerator Vote(
		        UnityAction<AsyncResult<EzVoteResult>> callback,
                string namespaceName,
                string ballotBody,
                string ballotSignature,
                List<EzGameResult> gameResults
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.Vote(
                    new VoteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults != null ? gameResults.Select(item => item?.ToModel()).ToList() : new List<GameResult>(new GameResult[]{})),
                    r => cb.Invoke(
                        new AsyncResult<EzVoteResult>(
                            r.Result == null ? null : new EzVoteResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  対戦結果をまとめて投票します。<br />
		///    <br />
		///    ゲームに勝利した側が他プレイヤーの投票用紙を集めてまとめて投票するのに使用します。<br />
		///    『勝利した側』としているのは、敗北した側が自分たちが勝ったことにして報告することにインセンティブはありますが、その逆はないためです。<br />
		///    負けた側が投票用紙を渡してこない可能性がありますが、その場合も過半数の投票用紙があれば結果を通すことができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="signedBallots">署名付の投票用紙リスト</param>
		/// <param name="gameResults">投票内容。対戦を行ったプレイヤーグループ1に所属するユーザIDのリスト</param>
		public IEnumerator VoteMultiple(
		        UnityAction<AsyncResult<EzVoteMultipleResult>> callback,
                string namespaceName,
                List<EzSignedBallot> signedBallots,
                List<EzGameResult> gameResults
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.VoteMultiple(
                    new VoteMultipleRequest()
                        .WithNamespaceName(namespaceName)
                        .WithSignedBallots(signedBallots != null ? signedBallots.Select(item => item?.ToModel()).ToList() : new List<SignedBallot>(new SignedBallot[]{}))
                        .WithGameResults(gameResults != null ? gameResults.Select(item => item?.ToModel()).ToList() : new List<GameResult>(new GameResult[]{})),
                    r => cb.Invoke(
                        new AsyncResult<EzVoteMultipleResult>(
                            r.Result == null ? null : new EzVoteMultipleResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}