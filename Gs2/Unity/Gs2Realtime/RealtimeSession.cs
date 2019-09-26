using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Exception;
using Gs2.Unity.Gs2Realtime.Util;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;

namespace Gs2.Unity.Gs2Realtime
{
    public delegate void OnMessageHandler(BinaryMessage message);
    public delegate void OnJoinPlayerHandler(Player player);
    public delegate void OnLeavePlayerHandler(Player player);
    public delegate void OnUpdateProfileHandler(Player player);
    public delegate void OnErrorHandler(Error e);
    public delegate void OnGeneralErrorHandler(ErrorEventArgs e);
    public delegate void OnCloseHandler(CloseEventArgs e);
    
    public class RealtimeSession : IDisposable
    {
        protected readonly WebSocket _webSocket;
        protected readonly Messenger _messenger;
        private MonoBehaviour _monoBehaviour;
        private Coroutine _dispatchCoroutine;

        protected readonly string _accessToken;
        
        protected readonly Queue<RealtimeEvent> _eventQueue = new Queue<RealtimeEvent>();

        public bool Connected { get; private set; } = false;
        public uint MyConnectionId { get; private set; } = 0;
        public ByteString Profile { get; private set; } = null;

        public event OnMessageHandler OnMessage;
        public event OnJoinPlayerHandler OnJoinPlayer;
        public event OnLeavePlayerHandler OnLeavePlayer;
        public event OnUpdateProfileHandler OnUpdateProfile;
        public event OnErrorHandler OnError;
        public event OnGeneralErrorHandler OnGeneralError;
        public event OnCloseHandler OnClose;

        protected virtual void EventHandler(RealtimeEvent @event)
        {
            switch (@event.EventType)
            {
                case RealtimeEventType.OnMessage:
                    if (OnMessage != null)
                    {
                        OnMessage.Invoke((@event as OnMessageEvent).Message);
                    }

                    break;
                case RealtimeEventType.OnJoinPlayer:
                    if (OnJoinPlayer != null)
                    {
                        OnJoinPlayer.Invoke((@event as OnJoinPlayerEvent).Player);
                    }

                    break;
                case RealtimeEventType.OnLeavePlayer:
                    if (OnLeavePlayer != null)
                    {
                        OnLeavePlayer.Invoke((@event as OnLeavePlayerEvent).Player);
                    }

                    break;
                case RealtimeEventType.OnUpdateProfile:
                    if (OnUpdateProfile != null)
                    {
                        OnUpdateProfile.Invoke((@event as OnUpdateProfileEvent).Player);
                    }

                    break;
                case RealtimeEventType.OnError:
                    if (OnError != null)
                    {
                        OnError.Invoke((@event as OnErrorEvent).Error);
                    }

                    break;
                case RealtimeEventType.OnGeneralError:
                    if (OnGeneralError != null)
                    {
                        OnGeneralError.Invoke((@event as OnGeneralErrorEvent).Error);
                    }

                    break;
                case RealtimeEventType.OnClose:
                    if (OnClose != null)
                    {
                        OnClose.Invoke((@event as OnCloseEvent).Error);
                    }

                    break;
                case RealtimeEventType.PluginEventType:
                    break;
            }
        }
        
