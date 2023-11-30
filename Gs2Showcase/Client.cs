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

using Gs2.Gs2Showcase;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.Result;
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
namespace Gs2.Unity.Gs2Showcase
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
		private readonly Gs2ShowcaseWebSocketClient _client;
		private readonly Gs2ShowcaseRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2ShowcaseWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2ShowcaseRestClient(connection.RestSession);
		}

        public IEnumerator Buy(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzBuyResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName,
                string displayItemId = null,
                int? quantity = null,
                List<Gs2.Unity.Gs2Showcase.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Buy(
                    new Gs2.Gs2Showcase.Request.BuyRequest()
                        .WithNamespaceName(namespaceName)
                        .WithShowcaseName(showcaseName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDisplayItemId(displayItemId)
                        .WithQuantity(quantity)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzBuyResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Showcase.Result.EzBuyResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetShowcase(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzGetShowcaseResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetShowcase(
                    new Gs2.Gs2Showcase.Request.GetShowcaseRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithShowcaseName(showcaseName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzGetShowcaseResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Showcase.Result.EzGetShowcaseResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetRandomShowcaseDisplayItem(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzGetRandomShowcaseDisplayItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName,
                string displayItemName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetRandomDisplayItem(
                    new Gs2.Gs2Showcase.Request.GetRandomDisplayItemRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithShowcaseName(showcaseName)
                        .WithDisplayItemName(displayItemName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzGetRandomShowcaseDisplayItemResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Showcase.Result.EzGetRandomShowcaseDisplayItemResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListRandomShowcaseDisplayItems(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzListRandomShowcaseDisplayItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeRandomDisplayItems(
                    new Gs2.Gs2Showcase.Request.DescribeRandomDisplayItemsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithShowcaseName(showcaseName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzListRandomShowcaseDisplayItemsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Showcase.Result.EzListRandomShowcaseDisplayItemsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RandomShowcaseBuy(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzRandomShowcaseBuyResult>> callback,
		        GameSession session,
                string namespaceName,
                string showcaseName,
                string displayItemName = null,
                int? quantity = null,
                List<Gs2.Unity.Gs2Showcase.Model.EzConfig> config = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.RandomShowcaseBuy(
                    new Gs2.Gs2Showcase.Request.RandomShowcaseBuyRequest()
                        .WithNamespaceName(namespaceName)
                        .WithShowcaseName(showcaseName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDisplayItemName(displayItemName)
                        .WithQuantity(quantity)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Showcase.Result.EzRandomShowcaseBuyResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Showcase.Result.EzRandomShowcaseBuyResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}