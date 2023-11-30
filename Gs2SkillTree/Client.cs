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

using Gs2.Gs2SkillTree;
using Gs2.Unity.Gs2SkillTree.Model;
using Gs2.Unity.Gs2SkillTree.Result;
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
namespace Gs2.Unity.Gs2SkillTree
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
		private readonly Gs2SkillTreeWebSocketClient _client;
		private readonly Gs2SkillTreeRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2SkillTreeWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2SkillTreeRestClient(connection.RestSession);
		}

        public IEnumerator GetNodeModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzGetNodeModelResult>> callback,
                string namespaceName,
                string nodeModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetNodeModel(
                    new Gs2.Gs2SkillTree.Request.GetNodeModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithNodeModelName(nodeModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzGetNodeModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzGetNodeModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListNodeModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzListNodeModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeNodeModels(
                    new Gs2.Gs2SkillTree.Request.DescribeNodeModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzListNodeModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzListNodeModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStatus(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzGetStatusResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetStatus(
                    new Gs2.Gs2SkillTree.Request.GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzGetStatusResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzGetStatusResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Release(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzReleaseResult>> callback,
		        GameSession session,
                string namespaceName,
                List<string> nodeModelNames
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Release(
                    new Gs2.Gs2SkillTree.Request.ReleaseRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithNodeModelNames(nodeModelNames?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzReleaseResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzReleaseResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Reset(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzResetResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Reset(
                    new Gs2.Gs2SkillTree.Request.ResetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzResetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzResetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Restrain(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzRestrainResult>> callback,
		        GameSession session,
                string namespaceName,
                List<string> nodeModelNames
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Restrain(
                    new Gs2.Gs2SkillTree.Request.RestrainRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithNodeModelNames(nodeModelNames?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2SkillTree.Result.EzRestrainResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2SkillTree.Result.EzRestrainResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}