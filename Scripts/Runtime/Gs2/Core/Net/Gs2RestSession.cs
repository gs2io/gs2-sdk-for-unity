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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Gs2Identifier.Model;
using LitJson;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Core.Net
{
    public class Gs2RestSession : Gs2Session
    {
        public const string EndpointHost = "https://{service}.{region}.gen2.gs2io.com";

        private class LoginResult
        {
            /** プロジェクトトークン */
            public string access_token { set; get; }

            /** Bearer */
            public string token_type { set; get; }

            /** 有効期間(秒) */
            public int? expires_in { set; get; }

        }

        private class LoginTask : Gs2RestTask
        {
            private Gs2RestSession _gs2RestSession;

            public LoginTask(Gs2RestSession gs2RestSession)
            {
                _gs2RestSession = gs2RestSession;
            }

            public override void Callback(Gs2RestResponse gs2RestResponse)
            {
                var error = gs2RestResponse.Error;
                string accessToken = null;

                if (error == null)
                {
                    try
                    {
                        accessToken = JsonMapper.ToObject<LoginResult>(gs2RestResponse.Message).access_token;
                    }
                    catch (System.Exception e)
                    {
                        error = new UnknownException("JSON parsing error: \n" + gs2RestResponse.Message);
                    }
                }
                
                _gs2RestSession.OpenCallback(accessToken, error);
            }
        }

        private LoginTask _loginTask;

        public Gs2RestSession(BasicGs2Credential basicGs2Credential) : base(basicGs2Credential)
        {
        }

        public Gs2RestSession(BasicGs2Credential basicGs2Credential, Region region) : base(basicGs2Credential, region)
        {
        }

        public Gs2RestSession(BasicGs2Credential basicGs2Credential, string region) : base(basicGs2Credential, region)
        {
        }

        public IEnumerator Execute(Gs2RestSessionTask gs2RestSessionTask)
        {
            return base.Execute(gs2RestSessionTask);
        }

        protected override IEnumerator OpenImpl()
        {
            _loginTask = new LoginTask(this);

            _loginTask.UnityWebRequest.method = UnityWebRequest.kHttpVerbPOST;
            _loginTask.UnityWebRequest.url = EndpointHost
                .Replace("{service}", "identifier")
                .Replace("{region}", Region.DisplayName())
                + "/projectToken/login";
            _loginTask.UnityWebRequest.SetRequestHeader("Content-Type", "application/json");

            var stringBuilder = new StringBuilder();
            var jsonWriter = new JsonWriter(stringBuilder);
            jsonWriter.WriteObjectStart();
            if(Credential.ClientId != null)
            {
                jsonWriter.WritePropertyName("client_id");
                jsonWriter.Write(Credential.ClientId);
            }
            if(Credential.ClientSecret != null)
            {
                jsonWriter.WritePropertyName("client_secret");
                jsonWriter.Write(Credential.ClientSecret);
            }
            jsonWriter.WriteObjectEnd();

            var body = stringBuilder.ToString();
            if (!string.IsNullOrEmpty(body))
            {
                _loginTask.UnityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            }

            yield return _loginTask.Send();

            _loginTask = null;
        }

        protected override IEnumerator CancelOpenImpl()
        {
            _loginTask.UnityWebRequest.Abort();
            yield break;
        }

        protected override IEnumerator CloseImpl()
        {
            CloseCallback();
            yield break;
        }
    }
}
