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

namespace Gs2.Gs2Auth
{
	public class Gs2AuthWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "auth";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2AuthWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

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

                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.timeOffset != null)
                {
                    jsonWriter.WritePropertyName("timeOffset");
                    jsonWriter.Write(_request.timeOffset.ToString());
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
                jsonWriter.Write("auth");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("accessToken");
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
		///  指定したユーザIDでGS2にログインし、アクセストークンを取得します<br />
		///    本APIは信頼出来るゲームサーバーから呼び出されることを想定しています。<br />
		///    ユーザIDの値の検証処理が存在しないため、クライアントから呼び出すのは不適切です。<br />
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

        private class LoginBySignatureTask : Gs2WebSocketSessionTask<Result.LoginBySignatureResult>
        {
			private readonly Request.LoginBySignatureRequest _request;

			public LoginBySignatureTask(Request.LoginBySignatureRequest request, UnityAction<AsyncResult<Result.LoginBySignatureResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.body != null)
                {
                    jsonWriter.WritePropertyName("body");
                    jsonWriter.Write(_request.body.ToString());
                }
                if (_request.signature != null)
                {
                    jsonWriter.WritePropertyName("signature");
                    jsonWriter.Write(_request.signature.ToString());
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
                jsonWriter.Write("auth");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("accessToken");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("loginBySignature");
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
		///  指定したユーザIDでGS2にログインし、アクセストークンを取得します<br />
		///    ユーザIDの署名検証を実施することで、本APIはクライアントから呼び出しても安全です。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator LoginBySignature(
                Request.LoginBySignatureRequest request,
                UnityAction<AsyncResult<Result.LoginBySignatureResult>> callback
        )
		{
			var task = new LoginBySignatureTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}