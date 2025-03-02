/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
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

using Gs2.Gs2Money2;
using Gs2.Unity.Gs2Money2.Model;
using Gs2.Unity.Gs2Money2.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money2
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	[Preserve]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public partial class Client
	{
		private readonly Gs2.Unity.Util.Gs2Connection _connection;
		private readonly Gs2Money2WebSocketClient _client;
		private readonly Gs2Money2RestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2Money2WebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2Money2RestClient(connection.RestSession);
		}

        public IEnumerator Get(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money2.Result.EzGetResult>> callback,
		        IGameSession session,
                string namespaceName,
                int slot
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetWallet(
                    new Gs2.Gs2Money2.Request.GetWalletRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithSlot(slot),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money2.Result.EzGetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money2.Result.EzGetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator List(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money2.Result.EzListResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeWallets(
                    new Gs2.Gs2Money2.Request.DescribeWalletsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money2.Result.EzListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money2.Result.EzListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Withdraw(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money2.Result.EzWithdrawResult>> callback,
		        IGameSession session,
                string namespaceName,
                int slot,
                int withdrawCount,
                bool? paidOnly = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Withdraw(
                    new Gs2.Gs2Money2.Request.WithdrawRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithSlot(slot)
                        .WithWithdrawCount(withdrawCount)
                        .WithPaidOnly(paidOnly),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money2.Result.EzWithdrawResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money2.Result.EzWithdrawResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSubscriptionStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money2.Result.EzGetSubscriptionStatusResult>> callback,
		        IGameSession session,
                string namespaceName,
                string contentName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetSubscriptionStatus(
                    new Gs2.Gs2Money2.Request.GetSubscriptionStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithContentName(contentName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money2.Result.EzGetSubscriptionStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money2.Result.EzGetSubscriptionStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSubscriptionStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money2.Result.EzListSubscriptionStatusesResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeSubscriptionStatuses(
                    new Gs2.Gs2Money2.Request.DescribeSubscriptionStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money2.Result.EzListSubscriptionStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money2.Result.EzListSubscriptionStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}