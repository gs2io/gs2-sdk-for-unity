using Gs2.Gs2Realtime.Message;

namespace Gs2.Unity.Gs2Realtime
{
    public class MessageMetadata
    {
        public uint SequenceNumber;
        public uint LifeTimeMilliSeconds;
    }

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

    public class OnRelayMessageWithMetadataEvent : OnRelayMessageEvent
    {
        public MessageMetadata Metadata;

        public OnRelayMessageWithMetadataEvent(
            RelayBinaryMessage message,
            MessageMetadata metadata
        ) : base(message)
        {
            Metadata = metadata;
        }
    }

}