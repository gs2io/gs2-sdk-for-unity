using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Exception;
using Gs2.Unity.Gs2Realtime.Util;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using Gs2.Util.WebSocketSharp;

namespace Gs2.Unity.Gs2Realtime
{
    public delegate void OnRelayMessageHandler(RelayBinaryMessage message);
    public delegate void OnRelayMessageWithMetadataHandler(RelayBinaryMessage message, MessageMetadata metadata);
    
    public class RelayRealtimeSession : RealtimeSession
    {
        public event OnRelayMessageHandler OnRelayMessage;
        public event OnRelayMessageWithMetadataHandler OnRelayMessageWithMetadata;

        public RelayRealtimeSession(
            string accessToken, 
            string ipAddress, 
            int port, 
            string encryptionKey,
            ByteString profile = null
        ) : base(
                accessToken, 
                ipAddress, 
                port, 
                encryptionKey,
                profile
            )
        {
            
        }

        protected override void OnMessageHandler(object sender, EventArgs e) 
        {
            
            if (!(e is MessageEventArgs data)) return;
            
            var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = _messenger.Unpack(data.RawData);
            var message = _messenger.Parse(messageType, payload);
            if (message is BinaryMessage binaryMessage)
            {
                _eventQueue.Enqueue(
                    new OnRelayMessageWithMetadataEvent(
                        RelayBinaryMessage.Parser.ParseFrom(binaryMessage.Data),
                        new MessageMetadata
                        {
                            SequenceNumber = sequenceNumber,
                            LifeTimeMilliSeconds = lifeTimeMilliSeconds,
                        }
                    )
                );
            }
            else
            {
                base.OnMessageHandler(sender, e);
            }
        }

        protected override void EventHandler(RealtimeEvent @event)
        {
            base.EventHandler(@event);

            if (@event is RelayRealtimeEvent relayEvent)
            {
                switch (relayEvent.EventType)
                {
                    case RelayRealtimeEventType.OnRelayMessage:
                        OnRelayMessageWithMetadataEvent relayMessageEvent = relayEvent as OnRelayMessageWithMetadataEvent;
                        if (OnRelayMessage != null)
                        {
                            OnRelayMessage.Invoke(relayMessageEvent.Message);
                        }
                        if (OnRelayMessageWithMetadata != null)
                        {
                            OnRelayMessageWithMetadata.Invoke(relayMessageEvent.Message, relayMessageEvent.Metadata);
                        }
                        break;
                }
            }
        }

        public IEnumerator Send(UnityAction<AsyncResult<bool>> callback, ByteString data, uint[] targetConnectionIds = null)
        {
            var relayBinaryMessage = new RelayBinaryMessage
            {
                Data = data
            };
            if (targetConnectionIds != null)
            {
                relayBinaryMessage.TargetConnectionId.AddRange(targetConnectionIds);
            }
            yield return Send(
                callback,
                new BinaryMessage
                {
                    Data = relayBinaryMessage.ToByteString()
                }
            );
        }

        private new IEnumerator Send(UnityAction<AsyncResult<bool>> callback, BinaryMessage message)
        {
            yield return base.Send(callback, message);
        }
    }
}