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
	public class Gs2InventoryRestClient : AbstractGs2Client
	{

		public static string Endpoint = "inventory";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2InventoryRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

        private class DescribeNamespacesTask : Gs2RestSessionTask<Result.DescribeNamespacesResult>
        {
			private readonly Request.DescribeNamespacesRequest _request;

			public DescribeNamespacesTask(Request.DescribeNamespacesRequest request, UnityAction<AsyncResult<Result.DescribeNamespacesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/";

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
		///  ネームスペースの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeNamespaces(
                Request.DescribeNamespacesRequest request,
                UnityAction<AsyncResult<Result.DescribeNamespacesResult>> callback
        )
		{
			var task = new DescribeNamespacesTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateNamespaceTask : Gs2RestSessionTask<Result.CreateNamespaceResult>
        {
			private readonly Request.CreateNamespaceRequest _request;

			public CreateNamespaceTask(Request.CreateNamespaceRequest request, UnityAction<AsyncResult<Result.CreateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.acquireScript != null)
                {
                    jsonWriter.WritePropertyName("acquireScript");
                    _request.acquireScript.WriteJson(jsonWriter);
                }
                if (_request.overflowScript != null)
                {
                    jsonWriter.WritePropertyName("overflowScript");
                    _request.overflowScript.WriteJson(jsonWriter);
                }
                if (_request.consumeScript != null)
                {
                    jsonWriter.WritePropertyName("consumeScript");
                    _request.consumeScript.WriteJson(jsonWriter);
                }
                if (_request.logSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.logSetting.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  ネームスペースを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateNamespace(
                Request.CreateNamespaceRequest request,
                UnityAction<AsyncResult<Result.CreateNamespaceResult>> callback
        )
		{
			var task = new CreateNamespaceTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetNamespaceStatusTask : Gs2RestSessionTask<Result.GetNamespaceStatusResult>
        {
			private readonly Request.GetNamespaceStatusRequest _request;

			public GetNamespaceStatusTask(Request.GetNamespaceStatusRequest request, UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/status";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  ネームスペースの状態を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespaceStatus(
                Request.GetNamespaceStatusRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> callback
        )
		{
			var task = new GetNamespaceStatusTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetNamespaceTask : Gs2RestSessionTask<Result.GetNamespaceResult>
        {
			private readonly Request.GetNamespaceRequest _request;

			public GetNamespaceTask(Request.GetNamespaceRequest request, UnityAction<AsyncResult<Result.GetNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  ネームスペースを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespace(
                Request.GetNamespaceRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceResult>> callback
        )
		{
			var task = new GetNamespaceTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateNamespaceTask : Gs2RestSessionTask<Result.UpdateNamespaceResult>
        {
			private readonly Request.UpdateNamespaceRequest _request;

			public UpdateNamespaceTask(Request.UpdateNamespaceRequest request, UnityAction<AsyncResult<Result.UpdateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.acquireScript != null)
                {
                    jsonWriter.WritePropertyName("acquireScript");
                    _request.acquireScript.WriteJson(jsonWriter);
                }
                if (_request.overflowScript != null)
                {
                    jsonWriter.WritePropertyName("overflowScript");
                    _request.overflowScript.WriteJson(jsonWriter);
                }
                if (_request.consumeScript != null)
                {
                    jsonWriter.WritePropertyName("consumeScript");
                    _request.consumeScript.WriteJson(jsonWriter);
                }
                if (_request.logSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.logSetting.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  ネームスペースを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateNamespace(
                Request.UpdateNamespaceRequest request,
                UnityAction<AsyncResult<Result.UpdateNamespaceResult>> callback
        )
		{
			var task = new UpdateNamespaceTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteNamespaceTask : Gs2RestSessionTask<Result.DeleteNamespaceResult>
        {
			private readonly Request.DeleteNamespaceRequest _request;

			public DeleteNamespaceTask(Request.DeleteNamespaceRequest request, UnityAction<AsyncResult<Result.DeleteNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  ネームスペースを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteNamespace(
                Request.DeleteNamespaceRequest request,
                UnityAction<AsyncResult<Result.DeleteNamespaceResult>> callback
        )
		{
			var task = new DeleteNamespaceTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeInventoryModelMastersTask : Gs2RestSessionTask<Result.DescribeInventoryModelMastersResult>
        {
			private readonly Request.DescribeInventoryModelMastersRequest _request;

			public DescribeInventoryModelMastersTask(Request.DescribeInventoryModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeInventoryModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

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
		///  インベントリモデルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeInventoryModelMasters(
                Request.DescribeInventoryModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeInventoryModelMastersResult>> callback
        )
		{
			var task = new DescribeInventoryModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateInventoryModelMasterTask : Gs2RestSessionTask<Result.CreateInventoryModelMasterResult>
        {
			private readonly Request.CreateInventoryModelMasterRequest _request;

			public CreateInventoryModelMasterTask(Request.CreateInventoryModelMasterRequest request, UnityAction<AsyncResult<Result.CreateInventoryModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.initialCapacity != null)
                {
                    jsonWriter.WritePropertyName("initialCapacity");
                    jsonWriter.Write(_request.initialCapacity.ToString());
                }
                if (_request.maxCapacity != null)
                {
                    jsonWriter.WritePropertyName("maxCapacity");
                    jsonWriter.Write(_request.maxCapacity.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリモデルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateInventoryModelMaster(
                Request.CreateInventoryModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateInventoryModelMasterResult>> callback
        )
		{
			var task = new CreateInventoryModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetInventoryModelMasterTask : Gs2RestSessionTask<Result.GetInventoryModelMasterResult>
        {
			private readonly Request.GetInventoryModelMasterRequest _request;

			public GetInventoryModelMasterTask(Request.GetInventoryModelMasterRequest request, UnityAction<AsyncResult<Result.GetInventoryModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  インベントリモデルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetInventoryModelMaster(
                Request.GetInventoryModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetInventoryModelMasterResult>> callback
        )
		{
			var task = new GetInventoryModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateInventoryModelMasterTask : Gs2RestSessionTask<Result.UpdateInventoryModelMasterResult>
        {
			private readonly Request.UpdateInventoryModelMasterRequest _request;

			public UpdateInventoryModelMasterTask(Request.UpdateInventoryModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateInventoryModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.initialCapacity != null)
                {
                    jsonWriter.WritePropertyName("initialCapacity");
                    jsonWriter.Write(_request.initialCapacity.ToString());
                }
                if (_request.maxCapacity != null)
                {
                    jsonWriter.WritePropertyName("maxCapacity");
                    jsonWriter.Write(_request.maxCapacity.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリモデルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateInventoryModelMaster(
                Request.UpdateInventoryModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateInventoryModelMasterResult>> callback
        )
		{
			var task = new UpdateInventoryModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteInventoryModelMasterTask : Gs2RestSessionTask<Result.DeleteInventoryModelMasterResult>
        {
			private readonly Request.DeleteInventoryModelMasterRequest _request;

			public DeleteInventoryModelMasterTask(Request.DeleteInventoryModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteInventoryModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  インベントリモデルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteInventoryModelMaster(
                Request.DeleteInventoryModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteInventoryModelMasterResult>> callback
        )
		{
			var task = new DeleteInventoryModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeInventoryModelsTask : Gs2RestSessionTask<Result.DescribeInventoryModelsResult>
        {
			private readonly Request.DescribeInventoryModelsRequest _request;

			public DescribeInventoryModelsTask(Request.DescribeInventoryModelsRequest request, UnityAction<AsyncResult<Result.DescribeInventoryModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/inventory";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  インベントリモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeInventoryModels(
                Request.DescribeInventoryModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeInventoryModelsResult>> callback
        )
		{
			var task = new DescribeInventoryModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetInventoryModelTask : Gs2RestSessionTask<Result.GetInventoryModelResult>
        {
			private readonly Request.GetInventoryModelRequest _request;

			public GetInventoryModelTask(Request.GetInventoryModelRequest request, UnityAction<AsyncResult<Result.GetInventoryModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  インベントリモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetInventoryModel(
                Request.GetInventoryModelRequest request,
                UnityAction<AsyncResult<Result.GetInventoryModelResult>> callback
        )
		{
			var task = new GetInventoryModelTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeItemModelMastersTask : Gs2RestSessionTask<Result.DescribeItemModelMastersResult>
        {
			private readonly Request.DescribeItemModelMastersRequest _request;

			public DescribeItemModelMastersTask(Request.DescribeItemModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeItemModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}/item";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

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
		///  アイテムモデルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeItemModelMasters(
                Request.DescribeItemModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeItemModelMastersResult>> callback
        )
		{
			var task = new DescribeItemModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateItemModelMasterTask : Gs2RestSessionTask<Result.CreateItemModelMasterResult>
        {
			private readonly Request.CreateItemModelMasterRequest _request;

			public CreateItemModelMasterTask(Request.CreateItemModelMasterRequest request, UnityAction<AsyncResult<Result.CreateItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}/item";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.stackingLimit != null)
                {
                    jsonWriter.WritePropertyName("stackingLimit");
                    jsonWriter.Write(_request.stackingLimit.ToString());
                }
                if (_request.allowMultipleStacks != null)
                {
                    jsonWriter.WritePropertyName("allowMultipleStacks");
                    jsonWriter.Write(_request.allowMultipleStacks.ToString());
                }
                if (_request.sortValue != null)
                {
                    jsonWriter.WritePropertyName("sortValue");
                    jsonWriter.Write(_request.sortValue.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  アイテムモデルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateItemModelMaster(
                Request.CreateItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateItemModelMasterResult>> callback
        )
		{
			var task = new CreateItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemModelMasterTask : Gs2RestSessionTask<Result.GetItemModelMasterResult>
        {
			private readonly Request.GetItemModelMasterRequest _request;

			public GetItemModelMasterTask(Request.GetItemModelMasterRequest request, UnityAction<AsyncResult<Result.GetItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  アイテムモデルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemModelMaster(
                Request.GetItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetItemModelMasterResult>> callback
        )
		{
			var task = new GetItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateItemModelMasterTask : Gs2RestSessionTask<Result.UpdateItemModelMasterResult>
        {
			private readonly Request.UpdateItemModelMasterRequest _request;

			public UpdateItemModelMasterTask(Request.UpdateItemModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.stackingLimit != null)
                {
                    jsonWriter.WritePropertyName("stackingLimit");
                    jsonWriter.Write(_request.stackingLimit.ToString());
                }
                if (_request.allowMultipleStacks != null)
                {
                    jsonWriter.WritePropertyName("allowMultipleStacks");
                    jsonWriter.Write(_request.allowMultipleStacks.ToString());
                }
                if (_request.sortValue != null)
                {
                    jsonWriter.WritePropertyName("sortValue");
                    jsonWriter.Write(_request.sortValue.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  アイテムモデルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateItemModelMaster(
                Request.UpdateItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateItemModelMasterResult>> callback
        )
		{
			var task = new UpdateItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteItemModelMasterTask : Gs2RestSessionTask<Result.DeleteItemModelMasterResult>
        {
			private readonly Request.DeleteItemModelMasterRequest _request;

			public DeleteItemModelMasterTask(Request.DeleteItemModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  アイテムモデルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteItemModelMaster(
                Request.DeleteItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteItemModelMasterResult>> callback
        )
		{
			var task = new DeleteItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeItemModelsTask : Gs2RestSessionTask<Result.DescribeItemModelsResult>
        {
			private readonly Request.DescribeItemModelsRequest _request;

			public DescribeItemModelsTask(Request.DescribeItemModelsRequest request, UnityAction<AsyncResult<Result.DescribeItemModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/inventory/{inventoryName}/item";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  Noneの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeItemModels(
                Request.DescribeItemModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeItemModelsResult>> callback
        )
		{
			var task = new DescribeItemModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemModelTask : Gs2RestSessionTask<Result.GetItemModelResult>
        {
			private readonly Request.GetItemModelRequest _request;

			public GetItemModelTask(Request.GetItemModelRequest request, UnityAction<AsyncResult<Result.GetItemModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  Noneを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemModel(
                Request.GetItemModelRequest request,
                UnityAction<AsyncResult<Result.GetItemModelResult>> callback
        )
		{
			var task = new GetItemModelTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ExportMasterTask : Gs2RestSessionTask<Result.ExportMasterResult>
        {
			private readonly Request.ExportMasterRequest _request;

			public ExportMasterTask(Request.ExportMasterRequest request, UnityAction<AsyncResult<Result.ExportMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/export";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  現在有効な所持品マスターのマスターデータをエクスポートします<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ExportMaster(
                Request.ExportMasterRequest request,
                UnityAction<AsyncResult<Result.ExportMasterResult>> callback
        )
		{
			var task = new ExportMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCurrentItemModelMasterTask : Gs2RestSessionTask<Result.GetCurrentItemModelMasterResult>
        {
			private readonly Request.GetCurrentItemModelMasterRequest _request;

			public GetCurrentItemModelMasterTask(Request.GetCurrentItemModelMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
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
		///  現在有効な所持品マスターを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentItemModelMaster(
                Request.GetCurrentItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentItemModelMasterResult>> callback
        )
		{
			var task = new GetCurrentItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentItemModelMasterTask : Gs2RestSessionTask<Result.UpdateCurrentItemModelMasterResult>
        {
			private readonly Request.UpdateCurrentItemModelMasterRequest _request;

			public UpdateCurrentItemModelMasterTask(Request.UpdateCurrentItemModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentItemModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.settings != null)
                {
                    jsonWriter.WritePropertyName("settings");
                    jsonWriter.Write(_request.settings.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  現在有効な所持品マスターを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentItemModelMaster(
                Request.UpdateCurrentItemModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentItemModelMasterResult>> callback
        )
		{
			var task = new UpdateCurrentItemModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentItemModelMasterFromGitHubTask : Gs2RestSessionTask<Result.UpdateCurrentItemModelMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentItemModelMasterFromGitHubRequest _request;

			public UpdateCurrentItemModelMasterFromGitHubTask(Request.UpdateCurrentItemModelMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentItemModelMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/from_git_hub";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.checkoutSetting != null)
                {
                    jsonWriter.WritePropertyName("checkoutSetting");
                    _request.checkoutSetting.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  現在有効な所持品マスターを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentItemModelMasterFromGitHub(
                Request.UpdateCurrentItemModelMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentItemModelMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentItemModelMasterFromGitHubTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeInventoriesTask : Gs2RestSessionTask<Result.DescribeInventoriesResult>
        {
			private readonly Request.DescribeInventoriesRequest _request;

			public DescribeInventoriesTask(Request.DescribeInventoriesRequest request, UnityAction<AsyncResult<Result.DescribeInventoriesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

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
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeInventories(
                Request.DescribeInventoriesRequest request,
                UnityAction<AsyncResult<Result.DescribeInventoriesResult>> callback
        )
		{
			var task = new DescribeInventoriesTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeInventoriesByUserIdTask : Gs2RestSessionTask<Result.DescribeInventoriesByUserIdResult>
        {
			private readonly Request.DescribeInventoriesByUserIdRequest _request;

			public DescribeInventoriesByUserIdTask(Request.DescribeInventoriesByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeInventoriesByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

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
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeInventoriesByUserId(
                Request.DescribeInventoriesByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeInventoriesByUserIdResult>> callback
        )
		{
			var task = new DescribeInventoriesByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetInventoryTask : Gs2RestSessionTask<Result.GetInventoryResult>
        {
			private readonly Request.GetInventoryRequest _request;

			public GetInventoryTask(Request.GetInventoryRequest request, UnityAction<AsyncResult<Result.GetInventoryResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetInventory(
                Request.GetInventoryRequest request,
                UnityAction<AsyncResult<Result.GetInventoryResult>> callback
        )
		{
			var task = new GetInventoryTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetInventoryByUserIdTask : Gs2RestSessionTask<Result.GetInventoryByUserIdResult>
        {
			private readonly Request.GetInventoryByUserIdRequest _request;

			public GetInventoryByUserIdTask(Request.GetInventoryByUserIdRequest request, UnityAction<AsyncResult<Result.GetInventoryByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetInventoryByUserId(
                Request.GetInventoryByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetInventoryByUserIdResult>> callback
        )
		{
			var task = new GetInventoryByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class AddCapacityByUserIdTask : Gs2RestSessionTask<Result.AddCapacityByUserIdResult>
        {
			private readonly Request.AddCapacityByUserIdRequest _request;

			public AddCapacityByUserIdTask(Request.AddCapacityByUserIdRequest request, UnityAction<AsyncResult<Result.AddCapacityByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/capacity";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.addCapacityValue != null)
                {
                    jsonWriter.WritePropertyName("addCapacityValue");
                    jsonWriter.Write(_request.addCapacityValue.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  キャパシティサイズを加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AddCapacityByUserId(
                Request.AddCapacityByUserIdRequest request,
                UnityAction<AsyncResult<Result.AddCapacityByUserIdResult>> callback
        )
		{
			var task = new AddCapacityByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetCapacityByUserIdTask : Gs2RestSessionTask<Result.SetCapacityByUserIdResult>
        {
			private readonly Request.SetCapacityByUserIdRequest _request;

			public SetCapacityByUserIdTask(Request.SetCapacityByUserIdRequest request, UnityAction<AsyncResult<Result.SetCapacityByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/capacity";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.newCapacityValue != null)
                {
                    jsonWriter.WritePropertyName("newCapacityValue");
                    jsonWriter.Write(_request.newCapacityValue.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  キャパシティサイズを設定<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetCapacityByUserId(
                Request.SetCapacityByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetCapacityByUserIdResult>> callback
        )
		{
			var task = new SetCapacityByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteInventoryByUserIdTask : Gs2RestSessionTask<Result.DeleteInventoryByUserIdResult>
        {
			private readonly Request.DeleteInventoryByUserIdRequest _request;

			public DeleteInventoryByUserIdTask(Request.DeleteInventoryByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteInventoryByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteInventoryByUserId(
                Request.DeleteInventoryByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteInventoryByUserIdResult>> callback
        )
		{
			var task = new DeleteInventoryByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class AddCapacityByStampSheetTask : Gs2RestSessionTask<Result.AddCapacityByStampSheetResult>
        {
			private readonly Request.AddCapacityByStampSheetRequest _request;

			public AddCapacityByStampSheetTask(Request.AddCapacityByStampSheetRequest request, UnityAction<AsyncResult<Result.AddCapacityByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/inventory/capacity/add";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.stampSheet != null)
                {
                    jsonWriter.WritePropertyName("stampSheet");
                    jsonWriter.Write(_request.stampSheet.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  スタンプシートでキャパシティサイズを加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AddCapacityByStampSheet(
                Request.AddCapacityByStampSheetRequest request,
                UnityAction<AsyncResult<Result.AddCapacityByStampSheetResult>> callback
        )
		{
			var task = new AddCapacityByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetCapacityByStampSheetTask : Gs2RestSessionTask<Result.SetCapacityByStampSheetResult>
        {
			private readonly Request.SetCapacityByStampSheetRequest _request;

			public SetCapacityByStampSheetTask(Request.SetCapacityByStampSheetRequest request, UnityAction<AsyncResult<Result.SetCapacityByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/inventory/capacity/set";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.stampSheet != null)
                {
                    jsonWriter.WritePropertyName("stampSheet");
                    jsonWriter.Write(_request.stampSheet.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  スタンプシートでキャパシティサイズを設定<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetCapacityByStampSheet(
                Request.SetCapacityByStampSheetRequest request,
                UnityAction<AsyncResult<Result.SetCapacityByStampSheetResult>> callback
        )
		{
			var task = new SetCapacityByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeItemSetsTask : Gs2RestSessionTask<Result.DescribeItemSetsResult>
        {
			private readonly Request.DescribeItemSetsRequest _request;

			public DescribeItemSetsTask(Request.DescribeItemSetsRequest request, UnityAction<AsyncResult<Result.DescribeItemSetsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory/{inventoryName}/item";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");

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
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeItemSets(
                Request.DescribeItemSetsRequest request,
                UnityAction<AsyncResult<Result.DescribeItemSetsResult>> callback
        )
		{
			var task = new DescribeItemSetsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeItemSetsByUserIdTask : Gs2RestSessionTask<Result.DescribeItemSetsByUserIdResult>
        {
			private readonly Request.DescribeItemSetsByUserIdRequest _request;

			public DescribeItemSetsByUserIdTask(Request.DescribeItemSetsByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeItemSetsByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

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
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeItemSetsByUserId(
                Request.DescribeItemSetsByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeItemSetsByUserIdResult>> callback
        )
		{
			var task = new DescribeItemSetsByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemSetTask : Gs2RestSessionTask<Result.GetItemSetResult>
        {
			private readonly Request.GetItemSetRequest _request;

			public GetItemSetTask(Request.GetItemSetRequest request, UnityAction<AsyncResult<Result.GetItemSetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.itemSetName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "itemSetName", UnityWebRequest.EscapeURL(_request.itemSetName)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemSet(
                Request.GetItemSetRequest request,
                UnityAction<AsyncResult<Result.GetItemSetResult>> callback
        )
		{
			var task = new GetItemSetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemSetByUserIdTask : Gs2RestSessionTask<Result.GetItemSetByUserIdResult>
        {
			private readonly Request.GetItemSetByUserIdRequest _request;

			public GetItemSetByUserIdTask(Request.GetItemSetByUserIdRequest request, UnityAction<AsyncResult<Result.GetItemSetByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.itemSetName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "itemSetName", UnityWebRequest.EscapeURL(_request.itemSetName)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemSetByUserId(
                Request.GetItemSetByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetItemSetByUserIdResult>> callback
        )
		{
			var task = new GetItemSetByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemWithSignatureTask : Gs2RestSessionTask<Result.GetItemWithSignatureResult>
        {
			private readonly Request.GetItemWithSignatureRequest _request;

			public GetItemWithSignatureTask(Request.GetItemWithSignatureRequest request, UnityAction<AsyncResult<Result.GetItemWithSignatureResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory/{inventoryName}/item/{itemName}/signature";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.itemSetName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "itemSetName", UnityWebRequest.EscapeURL(_request.itemSetName)));
                }
                if (_request.keyId != null) {
                    queryStrings.Add(string.Format("{0}={1}", "keyId", UnityWebRequest.EscapeURL(_request.keyId)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemWithSignature(
                Request.GetItemWithSignatureRequest request,
                UnityAction<AsyncResult<Result.GetItemWithSignatureResult>> callback
        )
		{
			var task = new GetItemWithSignatureTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetItemWithSignatureByUserIdTask : Gs2RestSessionTask<Result.GetItemWithSignatureByUserIdResult>
        {
			private readonly Request.GetItemWithSignatureByUserIdRequest _request;

			public GetItemWithSignatureByUserIdTask(Request.GetItemWithSignatureByUserIdRequest request, UnityAction<AsyncResult<Result.GetItemWithSignatureByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item/{itemName}/signature";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.itemSetName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "itemSetName", UnityWebRequest.EscapeURL(_request.itemSetName)));
                }
                if (_request.keyId != null) {
                    queryStrings.Add(string.Format("{0}={1}", "keyId", UnityWebRequest.EscapeURL(_request.keyId)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetItemWithSignatureByUserId(
                Request.GetItemWithSignatureByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetItemWithSignatureByUserIdResult>> callback
        )
		{
			var task = new GetItemWithSignatureByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class AcquireItemSetByUserIdTask : Gs2RestSessionTask<Result.AcquireItemSetByUserIdResult>
        {
			private readonly Request.AcquireItemSetByUserIdRequest _request;

			public AcquireItemSetByUserIdTask(Request.AcquireItemSetByUserIdRequest request, UnityAction<AsyncResult<Result.AcquireItemSetByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item/{itemName}/acquire";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.acquireCount != null)
                {
                    jsonWriter.WritePropertyName("acquireCount");
                    jsonWriter.Write(_request.acquireCount.ToString());
                }
                if (_request.expiresAt != null)
                {
                    jsonWriter.WritePropertyName("expiresAt");
                    jsonWriter.Write(_request.expiresAt.ToString());
                }
                if (_request.createNewItemSet != null)
                {
                    jsonWriter.WritePropertyName("createNewItemSet");
                    jsonWriter.Write(_request.createNewItemSet.ToString());
                }
                if (_request.itemSetName != null)
                {
                    jsonWriter.WritePropertyName("itemSetName");
                    jsonWriter.Write(_request.itemSetName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  アイテムをインベントリに追加<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AcquireItemSetByUserId(
                Request.AcquireItemSetByUserIdRequest request,
                UnityAction<AsyncResult<Result.AcquireItemSetByUserIdResult>> callback
        )
		{
			var task = new AcquireItemSetByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeItemSetTask : Gs2RestSessionTask<Result.ConsumeItemSetResult>
        {
			private readonly Request.ConsumeItemSetRequest _request;

			public ConsumeItemSetTask(Request.ConsumeItemSetRequest request, UnityAction<AsyncResult<Result.ConsumeItemSetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/inventory/{inventoryName}/item/{itemName}/consume";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.consumeCount != null)
                {
                    jsonWriter.WritePropertyName("consumeCount");
                    jsonWriter.Write(_request.consumeCount.ToString());
                }
                if (_request.itemSetName != null)
                {
                    jsonWriter.WritePropertyName("itemSetName");
                    jsonWriter.Write(_request.itemSetName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.accessToken != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-ACCESS-TOKEN", _request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリのアイテムを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeItemSet(
                Request.ConsumeItemSetRequest request,
                UnityAction<AsyncResult<Result.ConsumeItemSetResult>> callback
        )
		{
			var task = new ConsumeItemSetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeItemSetByUserIdTask : Gs2RestSessionTask<Result.ConsumeItemSetByUserIdResult>
        {
			private readonly Request.ConsumeItemSetByUserIdRequest _request;

			public ConsumeItemSetByUserIdTask(Request.ConsumeItemSetByUserIdRequest request, UnityAction<AsyncResult<Result.ConsumeItemSetByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item/{itemName}/consume";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.consumeCount != null)
                {
                    jsonWriter.WritePropertyName("consumeCount");
                    jsonWriter.Write(_request.consumeCount.ToString());
                }
                if (_request.itemSetName != null)
                {
                    jsonWriter.WritePropertyName("itemSetName");
                    jsonWriter.Write(_request.itemSetName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  インベントリのアイテムを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeItemSetByUserId(
                Request.ConsumeItemSetByUserIdRequest request,
                UnityAction<AsyncResult<Result.ConsumeItemSetByUserIdResult>> callback
        )
		{
			var task = new ConsumeItemSetByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteItemSetByUserIdTask : Gs2RestSessionTask<Result.DeleteItemSetByUserIdResult>
        {
			private readonly Request.DeleteItemSetByUserIdRequest _request;

			public DeleteItemSetByUserIdTask(Request.DeleteItemSetByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteItemSetByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/inventory/{inventoryName}/item/{itemName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{inventoryName}", !string.IsNullOrEmpty(_request.inventoryName) ? _request.inventoryName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{itemName}", !string.IsNullOrEmpty(_request.itemName) ? _request.itemName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.itemSetName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "itemSetName", UnityWebRequest.EscapeURL(_request.itemSetName)));
                }
                url += "?" + string.Join("&", queryStrings.ToArray());

                UnityWebRequest.url = url;

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  有効期限ごとのアイテム所持数量を削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteItemSetByUserId(
                Request.DeleteItemSetByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteItemSetByUserIdResult>> callback
        )
		{
			var task = new DeleteItemSetByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class AcquireItemSetByStampSheetTask : Gs2RestSessionTask<Result.AcquireItemSetByStampSheetResult>
        {
			private readonly Request.AcquireItemSetByStampSheetRequest _request;

			public AcquireItemSetByStampSheetTask(Request.AcquireItemSetByStampSheetRequest request, UnityAction<AsyncResult<Result.AcquireItemSetByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/item/acquire";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.stampSheet != null)
                {
                    jsonWriter.WritePropertyName("stampSheet");
                    jsonWriter.Write(_request.stampSheet.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  スタンプシートでアイテムをインベントリに追加<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AcquireItemSetByStampSheet(
                Request.AcquireItemSetByStampSheetRequest request,
                UnityAction<AsyncResult<Result.AcquireItemSetByStampSheetResult>> callback
        )
		{
			var task = new AcquireItemSetByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeItemSetByStampTaskTask : Gs2RestSessionTask<Result.ConsumeItemSetByStampTaskResult>
        {
			private readonly Request.ConsumeItemSetByStampTaskRequest _request;

			public ConsumeItemSetByStampTaskTask(Request.ConsumeItemSetByStampTaskRequest request, UnityAction<AsyncResult<Result.ConsumeItemSetByStampTaskResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "inventory")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/item/consume";

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.stampTask != null)
                {
                    jsonWriter.WritePropertyName("stampTask");
                    jsonWriter.Write(_request.stampTask.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                jsonWriter.WriteObjectEnd();

                var body = stringBuilder.ToString();
                if (!string.IsNullOrEmpty(body))
                {
                    UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                }
                UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

                if (_request.requestId != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-REQUEST-ID", _request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  スタンプシートでインベントリのアイテムを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeItemSetByStampTask(
                Request.ConsumeItemSetByStampTaskRequest request,
                UnityAction<AsyncResult<Result.ConsumeItemSetByStampTaskResult>> callback
        )
		{
			var task = new ConsumeItemSetByStampTaskTask(request, callback);
			return Gs2RestSession.Execute(task);
        }
	}
}