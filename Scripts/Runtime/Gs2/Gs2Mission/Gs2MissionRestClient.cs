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

namespace Gs2.Gs2Mission
{
	public class Gs2MissionRestClient : AbstractGs2Client
	{

		public static string Endpoint = "mission";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2MissionRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

		}

        private class DescribeMissionTaskModelsTask : Gs2RestSessionTask<Result.DescribeMissionTaskModelsResult>
        {
			private readonly Request.DescribeMissionTaskModelsRequest _request;

			public DescribeMissionTaskModelsTask(Request.DescribeMissionTaskModelsRequest request, UnityAction<AsyncResult<Result.DescribeMissionTaskModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  ミッションタスクの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeMissionTaskModels(
                Request.DescribeMissionTaskModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionTaskModelsResult>> callback
        )
		{
			var task = new DescribeMissionTaskModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetMissionTaskModelTask : Gs2RestSessionTask<Result.GetMissionTaskModelResult>
        {
			private readonly Request.GetMissionTaskModelRequest _request;

			public GetMissionTaskModelTask(Request.GetMissionTaskModelRequest request, UnityAction<AsyncResult<Result.GetMissionTaskModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");

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
		///  ミッションタスクを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetMissionTaskModel(
                Request.GetMissionTaskModelRequest request,
                UnityAction<AsyncResult<Result.GetMissionTaskModelResult>> callback
        )
		{
			var task = new GetMissionTaskModelTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCounterModelMastersTask : Gs2RestSessionTask<Result.DescribeCounterModelMastersResult>
        {
			private readonly Request.DescribeCounterModelMastersRequest _request;

			public DescribeCounterModelMastersTask(Request.DescribeCounterModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeCounterModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter";

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
		///  カウンターの種類マスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCounterModelMasters(
                Request.DescribeCounterModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeCounterModelMastersResult>> callback
        )
		{
			var task = new DescribeCounterModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateCounterModelMasterTask : Gs2RestSessionTask<Result.CreateCounterModelMasterResult>
        {
			private readonly Request.CreateCounterModelMasterRequest _request;

			public CreateCounterModelMasterTask(Request.CreateCounterModelMasterRequest request, UnityAction<AsyncResult<Result.CreateCounterModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter";

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
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.scopes != null)
                {
                    jsonWriter.WritePropertyName("scopes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.scopes)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.challengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(_request.challengePeriodEventId.ToString());
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
		///  カウンターの種類マスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateCounterModelMaster(
                Request.CreateCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateCounterModelMasterResult>> callback
        )
		{
			var task = new CreateCounterModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCounterModelMasterTask : Gs2RestSessionTask<Result.GetCounterModelMasterResult>
        {
			private readonly Request.GetCounterModelMasterRequest _request;

			public GetCounterModelMasterTask(Request.GetCounterModelMasterRequest request, UnityAction<AsyncResult<Result.GetCounterModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

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
		///  カウンターの種類マスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCounterModelMaster(
                Request.GetCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetCounterModelMasterResult>> callback
        )
		{
			var task = new GetCounterModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCounterModelMasterTask : Gs2RestSessionTask<Result.UpdateCounterModelMasterResult>
        {
			private readonly Request.UpdateCounterModelMasterRequest _request;

			public UpdateCounterModelMasterTask(Request.UpdateCounterModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateCounterModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.scopes != null)
                {
                    jsonWriter.WritePropertyName("scopes");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.scopes)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.challengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(_request.challengePeriodEventId.ToString());
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
		///  カウンターの種類マスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCounterModelMaster(
                Request.UpdateCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCounterModelMasterResult>> callback
        )
		{
			var task = new UpdateCounterModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteCounterModelMasterTask : Gs2RestSessionTask<Result.DeleteCounterModelMasterResult>
        {
			private readonly Request.DeleteCounterModelMasterRequest _request;

			public DeleteCounterModelMasterTask(Request.DeleteCounterModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteCounterModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

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
		///  カウンターの種類マスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteCounterModelMaster(
                Request.DeleteCounterModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteCounterModelMasterResult>> callback
        )
		{
			var task = new DeleteCounterModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeMissionGroupModelsTask : Gs2RestSessionTask<Result.DescribeMissionGroupModelsResult>
        {
			private readonly Request.DescribeMissionGroupModelsRequest _request;

			public DescribeMissionGroupModelsTask(Request.DescribeMissionGroupModelsRequest request, UnityAction<AsyncResult<Result.DescribeMissionGroupModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/group";

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
		///  ミッショングループの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeMissionGroupModels(
                Request.DescribeMissionGroupModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionGroupModelsResult>> callback
        )
		{
			var task = new DescribeMissionGroupModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetMissionGroupModelTask : Gs2RestSessionTask<Result.GetMissionGroupModelResult>
        {
			private readonly Request.GetMissionGroupModelRequest _request;

			public GetMissionGroupModelTask(Request.GetMissionGroupModelRequest request, UnityAction<AsyncResult<Result.GetMissionGroupModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  ミッショングループを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetMissionGroupModel(
                Request.GetMissionGroupModelRequest request,
                UnityAction<AsyncResult<Result.GetMissionGroupModelResult>> callback
        )
		{
			var task = new GetMissionGroupModelTask(request, callback);
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
                    .Replace("{service}", "mission")
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
		///  現在有効なミッションのマスターデータをエクスポートします<br />
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

        private class GetCurrentMissionMasterTask : Gs2RestSessionTask<Result.GetCurrentMissionMasterResult>
        {
			private readonly Request.GetCurrentMissionMasterRequest _request;

			public GetCurrentMissionMasterTask(Request.GetCurrentMissionMasterRequest request, UnityAction<AsyncResult<Result.GetCurrentMissionMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
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
		///  現在有効なミッションを取得します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCurrentMissionMaster(
                Request.GetCurrentMissionMasterRequest request,
                UnityAction<AsyncResult<Result.GetCurrentMissionMasterResult>> callback
        )
		{
			var task = new GetCurrentMissionMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentMissionMasterTask : Gs2RestSessionTask<Result.UpdateCurrentMissionMasterResult>
        {
			private readonly Request.UpdateCurrentMissionMasterRequest _request;

			public UpdateCurrentMissionMasterTask(Request.UpdateCurrentMissionMasterRequest request, UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
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
		///  現在有効なミッションを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentMissionMaster(
                Request.UpdateCurrentMissionMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterResult>> callback
        )
		{
			var task = new UpdateCurrentMissionMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateCurrentMissionMasterFromGitHubTask : Gs2RestSessionTask<Result.UpdateCurrentMissionMasterFromGitHubResult>
        {
			private readonly Request.UpdateCurrentMissionMasterFromGitHubRequest _request;

			public UpdateCurrentMissionMasterFromGitHubTask(Request.UpdateCurrentMissionMasterFromGitHubRequest request, UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterFromGitHubResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
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
		///  現在有効なミッションを更新します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateCurrentMissionMasterFromGitHub(
                Request.UpdateCurrentMissionMasterFromGitHubRequest request,
                UnityAction<AsyncResult<Result.UpdateCurrentMissionMasterFromGitHubResult>> callback
        )
		{
			var task = new UpdateCurrentMissionMasterFromGitHubTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCountersTask : Gs2RestSessionTask<Result.DescribeCountersResult>
        {
			private readonly Request.DescribeCountersRequest _request;

			public DescribeCountersTask(Request.DescribeCountersRequest request, UnityAction<AsyncResult<Result.DescribeCountersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/counter";

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
		///  カウンターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCounters(
                Request.DescribeCountersRequest request,
                UnityAction<AsyncResult<Result.DescribeCountersResult>> callback
        )
		{
			var task = new DescribeCountersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCountersByUserIdTask : Gs2RestSessionTask<Result.DescribeCountersByUserIdResult>
        {
			private readonly Request.DescribeCountersByUserIdRequest _request;

			public DescribeCountersByUserIdTask(Request.DescribeCountersByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeCountersByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter";

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
		///  ユーザIDを指定してカウンターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCountersByUserId(
                Request.DescribeCountersByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeCountersByUserIdResult>> callback
        )
		{
			var task = new DescribeCountersByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class IncreaseCounterByUserIdTask : Gs2RestSessionTask<Result.IncreaseCounterByUserIdResult>
        {
			private readonly Request.IncreaseCounterByUserIdRequest _request;

			public IncreaseCounterByUserIdTask(Request.IncreaseCounterByUserIdRequest request, UnityAction<AsyncResult<Result.IncreaseCounterByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");
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
		///  カウンターに加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator IncreaseCounterByUserId(
                Request.IncreaseCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.IncreaseCounterByUserIdResult>> callback
        )
		{
			var task = new IncreaseCounterByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCounterTask : Gs2RestSessionTask<Result.GetCounterResult>
        {
			private readonly Request.GetCounterRequest _request;

			public GetCounterTask(Request.GetCounterRequest request, UnityAction<AsyncResult<Result.GetCounterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

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
		///  カウンターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCounter(
                Request.GetCounterRequest request,
                UnityAction<AsyncResult<Result.GetCounterResult>> callback
        )
		{
			var task = new GetCounterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCounterByUserIdTask : Gs2RestSessionTask<Result.GetCounterByUserIdResult>
        {
			private readonly Request.GetCounterByUserIdRequest _request;

			public GetCounterByUserIdTask(Request.GetCounterByUserIdRequest request, UnityAction<AsyncResult<Result.GetCounterByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");
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
		///  ユーザIDを指定してカウンターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCounterByUserId(
                Request.GetCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetCounterByUserIdResult>> callback
        )
		{
			var task = new GetCounterByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteCounterByUserIdTask : Gs2RestSessionTask<Result.DeleteCounterByUserIdResult>
        {
			private readonly Request.DeleteCounterByUserIdRequest _request;

			public DeleteCounterByUserIdTask(Request.DeleteCounterByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteCounterByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

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
		///  カウンターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteCounterByUserId(
                Request.DeleteCounterByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteCounterByUserIdResult>> callback
        )
		{
			var task = new DeleteCounterByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class IncreaseByStampSheetTask : Gs2RestSessionTask<Result.IncreaseByStampSheetResult>
        {
			private readonly Request.IncreaseByStampSheetRequest _request;

			public IncreaseByStampSheetTask(Request.IncreaseByStampSheetRequest request, UnityAction<AsyncResult<Result.IncreaseByStampSheetResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/increase";

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
		///  カウンター加算<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator IncreaseByStampSheet(
                Request.IncreaseByStampSheetRequest request,
                UnityAction<AsyncResult<Result.IncreaseByStampSheetResult>> callback
        )
		{
			var task = new IncreaseByStampSheetTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCompletesTask : Gs2RestSessionTask<Result.DescribeCompletesResult>
        {
			private readonly Request.DescribeCompletesRequest _request;

			public DescribeCompletesTask(Request.DescribeCompletesRequest request, UnityAction<AsyncResult<Result.DescribeCompletesResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete";

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
		///  達成状況の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCompletes(
                Request.DescribeCompletesRequest request,
                UnityAction<AsyncResult<Result.DescribeCompletesResult>> callback
        )
		{
			var task = new DescribeCompletesTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCompletesByUserIdTask : Gs2RestSessionTask<Result.DescribeCompletesByUserIdResult>
        {
			private readonly Request.DescribeCompletesByUserIdRequest _request;

			public DescribeCompletesByUserIdTask(Request.DescribeCompletesByUserIdRequest request, UnityAction<AsyncResult<Result.DescribeCompletesByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete";

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
		///  ユーザIDを指定して達成状況の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCompletesByUserId(
                Request.DescribeCompletesByUserIdRequest request,
                UnityAction<AsyncResult<Result.DescribeCompletesByUserIdResult>> callback
        )
		{
			var task = new DescribeCompletesByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CompleteTask : Gs2RestSessionTask<Result.CompleteResult>
        {
			private readonly Request.CompleteRequest _request;

			public CompleteTask(Request.CompleteRequest request, UnityAction<AsyncResult<Result.CompleteResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");

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
                        item.WriteJson(jsonWriter);
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
		///  ミッション達成報酬を受領するためのスタンプシートを発行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Complete(
                Request.CompleteRequest request,
                UnityAction<AsyncResult<Result.CompleteResult>> callback
        )
		{
			var task = new CompleteTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CompleteByUserIdTask : Gs2RestSessionTask<Result.CompleteByUserIdResult>
        {
			private readonly Request.CompleteByUserIdRequest _request;

			public CompleteByUserIdTask(Request.CompleteByUserIdRequest request, UnityAction<AsyncResult<Result.CompleteByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

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
                        item.WriteJson(jsonWriter);
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
		///  達成状況を新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CompleteByUserId(
                Request.CompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.CompleteByUserIdResult>> callback
        )
		{
			var task = new CompleteByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ReceiveByUserIdTask : Gs2RestSessionTask<Result.ReceiveByUserIdResult>
        {
			private readonly Request.ReceiveByUserIdRequest _request;

			public ReceiveByUserIdTask(Request.ReceiveByUserIdRequest request, UnityAction<AsyncResult<Result.ReceiveByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}/task/{missionTaskName}/receive";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");

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
                if (_request.duplicationAvoider != null)
                {
                    UnityWebRequest.SetRequestHeader("X-GS2-DUPLICATION-AVOIDER", _request.duplicationAvoider);
                }

                return Send((Gs2RestSession)gs2Session);
            }
        }

		/// <summary>
		///  ミッション達成報酬を受領する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ReceiveByUserId(
                Request.ReceiveByUserIdRequest request,
                UnityAction<AsyncResult<Result.ReceiveByUserIdResult>> callback
        )
		{
			var task = new ReceiveByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCompleteTask : Gs2RestSessionTask<Result.GetCompleteResult>
        {
			private readonly Request.GetCompleteRequest _request;

			public GetCompleteTask(Request.GetCompleteRequest request, UnityAction<AsyncResult<Result.GetCompleteResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/me/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  達成状況を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetComplete(
                Request.GetCompleteRequest request,
                UnityAction<AsyncResult<Result.GetCompleteResult>> callback
        )
		{
			var task = new GetCompleteTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCompleteByUserIdTask : Gs2RestSessionTask<Result.GetCompleteByUserIdResult>
        {
			private readonly Request.GetCompleteByUserIdRequest _request;

			public GetCompleteByUserIdTask(Request.GetCompleteByUserIdRequest request, UnityAction<AsyncResult<Result.GetCompleteByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
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
		///  ユーザIDを指定して達成状況を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCompleteByUserId(
                Request.GetCompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetCompleteByUserIdResult>> callback
        )
		{
			var task = new GetCompleteByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteCompleteByUserIdTask : Gs2RestSessionTask<Result.DeleteCompleteByUserIdResult>
        {
			private readonly Request.DeleteCompleteByUserIdRequest _request;

			public DeleteCompleteByUserIdTask(Request.DeleteCompleteByUserIdRequest request, UnityAction<AsyncResult<Result.DeleteCompleteByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/user/{userId}/complete/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{userId}", !string.IsNullOrEmpty(_request.userId) ? _request.userId.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  達成状況を削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteCompleteByUserId(
                Request.DeleteCompleteByUserIdRequest request,
                UnityAction<AsyncResult<Result.DeleteCompleteByUserIdResult>> callback
        )
		{
			var task = new DeleteCompleteByUserIdTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class ReceiveByStampTaskTask : Gs2RestSessionTask<Result.ReceiveByStampTaskResult>
        {
			private readonly Request.ReceiveByStampTaskRequest _request;

			public ReceiveByStampTaskTask(Request.ReceiveByStampTaskRequest request, UnityAction<AsyncResult<Result.ReceiveByStampTaskResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/stamp/receive";

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
		///  達成状況を作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator ReceiveByStampTask(
                Request.ReceiveByStampTaskRequest request,
                UnityAction<AsyncResult<Result.ReceiveByStampTaskResult>> callback
        )
		{
			var task = new ReceiveByStampTaskTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeCounterModelsTask : Gs2RestSessionTask<Result.DescribeCounterModelsResult>
        {
			private readonly Request.DescribeCounterModelsRequest _request;

			public DescribeCounterModelsTask(Request.DescribeCounterModelsRequest request, UnityAction<AsyncResult<Result.DescribeCounterModelsResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/counter";

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
		///  カウンターの種類の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeCounterModels(
                Request.DescribeCounterModelsRequest request,
                UnityAction<AsyncResult<Result.DescribeCounterModelsResult>> callback
        )
		{
			var task = new DescribeCounterModelsTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetCounterModelTask : Gs2RestSessionTask<Result.GetCounterModelResult>
        {
			private readonly Request.GetCounterModelRequest _request;

			public GetCounterModelTask(Request.GetCounterModelRequest request, UnityAction<AsyncResult<Result.GetCounterModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/counter/{counterName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{counterName}", !string.IsNullOrEmpty(_request.counterName) ? _request.counterName.ToString() : "null");

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
		///  カウンターの種類を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCounterModel(
                Request.GetCounterModelRequest request,
                UnityAction<AsyncResult<Result.GetCounterModelResult>> callback
        )
		{
			var task = new GetCounterModelTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeMissionGroupModelMastersTask : Gs2RestSessionTask<Result.DescribeMissionGroupModelMastersResult>
        {
			private readonly Request.DescribeMissionGroupModelMastersRequest _request;

			public DescribeMissionGroupModelMastersTask(Request.DescribeMissionGroupModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeMissionGroupModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group";

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
		///  ミッショングループマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeMissionGroupModelMasters(
                Request.DescribeMissionGroupModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionGroupModelMastersResult>> callback
        )
		{
			var task = new DescribeMissionGroupModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateMissionGroupModelMasterTask : Gs2RestSessionTask<Result.CreateMissionGroupModelMasterResult>
        {
			private readonly Request.CreateMissionGroupModelMasterRequest _request;

			public CreateMissionGroupModelMasterTask(Request.CreateMissionGroupModelMasterRequest request, UnityAction<AsyncResult<Result.CreateMissionGroupModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group";

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
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.completeNotificationNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeNotificationNamespaceId");
                    jsonWriter.Write(_request.completeNotificationNamespaceId.ToString());
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
		///  ミッショングループマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateMissionGroupModelMaster(
                Request.CreateMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateMissionGroupModelMasterResult>> callback
        )
		{
			var task = new CreateMissionGroupModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetMissionGroupModelMasterTask : Gs2RestSessionTask<Result.GetMissionGroupModelMasterResult>
        {
			private readonly Request.GetMissionGroupModelMasterRequest _request;

			public GetMissionGroupModelMasterTask(Request.GetMissionGroupModelMasterRequest request, UnityAction<AsyncResult<Result.GetMissionGroupModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  ミッショングループマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetMissionGroupModelMaster(
                Request.GetMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetMissionGroupModelMasterResult>> callback
        )
		{
			var task = new GetMissionGroupModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateMissionGroupModelMasterTask : Gs2RestSessionTask<Result.UpdateMissionGroupModelMasterResult>
        {
			private readonly Request.UpdateMissionGroupModelMasterRequest _request;

			public UpdateMissionGroupModelMasterTask(Request.UpdateMissionGroupModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateMissionGroupModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.completeNotificationNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeNotificationNamespaceId");
                    jsonWriter.Write(_request.completeNotificationNamespaceId.ToString());
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
		///  ミッショングループマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateMissionGroupModelMaster(
                Request.UpdateMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateMissionGroupModelMasterResult>> callback
        )
		{
			var task = new UpdateMissionGroupModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteMissionGroupModelMasterTask : Gs2RestSessionTask<Result.DeleteMissionGroupModelMasterResult>
        {
			private readonly Request.DeleteMissionGroupModelMasterRequest _request;

			public DeleteMissionGroupModelMasterTask(Request.DeleteMissionGroupModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteMissionGroupModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  ミッショングループマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteMissionGroupModelMaster(
                Request.DeleteMissionGroupModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteMissionGroupModelMasterResult>> callback
        )
		{
			var task = new DeleteMissionGroupModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DescribeMissionTaskModelMastersTask : Gs2RestSessionTask<Result.DescribeMissionTaskModelMastersResult>
        {
			private readonly Request.DescribeMissionTaskModelMastersRequest _request;

			public DescribeMissionTaskModelMastersTask(Request.DescribeMissionTaskModelMastersRequest request, UnityAction<AsyncResult<Result.DescribeMissionTaskModelMastersResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

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
		///  ミッションタスクマスターの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DescribeMissionTaskModelMasters(
                Request.DescribeMissionTaskModelMastersRequest request,
                UnityAction<AsyncResult<Result.DescribeMissionTaskModelMastersResult>> callback
        )
		{
			var task = new DescribeMissionTaskModelMastersTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class CreateMissionTaskModelMasterTask : Gs2RestSessionTask<Result.CreateMissionTaskModelMasterResult>
        {
			private readonly Request.CreateMissionTaskModelMasterRequest _request;

			public CreateMissionTaskModelMasterTask(Request.CreateMissionTaskModelMasterRequest request, UnityAction<AsyncResult<Result.CreateMissionTaskModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.counterName != null)
                {
                    jsonWriter.WritePropertyName("counterName");
                    jsonWriter.Write(_request.counterName.ToString());
                }
                if (_request.resetType != null)
                {
                    jsonWriter.WritePropertyName("resetType");
                    jsonWriter.Write(_request.resetType.ToString());
                }
                if (_request.targetValue != null)
                {
                    jsonWriter.WritePropertyName("targetValue");
                    jsonWriter.Write(_request.targetValue.ToString());
                }
                if (_request.completeAcquireActions != null)
                {
                    jsonWriter.WritePropertyName("completeAcquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.completeAcquireActions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.challengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(_request.challengePeriodEventId.ToString());
                }
                if (_request.premiseMissionTaskName != null)
                {
                    jsonWriter.WritePropertyName("premiseMissionTaskName");
                    jsonWriter.Write(_request.premiseMissionTaskName.ToString());
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
		///  ミッションタスクマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateMissionTaskModelMaster(
                Request.CreateMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateMissionTaskModelMasterResult>> callback
        )
		{
			var task = new CreateMissionTaskModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class GetMissionTaskModelMasterTask : Gs2RestSessionTask<Result.GetMissionTaskModelMasterResult>
        {
			private readonly Request.GetMissionTaskModelMasterRequest _request;

			public GetMissionTaskModelMasterTask(Request.GetMissionTaskModelMasterRequest request, UnityAction<AsyncResult<Result.GetMissionTaskModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbGET;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");

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
		///  ミッションタスクマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetMissionTaskModelMaster(
                Request.GetMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetMissionTaskModelMasterResult>> callback
        )
		{
			var task = new GetMissionTaskModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class UpdateMissionTaskModelMasterTask : Gs2RestSessionTask<Result.UpdateMissionTaskModelMasterResult>
        {
			private readonly Request.UpdateMissionTaskModelMasterRequest _request;

			public UpdateMissionTaskModelMasterTask(Request.UpdateMissionTaskModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateMissionTaskModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPUT;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");

                UnityWebRequest.url = url;

                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);
                jsonWriter.WriteObjectStart();
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.counterName != null)
                {
                    jsonWriter.WritePropertyName("counterName");
                    jsonWriter.Write(_request.counterName.ToString());
                }
                if (_request.resetType != null)
                {
                    jsonWriter.WritePropertyName("resetType");
                    jsonWriter.Write(_request.resetType.ToString());
                }
                if (_request.targetValue != null)
                {
                    jsonWriter.WritePropertyName("targetValue");
                    jsonWriter.Write(_request.targetValue.ToString());
                }
                if (_request.completeAcquireActions != null)
                {
                    jsonWriter.WritePropertyName("completeAcquireActions");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.completeAcquireActions)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.challengePeriodEventId != null)
                {
                    jsonWriter.WritePropertyName("challengePeriodEventId");
                    jsonWriter.Write(_request.challengePeriodEventId.ToString());
                }
                if (_request.premiseMissionTaskName != null)
                {
                    jsonWriter.WritePropertyName("premiseMissionTaskName");
                    jsonWriter.Write(_request.premiseMissionTaskName.ToString());
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
		///  ミッションタスクマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateMissionTaskModelMaster(
                Request.UpdateMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateMissionTaskModelMasterResult>> callback
        )
		{
			var task = new UpdateMissionTaskModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
        }

        private class DeleteMissionTaskModelMasterTask : Gs2RestSessionTask<Result.DeleteMissionTaskModelMasterResult>
        {
			private readonly Request.DeleteMissionTaskModelMasterRequest _request;

			public DeleteMissionTaskModelMasterTask(Request.DeleteMissionTaskModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteMissionTaskModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbDELETE;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "mission")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/{namespaceName}/master/group/{missionGroupName}/task/{missionTaskName}";

                url = url.Replace("{namespaceName}", !string.IsNullOrEmpty(_request.namespaceName) ? _request.namespaceName.ToString() : "null");
                url = url.Replace("{missionGroupName}", !string.IsNullOrEmpty(_request.missionGroupName) ? _request.missionGroupName.ToString() : "null");
                url = url.Replace("{missionTaskName}", !string.IsNullOrEmpty(_request.missionTaskName) ? _request.missionTaskName.ToString() : "null");

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
		///  ミッションタスクマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteMissionTaskModelMaster(
                Request.DeleteMissionTaskModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteMissionTaskModelMasterResult>> callback
        )
		{
			var task = new DeleteMissionTaskModelMasterTask(request, callback);
			return Gs2RestSession.Execute(task);
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
                    .Replace("{service}", "mission")
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
                    .Replace("{service}", "mission")
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
                if (_request.missionCompleteTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteTriggerScriptId");
                    jsonWriter.Write(_request.missionCompleteTriggerScriptId.ToString());
                }
                if (_request.missionCompleteDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteDoneTriggerScriptId");
                    jsonWriter.Write(_request.missionCompleteDoneTriggerScriptId.ToString());
                }
                if (_request.missionCompleteDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.missionCompleteDoneTriggerQueueNamespaceId.ToString());
                }
                if (_request.counterIncrementTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementTriggerScriptId");
                    jsonWriter.Write(_request.counterIncrementTriggerScriptId.ToString());
                }
                if (_request.counterIncrementDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementDoneTriggerScriptId");
                    jsonWriter.Write(_request.counterIncrementDoneTriggerScriptId.ToString());
                }
                if (_request.counterIncrementDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.counterIncrementDoneTriggerQueueNamespaceId.ToString());
                }
                if (_request.receiveRewardsTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsTriggerScriptId");
                    jsonWriter.Write(_request.receiveRewardsTriggerScriptId.ToString());
                }
                if (_request.receiveRewardsDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsDoneTriggerScriptId");
                    jsonWriter.Write(_request.receiveRewardsDoneTriggerScriptId.ToString());
                }
                if (_request.receiveRewardsDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.receiveRewardsDoneTriggerQueueNamespaceId.ToString());
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
                if (_request.completeNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    _request.completeNotification.WriteJson(jsonWriter);
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
                    .Replace("{service}", "mission")
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
                    .Replace("{service}", "mission")
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
                    .Replace("{service}", "mission")
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
                if (_request.missionCompleteTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteTriggerScriptId");
                    jsonWriter.Write(_request.missionCompleteTriggerScriptId.ToString());
                }
                if (_request.missionCompleteDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteDoneTriggerScriptId");
                    jsonWriter.Write(_request.missionCompleteDoneTriggerScriptId.ToString());
                }
                if (_request.missionCompleteDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("missionCompleteDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.missionCompleteDoneTriggerQueueNamespaceId.ToString());
                }
                if (_request.counterIncrementTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementTriggerScriptId");
                    jsonWriter.Write(_request.counterIncrementTriggerScriptId.ToString());
                }
                if (_request.counterIncrementDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementDoneTriggerScriptId");
                    jsonWriter.Write(_request.counterIncrementDoneTriggerScriptId.ToString());
                }
                if (_request.counterIncrementDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("counterIncrementDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.counterIncrementDoneTriggerQueueNamespaceId.ToString());
                }
                if (_request.receiveRewardsTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsTriggerScriptId");
                    jsonWriter.Write(_request.receiveRewardsTriggerScriptId.ToString());
                }
                if (_request.receiveRewardsDoneTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsDoneTriggerScriptId");
                    jsonWriter.Write(_request.receiveRewardsDoneTriggerScriptId.ToString());
                }
                if (_request.receiveRewardsDoneTriggerQueueNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("receiveRewardsDoneTriggerQueueNamespaceId");
                    jsonWriter.Write(_request.receiveRewardsDoneTriggerQueueNamespaceId.ToString());
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
                if (_request.completeNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    _request.completeNotification.WriteJson(jsonWriter);
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
                    .Replace("{service}", "mission")
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
	}
}