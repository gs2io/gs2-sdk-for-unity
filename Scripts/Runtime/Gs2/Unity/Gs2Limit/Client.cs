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

namespace Gs2.Unity.Gs2Limit
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2LimitWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2LimitWebSocketClient(profile.Gs2Session);
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
		public IEnumerator ListCounters(
		        UnityAction<AsyncResult<EzListCountersResult>> callback,
		        GameSession session,
                string namespaceName,
                string limitName=null
        )
		{
            yield return _client.DescribeCounters(
                new DescribeCountersRequest()
                    .WithNamespaceName(namespaceName)
                    .WithLimitName(limitName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListCountersResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListCountersResult>(
                                new EzListCountersResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.GetCounter(
                new GetCounterRequest()
                    .WithNamespaceName(namespaceName)
                    .WithLimitName(limitName)
                    .WithCounterName(counterName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCounterResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCounterResult>(
                                new EzGetCounterResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.CountUp(
                new CountUpRequest()
                    .WithNamespaceName(namespaceName)
                    .WithLimitName(limitName)
                    .WithCounterName(counterName)
                    .WithCountUpValue(countUpValue)
                    .WithMaxValue(maxValue)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzCountUpResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzCountUpResult>(
                                new EzCountUpResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DescribeLimitModels(
                new DescribeLimitModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListLimitModelsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListLimitModelsResult>(
                                new EzListLimitModelsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.GetLimitModel(
                new GetLimitModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithLimitName(limitName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetLimitModelResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetLimitModelResult>(
                                new EzGetLimitModelResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}