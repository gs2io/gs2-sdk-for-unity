/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0(the "License").
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Gs2Datastore;
using Gs2.Gs2Datastore.Model;
using Gs2.Gs2Datastore.Request;
using Gs2.Gs2Datastore.Result;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Datastore
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

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

		/// <summary>
		///  データオブジェクトの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="status">状態</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListMyDataObjects(
		        UnityAction<AsyncResult<EzListMyDataObjectsResult>> callback,
		        GameSession session,
                string namespaceName,
                string status=null,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeDataObjects(
                    new DescribeDataObjectsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStatus(status)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListMyDataObjectsResult>(
                            r.Result == null ? null : new EzListMyDataObjectsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データオブジェクトを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="scope">ファイルのアクセス権</param>
		/// <param name="allowUserIds">公開するユーザIDリスト</param>
		public IEnumerator UpdateDataObject(
		        UnityAction<AsyncResult<EzUpdateDataObjectResult>> callback,
		        GameSession session,
                string namespaceName,
                string scope,
                List<string> allowUserIds
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.UpdateDataObject(
                    new UpdateDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithScope(scope)
                        .WithAllowUserIds(allowUserIds)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzUpdateDataObjectResult>(
                            r.Result == null ? null : new EzUpdateDataObjectResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データのアップロード準備<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="name">データの名前</param>
		/// <param name="scope">ファイルのアクセス権</param>
		/// <param name="allowUserIds">公開するユーザIDリスト</param>
		/// <param name="updateIfExists">既にデータが存在する場合にエラーとするか、データを更新するか</param>
		public IEnumerator PrepareUpload(
		        UnityAction<AsyncResult<EzPrepareUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string scope,
                List<string> allowUserIds,
                string name=null,
                bool? updateIfExists=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PrepareUpload(
                    new PrepareUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithName(name)
                        .WithScope(scope)
                        .WithAllowUserIds(allowUserIds)
                        .WithUpdateIfExists(updateIfExists)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPrepareUploadResult>(
                            r.Result == null ? null : new EzPrepareUploadResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データの再アップロード準備<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator PrepareReUpload(
		        UnityAction<AsyncResult<EzPrepareReUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PrepareReUpload(
                    new PrepareReUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPrepareReUploadResult>(
                            r.Result == null ? null : new EzPrepareReUploadResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データのアップロード完了を報告<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator DoneUpload(
		        UnityAction<AsyncResult<EzDoneUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DoneUpload(
                    new DoneUploadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDoneUploadResult>(
                            r.Result == null ? null : new EzDoneUploadResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データをダウンロード準備<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectId">データオブジェクト</param>
		public IEnumerator PrepareDownload(
		        UnityAction<AsyncResult<EzPrepareDownloadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PrepareDownload(
                    new PrepareDownloadRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectId(dataObjectId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPrepareDownloadResult>(
                            r.Result == null ? null : new EzPrepareDownloadResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  自分のデータをダウンロード準備<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator PrepareDownloadOwnData(
		        UnityAction<AsyncResult<EzPrepareDownloadOwnDataResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.PrepareDownloadOwnData(
                    new PrepareDownloadOwnDataRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzPrepareDownloadOwnDataResult>(
                            r.Result == null ? null : new EzPrepareDownloadOwnDataResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ユーザIDとデータ名を指定してデータをダウンロード準備<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="userId">ユーザーID</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator PrepareDownloadByUserIdAndDataObjectName(
		        UnityAction<AsyncResult<EzPrepareDownloadByUserIdAndDataObjectNameResult>> callback,
                string namespaceName,
                string userId,
                string dataObjectName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.PrepareDownloadByUserIdAndDataObjectName(
                    new PrepareDownloadByUserIdAndDataObjectNameRequest()
                        .WithNamespaceName(namespaceName)
                        .WithUserId(userId)
                        .WithDataObjectName(dataObjectName),
                    r => cb.Invoke(
                        new AsyncResult<EzPrepareDownloadByUserIdAndDataObjectNameResult>(
                            r.Result == null ? null : new EzPrepareDownloadByUserIdAndDataObjectNameResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  アップロードしたデータを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator DeleteDataObject(
		        UnityAction<AsyncResult<EzDeleteDataObjectResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteDataObject(
                    new DeleteDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteDataObjectResult>(
                            r.Result == null ? null : new EzDeleteDataObjectResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データの管理情報を修復<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectId">データオブジェクト</param>
		public IEnumerator RestoreDataObject(
		        UnityAction<AsyncResult<EzRestoreDataObjectResult>> callback,
                string namespaceName,
                string dataObjectId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RestoreDataObject(
                    new RestoreDataObjectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectId(dataObjectId),
                    r => cb.Invoke(
                        new AsyncResult<EzRestoreDataObjectResult>(
                            r.Result == null ? null : new EzRestoreDataObjectResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  データオブジェクト履歴の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListDataObhectHistories(
		        UnityAction<AsyncResult<EzListDataObhectHistoriesResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeDataObjectHistories(
                    new DescribeDataObjectHistoriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDataObjectName(dataObjectName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListDataObhectHistoriesResult>(
                            r.Result == null ? null : new EzListDataObhectHistoriesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}