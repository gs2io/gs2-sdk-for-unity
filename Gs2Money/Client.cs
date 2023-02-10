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

using Gs2.Gs2Money;
using Gs2.Unity.Gs2Money.Model;
using Gs2.Unity.Gs2Money.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Gs2Quest;
using Gs2.Gs2Quest.Model;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money
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
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MoneyWebSocketClient _client;
		private readonly Gs2MoneyRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MoneyWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2MoneyRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2MoneyRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator Get(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money.Result.EzGetResult>> callback,
		        GameSession session,
                string namespaceName,
                int slot
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetWallet(
                    new Gs2.Gs2Money.Request.GetWalletRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithSlot(slot),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money.Result.EzGetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money.Result.EzGetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Withdraw(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Money.Result.EzWithdrawResult>> callback,
		        GameSession session,
                string namespaceName,
                int slot,
                int count,
                bool? paidOnly = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Withdraw(
                    new Gs2.Gs2Money.Request.WithdrawRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithSlot(slot)
                        .WithCount(count)
                        .WithPaidOnly(paidOnly),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Money.Result.EzWithdrawResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Money.Result.EzWithdrawResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}