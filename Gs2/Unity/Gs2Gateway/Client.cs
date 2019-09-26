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
using Gs2.Gs2Gateway;
using Gs2.Gs2Gateway.Model;
using Gs2.Gs2Gateway.Request;
using Gs2.Gs2Gateway.Result;
using Gs2.Unity.Gs2Gateway.Model;
using Gs2.Unity.Gs2Gateway.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Gateway
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2GatewayWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2GatewayWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  サーバからプッシュ通知を受けるためのユーザーIDを設定<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="allowConcurrentAccess">同時に異なるクライアントからの接続を許容するか</param>
		public IEnumerator SetUserId(
		        UnityAction<AsyncResult<EzSetUserIdResult>> callback,
		        GameSession session,
                string namespaceName,
                bool allowConcurrentAccess
        )
		{
            yield return _client.SetUserId(
                new SetUserIdRequest()
                    .WithNamespaceName(namespaceName)
                    .WithAllowConcurrentAccess(allowConcurrentAccess)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzSetUserIdResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzSetUserIdResult>(
                                new EzSetUserIdResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}