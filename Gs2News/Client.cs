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
using Gs2.Gs2News;
using Gs2.Gs2News.Model;
using Gs2.Gs2News.Request;
using Gs2.Gs2News.Result;
using Gs2.Unity.Gs2News.Model;
using Gs2.Unity.Gs2News.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2News
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
		private readonly Gs2NewsWebSocketClient _client;
		private readonly Gs2NewsRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2NewsWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2NewsRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2NewsRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  達成したミッションの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペースの名前</param>
		public IEnumerator ListNewses(
		        UnityAction<AsyncResult<EzListNewsesResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeNews(
                    new DescribeNewsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListNewsesResult>(
                            r.Result == null ? null : new EzListNewsesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  達成したミッションの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペースの名前</param>
		public IEnumerator GetContentsUrl(
		        UnityAction<AsyncResult<EzGetContentsUrlResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.WantGrant(
                    new WantGrantRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetContentsUrlResult>(
                            r.Result == null ? null : new EzGetContentsUrlResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}