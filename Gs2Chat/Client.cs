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
using Gs2.Gs2Chat;
using Gs2.Gs2Chat.Model;
using Gs2.Gs2Chat.Request;
using Gs2.Gs2Chat.Result;
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Gs2Chat.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Chat
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

		/// <summary>
		///  ルームの作成<br />
		///    <br />
		///    ネームスペースの設定でゲームプレイヤーによるルーム作成が許可されていない場合、失敗します。<br />
		///    ルームにパスワードを設定すると発言する際にパスワードが一致しなければ発言できません。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="name">ルーム名</param>
		/// <param name="metadata">メタデータ</param>
		/// <param name="password">メッセージを投稿するために必要となるパスワード</param>
		/// <param name="whiteListUserIds">ルームに参加可能なユーザIDリスト</param>
		public IEnumerator CreateRoom(
		        UnityAction<AsyncResult<EzCreateRoomResult>> callback,
		        GameSession session,
                string namespaceName,
                string name=null,
                string metadata=null,
                string password=null,
                List<string> whiteListUserIds=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.CreateRoom(
                    new CreateRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithName(name)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCreateRoomResult>(
                            r.Result == null ? null : new EzCreateRoomResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ルームの取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		public IEnumerator GetRoom(
		        UnityAction<AsyncResult<EzGetRoomResult>> callback,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRoom(
                    new GetRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRoomResult>(
                            r.Result == null ? null : new EzGetRoomResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ルームの削除<br />
		///    <br />
		///    自分が作成したルームに対してしか実行できません。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		public IEnumerator DeleteRoom(
		        UnityAction<AsyncResult<EzDeleteRoomResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteRoom(
                    new DeleteRoomRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteRoomResult>(
                            r.Result == null ? null : new EzDeleteRoomResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  メッセージを投稿します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		/// <param name="category">メッセージの種類を分類したい時の種類番号</param>
		/// <param name="metadata">メタデータ</param>
		/// <param name="password">メッセージを投稿するために必要となるパスワード</param>
		public IEnumerator Post(
		        UnityAction<AsyncResult<EzPostResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                int category,
                string metadata,
                string password=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Post(
                    new PostRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithCategory(category)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPostResult>(
                            r.Result == null ? null : new EzPostResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ルーム内のメッセージ一覧を取得<br />
		///    <br />
		///    startAt にしていた時刻以降に投稿されたメッセージを取得できます<br />
		///    startAt と完全一致するメッセージも対象に含まれます<br />
		///    <br />
		///    メッセージは投稿の古いものから順番に取得されます<br />
		///    <br />
		///    メッセージは過去1時間分まで遡れます<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		/// <param name="startAt">メッセージの取得を開始する時間</param>
		/// <param name="limit">データの取得件数</param>
		/// <param name="password">メッセージを投稿するために必要となるパスワード</param>
		public IEnumerator ListMessages(
		        UnityAction<AsyncResult<EzListMessagesResult>> callback,
                string namespaceName,
                string roomName,
                long? startAt=null,
                long? limit=null,
                string password=null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeMessages(
                    new DescribeMessagesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithStartAt(startAt)
                        .WithLimit(limit)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<EzListMessagesResult>(
                            r.Result == null ? null : new EzListMessagesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  購読しているルームの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListSubscribeRooms(
		        UnityAction<AsyncResult<EzListSubscribeRoomsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscribes(
                    new DescribeSubscribesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListSubscribeRoomsResult>(
                            r.Result == null ? null : new EzListSubscribeRoomsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ルームを購読<br />
		///    <br />
		///    ルームを購読することで、そのルームに関する新着メッセージ投稿の通知を受けることができます<br />
		///    購読する際のオプションとして、「メッセージに付加されたカテゴリが特定の値のものだけ通知する」といった設定や<br />
		///    「通知を受けたときにオフラインだった場合、モバイルプッシュ通知に転送する」といった設定ができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		/// <param name="notificationTypes">新着メッセージ通知を受け取るカテゴリリスト</param>
		public IEnumerator Subscribe(
		        UnityAction<AsyncResult<EzSubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                List<EzNotificationType> notificationTypes=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Subscribe(
                    new SubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithNotificationTypes(notificationTypes != null ? notificationTypes.Select(item => item?.ToModel()).ToList() : new List<NotificationType>(new NotificationType[]{}))
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
		///  購読設定の更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="roomName">ルーム名</param>
		/// <param name="notificationTypes">新着メッセージ通知を受け取るカテゴリリスト</param>
		public IEnumerator UpdateSubscribeSetting(
		        UnityAction<AsyncResult<EzUpdateSubscribeSettingResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName,
                List<EzNotificationType> notificationTypes=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateNotificationType(
                    new UpdateNotificationTypeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
                        .WithNotificationTypes(notificationTypes != null ? notificationTypes.Select(item => item?.ToModel()).ToList() : new List<NotificationType>(new NotificationType[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUpdateSubscribeSettingResult>(
                            r.Result == null ? null : new EzUpdateSubscribeSettingResult(r.Result),
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
		/// <param name="roomName">ルーム名</param>
		public IEnumerator Unsubscribe(
		        UnityAction<AsyncResult<EzUnsubscribeResult>> callback,
		        GameSession session,
                string namespaceName,
                string roomName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Unsubscribe(
                    new UnsubscribeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRoomName(roomName)
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
	}
}