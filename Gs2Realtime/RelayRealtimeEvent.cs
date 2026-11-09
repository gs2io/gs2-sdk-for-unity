using Gs2.Gs2Realtime.Message;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime
{
    /// <summary>The sequence number and lifetime (milliseconds) of a room message.</summary>
    [Preserve]
    public class MessageMetadata
    {
        public uint SequenceNumber;
        public uint LifeTimeMilliSeconds;
    }

    /// <summary>A relayed message (RelayRealtimeSession).</summary>
    [Preserve]
    public sealed class OnRelayMessageEvent : RealtimeEvent
    {
        public readonly RelayBinaryMessage Message;
        public readonly MessageMetadata Metadata;
        public OnRelayMessageEvent(RelayBinaryMessage message, MessageMetadata metadata)
        {
            Message = message;
            Metadata = metadata;
        }
        internal override void DeliverTo(RealtimeSession session) => ((RelayRealtimeSession)session).RaiseRelayMessage(Message, Metadata);
    }
}
