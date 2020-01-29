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

namespace Gs2.Gs2Deploy
{
	public class Gs2DeployRestClient : AbstractGs2Client
	{

		public static string Endpoint = "deploy";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2DeployRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

        private class DescribeStacksTask : Gs2RestSessionTask<Result.DescribeStacksResult>
        {
			private readonly Request.DescribeStacksRequest _request;

			public DescribeStacksTask(Request.DescribeStacksRequest request, UnityAction<AsyncResult<Result.DescribeStacksResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack";

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
		///  スタックの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeStacks(
                Request.DescribeStacksRequest request,
                UnityAction<AsyncResult<Result.DescribeStacksResult>> callback
        )
		{
			var task = new DescribeStacksTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateStackTask : Gs2RestSessionTask<Result.CreateStackResult>
        {
			private readonly Request.CreateStackRequest _request;

			public CreateStackTask(Request.CreateStackRequest request, UnityAction<AsyncResult<Result.CreateStackResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack";

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
                if (_request.template != null)
                {
                    jsonWriter.WritePropertyName("template");
                    jsonWriter.Write(_request.template.ToString());
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
		///  スタックを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateStack(
                Request.CreateStackRequest request,
                UnityAction<AsyncResult<Result.CreateStackResult>> callback
        )
		{
			var task = new CreateStackTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateStackFromGitHubTask : Gs2RestSessionTask<Result.CreateStackFromGitHubResult>
        {
			private readonly Request.CreateStackFromGitHubRequest _request;

			public CreateStackFromGitHubTask(Request.CreateStackFromGitHubRequest request, UnityAction<AsyncResult<Result.CreateStackFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/from_git_hub";

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
		///  スタックを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateStackFromGitHub(
                Request.CreateStackFromGitHubRequest request,
                UnityAction<AsyncResult<Result.CreateStackFromGitHubResult>> callback
        )
		{
			var task = new CreateStackFromGitHubTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStackStatusTask : Gs2RestSessionTask<Result.GetStackStatusResult>
        {
			private readonly Request.GetStackStatusRequest _request;

			public GetStackStatusTask(Request.GetStackStatusRequest request, UnityAction<AsyncResult<Result.GetStackStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/status";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStackStatus(
                Request.GetStackStatusRequest request,
                UnityAction<AsyncResult<Result.GetStackStatusResult>> callback
        )
		{
			var task = new GetStackStatusTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetStackTask : Gs2RestSessionTask<Result.GetStackResult>
        {
			private readonly Request.GetStackRequest _request;

			public GetStackTask(Request.GetStackRequest request, UnityAction<AsyncResult<Result.GetStackResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetStack(
                Request.GetStackRequest request,
                UnityAction<AsyncResult<Result.GetStackResult>> callback
        )
		{
			var task = new GetStackTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateStackTask : Gs2RestSessionTask<Result.UpdateStackResult>
        {
			private readonly Request.UpdateStackRequest _request;

			public UpdateStackTask(Request.UpdateStackRequest request, UnityAction<AsyncResult<Result.UpdateStackResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.template != null)
                {
                    jsonWriter.WritePropertyName("template");
                    jsonWriter.Write(_request.template.ToString());
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
		///  スタックを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateStack(
                Request.UpdateStackRequest request,
                UnityAction<AsyncResult<Result.UpdateStackResult>> callback
        )
		{
			var task = new UpdateStackTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateStackFromGitHubTask : Gs2RestSessionTask<Result.UpdateStackFromGitHubResult>
        {
			private readonly Request.UpdateStackFromGitHubRequest _request;

			public UpdateStackFromGitHubTask(Request.UpdateStackFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateStackFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/from_git_hub";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
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
		///  スタックを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateStackFromGitHub(
                Request.UpdateStackFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateStackFromGitHubResult>> callback
        )
		{
			var task = new UpdateStackFromGitHubTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteStackTask : Gs2RestSessionTask<Result.DeleteStackResult>
        {
			private readonly Request.DeleteStackRequest _request;

			public DeleteStackTask(Request.DeleteStackRequest request, UnityAction<AsyncResult<Result.DeleteStackResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックを削除<br />
		///    <br />
		///    スタックによって作成されたリソースの削除を行い、成功すればエンティティを削除します。<br />
		///    何らかの理由でリソースの削除に失敗した場合はエンティティが残ります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteStack(
                Request.DeleteStackRequest request,
                UnityAction<AsyncResult<Result.DeleteStackResult>> callback
        )
		{
			var task = new DeleteStackTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ForceDeleteStackTask : Gs2RestSessionTask<Result.ForceDeleteStackResult>
        {
			private readonly Request.ForceDeleteStackRequest _request;

			public ForceDeleteStackTask(Request.ForceDeleteStackRequest request, UnityAction<AsyncResult<Result.ForceDeleteStackResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/force";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックを強制的に最終削除<br />
		///    <br />
		///    スタックのエンティティを強制的に削除します。<br />
		///    スタックが作成したリソースが残っていても、それらは削除されません。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ForceDeleteStack(
                Request.ForceDeleteStackRequest request,
                UnityAction<AsyncResult<Result.ForceDeleteStackResult>> callback
        )
		{
			var task = new ForceDeleteStackTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteStackResourcesTask : Gs2RestSessionTask<Result.DeleteStackResourcesResult>
        {
			private readonly Request.DeleteStackResourcesRequest _request;

			public DeleteStackResourcesTask(Request.DeleteStackResourcesRequest request, UnityAction<AsyncResult<Result.DeleteStackResourcesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/resources";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックのリソースを削除<br />
		///    <br />
		///    スタックによって作成されたリソースの削除を行います。<br />
		///    空のテンプレートでスタックを更新するのとほぼ同様の挙動ですが、スタックに適用されていたテンプレートが残るため、誤操作時に、残ったテンプレートからリソースを復元することができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteStackResources(
                Request.DeleteStackResourcesRequest request,
                UnityAction<AsyncResult<Result.DeleteStackResourcesResult>> callback
        )
		{
			var task = new DeleteStackResourcesTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteStackEntityTask : Gs2RestSessionTask<Result.DeleteStackEntityResult>
        {
			private readonly Request.DeleteStackEntityRequest _request;

			public DeleteStackEntityTask(Request.DeleteStackEntityRequest request, UnityAction<AsyncResult<Result.DeleteStackEntityResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/entity";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  スタックを最終削除<br />
		///    <br />
		///    スタックのエンティティを削除します。<br />
		///    リソースの残っているスタックを削除しようとするとエラーになります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteStackEntity(
                Request.DeleteStackEntityRequest request,
                UnityAction<AsyncResult<Result.DeleteStackEntityResult>> callback
        )
		{
			var task = new DeleteStackEntityTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeResourcesTask : Gs2RestSessionTask<Result.DescribeResourcesResult>
        {
			private readonly Request.DescribeResourcesRequest _request;

			public DescribeResourcesTask(Request.DescribeResourcesRequest request, UnityAction<AsyncResult<Result.DescribeResourcesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/resource";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  作成されたのリソースの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeResources(
                Request.DescribeResourcesRequest request,
                UnityAction<AsyncResult<Result.DescribeResourcesResult>> callback
        )
		{
			var task = new DescribeResourcesTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetResourceTask : Gs2RestSessionTask<Result.GetResourceResult>
        {
			private readonly Request.GetResourceRequest _request;

			public GetResourceTask(Request.GetResourceRequest request, UnityAction<AsyncResult<Result.GetResourceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/resource/{resourceName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");
                url = url.Replace("{resourceName}", !string.IsNullOrEmpty(_request.resourceName) ? _request.resourceName.ToString() : "null");

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
		///  作成されたのリソースを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetResource(
                Request.GetResourceRequest request,
                UnityAction<AsyncResult<Result.GetResourceResult>> callback
        )
		{
			var task = new GetResourceTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeEventsTask : Gs2RestSessionTask<Result.DescribeEventsResult>
        {
			private readonly Request.DescribeEventsRequest _request;

			public DescribeEventsTask(Request.DescribeEventsRequest request, UnityAction<AsyncResult<Result.DescribeEventsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/event";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  発生したイベントの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeEvents(
                Request.DescribeEventsRequest request,
                UnityAction<AsyncResult<Result.DescribeEventsResult>> callback
        )
		{
			var task = new DescribeEventsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetEventTask : Gs2RestSessionTask<Result.GetEventResult>
        {
			private readonly Request.GetEventRequest _request;

			public GetEventTask(Request.GetEventRequest request, UnityAction<AsyncResult<Result.GetEventResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/event/{eventName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");
                url = url.Replace("{eventName}", !string.IsNullOrEmpty(_request.eventName) ? _request.eventName.ToString() : "null");

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
		///  発生したイベントを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetEvent(
                Request.GetEventRequest request,
                UnityAction<AsyncResult<Result.GetEventResult>> callback
        )
		{
			var task = new GetEventTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeOutputsTask : Gs2RestSessionTask<Result.DescribeOutputsResult>
        {
			private readonly Request.DescribeOutputsRequest _request;

			public DescribeOutputsTask(Request.DescribeOutputsRequest request, UnityAction<AsyncResult<Result.DescribeOutputsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/output";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");

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
		///  アウトプットの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeOutputs(
                Request.DescribeOutputsRequest request,
                UnityAction<AsyncResult<Result.DescribeOutputsResult>> callback
        )
		{
			var task = new DescribeOutputsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetOutputTask : Gs2RestSessionTask<Result.GetOutputResult>
        {
			private readonly Request.GetOutputRequest _request;

			public GetOutputTask(Request.GetOutputRequest request, UnityAction<AsyncResult<Result.GetOutputResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "deploy")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stack/{stackName}/output/{outputName}";

                url = url.Replace("{stackName}", !string.IsNullOrEmpty(_request.stackName) ? _request.stackName.ToString() : "null");
                url = url.Replace("{outputName}", !string.IsNullOrEmpty(_request.outputName) ? _request.outputName.ToString() : "null");

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
		///  アウトプットを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetOutput(
                Request.GetOutputRequest request,
                UnityAction<AsyncResult<Result.GetOutputResult>> callback
        )
		{
			var task = new GetOutputTask(request, callback);
			return Gs2RestSession.Execute(task);
        }
	}
}