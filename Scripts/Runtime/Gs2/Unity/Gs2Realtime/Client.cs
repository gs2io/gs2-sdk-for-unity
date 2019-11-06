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
using Gs2.Gs2Realtime;
using Gs2.Gs2Realtime.Model;
using Gs2.Gs2Realtime.Request;
using Gs2.Gs2Realtime.Result;
using Gs2.Unity.Gs2Realtime.Model;
using Gs2.Unity.Gs2Realtime.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Realtime
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2RealtimeWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2RealtimeWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  ルームの情報を取得<br />
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
            yield return _client.GetRoom(
                new GetRoomRequest()
                    .WithNamespaceName(namespaceName)
                    .WithRoomName(roomName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRoomResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRoomResult>(
                                new EzGetRoomResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}