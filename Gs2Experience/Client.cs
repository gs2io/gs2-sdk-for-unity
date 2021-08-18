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

using Gs2.Gs2Experience;
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.Result;
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
namespace Gs2.Unity.Gs2Experience
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
		private readonly Gs2ExperienceWebSocketClient _client;
		private readonly Gs2ExperienceRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ExperienceWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2ExperienceRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2ExperienceRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetExperienceModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Experience.Result.EzGetExperienceModelResult>> callback,
                string namespaceName,
                string experienceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetExperienceModel(
                    new Gs2.Gs2Experience.Request.GetExperienceModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Experience.Result.EzGetExperienceModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Experience.Result.EzGetExperienceModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListExperienceModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Experience.Result.EzListExperienceModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeExperienceModels(
                    new Gs2.Gs2Experience.Request.DescribeExperienceModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Experience.Result.EzListExperienceModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Experience.Result.EzListExperienceModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Experience.Result.EzGetStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string experienceName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetStatus(
                    new Gs2.Gs2Experience.Request.GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPropertyId(propertyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Experience.Result.EzGetStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Experience.Result.EzGetStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListStatuses(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Experience.Result.EzListStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                int limit,
                string experienceName = null,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeStatuses(
                    new Gs2.Gs2Experience.Request.DescribeStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Experience.Result.EzListStatusesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Experience.Result.EzListStatusesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}