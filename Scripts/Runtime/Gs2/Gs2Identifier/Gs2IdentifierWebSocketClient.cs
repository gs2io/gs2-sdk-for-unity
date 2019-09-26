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

namespace Gs2.Gs2Identifier
{
	public class Gs2IdentifierWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "identifier";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2IdentifierWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}

        private class DescribeUsersTask : Gs2WebSocketSessionTask<Result.DescribeUsersResult>
        {
			private readonly Request.DescribeUsersRequest _request;

			public DescribeUsersTask(Request.DescribeUsersRequest request, UnityAction<AsyncResult<Result.DescribeUsersResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("user");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeUsers");
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
		///  ユーザの一覧を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeUsers(
                Request.DescribeUsersRequest request,
                UnityAction<AsyncResult<Result.DescribeUsersResult>> callback
        )
		{
			var task = new DescribeUsersTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateUserTask : Gs2WebSocketSessionTask<Result.CreateUserResult>
        {
			private readonly Request.CreateUserRequest _request;

			public CreateUserTask(Request.CreateUserRequest request, UnityAction<AsyncResult<Result.CreateUserResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("user");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createUser");
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
		///  ユーザを新規作成します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateUser(
                Request.CreateUserRequest request,
                UnityAction<AsyncResult<Result.CreateUserResult>> callback
        )
		{
			var task = new CreateUserTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateUserTask : Gs2WebSocketSessionTask<Result.UpdateUserResult>
        {
			private readonly Request.UpdateUserRequest _request;

			public UpdateUserTask(Request.UpdateUserRequest request, UnityAction<AsyncResult<Result.UpdateUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("user");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateUser");
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
		///  ユーザを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateUser(
                Request.UpdateUserRequest request,
                UnityAction<AsyncResult<Result.UpdateUserResult>> callback
        )
		{
			var task = new UpdateUserTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetUserTask : Gs2WebSocketSessionTask<Result.GetUserResult>
        {
			private readonly Request.GetUserRequest _request;

			public GetUserTask(Request.GetUserRequest request, UnityAction<AsyncResult<Result.GetUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("user");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getUser");
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
		///  ユーザを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetUser(
                Request.GetUserRequest request,
                UnityAction<AsyncResult<Result.GetUserResult>> callback
        )
		{
			var task = new GetUserTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteUserTask : Gs2WebSocketSessionTask<Result.DeleteUserResult>
        {
			private readonly Request.DeleteUserRequest _request;

			public DeleteUserTask(Request.DeleteUserRequest request, UnityAction<AsyncResult<Result.DeleteUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("user");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteUser");
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
		///  ユーザを削除します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteUser(
                Request.DeleteUserRequest request,
                UnityAction<AsyncResult<Result.DeleteUserResult>> callback
        )
		{
			var task = new DeleteUserTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeSecurityPoliciesTask : Gs2WebSocketSessionTask<Result.DescribeSecurityPoliciesResult>
        {
			private readonly Request.DescribeSecurityPoliciesRequest _request;

			public DescribeSecurityPoliciesTask(Request.DescribeSecurityPoliciesRequest request, UnityAction<AsyncResult<Result.DescribeSecurityPoliciesResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeSecurityPolicies");
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
		///  セキュリティポリシーの一覧を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeSecurityPolicies(
                Request.DescribeSecurityPoliciesRequest request,
                UnityAction<AsyncResult<Result.DescribeSecurityPoliciesResult>> callback
        )
		{
			var task = new DescribeSecurityPoliciesTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeCommonSecurityPoliciesTask : Gs2WebSocketSessionTask<Result.DescribeCommonSecurityPoliciesResult>
        {
			private readonly Request.DescribeCommonSecurityPoliciesRequest _request;

			public DescribeCommonSecurityPoliciesTask(Request.DescribeCommonSecurityPoliciesRequest request, UnityAction<AsyncResult<Result.DescribeCommonSecurityPoliciesResult>> userCallback) : base(userCallback)
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeCommonSecurityPolicies");
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
		///  オーナーIDを指定してセキュリティポリシーの一覧を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCommonSecurityPolicies(
                Request.DescribeCommonSecurityPoliciesRequest request,
                UnityAction<AsyncResult<Result.DescribeCommonSecurityPoliciesResult>> callback
        )
		{
			var task = new DescribeCommonSecurityPoliciesTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateSecurityPolicyTask : Gs2WebSocketSessionTask<Result.CreateSecurityPolicyResult>
        {
			private readonly Request.CreateSecurityPolicyRequest _request;

			public CreateSecurityPolicyTask(Request.CreateSecurityPolicyRequest request, UnityAction<AsyncResult<Result.CreateSecurityPolicyResult>> userCallback) : base(userCallback)
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
                if (_request.policy != null)
                {
                    jsonWriter.WritePropertyName("policy");
                    jsonWriter.Write(_request.policy.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createSecurityPolicy");
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
		///  セキュリティポリシーを新規作成します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateSecurityPolicy(
                Request.CreateSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.CreateSecurityPolicyResult>> callback
        )
		{
			var task = new CreateSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateSecurityPolicyTask : Gs2WebSocketSessionTask<Result.UpdateSecurityPolicyResult>
        {
			private readonly Request.UpdateSecurityPolicyRequest _request;

			public UpdateSecurityPolicyTask(Request.UpdateSecurityPolicyRequest request, UnityAction<AsyncResult<Result.UpdateSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.securityPolicyName != null)
                {
                    jsonWriter.WritePropertyName("securityPolicyName");
                    jsonWriter.Write(_request.securityPolicyName.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.policy != null)
                {
                    jsonWriter.WritePropertyName("policy");
                    jsonWriter.Write(_request.policy.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateSecurityPolicy");
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
		///  セキュリティポリシーを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateSecurityPolicy(
                Request.UpdateSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.UpdateSecurityPolicyResult>> callback
        )
		{
			var task = new UpdateSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetSecurityPolicyTask : Gs2WebSocketSessionTask<Result.GetSecurityPolicyResult>
        {
			private readonly Request.GetSecurityPolicyRequest _request;

			public GetSecurityPolicyTask(Request.GetSecurityPolicyRequest request, UnityAction<AsyncResult<Result.GetSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.securityPolicyName != null)
                {
                    jsonWriter.WritePropertyName("securityPolicyName");
                    jsonWriter.Write(_request.securityPolicyName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getSecurityPolicy");
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
		///  セキュリティポリシーを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetSecurityPolicy(
                Request.GetSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.GetSecurityPolicyResult>> callback
        )
		{
			var task = new GetSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteSecurityPolicyTask : Gs2WebSocketSessionTask<Result.DeleteSecurityPolicyResult>
        {
			private readonly Request.DeleteSecurityPolicyRequest _request;

			public DeleteSecurityPolicyTask(Request.DeleteSecurityPolicyRequest request, UnityAction<AsyncResult<Result.DeleteSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.securityPolicyName != null)
                {
                    jsonWriter.WritePropertyName("securityPolicyName");
                    jsonWriter.Write(_request.securityPolicyName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("securityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteSecurityPolicy");
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
		///  セキュリティポリシーを削除します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteSecurityPolicy(
                Request.DeleteSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.DeleteSecurityPolicyResult>> callback
        )
		{
			var task = new DeleteSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DescribeIdentifiersTask : Gs2WebSocketSessionTask<Result.DescribeIdentifiersResult>
        {
			private readonly Request.DescribeIdentifiersRequest _request;

			public DescribeIdentifiersTask(Request.DescribeIdentifiersRequest request, UnityAction<AsyncResult<Result.DescribeIdentifiersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("describeIdentifiers");
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
		///  クレデンシャルの一覧を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeIdentifiers(
                Request.DescribeIdentifiersRequest request,
                UnityAction<AsyncResult<Result.DescribeIdentifiersResult>> callback
        )
		{
			var task = new DescribeIdentifiersTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateIdentifierTask : Gs2WebSocketSessionTask<Result.CreateIdentifierResult>
        {
			private readonly Request.CreateIdentifierRequest _request;

			public CreateIdentifierTask(Request.CreateIdentifierRequest request, UnityAction<AsyncResult<Result.CreateIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createIdentifier");
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
		///  クレデンシャルを新規作成します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateIdentifier(
                Request.CreateIdentifierRequest request,
                UnityAction<AsyncResult<Result.CreateIdentifierResult>> callback
        )
		{
			var task = new CreateIdentifierTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetIdentifierTask : Gs2WebSocketSessionTask<Result.GetIdentifierResult>
        {
			private readonly Request.GetIdentifierRequest _request;

			public GetIdentifierTask(Request.GetIdentifierRequest request, UnityAction<AsyncResult<Result.GetIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
                }
                if (_request.clientId != null)
                {
                    jsonWriter.WritePropertyName("clientId");
                    jsonWriter.Write(_request.clientId.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getIdentifier");
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
		///  クレデンシャルを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetIdentifier(
                Request.GetIdentifierRequest request,
                UnityAction<AsyncResult<Result.GetIdentifierResult>> callback
        )
		{
			var task = new GetIdentifierTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteIdentifierTask : Gs2WebSocketSessionTask<Result.DeleteIdentifierResult>
        {
			private readonly Request.DeleteIdentifierRequest _request;

			public DeleteIdentifierTask(Request.DeleteIdentifierRequest request, UnityAction<AsyncResult<Result.DeleteIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
                }
                if (_request.clientId != null)
                {
                    jsonWriter.WritePropertyName("clientId");
                    jsonWriter.Write(_request.clientId.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteIdentifier");
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
		///  クレデンシャルを削除します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteIdentifier(
                Request.DeleteIdentifierRequest request,
                UnityAction<AsyncResult<Result.DeleteIdentifierResult>> callback
        )
		{
			var task = new DeleteIdentifierTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetHasSecurityPolicyTask : Gs2WebSocketSessionTask<Result.GetHasSecurityPolicyResult>
        {
			private readonly Request.GetHasSecurityPolicyRequest _request;

			public GetHasSecurityPolicyTask(Request.GetHasSecurityPolicyRequest request, UnityAction<AsyncResult<Result.GetHasSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("attachSecurityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getHasSecurityPolicy");
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
		///  割り当てられたセキュリティポリシーの一覧を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetHasSecurityPolicy(
                Request.GetHasSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.GetHasSecurityPolicyResult>> callback
        )
		{
			var task = new GetHasSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class AttachSecurityPolicyTask : Gs2WebSocketSessionTask<Result.AttachSecurityPolicyResult>
        {
			private readonly Request.AttachSecurityPolicyRequest _request;

			public AttachSecurityPolicyTask(Request.AttachSecurityPolicyRequest request, UnityAction<AsyncResult<Result.AttachSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
                }
                if (_request.securityPolicyId != null)
                {
                    jsonWriter.WritePropertyName("securityPolicyId");
                    jsonWriter.Write(_request.securityPolicyId.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("attachSecurityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("attachSecurityPolicy");
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
		///  割り当てられたセキュリティポリシーを新しくユーザーに割り当てます<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator AttachSecurityPolicy(
                Request.AttachSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.AttachSecurityPolicyResult>> callback
        )
		{
			var task = new AttachSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DetachSecurityPolicyTask : Gs2WebSocketSessionTask<Result.DetachSecurityPolicyResult>
        {
			private readonly Request.DetachSecurityPolicyRequest _request;

			public DetachSecurityPolicyTask(Request.DetachSecurityPolicyRequest request, UnityAction<AsyncResult<Result.DetachSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userName != null)
                {
                    jsonWriter.WritePropertyName("userName");
                    jsonWriter.Write(_request.userName.ToString());
                }
                if (_request.securityPolicyId != null)
                {
                    jsonWriter.WritePropertyName("securityPolicyId");
                    jsonWriter.Write(_request.securityPolicyId.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("attachSecurityPolicy");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("detachSecurityPolicy");
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
		///  割り当てられたセキュリティポリシーをユーザーから外します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DetachSecurityPolicy(
                Request.DetachSecurityPolicyRequest request,
                UnityAction<AsyncResult<Result.DetachSecurityPolicyResult>> callback
        )
		{
			var task = new DetachSecurityPolicyTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class LoginTask : Gs2WebSocketSessionTask<Result.LoginResult>
        {
			private readonly Request.LoginRequest _request;

			public LoginTask(Request.LoginRequest request, UnityAction<AsyncResult<Result.LoginResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.clientId != null)
                {
                    jsonWriter.WritePropertyName("clientId");
                    jsonWriter.Write(_request.clientId.ToString());
                }
                if (_request.clientSecret != null)
                {
                    jsonWriter.WritePropertyName("clientSecret");
                    jsonWriter.Write(_request.clientSecret.ToString());
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
                jsonWriter.Write("identifier");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("projectToken");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("login");
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
		///  プロジェクトトークン を取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Login(
                Request.LoginRequest request,
                UnityAction<AsyncResult<Result.LoginResult>> callback
        )
		{
			var task = new LoginTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}