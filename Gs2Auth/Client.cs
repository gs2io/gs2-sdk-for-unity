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
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Auth
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
		private readonly Gs2AuthWebSocketClient _client;
		private readonly Gs2AuthRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2AuthWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2AuthRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2AuthRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  指定したユーザIDでGS2にログイン<br />
		///    <br />
		///    body と signature には GS2-Account::Authentication の結果を指定します。<br />
		///    body と signature の検証に成功すると、 `アクセストークン` を応答します。<br />
		///    `アクセストークン` は有効期限が1時間の一時的な認証情報で、GS2内の各サービスでゲームプレイヤーを識別するために使用されます。<br />
		///    なおUnityとCocos2d-x向けにGS2-Account::AuthenticationとこのAPIをひとまとめにしたGS2-Profile::Loginを用意しています。<br />
		///    GS2-Profile::Loginははじめかた⇒サンプルプログラムで解説しています。<br />
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
            yield return _profile.Run(
                callback,
                null,
                cb => _client.LoginBySignature(
                    new LoginBySignatureRequest()
                        .WithUserId(userId)
                        .WithKeyId(keyId)
                        .WithBody(body)
                        .WithSignature(signature),
                    r => cb.Invoke(
                        new AsyncResult<EzLoginResult>(
                            r.Result == null ? null : new EzLoginResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}