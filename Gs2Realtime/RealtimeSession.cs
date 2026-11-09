using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Google.Protobuf;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Exception;
using Gs2.Unity.Gs2Realtime.Util;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_WEBGL && !UNITY_EDITOR
using Gs2.HybridWebSocket;
#else
using Gs2.Util.WebSocketSharp;
#endif
using UnityEngine.Scripting;

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

    /// <summary>The lifecycle state of a <see cref="RealtimeSession"/>.</summary>
    public enum RealtimeSessionState : byte
    {
        /// <summary>Created; Connect has not been called yet.</summary>
        Idle,
        /// <summary>The WebSocket is opening.</summary>
        Opening,
        /// <summary>Open; Hello sent, waiting for the room's HelloResult.</summary>
        Greeting,
        /// <summary>The room accepted Hello.</summary>
        Connected,
        /// <summary>Close was called.</summary>
        Closing,
        /// <summary>Closed after being connected.</summary>
        Closed,
        /// <summary>The connection or Hello failed (ConnectionError says why).</summary>
        Failed,
    }

    /// <summary>
    /// A connection to a GS2-Realtime room. Connect, Send, UpdateProfile and Close (coroutines,
    /// or their UniTask forms) each wait at most 30 s of real time for their outcome. Room
    /// messages are queued and raised as events by <see cref="Dispatch"/> on the main thread.
    /// </summary>
    // The connection is one state machine (RealtimeSessionState) driven by the socket's events
    // under _gate: the socket may raise them on another thread. Messages that arrive while
    // Hello is being answered are delivered right after it, in order.
    [Preserve]
    public class RealtimeSession : IDisposable
    {
        /// <summary>
        /// When true, each wait step also sleeps 10 ms on the calling thread, for driving the
        /// coroutines without Unity frames.
        /// </summary>
        public static bool NotUseWaitForSeconds = false;
        const double TimeoutSeconds = 30;

        readonly IRealtimeSocket _socket;
        protected readonly Messenger _messenger;
        readonly string _accessToken;
        readonly object _gate = new object();
        readonly Queue<RealtimeEvent> _eventQueue = new Queue<RealtimeEvent>();
        // Room messages received while Greeting, delivered once Connected.
        readonly List<byte[]> _early = new List<byte[]>();
        Error _helloError;           // the room's answer refusing Hello
        EventArgs _socketError;      // the socket's latest error before Connected
        MonoBehaviour _monoBehaviour;
        Coroutine _dispatchCoroutine;

        /// <summary>The current lifecycle state.</summary>
        public RealtimeSessionState State { get; private set; }
        /// <summary>True while the room has accepted this session and it is not closing.</summary>
        public bool Connected => State == RealtimeSessionState.Connected;
        /// <summary>This player's connection id in the room, set once connected.</summary>
        public uint MyConnectionId { get; private set; }
        /// <summary>
        /// Registration secret for the UDP side channel, from HelloResult (0 when the room has no
        /// UDP). See <see cref="RealtimeUdpChannel"/>.
        /// </summary>
        public uint UdpSecret { get; private set; }
        public string IpAddress { get; }
        public int Port { get; }
        public string EncryptionKey { get; }
        /// <summary>This player's profile; replaced when UpdateProfile succeeds.</summary>
        public ByteString Profile { get; private set; }
        /// <summary>
        /// Why Connect failed (State is Failed); its ServerError holds the room's answer, if any.
        /// </summary>
        public ConnectionException ConnectionError { get; private set; }

        // The events below are raised by Dispatch on the thread that runs it (the main thread).
        // The ...WithMetadata variants also carry the message's sequence number and lifetime.

        /// <summary>A binary message from another player arrived.</summary>
        public event OnMessageHandler OnMessage;
        public event OnMessageWithMetadataHandler OnMessageWithMetadata;
        /// <summary>
        /// A player joined. Also raised for each player the room lists when this session connects.
        /// </summary>
        public event OnJoinPlayerHandler OnJoinPlayer;
        public event OnJoinPlayerWithMetadataHandler OnJoinPlayerWithMetadata;
        /// <summary>A player left the room.</summary>
        public event OnLeavePlayerHandler OnLeavePlayer;
        public event OnLeavePlayerWithMetadataHandler OnLeavePlayerWithMetadata;
        /// <summary>A player changed their profile.</summary>
        public event OnUpdateProfileHandler OnUpdateProfile;
        public event OnUpdateProfileWithMetadataHandler OnUpdateProfileWithMetadata;
        /// <summary>The room reported an error.</summary>
        public event OnErrorHandler OnError;
        public event OnErrorWithMetadataHandler OnErrorWithMetadata;
        /// <summary>The WebSocket reported an error after the session connected.</summary>
        public event OnGeneralErrorHandler OnGeneralError;
        /// <summary>The connection closed after the session connected.</summary>
        public event OnCloseHandler OnClose;

        /// <summary>A session to the room at ipAddress:port over the platform's WebSocket.</summary>
        public RealtimeSession(
            string accessToken,
            string ipAddress,
            int port,
            string encryptionKey,
            ByteString profile = null
        ) : this(new RealtimeSocket(string.Format("ws://{0}:{1}/", ipAddress, port)), accessToken, ipAddress, port, encryptionKey, profile)
        {
        }

        /// <summary>
        /// A session over a custom transport. ipAddress and port are still what
        /// <see cref="RealtimeUdpChannel"/> uses.
        /// </summary>
        public RealtimeSession(
            IRealtimeSocket socket,
            string accessToken,
            string ipAddress,
            int port,
            string encryptionKey,
            ByteString profile = null
        )
        {
            _socket = socket ?? throw new ArgumentNullException(nameof(socket));
            _messenger = new Messenger(encryptionKey);
            _accessToken = accessToken;
            Profile = profile ?? ByteString.Empty;
            IpAddress = ipAddress;
            Port = port;
            EncryptionKey = encryptionKey;
            _socket.Opened += () => Step(SocketEvent.Opened);
            _socket.Received += data => Step(SocketEvent.Received, data);
            _socket.Failed += e => Step(SocketEvent.Failed, error: e);
            _socket.Closed += e => Step(SocketEvent.Closed, close: e);
        }

        enum SocketEvent : byte { Opened, Received, Failed, Closed }

        // Every transition driven by the socket happens here, under _gate.
        void Step(SocketEvent what, byte[] data = null, ErrorEventArgs error = null, CloseEventArgs close = null)
        {
            lock (_gate)
            {
                switch (what)
                {
                    case SocketEvent.Opened when State == RealtimeSessionState.Opening:
                        State = RealtimeSessionState.Greeting;
                        _socket.Send(_messenger.Pack(new HelloRequest { AccessToken = _accessToken, MyProfile = Profile }), null);
                        break;
                    case SocketEvent.Received when State == RealtimeSessionState.Greeting:
                        Greet(data);
                        break;
                    case SocketEvent.Received when State == RealtimeSessionState.Connected || State == RealtimeSessionState.Closing:
                        Receive(data);
                        break;
                    case SocketEvent.Failed when State == RealtimeSessionState.Connected || State == RealtimeSessionState.Closing:
                        _eventQueue.Enqueue(new OnGeneralErrorEvent(error));
                        break;
                    case SocketEvent.Failed:
                        _socketError = error; // the reason, if the attempt then fails
                        break;
                    case SocketEvent.Closed when State == RealtimeSessionState.Opening || State == RealtimeSessionState.Greeting:
                        Fail();
                        break;
                    case SocketEvent.Closed when State == RealtimeSessionState.Connected || State == RealtimeSessionState.Closing:
                        State = RealtimeSessionState.Closed;
                        _eventQueue.Enqueue(new OnCloseEvent(close));
                        break;
                }
            }
        }
        // While Greeting: the room's answer to Hello, or a message that follows it early.
        void Greet(byte[] data)
        {
            var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = _messenger.Unpack(data);
            switch (_messenger.Parse(messageType, payload))
            {
                case Error error:
                    _eventQueue.Enqueue(new OnErrorEvent(error, sequenceNumber, lifeTimeMilliSeconds));
                    _helloError = error;
                    Fail();
                    _socket.Close();
                    break;
                case HelloResult hello:
                    MyConnectionId = hello.MyProfile.ConnectionId;
                    UdpSecret = hello.Secret;
                    State = RealtimeSessionState.Connected;
                    foreach (var player in hello.Players)
                        _eventQueue.Enqueue(new OnJoinPlayerEvent(player, 0, 0));
                    foreach (var early in _early)
                        Receive(early);
                    _early.Clear();
                    break;
                default:
                    _early.Add(data);
                    break;
            }
        }
        void Fail()
        {
            if (State == RealtimeSessionState.Failed)
                return;
            State = RealtimeSessionState.Failed;
            ConnectionError = new ConnectionException(_socketError ?? EventArgs.Empty, _helloError);
            _early.Clear();
        }
        void Receive(byte[] data)
        {
            var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = _messenger.Unpack(data);
            Handle(_messenger.Parse(messageType, payload), sequenceNumber, lifeTimeMilliSeconds);
        }
        /// <summary>
        /// Turns a room message into its event. Called under the session's lock, possibly on the
        /// socket's thread: only <see cref="Enqueue"/> events here, never raise them.
        /// </summary>
        protected virtual void Handle(IMessage message, uint sequenceNumber, uint lifeTimeMilliSeconds)
        {
            switch (message)
            {
                case JoinNotification join:
                    Enqueue(new OnJoinPlayerEvent(join.JoinPlayer, sequenceNumber, lifeTimeMilliSeconds));
                    break;
                case LeaveNotification leave:
                    Enqueue(new OnLeavePlayerEvent(leave.LeavePlayer, sequenceNumber, lifeTimeMilliSeconds));
                    break;
                case UpdateProfileNotification update:
                    Enqueue(new OnUpdateProfileEvent(update.UpdatePlayer, sequenceNumber, lifeTimeMilliSeconds));
                    break;
                case BinaryMessage binary:
                    Enqueue(new OnMessageEvent(binary, sequenceNumber, lifeTimeMilliSeconds));
                    break;
                case Error error:
                    Enqueue(new OnErrorEvent(error, sequenceNumber, lifeTimeMilliSeconds));
                    break;
            }
        }
        /// <summary>Queues an event for Dispatch. Call only from <see cref="Handle"/>.</summary>
        protected void Enqueue(RealtimeEvent @event) => _eventQueue.Enqueue(@event);

        // Called by each RealtimeEvent as Dispatch delivers it.
        internal void RaiseMessage(BinaryMessage message, MessageMetadata metadata)
        {
            OnMessage?.Invoke(message);
            OnMessageWithMetadata?.Invoke(message, metadata);
        }
        internal void RaiseJoinPlayer(Player player, MessageMetadata metadata)
        {
            OnJoinPlayer?.Invoke(player);
            OnJoinPlayerWithMetadata?.Invoke(player, metadata);
        }
        internal void RaiseLeavePlayer(Player player, MessageMetadata metadata)
        {
            OnLeavePlayer?.Invoke(player);
            OnLeavePlayerWithMetadata?.Invoke(player, metadata);
        }
        internal void RaiseUpdateProfile(Player player, MessageMetadata metadata)
        {
            OnUpdateProfile?.Invoke(player);
            OnUpdateProfileWithMetadata?.Invoke(player, metadata);
        }
        internal void RaiseError(Error error, MessageMetadata metadata)
        {
            OnError?.Invoke(error);
            OnErrorWithMetadata?.Invoke(error, metadata);
        }
        internal void RaiseGeneralError(ErrorEventArgs error) => OnGeneralError?.Invoke(error);
        internal void RaiseClose(CloseEventArgs error) => OnClose?.Invoke(error);

        /// <summary>
        /// Raises the queued events, as a coroutine that never ends. Connect starts it when given
        /// a MonoBehaviour; otherwise run it yourself on the main thread.
        /// </summary>
        public IEnumerator Dispatch()
        {
            while (true)
            {
                RealtimeEvent next = null;
                lock (_gate)
                {
                    if (_eventQueue.Count > 0)
                        next = _eventQueue.Dequeue();
                }
                if (next == null)
                    yield return null;
                else
                    next.DeliverTo(this);
            }
        }

        bool BeginConnect()
        {
            lock (_gate)
            {
                if (State != RealtimeSessionState.Idle)
                    return false;
                State = RealtimeSessionState.Opening;
            }
            _socket.Open();
            return true;
        }
        // An attempt still unsettled here has timed out: fail it and close the socket.
        bool EndConnect(MonoBehaviour monoBehaviour)
        {
            lock (_gate)
            {
                if (State == RealtimeSessionState.Opening || State == RealtimeSessionState.Greeting)
                {
                    Fail();
                    _socket.Close();
                }
                if (State != RealtimeSessionState.Connected)
                {
                    ConnectionError = ConnectionError ?? new ConnectionException(EventArgs.Empty);
                    return false;
                }
            }
            if (monoBehaviour != null)
            {
                _monoBehaviour = monoBehaviour;
                _dispatchCoroutine = monoBehaviour.StartCoroutine(Dispatch());
            }
            return true;
        }
        bool Settled => State != RealtimeSessionState.Opening && State != RealtimeSessionState.Greeting;
        bool Ended => State == RealtimeSessionState.Closed || State == RealtimeSessionState.Failed;

        // A send's outcome, set by the socket possibly on another thread (hence volatile).
        sealed class Pending
        {
            volatile bool done, ok;
            public bool Done => done;
            public bool Ok => ok;
            public void Set(bool result)
            {
                ok = result;
                done = true;
            }
        }
        Pending Transmit(IMessage message)
        {
            var pending = new Pending();
            if (!Connected)
                pending.Set(false);
            else
                _socket.Send(_messenger.Pack(message), pending.Set);
            return pending;
        }
        bool BeginClose()
        {
            lock (_gate)
            {
                if (State != RealtimeSessionState.Connected)
                    return false;
                State = RealtimeSessionState.Closing;
            }
            _socket.Close();
            return true;
        }
        void EndClose()
        {
            lock (_gate)
            {
                if (State == RealtimeSessionState.Closing)
                    State = RealtimeSessionState.Closed; // no close frame in time
            }
            if (_dispatchCoroutine != null && _monoBehaviour != null)
                _monoBehaviour.StopCoroutine(_dispatchCoroutine);
            _dispatchCoroutine = null;
            _monoBehaviour = null;
        }

        // Real time, not game time: a paused game (Time.timeScale = 0) must not stop the
        // connection.
        static IEnumerator WaitUntil(Func<bool> done)
        {
            for (var clock = Stopwatch.StartNew(); !done() && clock.Elapsed.TotalSeconds < TimeoutSeconds;)
            {
                if (NotUseWaitForSeconds)
                    Thread.Sleep(10);
                yield return null;
            }
        }

        /// <summary>
        /// Opens the connection and sends Hello, waiting at most 30 s. A session connects only
        /// once; calling this again reports whether it is connected. On failure the result's
        /// error is <see cref="ConnectionError"/>.
        /// </summary>
        /// <param name="monoBehaviour">
        /// When given, runs <see cref="Dispatch"/> once connected; when null, the caller must.
        /// </param>
        public IEnumerator Connect(MonoBehaviour monoBehaviour, UnityAction<AsyncResult<bool>> callback)
        {
            if (BeginConnect())
                yield return WaitUntil(() => Settled);
            bool connected = EndConnect(monoBehaviour);
            callback.Invoke(new AsyncResult<bool>(connected, connected ? null : ConnectionError));
        }

        /// <summary>
        /// Sends a message to the room. The result is false, with a <see cref="SendException"/>,
        /// when not connected or the socket did not take the frame within 30 s.
        /// </summary>
        public IEnumerator Send(UnityAction<AsyncResult<bool>> callback, BinaryMessage message)
        {
            var pending = Transmit(message);
            yield return WaitUntil(() => pending.Done);
            callback.Invoke(new AsyncResult<bool>(pending.Ok, pending.Ok ? null : new SendException(message)));
        }

        /// <summary>
        /// Sends a new profile and, once sent, stores it in <see cref="Profile"/>. Fails like Send,
        /// with an <see cref="UpdateProfileException"/>.
        /// </summary>
        public IEnumerator UpdateProfile(UnityAction<AsyncResult<bool>> callback, ByteString profile)
        {
            var pending = Transmit(new UpdateProfileRequest { MyProfile = profile });
            yield return WaitUntil(() => pending.Done);
            if (pending.Ok)
                Profile = profile;
            callback.Invoke(new AsyncResult<bool>(pending.Ok, pending.Ok ? null : new UpdateProfileException(profile)));
        }

        /// <summary>
        /// Closes a connected session, waiting at most 30 s for the socket to close, and stops
        /// the Dispatch coroutine that Connect started.
        /// </summary>
        public IEnumerator Close()
        {
            if (BeginClose())
                yield return WaitUntil(() => Ended);
            EndClose();
        }

#if GS2_ENABLE_UNITASK
        static async UniTask WaitUntilAsync(Func<bool> done)
        {
            for (var clock = Stopwatch.StartNew(); !done() && clock.Elapsed.TotalSeconds < TimeoutSeconds;)
                await UniTask.Yield();
        }

        /// <summary>As <see cref="Connect"/>; throws <see cref="ConnectionError"/> on failure.</summary>
        public async UniTask ConnectAsync(MonoBehaviour monoBehaviour)
        {
            if (BeginConnect())
                await WaitUntilAsync(() => Settled);
            if (!EndConnect(monoBehaviour))
                throw ConnectionError;
        }

        /// <summary>As <see cref="Send"/>; throws <see cref="SendException"/> on failure.</summary>
        public async UniTask SendAsync(BinaryMessage message)
        {
            var pending = Transmit(message);
            await WaitUntilAsync(() => pending.Done);
            if (!pending.Ok)
                throw new SendException(message);
        }

        /// <summary>
        /// As <see cref="UpdateProfile"/>; throws <see cref="UpdateProfileException"/> on failure.
        /// </summary>
        public async UniTask UpdateProfileAsync(ByteString profile)
        {
            var pending = Transmit(new UpdateProfileRequest { MyProfile = profile });
            await WaitUntilAsync(() => pending.Done);
            if (!pending.Ok)
                throw new UpdateProfileException(profile);
            Profile = profile;
        }

        /// <summary>As <see cref="Close"/>.</summary>
        public async UniTask CloseAsync()
        {
            if (BeginClose())
                await WaitUntilAsync(() => Ended);
            EndClose();
        }
#endif

        /// <summary>Disposes the underlying socket.</summary>
        public void Dispose()
        {
            _socket.Dispose();
        }
    }
}