        public IEnumerator Dispatch()
        {
            while (true)
            {
                if (_eventQueue.Count > 0)
                {
                    var @event = _eventQueue.Dequeue();
                    EventHandler(@event);
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        public RealtimeSession(
            string accessToken, 
            string ipAddress, 
            int port, 
            string encryptionKey,
            ByteString profile = null
        )
        {
            Debug.Log(string.Format("ws://{0}:{1}/", ipAddress, port));
            _webSocket = new WebSocket(string.Format("ws://{0}:{1}/", ipAddress, port));
            _messenger = new Messenger(encryptionKey);
            _accessToken = accessToken;
            Profile = profile;
            
            _webSocket.OnClose += (sender, args) =>
            {
                if (Connected)
                {
                    _eventQueue.Enqueue(
                        new OnCloseEvent(args)
                    );

                    Connected = false;
                }
            };
        }

        protected void OnErrorHandler(object sender, ErrorEventArgs e)
        {
            _eventQueue.Enqueue(
                new OnGeneralErrorEvent(e)
            );
        }

        protected virtual void OnMessageHandler(object sender, EventArgs e)
        {
            if (!(e is MessageEventArgs data)) return;
            
            var (messageType, payload) = _messenger.Unpack(data.RawData);
            var message = _messenger.Parse(messageType, payload);
            if (message is JoinNotification joinNotification)
            {
                _eventQueue.Enqueue(
                    new OnJoinPlayerEvent(joinNotification.JoinPlayer)
                );
            }
            if (message is LeaveNotification leaveNotification)
            {
                _eventQueue.Enqueue(
                    new OnLeavePlayerEvent(leaveNotification.LeavePlayer)
                );
            }
            if (message is UpdateProfileNotification updateProfileNotification)
            {
                _eventQueue.Enqueue(
                    new OnUpdateProfileEvent(updateProfileNotification.UpdatePlayer)
                );
            }
            if (message is BinaryMessage binaryMessage)
            {
                _eventQueue.Enqueue(
                    new OnMessageEvent(binaryMessage)
                );
            }
            if (message is Error error)
            {
                _eventQueue.Enqueue(
                    new OnErrorEvent(error)
                );
            }
        }
        
        public IEnumerator Connect(
            MonoBehaviour monoBehaviour,
            UnityAction<AsyncResult<bool>> callback
        )
        {
            _webSocket.OnMessage -= this.OnMessageHandler;
            _webSocket.OnError -= this.OnErrorHandler;
            
            var done = false;
            var success = false;
            EventArgs args = null;
            Player[] players = null;
            
            void OnOpenHandler(object sender, EventArgs e)
            {
                // 完了フラグ・成功フラグを立てる
                Debug.Log("OnOpenHandler: " + e);
                done = true;
                success = true;
            }
            void OnErrorHandler(object sender, EventArgs e)
            {
                Debug.Log("OnErrorHandler: " + e);
                // 失敗理由を記録
                args = e;
            }
            void OnCloseHandler(object sender, EventArgs e)
            {
                Debug.Log("OnCloseHandler: " + e);
                // 完了フラグを立てる
                done = true;
            }

            try
            {
                _webSocket.OnOpen += OnOpenHandler;
                _webSocket.OnError += OnErrorHandler;
                _webSocket.OnClose += OnCloseHandler;
                _webSocket.ConnectAsync();

                for (var i=0; i<30 && !done; i++)
                {
                    yield return new WaitForSeconds(1);
                }

                if (!success)
                {
                    // 失敗した場合は抜ける
                    yield break;
                }

                success = false;
                done = false;

                Debug.Log("Hello");
                HelloResult helloResult = null;
                void OnMessageHandler(object sender, EventArgs e)
                {
                    // 認証処理の応答を処理するためのハンドラ
                    Debug.Log("OnMessage: " + e.ToString());
                    if (e is MessageEventArgs data)
                    {
                        var (messageType, payload) = _messenger.Unpack(data.RawData);
                        var message = _messenger.Parse(messageType, payload);
                        if (message is Error error)
                        {
                            _eventQueue.Enqueue(
                                new OnErrorEvent(error)
                            );
                            return;
                        }
                        helloResult = message as HelloResult;
                        success = true;
                    }
                    else
                    {
                        args = e;
                    }

                    done = true;
                }

                try
                {
                    _webSocket.OnMessage += OnMessageHandler;
                    
                    Debug.Log("SendAsync");
                    _webSocket.SendAsync(
                        _messenger.Pack(
                            new HelloRequest
                            {
                                AccessToken = _accessToken,
                                MyProfile = Profile,
                            }
                        ), completed =>
                        {
                            if (completed)
                            {
                                success = true;
                            }

                            done = true;
                        }
                    );
                    
                    for (var i=0; i<30 && !done; i++)
                    {
                        yield return new WaitForSeconds(1);
                    }

                    if (!success || helloResult == null)
                    {
                        // 失敗した場合は抜ける
                        yield break;
                    }

                    MyConnectionId = helloResult.MyProfile.ConnectionId;
                    players = helloResult.Players.ToArray();

                    Connected = true;
                }
                finally
                {
                    _webSocket.OnMessage -= OnMessageHandler;
                }
            }
            finally
            {
                _webSocket.OnOpen -= OnOpenHandler;
                _webSocket.OnError -= OnErrorHandler;
                _webSocket.OnClose -= OnCloseHandler;
            
                callback.Invoke(new AsyncResult<bool>(
                    success,
                    success ? null : new ConnectionException(args)
                ));

                if (players != null)
                {
                    foreach (var player in players)
                    {
                        _eventQueue.Enqueue(
                            new OnJoinPlayerEvent(player)
                        );
                    }
                }
                
                _webSocket.OnMessage += this.OnMessageHandler;
                _webSocket.OnError += this.OnErrorHandler;
            }

            _monoBehaviour = monoBehaviour;
            if (monoBehaviour != null)
            {
                _dispatchCoroutine = monoBehaviour.StartCoroutine(Dispatch());
            }
        }

        public IEnumerator Send(UnityAction<AsyncResult<bool>> callback, BinaryMessage message)
        {
            if (!Connected)
            {
                callback.Invoke(new AsyncResult<bool>(
                    false,
                    new SendException(message)
                ));
                yield break;
            }
            
            var success = false;
            var done = false;
            _webSocket.SendAsync(
                _messenger.Pack(
                    message
                ), completed =>
            {
                if (completed)
                {
                    success = true;
                }
                done = true;
            });
            
            for (var i=0; i<30 && !done; i++)
            {
                yield return new WaitForSeconds(1);
            }
            
            callback.Invoke(new AsyncResult<bool>(
                success,
                success ? null : new SendException(message)
            ));
        }

        public IEnumerator UpdateProfile(UnityAction<AsyncResult<bool>> callback, ByteString profile)
        {
            if (!Connected)
            {
                callback.Invoke(new AsyncResult<bool>(
                    false,
                    new UpdateProfileException(profile)
                ));
                yield break;
            }

            var success = false;
            var done = false;
            _webSocket.SendAsync(
                _messenger.Pack(
                    new UpdateProfileRequest
                    {
                        MyProfile = profile
                    }
                ), completed =>
                {
                    if (completed)
                    {
                        success = true;
                    }
                    done = true;
                });
            
            for (var i=0; i<30 && !done; i++)
            {
                yield return new WaitForSeconds(1);
            }

            if (success)
            {
                Profile = profile;
            }

            callback.Invoke(new AsyncResult<bool>(
                success,
                success ? null : new UpdateProfileException(profile)
            ));
        }
        
        public IEnumerator Close()
        {
            if (!Connected) yield break;
            
            var done = false;
            void OnCloseHandler(object sender, EventArgs e)
            {
                done = true;
            }
            void OnErrorHandler(object sender, EventArgs e)
            {
                done = true;
            }

            try
            {
                _webSocket.OnClose += OnCloseHandler;
                _webSocket.OnError += OnErrorHandler;
                _webSocket.CloseAsync();

                for (var i=0; i<30 && !done; i++)
                {
                    yield return new WaitForSeconds(1);
                }
            }
            finally
            {
                _webSocket.OnClose -= OnCloseHandler;
                _webSocket.OnError -= OnErrorHandler;
                
                if (_dispatchCoroutine != null)
                {
                    if (_monoBehaviour != null)
                    {
                        _monoBehaviour.StopCoroutine(_dispatchCoroutine);
                    }
                    _dispatchCoroutine = null;
                }

                _monoBehaviour = null;
            }

            yield break;
        }

        public void Dispose()
        {
            ((IDisposable)_webSocket).Dispose();
        }
    }
}