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

using Gs2.Gs2Auth;
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Gs2Auth.Result;
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
namespace Gs2.Unity.Gs2Auth
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
		private readonly Gs2AuthWebSocketClient _client;
		private readonly Gs2AuthRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2AuthWebSocketClient(profile.Gs2Session);
			if (profile.CheckRevokeCertificate)
			{
				_restClient = new Gs2AuthRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2AuthRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator Login(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Auth.Result.EzLoginResult>> callback,
                string keyId,
                string body,
                string signature
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.LoginBySignature(
                    new Gs2.Gs2Auth.Request.LoginBySignatureRequest()
                        .WithKeyId(keyId)
                        .WithBody(body)
                        .WithSignature(signature),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Auth.Result.EzLoginResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Auth.Result.EzLoginResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}