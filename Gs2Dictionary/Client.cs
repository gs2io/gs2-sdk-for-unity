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

using Gs2.Gs2Dictionary;
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.Result;
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
namespace Gs2.Unity.Gs2Dictionary
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
		private readonly Gs2DictionaryWebSocketClient _client;
		private readonly Gs2DictionaryRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2DictionaryWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2DictionaryRestClient(connection.RestSession);
		}

        public IEnumerator GetEntryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryModelResult>> callback,
                string namespaceName,
                string entryName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetEntryModel(
                    new Gs2.Gs2Dictionary.Request.GetEntryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEntryName(entryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzGetEntryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListEntryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListEntryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeEntryModels(
                    new Gs2.Gs2Dictionary.Request.DescribeEntryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListEntryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzListEntryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetEntry(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryResult>> callback,
		        IGameSession session,
                string namespaceName,
                string entryModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetEntry(
                    new Gs2.Gs2Dictionary.Request.GetEntryRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithEntryModelName(entryModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzGetEntryResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetEntryWithSignature(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryWithSignatureResult>> callback,
		        IGameSession session,
                string namespaceName,
                string entryModelName,
                string keyId = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetEntryWithSignature(
                    new Gs2.Gs2Dictionary.Request.GetEntryWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithEntryModelName(entryModelName)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetEntryWithSignatureResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzGetEntryWithSignatureResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListEntries(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListEntriesResult>> callback,
		        IGameSession session,
                string namespaceName,
                int? limit = null,
                string pageToken = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeEntries(
                    new Gs2.Gs2Dictionary.Request.DescribeEntriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithLimit(limit)
                        .WithPageToken(pageToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListEntriesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzListEntriesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AddLikes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzAddLikesResult>> callback,
		        IGameSession session,
                string namespaceName,
                List<string> entryModelNames = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.AddLikes(
                    new Gs2.Gs2Dictionary.Request.AddLikesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithEntryModelNames(entryModelNames?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzAddLikesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzAddLikesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteLikes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzDeleteLikesResult>> callback,
		        IGameSession session,
                string namespaceName,
                List<string> entryModelNames = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DeleteLikes(
                    new Gs2.Gs2Dictionary.Request.DeleteLikesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithEntryModelNames(entryModelNames?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzDeleteLikesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzDeleteLikesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetLike(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetLikeResult>> callback,
		        IGameSession session,
                string namespaceName,
                string entryModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetLike(
                    new Gs2.Gs2Dictionary.Request.GetLikeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithEntryModelName(entryModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzGetLikeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzGetLikeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListLikes(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListLikesResult>> callback,
		        IGameSession session,
                string namespaceName,
                int? limit = null,
                string pageToken = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeLikes(
                    new Gs2.Gs2Dictionary.Request.DescribeLikesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithLimit(limit)
                        .WithPageToken(pageToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Dictionary.Result.EzListLikesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Dictionary.Result.EzListLikesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}