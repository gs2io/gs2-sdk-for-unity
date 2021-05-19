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
using Gs2.Gs2Exchange;
using Gs2.Gs2Exchange.Model;
using Gs2.Gs2Exchange.Request;
using Gs2.Gs2Exchange.Result;
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Exchange.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Exchange
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
		private readonly Gs2ExchangeWebSocketClient _client;
		private readonly Gs2ExchangeRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ExchangeWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2ExchangeRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2ExchangeRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  交換レートモデル情報の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListRateModels(
		        UnityAction<AsyncResult<EzListRateModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeRateModels(
                    new DescribeRateModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListRateModelsResult>(
                            r.Result == null ? null : new EzListRateModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換レートモデル情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		public IEnumerator GetRateModel(
		        UnityAction<AsyncResult<EzGetRateModelResult>> callback,
                string namespaceName,
                string rateName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRateModel(
                    new GetRateModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRateModelResult>(
                            r.Result == null ? null : new EzGetRateModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レートの種類名</param>
		/// <param name="count">交換するロット数</param>
		/// <param name="config">設定値</param>
		public IEnumerator Exchange(
		        UnityAction<AsyncResult<EzExchangeResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                int count,
                List<EzConfig> config=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.Exchange(
                    new ExchangeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithCount(count)
                        .WithConfig(config != null ? config.Select(item => item?.ToModel()).ToList() : new List<Config>(new Config[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzExchangeResult>(
                            r.Result == null ? null : new EzExchangeResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換待機情報の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		public IEnumerator ListAwaits(
		        UnityAction<AsyncResult<EzListAwaitsResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName=null,
                string pageToken=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeAwaits(
                    new DescribeAwaitsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithPageToken(pageToken)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListAwaitsResult>(
                            r.Result == null ? null : new EzListAwaitsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換待機情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		/// <param name="awaitName">交換待機の名前</param>
		public IEnumerator GetAwait(
		        UnityAction<AsyncResult<EzGetAwaitResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string awaitName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetAwait(
                    new GetAwaitRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithAwaitName(awaitName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetAwaitResult>(
                            r.Result == null ? null : new EzGetAwaitResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換待機の報酬を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		/// <param name="awaitName">交換待機の名前</param>
		public IEnumerator Acquire(
		        UnityAction<AsyncResult<EzAcquireResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string awaitName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Acquire(
                    new AcquireRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithAwaitName(awaitName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzAcquireResult>(
                            r.Result == null ? null : new EzAcquireResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換待機を対価を払ってスキップ<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		/// <param name="awaitName">交換待機の名前</param>
		public IEnumerator Skip(
		        UnityAction<AsyncResult<EzSkipResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string awaitName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Skip(
                    new SkipRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithAwaitName(awaitName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzSkipResult>(
                            r.Result == null ? null : new EzSkipResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  交換待機情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">交換レート名</param>
		/// <param name="awaitName">交換待機の名前</param>
		public IEnumerator DeleteAwait(
		        UnityAction<AsyncResult<EzDeleteAwaitResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string awaitName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteAwait(
                    new DeleteAwaitRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithAwaitName(awaitName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteAwaitResult>(
                            r.Result == null ? null : new EzDeleteAwaitResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}