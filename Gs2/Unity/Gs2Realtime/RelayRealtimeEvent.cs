using Gs2.Gs2Realtime.Message;

namespace Gs2.Unity.Gs2Realtime
{
    public class RelayRealtimeEvent : RealtimeEvent
    {
        public new RelayRealtimeEventType EventType;

        public RelayRealtimeEvent(
            RelayRealtimeEventType eventType
        ) : base(RealtimeEventType.PluginEventType)
        {
            EventType = eventType;
        }
    }
    
    public class OnRelayMessageEvent : RelayRealtimeEvent
    {
        public RelayBinaryMessage Message;

        public OnRelayMessageEvent(
            RelayBinaryMessage message
        ) : base(RelayRealtimeEventType.OnRelayMessage)
        {
            Message = message;
        }
    }

}