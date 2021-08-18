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

using Gs2.Gs2Version;
using Gs2.Unity.Gs2Version.Model;
using Gs2.Unity.Gs2Version.Result;
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
namespace Gs2.Unity.Gs2Version
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
		private readonly Gs2VersionWebSocketClient _client;
		private readonly Gs2VersionRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2VersionWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2VersionRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2VersionRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetVersionModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzGetVersionModelResult>> callback,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetVersionModel(
                    new Gs2.Gs2Version.Request.GetVersionModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithVersionName(versionName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzGetVersionModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzGetVersionModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListVersionModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzListVersionModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeVersionModels(
                    new Gs2.Gs2Version.Request.DescribeVersionModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzListVersionModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzListVersionModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Accept(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzAcceptResult>> callback,
		        GameSession session,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Accept(
                    new Gs2.Gs2Version.Request.AcceptRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithVersionName(versionName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzAcceptResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzAcceptResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Delete(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzDeleteResult>> callback,
		        GameSession session,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteAcceptVersion(
                    new Gs2.Gs2Version.Request.DeleteAcceptVersionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithVersionName(versionName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzDeleteResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzDeleteResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator List(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzListResult>> callback,
		        GameSession session,
                string namespaceName,
                int limit,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeAcceptVersions(
                    new Gs2.Gs2Version.Request.DescribeAcceptVersionsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CheckVersion(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Version.Result.EzCheckVersionResult>> callback,
		        GameSession session,
                string namespaceName,
                List<Gs2.Unity.Gs2Version.Model.EzTargetVersion> targetVersions = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.CheckVersion(
                    new Gs2.Gs2Version.Request.CheckVersionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetVersions(targetVersions?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Version.Result.EzCheckVersionResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Version.Result.EzCheckVersionResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}