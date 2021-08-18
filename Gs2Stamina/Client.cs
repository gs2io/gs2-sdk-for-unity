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

using Gs2.Gs2Stamina;
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Gs2Stamina.Result;
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
namespace Gs2.Unity.Gs2Stamina
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
		private readonly Gs2StaminaWebSocketClient _client;
		private readonly Gs2StaminaRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2StaminaWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2StaminaRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2StaminaRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetStaminaModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzGetStaminaModelResult>> callback,
                string namespaceName,
                string staminaName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetStaminaModel(
                    new Gs2.Gs2Stamina.Request.GetStaminaModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStaminaName(staminaName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzGetStaminaModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Stamina.Result.EzGetStaminaModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListStaminaModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzListStaminaModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeStaminaModels(
                    new Gs2.Gs2Stamina.Request.DescribeStaminaModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzListStaminaModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Stamina.Result.EzListStaminaModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Consume(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzConsumeResult>> callback,
		        GameSession session,
                string namespaceName,
                string staminaName,
                int consumeValue
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.ConsumeStamina(
                    new Gs2.Gs2Stamina.Request.ConsumeStaminaRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStaminaName(staminaName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConsumeValue(consumeValue),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzConsumeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Stamina.Result.EzConsumeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStamina(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzGetStaminaResult>> callback,
		        GameSession session,
                string namespaceName,
                string staminaName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetStamina(
                    new Gs2.Gs2Stamina.Request.GetStaminaRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStaminaName(staminaName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Stamina.Result.EzGetStaminaResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Stamina.Result.EzGetStaminaResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}