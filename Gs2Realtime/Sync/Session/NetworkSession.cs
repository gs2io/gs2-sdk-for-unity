#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime;
using UnityEngine;

namespace Gs2.Realtime.Sync
{
    /// <summary>The connection state of a <see cref="NetworkSession"/>.</summary>
    public enum NetworkSessionState : byte
    {
        Disconnected,
        /// <summary>Opening the room's WebSocket.</summary>
        Connecting,
        /// <summary>Connected; the room's snapshot is being applied.</summary>
        Synchronizing,
        /// <summary>The snapshot is applied: spawn, send and read the room.</summary>
        Ready,
    }

    /// <summary>
    /// Connects the NetworkWorld on the same GameObject to a GS2-Realtime room: opens the
    /// RealtimeSession, attaches the world, sends its packets in order, delivers received
    /// messages with the room server's clock, uses UDP for state updates where available, and
    /// detaches the world when the session ends.
    /// </summary>
    /// <example>
    /// <code>
    /// // Subscribe once (Awake/OnEnable), not before each Connect: the events persist
    /// // across reconnects, so subscribing per connection repeats the handlers.
    /// session.Ready += () =&gt; world.Instantiate(prefabId, position, rotation);
    /// session.Disconnected += reason =&gt; ShowTitle(reason);
    /// // Each (re)connection:
    /// session.Connect(accessToken, ipAddress, port, encryptionKey);
    /// </code>
    /// </example>
    [RequireComponent(typeof(NetworkWorld))]
    [DisallowMultipleComponent]
    public sealed class NetworkSession : MonoBehaviour
    {
        // Packets waiting for the WebSocket. When it cannot keep up, the world's client
        // keeps the rest in its outbox (in order) and offers them again.
        const int MaxQueuedPackets = 32;
        [SerializeField, Tooltip("Send periodic state updates over UDP when the room answers (not in WebGL).")]
        bool useUdp = true;

        // One connection and everything that runs for it. It ends once (End); callbacks of an
        // ended connection do nothing, so a reconnect never sees the previous connection's
        // traffic.
        sealed class Connection
        {
            public readonly RealtimeSession Session;
            public readonly Queue<BinaryMessage> Outgoing = new Queue<BinaryMessage>();
            public Coroutine Dispatch, Sender;
            public string Error; // the latest error the session reported, for the disconnect reason
            public bool Ended;
            public bool CanQueue => !Ended && Session.Connected && Outgoing.Count < MaxQueuedPackets;
#if !UNITY_WEBGL || UNITY_EDITOR
            public RealtimeUdpChannel Udp;
#endif
            public Connection(RealtimeSession session)
            {
                Session = session;
            }
        }
        NetworkWorld world;
        Connection connection;

        public NetworkSessionState State { get; private set; }
        /// <summary>The NetworkWorld on this GameObject.</summary>
        public NetworkWorld World => world != null ? world : world = GetComponent<NetworkWorld>();
        /// <summary>
        /// The open RealtimeSession (null while disconnected), e.g. for player join/leave events.
        /// </summary>
        public RealtimeSession Session => connection?.Session;
        /// <summary>True while state updates travel over UDP (always false on WebGL).</summary>
        public bool UdpActive
        {
            get
            {
#if !UNITY_WEBGL || UNITY_EDITOR
                return connection?.Udp != null && connection.Udp.Active;
#else
                return false;
#endif
            }
        }
        /// <summary>
        /// Why the latest Connect failed, when it did: a ConnectionException whose ServerError
        /// (and StatusCode) carries the room's answer, e.g. an invalid access token, or none when
        /// the room could not be reached.
        /// </summary>
        public Exception ConnectionError { get; private set; }
        /// <summary>Total bytes of the world's packets handed to the transport.</summary>
        public long SentBytes { get; private set; }
        /// <summary>Bytes of the latest packet handed to the transport.</summary>
        public int LastPacketBytes { get; private set; }

        /// <summary>
        /// The world attached: subscribe to World.Client's events here; no room message has
        /// been applied yet.
        /// </summary>
        public event Action<SyncClient> Attached;
        /// <summary>
        /// The room's snapshot is applied (also after a new snapshot): the game may spawn, send
        /// and read the room. An exception from a handler is logged and does not end the session.
        /// </summary>
        public event Action Ready;
        /// <summary>
        /// The session ended (closed by either side, failed to connect, or failed to send or
        /// decode), with a readable reason. The world is detached (its objects removed) first.
        /// </summary>
        public event Action<string> Disconnected;

        /// <summary>
        /// Connects to the room that GS2-Realtime room creation reported. Runs as a coroutine of
        /// this component; follow State or the events.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Not Disconnected, or this component is not active and enabled.
        /// </exception>
        public Coroutine Connect(string accessToken, string ipAddress, int port, string encryptionKey, ByteString profile = null)
        {
            if (State != NetworkSessionState.Disconnected)
                throw new InvalidOperationException("Disconnect before connecting again");
            if (!isActiveAndEnabled)
                throw new InvalidOperationException("Connect needs an active and enabled NetworkSession");
            // Invalid settings throw here, before any state changes, so Connect may be retried.
            var opening = new Connection(new RealtimeSession(accessToken, ipAddress, port, encryptionKey, profile ?? ByteString.Empty));
            connection = opening;
            State = NetworkSessionState.Connecting;
            ConnectionError = null;
            return StartCoroutine(Open(opening));
        }

