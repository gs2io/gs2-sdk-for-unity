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
using Gs2.Gs2Friend;
using Gs2.Gs2Friend.Model;
using Gs2.Gs2Friend.Request;
using Gs2.Gs2Friend.Result;
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Friend
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	public partial class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2FriendWebSocketClient _client;
		private readonly Gs2FriendRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2FriendWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2FriendRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2FriendRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  自分のプロフィールを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator GetProfile(
		        UnityAction<AsyncResult<EzGetProfileResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetProfile(
                    new GetProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetProfileResult>(
                            r.Result == null ? null : new EzGetProfileResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  自分のプロフィールを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="publicProfile">公開されるプロフィール</param>
		/// <param name="followerProfile">フォロワー向けに公開されるプロフィール</param>
		/// <param name="friendProfile">フレンド向けに公開されるプロフィール</param>
		public IEnumerator UpdateProfile(
		        UnityAction<AsyncResult<EzUpdateProfileResult>> callback,
		        GameSession session,
                string namespaceName,
                string publicProfile=null,
                string followerProfile=null,
                string friendProfile=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateProfile(
                    new UpdateProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPublicProfile(publicProfile)
                        .WithFollowerProfile(followerProfile)
                        .WithFriendProfile(friendProfile)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUpdateProfileResult>(
                            r.Result == null ? null : new EzUpdateProfileResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  他人の公開プロフィールを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="userId">ユーザーID</param>
		public IEnumerator GetPublicProfile(
		        UnityAction<AsyncResult<EzGetPublicProfileResult>> callback,
                string namespaceName,
                string userId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetPublicProfile(
                    new GetPublicProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithUserId(userId),
                    r => cb.Invoke(
                        new AsyncResult<EzGetPublicProfileResult>(
                            r.Result == null ? null : new EzGetPublicProfileResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  他プレイヤーをフォローする<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="withProfile">プロフィールも一緒に取得するか</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator DescribeFollowUsers(
		        UnityAction<AsyncResult<EzDescribeFollowUsersResult>> callback,
		        GameSession session,
                string namespaceName,
                bool withProfile,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeFollows(
                    new DescribeFollowsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithWithProfile(withProfile)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDescribeFollowUsersResult>(
                            r.Result == null ? null : new EzDescribeFollowUsersResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  他プレイヤーをフォローする<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">フォローされるユーザID</param>
		public IEnumerator Follow(
		        UnityAction<AsyncResult<EzFollowResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Follow(
                    new FollowRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzFollowResult>(
                            r.Result == null ? null : new EzFollowResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォローしている相手をアンフォローする<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">フォローされているユーザID</param>
		public IEnumerator Unfollow(
		        UnityAction<AsyncResult<EzUnfollowResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Unfollow(
                    new UnfollowRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUnfollowResult>(
                            r.Result == null ? null : new EzUnfollowResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フレンドの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="withProfile">プロフィールも一緒に取得するか</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator DescribeFriends(
		        UnityAction<AsyncResult<EzDescribeFriendsResult>> callback,
		        GameSession session,
                string namespaceName,
                bool withProfile,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeFriends(
                    new DescribeFriendsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithWithProfile(withProfile)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDescribeFriendsResult>(
                            r.Result == null ? null : new EzDescribeFriendsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フレンドを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">ユーザーID</param>
		/// <param name="withProfile">プロフィールも一緒に取得するか</param>
		public IEnumerator GetFriend(
		        UnityAction<AsyncResult<EzGetFriendResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId,
                bool withProfile
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetFriend(
                    new GetFriendRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithWithProfile(withProfile)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetFriendResult>(
                            r.Result == null ? null : new EzGetFriendResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フレンドを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">ユーザーID</param>
		public IEnumerator DeleteFriend(
		        UnityAction<AsyncResult<EzDeleteFriendResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteFriend(
                    new DeleteFriendRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteFriendResult>(
                            r.Result == null ? null : new EzDeleteFriendResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  送信したフレンドリクエストの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator DescribeSendRequests(
		        UnityAction<AsyncResult<EzDescribeSendRequestsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSendRequests(
                    new DescribeSendRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDescribeSendRequestsResult>(
                            r.Result == null ? null : new EzDescribeSendRequestsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  <br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">リクエストの送信先ユーザID</param>
		public IEnumerator SendRequest(
		        UnityAction<AsyncResult<EzSendRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.SendRequest(
                    new SendRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzSendRequestResult>(
                            r.Result == null ? null : new EzSendRequestResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  <br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">リクエストの送信先ユーザID</param>
		public IEnumerator DeleteRequest(
		        UnityAction<AsyncResult<EzDeleteRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteRequest(
                    new DeleteRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteRequestResult>(
                            r.Result == null ? null : new EzDeleteRequestResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  受信したフレンドリクエスト一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator DescribeReceiveRequests(
		        UnityAction<AsyncResult<EzDescribeReceiveRequestsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeReceiveRequests(
                    new DescribeReceiveRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDescribeReceiveRequestsResult>(
                            r.Result == null ? null : new EzDescribeReceiveRequestsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フレンドリクエストを承認<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="fromUserId">フレンドリクエストを送信したユーザID</param>
		public IEnumerator Accept(
		        UnityAction<AsyncResult<EzAcceptResult>> callback,
		        GameSession session,
                string namespaceName,
                string fromUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.AcceptRequest(
                    new AcceptRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithFromUserId(fromUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzAcceptResult>(
                            r.Result == null ? null : new EzAcceptResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フレンドリクエストを拒否<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="fromUserId">フレンドリクエストを送信したユーザID</param>
		public IEnumerator Reject(
		        UnityAction<AsyncResult<EzRejectResult>> callback,
		        GameSession session,
                string namespaceName,
                string fromUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.RejectRequest(
                    new RejectRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithFromUserId(fromUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzRejectResult>(
                            r.Result == null ? null : new EzRejectResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ブラックリストを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator GetBlackList(
		        UnityAction<AsyncResult<EzGetBlackListResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeBlackList(
                    new DescribeBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetBlackListResult>(
                            r.Result == null ? null : new EzGetBlackListResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ブラックリストにユーザを登録<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">None</param>
		public IEnumerator RegisterBlackList(
		        UnityAction<AsyncResult<EzRegisterBlackListResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.RegisterBlackList(
                    new RegisterBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzRegisterBlackListResult>(
                            r.Result == null ? null : new EzRegisterBlackListResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ブラックリストからユーザを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetUserId">None</param>
		public IEnumerator UnregisterBlackList(
		        UnityAction<AsyncResult<EzUnregisterBlackListResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UnregisterBlackList(
                    new UnregisterBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUnregisterBlackListResult>(
                            r.Result == null ? null : new EzUnregisterBlackListResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}