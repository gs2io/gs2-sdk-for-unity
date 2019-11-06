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

namespace Gs2.Gs2Log
{
	public class Gs2LogWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "log";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2LogWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
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
                jsonWriter.Write("log");
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
                if (_request.type != null)
                {
                    jsonWriter.WritePropertyName("type");
                    jsonWriter.Write(_request.type.ToString());
                }
                if (_request.gcpCredentialJson != null)
                {
                    jsonWriter.WritePropertyName("gcpCredentialJson");
                    jsonWriter.Write(_request.gcpCredentialJson.ToString());
                }
                if (_request.bigQueryDatasetName != null)
                {
                    jsonWriter.WritePropertyName("bigQueryDatasetName");
                    jsonWriter.Write(_request.bigQueryDatasetName.ToString());
                }
                if (_request.logExpireDays != null)
                {
                    jsonWriter.WritePropertyName("logExpireDays");
                    jsonWriter.Write(_request.logExpireDays.ToString());
                }
                if (_request.awsRegion != null)
                {
                    jsonWriter.WritePropertyName("awsRegion");
                    jsonWriter.Write(_request.awsRegion.ToString());
                }
                if (_request.awsAccessKeyId != null)
                {
                    jsonWriter.WritePropertyName("awsAccessKeyId");
                    jsonWriter.Write(_request.awsAccessKeyId.ToString());
                }
                if (_request.awsSecretAccessKey != null)
                {
                    jsonWriter.WritePropertyName("awsSecretAccessKey");
                    jsonWriter.Write(_request.awsSecretAccessKey.ToString());
                }
                if (_request.firehoseStreamName != null)
                {
                    jsonWriter.WritePropertyName("firehoseStreamName");
                    jsonWriter.Write(_request.firehoseStreamName.ToString());
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
                jsonWriter.Write("log");
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
                jsonWriter.Write("log");
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
                jsonWriter.Write("log");
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
                if (_request.type != null)
                {
                    jsonWriter.WritePropertyName("type");
                    jsonWriter.Write(_request.type.ToString());
                }
                if (_request.gcpCredentialJson != null)
                {
                    jsonWriter.WritePropertyName("gcpCredentialJson");
                    jsonWriter.Write(_request.gcpCredentialJson.ToString());
                }
                if (_request.bigQueryDatasetName != null)
                {
                    jsonWriter.WritePropertyName("bigQueryDatasetName");
                    jsonWriter.Write(_request.bigQueryDatasetName.ToString());
                }
                if (_request.logExpireDays != null)
                {
                    jsonWriter.WritePropertyName("logExpireDays");
                    jsonWriter.Write(_request.logExpireDays.ToString());
                }
                if (_request.awsRegion != null)
                {
                    jsonWriter.WritePropertyName("awsRegion");
                    jsonWriter.Write(_request.awsRegion.ToString());
                }
                if (_request.awsAccessKeyId != null)
                {
                    jsonWriter.WritePropertyName("awsAccessKeyId");
                    jsonWriter.Write(_request.awsAccessKeyId.ToString());
                }
                if (_request.awsSecretAccessKey != null)
                {
                    jsonWriter.WritePropertyName("awsSecretAccessKey");
                    jsonWriter.Write(_request.awsSecretAccessKey.ToString());
                }
                if (_request.firehoseStreamName != null)
                {
                    jsonWriter.WritePropertyName("firehoseStreamName");
                    jsonWriter.Write(_request.firehoseStreamName.ToString());
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
                jsonWriter.Write("log");
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
                jsonWriter.Write("log");
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

        private class QueryAccessLogTask : Gs2WebSocketSessionTask<Result.QueryAccessLogResult>
        {
			private readonly Request.QueryAccessLogRequest _request;

			public QueryAccessLogTask(Request.QueryAccessLogRequest request, UnityAction<AsyncResult<Result.QueryAccessLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("accessLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("queryAccessLog");
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
		///  アクセスログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator QueryAccessLog(
                Request.QueryAccessLogRequest request,
                UnityAction<AsyncResult<Result.QueryAccessLogResult>> callback
        )
		{
			var task = new QueryAccessLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CountAccessLogTask : Gs2WebSocketSessionTask<Result.CountAccessLogResult>
        {
			private readonly Request.CountAccessLogRequest _request;

			public CountAccessLogTask(Request.CountAccessLogRequest request, UnityAction<AsyncResult<Result.CountAccessLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("accessLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("countAccessLog");
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
		///  アクセスログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CountAccessLog(
                Request.CountAccessLogRequest request,
                UnityAction<AsyncResult<Result.CountAccessLogResult>> callback
        )
		{
			var task = new CountAccessLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class QueryIssueStampSheetLogTask : Gs2WebSocketSessionTask<Result.QueryIssueStampSheetLogResult>
        {
			private readonly Request.QueryIssueStampSheetLogRequest _request;

			public QueryIssueStampSheetLogTask(Request.QueryIssueStampSheetLogRequest request, UnityAction<AsyncResult<Result.QueryIssueStampSheetLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("issueStampSheetLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("queryIssueStampSheetLog");
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
		///  スタンプシート発行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator QueryIssueStampSheetLog(
                Request.QueryIssueStampSheetLogRequest request,
                UnityAction<AsyncResult<Result.QueryIssueStampSheetLogResult>> callback
        )
		{
			var task = new QueryIssueStampSheetLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CountIssueStampSheetLogTask : Gs2WebSocketSessionTask<Result.CountIssueStampSheetLogResult>
        {
			private readonly Request.CountIssueStampSheetLogRequest _request;

			public CountIssueStampSheetLogTask(Request.CountIssueStampSheetLogRequest request, UnityAction<AsyncResult<Result.CountIssueStampSheetLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("issueStampSheetLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("countIssueStampSheetLog");
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
		///  スタンプシート発行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CountIssueStampSheetLog(
                Request.CountIssueStampSheetLogRequest request,
                UnityAction<AsyncResult<Result.CountIssueStampSheetLogResult>> callback
        )
		{
			var task = new CountIssueStampSheetLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class QueryExecuteStampSheetLogTask : Gs2WebSocketSessionTask<Result.QueryExecuteStampSheetLogResult>
        {
			private readonly Request.QueryExecuteStampSheetLogRequest _request;

			public QueryExecuteStampSheetLogTask(Request.QueryExecuteStampSheetLogRequest request, UnityAction<AsyncResult<Result.QueryExecuteStampSheetLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("executeStampSheetLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("queryExecuteStampSheetLog");
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
		///  スタンプシート実行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator QueryExecuteStampSheetLog(
                Request.QueryExecuteStampSheetLogRequest request,
                UnityAction<AsyncResult<Result.QueryExecuteStampSheetLogResult>> callback
        )
		{
			var task = new QueryExecuteStampSheetLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CountExecuteStampSheetLogTask : Gs2WebSocketSessionTask<Result.CountExecuteStampSheetLogResult>
        {
			private readonly Request.CountExecuteStampSheetLogRequest _request;

			public CountExecuteStampSheetLogTask(Request.CountExecuteStampSheetLogRequest request, UnityAction<AsyncResult<Result.CountExecuteStampSheetLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("executeStampSheetLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("countExecuteStampSheetLog");
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
		///  スタンプシート実行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CountExecuteStampSheetLog(
                Request.CountExecuteStampSheetLogRequest request,
                UnityAction<AsyncResult<Result.CountExecuteStampSheetLogResult>> callback
        )
		{
			var task = new CountExecuteStampSheetLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class QueryExecuteStampTaskLogTask : Gs2WebSocketSessionTask<Result.QueryExecuteStampTaskLogResult>
        {
			private readonly Request.QueryExecuteStampTaskLogRequest _request;

			public QueryExecuteStampTaskLogTask(Request.QueryExecuteStampTaskLogRequest request, UnityAction<AsyncResult<Result.QueryExecuteStampTaskLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("executeStampTaskLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("queryExecuteStampTaskLog");
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
		///  スタンプタスク実行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator QueryExecuteStampTaskLog(
                Request.QueryExecuteStampTaskLogRequest request,
                UnityAction<AsyncResult<Result.QueryExecuteStampTaskLogResult>> callback
        )
		{
			var task = new QueryExecuteStampTaskLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CountExecuteStampTaskLogTask : Gs2WebSocketSessionTask<Result.CountExecuteStampTaskLogResult>
        {
			private readonly Request.CountExecuteStampTaskLogRequest _request;

			public CountExecuteStampTaskLogTask(Request.CountExecuteStampTaskLogRequest request, UnityAction<AsyncResult<Result.CountExecuteStampTaskLogResult>> userCallback) : base(userCallback)
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
                if (_request.service != null)
                {
                    jsonWriter.WritePropertyName("service");
                    jsonWriter.Write(_request.service.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.action != null)
                {
                    jsonWriter.WritePropertyName("action");
                    jsonWriter.Write(_request.action.ToString());
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
                jsonWriter.Write("log");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("executeStampTaskLog");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("countExecuteStampTaskLog");
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
		///  スタンプタスク実行ログの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CountExecuteStampTaskLog(
                Request.CountExecuteStampTaskLogRequest request,
                UnityAction<AsyncResult<Result.CountExecuteStampTaskLogResult>> callback
        )
		{
			var task = new CountExecuteStampTaskLogTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}