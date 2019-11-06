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

namespace Gs2.Gs2Version
{
	public class Gs2VersionWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "version";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2VersionWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}

        private class DescribeNamespacesTask : Gs2WebSocketSessionTask<Result.DescribeNamespacesResult>
        {
			private readonly Request.DescribeNamespacesRequest _request;

			public DescribeNamespacesTask(Request.DescribeNamespacesRequest request, UnityAction<AsyncResult<Result.DescribeNamespacesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.pageToken != null)
                {
                    jsonWriter.WritePropertyName("pageToken");
                    jsonWriter.Write(_request.pageToken.ToString());
                }
                if (_request.limit != null)
                {
                    jsonWriter.WritePropertyName("limit");
                    jsonWriter.Write(_request.limit.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeNamespaces");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateNamespaceTask : Gs2WebSocketSessionTask<Result.CreateNamespaceResult>
        {
			private readonly Request.CreateNamespaceRequest _request;

			public CreateNamespaceTask(Request.CreateNamespaceRequest request, UnityAction<AsyncResult<Result.CreateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
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
                if (_request.assumeUserId != null)
                {
                    jsonWriter.WritePropertyName("assumeUserId");
                    jsonWriter.Write(_request.assumeUserId.ToString());
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
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetNamespaceStatusTask : Gs2WebSocketSessionTask<Result.GetNamespaceStatusResult>
        {
			private readonly Request.GetNamespaceStatusRequest _request;

			public GetNamespaceStatusTask(Request.GetNamespaceStatusRequest request, UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getNamespaceStatus");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetNamespaceTask : Gs2WebSocketSessionTask<Result.GetNamespaceResult>
        {
			private readonly Request.GetNamespaceRequest _request;

			public GetNamespaceTask(Request.GetNamespaceRequest request, UnityAction<AsyncResult<Result.GetNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateNamespaceTask : Gs2WebSocketSessionTask<Result.UpdateNamespaceResult>
        {
			private readonly Request.UpdateNamespaceRequest _request;

			public UpdateNamespaceTask(Request.UpdateNamespaceRequest request, UnityAction<AsyncResult<Result.UpdateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.assumeUserId != null)
                {
                    jsonWriter.WritePropertyName("assumeUserId");
                    jsonWriter.Write(_request.assumeUserId.ToString());
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
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteNamespaceTask : Gs2WebSocketSessionTask<Result.DeleteNamespaceResult>
        {
			private readonly Request.DeleteNamespaceRequest _request;

			public DeleteNamespaceTask(Request.DeleteNamespaceRequest request, UnityAction<AsyncResult<Result.DeleteNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeVersionModelMastersTask : Gs2WebSocketSessionTask<Result.DescribeVersionModelMastersResult>
        {
			private readonly Request.DescribeVersionModelMastersRequest _request;

			public DescribeVersionModelMastersTask(Request.DescribeVersionModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeVersionModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.pageToken != null)
                {
                    jsonWriter.WritePropertyName("pageToken");
                    jsonWriter.Write(_request.pageToken.ToString());
                }
                if (_request.limit != null)
                {
                    jsonWriter.WritePropertyName("limit");
                    jsonWriter.Write(_request.limit.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeVersionModelMasters");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeVersionModelMasters(
                Request.DescribeVersionModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeVersionModelMastersResult>> callback
        )
		{
			var task = new DescribeVersionModelMastersTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateVersionModelMasterTask : Gs2WebSocketSessionTask<Result.CreateVersionModelMasterResult>
        {
			private readonly Request.CreateVersionModelMasterRequest _request;

			public CreateVersionModelMasterTask(Request.CreateVersionModelMasterRequest request, UnityAction<AsyncResult<Result.CreateVersionModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
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
                if (_request.warningVersion != null)
                {
                    jsonWriter.WritePropertyName("warningVersion");
                    _request.warningVersion.WriteJson(jsonWriter);
                }
                if (_request.errorVersion != null)
                {
                    jsonWriter.WritePropertyName("errorVersion");
                    _request.errorVersion.WriteJson(jsonWriter);
                }
                if (_request.scope != null)
                {
                    jsonWriter.WritePropertyName("scope");
                    jsonWriter.Write(_request.scope.ToString());
                }
                if (_request.currentVersion != null)
                {
                    jsonWriter.WritePropertyName("currentVersion");
                    _request.currentVersion.WriteJson(jsonWriter);
                }
                if (_request.needSignature != null)
                {
                    jsonWriter.WritePropertyName("needSignature");
                    jsonWriter.Write(_request.needSignature.ToString());
                }
                if (_request.signatureKeyId != null)
                {
                    jsonWriter.WritePropertyName("signatureKeyId");
                    jsonWriter.Write(_request.signatureKeyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createVersionModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateVersionModelMaster(
                Request.CreateVersionModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateVersionModelMasterResult>> callback
        )
		{
			var task = new CreateVersionModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetVersionModelMasterTask : Gs2WebSocketSessionTask<Result.GetVersionModelMasterResult>
        {
			private readonly Request.GetVersionModelMasterRequest _request;

			public GetVersionModelMasterTask(Request.GetVersionModelMasterRequest request, UnityAction<AsyncResult<Result.GetVersionModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getVersionModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetVersionModelMaster(
                Request.GetVersionModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetVersionModelMasterResult>> callback
        )
		{
			var task = new GetVersionModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateVersionModelMasterTask : Gs2WebSocketSessionTask<Result.UpdateVersionModelMasterResult>
        {
			private readonly Request.UpdateVersionModelMasterRequest _request;

			public UpdateVersionModelMasterTask(Request.UpdateVersionModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateVersionModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
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
                if (_request.warningVersion != null)
                {
                    jsonWriter.WritePropertyName("warningVersion");
                    _request.warningVersion.WriteJson(jsonWriter);
                }
                if (_request.errorVersion != null)
                {
                    jsonWriter.WritePropertyName("errorVersion");
                    _request.errorVersion.WriteJson(jsonWriter);
                }
                if (_request.scope != null)
                {
                    jsonWriter.WritePropertyName("scope");
                    jsonWriter.Write(_request.scope.ToString());
                }
                if (_request.currentVersion != null)
                {
                    jsonWriter.WritePropertyName("currentVersion");
                    _request.currentVersion.WriteJson(jsonWriter);
                }
                if (_request.needSignature != null)
                {
                    jsonWriter.WritePropertyName("needSignature");
                    jsonWriter.Write(_request.needSignature.ToString());
                }
                if (_request.signatureKeyId != null)
                {
                    jsonWriter.WritePropertyName("signatureKeyId");
                    jsonWriter.Write(_request.signatureKeyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateVersionModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateVersionModelMaster(
                Request.UpdateVersionModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateVersionModelMasterResult>> callback
        )
		{
			var task = new UpdateVersionModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteVersionModelMasterTask : Gs2WebSocketSessionTask<Result.DeleteVersionModelMasterResult>
        {
			private readonly Request.DeleteVersionModelMasterRequest _request;

			public DeleteVersionModelMasterTask(Request.DeleteVersionModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteVersionModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteVersionModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteVersionModelMaster(
                Request.DeleteVersionModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteVersionModelMasterResult>> callback
        )
		{
			var task = new DeleteVersionModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeVersionModelsTask : Gs2WebSocketSessionTask<Result.DescribeVersionModelsResult>
        {
			private readonly Request.DescribeVersionModelsRequest _request;

			public DescribeVersionModelsTask(Request.DescribeVersionModelsRequest request, UnityAction<AsyncResult<Result.DescribeVersionModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModel");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeVersionModels");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョン設定の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeVersionModels(
                Request.DescribeVersionModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeVersionModelsResult>> callback
        )
		{
			var task = new DescribeVersionModelsTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetVersionModelTask : Gs2WebSocketSessionTask<Result.GetVersionModelResult>
        {
			private readonly Request.GetVersionModelRequest _request;

			public GetVersionModelTask(Request.GetVersionModelRequest request, UnityAction<AsyncResult<Result.GetVersionModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("versionModel");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getVersionModel");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョン設定を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetVersionModel(
                Request.GetVersionModelRequest request,
                UnityAction<AsyncResult<Result.GetVersionModelResult>> callback
        )
		{
			var task = new GetVersionModelTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeAcceptVersionsTask : Gs2WebSocketSessionTask<Result.DescribeAcceptVersionsResult>
        {
			private readonly Request.DescribeAcceptVersionsRequest _request;

			public DescribeAcceptVersionsTask(Request.DescribeAcceptVersionsRequest request, UnityAction<AsyncResult<Result.DescribeAcceptVersionsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.pageToken != null)
                {
                    jsonWriter.WritePropertyName("pageToken");
                    jsonWriter.Write(_request.pageToken.ToString());
                }
                if (_request.limit != null)
                {
                    jsonWriter.WritePropertyName("limit");
                    jsonWriter.Write(_request.limit.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeAcceptVersions");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  承認したバージョンの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeAcceptVersions(
                Request.DescribeAcceptVersionsRequest request,
                UnityAction<AsyncResult<Result.DescribeAcceptVersionsResult>> callback
        )
		{
			var task = new DescribeAcceptVersionsTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeAcceptVersionsByUserIdTask : Gs2WebSocketSessionTask<Result.DescribeAcceptVersionsByUserIdResult>
        {
			private readonly Request.DescribeAcceptVersionsByUserIdRequest _request;

			public DescribeAcceptVersionsByUserIdTask(Request.DescribeAcceptVersionsByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeAcceptVersionsByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.pageToken != null)
                {
                    jsonWriter.WritePropertyName("pageToken");
                    jsonWriter.Write(_request.pageToken.ToString());
                }
                if (_request.limit != null)
                {
                    jsonWriter.WritePropertyName("limit");
                    jsonWriter.Write(_request.limit.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeAcceptVersionsByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  承認したバージョンの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeAcceptVersionsByUserId(
                Request.DescribeAcceptVersionsByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeAcceptVersionsByUserIdResult>> callback
        )
		{
			var task = new DescribeAcceptVersionsByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class AcceptTask : Gs2WebSocketSessionTask<Result.AcceptResult>
        {
			private readonly Request.AcceptRequest _request;

			public AcceptTask(Request.AcceptRequest request, UnityAction<AsyncResult<Result.AcceptResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("accept");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  現在のバージョンを承認<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Accept(
                Request.AcceptRequest request,
                UnityAction<AsyncResult<Result.AcceptResult>> callback
        )
		{
			var task = new AcceptTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class AcceptByUserIdTask : Gs2WebSocketSessionTask<Result.AcceptByUserIdResult>
        {
			private readonly Request.AcceptByUserIdRequest _request;

			public AcceptByUserIdTask(Request.AcceptByUserIdRequest request, UnityAction<AsyncResult<Result.AcceptByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("acceptByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ユーザIDを指定して現在のバージョンを承認<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AcceptByUserId(
                Request.AcceptByUserIdRequest request,
                UnityAction<AsyncResult<Result.AcceptByUserIdResult>> callback
        )
		{
			var task = new AcceptByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetAcceptVersionTask : Gs2WebSocketSessionTask<Result.GetAcceptVersionResult>
        {
			private readonly Request.GetAcceptVersionRequest _request;

			public GetAcceptVersionTask(Request.GetAcceptVersionRequest request, UnityAction<AsyncResult<Result.GetAcceptVersionResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getAcceptVersion");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  承認したバージョンを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetAcceptVersion(
                Request.GetAcceptVersionRequest request,
                UnityAction<AsyncResult<Result.GetAcceptVersionResult>> callback
        )
		{
			var task = new GetAcceptVersionTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetAcceptVersionByUserIdTask : Gs2WebSocketSessionTask<Result.GetAcceptVersionByUserIdResult>
        {
			private readonly Request.GetAcceptVersionByUserIdRequest _request;

			public GetAcceptVersionByUserIdTask(Request.GetAcceptVersionByUserIdRequest request, UnityAction<AsyncResult<Result.GetAcceptVersionByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getAcceptVersionByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ユーザーIDを指定して承認したバージョンを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetAcceptVersionByUserId(
                Request.GetAcceptVersionByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetAcceptVersionByUserIdResult>> callback
        )
		{
			var task = new GetAcceptVersionByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteAcceptVersionTask : Gs2WebSocketSessionTask<Result.DeleteAcceptVersionResult>
        {
			private readonly Request.DeleteAcceptVersionRequest _request;

			public DeleteAcceptVersionTask(Request.DeleteAcceptVersionRequest request, UnityAction<AsyncResult<Result.DeleteAcceptVersionResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteAcceptVersion");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  承認したバージョンを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAcceptVersion(
                Request.DeleteAcceptVersionRequest request,
                UnityAction<AsyncResult<Result.DeleteAcceptVersionResult>> callback
        )
		{
			var task = new DeleteAcceptVersionTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteAcceptVersionByUserIdTask : Gs2WebSocketSessionTask<Result.DeleteAcceptVersionByUserIdResult>
        {
			private readonly Request.DeleteAcceptVersionByUserIdRequest _request;

			public DeleteAcceptVersionByUserIdTask(Request.DeleteAcceptVersionByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteAcceptVersionByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("acceptVersion");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteAcceptVersionByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ユーザーIDを指定して承認したバージョンを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAcceptVersionByUserId(
                Request.DeleteAcceptVersionByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteAcceptVersionByUserIdResult>> callback
        )
		{
			var task = new DeleteAcceptVersionByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CheckVersionTask : Gs2WebSocketSessionTask<Result.CheckVersionResult>
        {
			private readonly Request.CheckVersionRequest _request;

			public CheckVersionTask(Request.CheckVersionRequest request, UnityAction<AsyncResult<Result.CheckVersionResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.targetVersions != null)
                {
                    jsonWriter.WritePropertyName("targetVersions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.targetVersions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("checker");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("checkVersion");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンチェック<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CheckVersion(
                Request.CheckVersionRequest request,
                UnityAction<AsyncResult<Result.CheckVersionResult>> callback
        )
		{
			var task = new CheckVersionTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CheckVersionByUserIdTask : Gs2WebSocketSessionTask<Result.CheckVersionByUserIdResult>
        {
			private readonly Request.CheckVersionByUserIdRequest _request;

			public CheckVersionByUserIdTask(Request.CheckVersionByUserIdRequest request, UnityAction<AsyncResult<Result.CheckVersionByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.targetVersions != null)
                {
                    jsonWriter.WritePropertyName("targetVersions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.targetVersions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("checker");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("checkVersionByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  バージョンチェック<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CheckVersionByUserId(
                Request.CheckVersionByUserIdRequest request,
                UnityAction<AsyncResult<Result.CheckVersionByUserIdResult>> callback
        )
		{
			var task = new CheckVersionByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CalculateSignatureTask : Gs2WebSocketSessionTask<Result.CalculateSignatureResult>
        {
			private readonly Request.CalculateSignatureRequest _request;

			public CalculateSignatureTask(Request.CalculateSignatureRequest request, UnityAction<AsyncResult<Result.CalculateSignatureResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.versionName != null)
                {
                    jsonWriter.WritePropertyName("versionName");
                    jsonWriter.Write(_request.versionName.ToString());
                }
                if (_request.version != null)
                {
                    jsonWriter.WritePropertyName("version");
                    _request.version.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("checker");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("calculateSignature");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  スタンプシートのタスクを実行する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CalculateSignature(
                Request.CalculateSignatureRequest request,
                UnityAction<AsyncResult<Result.CalculateSignatureResult>> callback
        )
		{
			var task = new CalculateSignatureTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class ExportMasterTask : Gs2WebSocketSessionTask<Result.ExportMasterResult>
        {
			private readonly Request.ExportMasterRequest _request;

			public ExportMasterTask(Request.ExportMasterRequest request, UnityAction<AsyncResult<Result.ExportMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentVersionMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("exportMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  現在有効なバージョンのマスターデータをエクスポートします<br />
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
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetCurrentVersionMasterTask : Gs2WebSocketSessionTask<Result.GetCurrentVersionMasterResult>
        {
			private readonly Request.GetCurrentVersionMasterRequest _request;

			public GetCurrentVersionMasterTask(Request.GetCurrentVersionMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentVersionMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentVersionMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getCurrentVersionMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  現在有効なバージョンを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentVersionMaster(
                Request.GetCurrentVersionMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentVersionMasterResult>> callback
        )
		{
			var task = new GetCurrentVersionMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateCurrentVersionMasterTask : Gs2WebSocketSessionTask<Result.UpdateCurrentVersionMasterResult>
        {
			private readonly Request.UpdateCurrentVersionMasterRequest _request;

			public UpdateCurrentVersionMasterTask(Request.UpdateCurrentVersionMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentVersionMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
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
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentVersionMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateCurrentVersionMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  現在有効なバージョンを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentVersionMaster(
                Request.UpdateCurrentVersionMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentVersionMasterResult>> callback
        )
		{
			var task = new UpdateCurrentVersionMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateCurrentVersionMasterFromGitHubTask : Gs2WebSocketSessionTask<Result.UpdateCurrentVersionMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentVersionMasterFromGitHubRequest _request;

			public UpdateCurrentVersionMasterFromGitHubTask(Request.UpdateCurrentVersionMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentVersionMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
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
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("version");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentVersionMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateCurrentVersionMasterFromGitHub");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  現在有効なバージョンを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentVersionMasterFromGitHub(
                Request.UpdateCurrentVersionMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentVersionMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentVersionMasterFromGitHubTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}