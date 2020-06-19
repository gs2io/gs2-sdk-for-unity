// #define DISABLE_COROUTINE

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Google.Protobuf;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Exception;
using Gs2.Unity.Gs2Realtime.Util;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using Gs2.Util.WebSocketSharp;

namespace Gs2.Unity.Gs2Realtime
{
    public delegate void OnMessageHandler(BinaryMessage message);
    public delegate void OnMessageWithMetadataHandler(BinaryMessage message, MessageMetadata metadata);
    public delegate void OnJoinPlayerHandler(Player player);
    public delegate void OnJoinPlayerWithMetadataHandler(Player player, MessageMetadata metadata);
    public delegate void OnLeavePlayerHandler(Player player);
    public delegate void OnLeavePlayerWithMetadataHandler(Player player, MessageMetadata metadata);
    public delegate void OnUpdateProfileHandler(Player player);
    public delegate void OnUpdateProfileWithMetadataHandler(Player player, MessageMetadata metadata);
    public delegate void OnErrorHandler(Error e);
    public delegate void OnErrorWithMetadataHandler(Error e, MessageMetadata metadata);
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
        public event OnMessageWithMetadataHandler OnMessageWithMetadata;
        public event OnJoinPlayerHandler OnJoinPlayer;
        public event OnJoinPlayerWithMetadataHandler OnJoinPlayerWithMetadata;
        public event OnLeavePlayerHandler OnLeavePlayer;
        public event OnLeavePlayerWithMetadataHandler OnLeavePlayerWithMetadata;
        public event OnUpdateProfileHandler OnUpdateProfile;
        public event OnUpdateProfileWithMetadataHandler OnUpdateProfileWithMetadata;
        public event OnErrorHandler OnError;
        public event OnErrorWithMetadataHandler OnErrorWithMetadata;
        public event OnGeneralErrorHandler OnGeneralError;
        public event OnCloseHandler OnClose;

        protected virtual void EventHandler(RealtimeEvent @event)
        {
            switch (@event.EventType)
            {
                case RealtimeEventType.OnMessage:
                {
                    var typedEvent = @event as OnMessageEvent;
                    if (OnMessage != null)
                    {
                        OnMessage.Invoke(typedEvent.Message);
                    }

                    if (OnMessageWithMetadata != null)
                    {
                        OnMessageWithMetadata.Invoke(typedEvent.Message, typedEvent.Metadata);
                    }

                    break;
                }
                case RealtimeEventType.OnJoinPlayer:
                {
                    var typedEvent = @event as OnJoinPlayerEvent;
                    if (OnJoinPlayer != null)
                    {
                        OnJoinPlayer.Invoke(typedEvent.Player);
                    }

                    if (OnJoinPlayerWithMetadata != null)
                    {
                        OnJoinPlayerWithMetadata.Invoke(typedEvent.Player, typedEvent.Metadata);
                    }

                    break;
                }
                case RealtimeEventType.OnLeavePlayer:
                {
                    var typedEvent = @event as OnLeavePlayerEvent;
                    if (OnLeavePlayer != null)
                    {
                        OnLeavePlayer.Invoke(typedEvent.Player);
                    }

                    if (OnLeavePlayerWithMetadata != null)
                    {
                        OnLeavePlayerWithMetadata.Invoke(typedEvent.Player, typedEvent.Metadata);
                    }

                    break;
                }
                case RealtimeEventType.OnUpdateProfile:
                {
                    var typedEvent = @event as OnUpdateProfileEvent;
                    if (OnUpdateProfile != null)
                    {
                        OnUpdateProfile.Invoke(typedEvent.Player);
                    }

                    if (OnUpdateProfileWithMetadata != null)
                    {
                        OnUpdateProfileWithMetadata.Invoke(typedEvent.Player, typedEvent.Metadata);
                    }

                    break;
                }
                case RealtimeEventType.OnError:
                {
                    var typedEvent = @event as OnErrorEvent;
                    if (OnError != null)
                    {
                        OnError.Invoke(typedEvent.Error);
                    }

                    if (OnErrorWithMetadata != null)
                    {
                        OnErrorWithMetadata.Invoke(typedEvent.Error, typedEvent.Metadata);
                    }

                    break;
                }
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
#if DISABLE_COROUTINE
                    yield return null;
#else
                    yield return new WaitForSeconds(1);
#endif
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
            
            var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = _messenger.Unpack(data.RawData);
            var message = _messenger.Parse(messageType, payload);
            if (message is JoinNotification joinNotification)
            {
                _eventQueue.Enqueue(
                    new OnJoinPlayerEvent(
                        joinNotification.JoinPlayer,
                        sequenceNumber,
                        lifeTimeMilliSeconds
                    )
                );
            }
            if (message is LeaveNotification leaveNotification)
            {
                _eventQueue.Enqueue(
                    new OnLeavePlayerEvent(
                        leaveNotification.LeavePlayer,
                        sequenceNumber,
                        lifeTimeMilliSeconds
                    )
                );
            }
            if (message is UpdateProfileNotification updateProfileNotification)
            {
                _eventQueue.Enqueue(
                    new OnUpdateProfileEvent(
                        updateProfileNotification.UpdatePlayer,
                        sequenceNumber,
                        lifeTimeMilliSeconds
                    )
                );
            }
            if (message is BinaryMessage binaryMessage)
            {
                _eventQueue.Enqueue(
                    new OnMessageEvent(
                        binaryMessage,
                        sequenceNumber,
                        lifeTimeMilliSeconds
                    )
                );
            }
            if (message is Error error)
            {
                _eventQueue.Enqueue(
                    new OnErrorEvent(
                        error,
                        sequenceNumber,
                        lifeTimeMilliSeconds
                    )
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
            var messageArgsList = new List<MessageEventArgs>();
            
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
#if DISABLE_COROUTINE
                    yield return null;
#else
                    yield return new WaitForSeconds(1);
#endif
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
                        var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = _messenger.Unpack(data.RawData);
                        var message = _messenger.Parse(messageType, payload);
                        if (message is Error error)
                        {
                            _eventQueue.Enqueue(
                                new OnErrorEvent(
                                    error,
                                    sequenceNumber,
                                    lifeTimeMilliSeconds
                                )
                            );
                            return;
                        }
                        else if (message is HelloResult)
                        {
                            helloResult = message as HelloResult;
                            success = true;
                        }
                        else
                        {
                            messageArgsList.Add(data);
                        }
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
#if DISABLE_COROUTINE
                        yield return null;
#else
                        yield return new WaitForSeconds(1);
#endif
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
                            new OnJoinPlayerEvent(
                                player,
                                0,
                                0
                            )
                        );
                    }
                }

                foreach (var e in messageArgsList)
                {
                    this.OnMessageHandler(null, e);
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
#if DISABLE_COROUTINE
                yield return null;
#else
                yield return new WaitForSeconds(1);
#endif
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
#if DISABLE_COROUTINE
                yield return null;
#else
                yield return new WaitForSeconds(1);
#endif
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
#if DISABLE_COROUTINE
                    yield return null;
#else
                    yield return new WaitForSeconds(1);
#endif
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