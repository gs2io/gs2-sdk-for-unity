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

using Gs2.Gs2Datastore;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
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
namespace Gs2.Unity.Gs2Datastore
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
		private readonly Gs2DatastoreWebSocketClient _client;
		private readonly Gs2DatastoreRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DatastoreWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2DatastoreRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2DatastoreRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator DeleteDataObject(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzDeleteDataObjectResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DeleteDataObject(
                    new Gs2.Gs2Datastore.Request.DeleteDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDataObjectName(dataObjectName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzDeleteDataObjectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzDeleteDataObjectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DoneUpload(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzDoneUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DoneUpload(
                    new Gs2.Gs2Datastore.Request.DoneUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDataObjectName(dataObjectName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzDoneUploadResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzDoneUploadResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListMyDataObjects(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzListMyDataObjectsResult>> callback,
		        GameSession session,
                string namespaceName,
                string status = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeDataObjects(
                    new Gs2.Gs2Datastore.Request.DescribeDataObjectsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithStatus(status)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzListMyDataObjectsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzListMyDataObjectsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PrepareDownload(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.PrepareDownload(
                    new Gs2.Gs2Datastore.Request.PrepareDownloadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectId(dataObjectId)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PrepareDownloadByUserIdAndDataObjectName(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadByUserIdAndDataObjectNameResult>> callback,
                string namespaceName,
                string userId,
                string dataObjectName = null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.PrepareDownloadByUserIdAndDataObjectName(
                    new Gs2.Gs2Datastore.Request.PrepareDownloadByUserIdAndDataObjectNameRequest()
                        .WithNamespaceName(namespaceName)
                        .WithUserId(userId)
                        .WithDataObjectName(dataObjectName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadByUserIdAndDataObjectNameResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadByUserIdAndDataObjectNameResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PrepareDownloadOwnData(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadOwnDataResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.PrepareDownloadOwnData(
                    new Gs2.Gs2Datastore.Request.PrepareDownloadOwnDataRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadOwnDataResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzPrepareDownloadOwnDataResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PrepareReUpload(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareReUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName = null,
                string contentType = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.PrepareReUpload(
                    new Gs2.Gs2Datastore.Request.PrepareReUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDataObjectName(dataObjectName)
                        .WithContentType(contentType),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareReUploadResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzPrepareReUploadResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PrepareUpload(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string name = null,
                string scope = null,
                string contentType = null,
                List<string> allowUserIds = null,
                bool? updateIfExists = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.PrepareUpload(
                    new Gs2.Gs2Datastore.Request.PrepareUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithName(name)
                        .WithScope(scope)
                        .WithContentType(contentType)
                        .WithAllowUserIds(allowUserIds?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithUpdateIfExists(updateIfExists),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzPrepareUploadResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzPrepareUploadResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RestoreDataObject(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzRestoreDataObjectResult>> callback,
                string namespaceName,
                string dataObjectId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.RestoreDataObject(
                    new Gs2.Gs2Datastore.Request.RestoreDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectId(dataObjectId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzRestoreDataObjectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzRestoreDataObjectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateDataObject(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzUpdateDataObjectResult>> callback,
		        GameSession session,
                string namespaceName,
                string scope = null,
                List<string> allowUserIds = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.UpdateDataObject(
                    new Gs2.Gs2Datastore.Request.UpdateDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithScope(scope)
                        .WithAllowUserIds(allowUserIds?.Select(v => {
                            return v;
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzUpdateDataObjectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzUpdateDataObjectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListDataObjectHistories(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzListDataObjectHistoriesResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeDataObjectHistories(
                    new Gs2.Gs2Datastore.Request.DescribeDataObjectHistoriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDataObjectName(dataObjectName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Datastore.Result.EzListDataObjectHistoriesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Datastore.Result.EzListDataObjectHistoriesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}