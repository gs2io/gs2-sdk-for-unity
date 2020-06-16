using Gs2.Gs2Realtime.Message;
using Gs2.Util.WebSocketSharp;

namespace Gs2.Unity.Gs2Realtime
{
    public abstract class RealtimeEvent
    {
        public RealtimeEventType EventType;

        protected RealtimeEvent(RealtimeEventType eventType)
        {
            EventType = eventType;
        }
    }

    public abstract class ServerResponseRealtimeEvent : RealtimeEvent
    {
        public uint SequenceNumber;
        public uint LifeTimeMilliSeconds;

        protected ServerResponseRealtimeEvent(
            RealtimeEventType eventType,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(eventType)
        {
            SequenceNumber = sequenceNumber;
            LifeTimeMilliSeconds = lifeTimeMilliSeconds;
        }

        public MessageMetadata Metadata =>
            new MessageMetadata
            {
                SequenceNumber = SequenceNumber,
                LifeTimeMilliSeconds = LifeTimeMilliSeconds,
            };
    }

    public class OnMessageEvent : ServerResponseRealtimeEvent
    {
        public BinaryMessage Message;

        public OnMessageEvent(
            BinaryMessage message,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(RealtimeEventType.OnMessage, sequenceNumber, lifeTimeMilliSeconds)
        {
            Message = message;
        }
    }

    public class OnJoinPlayerEvent : ServerResponseRealtimeEvent
    {
        public Player Player;

        public OnJoinPlayerEvent(
            Player player,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(RealtimeEventType.OnJoinPlayer, sequenceNumber, lifeTimeMilliSeconds)
        {
            Player = player;
        }
    }

    public class OnLeavePlayerEvent : ServerResponseRealtimeEvent
    {
        public Player Player;

        public OnLeavePlayerEvent(
            Player player,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(RealtimeEventType.OnLeavePlayer, sequenceNumber, lifeTimeMilliSeconds)
        {
            Player = player;
        }
    }

    public class OnUpdateProfileEvent : ServerResponseRealtimeEvent
    {
        public Player Player;

        public OnUpdateProfileEvent(
            Player player,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(RealtimeEventType.OnUpdateProfile, sequenceNumber, lifeTimeMilliSeconds)
        {
            Player = player;
        }
    }

    public class OnErrorEvent : ServerResponseRealtimeEvent
    {
        public Error Error;

        public OnErrorEvent(
            Error error,
            uint sequenceNumber,
            uint lifeTimeMilliSeconds
        ) : base(RealtimeEventType.OnError, sequenceNumber, lifeTimeMilliSeconds)
        {
            Error = error;
        }
    }

    public class OnGeneralErrorEvent : RealtimeEvent
    {
        public ErrorEventArgs Error;

        public OnGeneralErrorEvent(
            ErrorEventArgs error
        ) : base(RealtimeEventType.OnGeneralError)
        {
            Error = error;
        }
    }

    public class OnCloseEvent : RealtimeEvent
    {
        public CloseEventArgs Error;

        public OnCloseEvent(
            CloseEventArgs error
        ) : base(RealtimeEventType.OnClose)
        {
            Error = error;
        }
    }
}