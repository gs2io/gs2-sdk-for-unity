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
using Gs2.Gs2Showcase;
using Gs2.Gs2Showcase.Model;
using Gs2.Gs2Showcase.Request;
using Gs2.Gs2Showcase.Result;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Showcase
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2ShowcaseWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ShowcaseWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  商品棚を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="showcaseName">商品名</param>
		public IEnumerator GetShowcase(
		        UnityAction<AsyncResult<EzGetShowcaseResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName
        )
		{
            yield return _client.GetShowcase(
                new GetShowcaseRequest()
                    .WithNamespaceName(namespaceName)
                    .WithShowcaseName(showcaseName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetShowcaseResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetShowcaseResult>(
                                new EzGetShowcaseResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  商品を購入します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="showcaseName">商品名</param>
		/// <param name="displayItemId">陳列商品ID</param>
		/// <param name="config">設定値</param>
		public IEnumerator Buy(
		        UnityAction<AsyncResult<EzBuyResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName,
                string displayItemId,
                List<EzConfig> config=null
        )
		{
            yield return _client.Buy(
                new BuyRequest()
                    .WithNamespaceName(namespaceName)
                    .WithShowcaseName(showcaseName)
                    .WithDisplayItemId(displayItemId)
                    .WithConfig(config != null ? config.Select(item => item.ToModel()).ToList() : new List<Config>(new Config[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzBuyResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzBuyResult>(
                                new EzBuyResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}