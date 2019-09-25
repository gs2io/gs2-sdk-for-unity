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
using WebSocketSharp;

namespace Gs2.Unity.Gs2Realtime
{
    public delegate void OnRelayMessageHandler(RelayBinaryMessage message);
    
    public class RelayRealtimeSession : RealtimeSession
    {
        public event OnRelayMessageHandler OnRelayMessage;

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
            
            var (messageType, payload) = _messenger.Unpack(data.RawData);
            var message = _messenger.Parse(messageType, payload);
            if (message is BinaryMessage binaryMessage)
            {
                if (OnRelayMessage != null)
                {
                    _eventQueue.Enqueue(
                        new OnRelayMessageEvent(
                            RelayBinaryMessage.Parser.ParseFrom(binaryMessage.Data)
                        )
                    );
                }
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
                        if (OnRelayMessage != null)
                        {
                            OnRelayMessage.Invoke((relayEvent as OnRelayMessageEvent).Message);
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