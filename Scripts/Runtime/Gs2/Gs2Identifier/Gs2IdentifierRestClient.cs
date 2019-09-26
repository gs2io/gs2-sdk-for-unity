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
	public class Gs2IdentifierRestClient : AbstractGs2Client
	{

		public static string Endpoint = "identifier";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2IdentifierRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

        private class DescribeUsersTask : Gs2RestSessionTask<Result.DescribeUsersResult>
        {
			private readonly Request.DescribeUsersRequest _request;

			public DescribeUsersTask(Request.DescribeUsersRequest request, UnityAction<AsyncResult<Result.DescribeUsersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user";

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
			return Gs2RestSession.Execute(task);
        }

        private class CreateUserTask : Gs2RestSessionTask<Result.CreateUserResult>
        {
			private readonly Request.CreateUserRequest _request;

			public CreateUserTask(Request.CreateUserRequest request, UnityAction<AsyncResult<Result.CreateUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user";

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
			return Gs2RestSession.Execute(task);
        }

        private class UpdateUserTask : Gs2RestSessionTask<Result.UpdateUserResult>
        {
			private readonly Request.UpdateUserRequest _request;

			public UpdateUserTask(Request.UpdateUserRequest request, UnityAction<AsyncResult<Result.UpdateUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
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
			return Gs2RestSession.Execute(task);
        }

        private class GetUserTask : Gs2RestSessionTask<Result.GetUserResult>
        {
			private readonly Request.GetUserRequest _request;

			public GetUserTask(Request.GetUserRequest request, UnityAction<AsyncResult<Result.GetUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class DeleteUserTask : Gs2RestSessionTask<Result.DeleteUserResult>
        {
			private readonly Request.DeleteUserRequest _request;

			public DeleteUserTask(Request.DeleteUserRequest request, UnityAction<AsyncResult<Result.DeleteUserResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class DescribeSecurityPoliciesTask : Gs2RestSessionTask<Result.DescribeSecurityPoliciesResult>
        {
			private readonly Request.DescribeSecurityPoliciesRequest _request;

			public DescribeSecurityPoliciesTask(Request.DescribeSecurityPoliciesRequest request, UnityAction<AsyncResult<Result.DescribeSecurityPoliciesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy";

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
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCommonSecurityPoliciesTask : Gs2RestSessionTask<Result.DescribeCommonSecurityPoliciesResult>
        {
			private readonly Request.DescribeCommonSecurityPoliciesRequest _request;

			public DescribeCommonSecurityPoliciesTask(Request.DescribeCommonSecurityPoliciesRequest request, UnityAction<AsyncResult<Result.DescribeCommonSecurityPoliciesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy/common";

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
			return Gs2RestSession.Execute(task);
        }

        private class CreateSecurityPolicyTask : Gs2RestSessionTask<Result.CreateSecurityPolicyResult>
        {
			private readonly Request.CreateSecurityPolicyRequest _request;

			public CreateSecurityPolicyTask(Request.CreateSecurityPolicyRequest request, UnityAction<AsyncResult<Result.CreateSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy";

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
			return Gs2RestSession.Execute(task);
        }

        private class UpdateSecurityPolicyTask : Gs2RestSessionTask<Result.UpdateSecurityPolicyResult>
        {
			private readonly Request.UpdateSecurityPolicyRequest _request;

			public UpdateSecurityPolicyTask(Request.UpdateSecurityPolicyRequest request, UnityAction<AsyncResult<Result.UpdateSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy/{securityPolicyName}";

                url = url.Replace("{securityPolicyName}", !string.IsNullOrEmpty(_request.securityPolicyName) ? _request.securityPolicyName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
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
			return Gs2RestSession.Execute(task);
        }

        private class GetSecurityPolicyTask : Gs2RestSessionTask<Result.GetSecurityPolicyResult>
        {
			private readonly Request.GetSecurityPolicyRequest _request;

			public GetSecurityPolicyTask(Request.GetSecurityPolicyRequest request, UnityAction<AsyncResult<Result.GetSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy/{securityPolicyName}";

                url = url.Replace("{securityPolicyName}", !string.IsNullOrEmpty(_request.securityPolicyName) ? _request.securityPolicyName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class DeleteSecurityPolicyTask : Gs2RestSessionTask<Result.DeleteSecurityPolicyResult>
        {
			private readonly Request.DeleteSecurityPolicyRequest _request;

			public DeleteSecurityPolicyTask(Request.DeleteSecurityPolicyRequest request, UnityAction<AsyncResult<Result.DeleteSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/securityPolicy/{securityPolicyName}";

                url = url.Replace("{securityPolicyName}", !string.IsNullOrEmpty(_request.securityPolicyName) ? _request.securityPolicyName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class DescribeIdentifiersTask : Gs2RestSessionTask<Result.DescribeIdentifiersResult>
        {
			private readonly Request.DescribeIdentifiersRequest _request;

			public DescribeIdentifiersTask(Request.DescribeIdentifiersRequest request, UnityAction<AsyncResult<Result.DescribeIdentifiersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/identifier";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class CreateIdentifierTask : Gs2RestSessionTask<Result.CreateIdentifierResult>
        {
			private readonly Request.CreateIdentifierRequest _request;

			public CreateIdentifierTask(Request.CreateIdentifierRequest request, UnityAction<AsyncResult<Result.CreateIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/identifier";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
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
			return Gs2RestSession.Execute(task);
        }

        private class GetIdentifierTask : Gs2RestSessionTask<Result.GetIdentifierResult>
        {
			private readonly Request.GetIdentifierRequest _request;

			public GetIdentifierTask(Request.GetIdentifierRequest request, UnityAction<AsyncResult<Result.GetIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/identifier/{clientId}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");
                url = url.Replace("{clientId}", !string.IsNullOrEmpty(_request.clientId) ? _request.clientId.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class DeleteIdentifierTask : Gs2RestSessionTask<Result.DeleteIdentifierResult>
        {
			private readonly Request.DeleteIdentifierRequest _request;

			public DeleteIdentifierTask(Request.DeleteIdentifierRequest request, UnityAction<AsyncResult<Result.DeleteIdentifierResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/identifier/{clientId}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");
                url = url.Replace("{clientId}", !string.IsNullOrEmpty(_request.clientId) ? _request.clientId.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class GetHasSecurityPolicyTask : Gs2RestSessionTask<Result.GetHasSecurityPolicyResult>
        {
			private readonly Request.GetHasSecurityPolicyRequest _request;

			public GetHasSecurityPolicyTask(Request.GetHasSecurityPolicyRequest request, UnityAction<AsyncResult<Result.GetHasSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/securityPolicy";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class AttachSecurityPolicyTask : Gs2RestSessionTask<Result.AttachSecurityPolicyResult>
        {
			private readonly Request.AttachSecurityPolicyRequest _request;

			public AttachSecurityPolicyTask(Request.AttachSecurityPolicyRequest request, UnityAction<AsyncResult<Result.AttachSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/securityPolicy";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
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
			return Gs2RestSession.Execute(task);
        }

        private class DetachSecurityPolicyTask : Gs2RestSessionTask<Result.DetachSecurityPolicyResult>
        {
			private readonly Request.DetachSecurityPolicyRequest _request;

			public DetachSecurityPolicyTask(Request.DetachSecurityPolicyRequest request, UnityAction<AsyncResult<Result.DetachSecurityPolicyResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/user/{userName}/securityPolicy/{securityPolicyId}";

                url = url.Replace("{userName}", !string.IsNullOrEmpty(_request.userName) ? _request.userName.ToString() : "null");
                url = url.Replace("{securityPolicyId}", !string.IsNullOrEmpty(_request.securityPolicyId) ? _request.securityPolicyId.ToString() : "null");

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
			return Gs2RestSession.Execute(task);
        }

        private class LoginTask : Gs2RestSessionTask<Result.LoginResult>
        {
			private readonly Request.LoginRequest _request;

			public LoginTask(Request.LoginRequest request, UnityAction<AsyncResult<Result.LoginResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "identifier")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/projectToken/login";

                UnityWebRequest.url = url;

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
			return Gs2RestSession.Execute(task);
        }
	}
}