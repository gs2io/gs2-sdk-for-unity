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
using Gs2.Gs2Lock;
using Gs2.Gs2Lock.Model;
using Gs2.Gs2Lock.Request;
using Gs2.Gs2Lock.Result;
using Gs2.Unity.Gs2Lock.Model;
using Gs2.Unity.Gs2Lock.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Lock
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2LockWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2LockWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  ロックを取得<br />
		///    <br />
		///    ttl で指定した秒数 `プロパティID` のリソースをロックします。<br />
		///    ロックする際には `トランザクションID` を指定する必要があります。<br />
		///    異なる `トランザクションID` による同一 `プロパティID` に対するロック取得は失敗します。<br />
		///    同一トランザクションからのロック取得リクエストの場合は参照カウントを増やします。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリー名</param>
		/// <param name="propertyId">プロパティID</param>
		/// <param name="transactionId">ロックを取得するトランザクションID</param>
		/// <param name="ttl">ロックを取得する期限（秒）</param>
		public IEnumerator Lock(
		        UnityAction<AsyncResult<EzLockResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyId,
                string transactionId,
                long ttl
        )
		{
            yield return _client.Lock(
                new LockRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPropertyId(propertyId)
                    .WithTransactionId(transactionId)
                    .WithTtl(ttl)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzLockResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzLockResult>(
                                new EzLockResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  ロックを解放<br />
		///    <br />
		///    ロックの解放には同一 `トランザクションID` から解放する必要があります。<br />
		///    ロックの取得時に再入を行った場合は同一回数ロックの解放を行い、参照カウントが0になったタイミングで実際に解放が行われます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリー名</param>
		/// <param name="propertyId">プロパティID</param>
		/// <param name="transactionId">ロックを取得したトランザクションID</param>
		public IEnumerator Unlock(
		        UnityAction<AsyncResult<EzUnlockResult>> callback,
		        GameSession session,
                string namespaceName,
                string propertyId,
                string transactionId
        )
		{
            yield return _client.Unlock(
                new UnlockRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPropertyId(propertyId)
                    .WithTransactionId(transactionId)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzUnlockResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzUnlockResult>(
                                new EzUnlockResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}