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
using Gs2.Gs2Stamina;
using Gs2.Gs2Stamina.Model;
using Gs2.Gs2Stamina.Request;
using Gs2.Gs2Stamina.Result;
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Gs2Stamina.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Stamina
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2StaminaWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2StaminaWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  スタミナモデルを認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListStaminaModels(
		        UnityAction<AsyncResult<EzListStaminaModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _client.DescribeStaminaModels(
                new DescribeStaminaModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListStaminaModelsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListStaminaModelsResult>(
                                new EzListStaminaModelsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  スタミナモデルを認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="staminaName">スタミナの種類名</param>
		public IEnumerator GetStaminaModel(
		        UnityAction<AsyncResult<EzGetStaminaModelResult>> callback,
                string namespaceName,
                string staminaName
        )
		{
            yield return _client.GetStaminaModel(
                new GetStaminaModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStaminaName(staminaName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetStaminaModelResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetStaminaModelResult>(
                                new EzGetStaminaModelResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  現在のスタミナを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="staminaName">スタミナの種類名</param>
		public IEnumerator GetStamina(
		        UnityAction<AsyncResult<EzGetStaminaResult>> callback,
		        GameSession session,
                string namespaceName,
                string staminaName
        )
		{
            yield return _client.GetStamina(
                new GetStaminaRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStaminaName(staminaName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetStaminaResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetStaminaResult>(
                                new EzGetStaminaResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  スタミナを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="staminaName">スタミナの種類名</param>
		/// <param name="consumeValue">消費するスタミナ量</param>
		public IEnumerator Consume(
		        UnityAction<AsyncResult<EzConsumeResult>> callback,
		        GameSession session,
                string namespaceName,
                string staminaName,
                int consumeValue
        )
		{
            yield return _client.ConsumeStamina(
                new ConsumeStaminaRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStaminaName(staminaName)
                    .WithConsumeValue(consumeValue)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzConsumeResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzConsumeResult>(
                                new EzConsumeResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}