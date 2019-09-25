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
	public class Gs2AuthRestClient : AbstractGs2Client
	{

		public static string Endpoint = "auth";

        protected Gs2RestSession Gs2RestSession => (Gs2RestSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2RestSession">REST API 用セッション</param>
		public Gs2AuthRestClient(Gs2RestSession Gs2RestSession) : base(Gs2RestSession)
		{

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
                    .Replace("{service}", "auth")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/login";

                UnityWebRequest.url = url;

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
			return Gs2RestSession.Execute(task);
        }

        private class LoginBySignatureTask : Gs2RestSessionTask<Result.LoginBySignatureResult>
        {
			private readonly Request.LoginBySignatureRequest _request;

			public LoginBySignatureTask(Request.LoginBySignatureRequest request, UnityAction<AsyncResult<Result.LoginBySignatureResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
				UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;

                var url = Gs2RestSession.EndpointHost
                    .Replace("{service}", "auth")
                    .Replace("{region}", gs2Session.Region.DisplayName())
                    + "/login/signed";

                UnityWebRequest.url = url;

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
			return Gs2RestSession.Execute(task);
        }
	}
}