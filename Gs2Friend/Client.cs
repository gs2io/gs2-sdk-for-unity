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

using Gs2.Gs2Friend;
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.Result;
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
namespace Gs2.Unity.Gs2Friend
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

        public IEnumerator GetProfile(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetProfileResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetProfile(
                    new Gs2.Gs2Friend.Request.GetProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetProfileResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetProfileResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetPublicProfile(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetPublicProfileResult>> callback,
                string namespaceName,
                string userId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetPublicProfile(
                    new Gs2.Gs2Friend.Request.GetPublicProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithUserId(userId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetPublicProfileResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetPublicProfileResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateProfile(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUpdateProfileResult>> callback,
		        GameSession session,
                string namespaceName,
                string publicProfile = null,
                string followerProfile = null,
                string friendProfile = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateProfile(
                    new Gs2.Gs2Friend.Request.UpdateProfileRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPublicProfile(publicProfile)
                        .WithFollowerProfile(followerProfile)
                        .WithFriendProfile(friendProfile),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUpdateProfileResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzUpdateProfileResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeFollowUsers(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeFollowUsersResult>> callback,
		        GameSession session,
                string namespaceName,
                bool? withProfile = null,
                int? limit = null,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeFollows(
                    new Gs2.Gs2Friend.Request.DescribeFollowsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithWithProfile(withProfile)
                        .WithLimit(limit)
                        .WithPageToken(pageToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeFollowUsersResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDescribeFollowUsersResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Follow(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzFollowResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Follow(
                    new Gs2.Gs2Friend.Request.FollowRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzFollowResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzFollowResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetFollowUser(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetFollowUserResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId,
                bool? withProfile = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetFollow(
                    new Gs2.Gs2Friend.Request.GetFollowRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithWithProfile(withProfile)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetFollowUserResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetFollowUserResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Unfollow(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUnfollowResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Unfollow(
                    new Gs2.Gs2Friend.Request.UnfollowRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUnfollowResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzUnfollowResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteFriend(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDeleteFriendResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteFriend(
                    new Gs2.Gs2Friend.Request.DeleteFriendRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDeleteFriendResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDeleteFriendResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeFriends(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeFriendsResult>> callback,
		        GameSession session,
                string namespaceName,
                bool? withProfile = null,
                int? limit = null,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeFriends(
                    new Gs2.Gs2Friend.Request.DescribeFriendsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithWithProfile(withProfile)
                        .WithLimit(limit)
                        .WithPageToken(pageToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeFriendsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDescribeFriendsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetFriend(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetFriendResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId,
                bool? withProfile = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetFriend(
                    new Gs2.Gs2Friend.Request.GetFriendRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId)
                        .WithWithProfile(withProfile),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetFriendResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetFriendResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDeleteRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteRequest(
                    new Gs2.Gs2Friend.Request.DeleteRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDeleteRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDeleteRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeSendRequests(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeSendRequestsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSendRequests(
                    new Gs2.Gs2Friend.Request.DescribeSendRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeSendRequestsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDescribeSendRequestsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSendRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetSendRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetSendRequest(
                    new Gs2.Gs2Friend.Request.GetSendRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetSendRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetSendRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SendRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzSendRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.SendRequest(
                    new Gs2.Gs2Friend.Request.SendRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzSendRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzSendRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Accept(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzAcceptResult>> callback,
		        GameSession session,
                string namespaceName,
                string fromUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.AcceptRequest(
                    new Gs2.Gs2Friend.Request.AcceptRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzAcceptResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzAcceptResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeReceiveRequests(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeReceiveRequestsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeReceiveRequests(
                    new Gs2.Gs2Friend.Request.DescribeReceiveRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzDescribeReceiveRequestsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzDescribeReceiveRequestsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetReceiveRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetReceiveRequestResult>> callback,
		        GameSession session,
                string namespaceName,
                string fromUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetReceiveRequest(
                    new Gs2.Gs2Friend.Request.GetReceiveRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetReceiveRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetReceiveRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Reject(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzRejectResult>> callback,
		        GameSession session,
                string namespaceName,
                string fromUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.RejectRequest(
                    new Gs2.Gs2Friend.Request.RejectRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzRejectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzRejectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBlackList(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetBlackListResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeBlackList(
                    new Gs2.Gs2Friend.Request.DescribeBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzGetBlackListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzGetBlackListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RegisterBlackList(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzRegisterBlackListResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.RegisterBlackList(
                    new Gs2.Gs2Friend.Request.RegisterBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzRegisterBlackListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzRegisterBlackListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UnregisterBlackList(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUnregisterBlackListResult>> callback,
		        GameSession session,
                string namespaceName,
                string targetUserId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.UnregisterBlackList(
                    new Gs2.Gs2Friend.Request.UnregisterBlackListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Friend.Result.EzUnregisterBlackListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Friend.Result.EzUnregisterBlackListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}