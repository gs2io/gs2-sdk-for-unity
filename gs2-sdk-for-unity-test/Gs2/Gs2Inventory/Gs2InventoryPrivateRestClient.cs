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
using UnityEngine.Events;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using LitJson;

namespace Gs2.Gs2Inventory
{
	public class Gs2InventoryPrivateRestClient : Gs2InventoryRestClient
	{

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="gs2RestSession">REST API 用セッション</param>
		public Gs2InventoryPrivateRestClient(Gs2RestSession gs2RestSession) : base(gs2RestSession)
		{

		}

        private class DescribeNamespacesByOwnerIdTask : Gs2RestSessionTask<Result.DescribeNamespacesByOwnerIdResult>
        {
			private readonly Request.DescribeNamespacesByOwnerIdRequest _request;

			public DescribeNamespacesByOwnerIdTask(Request.DescribeNamespacesByOwnerIdRequest request, UnityAction<AsyncResult<Result.DescribeNamespacesByOwnerIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/system/{ownerId}/namespace";

                url = url.Replace("{ownerId}", !string.IsNullOrEmpty(_request.ownerId) ? _request.ownerId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.pageToken != null) {
                    queryStrings.Add(string.Format("{0}={1}", "pageToken", UnityWebRequest.EscapeURL(_request.pageToken)));
                }
                if (_request.limit != null) {
                    queryStrings.Add(string.Format("{0}={1}", "limit", _request.limit));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  オーナーIDを指定してネームスペースの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeNamespacesByOwnerId(
                Request.DescribeNamespacesByOwnerIdRequest request,
                UnityAction<AsyncResult<Result.DescribeNamespacesByOwnerIdResult>> callback
        )
		{
			var task = new DescribeNamespacesByOwnerIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }
	}
}