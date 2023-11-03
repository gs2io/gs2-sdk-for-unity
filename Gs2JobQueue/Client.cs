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

using Gs2.Gs2JobQueue;
using Gs2.Unity.Gs2JobQueue.Model;
using Gs2.Unity.Gs2JobQueue.Result;
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
namespace Gs2.Unity.Gs2JobQueue
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
		private readonly Gs2JobQueueWebSocketClient _client;
		private readonly Gs2JobQueueRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2JobQueueWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2JobQueueRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2JobQueueRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator Run(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2JobQueue.Result.EzRunResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.Run(
                    new Gs2.Gs2JobQueue.Request.RunRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2JobQueue.Result.EzRunResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2JobQueue.Result.EzRunResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetResult(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2JobQueue.Result.EzGetResultResult>> callback,
		        GameSession session,
                string namespaceName,
                string jobName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetJobResult(
                    new Gs2.Gs2JobQueue.Request.GetJobResultRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithJobName(jobName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2JobQueue.Result.EzGetResultResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2JobQueue.Result.EzGetResultResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}