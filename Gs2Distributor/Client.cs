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
		private readonly Gs2.Unity.Util.Gs2Connection _connection;
		private readonly Gs2DistributorWebSocketClient _client;
		private readonly Gs2DistributorRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2DistributorWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2DistributorRestClient(connection.RestSession);
		}

        public IEnumerator GetDistributorModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetDistributorModelResult>> callback,
                string namespaceName,
                string distributorName
        )
		{
            yield return _connection.Run(
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
            yield return _connection.Run(
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

        public IEnumerator BatchExecuteApi(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzBatchExecuteApiResult>> callback,
                List<Gs2.Unity.Gs2Distributor.Model.EzBatchRequestPayload> requestPayloads
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.BatchExecuteApi(
                    new Gs2.Gs2Distributor.Request.BatchExecuteApiRequest()
                        .WithRequestPayloads(requestPayloads?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzBatchExecuteApiResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzBatchExecuteApiResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator FreezeMasterData(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataResult>> callback,
		        IGameSession session,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.FreezeMasterData(
                    new Gs2.Gs2Distributor.Request.FreezeMasterDataRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator FreezeMasterDataBySignedTimestamp(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataBySignedTimestampResult>> callback,
		        IGameSession session,
                string namespaceName,
                string body,
                string signature,
                string keyId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.FreezeMasterDataBySignedTimestamp(
                    new Gs2.Gs2Distributor.Request.FreezeMasterDataBySignedTimestampRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithBody(body)
                        .WithSignature(signature)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataBySignedTimestampResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzFreezeMasterDataBySignedTimestampResult.FromModel(r.Result),
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
            yield return _connection.Run(
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
            yield return _connection.Run(
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
            yield return _connection.Run(
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
            yield return _connection.Run(
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
            yield return _connection.Run(
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
            yield return _connection.Run(
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

        public IEnumerator RunVerifyTask(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskResult>> callback,
                string namespaceName,
                string verifyTask,
                string keyId,
                string contextStack = null
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.RunVerifyTask(
                    new Gs2.Gs2Distributor.Request.RunVerifyTaskRequest()
                        .WithNamespaceName(namespaceName)
                        .WithVerifyTask(verifyTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RunVerifyTaskWithoutNamespace(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskWithoutNamespaceResult>> callback,
                string verifyTask,
                string keyId,
                string contextStack = null
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.RunVerifyTaskWithoutNamespace(
                    new Gs2.Gs2Distributor.Request.RunVerifyTaskWithoutNamespaceRequest()
                        .WithVerifyTask(verifyTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskWithoutNamespaceResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzRunVerifyTaskWithoutNamespaceResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SetDefaultConfig(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzSetDefaultConfigResult>> callback,
		        IGameSession session,
                List<Gs2.Unity.Gs2Distributor.Model.EzConfig> config
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.SetTransactionDefaultConfig(
                    new Gs2.Gs2Distributor.Request.SetTransactionDefaultConfigRequest()
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzSetDefaultConfigResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzSetDefaultConfigResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetStampSheetResult(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetStampSheetResultResult>> callback,
		        IGameSession session,
                string namespaceName,
                string transactionId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetStampSheetResult(
                    new Gs2.Gs2Distributor.Request.GetStampSheetResultRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTransactionId(transactionId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetStampSheetResultResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzGetStampSheetResultResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetTransactionResult(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetTransactionResultResult>> callback,
		        IGameSession session,
                string namespaceName,
                string transactionId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetTransactionResult(
                    new Gs2.Gs2Distributor.Request.GetTransactionResultRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTransactionId(transactionId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Distributor.Result.EzGetTransactionResultResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Distributor.Result.EzGetTransactionResultResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}