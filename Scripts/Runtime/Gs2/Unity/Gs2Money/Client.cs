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
using Gs2.Gs2Money;
using Gs2.Gs2Money.Model;
using Gs2.Gs2Money.Request;
using Gs2.Gs2Money.Result;
using Gs2.Unity.Gs2Money.Model;
using Gs2.Unity.Gs2Money.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Money
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MoneyWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MoneyWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  ウォレットを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペースの名前</param>
		/// <param name="slot">スロット番号</param>
		public IEnumerator Get(
		        UnityAction<AsyncResult<EzGetResult>> callback,
		        GameSession session,
                string namespaceName,
                int slot
        )
		{
            yield return _client.GetWallet(
                new GetWalletRequest()
                    .WithNamespaceName(namespaceName)
                    .WithSlot(slot)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetResult>(
                                new EzGetResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  ウォレットから残高を消費します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペースの名前</param>
		/// <param name="slot">スロット番号</param>
		/// <param name="count">消費する課金通貨の数量</param>
		/// <param name="paidOnly">有償課金通貨のみを対象とするか</param>
		public IEnumerator Withdraw(
		        UnityAction<AsyncResult<EzWithdrawResult>> callback,
		        GameSession session,
                string namespaceName,
                int slot,
                int count,
                bool paidOnly
        )
		{
            yield return _client.Withdraw(
                new WithdrawRequest()
                    .WithNamespaceName(namespaceName)
                    .WithSlot(slot)
                    .WithCount(count)
                    .WithPaidOnly(paidOnly)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzWithdrawResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzWithdrawResult>(
                                new EzWithdrawResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}