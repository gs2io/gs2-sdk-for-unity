#if !UNITY_WEBGL || UNITY_EDITOR
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Util;

namespace Gs2.Unity.Gs2Realtime
{
    // UDP side channel of a synchronized room. The WebSocket session authenticates
    // (HelloResult.Secret); this channel then carries only the room's periodic state
    // updates, which may be lost or reordered. WebGL cannot use UDP and keeps WebSocket.
    //
    // Call Poll every frame from the main thread: it sends registration/keepalives and
    // hands received payloads to the callback. Use TrySend (for example as the sync
    // client's unreliable sender) only while Active.
    public sealed class RealtimeUdpChannel : IDisposable
    {
        // The room drops a registration after 15 s of silence; stay well inside it.
        public const double KeepAliveSeconds = 2, RetrySeconds = .5, ActiveSeconds = 10;
        readonly UdpClient udp;
        readonly Messenger messenger;
        readonly uint connectionId, secret;
        readonly Stopwatch clock = Stopwatch.StartNew();
        uint packetNumber, lastSequence;
        bool acknowledged, hasSequence;
        double lastSent = double.NegativeInfinity, lastHeard = double.NegativeInfinity;

        public RealtimeUdpChannel(RealtimeSession session)
            : this(session.IpAddress, session.Port, session.MyConnectionId, session.UdpSecret, session.EncryptionKey)
        {
        }
        public RealtimeUdpChannel(string host, int port, uint connectionId, uint secret, string encryptionKey)
        {
            if (connectionId == 0 || secret == 0)
                throw new ArgumentException("The room offers no UDP side channel (connect first; a zero secret means WebSocket only)");
            this.connectionId = connectionId;
            this.secret = secret;
            messenger = new Messenger(encryptionKey);
            udp = new UdpClient();
            udp.Connect(host, port);
        }
        double Now => clock.Elapsed.TotalSeconds;
        // The room answered recently: datagrams in both directions get through.
        public bool Active => acknowledged && Now - lastHeard < ActiveSeconds;
        public bool TrySend(byte[] data, int count)
        {
            if (!Active)
                return false;
            try
            {
                Send(data, count);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
        void Send(byte[] data, int count)
        {
            var message = new BinaryMessage { Data = count == 0 ? ByteString.Empty : ByteString.CopyFrom(data, 0, count) };
            var container = Container.Parser.ParseFrom(messenger.Pack(message));
            var bytes = new RUDPSendContainer { ConnectionId = connectionId, Secret = secret, PacketNumber = ++packetNumber, Container = container }.ToByteArray();
            udp.Send(bytes, bytes.Length);
            lastSent = Now;
        }
        public void Poll(Action<byte[], uint> received)
        {
            double now = Now;
            try
            {
                if (now - lastSent >= (acknowledged ? KeepAliveSeconds : RetrySeconds))
                    Send(Array.Empty<byte>(), 0);
                while (udp.Available > 0)
                {
                    var from = new IPEndPoint(IPAddress.Any, 0);
                    var bytes = udp.Receive(ref from);
                    RUDPReceiveContainer datagram;
                    try
                    {
                        datagram = RUDPReceiveContainer.Parser.ParseFrom(bytes);
                    }
                    catch (InvalidProtocolBufferException)
                    {
                        continue;
                    }
                    acknowledged = true;
                    lastHeard = Now;
                    if (datagram.Container == null)
                        continue;
                    var (type, payload, sequence, lifetime) = messenger.Unpack(datagram.Container.ToByteArray());
                    // Older datagrams arriving late would move state backwards.
                    if (type != Container.Types.MessageType.BinaryMessage || (hasSequence && unchecked((int)(sequence - lastSequence)) <= 0))
                        continue;
                    lastSequence = sequence;
                    hasSequence = true;
                    received(BinaryMessage.Parser.ParseFrom(payload.Value).Data.ToByteArray(), lifetime);
                }
            }
            catch (SocketException)
            {
                // ICMP unreachable or similar: the channel stays inactive and WebSocket carries everything.
            }
        }
        public void Dispose()
        {
            udp.Dispose();
        }
    }
}
#endif
