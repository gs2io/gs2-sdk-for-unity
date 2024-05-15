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

using Gs2.Gs2MegaField;
using Gs2.Unity.Gs2MegaField.Model;
using Gs2.Unity.Gs2MegaField.Result;
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
namespace Gs2.Unity.Gs2MegaField
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
		private readonly Gs2MegaFieldWebSocketClient _client;
		private readonly Gs2MegaFieldRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2MegaFieldWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2MegaFieldRestClient(connection.RestSession);
		}

        public IEnumerator DescribeAreaModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzDescribeAreaModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeAreaModels(
                    new Gs2.Gs2MegaField.Request.DescribeAreaModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzDescribeAreaModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2MegaField.Result.EzDescribeAreaModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetAreaModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzGetAreaModelResult>> callback,
                string namespaceName,
                string areaModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetAreaModel(
                    new Gs2.Gs2MegaField.Request.GetAreaModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAreaModelName(areaModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzGetAreaModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2MegaField.Result.EzGetAreaModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeLayerModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzDescribeLayerModelsResult>> callback,
                string namespaceName,
                string areaModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeLayerModels(
                    new Gs2.Gs2MegaField.Request.DescribeLayerModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAreaModelName(areaModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzDescribeLayerModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2MegaField.Result.EzDescribeLayerModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetLayerModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzGetLayerModelResult>> callback,
                string namespaceName,
                string areaModelName,
                string layerModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetLayerModel(
                    new Gs2.Gs2MegaField.Request.GetLayerModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAreaModelName(areaModelName)
                        .WithLayerModelName(layerModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzGetLayerModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2MegaField.Result.EzGetLayerModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Update(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzUpdateResult>> callback,
		        IGameSession session,
                string namespaceName,
                string areaModelName,
                string layerModelName,
                Gs2.Unity.Gs2MegaField.Model.EzMyPosition position,
                List<Gs2.Unity.Gs2MegaField.Model.EzScope> scopes = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Action(
                    new Gs2.Gs2MegaField.Request.ActionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAreaModelName(areaModelName)
                        .WithLayerModelName(layerModelName)
                        .WithPosition(position?.ToModel())
                        .WithScopes(scopes?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2MegaField.Result.EzUpdateResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2MegaField.Result.EzUpdateResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}