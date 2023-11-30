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
using Gs2.Core.Exception;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using Gs2.Util.WebSocketSharp;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Datastore
{
    public partial class Client
    {
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
			using var request = UnityWebRequest.Put(uploadUrl, data);
			request.downloadHandler = new DownloadHandlerBuffer();
			yield return request.SendWebRequest();

			var result = new RestResult(
				(int) request.responseCode,
				request.responseCode == 200 ? "{}" : string.IsNullOrEmpty(request.error) ? "{}" : request.error
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
			using var request = UnityWebRequest.Get(downloadUrl);
			request.downloadHandler = new DownloadHandlerBuffer();
			yield return request.SendWebRequest();

			try
			{
				var result = new RestResult(
					(int) request.responseCode,
					request.responseCode == 200 ? "{}" : string.IsNullOrEmpty(request.error) ? "{}" : request.error
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
			catch (JsonException)
			{
				callback.Invoke(
					new AsyncResult<EzDownloadImplResult>(
						null,
						new BadGatewayException(new[]
						{
							new RequestError(
								"Download",
								request.error
							)
						})
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
		/// <param name="name">データの名前</param>
		/// <param name="scope">ファイルのアクセス権</param>
		/// <param name="allowUserIds">公開するユーザIDリスト</param>
		/// <param name="data">アップロードするデータ</param>
		/// <param name="updateIfExists">既にデータが存在する場合にエラーとするか、データを更新するか</param>
		public IEnumerator Upload(
			UnityAction<AsyncResult<EzUploadResult>> callback,
			GameSession session,
			string namespaceName,
			string scope,
			List<string> allowUserIds,
			byte[] data,
			string name=null,
			string contentType=null,
			bool? updateIfExists=null
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
					name,
					scope,
					contentType,
					allowUserIds,
					updateIfExists
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
			var isDataAlreadyRestored = false;

			while (true)
			{
				EzDataObject dataObject;
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

					dataObject = result.Item;
					downloadUrl = result.FileUrl;
				}
				{
					AsyncResult<EzDownloadImplResult> asyncDownloadImplResult = null;

					yield return DownloadImpl(
						r => asyncDownloadImplResult = r,
						downloadUrl
					);

					// Not Found だった場合、データの不整合を疑って1回だけ修復を試みる
					if (!isDataAlreadyRestored && asyncDownloadImplResult.Error is NotFoundException)
					{
						AsyncResult<EzRestoreDataObjectResult> asyncRestoreDataObjectResult = null;

						yield return RestoreDataObject(
							r => asyncRestoreDataObjectResult = r,
							namespaceName,
							dataObject.DataObjectId
						);
						
						if (asyncDownloadImplResult.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzDownloadResult>(
									null,
									asyncRestoreDataObjectResult.Error
								)
							);
							yield break;
						}

						isDataAlreadyRestored = true;

						continue;
					}

					callback.Invoke(
						asyncDownloadImplResult.Error == null
						? new AsyncResult<EzDownloadResult>(
							new EzDownloadResult(asyncDownloadImplResult.Result.Data), 
							null
						)
						: new AsyncResult<EzDownloadResult>(
							null,
							asyncDownloadImplResult.Error
						)
					);
				}

				break;
			}
		}

		/// <summary>
		///  自分のデータのダウンロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator DownloadOwnData(
			UnityAction<AsyncResult<EzDownloadResult>> callback,
			GameSession session,
			string namespaceName,
			string dataObjectName
		)
		{
			var isDataAlreadyRestored = false;

			while (true)
			{
				EzDataObject dataObject;
				string downloadUrl;
				{
					EzPrepareDownloadOwnDataResult result = null;
					yield return PrepareDownloadOwnData(
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
						dataObjectName
					);
				
					if (result == null)
					{
						yield break;
					}

					dataObject = result.Item;
					downloadUrl = result.FileUrl;
				}
				{
					AsyncResult<EzDownloadImplResult> asyncDownloadImplResult = null;

					yield return DownloadImpl(
						r => asyncDownloadImplResult = r,
						downloadUrl
					);

					// Not Found だった場合、データの不整合を疑って1回だけ修復を試みる
					if (!isDataAlreadyRestored && asyncDownloadImplResult.Error is NotFoundException)
					{
						AsyncResult<EzRestoreDataObjectResult> asyncRestoreDataObjectResult = null;

						yield return RestoreDataObject(
							r => asyncRestoreDataObjectResult = r,
							namespaceName,
							dataObject.DataObjectId
						);
						
						if (asyncDownloadImplResult.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzDownloadResult>(
									null,
									asyncRestoreDataObjectResult.Error
								)
							);
							yield break;
						}

						isDataAlreadyRestored = true;

						continue;
					}

					callback.Invoke(
						asyncDownloadImplResult.Error == null
						? new AsyncResult<EzDownloadResult>(
							new EzDownloadResult(asyncDownloadImplResult.Result.Data), 
							null
						)
						: new AsyncResult<EzDownloadResult>(
							null,
							asyncDownloadImplResult.Error
						)
					);
				}

				break;
			}
		}

		/// <summary>
		///  ユーザIDとデータ名を指定してデータをダウンロード<br />
		/// </summary>
		///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="userId">ユーザーID</param>
		/// <param name="dataObjectName">データの名前</param>
		public IEnumerator DownloadByUserIdAndDataObjectName(
			UnityAction<AsyncResult<EzDownloadResult>> callback,
			string namespaceName,
			string userId,
			string dataObjectName
		)
		{
			var isDataAlreadyRestored = false;

			while (true)
			{
				EzDataObject dataObject;
				string downloadUrl;
				{
					EzPrepareDownloadByUserIdAndDataObjectNameResult result = null;
					yield return PrepareDownloadByUserIdAndDataObjectName(
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
						namespaceName,
						userId,
						dataObjectName
					);
				
					if (result == null)
					{
						yield break;
					}

					dataObject = result.Item;
					downloadUrl = result.FileUrl;
				}
				{
					AsyncResult<EzDownloadImplResult> asyncDownloadImplResult = null;

					yield return DownloadImpl(
						r => asyncDownloadImplResult = r,
						downloadUrl
					);

					// Not Found だった場合、データの不整合を疑って1回だけ修復を試みる
					if (!isDataAlreadyRestored && asyncDownloadImplResult.Error is NotFoundException)
					{
						AsyncResult<EzRestoreDataObjectResult> asyncRestoreDataObjectResult = null;

						yield return RestoreDataObject(
							r => asyncRestoreDataObjectResult = r,
							namespaceName,
							dataObject.DataObjectId
						);
						
						if (asyncDownloadImplResult.Error != null)
						{
							callback.Invoke(
								new AsyncResult<EzDownloadResult>(
									null,
									asyncRestoreDataObjectResult.Error
								)
							);
							yield break;
						}

						isDataAlreadyRestored = true;

						continue;
					}

					callback.Invoke(
						asyncDownloadImplResult.Error == null
						? new AsyncResult<EzDownloadResult>(
							new EzDownloadResult(asyncDownloadImplResult.Result.Data), 
							null
						)
						: new AsyncResult<EzDownloadResult>(
							null,
							asyncDownloadImplResult.Error
						)
					);
				}

				break;
			}
		}
    }
}