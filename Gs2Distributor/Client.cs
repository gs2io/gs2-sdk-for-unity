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

using Gs2.Gs2Distributor;
using Gs2.Unity.Gs2Distributor.Model;
using Gs2.Unity.Gs2Distributor.Result;
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
namespace Gs2.Unity.Gs2Distributor
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
		private readonly Gs2DistributorWebSocketClient _client;
		private readonly Gs2DistributorRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DistributorWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2DistributorRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2DistributorRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetDistributorModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetDistributorModelResult>> callback,
                string namespaceName,
                string distributorName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetDistributorModel(
                    new Gs2.Gs2Distributor.Request.GetDistributorModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDistributorName(distributorName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetDistributorModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzGetDistributorModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListDistributorModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzListDistributorModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeDistributorModels(
                    new Gs2.Gs2Distributor.Request.DescribeDistributorModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzListDistributorModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzListDistributorModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampSheet(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetResult>> callback,
                string namespaceName,
                string stampSheet,
                string keyId,
                string contextStack = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampSheet(
                    new Gs2.Gs2Distributor.Request.RunStampSheetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampSheetExpress(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressResult>> callback,
                string namespaceName,
                string stampSheet,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampSheetExpress(
                    new Gs2.Gs2Distributor.Request.RunStampSheetExpressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampSheetExpressWithoutNamespace(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressWithoutNamespaceResult>> callback,
                string stampSheet,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampSheetExpressWithoutNamespace(
                    new Gs2.Gs2Distributor.Request.RunStampSheetExpressWithoutNamespaceRequest()
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressWithoutNamespaceResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetExpressWithoutNamespaceResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampSheetWithoutNamespace(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetWithoutNamespaceResult>> callback,
                string stampSheet,
                string keyId,
                string contextStack = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampSheetWithoutNamespace(
                    new Gs2.Gs2Distributor.Request.RunStampSheetWithoutNamespaceRequest()
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetWithoutNamespaceResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampSheetWithoutNamespaceResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampTask(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskResult>> callback,
                string namespaceName,
                string stampTask,
                string keyId,
                string contextStack = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampTask(
                    new Gs2.Gs2Distributor.Request.RunStampTaskRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampTask(stampTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunStampTaskWithoutNamespace(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskWithoutNamespaceResult>> callback,
                string stampTask,
                string keyId,
                string contextStack = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RunStampTaskWithoutNamespace(
                    new Gs2.Gs2Distributor.Request.RunStampTaskWithoutNamespaceRequest()
                        .WithStampTask(stampTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskWithoutNamespaceResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunStampTaskWithoutNamespaceResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}