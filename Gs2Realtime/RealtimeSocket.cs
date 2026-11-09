using System;
#if UNITY_WEBGL && !UNITY_EDITOR
using Gs2.HybridWebSocket;
#else
using Gs2.Util.WebSocketSharp;
#endif

namespace Gs2.Unity.Gs2Realtime
{
    /// <summary>
    /// The WebSocket a RealtimeSession talks to its room over. Implement it to supply a custom
    /// transport; <see cref="RealtimeSocket"/> is the platform's. The events may be raised on
    /// any thread: the session handles them under its own lock.
    /// </summary>
    public interface IRealtimeSocket : IDisposable
    {
        event Action Opened;
        event Action<byte[]> Received;
        event Action<ErrorEventArgs> Failed;
        event Action<CloseEventArgs> Closed;
        void Open();
        /// <summary>Sends one binary frame.</summary>
        /// <param name="sent">
        /// Called, possibly on another thread, with whether the frame was handed over. May be null.
        /// </param>
        void Send(byte[] data, Action<bool> sent);
        void Close();
    }

    /// <summary>The platform's WebSocket: the browser's on WebGL, websocket-sharp elsewhere.</summary>
    public sealed class RealtimeSocket : IRealtimeSocket
    {
        public event Action Opened;
        public event Action<byte[]> Received;
        public event Action<ErrorEventArgs> Failed;
        public event Action<CloseEventArgs> Closed;

#if UNITY_WEBGL && !UNITY_EDITOR
        readonly WebSocket socket;

        public RealtimeSocket(string url)
        {
            socket = WebSocketFactory.CreateInstance(url);
            socket.OnOpen += () => Opened?.Invoke();
            socket.OnMessageBinary += data => Received?.Invoke(data);
            socket.OnError += e => Failed?.Invoke(e);
            socket.OnClose += e => Closed?.Invoke(e);
        }
        public void Open() => socket.Connect();
        // The browser queues the frame synchronously, so sent is called before returning.
        public void Send(byte[] data, Action<bool> sent)
        {
            bool ok;
            try
            {
                socket.Send(data);
                ok = true;
            }
            catch (WebSocketException)
            {
                ok = false;
            }
            sent?.Invoke(ok);
        }
        public void Close() => socket.Close();
#else
        readonly WebSocket socket;

        public RealtimeSocket(string url)
        {
            socket = new WebSocket(url);
            socket.OnOpen += (sender, e) => Opened?.Invoke();
            socket.OnMessage += (sender, e) => Received?.Invoke(e.RawData);
            socket.OnError += (sender, e) => Failed?.Invoke(e);
            socket.OnClose += (sender, e) => Closed?.Invoke(e);
        }
        public void Open() => socket.ConnectAsync();
        public void Send(byte[] data, Action<bool> sent) => socket.SendAsync(data, ok => sent?.Invoke(ok));
        public void Close() => socket.CloseAsync();
#endif

        public void Dispose() => ((IDisposable)socket).Dispose();
    }
}
