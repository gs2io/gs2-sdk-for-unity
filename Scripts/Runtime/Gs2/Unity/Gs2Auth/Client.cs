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
using Gs2.Gs2Auth;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Auth.Request;
using Gs2.Gs2Auth.Result;
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Gs2Auth.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Auth
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2AuthWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2AuthWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  指定したユーザIDでGS2にログイン<br />
		///    <br />
		///    body と signature には GS2-Account::Authentication の結果を指定します。<br />
		///    body と signature の検証に成功すると、 `アクセストークン` を応答します。<br />
		///    `アクセストークン` は有効期限が1時間の一時的な認証情報で、GS2内の各サービスでゲームプレイヤーを識別するために使用されます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="userId">ユーザーID</param>
		/// <param name="keyId">署名の作成に使用した暗号鍵 のGRN</param>
		/// <param name="body">アカウント認証情報の署名対象</param>
		/// <param name="signature">署名</param>
		public IEnumerator Login(
		        UnityAction<AsyncResult<EzLoginResult>> callback,
                string userId,
                string keyId,
                string body,
                string signature
        )
		{
            yield return _client.LoginBySignature(
                new LoginBySignatureRequest()
                    .WithUserId(userId)
                    .WithKeyId(keyId)
                    .WithBody(body)
                    .WithSignature(signature),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzLoginResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzLoginResult>(
                                new EzLoginResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}