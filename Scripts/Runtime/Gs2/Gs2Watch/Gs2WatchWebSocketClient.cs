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

namespace Gs2.Gs2Watch
{
	public class Gs2WatchWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "watch";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2WatchWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}

        private class GetChartTask : Gs2WebSocketSessionTask<Result.GetChartResult>
        {
			private readonly Request.GetChartRequest _request;

			public GetChartTask(Request.GetChartRequest request, UnityAction<AsyncResult<Result.GetChartResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.metrics != null)
                {
                    jsonWriter.WritePropertyName("metrics");
                    jsonWriter.Write(_request.metrics.ToString());
                }
                if (_request.grn != null)
                {
                    jsonWriter.WritePropertyName("grn");
                    jsonWriter.Write(_request.grn.ToString());
                }
                if (_request.queries != null)
                {
                    jsonWriter.WritePropertyName("queries");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.queries)
                    {
                        jsonWriter.Write(item);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.by != null)
                {
                    jsonWriter.WritePropertyName("by");
                    jsonWriter.Write(_request.by.ToString());
                }
                if (_request.timeframe != null)
                {
                    jsonWriter.WritePropertyName("timeframe");
                    jsonWriter.Write(_request.timeframe.ToString());
                }
                if (_request.size != null)
                {
                    jsonWriter.WritePropertyName("size");
                    jsonWriter.Write(_request.size.ToString());
                }
                if (_request.format != null)
                {
                    jsonWriter.WritePropertyName("format");
                    jsonWriter.Write(_request.format.ToString());
                }
                if (_request.aggregator != null)
                {
                    jsonWriter.WritePropertyName("aggregator");
                    jsonWriter.Write(_request.aggregator.ToString());
                }
                if (_request.style != null)
                {
                    jsonWriter.WritePropertyName("style");
                    jsonWriter.Write(_request.style.ToString());
                }
                if (_request.title != null)
                {
                    jsonWriter.WritePropertyName("title");
                    jsonWriter.Write(_request.title.ToString());
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
                jsonWriter.Write("watch");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("chart");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getChart");
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
		///  チャートを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetChart(
                Request.GetChartRequest request,
                UnityAction<AsyncResult<Result.GetChartResult>> callback
        )
		{
			var task = new GetChartTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetCumulativeTask : Gs2WebSocketSessionTask<Result.GetCumulativeResult>
        {
			private readonly Request.GetCumulativeRequest _request;

			public GetCumulativeTask(Request.GetCumulativeRequest request, UnityAction<AsyncResult<Result.GetCumulativeResult>> userCallback) : base(userCallback)
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
                if (_request.resourceGrn != null)
                {
                    jsonWriter.WritePropertyName("resourceGrn");
                    jsonWriter.Write(_request.resourceGrn.ToString());
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
                jsonWriter.Write("watch");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("cumulative");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getCumulative");
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
		///  累積値を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetCumulative(
                Request.GetCumulativeRequest request,
                UnityAction<AsyncResult<Result.GetCumulativeResult>> callback
        )
		{
			var task = new GetCumulativeTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}