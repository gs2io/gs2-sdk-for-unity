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

using Gs2.Gs2Chat;
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.Result;
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
namespace Gs2.Unity.Gs2Chat
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
		private readonly Gs2ChatWebSocketClient _client;
		private readonly Gs2ChatRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ChatWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2ChatRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2ChatRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator CreateRoom(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzCreateRoomResult>> callback,
		        GameSession session,
                string namespaceName,
                string name = null,
                string metadata = null,
                string password = null,
                List<string> whiteListUserIds = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.CreateRoom(
                    new Gs2.Gs2Chat.Request.CreateRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithName(name)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzCreateRoomResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzCreateRoomResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteRoom(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzDeleteRoomResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DeleteRoom(
                    new Gs2.Gs2Chat.Request.DeleteRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzDeleteRoomResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzDeleteRoomResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRoom(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzGetRoomResult>> callback,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetRoom(
                    new Gs2.Gs2Chat.Request.GetRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzGetRoomResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzGetRoomResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMessages(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzListMessagesResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                int limit,
                long? startAt = null,
                string password = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeMessages(
                    new Gs2.Gs2Chat.Request.DescribeMessagesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithStartAt(startAt)
                        .WithLimit(limit)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzListMessagesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzListMessagesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Post(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzPostResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                int category,
                string metadata,
                string password = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Post(
                    new Gs2.Gs2Chat.Request.PostRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithCategory(category)
                        .WithMetadata(metadata)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzPostResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzPostResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscribeRooms(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzListSubscribeRoomsResult>> callback,
		        GameSession session,
                string namespaceName,
                int limit,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribes(
                    new Gs2.Gs2Chat.Request.DescribeSubscribesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzListSubscribeRoomsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzListSubscribeRoomsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Subscribe(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzSubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                List<Gs2.Unity.Gs2Chat.Model.EzNotificationType> notificationTypes = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Subscribe(
                    new Gs2.Gs2Chat.Request.SubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithNotificationTypes(notificationTypes?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzSubscribeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzSubscribeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Unsubscribe(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzUnsubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Unsubscribe(
                    new Gs2.Gs2Chat.Request.UnsubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzUnsubscribeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzUnsubscribeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateSubscribeSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Chat.Result.EzUpdateSubscribeSettingResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                List<Gs2.Unity.Gs2Chat.Model.EzNotificationType> notificationTypes = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateNotificationType(
                    new Gs2.Gs2Chat.Request.UpdateNotificationTypeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithNotificationTypes(notificationTypes?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Chat.Result.EzUpdateSubscribeSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Chat.Result.EzUpdateSubscribeSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}