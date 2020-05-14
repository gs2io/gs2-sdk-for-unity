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
using Gs2.Gs2Schedule;
using Gs2.Gs2Schedule.Model;
using Gs2.Gs2Schedule.Request;
using Gs2.Gs2Schedule.Result;
using Gs2.Unity.Gs2Schedule.Model;
using Gs2.Unity.Gs2Schedule.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Schedule
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2ScheduleWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ScheduleWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  引かれているトリガーの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListTriggers(
		        UnityAction<AsyncResult<EzListTriggersResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DescribeTriggers(
                    new DescribeTriggersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListTriggersResult>(
                            r.Result == null ? null : new EzListTriggersResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  引かれているトリガーを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="triggerName">トリガーの名前</param>
		public IEnumerator GetTrigger(
		        UnityAction<AsyncResult<EzGetTriggerResult>> callback,
		        GameSession session,
                string namespaceName,
                string triggerName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetTrigger(
                    new GetTriggerRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTriggerName(triggerName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetTriggerResult>(
                            r.Result == null ? null : new EzGetTriggerResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  開催中のイベント一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListEvents(
		        UnityAction<AsyncResult<EzListEventsResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DescribeEvents(
                    new DescribeEventsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListEventsResult>(
                            r.Result == null ? null : new EzListEventsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  開催中のイベントを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="eventName">イベントの種類名</param>
		public IEnumerator GetEvent(
		        UnityAction<AsyncResult<EzGetEventResult>> callback,
		        GameSession session,
                string namespaceName,
                string eventName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetEvent(
                    new GetEventRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEventName(eventName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetEventResult>(
                            r.Result == null ? null : new EzGetEventResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}