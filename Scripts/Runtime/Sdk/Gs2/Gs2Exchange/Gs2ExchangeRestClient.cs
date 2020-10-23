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
using Gs2.Util.LitJson;

namespace Gs2.Gs2Exchange
{
	public class Gs2ExchangeRestClient : AbstractGs2Client
	{
		private readonly CertificateHandler _certificateHandler;

		public static string Endpoint = "exchange";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2ExchangeRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="gs2RestSession">REST API 用セッション</param>
		/// <param name="certificateHandler"></param>
		public Gs2ExchangeRestClient(Gs2RestSession gs2RestSession, CertificateHandler certificateHandler) : base(gs2RestSession)
		{
			_certificateHandler = certificateHandler;
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
                    .Replace("{service}", "exchange")
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
                if (_request.enableAwaitExchange != null)
                {
                    jsonWriter.WritePropertyName("enableAwaitExchange");
                    jsonWriter.Write(_request.enableAwaitExchange.ToString());
                }
                if (_request.enableDirectExchange != null)
                {
                    jsonWriter.WritePropertyName("enableDirectExchange");
                    jsonWriter.Write(_request.enableDirectExchange.ToString());
                }
                if (_request.queueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(_request.queueNamespaceId.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
		///  ネームスペースを取得<br />
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
                if (_request.enableAwaitExchange != null)
                {
                    jsonWriter.WritePropertyName("enableAwaitExchange");
                    jsonWriter.Write(_request.enableAwaitExchange.ToString());
                }
                if (_request.enableDirectExchange != null)
                {
                    jsonWriter.WritePropertyName("enableDirectExchange");
                    jsonWriter.Write(_request.enableDirectExchange.ToString());
                }
                if (_request.queueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("queueNamespaceId");
                    jsonWriter.Write(_request.queueNamespaceId.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeRateModelsTask : Gs2RestSessionTask<Result.DescribeRateModelsResult>
        {
			private readonly Request.DescribeRateModelsRequest _request;

			public DescribeRateModelsTask(Request.DescribeRateModelsRequest request, UnityAction<AsyncResult<Result.DescribeRateModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/model";

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
		///  交換レートモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeRateModels(
                Request.DescribeRateModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeRateModelsResult>> callback
        )
		{
			var task = new DescribeRateModelsTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetRateModelTask : Gs2RestSessionTask<Result.GetRateModelResult>
        {
			private readonly Request.GetRateModelRequest _request;

			public GetRateModelTask(Request.GetRateModelRequest request, UnityAction<AsyncResult<Result.GetRateModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/model/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

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
		///  交換レートモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRateModel(
                Request.GetRateModelRequest request,
                UnityAction<AsyncResult<Result.GetRateModelResult>> callback
        )
		{
			var task = new GetRateModelTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeRateModelMastersTask : Gs2RestSessionTask<Result.DescribeRateModelMastersResult>
        {
			private readonly Request.DescribeRateModelMastersRequest _request;

			public DescribeRateModelMastersTask(Request.DescribeRateModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeRateModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model";

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
		///  交換レートマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeRateModelMasters(
                Request.DescribeRateModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeRateModelMastersResult>> callback
        )
		{
			var task = new DescribeRateModelMastersTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class CreateRateModelMasterTask : Gs2RestSessionTask<Result.CreateRateModelMasterResult>
        {
			private readonly Request.CreateRateModelMasterRequest _request;

			public CreateRateModelMasterTask(Request.CreateRateModelMasterRequest request, UnityAction<AsyncResult<Result.CreateRateModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model";

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
                if (_request.timingType != null)
                {
                    jsonWriter.WritePropertyName("timingType");
                    jsonWriter.Write(_request.timingType.ToString());
                }
                if (_request.lockTime != null)
                {
                    jsonWriter.WritePropertyName("lockTime");
                    jsonWriter.Write(_request.lockTime.ToString());
                }
                if (_request.enableSkip != null)
                {
                    jsonWriter.WritePropertyName("enableSkip");
                    jsonWriter.Write(_request.enableSkip.ToString());
                }
                if (_request.skipConsumeActions != null)
                {
                    jsonWriter.WritePropertyName("skipConsumeActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.skipConsumeActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.acquireActions != null)
                {
                    jsonWriter.WritePropertyName("acquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.acquireActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.consumeActions != null)
                {
                    jsonWriter.WritePropertyName("consumeActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.consumeActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換レートマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateRateModelMaster(
                Request.CreateRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateRateModelMasterResult>> callback
        )
		{
			var task = new CreateRateModelMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetRateModelMasterTask : Gs2RestSessionTask<Result.GetRateModelMasterResult>
        {
			private readonly Request.GetRateModelMasterRequest _request;

			public GetRateModelMasterTask(Request.GetRateModelMasterRequest request, UnityAction<AsyncResult<Result.GetRateModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

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
		///  交換レートマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRateModelMaster(
                Request.GetRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetRateModelMasterResult>> callback
        )
		{
			var task = new GetRateModelMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class UpdateRateModelMasterTask : Gs2RestSessionTask<Result.UpdateRateModelMasterResult>
        {
			private readonly Request.UpdateRateModelMasterRequest _request;

			public UpdateRateModelMasterTask(Request.UpdateRateModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateRateModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

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
                if (_request.timingType != null)
                {
                    jsonWriter.WritePropertyName("timingType");
                    jsonWriter.Write(_request.timingType.ToString());
                }
                if (_request.lockTime != null)
                {
                    jsonWriter.WritePropertyName("lockTime");
                    jsonWriter.Write(_request.lockTime.ToString());
                }
                if (_request.enableSkip != null)
                {
                    jsonWriter.WritePropertyName("enableSkip");
                    jsonWriter.Write(_request.enableSkip.ToString());
                }
                if (_request.skipConsumeActions != null)
                {
                    jsonWriter.WritePropertyName("skipConsumeActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.skipConsumeActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.acquireActions != null)
                {
                    jsonWriter.WritePropertyName("acquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.acquireActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.consumeActions != null)
                {
                    jsonWriter.WritePropertyName("consumeActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.consumeActions)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換レートマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateRateModelMaster(
                Request.UpdateRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateRateModelMasterResult>> callback
        )
		{
			var task = new UpdateRateModelMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteRateModelMasterTask : Gs2RestSessionTask<Result.DeleteRateModelMasterResult>
        {
			private readonly Request.DeleteRateModelMasterRequest _request;

			public DeleteRateModelMasterTask(Request.DeleteRateModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteRateModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

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
		///  交換レートマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRateModelMaster(
                Request.DeleteRateModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteRateModelMasterResult>> callback
        )
		{
			var task = new DeleteRateModelMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class ExchangeTask : Gs2RestSessionTask<Result.ExchangeResult>
        {
			private readonly Request.ExchangeRequest _request;

			public ExchangeTask(Request.ExchangeRequest request, UnityAction<AsyncResult<Result.ExchangeResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.count != null)
                {
                    jsonWriter.WritePropertyName("count");
                    jsonWriter.Write(_request.count.ToString());
                }
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Exchange(
                Request.ExchangeRequest request,
                UnityAction<AsyncResult<Result.ExchangeResult>> callback
        )
		{
			var task = new ExchangeTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class ExchangeByUserIdTask : Gs2RestSessionTask<Result.ExchangeByUserIdResult>
        {
			private readonly Request.ExchangeByUserIdRequest _request;

			public ExchangeByUserIdTask(Request.ExchangeByUserIdRequest request, UnityAction<AsyncResult<Result.ExchangeByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.count != null)
                {
                    jsonWriter.WritePropertyName("count");
                    jsonWriter.Write(_request.count.ToString());
                }
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  ユーザIDを指定して交換を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ExchangeByUserId(
                Request.ExchangeByUserIdRequest request,
                UnityAction<AsyncResult<Result.ExchangeByUserIdResult>> callback
        )
		{
			var task = new ExchangeByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class ExchangeByStampSheetTask : Gs2RestSessionTask<Result.ExchangeByStampSheetResult>
        {
			private readonly Request.ExchangeByStampSheetRequest _request;

			public ExchangeByStampSheetTask(Request.ExchangeByStampSheetRequest request, UnityAction<AsyncResult<Result.ExchangeByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/exchange";

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
		///  スタンプシートで交換を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ExchangeByStampSheet(
                Request.ExchangeByStampSheetRequest request,
                UnityAction<AsyncResult<Result.ExchangeByStampSheetResult>> callback
        )
		{
			var task = new ExchangeByStampSheetTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
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
                    .Replace("{service}", "exchange")
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
		///  現在有効な交換レート設定のマスターデータをエクスポートします<br />
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
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetCurrentRateMasterTask : Gs2RestSessionTask<Result.GetCurrentRateMasterResult>
        {
			private readonly Request.GetCurrentRateMasterRequest _request;

			public GetCurrentRateMasterTask(Request.GetCurrentRateMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentRateMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
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
		///  現在有効な交換レート設定を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentRateMaster(
                Request.GetCurrentRateMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentRateMasterResult>> callback
        )
		{
			var task = new GetCurrentRateMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentRateMasterTask : Gs2RestSessionTask<Result.UpdateCurrentRateMasterResult>
        {
			private readonly Request.UpdateCurrentRateMasterRequest _request;

			public UpdateCurrentRateMasterTask(Request.UpdateCurrentRateMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentRateMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
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
		///  現在有効な交換レート設定を更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentRateMaster(
                Request.UpdateCurrentRateMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentRateMasterResult>> callback
        )
		{
			var task = new UpdateCurrentRateMasterTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentRateMasterFromGitHubTask : Gs2RestSessionTask<Result.UpdateCurrentRateMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentRateMasterFromGitHubRequest _request;

			public UpdateCurrentRateMasterFromGitHubTask(Request.UpdateCurrentRateMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentRateMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
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
		///  現在有効な交換レート設定を更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentRateMasterFromGitHub(
                Request.UpdateCurrentRateMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentRateMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentRateMasterFromGitHubTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class CreateAwaitByUserIdTask : Gs2RestSessionTask<Result.CreateAwaitByUserIdResult>
        {
			private readonly Request.CreateAwaitByUserIdRequest _request;

			public CreateAwaitByUserIdTask(Request.CreateAwaitByUserIdRequest request, UnityAction<AsyncResult<Result.CreateAwaitByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.count != null)
                {
                    jsonWriter.WritePropertyName("count");
                    jsonWriter.Write(_request.count.ToString());
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
		///  交換待機を作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateAwaitByUserId(
                Request.CreateAwaitByUserIdRequest request,
                UnityAction<AsyncResult<Result.CreateAwaitByUserIdResult>> callback
        )
		{
			var task = new CreateAwaitByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeAwaitsTask : Gs2RestSessionTask<Result.DescribeAwaitsResult>
        {
			private readonly Request.DescribeAwaitsRequest _request;

			public DescribeAwaitsTask(Request.DescribeAwaitsRequest request, UnityAction<AsyncResult<Result.DescribeAwaitsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/await";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.rateName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "rateName", UnityWebRequest.EscapeURL(_request.rateName)));
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
		///  交換待機の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeAwaits(
                Request.DescribeAwaitsRequest request,
                UnityAction<AsyncResult<Result.DescribeAwaitsResult>> callback
        )
		{
			var task = new DescribeAwaitsTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DescribeAwaitsByUserIdTask : Gs2RestSessionTask<Result.DescribeAwaitsByUserIdResult>
        {
			private readonly Request.DescribeAwaitsByUserIdRequest _request;

			public DescribeAwaitsByUserIdTask(Request.DescribeAwaitsByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeAwaitsByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/await";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                var queryStrings = new List<string> ();
                if (_request.contextStack != null)
                {
                    queryStrings.Add(string.Format("{0}={1}", "contextStack", UnityWebRequest.EscapeURL(_request.contextStack)));
                }
                if (_request.rateName != null) {
                    queryStrings.Add(string.Format("{0}={1}", "rateName", UnityWebRequest.EscapeURL(_request.rateName)));
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
		///  交換待機の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeAwaitsByUserId(
                Request.DescribeAwaitsByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeAwaitsByUserIdResult>> callback
        )
		{
			var task = new DescribeAwaitsByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetAwaitTask : Gs2RestSessionTask<Result.GetAwaitResult>
        {
			private readonly Request.GetAwaitRequest _request;

			public GetAwaitTask(Request.GetAwaitRequest request, UnityAction<AsyncResult<Result.GetAwaitResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

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
		///  交換待機を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetAwait(
                Request.GetAwaitRequest request,
                UnityAction<AsyncResult<Result.GetAwaitResult>> callback
        )
		{
			var task = new GetAwaitTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class GetAwaitByUserIdTask : Gs2RestSessionTask<Result.GetAwaitByUserIdResult>
        {
			private readonly Request.GetAwaitByUserIdRequest _request;

			public GetAwaitByUserIdTask(Request.GetAwaitByUserIdRequest request, UnityAction<AsyncResult<Result.GetAwaitByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

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
		///  交換待機を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetAwaitByUserId(
                Request.GetAwaitByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetAwaitByUserIdResult>> callback
        )
		{
			var task = new GetAwaitByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class AcquireTask : Gs2RestSessionTask<Result.AcquireResult>
        {
			private readonly Request.AcquireRequest _request;

			public AcquireTask(Request.AcquireRequest request, UnityAction<AsyncResult<Result.AcquireResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換待機の報酬を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Acquire(
                Request.AcquireRequest request,
                UnityAction<AsyncResult<Result.AcquireResult>> callback
        )
		{
			var task = new AcquireTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class AcquireByUserIdTask : Gs2RestSessionTask<Result.AcquireByUserIdResult>
        {
			private readonly Request.AcquireByUserIdRequest _request;

			public AcquireByUserIdTask(Request.AcquireByUserIdRequest request, UnityAction<AsyncResult<Result.AcquireByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換待機の報酬を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AcquireByUserId(
                Request.AcquireByUserIdRequest request,
                UnityAction<AsyncResult<Result.AcquireByUserIdResult>> callback
        )
		{
			var task = new AcquireByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class AcquireForceByUserIdTask : Gs2RestSessionTask<Result.AcquireForceByUserIdResult>
        {
			private readonly Request.AcquireForceByUserIdRequest _request;

			public AcquireForceByUserIdTask(Request.AcquireForceByUserIdRequest request, UnityAction<AsyncResult<Result.AcquireForceByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await/{awaitName}/force";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換待機の報酬を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AcquireForceByUserId(
                Request.AcquireForceByUserIdRequest request,
                UnityAction<AsyncResult<Result.AcquireForceByUserIdResult>> callback
        )
		{
			var task = new AcquireForceByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SkipTask : Gs2RestSessionTask<Result.SkipResult>
        {
			private readonly Request.SkipRequest _request;

			public SkipTask(Request.SkipRequest request, UnityAction<AsyncResult<Result.SkipResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/{rateName}/await/{awaitName}/skip";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換待機を対価を払ってスキップ<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Skip(
                Request.SkipRequest request,
                UnityAction<AsyncResult<Result.SkipResult>> callback
        )
		{
			var task = new SkipTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class SkipByUserIdTask : Gs2RestSessionTask<Result.SkipByUserIdResult>
        {
			private readonly Request.SkipByUserIdRequest _request;

			public SkipByUserIdTask(Request.SkipByUserIdRequest request, UnityAction<AsyncResult<Result.SkipByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await/{awaitName}/skip";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.config != null)
                {
                    jsonWriter.WritePropertyName("config");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.config)
                    {
                        if (item == null) {
                            jsonWriter.Write(null);
                        } else {
                            item.WriteJson(jsonWriter);
                        }
                    }
                    jsonWriter.WriteArrayEnd();
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
		///  交換待機を対価を払ってスキップ<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SkipByUserId(
                Request.SkipByUserIdRequest request,
                UnityAction<AsyncResult<Result.SkipByUserIdResult>> callback
        )
		{
			var task = new SkipByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteAwaitTask : Gs2RestSessionTask<Result.DeleteAwaitResult>
        {
			private readonly Request.DeleteAwaitRequest _request;

			public DeleteAwaitTask(Request.DeleteAwaitRequest request, UnityAction<AsyncResult<Result.DeleteAwaitResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

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
		///  交換待機を削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAwait(
                Request.DeleteAwaitRequest request,
                UnityAction<AsyncResult<Result.DeleteAwaitResult>> callback
        )
		{
			var task = new DeleteAwaitTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteAwaitByUserIdTask : Gs2RestSessionTask<Result.DeleteAwaitByUserIdResult>
        {
			private readonly Request.DeleteAwaitByUserIdRequest _request;

			public DeleteAwaitByUserIdTask(Request.DeleteAwaitByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteAwaitByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/exchange/{rateName}/await/{awaitName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{rateName}", !string.IsNullOrEmpty(_request.rateName) ? _request.rateName.ToString() : "null");
                url = url.Replace("{awaitName}", !string.IsNullOrEmpty(_request.awaitName) ? _request.awaitName.ToString() : "null");

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
		///  交換待機を削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAwaitByUserId(
                Request.DeleteAwaitByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteAwaitByUserIdResult>> callback
        )
		{
			var task = new DeleteAwaitByUserIdTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class CreateAwaitByStampSheetTask : Gs2RestSessionTask<Result.CreateAwaitByStampSheetResult>
        {
			private readonly Request.CreateAwaitByStampSheetRequest _request;

			public CreateAwaitByStampSheetTask(Request.CreateAwaitByStampSheetRequest request, UnityAction<AsyncResult<Result.CreateAwaitByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/await/create";

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
		///  スタンプシートで交換待機 を作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateAwaitByStampSheet(
                Request.CreateAwaitByStampSheetRequest request,
                UnityAction<AsyncResult<Result.CreateAwaitByStampSheetResult>> callback
        )
		{
			var task = new CreateAwaitByStampSheetTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }

        private class DeleteAwaitByStampTaskTask : Gs2RestSessionTask<Result.DeleteAwaitByStampTaskResult>
        {
			private readonly Request.DeleteAwaitByStampTaskRequest _request;

			public DeleteAwaitByStampTaskTask(Request.DeleteAwaitByStampTaskRequest request, UnityAction<AsyncResult<Result.DeleteAwaitByStampTaskResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "exchange")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/await/delete";

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
		///  スタンプタスクで 交換待機 を削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAwaitByStampTask(
                Request.DeleteAwaitByStampTaskRequest request,
                UnityAction<AsyncResult<Result.DeleteAwaitByStampTaskResult>> callback
        )
		{
			var task = new DeleteAwaitByStampTaskTask(request, callback);
			if (_certificateHandler != null)
			{
				task.UnityWebRequest.certificateHandler = _certificateHandler;
			}
			return Gs2RestSession.Execute(task);
        }
	}
}