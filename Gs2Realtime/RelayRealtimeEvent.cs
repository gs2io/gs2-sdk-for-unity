using Gs2.Gs2Realtime.Message;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime
{
    [Preserve]
    public class MessageMetadata
    {
        public uint SequenceNumber;
        public uint LifeTimeMilliSeconds;
    }

    [Preserve]
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
    
    [Preserve]
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

    [Preserve]
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