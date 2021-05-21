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
using Gs2.Gs2Limit;
using Gs2.Gs2Limit.Model;
using Gs2.Gs2Limit.Request;
using Gs2.Gs2Limit.Result;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Unity.Gs2Limit.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Limit
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
		private readonly Gs2LimitWebSocketClient _client;
		private readonly Gs2LimitRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2LimitWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2LimitRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2LimitRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  ゲームプレイヤーに紐づく回数制限カウンターの一覧を取得<br />
		///    <br />
		///    回数制限名は省略可能です。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="limitName">回数制限の種類の名前</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListCounters(
		        UnityAction<AsyncResult<EzListCountersResult>> callback,
		        GameSession session,
                string namespaceName,
                string limitName=null,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeCounters(
                    new DescribeCountersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListCountersResult>(
                            r.Result == null ? null : new EzListCountersResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  回数制限名とカウンター名を指定してゲームプレイヤーに紐づく回数制限カウンターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="limitName">回数制限の種類の名前</param>
		/// <param name="counterName">カウンターの名前</param>
		public IEnumerator GetCounter(
		        UnityAction<AsyncResult<EzGetCounterResult>> callback,
		        GameSession session,
                string namespaceName,
                string limitName,
                string counterName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetCounter(
                    new GetCounterRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithCounterName(counterName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetCounterResult>(
                            r.Result == null ? null : new EzGetCounterResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  回数制限名とカウンター名を指定してゲームプレイヤーに紐づく回数制限カウンターをカウントアップ<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="limitName">回数制限の種類の名前</param>
		/// <param name="counterName">カウンターの名前</param>
		/// <param name="countUpValue">カウントアップする量</param>
		/// <param name="maxValue">カウントアップを許容する最大値 を入力してください</param>
		public IEnumerator CountUp(
		        UnityAction<AsyncResult<EzCountUpResult>> callback,
		        GameSession session,
                string namespaceName,
                string limitName,
                string counterName,
                int? countUpValue=null,
                int? maxValue=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.CountUp(
                    new CountUpRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName)
                        .WithCounterName(counterName)
                        .WithCountUpValue(countUpValue)
                        .WithMaxValue(maxValue)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCountUpResult>(
                            r.Result == null ? null : new EzCountUpResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  回数制限モデルの一覧を取得<br />
		///    <br />
		///    近日実施する予定のイベントに関する回数制限設定をしたことで、イベントの情報などが露呈する可能性があります。<br />
		///    回数制限モデルの持つ情報をゲーム内から使用しない場合は 本APIへのアクセス権を剥奪しておく方が安全です。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListLimitModels(
		        UnityAction<AsyncResult<EzListLimitModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeLimitModels(
                    new DescribeLimitModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListLimitModelsResult>(
                            r.Result == null ? null : new EzListLimitModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  回数制限名を指定して回数制限モデルを取得<br />
		///    <br />
		///    近日実施する予定のイベントに関する回数制限設定をしたことで、イベントの情報などが露呈する可能性があります。<br />
		///    回数制限モデルの持つ情報をゲーム内から使用しない場合は 本APIへのアクセス権を剥奪しておく方が安全です。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="limitName">回数制限の種類名</param>
		public IEnumerator GetLimitModel(
		        UnityAction<AsyncResult<EzGetLimitModelResult>> callback,
                string namespaceName,
                string limitName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetLimitModel(
                    new GetLimitModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimitName(limitName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetLimitModelResult>(
                            r.Result == null ? null : new EzGetLimitModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}