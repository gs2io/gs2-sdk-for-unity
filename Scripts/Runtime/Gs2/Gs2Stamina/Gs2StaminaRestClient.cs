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

namespace Gs2.Gs2Stamina
{
	public class Gs2StaminaRestClient : AbstractGs2Client
	{

		public static string Endpoint = "stamina";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2StaminaRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
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
                    .Replace("{service}", "stamina")
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
                    .Replace("{service}", "stamina")
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
                if (_request.overflowTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("overflowTriggerScriptId");
                    jsonWriter.Write(_request.overflowTriggerScriptId.ToString());
                }
                if (_request.overflowTriggerNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("overflowTriggerNamespaceId");
                    jsonWriter.Write(_request.overflowTriggerNamespaceId.ToString());
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
                    .Replace("{service}", "stamina")
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
                    .Replace("{service}", "stamina")
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
                    .Replace("{service}", "stamina")
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
                if (_request.overflowTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("overflowTriggerScriptId");
                    jsonWriter.Write(_request.overflowTriggerScriptId.ToString());
                }
                if (_request.overflowTriggerNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("overflowTriggerNamespaceId");
                    jsonWriter.Write(_request.overflowTriggerNamespaceId.ToString());
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
                    .Replace("{service}", "stamina")
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

        private class DescribeStaminaModelMastersTask : Gs2RestSessionTask<Result.DescribeStaminaModelMastersResult>
        {
			private readonly Request.DescribeStaminaModelMastersRequest _request;

			public DescribeStaminaModelMastersTask(Request.DescribeStaminaModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeStaminaModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
		///  スタミナモデルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeStaminaModelMasters(
                Request.DescribeStaminaModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeStaminaModelMastersResult>> callback
        )
		{
			var task = new DescribeStaminaModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateStaminaModelMasterTask : Gs2RestSessionTask<Result.CreateStaminaModelMasterResult>
        {
			private readonly Request.CreateStaminaModelMasterRequest _request;

			public CreateStaminaModelMasterTask(Request.CreateStaminaModelMasterRequest request, UnityAction<AsyncResult<Result.CreateStaminaModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
                if (_request.recoverIntervalMinutes != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalMinutes");
                    jsonWriter.Write(_request.recoverIntervalMinutes.ToString());
                }
                if (_request.recoverValue != null)
                {
                    jsonWriter.WritePropertyName("recoverValue");
                    jsonWriter.Write(_request.recoverValue.ToString());
                }
                if (_request.initialCapacity != null)
                {
                    jsonWriter.WritePropertyName("initialCapacity");
                    jsonWriter.Write(_request.initialCapacity.ToString());
                }
                if (_request.isOverflow != null)
                {
                    jsonWriter.WritePropertyName("isOverflow");
                    jsonWriter.Write(_request.isOverflow.ToString());
                }
                if (_request.maxCapacity != null)
                {
                    jsonWriter.WritePropertyName("maxCapacity");
                    jsonWriter.Write(_request.maxCapacity.ToString());
                }
                if (_request.maxStaminaTableId != null)
                {
                    jsonWriter.WritePropertyName("maxStaminaTableId");
                    jsonWriter.Write(_request.maxStaminaTableId.ToString());
                }
                if (_request.recoverIntervalTableId != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalTableId");
                    jsonWriter.Write(_request.recoverIntervalTableId.ToString());
                }
                if (_request.recoverValueTableId != null)
                {
                    jsonWriter.WritePropertyName("recoverValueTableId");
                    jsonWriter.Write(_request.recoverValueTableId.ToString());
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
		///  スタミナモデルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateStaminaModelMaster(
                Request.CreateStaminaModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateStaminaModelMasterResult>> callback
        )
		{
			var task = new CreateStaminaModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStaminaModelMasterTask : Gs2RestSessionTask<Result.GetStaminaModelMasterResult>
        {
			private readonly Request.GetStaminaModelMasterRequest _request;

			public GetStaminaModelMasterTask(Request.GetStaminaModelMasterRequest request, UnityAction<AsyncResult<Result.GetStaminaModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

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
		///  スタミナモデルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStaminaModelMaster(
                Request.GetStaminaModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetStaminaModelMasterResult>> callback
        )
		{
			var task = new GetStaminaModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateStaminaModelMasterTask : Gs2RestSessionTask<Result.UpdateStaminaModelMasterResult>
        {
			private readonly Request.UpdateStaminaModelMasterRequest _request;

			public UpdateStaminaModelMasterTask(Request.UpdateStaminaModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateStaminaModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

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
                if (_request.recoverIntervalMinutes != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalMinutes");
                    jsonWriter.Write(_request.recoverIntervalMinutes.ToString());
                }
                if (_request.recoverValue != null)
                {
                    jsonWriter.WritePropertyName("recoverValue");
                    jsonWriter.Write(_request.recoverValue.ToString());
                }
                if (_request.initialCapacity != null)
                {
                    jsonWriter.WritePropertyName("initialCapacity");
                    jsonWriter.Write(_request.initialCapacity.ToString());
                }
                if (_request.isOverflow != null)
                {
                    jsonWriter.WritePropertyName("isOverflow");
                    jsonWriter.Write(_request.isOverflow.ToString());
                }
                if (_request.maxCapacity != null)
                {
                    jsonWriter.WritePropertyName("maxCapacity");
                    jsonWriter.Write(_request.maxCapacity.ToString());
                }
                if (_request.maxStaminaTableId != null)
                {
                    jsonWriter.WritePropertyName("maxStaminaTableId");
                    jsonWriter.Write(_request.maxStaminaTableId.ToString());
                }
                if (_request.recoverIntervalTableId != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalTableId");
                    jsonWriter.Write(_request.recoverIntervalTableId.ToString());
                }
                if (_request.recoverValueTableId != null)
                {
                    jsonWriter.WritePropertyName("recoverValueTableId");
                    jsonWriter.Write(_request.recoverValueTableId.ToString());
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
		///  スタミナモデルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateStaminaModelMaster(
                Request.UpdateStaminaModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateStaminaModelMasterResult>> callback
        )
		{
			var task = new UpdateStaminaModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteStaminaModelMasterTask : Gs2RestSessionTask<Result.DeleteStaminaModelMasterResult>
        {
			private readonly Request.DeleteStaminaModelMasterRequest _request;

			public DeleteStaminaModelMasterTask(Request.DeleteStaminaModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteStaminaModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/model/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

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
		///  スタミナモデルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteStaminaModelMaster(
                Request.DeleteStaminaModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteStaminaModelMasterResult>> callback
        )
		{
			var task = new DeleteStaminaModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeMaxStaminaTableMastersTask : Gs2RestSessionTask<Result.DescribeMaxStaminaTableMastersResult>
        {
			private readonly Request.DescribeMaxStaminaTableMastersRequest _request;

			public DescribeMaxStaminaTableMastersTask(Request.DescribeMaxStaminaTableMastersRequest request, UnityAction<AsyncResult<Result.DescribeMaxStaminaTableMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/maxStaminaTable";

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
		///  スタミナの最大値テーブルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeMaxStaminaTableMasters(
                Request.DescribeMaxStaminaTableMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeMaxStaminaTableMastersResult>> callback
        )
		{
			var task = new DescribeMaxStaminaTableMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateMaxStaminaTableMasterTask : Gs2RestSessionTask<Result.CreateMaxStaminaTableMasterResult>
        {
			private readonly Request.CreateMaxStaminaTableMasterRequest _request;

			public CreateMaxStaminaTableMasterTask(Request.CreateMaxStaminaTableMasterRequest request, UnityAction<AsyncResult<Result.CreateMaxStaminaTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/maxStaminaTable";

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナの最大値テーブルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateMaxStaminaTableMaster(
                Request.CreateMaxStaminaTableMasterRequest request,
                UnityAction<AsyncResult<Result.CreateMaxStaminaTableMasterResult>> callback
        )
		{
			var task = new CreateMaxStaminaTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetMaxStaminaTableMasterTask : Gs2RestSessionTask<Result.GetMaxStaminaTableMasterResult>
        {
			private readonly Request.GetMaxStaminaTableMasterRequest _request;

			public GetMaxStaminaTableMasterTask(Request.GetMaxStaminaTableMasterRequest request, UnityAction<AsyncResult<Result.GetMaxStaminaTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/maxStaminaTable/{maxStaminaTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{maxStaminaTableName}", !string.IsNullOrEmpty(_request.maxStaminaTableName) ? _request.maxStaminaTableName.ToString() : "null");

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
		///  スタミナの最大値テーブルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetMaxStaminaTableMaster(
                Request.GetMaxStaminaTableMasterRequest request,
                UnityAction<AsyncResult<Result.GetMaxStaminaTableMasterResult>> callback
        )
		{
			var task = new GetMaxStaminaTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateMaxStaminaTableMasterTask : Gs2RestSessionTask<Result.UpdateMaxStaminaTableMasterResult>
        {
			private readonly Request.UpdateMaxStaminaTableMasterRequest _request;

			public UpdateMaxStaminaTableMasterTask(Request.UpdateMaxStaminaTableMasterRequest request, UnityAction<AsyncResult<Result.UpdateMaxStaminaTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/maxStaminaTable/{maxStaminaTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{maxStaminaTableName}", !string.IsNullOrEmpty(_request.maxStaminaTableName) ? _request.maxStaminaTableName.ToString() : "null");

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナの最大値テーブルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateMaxStaminaTableMaster(
                Request.UpdateMaxStaminaTableMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateMaxStaminaTableMasterResult>> callback
        )
		{
			var task = new UpdateMaxStaminaTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteMaxStaminaTableMasterTask : Gs2RestSessionTask<Result.DeleteMaxStaminaTableMasterResult>
        {
			private readonly Request.DeleteMaxStaminaTableMasterRequest _request;

			public DeleteMaxStaminaTableMasterTask(Request.DeleteMaxStaminaTableMasterRequest request, UnityAction<AsyncResult<Result.DeleteMaxStaminaTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/maxStaminaTable/{maxStaminaTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{maxStaminaTableName}", !string.IsNullOrEmpty(_request.maxStaminaTableName) ? _request.maxStaminaTableName.ToString() : "null");

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
		///  スタミナの最大値テーブルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteMaxStaminaTableMaster(
                Request.DeleteMaxStaminaTableMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteMaxStaminaTableMasterResult>> callback
        )
		{
			var task = new DeleteMaxStaminaTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeRecoverIntervalTableMastersTask : Gs2RestSessionTask<Result.DescribeRecoverIntervalTableMastersResult>
        {
			private readonly Request.DescribeRecoverIntervalTableMastersRequest _request;

			public DescribeRecoverIntervalTableMastersTask(Request.DescribeRecoverIntervalTableMastersRequest request, UnityAction<AsyncResult<Result.DescribeRecoverIntervalTableMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverIntervalTable";

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
		///  スタミナ回復間隔テーブルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeRecoverIntervalTableMasters(
                Request.DescribeRecoverIntervalTableMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeRecoverIntervalTableMastersResult>> callback
        )
		{
			var task = new DescribeRecoverIntervalTableMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateRecoverIntervalTableMasterTask : Gs2RestSessionTask<Result.CreateRecoverIntervalTableMasterResult>
        {
			private readonly Request.CreateRecoverIntervalTableMasterRequest _request;

			public CreateRecoverIntervalTableMasterTask(Request.CreateRecoverIntervalTableMasterRequest request, UnityAction<AsyncResult<Result.CreateRecoverIntervalTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverIntervalTable";

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナ回復間隔テーブルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateRecoverIntervalTableMaster(
                Request.CreateRecoverIntervalTableMasterRequest request,
                UnityAction<AsyncResult<Result.CreateRecoverIntervalTableMasterResult>> callback
        )
		{
			var task = new CreateRecoverIntervalTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetRecoverIntervalTableMasterTask : Gs2RestSessionTask<Result.GetRecoverIntervalTableMasterResult>
        {
			private readonly Request.GetRecoverIntervalTableMasterRequest _request;

			public GetRecoverIntervalTableMasterTask(Request.GetRecoverIntervalTableMasterRequest request, UnityAction<AsyncResult<Result.GetRecoverIntervalTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverIntervalTable/{recoverIntervalTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverIntervalTableName}", !string.IsNullOrEmpty(_request.recoverIntervalTableName) ? _request.recoverIntervalTableName.ToString() : "null");

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
		///  スタミナ回復間隔テーブルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRecoverIntervalTableMaster(
                Request.GetRecoverIntervalTableMasterRequest request,
                UnityAction<AsyncResult<Result.GetRecoverIntervalTableMasterResult>> callback
        )
		{
			var task = new GetRecoverIntervalTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateRecoverIntervalTableMasterTask : Gs2RestSessionTask<Result.UpdateRecoverIntervalTableMasterResult>
        {
			private readonly Request.UpdateRecoverIntervalTableMasterRequest _request;

			public UpdateRecoverIntervalTableMasterTask(Request.UpdateRecoverIntervalTableMasterRequest request, UnityAction<AsyncResult<Result.UpdateRecoverIntervalTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverIntervalTable/{recoverIntervalTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverIntervalTableName}", !string.IsNullOrEmpty(_request.recoverIntervalTableName) ? _request.recoverIntervalTableName.ToString() : "null");

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナ回復間隔テーブルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateRecoverIntervalTableMaster(
                Request.UpdateRecoverIntervalTableMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateRecoverIntervalTableMasterResult>> callback
        )
		{
			var task = new UpdateRecoverIntervalTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteRecoverIntervalTableMasterTask : Gs2RestSessionTask<Result.DeleteRecoverIntervalTableMasterResult>
        {
			private readonly Request.DeleteRecoverIntervalTableMasterRequest _request;

			public DeleteRecoverIntervalTableMasterTask(Request.DeleteRecoverIntervalTableMasterRequest request, UnityAction<AsyncResult<Result.DeleteRecoverIntervalTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverIntervalTable/{recoverIntervalTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverIntervalTableName}", !string.IsNullOrEmpty(_request.recoverIntervalTableName) ? _request.recoverIntervalTableName.ToString() : "null");

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
		///  スタミナ回復間隔テーブルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRecoverIntervalTableMaster(
                Request.DeleteRecoverIntervalTableMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteRecoverIntervalTableMasterResult>> callback
        )
		{
			var task = new DeleteRecoverIntervalTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeRecoverValueTableMastersTask : Gs2RestSessionTask<Result.DescribeRecoverValueTableMastersResult>
        {
			private readonly Request.DescribeRecoverValueTableMastersRequest _request;

			public DescribeRecoverValueTableMastersTask(Request.DescribeRecoverValueTableMastersRequest request, UnityAction<AsyncResult<Result.DescribeRecoverValueTableMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverValueTable";

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
		///  スタミナ回復量テーブルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeRecoverValueTableMasters(
                Request.DescribeRecoverValueTableMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeRecoverValueTableMastersResult>> callback
        )
		{
			var task = new DescribeRecoverValueTableMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateRecoverValueTableMasterTask : Gs2RestSessionTask<Result.CreateRecoverValueTableMasterResult>
        {
			private readonly Request.CreateRecoverValueTableMasterRequest _request;

			public CreateRecoverValueTableMasterTask(Request.CreateRecoverValueTableMasterRequest request, UnityAction<AsyncResult<Result.CreateRecoverValueTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverValueTable";

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナ回復量テーブルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateRecoverValueTableMaster(
                Request.CreateRecoverValueTableMasterRequest request,
                UnityAction<AsyncResult<Result.CreateRecoverValueTableMasterResult>> callback
        )
		{
			var task = new CreateRecoverValueTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetRecoverValueTableMasterTask : Gs2RestSessionTask<Result.GetRecoverValueTableMasterResult>
        {
			private readonly Request.GetRecoverValueTableMasterRequest _request;

			public GetRecoverValueTableMasterTask(Request.GetRecoverValueTableMasterRequest request, UnityAction<AsyncResult<Result.GetRecoverValueTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverValueTable/{recoverValueTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverValueTableName}", !string.IsNullOrEmpty(_request.recoverValueTableName) ? _request.recoverValueTableName.ToString() : "null");

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
		///  スタミナ回復量テーブルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRecoverValueTableMaster(
                Request.GetRecoverValueTableMasterRequest request,
                UnityAction<AsyncResult<Result.GetRecoverValueTableMasterResult>> callback
        )
		{
			var task = new GetRecoverValueTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateRecoverValueTableMasterTask : Gs2RestSessionTask<Result.UpdateRecoverValueTableMasterResult>
        {
			private readonly Request.UpdateRecoverValueTableMasterRequest _request;

			public UpdateRecoverValueTableMasterTask(Request.UpdateRecoverValueTableMasterRequest request, UnityAction<AsyncResult<Result.UpdateRecoverValueTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverValueTable/{recoverValueTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverValueTableName}", !string.IsNullOrEmpty(_request.recoverValueTableName) ? _request.recoverValueTableName.ToString() : "null");

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
                if (_request.experienceModelId != null)
                {
                    jsonWriter.WritePropertyName("experienceModelId");
                    jsonWriter.Write(_request.experienceModelId.ToString());
                }
                if (_request.values != null)
                {
                    jsonWriter.WritePropertyName("values");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.values)
                    {
                        jsonWriter.Write(item.Value);
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
		///  スタミナ回復量テーブルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateRecoverValueTableMaster(
                Request.UpdateRecoverValueTableMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateRecoverValueTableMasterResult>> callback
        )
		{
			var task = new UpdateRecoverValueTableMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteRecoverValueTableMasterTask : Gs2RestSessionTask<Result.DeleteRecoverValueTableMasterResult>
        {
			private readonly Request.DeleteRecoverValueTableMasterRequest _request;

			public DeleteRecoverValueTableMasterTask(Request.DeleteRecoverValueTableMasterRequest request, UnityAction<AsyncResult<Result.DeleteRecoverValueTableMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/recoverValueTable/{recoverValueTableName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{recoverValueTableName}", !string.IsNullOrEmpty(_request.recoverValueTableName) ? _request.recoverValueTableName.ToString() : "null");

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
		///  スタミナ回復量テーブルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRecoverValueTableMaster(
                Request.DeleteRecoverValueTableMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteRecoverValueTableMasterResult>> callback
        )
		{
			var task = new DeleteRecoverValueTableMasterTask(request, callback);
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
                    .Replace("{service}", "stamina")
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
		///  現在有効なスタミナマスターのマスターデータをエクスポートします<br />
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

        private class GetCurrentStaminaMasterTask : Gs2RestSessionTask<Result.GetCurrentStaminaMasterResult>
        {
			private readonly Request.GetCurrentStaminaMasterRequest _request;

			public GetCurrentStaminaMasterTask(Request.GetCurrentStaminaMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentStaminaMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
		///  現在有効なスタミナマスターを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentStaminaMaster(
                Request.GetCurrentStaminaMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentStaminaMasterResult>> callback
        )
		{
			var task = new GetCurrentStaminaMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentStaminaMasterTask : Gs2RestSessionTask<Result.UpdateCurrentStaminaMasterResult>
        {
			private readonly Request.UpdateCurrentStaminaMasterRequest _request;

			public UpdateCurrentStaminaMasterTask(Request.UpdateCurrentStaminaMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentStaminaMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
		///  現在有効なスタミナマスターを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentStaminaMaster(
                Request.UpdateCurrentStaminaMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentStaminaMasterResult>> callback
        )
		{
			var task = new UpdateCurrentStaminaMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentStaminaMasterFromGitHubTask : Gs2RestSessionTask<Result.UpdateCurrentStaminaMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentStaminaMasterFromGitHubRequest _request;

			public UpdateCurrentStaminaMasterFromGitHubTask(Request.UpdateCurrentStaminaMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentStaminaMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
		///  現在有効なスタミナマスターを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentStaminaMasterFromGitHub(
                Request.UpdateCurrentStaminaMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentStaminaMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentStaminaMasterFromGitHubTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeStaminaModelsTask : Gs2RestSessionTask<Result.DescribeStaminaModelsResult>
        {
			private readonly Request.DescribeStaminaModelsRequest _request;

			public DescribeStaminaModelsTask(Request.DescribeStaminaModelsRequest request, UnityAction<AsyncResult<Result.DescribeStaminaModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
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
		///  スタミナモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeStaminaModels(
                Request.DescribeStaminaModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeStaminaModelsResult>> callback
        )
		{
			var task = new DescribeStaminaModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStaminaModelTask : Gs2RestSessionTask<Result.GetStaminaModelResult>
        {
			private readonly Request.GetStaminaModelRequest _request;

			public GetStaminaModelTask(Request.GetStaminaModelRequest request, UnityAction<AsyncResult<Result.GetStaminaModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/model/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

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
		///  スタミナモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStaminaModel(
                Request.GetStaminaModelRequest request,
                UnityAction<AsyncResult<Result.GetStaminaModelResult>> callback
        )
		{
			var task = new GetStaminaModelTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeStaminasTask : Gs2RestSessionTask<Result.DescribeStaminasResult>
        {
			private readonly Request.DescribeStaminasRequest _request;

			public DescribeStaminasTask(Request.DescribeStaminasRequest request, UnityAction<AsyncResult<Result.DescribeStaminasResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina";

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
		///  スタミナを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeStaminas(
                Request.DescribeStaminasRequest request,
                UnityAction<AsyncResult<Result.DescribeStaminasResult>> callback
        )
		{
			var task = new DescribeStaminasTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeStaminasByUserIdTask : Gs2RestSessionTask<Result.DescribeStaminasByUserIdResult>
        {
			private readonly Request.DescribeStaminasByUserIdRequest _request;

			public DescribeStaminasByUserIdTask(Request.DescribeStaminasByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeStaminasByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina";

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
		///  ユーザIDを指定してスタミナを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeStaminasByUserId(
                Request.DescribeStaminasByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeStaminasByUserIdResult>> callback
        )
		{
			var task = new DescribeStaminasByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStaminaTask : Gs2RestSessionTask<Result.GetStaminaResult>
        {
			private readonly Request.GetStaminaRequest _request;

			public GetStaminaTask(Request.GetStaminaRequest request, UnityAction<AsyncResult<Result.GetStaminaResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

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
		///  スタミナを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStamina(
                Request.GetStaminaRequest request,
                UnityAction<AsyncResult<Result.GetStaminaResult>> callback
        )
		{
			var task = new GetStaminaTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStaminaByUserIdTask : Gs2RestSessionTask<Result.GetStaminaByUserIdResult>
        {
			private readonly Request.GetStaminaByUserIdRequest _request;

			public GetStaminaByUserIdTask(Request.GetStaminaByUserIdRequest request, UnityAction<AsyncResult<Result.GetStaminaByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
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
		///  ユーザIDを指定してスタミナを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStaminaByUserId(
                Request.GetStaminaByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetStaminaByUserIdResult>> callback
        )
		{
			var task = new GetStaminaByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateStaminaByUserIdTask : Gs2RestSessionTask<Result.UpdateStaminaByUserIdResult>
        {
			private readonly Request.UpdateStaminaByUserIdRequest _request;

			public UpdateStaminaByUserIdTask(Request.UpdateStaminaByUserIdRequest request, UnityAction<AsyncResult<Result.UpdateStaminaByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.value != null)
                {
                    jsonWriter.WritePropertyName("value");
                    jsonWriter.Write(_request.value.ToString());
                }
                if (_request.maxValue != null)
                {
                    jsonWriter.WritePropertyName("maxValue");
                    jsonWriter.Write(_request.maxValue.ToString());
                }
                if (_request.recoverIntervalMinutes != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalMinutes");
                    jsonWriter.Write(_request.recoverIntervalMinutes.ToString());
                }
                if (_request.recoverValue != null)
                {
                    jsonWriter.WritePropertyName("recoverValue");
                    jsonWriter.Write(_request.recoverValue.ToString());
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
		///  ユーザIDを指定してスタミナを作成・更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateStaminaByUserId(
                Request.UpdateStaminaByUserIdRequest request,
                UnityAction<AsyncResult<Result.UpdateStaminaByUserIdResult>> callback
        )
		{
			var task = new UpdateStaminaByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeStaminaTask : Gs2RestSessionTask<Result.ConsumeStaminaResult>
        {
			private readonly Request.ConsumeStaminaRequest _request;

			public ConsumeStaminaTask(Request.ConsumeStaminaRequest request, UnityAction<AsyncResult<Result.ConsumeStaminaResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina/{staminaName}/consume";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.consumeValue != null)
                {
                    jsonWriter.WritePropertyName("consumeValue");
                    jsonWriter.Write(_request.consumeValue.ToString());
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
		///  スタミナを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeStamina(
                Request.ConsumeStaminaRequest request,
                UnityAction<AsyncResult<Result.ConsumeStaminaResult>> callback
        )
		{
			var task = new ConsumeStaminaTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeStaminaByUserIdTask : Gs2RestSessionTask<Result.ConsumeStaminaByUserIdResult>
        {
			private readonly Request.ConsumeStaminaByUserIdRequest _request;

			public ConsumeStaminaByUserIdTask(Request.ConsumeStaminaByUserIdRequest request, UnityAction<AsyncResult<Result.ConsumeStaminaByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/consume";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.consumeValue != null)
                {
                    jsonWriter.WritePropertyName("consumeValue");
                    jsonWriter.Write(_request.consumeValue.ToString());
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
		///  ユーザIDを指定してスタミナを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeStaminaByUserId(
                Request.ConsumeStaminaByUserIdRequest request,
                UnityAction<AsyncResult<Result.ConsumeStaminaByUserIdResult>> callback
        )
		{
			var task = new ConsumeStaminaByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class RecoverStaminaByUserIdTask : Gs2RestSessionTask<Result.RecoverStaminaByUserIdResult>
        {
			private readonly Request.RecoverStaminaByUserIdRequest _request;

			public RecoverStaminaByUserIdTask(Request.RecoverStaminaByUserIdRequest request, UnityAction<AsyncResult<Result.RecoverStaminaByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/recover";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.recoverValue != null)
                {
                    jsonWriter.WritePropertyName("recoverValue");
                    jsonWriter.Write(_request.recoverValue.ToString());
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
		///  ユーザIDを指定してスタミナを回復<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator RecoverStaminaByUserId(
                Request.RecoverStaminaByUserIdRequest request,
                UnityAction<AsyncResult<Result.RecoverStaminaByUserIdResult>> callback
        )
		{
			var task = new RecoverStaminaByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class RaiseMaxValueByUserIdTask : Gs2RestSessionTask<Result.RaiseMaxValueByUserIdResult>
        {
			private readonly Request.RaiseMaxValueByUserIdRequest _request;

			public RaiseMaxValueByUserIdTask(Request.RaiseMaxValueByUserIdRequest request, UnityAction<AsyncResult<Result.RaiseMaxValueByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/raise";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.raiseValue != null)
                {
                    jsonWriter.WritePropertyName("raiseValue");
                    jsonWriter.Write(_request.raiseValue.ToString());
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
		///  ユーザIDを指定してスタミナの最大値を加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator RaiseMaxValueByUserId(
                Request.RaiseMaxValueByUserIdRequest request,
                UnityAction<AsyncResult<Result.RaiseMaxValueByUserIdResult>> callback
        )
		{
			var task = new RaiseMaxValueByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetMaxValueByUserIdTask : Gs2RestSessionTask<Result.SetMaxValueByUserIdResult>
        {
			private readonly Request.SetMaxValueByUserIdRequest _request;

			public SetMaxValueByUserIdTask(Request.SetMaxValueByUserIdRequest request, UnityAction<AsyncResult<Result.SetMaxValueByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.maxValue != null)
                {
                    jsonWriter.WritePropertyName("maxValue");
                    jsonWriter.Write(_request.maxValue.ToString());
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
		///  ユーザIDを指定してスタミナの最大値を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetMaxValueByUserId(
                Request.SetMaxValueByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetMaxValueByUserIdResult>> callback
        )
		{
			var task = new SetMaxValueByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverIntervalByUserIdTask : Gs2RestSessionTask<Result.SetRecoverIntervalByUserIdResult>
        {
			private readonly Request.SetRecoverIntervalByUserIdRequest _request;

			public SetRecoverIntervalByUserIdTask(Request.SetRecoverIntervalByUserIdRequest request, UnityAction<AsyncResult<Result.SetRecoverIntervalByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/recoverInterval/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.recoverIntervalMinutes != null)
                {
                    jsonWriter.WritePropertyName("recoverIntervalMinutes");
                    jsonWriter.Write(_request.recoverIntervalMinutes.ToString());
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
		///  ユーザIDを指定してスタミナの回復間隔(分)を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverIntervalByUserId(
                Request.SetRecoverIntervalByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetRecoverIntervalByUserIdResult>> callback
        )
		{
			var task = new SetRecoverIntervalByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverValueByUserIdTask : Gs2RestSessionTask<Result.SetRecoverValueByUserIdResult>
        {
			private readonly Request.SetRecoverValueByUserIdRequest _request;

			public SetRecoverValueByUserIdTask(Request.SetRecoverValueByUserIdRequest request, UnityAction<AsyncResult<Result.SetRecoverValueByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}/recoverValue/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.recoverValue != null)
                {
                    jsonWriter.WritePropertyName("recoverValue");
                    jsonWriter.Write(_request.recoverValue.ToString());
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
		///  ユーザIDを指定してスタミナの回復間隔(分)を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverValueByUserId(
                Request.SetRecoverValueByUserIdRequest request,
                UnityAction<AsyncResult<Result.SetRecoverValueByUserIdResult>> callback
        )
		{
			var task = new SetRecoverValueByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetMaxValueByStatusTask : Gs2RestSessionTask<Result.SetMaxValueByStatusResult>
        {
			private readonly Request.SetMaxValueByStatusRequest _request;

			public SetMaxValueByStatusTask(Request.SetMaxValueByStatusRequest request, UnityAction<AsyncResult<Result.SetMaxValueByStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina/{staminaName}/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.signedStatusBody != null)
                {
                    jsonWriter.WritePropertyName("signedStatusBody");
                    jsonWriter.Write(_request.signedStatusBody.ToString());
                }
                if (_request.signedStatusSignature != null)
                {
                    jsonWriter.WritePropertyName("signedStatusSignature");
                    jsonWriter.Write(_request.signedStatusSignature.ToString());
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
		///  スタミナの最大値をGS2-Experienceのステータスを使用して更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetMaxValueByStatus(
                Request.SetMaxValueByStatusRequest request,
                UnityAction<AsyncResult<Result.SetMaxValueByStatusResult>> callback
        )
		{
			var task = new SetMaxValueByStatusTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverIntervalByStatusTask : Gs2RestSessionTask<Result.SetRecoverIntervalByStatusResult>
        {
			private readonly Request.SetRecoverIntervalByStatusRequest _request;

			public SetRecoverIntervalByStatusTask(Request.SetRecoverIntervalByStatusRequest request, UnityAction<AsyncResult<Result.SetRecoverIntervalByStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina/{staminaName}/recoverInterval/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.signedStatusBody != null)
                {
                    jsonWriter.WritePropertyName("signedStatusBody");
                    jsonWriter.Write(_request.signedStatusBody.ToString());
                }
                if (_request.signedStatusSignature != null)
                {
                    jsonWriter.WritePropertyName("signedStatusSignature");
                    jsonWriter.Write(_request.signedStatusSignature.ToString());
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
		///  スタミナの最大値をGS2-Experienceのステータスを使用して更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverIntervalByStatus(
                Request.SetRecoverIntervalByStatusRequest request,
                UnityAction<AsyncResult<Result.SetRecoverIntervalByStatusResult>> callback
        )
		{
			var task = new SetRecoverIntervalByStatusTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverValueByStatusTask : Gs2RestSessionTask<Result.SetRecoverValueByStatusResult>
        {
			private readonly Request.SetRecoverValueByStatusRequest _request;

			public SetRecoverValueByStatusTask(Request.SetRecoverValueByStatusRequest request, UnityAction<AsyncResult<Result.SetRecoverValueByStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/stamina/{staminaName}/reoverValue/set";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.signedStatusBody != null)
                {
                    jsonWriter.WritePropertyName("signedStatusBody");
                    jsonWriter.Write(_request.signedStatusBody.ToString());
                }
                if (_request.signedStatusSignature != null)
                {
                    jsonWriter.WritePropertyName("signedStatusSignature");
                    jsonWriter.Write(_request.signedStatusSignature.ToString());
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
		///  スタミナの最大値をGS2-Experienceのステータスを使用して更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverValueByStatus(
                Request.SetRecoverValueByStatusRequest request,
                UnityAction<AsyncResult<Result.SetRecoverValueByStatusResult>> callback
        )
		{
			var task = new SetRecoverValueByStatusTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteStaminaByUserIdTask : Gs2RestSessionTask<Result.DeleteStaminaByUserIdResult>
        {
			private readonly Request.DeleteStaminaByUserIdRequest _request;

			public DeleteStaminaByUserIdTask(Request.DeleteStaminaByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteStaminaByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/stamina/{staminaName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{staminaName}", !string.IsNullOrEmpty(_request.staminaName) ? _request.staminaName.ToString() : "null");
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
		///  ユーザIDを指定してスタミナを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteStaminaByUserId(
                Request.DeleteStaminaByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteStaminaByUserIdResult>> callback
        )
		{
			var task = new DeleteStaminaByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class RecoverStaminaByStampSheetTask : Gs2RestSessionTask<Result.RecoverStaminaByStampSheetResult>
        {
			private readonly Request.RecoverStaminaByStampSheetRequest _request;

			public RecoverStaminaByStampSheetTask(Request.RecoverStaminaByStampSheetRequest request, UnityAction<AsyncResult<Result.RecoverStaminaByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/recover";

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
		///  スタンプシートを使用してスタミナを回復<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator RecoverStaminaByStampSheet(
                Request.RecoverStaminaByStampSheetRequest request,
                UnityAction<AsyncResult<Result.RecoverStaminaByStampSheetResult>> callback
        )
		{
			var task = new RecoverStaminaByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class RaiseMaxValueByStampSheetTask : Gs2RestSessionTask<Result.RaiseMaxValueByStampSheetResult>
        {
			private readonly Request.RaiseMaxValueByStampSheetRequest _request;

			public RaiseMaxValueByStampSheetTask(Request.RaiseMaxValueByStampSheetRequest request, UnityAction<AsyncResult<Result.RaiseMaxValueByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/raise";

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
		///  スタンプシートでスタミナの最大値を加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator RaiseMaxValueByStampSheet(
                Request.RaiseMaxValueByStampSheetRequest request,
                UnityAction<AsyncResult<Result.RaiseMaxValueByStampSheetResult>> callback
        )
		{
			var task = new RaiseMaxValueByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetMaxValueByStampSheetTask : Gs2RestSessionTask<Result.SetMaxValueByStampSheetResult>
        {
			private readonly Request.SetMaxValueByStampSheetRequest _request;

			public SetMaxValueByStampSheetTask(Request.SetMaxValueByStampSheetRequest request, UnityAction<AsyncResult<Result.SetMaxValueByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/max/set";

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
		///  スタンプシートでスタミナの最大値を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetMaxValueByStampSheet(
                Request.SetMaxValueByStampSheetRequest request,
                UnityAction<AsyncResult<Result.SetMaxValueByStampSheetResult>> callback
        )
		{
			var task = new SetMaxValueByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverIntervalByStampSheetTask : Gs2RestSessionTask<Result.SetRecoverIntervalByStampSheetResult>
        {
			private readonly Request.SetRecoverIntervalByStampSheetRequest _request;

			public SetRecoverIntervalByStampSheetTask(Request.SetRecoverIntervalByStampSheetRequest request, UnityAction<AsyncResult<Result.SetRecoverIntervalByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/recoverInterval/set";

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
		///  スタンプシートでスタミナの最大値を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverIntervalByStampSheet(
                Request.SetRecoverIntervalByStampSheetRequest request,
                UnityAction<AsyncResult<Result.SetRecoverIntervalByStampSheetResult>> callback
        )
		{
			var task = new SetRecoverIntervalByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class SetRecoverValueByStampSheetTask : Gs2RestSessionTask<Result.SetRecoverValueByStampSheetResult>
        {
			private readonly Request.SetRecoverValueByStampSheetRequest _request;

			public SetRecoverValueByStampSheetTask(Request.SetRecoverValueByStampSheetRequest request, UnityAction<AsyncResult<Result.SetRecoverValueByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/recoverValue/set";

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
		///  スタンプシートでスタミナの最大値を更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SetRecoverValueByStampSheet(
                Request.SetRecoverValueByStampSheetRequest request,
                UnityAction<AsyncResult<Result.SetRecoverValueByStampSheetResult>> callback
        )
		{
			var task = new SetRecoverValueByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ConsumeStaminaByStampTaskTask : Gs2RestSessionTask<Result.ConsumeStaminaByStampTaskResult>
        {
			private readonly Request.ConsumeStaminaByStampTaskRequest _request;

			public ConsumeStaminaByStampTaskTask(Request.ConsumeStaminaByStampTaskRequest request, UnityAction<AsyncResult<Result.ConsumeStaminaByStampTaskResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "stamina")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamina/consume";

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
		///  スタンプタスクを使用してスタミナを消費<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ConsumeStaminaByStampTask(
                Request.ConsumeStaminaByStampTaskRequest request,
                UnityAction<AsyncResult<Result.ConsumeStaminaByStampTaskResult>> callback
        )
		{
			var task = new ConsumeStaminaByStampTaskTask(request, callback);
			return Gs2RestSession.Execute(task);
        }
	}
}