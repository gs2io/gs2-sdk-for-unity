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

using Gs2.Gs2Enhance;
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.Result;
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
namespace Gs2.Unity.Gs2Enhance
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
		private readonly Gs2EnhanceWebSocketClient _client;
		private readonly Gs2EnhanceRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2EnhanceWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2EnhanceRestClient(connection.RestSession);
		}

        public IEnumerator GetRateModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzGetRateModelResult>> callback,
                string namespaceName,
                string rateName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetRateModel(
                    new Gs2.Gs2Enhance.Request.GetRateModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzGetRateModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzGetRateModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRateModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzListRateModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeRateModels(
                    new Gs2.Gs2Enhance.Request.DescribeRateModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzListRateModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzListRateModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteProgress(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzDeleteProgressResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteProgress(
                    new Gs2.Gs2Enhance.Request.DeleteProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzDeleteProgressResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzDeleteProgressResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator End(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzEndResult>> callback,
		        IGameSession session,
                string namespaceName,
                List<Gs2.Unity.Gs2Enhance.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.End(
                    new Gs2.Gs2Enhance.Request.EndRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzEndResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzEndResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetProgress(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzGetProgressResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetProgress(
                    new Gs2.Gs2Enhance.Request.GetProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzGetProgressResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzGetProgressResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Start(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzStartResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rateName,
                string targetItemSetId,
                List<Gs2.Unity.Gs2Enhance.Model.EzMaterial> materials = null,
                bool? force = null,
                List<Gs2.Unity.Gs2Enhance.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Start(
                    new Gs2.Gs2Enhance.Request.StartRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithAccessToken(session.AccessToken.Token)
                        .WithForce(force)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzStartResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzStartResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Enhance(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzEnhanceResult>> callback,
		        IGameSession session,
                string namespaceName,
                string rateName,
                string targetItemSetId,
                List<Gs2.Unity.Gs2Enhance.Model.EzMaterial> materials,
                List<Gs2.Unity.Gs2Enhance.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DirectEnhance(
                    new Gs2.Gs2Enhance.Request.DirectEnhanceRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Enhance.Result.EzEnhanceResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Enhance.Result.EzEnhanceResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}