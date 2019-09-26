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
using Gs2.Gs2Inbox;
using Gs2.Gs2Inbox.Model;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Inbox
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2InboxWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2InboxWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  プレゼントボックス に届いているメッセージの一覧を取得<br />
		///    <br />
		///    メッセージは最新のメッセージから順番に取得できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">プレゼントボックス名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator List(
		        UnityAction<AsyncResult<EzListResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _client.DescribeMessages(
                new DescribeMessagesRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListResult>(
                                new EzListResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  メッセージを既読にする<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">プレゼントボックス名</param>
		/// <param name="messageName">メッセージID</param>
		public IEnumerator Read(
		        UnityAction<AsyncResult<EzReadResult>> callback,
		        GameSession session,
                string namespaceName,
                string messageName=null
        )
		{
            yield return _client.ReadMessage(
                new ReadMessageRequest()
                    .WithNamespaceName(namespaceName)
                    .WithMessageName(messageName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzReadResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzReadResult>(
                                new EzReadResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  メッセージを削除する<br />
		///    <br />
		///    プレゼントボックスの設定でメッセージを開封したときに自動的に削除するオプションを付けていない場合は、このAPIを使用して明示的にメッセージを削除する必要があります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">プレゼントボックス名</param>
		/// <param name="messageName">メッセージID</param>
		public IEnumerator Delete(
		        UnityAction<AsyncResult<EzDeleteResult>> callback,
		        GameSession session,
                string namespaceName,
                string messageName
        )
		{
            yield return _client.DeleteMessage(
                new DeleteMessageRequest()
                    .WithNamespaceName(namespaceName)
                    .WithMessageName(messageName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteResult>(
                                new EzDeleteResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}