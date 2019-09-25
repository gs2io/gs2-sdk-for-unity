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

namespace Gs2.Gs2Project
{
	public class Gs2ProjectWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "project";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2ProjectWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}

        private class CreateAccountTask : Gs2WebSocketSessionTask<Result.CreateAccountResult>
        {
			private readonly Request.CreateAccountRequest _request;

			public CreateAccountTask(Request.CreateAccountRequest request, UnityAction<AsyncResult<Result.CreateAccountResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.email != null)
                {
                    jsonWriter.WritePropertyName("email");
                    jsonWriter.Write(_request.email.ToString());
                }
                if (_request.fullName != null)
                {
                    jsonWriter.WritePropertyName("fullName");
                    jsonWriter.Write(_request.fullName.ToString());
                }
                if (_request.companyName != null)
                {
                    jsonWriter.WritePropertyName("companyName");
                    jsonWriter.Write(_request.companyName.ToString());
                }
                if (_request.password != null)
                {
                    jsonWriter.WritePropertyName("password");
                    jsonWriter.Write(_request.password.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createAccount");
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
		///  アカウントを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateAccount(
                Request.CreateAccountRequest request,
                UnityAction<AsyncResult<Result.CreateAccountResult>> callback
        )
		{
			var task = new CreateAccountTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class VerifyTask : Gs2WebSocketSessionTask<Result.VerifyResult>
        {
			private readonly Request.VerifyRequest _request;

			public VerifyTask(Request.VerifyRequest request, UnityAction<AsyncResult<Result.VerifyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.verifyToken != null)
                {
                    jsonWriter.WritePropertyName("verifyToken");
                    jsonWriter.Write(_request.verifyToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("verify");
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
		///  GS2アカウントを有効化します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Verify(
                Request.VerifyRequest request,
                UnityAction<AsyncResult<Result.VerifyResult>> callback
        )
		{
			var task = new VerifyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class SignInTask : Gs2WebSocketSessionTask<Result.SignInResult>
        {
			private readonly Request.SignInRequest _request;

			public SignInTask(Request.SignInRequest request, UnityAction<AsyncResult<Result.SignInResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.email != null)
                {
                    jsonWriter.WritePropertyName("email");
                    jsonWriter.Write(_request.email.ToString());
                }
                if (_request.password != null)
                {
                    jsonWriter.WritePropertyName("password");
                    jsonWriter.Write(_request.password.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("signIn");
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
		///  サインインします<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator SignIn(
                Request.SignInRequest request,
                UnityAction<AsyncResult<Result.SignInResult>> callback
        )
		{
			var task = new SignInTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class IssueAccountTokenTask : Gs2WebSocketSessionTask<Result.IssueAccountTokenResult>
        {
			private readonly Request.IssueAccountTokenRequest _request;

			public IssueAccountTokenTask(Request.IssueAccountTokenRequest request, UnityAction<AsyncResult<Result.IssueAccountTokenResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountName != null)
                {
                    jsonWriter.WritePropertyName("accountName");
                    jsonWriter.Write(_request.accountName.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("issueAccountToken");
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
		///  指定したアカウント名のアカウントトークンを発行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator IssueAccountToken(
                Request.IssueAccountTokenRequest request,
                UnityAction<AsyncResult<Result.IssueAccountTokenResult>> callback
        )
		{
			var task = new IssueAccountTokenTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class ForgetTask : Gs2WebSocketSessionTask<Result.ForgetResult>
        {
			private readonly Request.ForgetRequest _request;

			public ForgetTask(Request.ForgetRequest request, UnityAction<AsyncResult<Result.ForgetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.email != null)
                {
                    jsonWriter.WritePropertyName("email");
                    jsonWriter.Write(_request.email.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("forget");
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
		///  パスワード再発行トークンを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Forget(
                Request.ForgetRequest request,
                UnityAction<AsyncResult<Result.ForgetResult>> callback
        )
		{
			var task = new ForgetTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class IssuePasswordTask : Gs2WebSocketSessionTask<Result.IssuePasswordResult>
        {
			private readonly Request.IssuePasswordRequest _request;

			public IssuePasswordTask(Request.IssuePasswordRequest request, UnityAction<AsyncResult<Result.IssuePasswordResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.issuePasswordToken != null)
                {
                    jsonWriter.WritePropertyName("issuePasswordToken");
                    jsonWriter.Write(_request.issuePasswordToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("issuePassword");
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
		///  パスワードを再発行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator IssuePassword(
                Request.IssuePasswordRequest request,
                UnityAction<AsyncResult<Result.IssuePasswordResult>> callback
        )
		{
			var task = new IssuePasswordTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateAccountTask : Gs2WebSocketSessionTask<Result.UpdateAccountResult>
        {
			private readonly Request.UpdateAccountRequest _request;

			public UpdateAccountTask(Request.UpdateAccountRequest request, UnityAction<AsyncResult<Result.UpdateAccountResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.email != null)
                {
                    jsonWriter.WritePropertyName("email");
                    jsonWriter.Write(_request.email.ToString());
                }
                if (_request.fullName != null)
                {
                    jsonWriter.WritePropertyName("fullName");
                    jsonWriter.Write(_request.fullName.ToString());
                }
                if (_request.companyName != null)
                {
                    jsonWriter.WritePropertyName("companyName");
                    jsonWriter.Write(_request.companyName.ToString());
                }
                if (_request.password != null)
                {
                    jsonWriter.WritePropertyName("password");
                    jsonWriter.Write(_request.password.ToString());
                }
                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateAccount");
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
		///  GS2アカウントを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateAccount(
                Request.UpdateAccountRequest request,
                UnityAction<AsyncResult<Result.UpdateAccountResult>> callback
        )
		{
			var task = new UpdateAccountTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteAccountTask : Gs2WebSocketSessionTask<Result.DeleteAccountResult>
        {
			private readonly Request.DeleteAccountRequest _request;

			public DeleteAccountTask(Request.DeleteAccountRequest request, UnityAction<AsyncResult<Result.DeleteAccountResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("account");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteAccount");
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
		///  GS2アカウントを削除します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteAccount(
                Request.DeleteAccountRequest request,
                UnityAction<AsyncResult<Result.DeleteAccountResult>> callback
        )
		{
			var task = new DeleteAccountTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeProjectsTask : Gs2WebSocketSessionTask<Result.DescribeProjectsResult>
        {
			private readonly Request.DescribeProjectsRequest _request;

			public DescribeProjectsTask(Request.DescribeProjectsRequest request, UnityAction<AsyncResult<Result.DescribeProjectsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeProjects");
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
		///  プロジェクトの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeProjects(
                Request.DescribeProjectsRequest request,
                UnityAction<AsyncResult<Result.DescribeProjectsResult>> callback
        )
		{
			var task = new DescribeProjectsTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateProjectTask : Gs2WebSocketSessionTask<Result.CreateProjectResult>
        {
			private readonly Request.CreateProjectRequest _request;

			public CreateProjectTask(Request.CreateProjectRequest request, UnityAction<AsyncResult<Result.CreateProjectResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createProject");
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
		///  プロジェクトを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateProject(
                Request.CreateProjectRequest request,
                UnityAction<AsyncResult<Result.CreateProjectResult>> callback
        )
		{
			var task = new CreateProjectTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetProjectTask : Gs2WebSocketSessionTask<Result.GetProjectResult>
        {
			private readonly Request.GetProjectRequest _request;

			public GetProjectTask(Request.GetProjectRequest request, UnityAction<AsyncResult<Result.GetProjectResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
                }
                if (_request.projectName != null)
                {
                    jsonWriter.WritePropertyName("projectName");
                    jsonWriter.Write(_request.projectName.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getProject");
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
		///  プロジェクトを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetProject(
                Request.GetProjectRequest request,
                UnityAction<AsyncResult<Result.GetProjectResult>> callback
        )
		{
			var task = new GetProjectTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetProjectTokenTask : Gs2WebSocketSessionTask<Result.GetProjectTokenResult>
        {
			private readonly Request.GetProjectTokenRequest _request;

			public GetProjectTokenTask(Request.GetProjectTokenRequest request, UnityAction<AsyncResult<Result.GetProjectTokenResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.projectName != null)
                {
                    jsonWriter.WritePropertyName("projectName");
                    jsonWriter.Write(_request.projectName.ToString());
                }
                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getProjectToken");
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
		///  プロジェクトトークンを発行します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetProjectToken(
                Request.GetProjectTokenRequest request,
                UnityAction<AsyncResult<Result.GetProjectTokenResult>> callback
        )
		{
			var task = new GetProjectTokenTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateProjectTask : Gs2WebSocketSessionTask<Result.UpdateProjectResult>
        {
			private readonly Request.UpdateProjectRequest _request;

			public UpdateProjectTask(Request.UpdateProjectRequest request, UnityAction<AsyncResult<Result.UpdateProjectResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
                }
                if (_request.projectName != null)
                {
                    jsonWriter.WritePropertyName("projectName");
                    jsonWriter.Write(_request.projectName.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateProject");
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
		///  プロジェクトを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateProject(
                Request.UpdateProjectRequest request,
                UnityAction<AsyncResult<Result.UpdateProjectResult>> callback
        )
		{
			var task = new UpdateProjectTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteProjectTask : Gs2WebSocketSessionTask<Result.DeleteProjectResult>
        {
			private readonly Request.DeleteProjectRequest _request;

			public DeleteProjectTask(Request.DeleteProjectRequest request, UnityAction<AsyncResult<Result.DeleteProjectResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.accountToken != null)
                {
                    jsonWriter.WritePropertyName("accountToken");
                    jsonWriter.Write(_request.accountToken.ToString());
                }
                if (_request.projectName != null)
                {
                    jsonWriter.WritePropertyName("projectName");
                    jsonWriter.Write(_request.projectName.ToString());
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
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("project");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteProject");
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
		///  プロジェクトを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteProject(
                Request.DeleteProjectRequest request,
                UnityAction<AsyncResult<Result.DeleteProjectResult>> callback
        )
		{
			var task = new DeleteProjectTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}