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

using Gs2.Gs2Enchant;
using Gs2.Unity.Gs2Enchant.Model;
using Gs2.Unity.Gs2Enchant.Result;
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
namespace Gs2.Unity.Gs2Enchant
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
		private readonly Gs2EnchantWebSocketClient _client;
		private readonly Gs2EnchantRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2EnchantWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2EnchantRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2EnchantRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetBalanceParameterModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterModelResult>> callback,
                string namespaceName,
                string parameterName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetBalanceParameterModel(
                    new Gs2.Gs2Enchant.Request.GetBalanceParameterModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBalanceParameterModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeBalanceParameterModels(
                    new Gs2.Gs2Enchant.Request.DescribeBalanceParameterModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBalanceParameterStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string parameterName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetBalanceParameterStatus(
                    new Gs2.Gs2Enchant.Request.GetBalanceParameterStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzGetBalanceParameterStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBalanceParameterStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                string parameterName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeBalanceParameterStatuses(
                    new Gs2.Gs2Enchant.Request.DescribeBalanceParameterStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzListBalanceParameterStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRarityParameterModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterModelResult>> callback,
                string namespaceName,
                string parameterName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRarityParameterModel(
                    new Gs2.Gs2Enchant.Request.GetRarityParameterModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRarityParameterModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeRarityParameterModels(
                    new Gs2.Gs2Enchant.Request.DescribeRarityParameterModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRarityParameterStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string parameterName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetRarityParameterStatus(
                    new Gs2.Gs2Enchant.Request.GetRarityParameterStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzGetRarityParameterStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRarityParameterStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                string parameterName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeRarityParameterStatuses(
                    new Gs2.Gs2Enchant.Request.DescribeRarityParameterStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzListRarityParameterStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator VerifyRarityParameterStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzVerifyRarityParameterStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string parameterName,
                string propertyId,
                string verifyType,
                string parameterValueName,
                int parameterCount
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.VerifyRarityParameterStatus(
                    new Gs2.Gs2Enchant.Request.VerifyRarityParameterStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithParameterName(parameterName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId)
                        .WithVerifyType(verifyType)
                        .WithParameterValueName(parameterValueName)
                        .WithParameterCount(parameterCount),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enchant.Result.EzVerifyRarityParameterStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enchant.Result.EzVerifyRarityParameterStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}