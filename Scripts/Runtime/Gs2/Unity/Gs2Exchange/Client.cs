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

namespace Gs2.Unity.Gs2Exchange
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2ExchangeWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ExchangeWebSocketClient(profile.Gs2Session);
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
            yield return _client.DescribeRateModels(
                new DescribeRateModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListRateModelsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListRateModelsResult>(
                                new EzListRateModelsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.GetRateModel(
                new GetRateModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithRateName(rateName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRateModelResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetRateModelResult>(
                                new EzGetRateModelResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.Exchange(
                new ExchangeRequest()
                    .WithNamespaceName(namespaceName)
                    .WithRateName(rateName)
                    .WithCount(count)
                    .WithConfig(config != null ? config.Select(item => item.ToModel()).ToList() : new List<Config>(new Config[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzExchangeResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzExchangeResult>(
                                new EzExchangeResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}