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

namespace Gs2.Gs2Lottery
{
	public class Gs2LotteryWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "lottery";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2LotteryWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
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
                jsonWriter.Write("lottery");
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
                if (_request.lotteryTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("lotteryTriggerScriptId");
                    jsonWriter.Write(_request.lotteryTriggerScriptId.ToString());
                }
                if (_request.choicePrizeTableScriptId != null)
                {
                    jsonWriter.WritePropertyName("choicePrizeTableScriptId");
                    jsonWriter.Write(_request.choicePrizeTableScriptId.ToString());
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
                jsonWriter.Write("lottery");
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
                jsonWriter.Write("lottery");
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
                jsonWriter.Write("lottery");
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
                if (_request.lotteryTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("lotteryTriggerScriptId");
                    jsonWriter.Write(_request.lotteryTriggerScriptId.ToString());
                }
                if (_request.choicePrizeTableScriptId != null)
                {
                    jsonWriter.WritePropertyName("choicePrizeTableScriptId");
                    jsonWriter.Write(_request.choicePrizeTableScriptId.ToString());
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
                jsonWriter.Write("lottery");
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
                jsonWriter.Write("lottery");
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

        private class DescribeLotteryModelMastersTask : Gs2WebSocketSessionTask<Result.DescribeLotteryModelMastersResult>
        {
			private readonly Request.DescribeLotteryModelMastersRequest _request;

			public DescribeLotteryModelMastersTask(Request.DescribeLotteryModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeLotteryModelMastersResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeLotteryModelMasters");
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
		///  抽選の種類マスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeLotteryModelMasters(
                Request.DescribeLotteryModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeLotteryModelMastersResult>> callback
        )
		{
			var task = new DescribeLotteryModelMastersTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateLotteryModelMasterTask : Gs2WebSocketSessionTask<Result.CreateLotteryModelMasterResult>
        {
			private readonly Request.CreateLotteryModelMasterRequest _request;

			public CreateLotteryModelMasterTask(Request.CreateLotteryModelMasterRequest request, UnityAction<AsyncResult<Result.CreateLotteryModelMasterResult>> userCallback) : base(userCallback)
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
                if (_request.mode != null)
                {
                    jsonWriter.WritePropertyName("mode");
                    jsonWriter.Write(_request.mode.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
                }
                if (_request.choicePrizeTableScriptId != null)
                {
                    jsonWriter.WritePropertyName("choicePrizeTableScriptId");
                    jsonWriter.Write(_request.choicePrizeTableScriptId.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createLotteryModelMaster");
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
		///  抽選の種類マスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateLotteryModelMaster(
                Request.CreateLotteryModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateLotteryModelMasterResult>> callback
        )
		{
			var task = new CreateLotteryModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetLotteryModelMasterTask : Gs2WebSocketSessionTask<Result.GetLotteryModelMasterResult>
        {
			private readonly Request.GetLotteryModelMasterRequest _request;

			public GetLotteryModelMasterTask(Request.GetLotteryModelMasterRequest request, UnityAction<AsyncResult<Result.GetLotteryModelMasterResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getLotteryModelMaster");
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
		///  抽選の種類マスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetLotteryModelMaster(
                Request.GetLotteryModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetLotteryModelMasterResult>> callback
        )
		{
			var task = new GetLotteryModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateLotteryModelMasterTask : Gs2WebSocketSessionTask<Result.UpdateLotteryModelMasterResult>
        {
			private readonly Request.UpdateLotteryModelMasterRequest _request;

			public UpdateLotteryModelMasterTask(Request.UpdateLotteryModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateLotteryModelMasterResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                if (_request.mode != null)
                {
                    jsonWriter.WritePropertyName("mode");
                    jsonWriter.Write(_request.mode.ToString());
                }
                if (_request.method != null)
                {
                    jsonWriter.WritePropertyName("method");
                    jsonWriter.Write(_request.method.ToString());
                }
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
                }
                if (_request.choicePrizeTableScriptId != null)
                {
                    jsonWriter.WritePropertyName("choicePrizeTableScriptId");
                    jsonWriter.Write(_request.choicePrizeTableScriptId.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateLotteryModelMaster");
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
		///  抽選の種類マスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateLotteryModelMaster(
                Request.UpdateLotteryModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateLotteryModelMasterResult>> callback
        )
		{
			var task = new UpdateLotteryModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteLotteryModelMasterTask : Gs2WebSocketSessionTask<Result.DeleteLotteryModelMasterResult>
        {
			private readonly Request.DeleteLotteryModelMasterRequest _request;

			public DeleteLotteryModelMasterTask(Request.DeleteLotteryModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteLotteryModelMasterResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteLotteryModelMaster");
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
		///  抽選の種類マスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteLotteryModelMaster(
                Request.DeleteLotteryModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteLotteryModelMasterResult>> callback
        )
		{
			var task = new DeleteLotteryModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribePrizeTableMastersTask : Gs2WebSocketSessionTask<Result.DescribePrizeTableMastersResult>
        {
			private readonly Request.DescribePrizeTableMastersRequest _request;

			public DescribePrizeTableMastersTask(Request.DescribePrizeTableMastersRequest request, UnityAction<AsyncResult<Result.DescribePrizeTableMastersResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTableMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describePrizeTableMasters");
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
		///  排出確率テーブルマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribePrizeTableMasters(
                Request.DescribePrizeTableMastersRequest request,
                UnityAction<AsyncResult<Result.DescribePrizeTableMastersResult>> callback
        )
		{
			var task = new DescribePrizeTableMastersTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreatePrizeTableMasterTask : Gs2WebSocketSessionTask<Result.CreatePrizeTableMasterResult>
        {
			private readonly Request.CreatePrizeTableMasterRequest _request;

			public CreatePrizeTableMasterTask(Request.CreatePrizeTableMasterRequest request, UnityAction<AsyncResult<Result.CreatePrizeTableMasterResult>> userCallback) : base(userCallback)
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
                if (_request.prizes != null)
                {
                    jsonWriter.WritePropertyName("prizes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.prizes)
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

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTableMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createPrizeTableMaster");
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
		///  排出確率テーブルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreatePrizeTableMaster(
                Request.CreatePrizeTableMasterRequest request,
                UnityAction<AsyncResult<Result.CreatePrizeTableMasterResult>> callback
        )
		{
			var task = new CreatePrizeTableMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetPrizeTableMasterTask : Gs2WebSocketSessionTask<Result.GetPrizeTableMasterResult>
        {
			private readonly Request.GetPrizeTableMasterRequest _request;

			public GetPrizeTableMasterTask(Request.GetPrizeTableMasterRequest request, UnityAction<AsyncResult<Result.GetPrizeTableMasterResult>> userCallback) : base(userCallback)
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
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTableMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getPrizeTableMaster");
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
		///  排出確率テーブルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetPrizeTableMaster(
                Request.GetPrizeTableMasterRequest request,
                UnityAction<AsyncResult<Result.GetPrizeTableMasterResult>> callback
        )
		{
			var task = new GetPrizeTableMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdatePrizeTableMasterTask : Gs2WebSocketSessionTask<Result.UpdatePrizeTableMasterResult>
        {
			private readonly Request.UpdatePrizeTableMasterRequest _request;

			public UpdatePrizeTableMasterTask(Request.UpdatePrizeTableMasterRequest request, UnityAction<AsyncResult<Result.UpdatePrizeTableMasterResult>> userCallback) : base(userCallback)
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
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
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
                if (_request.prizes != null)
                {
                    jsonWriter.WritePropertyName("prizes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.prizes)
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

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTableMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updatePrizeTableMaster");
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
		///  排出確率テーブルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdatePrizeTableMaster(
                Request.UpdatePrizeTableMasterRequest request,
                UnityAction<AsyncResult<Result.UpdatePrizeTableMasterResult>> callback
        )
		{
			var task = new UpdatePrizeTableMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeletePrizeTableMasterTask : Gs2WebSocketSessionTask<Result.DeletePrizeTableMasterResult>
        {
			private readonly Request.DeletePrizeTableMasterRequest _request;

			public DeletePrizeTableMasterTask(Request.DeletePrizeTableMasterRequest request, UnityAction<AsyncResult<Result.DeletePrizeTableMasterResult>> userCallback) : base(userCallback)
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
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTableMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deletePrizeTableMaster");
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
		///  排出確率テーブルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeletePrizeTableMaster(
                Request.DeletePrizeTableMasterRequest request,
                UnityAction<AsyncResult<Result.DeletePrizeTableMasterResult>> callback
        )
		{
			var task = new DeletePrizeTableMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeBoxesTask : Gs2WebSocketSessionTask<Result.DescribeBoxesResult>
        {
			private readonly Request.DescribeBoxesRequest _request;

			public DescribeBoxesTask(Request.DescribeBoxesRequest request, UnityAction<AsyncResult<Result.DescribeBoxesResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeBoxes");
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
		///  ボックスの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeBoxes(
                Request.DescribeBoxesRequest request,
                UnityAction<AsyncResult<Result.DescribeBoxesResult>> callback
        )
		{
			var task = new DescribeBoxesTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeBoxesByUserIdTask : Gs2WebSocketSessionTask<Result.DescribeBoxesByUserIdResult>
        {
			private readonly Request.DescribeBoxesByUserIdRequest _request;

			public DescribeBoxesByUserIdTask(Request.DescribeBoxesByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeBoxesByUserIdResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeBoxesByUserId");
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
		///  ユーザIDを指定してボックスの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeBoxesByUserId(
                Request.DescribeBoxesByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeBoxesByUserIdResult>> callback
        )
		{
			var task = new DescribeBoxesByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetBoxTask : Gs2WebSocketSessionTask<Result.GetBoxResult>
        {
			private readonly Request.GetBoxRequest _request;

			public GetBoxTask(Request.GetBoxRequest request, UnityAction<AsyncResult<Result.GetBoxResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getBox");
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
		///  ボックスを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetBox(
                Request.GetBoxRequest request,
                UnityAction<AsyncResult<Result.GetBoxResult>> callback
        )
		{
			var task = new GetBoxTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetBoxByUserIdTask : Gs2WebSocketSessionTask<Result.GetBoxByUserIdResult>
        {
			private readonly Request.GetBoxByUserIdRequest _request;

			public GetBoxByUserIdTask(Request.GetBoxByUserIdRequest request, UnityAction<AsyncResult<Result.GetBoxByUserIdResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getBoxByUserId");
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
		///  ユーザIDを指定してボックスを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetBoxByUserId(
                Request.GetBoxByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetBoxByUserIdResult>> callback
        )
		{
			var task = new GetBoxByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class ResetBoxTask : Gs2WebSocketSessionTask<Result.ResetBoxResult>
        {
			private readonly Request.ResetBoxRequest _request;

			public ResetBoxTask(Request.ResetBoxRequest request, UnityAction<AsyncResult<Result.ResetBoxResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("resetBox");
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
		///  ボックスをリセット<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ResetBox(
                Request.ResetBoxRequest request,
                UnityAction<AsyncResult<Result.ResetBoxResult>> callback
        )
		{
			var task = new ResetBoxTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class ResetBoxByUserIdTask : Gs2WebSocketSessionTask<Result.ResetBoxByUserIdResult>
        {
			private readonly Request.ResetBoxByUserIdRequest _request;

			public ResetBoxByUserIdTask(Request.ResetBoxByUserIdRequest request, UnityAction<AsyncResult<Result.ResetBoxByUserIdResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("box");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("resetBoxByUserId");
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
		///  ユーザIDを指定してボックスをリセット<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ResetBoxByUserId(
                Request.ResetBoxByUserIdRequest request,
                UnityAction<AsyncResult<Result.ResetBoxByUserIdResult>> callback
        )
		{
			var task = new ResetBoxByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeLotteryModelsTask : Gs2WebSocketSessionTask<Result.DescribeLotteryModelsResult>
        {
			private readonly Request.DescribeLotteryModelsRequest _request;

			public DescribeLotteryModelsTask(Request.DescribeLotteryModelsRequest request, UnityAction<AsyncResult<Result.DescribeLotteryModelsResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModel");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeLotteryModels");
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
		///  抽選の種類の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeLotteryModels(
                Request.DescribeLotteryModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeLotteryModelsResult>> callback
        )
		{
			var task = new DescribeLotteryModelsTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetLotteryModelTask : Gs2WebSocketSessionTask<Result.GetLotteryModelResult>
        {
			private readonly Request.GetLotteryModelRequest _request;

			public GetLotteryModelTask(Request.GetLotteryModelRequest request, UnityAction<AsyncResult<Result.GetLotteryModelResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lotteryModel");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getLotteryModel");
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
		///  抽選の種類を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetLotteryModel(
                Request.GetLotteryModelRequest request,
                UnityAction<AsyncResult<Result.GetLotteryModelResult>> callback
        )
		{
			var task = new GetLotteryModelTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribePrizeTablesTask : Gs2WebSocketSessionTask<Result.DescribePrizeTablesResult>
        {
			private readonly Request.DescribePrizeTablesRequest _request;

			public DescribePrizeTablesTask(Request.DescribePrizeTablesRequest request, UnityAction<AsyncResult<Result.DescribePrizeTablesResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTable");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describePrizeTables");
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
		///  排出確率テーブルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribePrizeTables(
                Request.DescribePrizeTablesRequest request,
                UnityAction<AsyncResult<Result.DescribePrizeTablesResult>> callback
        )
		{
			var task = new DescribePrizeTablesTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetPrizeTableTask : Gs2WebSocketSessionTask<Result.GetPrizeTableResult>
        {
			private readonly Request.GetPrizeTableRequest _request;

			public GetPrizeTableTask(Request.GetPrizeTableRequest request, UnityAction<AsyncResult<Result.GetPrizeTableResult>> userCallback) : base(userCallback)
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
                if (_request.prizeTableName != null)
                {
                    jsonWriter.WritePropertyName("prizeTableName");
                    jsonWriter.Write(_request.prizeTableName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("prizeTable");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getPrizeTable");
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
		///  排出確率テーブルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetPrizeTable(
                Request.GetPrizeTableRequest request,
                UnityAction<AsyncResult<Result.GetPrizeTableResult>> callback
        )
		{
			var task = new GetPrizeTableTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DrawByUserIdTask : Gs2WebSocketSessionTask<Result.DrawByUserIdResult>
        {
			private readonly Request.DrawByUserIdRequest _request;

			public DrawByUserIdTask(Request.DrawByUserIdRequest request, UnityAction<AsyncResult<Result.DrawByUserIdResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("drawByUserId");
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
		///  ユーザIDを指定して抽選を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DrawByUserId(
                Request.DrawByUserIdRequest request,
                UnityAction<AsyncResult<Result.DrawByUserIdResult>> callback
        )
		{
			var task = new DrawByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeProbabilitiesTask : Gs2WebSocketSessionTask<Result.DescribeProbabilitiesResult>
        {
			private readonly Request.DescribeProbabilitiesRequest _request;

			public DescribeProbabilitiesTask(Request.DescribeProbabilitiesRequest request, UnityAction<AsyncResult<Result.DescribeProbabilitiesResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeProbabilities");
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
		///  排出確率を取得<br />
		///    <br />
		///    通常抽選ではすべてのゲームプレイヤーに対して同じ確率を応答します。<br />
		///    ボックス抽選ではボックスの中身の残りを考慮したゲームプレイヤーごとに異なる確率を応答します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeProbabilities(
                Request.DescribeProbabilitiesRequest request,
                UnityAction<AsyncResult<Result.DescribeProbabilitiesResult>> callback
        )
		{
			var task = new DescribeProbabilitiesTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeProbabilitiesByUserIdTask : Gs2WebSocketSessionTask<Result.DescribeProbabilitiesByUserIdResult>
        {
			private readonly Request.DescribeProbabilitiesByUserIdRequest _request;

			public DescribeProbabilitiesByUserIdTask(Request.DescribeProbabilitiesByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeProbabilitiesByUserIdResult>> userCallback) : base(userCallback)
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
                if (_request.lotteryName != null)
                {
                    jsonWriter.WritePropertyName("lotteryName");
                    jsonWriter.Write(_request.lotteryName.ToString());
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeProbabilitiesByUserId");
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
		///  排出確率を取得<br />
		///    <br />
		///    通常抽選ではすべてのゲームプレイヤーに対して同じ確率を応答します。<br />
		///    ボックス抽選ではボックスの中身の残りを考慮したゲームプレイヤーごとに異なる確率を応答します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeProbabilitiesByUserId(
                Request.DescribeProbabilitiesByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeProbabilitiesByUserIdResult>> callback
        )
		{
			var task = new DescribeProbabilitiesByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DrawByStampSheetTask : Gs2WebSocketSessionTask<Result.DrawByStampSheetResult>
        {
			private readonly Request.DrawByStampSheetRequest _request;

			public DrawByStampSheetTask(Request.DrawByStampSheetRequest request, UnityAction<AsyncResult<Result.DrawByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("drawByStampSheet");
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
		///  スタンプシートを使用して抽選処理を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DrawByStampSheet(
                Request.DrawByStampSheetRequest request,
                UnityAction<AsyncResult<Result.DrawByStampSheetResult>> callback
        )
		{
			var task = new DrawByStampSheetTask(request, callback);
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentLotteryMaster");
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
		///  現在有効な抽選設定のマスターデータをエクスポートします<br />
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

        private class GetCurrentLotteryMasterTask : Gs2WebSocketSessionTask<Result.GetCurrentLotteryMasterResult>
        {
			private readonly Request.GetCurrentLotteryMasterRequest _request;

			public GetCurrentLotteryMasterTask(Request.GetCurrentLotteryMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentLotteryMasterResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentLotteryMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getCurrentLotteryMaster");
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
		///  現在有効な抽選設定を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentLotteryMaster(
                Request.GetCurrentLotteryMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentLotteryMasterResult>> callback
        )
		{
			var task = new GetCurrentLotteryMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateCurrentLotteryMasterTask : Gs2WebSocketSessionTask<Result.UpdateCurrentLotteryMasterResult>
        {
			private readonly Request.UpdateCurrentLotteryMasterRequest _request;

			public UpdateCurrentLotteryMasterTask(Request.UpdateCurrentLotteryMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentLotteryMasterResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentLotteryMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateCurrentLotteryMaster");
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
		///  現在有効な抽選設定を更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentLotteryMaster(
                Request.UpdateCurrentLotteryMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentLotteryMasterResult>> callback
        )
		{
			var task = new UpdateCurrentLotteryMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateCurrentLotteryMasterFromGitHubTask : Gs2WebSocketSessionTask<Result.UpdateCurrentLotteryMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentLotteryMasterFromGitHubRequest _request;

			public UpdateCurrentLotteryMasterFromGitHubTask(Request.UpdateCurrentLotteryMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentLotteryMasterFromGitHubResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("lottery");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("currentLotteryMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateCurrentLotteryMasterFromGitHub");
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
		///  現在有効な抽選設定を更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentLotteryMasterFromGitHub(
                Request.UpdateCurrentLotteryMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentLotteryMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentLotteryMasterFromGitHubTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}