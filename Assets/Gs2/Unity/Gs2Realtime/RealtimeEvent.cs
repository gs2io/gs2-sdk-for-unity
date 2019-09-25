using Gs2.Gs2Realtime.Message;
using WebSocketSharp;

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

    public class OnMessageEvent : RealtimeEvent
    {
        public BinaryMessage Message;

        public OnMessageEvent(
            BinaryMessage message
        ) : base(RealtimeEventType.OnMessage)
        {
            Message = message;
        }
    }

    public class OnJoinPlayerEvent : RealtimeEvent
    {
        public Player Player;

        public OnJoinPlayerEvent(
            Player player
        ) : base(RealtimeEventType.OnJoinPlayer)
        {
            Player = player;
        }
    }

    public class OnLeavePlayerEvent : RealtimeEvent
    {
        public Player Player;

        public OnLeavePlayerEvent(
            Player player
        ) : base(RealtimeEventType.OnLeavePlayer)
        {
            Player = player;
        }
    }

    public class OnUpdateProfileEvent : RealtimeEvent
    {
        public Player Player;

        public OnUpdateProfileEvent(
            Player player
        ) : base(RealtimeEventType.OnUpdateProfile)
        {
            Player = player;
        }
    }

    public class OnErrorEvent : RealtimeEvent
    {
        public Error Error;

        public OnErrorEvent(
            Error error
        ) : base(RealtimeEventType.OnError)
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