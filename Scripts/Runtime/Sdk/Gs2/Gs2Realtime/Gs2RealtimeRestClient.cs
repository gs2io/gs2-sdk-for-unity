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

namespace Gs2.Gs2Realtime
{
	public class Gs2RealtimeRestClient : AbstractGs2Client
	{

		public static string Endpoint = "realtime";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2RealtimeRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
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
                    .Replace("{service}", "realtime")
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
                    .Replace("{service}", "realtime")
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
                if (_request.serverType != null)
                {
                    jsonWriter.WritePropertyName("serverType");
                    jsonWriter.Write(_request.serverType.ToString());
                }
                if (_request.serverSpec != null)
                {
                    jsonWriter.WritePropertyName("serverSpec");
                    jsonWriter.Write(_request.serverSpec.ToString());
                }
                if (_request.createNotification != null)
                {
                    jsonWriter.WritePropertyName("createNotification");
                    _request.createNotification.WriteJson(jsonWriter);
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
                    .Replace("{service}", "realtime")
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
                    .Replace("{service}", "realtime")
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
                    .Replace("{service}", "realtime")
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
                if (_request.serverType != null)
                {
                    jsonWriter.WritePropertyName("serverType");
                    jsonWriter.Write(_request.serverType.ToString());
                }
                if (_request.serverSpec != null)
                {
                    jsonWriter.WritePropertyName("serverSpec");
                    jsonWriter.Write(_request.serverSpec.ToString());
                }
                if (_request.createNotification != null)
                {
                    jsonWriter.WritePropertyName("createNotification");
                    _request.createNotification.WriteJson(jsonWriter);
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
                    .Replace("{service}", "realtime")
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

        private class DescribeRoomsTask : Gs2RestSessionTask<Result.DescribeRoomsResult>
        {
			private readonly Request.DescribeRoomsRequest _request;

			public DescribeRoomsTask(Request.DescribeRoomsRequest request, UnityAction<AsyncResult<Result.DescribeRoomsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "realtime")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/room";

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
		///  ルームの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeRooms(
                Request.DescribeRoomsRequest request,
                UnityAction<AsyncResult<Result.DescribeRoomsResult>> callback
        )
		{
			var task = new DescribeRoomsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class WantRoomTask : Gs2RestSessionTask<Result.WantRoomResult>
        {
			private readonly Request.WantRoomRequest _request;

			public WantRoomTask(Request.WantRoomRequest request, UnityAction<AsyncResult<Result.WantRoomResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "realtime")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/room";

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
                if (_request.notificationUserIds != null)
                {
                    jsonWriter.WritePropertyName("notificationUserIds");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.notificationUserIds)
                    {
                        jsonWriter.Write(item);
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
		///  ルームの作成依頼。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator WantRoom(
                Request.WantRoomRequest request,
                UnityAction<AsyncResult<Result.WantRoomResult>> callback
        )
		{
			var task = new WantRoomTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetRoomTask : Gs2RestSessionTask<Result.GetRoomResult>
        {
			private readonly Request.GetRoomRequest _request;

			public GetRoomTask(Request.GetRoomRequest request, UnityAction<AsyncResult<Result.GetRoomResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "realtime")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/room/{roomName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{roomName}", !string.IsNullOrEmpty(_request.roomName) ? _request.roomName.ToString() : "null");

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
		///  ルームを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRoom(
                Request.GetRoomRequest request,
                UnityAction<AsyncResult<Result.GetRoomResult>> callback
        )
		{
			var task = new GetRoomTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteRoomTask : Gs2RestSessionTask<Result.DeleteRoomResult>
        {
			private readonly Request.DeleteRoomRequest _request;

			public DeleteRoomTask(Request.DeleteRoomRequest request, UnityAction<AsyncResult<Result.DeleteRoomResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "realtime")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/room/{roomName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{roomName}", !string.IsNullOrEmpty(_request.roomName) ? _request.roomName.ToString() : "null");

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
		///  ルームを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRoom(
                Request.DeleteRoomRequest request,
                UnityAction<AsyncResult<Result.DeleteRoomResult>> callback
        )
		{
			var task = new DeleteRoomTask(request, callback);
			return Gs2RestSession.Execute(task);
        }
	}
}