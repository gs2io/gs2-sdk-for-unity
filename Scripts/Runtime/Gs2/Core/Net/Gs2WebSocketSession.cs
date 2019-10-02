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

using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using LitJson;
using UnityEngine;
using WebSocketSharp;

namespace Gs2.Core.Net
{
    public class Gs2WebSocketSession : Gs2Session
    {
        public const string EndpointHost = "wss://gateway-ws.{region}.gen2.gs2io.com";

        public delegate void NotificationHandler(NotificationMessage message);
        public event NotificationHandler OnNotificationMessage;

        private class LoginResult
        {
            /** プロジェクトトークン */
            public string access_token { set; get; }

            /** Bearer */
            public string token_type { set; get; }

            /** 有効期間(秒) */
            public int? expires_in { set; get; }

            public static LoginResult FromDict(JsonData data)
            {
                if (data == null)
                {
                    return new LoginResult();
                }
                return new LoginResult
                {
                    access_token = data.Keys.Contains("access_token") ? (string)data["access_token"] : null,
                    token_type = data.Keys.Contains("token_type") ? (string)data["token_type"] : null,
                    expires_in = data.Keys.Contains("expires_in") ? (int?)data["expires_in"] : null,
                };
            }

        }

        enum State
        {
            Idle,
            Opening,
            LoggingIn,
            LoginFailed,
            Available,
        }

        private State _state = State.Idle;
        private Gs2Exception _lastGs2Exception = null;

        private readonly WebSocket _webSocket;

        private WebSocket CreateWebSocket()
        {
            var url = EndpointHost.Replace("{region}", Region.DisplayName());

            var webSocket = new WebSocket(url) {SslConfiguration = {EnabledSslProtocols = SslProtocols.Tls12}};

            webSocket.OnOpen += (sender, eventArgs) =>
            {
                _state = State.LoggingIn;

                webSocket.SendAsync(
                    "{" +
                       $"\"client_id\": \"{Credential.ClientId}\"," +
                       $"\"client_secret\": \"{Credential.ClientSecret}\"," +
                        "\"x_gs2\": {" +
                            "\"service\": \"identifier\"," +
                            "\"component\": \"projectToken\"," +
                            "\"function\": \"login\"," +
                            "\"contentType\": \"application/json\"," +
                           $"\"requestId\": \"{Gs2SessionTaskId.LoginId.ToString()}\"" +
                        "}" +
                    "}",
                    null
                );
            };

            webSocket.OnMessage += (sender, messageEventArgs) =>
            {
                if (messageEventArgs.IsText)
                {
                    var gs2WebSocketResponse = new Gs2WebSocketResponse(messageEventArgs.Data);

                    switch (_state)
                    {
                        case State.LoggingIn:
                            if (gs2WebSocketResponse.Gs2SessionTaskId == Gs2SessionTaskId.LoginId)
                            {
                                if (gs2WebSocketResponse.Error == null)
                                {
                                    LoginResult loginResult = LoginResult.FromDict(gs2WebSocketResponse.Body);
                                    if (loginResult.access_token != null)
                                    {
                                        _state = State.Available;
                                        OpenCallback(loginResult.access_token, null);
                                    }
                                    else
                                    {
                                        _lastGs2Exception = new UnknownException("No project token returned.");
                                        _state = State.LoginFailed;
                                        
                                        webSocket.CloseAsync();
                                    }
                                }
                                else
                                {
                                    _lastGs2Exception = gs2WebSocketResponse.Error;
                                    _state = State.LoginFailed;

                                    webSocket.CloseAsync();
                                }
                            }
                            break;

                        case State.Available:
                            if (gs2WebSocketResponse.Gs2SessionTaskId == Gs2SessionTaskId.InvalidId)
                            {
                                // API 応答以外のメッセージ
                                OnNotificationMessage?.Invoke(
                                    NotificationMessage.FromDict(gs2WebSocketResponse.Body)
                                );
                            }
                            else
                            {
                                OnMessage(gs2WebSocketResponse.Gs2SessionTaskId, gs2WebSocketResponse);
                            }
                            break;
                        
                        case State.Idle:
                        case State.Opening:
                        case State.LoginFailed:
                            break;
                    }
                }
            };

            webSocket.OnClose += (sender, closeEventArgs) =>
            {
                var state = _state;

                _state = State.Idle;

                switch (state)
                {
                    case State.Idle:
                        // 来ない
                        break;
                    
                    case State.Opening:    // TODO: OnError を通ってからくるか確認
                    case State.LoggingIn:
                    case State.LoginFailed:
                        // Gs2Session としては Available になっていないので closeCallback ではなく openCallback に失敗を伝える
                        OpenCallback(null, _lastGs2Exception);
                        break;
                    
                    case State.Available:
                        // 自発的な切断も外部要因による切断もここ
                        CloseCallback();    // TODO: Cancel にわたすエラーを引数に取る
                        break;
                }
            };

            webSocket.OnError += (sender, errorEventArgs) =>
            {
                var gs2Exception = new SessionNotOpenException("Session no longer open.");

                switch (_state)
                {
                    case State.Idle:
                        // 来ない
                        break;
                    
                    case State.Opening:
                        // この直後に OnClose が呼ばれる
                        _lastGs2Exception = gs2Exception;
                        break;
                    
                    case State.LoggingIn:
                        _lastGs2Exception = gs2Exception;
                        _state = State.LoginFailed;
                        webSocket.CloseAsync();
                        break;
                        
                    case State.LoginFailed:
                        // 来ないはず
                        break;
                    
                    case State.Available:
                        // 実行中のタスクのどれが失敗したのかわからないので、全部失敗にする
                        // TODO
                        break;
                }
            };

            return webSocket;
        }

        public Gs2WebSocketSession(BasicGs2Credential basicGs2Credential) : base(basicGs2Credential)
        {
            _webSocket = CreateWebSocket();
        }

        public Gs2WebSocketSession(BasicGs2Credential basicGs2Credential, Region region) : base(basicGs2Credential, region)
        {
            _webSocket = CreateWebSocket();
        }

        public Gs2WebSocketSession(BasicGs2Credential basicGs2Credential, string region) : base(basicGs2Credential, region)
        {
            _webSocket = CreateWebSocket();
        }

        public IEnumerator Execute(Gs2WebSocketSessionTask gs2WebSocketSessionTask)
        {
            return base.Execute(gs2WebSocketSessionTask);
        }

        public void Send(string message)
        {
            _webSocket.SendAsync(message, null);
        }

        protected override IEnumerator OpenImpl()
        {
            _state = State.Opening;

            _webSocket.ConnectAsync();

            yield break;
        }

        protected override IEnumerator CancelOpenImpl()
        {
            _webSocket.CloseAsync();

            yield break;
        }

        protected override IEnumerator CloseImpl()
        {
            // コールバックスレッド以外からのステート変更は排他を保証できない
            // （Gs2Session と独立してロックを用意するとデッドロックしうる）ので、
            // ステートは変化させない

            _webSocket.CloseAsync();

            yield break;
        }
    }
}