        /// <summary>Closes the session; Disconnected follows. Returns null when not connected.</summary>
        public Coroutine Disconnect()
        {
            return connection == null ? null : StartCoroutine(Close(connection, "Disconnected"));
        }

        IEnumerator Open(Connection c)
        {
            var session = c.Session;
            session.OnError += e => c.Error = "Room error " + e.StatusCode;
            session.OnGeneralError += e => c.Error = e.Message;
            session.OnClose += e => End(c, c.Error ?? "Closed by the room");
            AsyncResult<bool> result = null;
            // No MonoBehaviour: the session must not dispatch on its own before the world
            // subscribes, or the messages buffered during Hello (the snapshot) are lost.
            yield return session.Connect(null, value => result = value);
            if (c.Ended)
                yield break; // disconnected meanwhile
            bool connected = result != null && result.Error == null && session.Connected;
            if (!connected)
            {
                ConnectionError = result?.Error;
                End(c, "Connection failed: " + (result?.Error?.Message ?? c.Error ?? "no answer"));
                yield break;
            }
            // An invalid world (its prefab catalog, for example) ends the session.
            try
            {
                World.Attach(session.MyConnectionId, (bytes, count) => Enqueue(c, bytes, count));
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
                End(c, "Attach failed: " + e.Message);
                yield break;
            }
            // The client lives as long as this connection, so nothing has to unsubscribe.
            var client = World.Client;
            client.SnapshotStarted += () =>
            {
                if (!c.Ended)
                    State = NetworkSessionState.Synchronizing;
            };
            client.SnapshotCompleted += () => OnSnapshotCompleted(c);
            try
            {
                Attached?.Invoke(client);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
            // An Attached handler may have ended the session (disabled this component, called
            // Disconnect).
            if (c.Ended)
                yield break;
            session.OnMessageWithMetadata += (message, metadata) => Deliver(c, message.Data.ToByteArray(), metadata.LifeTimeMilliSeconds);
#if !UNITY_WEBGL || UNITY_EDITOR
            if (useUdp && session.UdpSecret != 0)
            {
                try
                {
                    c.Udp = new RealtimeUdpChannel(session);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("UDP unavailable: " + e.Message, this);
                }
            }
#endif
            State = NetworkSessionState.Synchronizing;
            c.Dispatch = StartCoroutine(session.Dispatch());
            c.Sender = StartCoroutine(Send(c));
        }

        void OnSnapshotCompleted(Connection c)
        {
            if (c.Ended)
                return;
            State = NetworkSessionState.Ready;
            // The game's handler is not part of decoding the packet: its failure must not end
            // the session.
            try
            {
                Ready?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }

        bool Enqueue(Connection c, byte[] bytes, int count)
        {
            if (!c.CanQueue)
                return false;
            c.Outgoing.Enqueue(new BinaryMessage { Data = ByteString.CopyFrom(bytes, 0, count) });
            SentBytes += count;
            LastPacketBytes = count;
            return true;
        }

        IEnumerator Send(Connection c)
        {
            while (!c.Ended && c.Session.Connected)
            {
                if (c.Outgoing.Count == 0)
                {
                    yield return null;
                    continue;
                }
                bool sent = false;
                yield return c.Session.Send(result => sent = result.Error == null && result.Result, c.Outgoing.Dequeue());
                if (!sent)
                {
                    StartCoroutine(Close(c, "Send failed"));
                    yield break;
                }
            }
        }

        void Deliver(Connection c, byte[] bytes, uint serverMilliseconds)
        {
            if (c.Ended || World.Client == null)
                return;
            try
            {
                World.Receive(bytes, serverMilliseconds);
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
                StartCoroutine(Close(c, "Decode failed: " + e.Message));
            }
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        // Runs before NetworkWorld's Update (its execution order is later), so datagrams
        // received this frame are applied before the world services its objects.
        void Update()
        {
            var c = connection;
            if (c?.Udp == null || World.Client == null)
                return;
            c.Udp.Poll((bytes, milliseconds) => Deliver(c, bytes, milliseconds));
            if (!c.Ended && World.Client != null)
                World.Unreliable = c.Udp.Active ? c.Udp.TrySend : (TrySend)null;
        }
#endif

        IEnumerator Close(Connection c, string reason)
        {
            if (c.Ended)
                yield break;
            yield return c.Session.Close();
            End(c, reason);
        }

        void End(Connection c, string reason)
        {
            if (c.Ended)
                return;
            c.Ended = true;
            if (connection == c)
                connection = null;
            if (c.Dispatch != null)
                StopCoroutine(c.Dispatch);
            if (c.Sender != null)
                StopCoroutine(c.Sender);
            c.Outgoing.Clear();
            var world = World;
            if (world != null) // destroyed first when the scene unloads
                world.Detach();
            Release(c);
            State = NetworkSessionState.Disconnected;
            Disconnected?.Invoke(reason);
        }
        static void Release(Connection c)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            c.Udp?.Dispose();
            c.Udp = null;
#endif
            c.Session.Dispose();
        }

        // Disabling the component (or its GameObject) stops its coroutines, which carry
        // the traffic: the session ends rather than look Ready while nothing moves.
        void OnDisable()
        {
            if (connection != null)
                End(connection, "Disabled");
        }

        void OnDestroy()
        {
            var c = connection;
            connection = null;
            if (c == null)
                return;
            c.Ended = true;
            Release(c);
        }
    }
}
#endif
