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
 *
 * deny overwrite
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
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2DatastoreWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DatastoreWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  データのアップロード準備<br />
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
            yield return _client.DescribeDataObjects(
                new DescribeDataObjectsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStatus(status)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListMyDataObjectsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListMyDataObjectsResult>(
                                new EzListMyDataObjectsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.UpdateDataObject(
                new UpdateDataObjectRequest()
                    .WithNamespaceName(namespaceName)
                    .WithScope(scope)
                    .WithAllowUserIds(allowUserIds)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateDataObjectResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateDataObjectResult>(
                                new EzUpdateDataObjectResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
		/// <param name="scope">ファイルのアクセス権</param>
		/// <param name="allowUserIds">公開するユーザIDリスト</param>
		public IEnumerator PrepareUpload(
		        UnityAction<AsyncResult<EzPrepareUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string scope,
                List<string> allowUserIds
        )
		{
            yield return _client.PrepareUpload(
                new PrepareUploadRequest()
                    .WithNamespaceName(namespaceName)
                    .WithScope(scope)
                    .WithAllowUserIds(allowUserIds)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareUploadResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareUploadResult>(
                                new EzPrepareUploadResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.PrepareReUpload(
                new PrepareReUploadRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDataObjectName(dataObjectName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareReUploadResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareReUploadResult>(
                                new EzPrepareReUploadResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator DoneUpload(
		        UnityAction<AsyncResult<EzDoneUploadResult>> callback,
		        GameSession session,
                string namespaceName,
                string dataObjectName
        )
		{
            yield return _client.DoneUpload(
                new DoneUploadRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDataObjectName(dataObjectName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoneUploadResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoneUploadResult>(
                                new EzDoneUploadResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  データをダウンロード<br />
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
            yield return _client.PrepareDownload(
                new PrepareDownloadRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDataObjectId(dataObjectId)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareDownloadResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzPrepareDownloadResult>(
                                new EzPrepareDownloadResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DeleteDataObject(
                new DeleteDataObjectRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDataObjectName(dataObjectName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteDataObjectResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteDataObjectResult>(
                                new EzDeleteDataObjectResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DescribeDataObjectHistories(
                new DescribeDataObjectHistoriesRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDataObjectName(dataObjectName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListDataObhectHistoriesResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListDataObhectHistoriesResult>(
                                new EzListDataObhectHistoriesResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  データのアップロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="uploadUrl">URL</param>
		/// <param name="data">アップロードするデータ</param>
		public IEnumerator UploadImpl(
			UnityAction<AsyncResult<EzUploadImplResult>> callback,
			string uploadUrl,
			byte[] data
		)
		{
			var request = UnityWebRequest.Put(uploadUrl, data);
			request.downloadHandler = new DownloadHandlerBuffer();
			yield return request.SendWebRequest();

			var result = new Gs2RestResponse(
				!request.isNetworkError || request.isHttpError ? request.downloadHandler.text : request.error,
				request.responseCode
			);

			if (result.Error == null)
			{
				callback.Invoke(
					new AsyncResult<EzUploadImplResult>(
						new EzUploadImplResult(), 
						result.Error
					)
				);
			}
			else
			{
				callback.Invoke(
					new AsyncResult<EzUploadImplResult>(
						null,
						result.Error
					)
				);
			}
		}

		/// <summary>
		///  データのダウンロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="downloadUrl">URL</param>
		public IEnumerator DownloadImpl(
			UnityAction<AsyncResult<EzDownloadImplResult>> callback,
			string downloadUrl
		)
		{
			var request = UnityWebRequest.Get(downloadUrl);
			request.downloadHandler = new DownloadHandlerBuffer();
			yield return request.SendWebRequest();
			
			var result = new Gs2RestResponse(
				!request.isNetworkError || request.isHttpError ? null : request.error,
				request.responseCode
			);

			if (result.Error == null)
			{
				callback.Invoke(
					new AsyncResult<EzDownloadImplResult>(
						new EzDownloadImplResult(request.downloadHandler.data), 
						result.Error
					)
				);
			}
			else
			{
				callback.Invoke(
					new AsyncResult<EzDownloadImplResult>(
						null,
						result.Error
					)
				);
			}
		}

		/// <summary>
		///  データのアップロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="scope">ファイルのアクセス権</param>
		/// <param name="allowUserIds">公開するユーザIDリスト</param>
		/// <param name="data">アップロードするデータ</param>
		public IEnumerator Upload(
			UnityAction<AsyncResult<EzUploadResult>> callback,
			GameSession session,
			string namespaceName,
			string scope,
			List<string> allowUserIds,
			byte[] data
		)
		{
			string uploadUrl;
			EzDataObject dataObject;
			{
				EzPrepareUploadResult result = null;
				yield return PrepareUpload(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					session,
					namespaceName,
					scope,
					allowUserIds
				);

				if (result == null)
				{
					yield break;
				}

				uploadUrl = result.UploadUrl;
				dataObject = result.Item;
			}
			{
				EzUploadImplResult result = null;
				yield return UploadImpl(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					uploadUrl,
					data
				);

				if (result == null)
				{
					yield break;
				}
			}
			{
				EzDoneUploadResult result = null;
				yield return DoneUpload(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					session,
					namespaceName,
					dataObject.Name
				);
				
				if (result == null)
				{
					yield break;
				}

				dataObject = result.Item;
				
				callback.Invoke(
					new AsyncResult<EzUploadResult>(
						new EzUploadResult(dataObject),
						null
					)
				);
			}
		}

		/// <summary>
		///  データの再アップロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObject">データオブジェクト</param>
		/// <param name="data">アップロードするデータ</param>
		public IEnumerator ReUpload(
			UnityAction<AsyncResult<EzReUploadResult>> callback,
			GameSession session,
			string namespaceName,
			EzDataObject dataObject,
			byte[] data
		)
		{
			string uploadUrl;
			{
				EzPrepareReUploadResult result = null;
				yield return PrepareReUpload(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzReUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					session,
					namespaceName,
					dataObject.Name
				);

				if (result == null)
				{
					yield break;
				}

				uploadUrl = result.UploadUrl;
				dataObject = result.Item;
			}
			{
				EzUploadImplResult result = null;
				yield return UploadImpl(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzReUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					uploadUrl,
					data
				);

				if (result == null)
				{
					yield break;
				}
			}
			{
				EzDoneUploadResult result = null;
				yield return DoneUpload(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzReUploadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					session,
					namespaceName,
					dataObject.Name
				);
				
				if (result == null)
				{
					yield break;
				}

				dataObject = result.Item;
				
				callback.Invoke(
					new AsyncResult<EzReUploadResult>(
						new EzReUploadResult(dataObject),
						null
					)
				);
			}
		}

		/// <summary>
		///  データのダウンロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectId">データのID</param>
		public IEnumerator Download(
			UnityAction<AsyncResult<EzDownloadResult>> callback,
			GameSession session,
			string namespaceName,
			string dataObjectId
		)
		{
			string downloadUrl;
			{
				EzPrepareDownloadResult result = null;
				yield return PrepareDownload(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzDownloadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					session,
					namespaceName,
					dataObjectId
				);
				
				if (result == null)
				{
					yield break;
				}

				downloadUrl = result.FileUrl;
			}
			{
				EzDownloadImplResult result = null;
				yield return DownloadImpl(
					r =>
					{
						if (r.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzDownloadResult>(
									null,
									r.Error
								)
							);
						}
						else
						{
							result = r.Result;
						}
					},
					downloadUrl
				);

				if (result == null)
				{
					yield break;
				}
				
				callback.Invoke(
					new AsyncResult<EzDownloadResult>(
						new EzDownloadResult(result.Data), 
						null
					)
				);
			}
		}
	}
}