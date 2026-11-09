using Gs2.Gs2Realtime.Message;
#if UNITY_WEBGL && !UNITY_EDITOR
using Gs2.HybridWebSocket;
#else
using Gs2.Util.WebSocketSharp;
#endif
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime
{
    /// <summary>
    /// Something that happened to a session, queued until RealtimeSession.Dispatch raises the
    /// matching session event on the main thread.
    /// </summary>
    [Preserve]
    public abstract class RealtimeEvent
    {
        internal abstract void DeliverTo(RealtimeSession session);
    }

    /// <summary>An event carried by a room message, with its sequence number and lifetime.</summary>
    [Preserve]
    public abstract class ServerResponseRealtimeEvent : RealtimeEvent
    {
        public readonly uint SequenceNumber;
        public readonly uint LifeTimeMilliSeconds;

        protected ServerResponseRealtimeEvent(uint sequenceNumber, uint lifeTimeMilliSeconds)
        {
            SequenceNumber = sequenceNumber;
            LifeTimeMilliSeconds = lifeTimeMilliSeconds;
        }

        public MessageMetadata Metadata => new MessageMetadata
        {
            SequenceNumber = SequenceNumber,
            LifeTimeMilliSeconds = LifeTimeMilliSeconds,
        };
    }

    [Preserve]
    public sealed class OnMessageEvent : ServerResponseRealtimeEvent
    {
        public readonly BinaryMessage Message;
        public OnMessageEvent(BinaryMessage message, uint sequenceNumber, uint lifeTimeMilliSeconds) : base(sequenceNumber, lifeTimeMilliSeconds) => Message = message;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseMessage(Message, Metadata);
    }

    [Preserve]
    public sealed class OnJoinPlayerEvent : ServerResponseRealtimeEvent
    {
        public readonly Player Player;
        public OnJoinPlayerEvent(Player player, uint sequenceNumber, uint lifeTimeMilliSeconds) : base(sequenceNumber, lifeTimeMilliSeconds) => Player = player;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseJoinPlayer(Player, Metadata);
    }

    [Preserve]
    public sealed class OnLeavePlayerEvent : ServerResponseRealtimeEvent
    {
        public readonly Player Player;
        public OnLeavePlayerEvent(Player player, uint sequenceNumber, uint lifeTimeMilliSeconds) : base(sequenceNumber, lifeTimeMilliSeconds) => Player = player;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseLeavePlayer(Player, Metadata);
    }

    [Preserve]
    public sealed class OnUpdateProfileEvent : ServerResponseRealtimeEvent
    {
        public readonly Player Player;
        public OnUpdateProfileEvent(Player player, uint sequenceNumber, uint lifeTimeMilliSeconds) : base(sequenceNumber, lifeTimeMilliSeconds) => Player = player;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseUpdateProfile(Player, Metadata);
    }

    [Preserve]
    public sealed class OnErrorEvent : ServerResponseRealtimeEvent
    {
        public readonly Error Error;
        public OnErrorEvent(Error error, uint sequenceNumber, uint lifeTimeMilliSeconds) : base(sequenceNumber, lifeTimeMilliSeconds) => Error = error;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseError(Error, Metadata);
    }

    /// <summary>The socket's own error (not a room message).</summary>
    [Preserve]
    public sealed class OnGeneralErrorEvent : RealtimeEvent
    {
        public readonly ErrorEventArgs Error;
        public OnGeneralErrorEvent(ErrorEventArgs error) => Error = error;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseGeneralError(Error);
    }

    [Preserve]
    public sealed class OnCloseEvent : RealtimeEvent
    {
        public readonly CloseEventArgs Error;
        public OnCloseEvent(CloseEventArgs error) => Error = error;
        internal override void DeliverTo(RealtimeSession session) => session.RaiseClose(Error);
    }
}
